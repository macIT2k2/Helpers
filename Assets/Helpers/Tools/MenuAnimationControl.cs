using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dev_Tools.UIAnimation
{
    public class MenuAnimationControl : MonoBehaviour
    {
        [TableMatrix]
        public List<MenuItem> menuItems;

        //[ShowInInspector]
        //public bool IsShow
        //{
        //    get => this.gameObject.activeSelf;
        //}

        [ShowInInspector, ReadOnly]
        public MenuItemState ThisMenuItemState
        {
            get;
            protected set;
        }

        public void StartShow(float _delay, Action _onShowStarted, Action _onShowCompleted , bool workWithTimeScale = false)
        {
            StartCoroutine(_IEStarShow());
            IEnumerator _IEStarShow()
            {
                ThisMenuItemState = MenuItemState.Showing;
                if (_onShowStarted != null)
                    _onShowStarted.Invoke();
                yield return workWithTimeScale ? new WaitForSecondsRealtime(_delay) : new WaitForSeconds(_delay);
                //Debug.Log($"<color=green>StartShow - {workWithTimeScale}</color>");
                foreach (var _item in menuItems)
                {
                    _item.StartShow();
                }
                yield return workWithTimeScale ? new WaitForSecondsRealtime(GetLongestAnimationTime(true)) : new WaitForSeconds(GetLongestAnimationTime(true));
                //Debug.Log($"<color=green>FinishShow - {workWithTimeScale}</color>");
                if (_onShowCompleted != null)
                    _onShowCompleted.Invoke();

                ThisMenuItemState = MenuItemState.Showed;
            }
        }

        public void StartHide(float _delay, Action _onHideStarted, Action _onHideCompleted , bool workWithTimeScale = false)
        {
            StartCoroutine(_IEStartHide());
            IEnumerator _IEStartHide()
            {
                ThisMenuItemState = MenuItemState.Hiding;
                if (_onHideStarted != null)
                    _onHideStarted.Invoke();
                //yield return new WaitForSeconds(_delay);
                yield return workWithTimeScale ? new WaitForSecondsRealtime(_delay) : new WaitForSeconds(_delay);
                foreach (var _item in menuItems)
                {
                    _item.StartHide();
                }
                // if(workWithTimeScale)
                //     yield return new WaitForSecondsRealtime(GetLongestAnimationTime(false));
                // else yield return new WaitForSeconds(GetLongestAnimationTime(false));
                yield return workWithTimeScale ? new WaitForSecondsRealtime(GetLongestAnimationTime(false)) : new WaitForSeconds(GetLongestAnimationTime(false));
                if (_onHideCompleted != null)
                    _onHideCompleted.Invoke();

                ThisMenuItemState = MenuItemState.Hidden;
            }
        }

        public void PretendShow()
        {
            foreach (var _item in menuItems)
            {
                _item.StartShow();
            }
        }

        public void PretendHide()
        {
            foreach (var _item in menuItems)
            {
                _item.StartHide();
            }
        }

        public void ShowAndHide()
        {
            StartShow(0, null, () =>
            {
                StartHide(0, null, null);
            });
        }

        public void HideImmediate()
        {
            foreach (var _item in menuItems)
            {
                _item.PreviewHide();
            }
        }

        float GetLongestAnimationTime(bool _isShowTime)
        {
            float _temp = 0;
            foreach (var _item in menuItems)
            {
                if (_isShowTime)
                {
                    if ((_item.DelayShow + _item.TimeShow) > _temp)
                        _temp = _item.DelayShow + _item.TimeShow;
                }
                else
                {
                    if ((_item.DelayHide + _item.TimeHide) > _temp)
                        _temp = _item.DelayHide + _item.TimeHide;
                }
            }
            return _temp;
        }


        [Button]
        public void GetAllMenuItem()
        {
            menuItems.Clear();
            menuItems = Helper.GetAllChildsComponent<MenuItem>(this.transform);
        }

    }
}

public enum MenuItemState
{
    None,
    Showing,
    Showed,
    Hiding,
    Hidden,
}