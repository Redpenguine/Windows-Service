using System;
using UnityEngine;

namespace Redpenguin.UiManager.UIWindows.Animations
{
  public abstract class OpenCloseAnimation : MonoBehaviour, IOpenCloseAnimation
  {
    public abstract float OpenDuration { get; }
    public abstract float CloseDuration { get; }
    public abstract void AnimateOpen(Action onComplete = null);
    public abstract void AnimateClose(Action onComplete = null);
    
  }
}