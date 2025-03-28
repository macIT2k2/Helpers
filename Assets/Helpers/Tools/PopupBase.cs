using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dev_Tools.UIAnimation;
using Dev_Tools;
using Watermelon;
public enum PopupState
{
    Show,
    Hide
}

public class PopupBase : MonoBehaviour
{
    [SerializeField, Sirenix.OdinInspector.BoxGroup("Popup Reference")] protected Button closeButton;
    [SerializeField] public Button CloseButton { get => closeButton; }
    [SerializeField] private MenuAnimationControl menuAnimationControl;
    public bool isAnimationActive = false;
    //public PopupState currentState; 
    [SerializeField] private CanvasGroup canvasGroup;
    public CanvasGroup CanvasGroup { get => canvasGroup; set => canvasGroup = value; }
    private RectTransform rectTransform;
    public RectTransform RectTransform
    {
        get
        {
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();
            return rectTransform;
        }
    }

    private bool isPopup = true;
    public bool IsPopup { get => isPopup; set => isPopup = value; }

    public bool IsShow => this.gameObject.activeSelf;

    public Action OnCompleteClose;

    protected virtual void Start()
    {
        ButtonAddListener();
    }
    public MenuAnimationControl ThisMenuAnimationControl
    {
        get
        {
            if (menuAnimationControl == null)
                menuAnimationControl = GetComponent<MenuAnimationControl>();
            return menuAnimationControl;
        }
    }
    protected System.Action actionOnStartShow, actionOnCompleteShow, actionOnStartHide, actionOnCompleteHide;
    protected object data;
    // public bool IsShow
    // {
    //    get
    //    {
    //        return this.gameObject.activeSelf;
    //    }
    // }

    #region SHOW
    public virtual void Show(object _data = null, float _delay = 0f, bool _hasAnimation = true, Action _actionOnStartShow = null, Action _actionOnCompleteShow = null, Action _actionOnStartHide = null, Action _actionOnCompleteHide = null , bool workWithTimeScale = false)
    {
        // if (IsShow)
        //     return;
        if (isAnimationActive) return;
        //currentState = PopupState.Show;
        isAnimationActive = true;
        this.data = _data;
        this.actionOnStartShow = _actionOnStartShow;
        this.actionOnCompleteShow = _actionOnCompleteShow;
        this.actionOnStartHide = _actionOnStartHide;
        this.actionOnCompleteHide = _actionOnCompleteHide;
        this.Init();

        //ButtonAddListener();
        if (ThisMenuAnimationControl == null || !_hasAnimation)
        {
            this.gameObject.SetActive(true);
            PreviewShow();
            OnShowStarted();
            OnShowCompleted();
        }
        else
        {
            this.gameObject.SetActive(true);
            ThisMenuAnimationControl.StartShow(_delay, _onShowStarted: OnShowStarted, _onShowCompleted: OnShowCompleted, workWithTimeScale);
        }
    }
    protected virtual void OnShowStarted()
    {
        this.actionOnStartShow?.Invoke();
    }
    protected virtual void OnShowCompleted()
    {
        isAnimationActive = false;
        this.actionOnCompleteShow?.Invoke();
    }
    [Sirenix.OdinInspector.Button, Sirenix.OdinInspector.BoxGroup("UI preview")]
    public virtual void PreviewShow()
    {
        foreach (var _item in ThisMenuAnimationControl.menuItems)
        {
            _item.PreviewShow();
        }
    }

    #endregion

    #region HIDE
    public virtual void Hide(bool _hasAnimation = true , bool workWithTimeScale = false)
    {
        if (!IsShow)
            return;
        //ButtonRemoveListener();
        isAnimationActive = true;
        //currentState = PopupState.Hide;
        if (ThisMenuAnimationControl == null || !_hasAnimation)
        {
            OnHideStarted();
            OnHideCompleted();
            PreviewHide();
        }
        else
        {
            ThisMenuAnimationControl.StartHide(0f, _onHideStarted: OnHideStarted, _onHideCompleted: OnHideCompleted , workWithTimeScale);
        }
    }
    protected virtual void OnHideStarted()
    {
        this.actionOnStartHide?.Invoke();
    }
    protected virtual void OnHideCompleted()
    {
        isAnimationActive = false;
        this.actionOnCompleteHide?.Invoke();
        this.gameObject.SetActive(false);
    }
    [Sirenix.OdinInspector.Button, Sirenix.OdinInspector.BoxGroup("UI preview")]
    public virtual void PreviewHide()
    {
        foreach (var _item in ThisMenuAnimationControl.menuItems)
        {
            _item.PreviewHide();
        }
    }
    #endregion

    protected virtual void Init()
    {

    }

    protected virtual void ButtonAddListener()
    {
        closeButton?.onClick.AddListener(OnCloseClick);
    }
    protected virtual void ButtonRemoveListener()
    {
        closeButton?.onClick.RemoveListener(OnCloseClick);
    }
    protected virtual void OnCloseClick()
    {
        //gameObject.SetActive(false);
    }

    protected virtual void SetOnOff(bool _isOn)
    {

    }

    #region [Custom PopupBase]
    public virtual void ShowPopup()
    {

    }

    public void HidePopup()
    {
        OnCloseClick();
    }
    #endregion
}
