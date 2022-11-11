using System;
using DG.Tweening;
using UIManagement.UIWindows.Animations;
using UnityEngine;

namespace Redpenguin.UiManager.UIWindows.Animations.AnimationTypes
{
    public class AnimationFadeAndScaleWithPanch :  IOpenCloseAnimation
    {
        private readonly CanvasGroup _canvasGroup;
        private readonly Transform _transform;
        private Sequence mySq;

        public AnimationFadeAndScaleWithPanch(Transform transform)
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
            _canvasGroup.DOFade(1, OpenDuration).SetEase(Ease.OutExpo);
            mySq = DOTween.Sequence();
            mySq.Append(_transform.DOScale(Vector3.one, OpenDuration));
            mySq.Append(_transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.25f, 1, 0).SetEase(Ease.InExpo).OnComplete(() => onComplete?.Invoke()));
        }

        public void AnimateClose(Action onComplete = null)
        {
            mySq?.Kill();
            _transform.DOScale(Vector3.one * 0.05f, CloseDuration).SetEase(Ease.InExpo);
            _canvasGroup.DOFade(0, CloseDuration).SetEase(Ease.InExpo).OnComplete(() => onComplete?.Invoke());
        }
    }
}