using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Redpenguin.UiManager
{
  public class OverlayUIRootFactory : IOverlayUIRootFactory
  {
    private const string UIRootPrefabPath = "UIManagement/OverlayUIRoot";
    private Transform _uiRoot;
    private EventSystem _eventSystem;

    public void Create()
    {
      var root = Resources.Load<GameObject>(UIRootPrefabPath);
      var canvas = root.GetComponent<Canvas>();
      var canvasScaler = root.GetComponent<CanvasScaler>();
      canvas.worldCamera = Camera.main;
      canvas.planeDistance = 100f;
      canvas.sortingLayerName = "Popup";
      canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
      canvasScaler.matchWidthOrHeight = 0.5f;
      _uiRoot = Object.Instantiate(root).transform;
    }

    public Transform Root() => _uiRoot;
  }
}