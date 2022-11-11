using Demos.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Redpenguin.UiManager
{
  public interface IOverlayUIRootFactory : IService
  {
    void Create();
    Transform Root();
  }
}