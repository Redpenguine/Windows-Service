using System;
using DG.Tweening;
using UIManagement.UIWindows.Animations;
using UnityEngine;

namespace Redpenguin.UiManager.UIWindows.Animations.AnimationTypes
{
    public class AnimationFade : IOpenCloseAnimation
    {
        private readonly CanvasGroup _canvasGroup;
        private readonly Transform _transform;

        public AnimationFade(Transform transform)
        {
            _transform = transform;
            if (_transform.gameObject.TryGetComponent(out _canvasGroup) == false)
            {
                _canvasGroup = _transform.gameObject.AddComponent<CanvasGroup>() ; 
            }
        }

        public float OpenDuration => 0.45f;
        public float CloseDuration => 0.3f;

        public void AnimateOpen(Action onComplete = null)
        {
            _transform.localPosition = Vector3.zero;
            _transform.localScale = Vector3.one;
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, OpenDuration).SetEase(Ease.OutExpo).OnComplete(() => onComplete?.Invoke());
        }

        public void AnimateClose(Action onComplete = null)
        {
            _canvasGroup.DOFade(0, CloseDuration).SetEase(Ease.InExpo).OnComplete(() => onComplete?.Invoke());
        }
    }
}