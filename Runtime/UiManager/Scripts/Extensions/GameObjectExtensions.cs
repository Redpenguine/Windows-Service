using UnityEngine;

namespace Redpenguin.UiManager
{
    public static class GameObjectExtensions
    {
        public static RectTransform ToFullScreenRect(this GameObject gameObject)
        {
            RectTransform rect = gameObject.TryGetComponent(out RectTransform rectTransform) ? 
                rectTransform : gameObject.AddComponent<RectTransform>();
            rect.anchoredPosition = Vector2.zero;
            rect.localPosition = Vector3.zero;
            rect.localScale = Vector3.one;
            rect.anchorMax = Vector2.one;
            rect.anchorMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            rect.offsetMin = Vector2.zero;
            return rect;
        } 
        public static RectTransform SetScreenRect(this GameObject gameObject, float value)
        {
            RectTransform rect = gameObject.TryGetComponent(out RectTransform rectTransform) ?
                rectTransform : gameObject.AddComponent<RectTransform>();
            rect.anchoredPosition = Vector2.zero;
            rect.localPosition = Vector3.zero;
            rect.localScale = Vector3.one;
            rect.anchorMax = Vector2.one;
            rect.anchorMin = Vector2.zero;
            rect.offsetMin = new Vector2(value, value);
            rect.offsetMax = new Vector2(-value, -value);
            return rect;
        }      
        
    }
}