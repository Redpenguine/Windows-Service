using System;
using DG.Tweening;
using UIManagement.UIWindows.Animations;
using UnityEngine;

namespace Redpenguin.UiManager.UIWindows.Animations.AnimationTypes
{
    public class AnimationTranslateFromLeft : IOpenCloseAnimation
    {

        private readonly Transform _transform;

        public AnimationTranslateFromLeft(Transform transform)
        {
            _transform = transform;
        }

        public float OpenDuration => 0.35f;
        public float CloseDuration => 0.25f;

        public void AnimateOpen(Action onComplete = null)
        {
            _transform.localPosition = new Vector3(-((RectTransform) _transform).rect.size.x , 0, 1);
            _transform.DOLocalMoveX(0, OpenDuration).OnComplete(() => onComplete?.Invoke());
        }

        public void AnimateClose(Action onComplete = null)
        {
            _transform.DOLocalMoveX(((RectTransform) _transform).rect.size.x, OpenDuration).SetEase(Ease.InExpo).OnComplete(() => onComplete?.Invoke());
        }
    }
}