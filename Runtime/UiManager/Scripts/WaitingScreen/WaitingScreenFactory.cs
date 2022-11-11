using UnityEngine;

namespace UIManagement.WaitingScreen
{
    public class WaitingScreenFactory : IWaitingScreenFactory
    {
        private const string PrefabPath = "UIManagement/WaitingScreen";
        private readonly WaitingScreen _waitingScreenPrefab;

        public WaitingScreenFactory()
        {
            _waitingScreenPrefab = Resources.Load<WaitingScreen>(PrefabPath);
        }
        
        public WaitingScreen CreateWaitingScreen(Transform root)
        {
            return Object.Instantiate(_waitingScreenPrefab, root);
        }
    }
}