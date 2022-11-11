using Redpenguin.UiManager.UIWindows.Windows;
using UIManagement.UIWindows.Animations;
using UIManagement.UIWindows.Windows;

namespace UIManagement.UIWindows.Extensions
{
    public static class WindowExtensions
    {
        public static float OpenDuration(this BaseWindow baseWindow) => baseWindow.Animation?.OpenDuration ?? 0;

        public static float CloseDuration(this BaseWindow baseWindow) => baseWindow.Animation?.CloseDuration ?? 0;
    }
}