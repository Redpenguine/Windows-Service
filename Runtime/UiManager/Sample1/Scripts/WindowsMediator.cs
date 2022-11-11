using NaughtyAttributes;
using Redpenguin.UiManager.Sample1.ExamplesWindows;
using Redpenguin.UiManager.UIWindows;
using UnityEngine;
using Zenject;

namespace Redpenguin.UiManager.Sample1
{
  public class WindowsMediator : MonoBehaviour
  {
    [Inject] private IWindowsService _windowsService;

    [Button()]
    private void OpenErrorInCurrent()
    {
      _windowsService.OpenInCurrentLayer<ExampleWindowError>();
    }
    [Button()]
    private void OpenErrorInHigher()
    {
      _windowsService.OpenInHigherLayer<ExampleWindowError>();
    }
    [Button()]
    private void OpenEx1InCurrent()
    {
      _windowsService.OpenInCurrentLayer<ExampleWindow1>();
    }
    [Button()]
    private void OpenEx1InHigher()
    {
      _windowsService.OpenInHigherLayer<ExampleWindow1>();
    }
    [Button()]
    private void OpenEx2InCurrent()
    {
      _windowsService.OpenInCurrentLayer<ExampleWindow2>();
    }
    [Button()]
    private void OpenEx2InHigher()
    {
      _windowsService.OpenInHigherLayer<ExampleWindow2>();
    }
  }
}