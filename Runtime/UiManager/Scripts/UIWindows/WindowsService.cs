using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Redpenguin.UiManager.TouchLocker;
using Redpenguin.UiManager.UIWindows.Windows;
using UIManagement.UIWindows;
using UIManagement.UIWindows.Extensions;
using UIManagement.UIWindows.Factory;
using UIManagement.UIWindows.Windows;
using UnityEngine;

namespace Redpenguin.UiManager.UIWindows
{
  public class WindowsService : IWindowsService
  {
    private readonly ITouchLocker _touchLocker;
    private readonly Stack<BaseWindow> _windows = new Stack<BaseWindow>();
    private readonly Dictionary<Type, BaseWindow> _cacheWindows = new Dictionary<Type, BaseWindow>();
    private readonly Stack<Darken> _darkens = new Stack<Darken>();
    private readonly IWindowsFactory _factory;
    private readonly int _maxLayersCount;

    public WindowsService(IWindowsFactory windowsFactory, ITouchLocker touchLocker, int maxLayersCount)
    {
      _touchLocker = touchLocker;
      _factory = windowsFactory;
      _maxLayersCount = maxLayersCount;
      _factory.SetWindowsService(this);
    }

    public event Action<BaseWindow> OnWindowOpen;
    public event Action OnWindowClose;

    public BaseWindow TopmostWindow()
    {
      return AnyWindowIsOpen() ? _windows.Peek() : null;
    }

    public void Cleanup()
    {
      _windows.Clear();
      _darkens.Clear();
    }

    public void OpenInCurrentLayer<TWindow>(bool isOnce = false) where TWindow : Window
    {
      if (isOnce && WasTheWindowOpen<TWindow>()) return;
      OpenWindow(AnyWindowIsOpen(), CreateWindow<TWindow>);
      WindowOpeningTracking<TWindow>();
    }

    public void OpenInHigherLayer<TWindow>(bool isOnce = false) where TWindow : Window
    {
      if (isOnce && WasTheWindowOpen<TWindow>()) return;
      OpenWindow(StackIsFull(), CreateWindow<TWindow>);
      WindowOpeningTracking<TWindow>();
    }

    public void OpenInCurrentLayer<TWindow, TPayload>(TPayload payload, bool isOnce = false)
      where TWindow : PayloadWindow<TPayload>
    {
      if (isOnce && WasTheWindowOpen<TWindow, TPayload>()) return;
      OpenWindow(AnyWindowIsOpen(), () => CreatePayloadWindow<TWindow, TPayload>(payload));
      WindowOpeningTracking<TWindow, TPayload>();
    }

    public void OpenInHigherLayer<TWindow, TPayload>(TPayload payload, bool isOnce = false)
      where TWindow : PayloadWindow<TPayload>
    {
      if (isOnce && WasTheWindowOpen<TWindow, TPayload>()) return;
      OpenWindow(StackIsFull(),
        () => CreatePayloadWindow<TWindow, TPayload>(payload));
      WindowOpeningTracking<TWindow, TPayload>();
    }

    public void CloseTopmostWindow(Action onClosed = null)
    {
      if (CanClose() == false) return;
      _touchLocker.Lock();
      _darkens.Pop().AnimateHide(TopmostWindow().CloseDuration());
      _windows.Pop().Close(() =>
      {
        _touchLocker.Unlock();
        onClosed?.Invoke();
      });
      OnWindowClose?.Invoke();
    }

    public void CloseWindow<TWindow>(TWindow window, Action onClosed = null) where TWindow : Window
    {
      var windowsList = _windows.ToList();
      var index = windowsList.IndexOf(window);
      _darkens.ToList()[index].AnimateHide(TopmostWindow().CloseDuration());
      windowsList[index].Close(() => { onClosed?.Invoke(); });
    }

    public void CloseWindow<TWindow, TPayload>(TWindow window, Action onClosed = null)
      where TWindow : PayloadWindow<TPayload>
    {
      var windowsList = _windows.ToList();
      var index = windowsList.IndexOf(window);
      _darkens.ToList()[index].AnimateHide(TopmostWindow().CloseDuration());
      windowsList[index].Close(() => { onClosed?.Invoke(); });
    }

    public bool IsWindowBlocked<TWindow>() where TWindow : Window
    {
      if (_windows.Count == 0)
        return false;
      return _windows.Peek() is not TWindow;
    }

    public bool CurrentOpen<TWindow>() where TWindow : Window
    {
      if (_windows.Count == 0)
        return false;
      return _windows.Peek() is TWindow;
    }

    private void OpenWindow(bool hideCurrent, Action createNextWindow)
    {
      if (_touchLocker.IsLocked) return;
      _touchLocker.Lock();
      if (hideCurrent) _windows.Pop().Close(createNextWindow);
      else
      {
        createNextWindow();
        ShowDarken();
      }
    }

    private void CreateWindow<TWindow>() where TWindow : Window
    {
      TWindow window;
      if (_cacheWindows.ContainsKey(typeof(TWindow)))
      {
        window = (TWindow) _cacheWindows[typeof(TWindow)];
      }
      else
      {
        window = _factory.CreateWindow<TWindow>();
        _cacheWindows.Add(typeof(TWindow), window);
      }
      window.Open(() => _touchLocker.Unlock());
      _windows.Push(window);
      window.transform.SetAsLastSibling();
      OnWindowOpen?.Invoke(window);
    }

    private void CreatePayloadWindow<TWindow, TPayload>(TPayload payload) where TWindow : PayloadWindow<TPayload>
    {
      TWindow window;
      if (_cacheWindows.ContainsKey(typeof(TWindow)))
      {
        window = (TWindow) _cacheWindows[typeof(TWindow)];
      }
      else
      {
        window = _factory.CreateWindow<TWindow>();
        _cacheWindows.Add(typeof(TWindow), window);
      }
      window.Open(payload, () => _touchLocker.Unlock());
      _windows.Push(window);
      window.transform.SetAsLastSibling();
      OnWindowOpen?.Invoke(window);
    }

    private void ShowDarken()
    {
      Darken darken = _factory.CreateDarken();
      darken.AnimateShow(TopmostWindow().OpenDuration());
      _darkens.Push(darken);
    }

    private bool CanClose()
    {
      //return AnyWindowIsOpen() && _touchLocker.IsLocked == false;
      return AnyWindowIsOpen();
    }

    public async UniTask WaitUntilCanOpen(bool withWindow = true)
    {
      if (withWindow)
      {
        await UniTask.WaitUntil(() => _touchLocker.IsLocked == false && TopmostWindow() == null);
      }
      else
      {
        await UniTask.WaitUntil(() => _touchLocker.IsLocked == false);
      }
    }

    public bool CanOpen()
    {
      return _touchLocker.IsLocked == false && TopmostWindow() == null;
      //return TopmostWindow() == null;
    }

    private void WindowOpeningTracking<TWindow>() where TWindow : Window
    {
      PlayerPrefs.SetInt(typeof(TWindow).ToString(), 1);
    }

    private void WindowOpeningTracking<TWindow, TPayload>() where TWindow : PayloadWindow<TPayload>
    {
      var name = $"{typeof(TWindow)}-{typeof(TPayload)}";
      PlayerPrefs.SetInt(name, 1);
    }

    public bool WasTheWindowOpen<TWindow>() where TWindow : Window
    {
      var name = $"{typeof(TWindow)}";
      var value = PlayerPrefs.HasKey(name);
      Debug.Log($"WasTheWindowOpen {name} {value}");
      return value;
    }

    public bool WasTheWindowOpen<TWindow, TPayload>() where TWindow : PayloadWindow<TPayload>
    {
      var name = $"{typeof(TWindow)}-{typeof(TPayload)}";
      var value = PlayerPrefs.HasKey(name);
      Debug.Log($"WasTheWindowOpen {name} {value}");
      return value;
    }

    private bool StackIsFull() => _windows.Count == _maxLayersCount;

    private bool AnyWindowIsOpen() => _windows.Count > 0;
  }
}