using System;
using DG.Tweening;
using Redpenguin.UiManager;
using UIManagement.UIWindows.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace UIManagement.UIWindows
{
    [RequireComponent(typeof(Image))]
    public class Darken : MonoBehaviour
    {
        [SerializeField] private Color _baseColor = Color.black;
        //[SerializeField] private Color _baseColor = new Color32(235, 235, 235, 255);
        [SerializeField] private float _alpha = 0.8f;
        //[SerializeField] private float _alpha = 1f;
        [SerializeField] private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void AnimateShow(float duration)
        {
            _image.color = _baseColor.WithAlpha(0);
            _image.DOColor(_baseColor.WithAlpha(_alpha), duration);
        }

        public void AnimateHide(float duration)
        {
            _image.DOColor(_baseColor.WithAlpha(0), duration).OnComplete(() => Destroy(gameObject));
        }
    }
}