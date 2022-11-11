using System;
using System.Collections.Generic;
using Redpenguin.UiManager.UIWindows;
using UIManagement.WaitingScreen;
using UnityEngine;
using Zenject;

namespace Redpenguin.UiManager
{
    public class EscapeButtonListener : MonoBehaviour
    {
        private IWindowsService _windowsService;
        private IWaitingScreenService _waitingScreenService;
        private List<Action> _stack;
        private void Awake()
        {
            //DontDestroyOnLoad(this);
        }

        [Inject]
        private void Construct(IWindowsService windowsService, IWaitingScreenService waitingScreenService)
        {
            _windowsService = windowsService;
            _waitingScreenService = waitingScreenService;
            _stack = new List<Action>();
        }
        
        public void Add(Action action)
        {
            _stack.Add(action);
        }
        
        public void Remove(Action action)
        {
            _stack.Remove(action);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_stack.Count != 0)
                { 
                    var action = _stack[0];
                    action.Invoke();
                    _stack.Remove(action);
                }
                else if (CanCloseWindow())
                {
                    _windowsService.CloseTopmostWindow();
                }
            }
        }

        private bool CanCloseWindow()
        {
            return _waitingScreenService.IsOpen == false && TopmostWindowIsClosable();
        }

        private bool TopmostWindowIsClosable()
        {
           return _windowsService?.TopmostWindow() != null && _windowsService.TopmostWindow().ClosableFromUser;
        }
    }
}