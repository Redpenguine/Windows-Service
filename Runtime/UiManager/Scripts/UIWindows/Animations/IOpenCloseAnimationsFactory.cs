using Redpenguin.UiManager.UIWindows.Animations;
using Redpenguin.UiManager.UIWindows.Animations.AnimationTypes;
using UnityEngine;

namespace UIManagement.UIWindows.Animations
{
    public interface IOpenCloseAnimationsFactory
    {
        IOpenCloseAnimation Create(Transform container, AnimationId id);
    }
}