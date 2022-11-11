using System;
using Redpenguin.UiManager.UIWindows.Windows;

namespace UIManagement.UIWindows.Windows
{
    public abstract class PayloadWindow<T> : BaseWindow
    {
        public void Open(T payload, Action onComplete = null)
        {
            gameObject.SetActive(true);
            OnOpening(payload);
            PlayOpening(onComplete);
        }
        
        protected virtual void OnOpening(T payload)
        {
            
        }      

    }
}