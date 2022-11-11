using Demos.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Redpenguin.UiManager
{
    public interface IUIRootFactory : IService
    {
        void CreateUIRoot();
        Transform UIRoot();
        EventSystem EventSystem { get; }
    }
}