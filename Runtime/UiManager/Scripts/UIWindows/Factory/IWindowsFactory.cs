using Cysharp.Threading.Tasks;
using Demos.Scripts;
using Redpenguin.UiManager.UIWindows;
using Redpenguin.UiManager.UIWindows.Windows;
using UIManagement.UIWindows.Data;
using UIManagement.UIWindows.Windows;

namespace UIManagement.UIWindows.Factory
{
    public interface IWindowsFactory : IService
    {
        Darken CreateDarken();
        TWindow CreateWindow<TWindow>() where TWindow : BaseWindow;
        void CreateWindowsRoot();
        void SetWindowsService(IWindowsService windowsService);
    }
}