using Zenject;

namespace Redpenguin.UiManager
{
  public class WindowsServiceInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.BindUiServices();
    }
  }
}