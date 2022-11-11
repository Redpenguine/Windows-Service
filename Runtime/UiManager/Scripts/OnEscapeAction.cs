using System;

namespace Redpenguin.UiManager
{
  public struct OnEscapeAction
  {
    public bool IsBefore;
    public Action Action;
    public bool RemoveOnInvoke;

    public OnEscapeAction(bool isBefore, Action action, bool removeOnInvoke)
    {
      IsBefore = isBefore;
      Action = action;
      RemoveOnInvoke = removeOnInvoke;
    }
  }
}