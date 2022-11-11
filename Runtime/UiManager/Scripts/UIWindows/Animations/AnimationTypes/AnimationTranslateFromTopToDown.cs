using System;
using DG.Tweening;
using UIManagement.UIWindows.Animations;
using UnityEngine;

namespace Redpenguin.UiManager.UIWindows.Animations.AnimationTypes
{
    public class AnimationTranslateFromTopToDown : IOpenCloseAnimation
    {

        private readonly Transform _transform;

        public AnimationTranslateFromTopToDown(Transform transform)
        {
            _transform = transform;
        }

        public float OpenDuration => 0.35f;
        public float CloseDuration => 0.25f;

        public void AnimateOpen(Action onComplete = null)
        {
            _transform.localPosition = new Vector3(0, ((RectTransform)_transform).rect.size.y, 1);
            _transform.DOLocalMoveY(0, OpenDuration).OnComplete(() => onComplete?.Invoke());
        }

        public void AnimateClose(Action onComplete = null)
        {
            _transform.DOLocalMoveY(-((RectTransform)_transform).rect.size.y, OpenDuration).SetEase(Ease.InExpo).OnComplete(() => onComplete?.Invoke());
        }
    }
}