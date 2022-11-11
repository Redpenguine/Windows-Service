using UnityEngine;
using Zenject;

namespace Redpenguin.UiManager
{
  public class EscapeButtonListenerFactory
  {
    private DiContainer _diContainer;

    public EscapeButtonListenerFactory(DiContainer diContainer)
    {
      _diContainer = diContainer;
    }

    public EscapeButtonListener Create()
    { 
      var go = _diContainer.InstantiateComponentOnNewGameObject<EscapeButtonListener>();
      Object.DontDestroyOnLoad(go);
      return go;
    }
  }
}