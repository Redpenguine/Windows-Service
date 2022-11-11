using Redpenguin.UiManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIManagement
{
  public class UIRootFactory : IUIRootFactory
  {
    private const string UIRootPrefabPath = "UIManagement/UIRoot";
    private Transform _uiRoot;
    private EventSystem _eventSystem;

    public void CreateUIRoot()
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
      Object.DontDestroyOnLoad(_uiRoot);
      FindEventSystem();
    }

    public Transform UIRoot() => _uiRoot;

    public EventSystem EventSystem => FindEventSystem();

    private EventSystem FindEventSystem()
    {
      if (_eventSystem != null) return _eventSystem;
      _eventSystem = Object.FindObjectOfType<EventSystem>();
      if (_eventSystem == null)
      {
        _eventSystem = new GameObject(nameof(UnityEngine.EventSystems.EventSystem),
          typeof(StandaloneInputModule)).GetComponent<EventSystem>();
      }
      return _eventSystem;
    }
  }
}