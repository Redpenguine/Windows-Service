using System;

namespace UIManagement.WaitingScreen
{
    public class WaitingScreenConfiguration
    {
        private const int DefaultAutoCloseTime = 10;
        
        public float AutoCloseDelay { get; private set; }

        public Action OnAutoClose { get; private set; }

        public bool WithCloseButton { get; private set; }

        public Action OnUserClosed { get; private set; }

        public float CloseButtonShowingDelay { get; private set; }

        private WaitingScreenConfiguration(Builder builder)
        {
            AutoCloseDelay = builder.AutoCloseDelay;
            OnAutoClose = builder.OnAutoClose;
            WithCloseButton = builder.WithCloseButton;
            OnUserClosed = builder.OnUserClosed;
            CloseButtonShowingDelay = builder.CloseButtonShowingDelay;
        }

        public class Builder
        {
            public float AutoCloseDelay { get; private set; }
            public Action OnAutoClose { get; private set; }
            public bool WithCloseButton { get; private set; }
            public Action OnUserClosed { get; private set; }
            public float CloseButtonShowingDelay { get; private set; }

            public Builder()
            {
                AutoCloseDelay = DefaultAutoCloseTime;
                OnAutoClose = null;
                WithCloseButton = false;
                OnUserClosed = null;
                CloseButtonShowingDelay = 0;
            }

            public Builder SetAutoCloseTime(float timeout)
            {
                AutoCloseDelay = timeout;
                return this;
            }

            public Builder SetOnAutoCloseAction(Action onTimeout)
            {
                OnAutoClose = onTimeout;
                return this;
            }

            public Builder SetWithCloseButton(float delay)
            {
                WithCloseButton = true;
                CloseButtonShowingDelay = delay;
                return this;
            }

            public Builder SetOnUserClosedAction(Action onUserClosed)
            {
                OnUserClosed = onUserClosed;
                return this;
            }

            public WaitingScreenConfiguration Build()
            {
                return new WaitingScreenConfiguration(this);
            }
        }
    }
}