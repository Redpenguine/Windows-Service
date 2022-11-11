using System;
using Cysharp.Threading.Tasks;
using Demos.Scripts;
using Redpenguin.UiManager.UIWindows.Windows;
using UIManagement.UIWindows.Windows;

namespace Redpenguin.UiManager.UIWindows
{
    public interface IWindowsService  : IService
    {
        event Action<BaseWindow> OnWindowOpen;
        event Action OnWindowClose;
        BaseWindow TopmostWindow();
        void Cleanup();
        void OpenInCurrentLayer<TWindow>(bool isOnce = false) where TWindow : Window;
        void OpenInHigherLayer<TWindow>(bool isOnce = false) where TWindow : Window;
        void OpenInCurrentLayer<TWindow, TPayload>(TPayload payload, bool isOnce = false) where TWindow : PayloadWindow<TPayload>;
        void OpenInHigherLayer<TWindow, TPayload>(TPayload payload, bool isOnce = false) where TWindow : PayloadWindow<TPayload>;
        void CloseTopmostWindow(Action onClosed = null);
        void CloseWindow<TWindow>(TWindow window, Action onClosed = null) where TWindow : Window;
        UniTask WaitUntilCanOpen(bool withWindow = true);
        bool CanOpen();
        bool WasTheWindowOpen<TWindow>() where TWindow : Window;
        bool WasTheWindowOpen<TWindow, TPayload>() where TWindow : PayloadWindow<TPayload>;
        bool IsWindowBlocked<TWindow>() where TWindow : Window;
        bool CurrentOpen<TWindow>() where TWindow : Window;
        void CloseWindow<TWindow, TPayload>(TWindow window, Action onClosed = null) where TWindow : PayloadWindow<TPayload>;
    }
}