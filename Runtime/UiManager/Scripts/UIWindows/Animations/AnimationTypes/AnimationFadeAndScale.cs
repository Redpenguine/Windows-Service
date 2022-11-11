using System;
using DG.Tweening;
using UIManagement.UIWindows.Animations;
using UnityEngine;

namespace Redpenguin.UiManager.UIWindows.Animations.AnimationTypes
{
    public class AnimationFadeAndScale :  IOpenCloseAnimation
    {
        private readonly CanvasGroup _canvasGroup;
        private readonly Transform _transform;

        public AnimationFadeAndScale(Transform transform)
        {
            _transform = transform;
            if (_transform.gameObject.TryGetComponent(out _canvasGroup) == false)
            {
                _canvasGroup = _transform.gameObject.AddComponent<CanvasGroup>() ; 
            }
         
        }

        public float OpenDuration => 0.5f;
        public float CloseDuration => 0.25f;

        public void AnimateOpen(Action onComplete = null)
        {
            _transform.localPosition = Vector3.zero;
            _transform.localScale = Vector3.one * 0.05f;
            _canvasGroup.alpha = 0;
            _transform.DOScale(Vector3.one, OpenDuration).SetEase(Ease.OutExpo);
            _canvasGroup.DOFade(1, OpenDuration).SetEase(Ease.OutExpo).OnComplete(() => onComplete?.Invoke());
        }

        public void AnimateClose(Action onComplete = null)
        {
            _transform.DOScale(Vector3.one * 0.05f, CloseDuration).SetEase(Ease.InExpo);
            _canvasGroup.DOFade(0, CloseDuration).SetEase(Ease.InExpo).OnComplete(() => onComplete?.Invoke());
        }
    }
}