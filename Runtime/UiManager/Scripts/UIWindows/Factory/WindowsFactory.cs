using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Redpenguin.UiManager;
using Redpenguin.UiManager.UIWindows;
using Redpenguin.UiManager.UIWindows.Windows;
using UIManagement.UIWindows.Animations;
using UIManagement.UIWindows.Data;
using UIManagement.UIWindows.Windows;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace UIManagement.UIWindows.Factory
{
  public class WindowsFactory : IWindowsFactory
  {
    private string WindowsDataPath => "UIManagement/Windows/Configs";
    private Dictionary<Type, BaseWindow> _windowPrefabs = new Dictionary<Type, BaseWindow>();
    private IWindowsService _windowsService;
    private readonly IOpenCloseAnimationsFactory _animationsFactory;
    private readonly IUIRootFactory _uiRootFactory;
    private Transform _windowsRoot;
    private DiContainer _diContainer;

    public WindowsFactory(IUIRootFactory uiRootFactory, IOpenCloseAnimationsFactory animationsFactory,
      DiContainer diContainer)
    {
      _diContainer = diContainer;
      _uiRootFactory = uiRootFactory;
      _animationsFactory = animationsFactory;
      Load();
    }

    public void SetWindowsService(IWindowsService windowsService)
    {
      _windowsService = windowsService;
    }

    public UniTask<TWindow> CreateWindowAsync<TWindow>() where TWindow : BaseWindow
    {
      throw new NotImplementedException();
    }

    public void CreateWindowsRoot()
    {
      _windowsService.Cleanup();
      _windowsRoot = new GameObject(nameof(WindowsFactory), typeof(RectTransform)).transform;
      _windowsRoot.SetParent(_uiRootFactory.UIRoot());
      _windowsRoot.gameObject.ToFullScreenRect();
    }

    public Darken CreateDarken()
    {
      Darken darker = new GameObject(nameof(Darken)).AddComponent<Darken>();
      darker.transform.SetParent(_windowsRoot);
      darker.transform.SetSiblingIndex(Mathf.Clamp(_windowsRoot.childCount - 2, 0, _windowsRoot.childCount - 1));
      //darker.gameObject.ToFullScreenRect();
      darker.gameObject.SetScreenRect(-200);
      return darker;
    }

    public TWindow CreateWindow<TWindow>() where TWindow : BaseWindow
    {
      //var go = Object.Instantiate(_windowPrefabs[typeof(TWindow)], _windowsRoot);
       var go = _diContainer.InstantiatePrefab(_windowPrefabs[typeof(TWindow)], Vector3.zero,
         Quaternion.identity, _windowsRoot);
      var baseWindow = go.GetComponent<TWindow>();
      baseWindow.Construct(_windowsService, _animationsFactory);
      baseWindow.transform.SetAsLastSibling();
      return baseWindow;
    }

    private void Load()
    {
      _windowPrefabs = Resources.Load<WindowsStaticData>(WindowsDataPath)
        .Windows
        .ToDictionary(x => x.GetType(), x => x);
    }
  }
}