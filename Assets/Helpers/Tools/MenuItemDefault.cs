using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Dev_Tools.UIAnimation
{
    public class MenuItemDefault : MenuItem
    {
        [Button, BoxGroup("Tween setting")] public Vector3 showScale = Vector3.one;
        [Button, BoxGroup("Tween setting")] public Vector3 hideScale = Vector3.zero;

        private RectTransform ThisRectTransform
        {
            get
            {
                return GetComponent<RectTransform>();
            }
        }

        public override void StartShow()
        {
            //StartCoroutine(IEStartShow());
            ThisMenuItemState = MenuItemState.Showing;
            ThisRectTransform.localScale = showScale;
            ThisMenuItemState = MenuItemState.Showed;
        }

        public override IEnumerator IEStartShow()
        {
            yield return null;
            // ThisMenuItemState = MenuItemState.Showing;

            // ThisRectTransform.localScale = hideScale;
            // yield return new WaitForSeconds(DelayShow);
            // ThisRectTransform.DOScale(showScale, TimeShow).SetEase(easeShow).OnComplete(() =>
            // {
            //     ThisMenuItemState = MenuItemState.Showed;
            // });
        }

        public override void StartHide()
        {
            ThisMenuItemState = MenuItemState.Hiding;
            ThisRectTransform.localScale = hideScale;
            ThisMenuItemState = MenuItemState.Hidden;
        }

        public override IEnumerator IEStartHide()
        {
            yield return null;
            // ThisMenuItemState = MenuItemState.Hiding;

            // yield return new WaitForSeconds(DelayHide);
            // ThisRectTransform.DOScale(hideScale, TimeHide).SetEase(easeHide).OnComplete(() =>
            // {
            //     ThisMenuItemState = MenuItemState.Hidden;
            // });
        }

        public override void PreviewHide()
        {
            ThisRectTransform.localScale = hideScale;
            ThisMenuItemState = MenuItemState.Hidden;
        }

        public override void PreviewShow()
        {
            ThisRectTransform.localScale = showScale;
            ThisMenuItemState = MenuItemState.Showed;
        }

        public override void SetThisAsShow()
        {
            showScale = ThisRectTransform.localScale;
        }

        public override void SetThisAsHide()
        {
            hideScale = ThisRectTransform.localScale;
        }
    }
}

