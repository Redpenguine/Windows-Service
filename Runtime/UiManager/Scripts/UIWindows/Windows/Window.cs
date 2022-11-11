using System;

namespace Redpenguin.UiManager.UIWindows.Windows
{
    public abstract class Window : BaseWindow
    {
        public void Open(Action onComplete = null)
        {
            gameObject.SetActive(true);
            OnOpening();
            PlayOpening(OnComplete);

            void OnComplete()
            {
                onComplete?.Invoke();
                OnOpened();
            }
        }

        protected virtual void OnOpened()
        {
            
        }

        protected virtual void OnOpening()
        {
            
        }

    }
}