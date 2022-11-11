using Redpenguin.UiManager.TouchLocker;
using Redpenguin.UiManager.UIWindows;
using Redpenguin.UiManager.UIWindows.Animations;
using UIManagement;
using UIManagement.UIWindows.Animations;
using UIManagement.UIWindows.Factory;
using UIManagement.WaitingScreen;
using Zenject;

namespace Redpenguin.UiManager
{
  public static class UiManagerExtensions
  {
    public static DiContainer BindUiServices(this DiContainer diContainer)
    {
      diContainer.Bind<IUIRootFactory>().To<UIRootFactory>().AsSingle();
      //diContainer.Bind<IOverlayUIRootFactory>().To<OverlayUIRootFactory>().AsSingle();
      diContainer.Bind<ITouchLocker>().To<EventSystemToggle>().AsSingle();
      diContainer.Bind<IOpenCloseAnimationsFactory>().To<OpenCloseAnimationsFactory>().AsSingle();
      diContainer.Bind<IWindowsFactory>().To<WindowsFactory>().AsSingle();
      diContainer.Bind<IWindowsService>().To<WindowsService>().AsSingle().WithArguments(4);
      diContainer.Bind<IWaitingScreenService>().To<WaitingScreenService>().AsSingle();
      diContainer.Bind<EscapeButtonListenerFactory>().AsSingle();
      diContainer.BindInterfacesAndSelfTo<EscapeButtonService>().AsSingle();
      diContainer.BindInterfacesAndSelfTo<WindowsBootstrapper>().AsSingle().NonLazy();
      return diContainer;
    }
  }
}