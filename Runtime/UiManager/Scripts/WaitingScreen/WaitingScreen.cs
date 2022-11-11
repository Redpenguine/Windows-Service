using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UIManagement.WaitingScreen
{
    public class WaitingScreen : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        public event Action Destroyed;
    
        private void Awake()
        {
            _closeButton.gameObject.SetActive(false);
        }

        public void Construct(WaitingScreenConfiguration configuration)
        {
            if (configuration.WithCloseButton)
            {
                StartCoroutine(ShowCloseButton(configuration.CloseButtonShowingDelay, configuration.OnUserClosed)); 
            }
            StartCoroutine(AutoCloseWithDelay(configuration.AutoCloseDelay, configuration.OnAutoClose));
        }

        public void Close()
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }

        private IEnumerator ShowCloseButton(float closeButtonDelay, Action onUserClosed)
        {
            yield return new WaitForSeconds(closeButtonDelay);
            _closeButton.gameObject.SetActive(true);
            _closeButton.onClick.AddListener(() => OnCloseClick(onUserClosed));
        }

        private void OnCloseClick(Action onUserClosed)
        {
            onUserClosed?.Invoke();
            Close();
        }

        private IEnumerator AutoCloseWithDelay(float timeout, Action onAutoClose)
        {
            yield return new WaitForSeconds(timeout);
            onAutoClose?.Invoke();
            Close();
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke();
            _closeButton.onClick.RemoveAllListeners();
        }
    }
}
