using System;
using NaughtyAttributes;
using Redpenguin.UiManager.UIWindows.Animations;
using Redpenguin.UiManager.UIWindows.Animations.AnimationTypes;
using UIManagement.UIWindows.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace Redpenguin.UiManager.UIWindows.Windows
{
  public abstract class BaseWindow : MonoBehaviour
  {
    [SerializeField] private AnimationId _animationId = AnimationId.FadeAndScale;

    [ShowIf(nameof(_isCustomAnim)), SerializeField]
    private OpenCloseAnimation custonAnimation;

    [SerializeField] private bool _closableFromUser = true;

    [ShowIf(nameof(_closableFromUser)),SerializeField]
    private Button _closeButton;
    [SerializeField] private bool _extraButton = false;
    [ShowIf(nameof(_extraButton)),SerializeField]
    private Button _extraCloseButton;
    
    private IOpenCloseAnimation _animation;
    private IOpenCloseAnimationsFactory _animationsFactory;
    protected IWindowsService _windowsService;
    private bool _isCustomAnim => _animationId == AnimationId.CustomAnimation;

    //private IButtonClickSound _buttonClickSound;
    public virtual bool ClosableFromUser => _closableFromUser;
    public IOpenCloseAnimation Animation => _animation;

    public Button CloseButton => _closeButton;

    private void Awake()
    {
      if (_closableFromUser)
      {
        _closeButton.onClick.AddListener(OnBackClick);
        //_closeButton.onClick.AddListener(_buttonClickSound.ButtonClick);
        if (_extraButton)
        {
          _extraCloseButton.onClick.AddListener(OnBackClick);
          //_extraCloseButton.onClick.AddListener(_buttonClickSound.ButtonClick);
        }
      }
    }

    private void OnDestroy()
    {
      if (_closableFromUser)
      {
        _closeButton.onClick.RemoveAllListeners();
        if(_extraButton)_extraCloseButton.onClick.RemoveAllListeners();
      }
    }

    public void Construct(IWindowsService windowsService, IOpenCloseAnimationsFactory animationsFactory)
    {
      _windowsService = windowsService;
      _animationsFactory = animationsFactory;
      _animation = _isCustomAnim ? custonAnimation : _animationsFactory.Create(transform, _animationId);
      OnConstruct();
    }

    // [Inject]
    // private void BaseInject(IButtonClickSound buttonClickSound)
    // {
    //     _buttonClickSound = buttonClickSound;
    // }
    public void Close(Action onCloseComplete = null)
    {
      if (_animation != null) _animation.AnimateClose(() => CompleteClose(onCloseComplete));
      else CompleteClose(onCloseComplete);

      void CompleteClose(Action onComplete)
      {
        OnClosed();
        //Destroy(gameObject);
        gameObject.SetActive(false);
        onComplete?.Invoke();
      }
    }


    protected virtual void OnBackClick()
    {
      _windowsService.CloseTopmostWindow();
    }

    protected void PlayOpening(Action onComplete = null)
    {
      if (_animation != null) _animation.AnimateOpen(onComplete);
      else onComplete?.Invoke();
    }

    protected virtual void OnConstruct()
    {
    }

    protected virtual void OnClosed()
    {
      
    }
  }
}