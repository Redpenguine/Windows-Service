using System;

namespace Redpenguin.UiManager.UIWindows.Animations
{
    public interface IOpenCloseAnimation
    {
        void AnimateOpen(Action onComplete = null);
        void AnimateClose(Action onComplete = null);
        float OpenDuration { get; }
        float CloseDuration { get; }
    }
}