using UIManagement.UIWindows.Factory;
using UIManagement.WaitingScreen;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Redpenguin.UiManager
{
  public class WindowsBootstrapper : IInitializable
  {
    private readonly IUIRootFactory _uiRootFactory;
    private readonly IWindowsFactory _windowsFactory;

    public WindowsBootstrapper
    (
      IUIRootFactory uiRootFactory,
      IWindowsFactory windowsFactory
    )
    {
      _windowsFactory = windowsFactory;
      _uiRootFactory = uiRootFactory;
    }

    private void InstantiateUI()
    {
      _uiRootFactory.CreateUIRoot();
      _windowsFactory.CreateWindowsRoot();
    }

    public void Initialize()
    {
      InstantiateUI();
    }
  }
}