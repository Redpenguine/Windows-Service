using UIManagement;
using UnityEngine;

namespace Redpenguin.UiManager.TouchLocker
{
    public class EventSystemToggle : ITouchLocker
    {
        private readonly IUIRootFactory _uiRootFactory;

        public EventSystemToggle(IUIRootFactory uiRootFactory)
        {
            _uiRootFactory = uiRootFactory;
        }

        public void Lock()
        {
            Debug.Log("Lock");
            _uiRootFactory.EventSystem.enabled = false;
        }

        public void Unlock()
        {
            _uiRootFactory.EventSystem.enabled = true;
        }

        public bool IsLocked => _uiRootFactory.EventSystem.enabled == false;
    }
}