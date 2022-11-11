using Redpenguin.UiManager;
using UnityEngine;

namespace UIManagement.WaitingScreen
{
    public class WaitingScreenService :  IWaitingScreenService
    {
        private WaitingScreen _currentScreen;
        private readonly IWaitingScreenFactory _waitingScreenFactory;
        private readonly IUIRootFactory _uiRootFactory;

        public WaitingScreenService(IUIRootFactory uiRootFactory)
        {
            _waitingScreenFactory = new WaitingScreenFactory();
            _uiRootFactory = uiRootFactory;
        }

        public bool IsOpen { get; private set; }

        public void Open(WaitingScreenConfiguration configuration)
        {
            if (IsOpen) return;
            _currentScreen = _waitingScreenFactory.CreateWaitingScreen(_uiRootFactory.UIRoot());
            _currentScreen.Construct(configuration);
            _currentScreen.Destroyed += OnScreenDestroyed;
            IsOpen = true;
        }

        public void Close()
        {
            if (!IsOpen) return;
            _currentScreen.Close();
        }

        private void OnScreenDestroyed()
        {
            if (_currentScreen)
            {
                _currentScreen.Destroyed -= OnScreenDestroyed;
            }
            IsOpen = false;
        }
    }
}