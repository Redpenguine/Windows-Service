using System;
using TMPro;
using UIManagement.UIWindows.Windows;
using UnityEngine;

namespace Redpenguin.UiManager.Windows
{
  public class WindowError : PayloadWindow<ErrorType>
  {
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI infoText;

    protected override void OnOpening(ErrorType payload)
    {
      switch (payload)
      {
        case ErrorType.NoInternet:
          NoInternet();
          break;
        case ErrorType.NoVideo:
          NoVideo();
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(payload), payload, null);
      }
    }

    private void NoInternet()
    {
      titleText.SetText("Error");
      infoText.SetText("No internet connection");
    }
    private void NoVideo()
    {
      titleText.SetText("Reward video");
      infoText.SetText("No video available!");
    }
  }
}