using UnityEngine;

namespace UIManagement.WaitingScreen
{
    public interface IWaitingScreenFactory
    {
        WaitingScreen CreateWaitingScreen(Transform root);
    }
}