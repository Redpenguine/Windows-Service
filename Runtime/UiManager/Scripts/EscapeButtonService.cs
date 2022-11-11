using System;
using System.Collections.Generic;
using Zenject;

namespace Redpenguin.UiManager
{
  public class EscapeButtonService : IInitializable
  {
    private EscapeButtonListenerFactory _buttonListenerFactory;
    private EscapeButtonListener _escBtn;
    private Stack<Action> _stack;
    
    public EscapeButtonService(EscapeButtonListenerFactory buttonListenerFactory)
    {
      _buttonListenerFactory = buttonListenerFactory;
    }
    public void Initialize()
    {
      _escBtn = _buttonListenerFactory.Create();
      _stack = new Stack<Action>();  
    }

    public void AddEcsAction(Action action)
    {
      _escBtn.Add(action);
    }
    public void RemoveEcsAction(Action action)
    {
      _escBtn.Remove(action);
    }
  }
}