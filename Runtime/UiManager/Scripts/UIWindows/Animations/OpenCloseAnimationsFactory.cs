using Redpenguin.UiManager.UIWindows.Animations.AnimationTypes;
using UIManagement.UIWindows.Animations;
using UnityEngine;

namespace Redpenguin.UiManager.UIWindows.Animations
{
    public class OpenCloseAnimationsFactory : IOpenCloseAnimationsFactory
    {
        public IOpenCloseAnimation Create(Transform container, AnimationId id)
        {
            switch (id)
            {
                case AnimationId.None : return null;
                case AnimationId.FadeAndScale : return new AnimationFadeAndScale(container);
                case AnimationId.Fade : return new AnimationFade(container);
                case AnimationId.TranslateFromLeft : return new AnimationTranslateFromLeft(container);
                case AnimationId.TranslateFromTopToDown : return new AnimationTranslateFromTopToDown(container);
                case AnimationId.FadeAndScaleWithPunch : return new AnimationFadeAndScaleWithPanch(container);
                case AnimationId.CustomAnimation : return null;
                default: return null;
            }
        }
    }
}