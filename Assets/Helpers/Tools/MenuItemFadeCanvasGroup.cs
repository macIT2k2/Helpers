using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dev_Tools.UIAnimation
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MenuItemFadeCanvasGroup : MenuItem
    {
        [Button, BoxGroup("Tween setting")] public float showAlpha;
        [Button, BoxGroup("Tween setting")] public float hideAlpha;
        [Button, BoxGroup("Tween setting")] public Ease easeShow = Ease.Linear;
        [Button, BoxGroup("Tween setting")] public Ease easeHide = Ease.Linear;
        private CanvasGroup thisCanvasGroup
        {
            get
            {
                return GetComponent<CanvasGroup>();
            }
        }

        [Sirenix.OdinInspector.Button]
        public override void StartShow()
        {
            if (this.gameObject.activeSelf)
                StartCoroutine(IEStartShow());
        }

        public override IEnumerator IEStartShow()
        {
            ThisMenuItemState = MenuItemState.Showing;
            //Color _tempColor;
            if (thisCanvasGroup != null)
            {
                //_tempColor = thisCanvasGroup.color;
                thisCanvasGroup.alpha = hideAlpha;
                thisCanvasGroup.blocksRaycasts = true;
                thisCanvasGroup.interactable = true;
                yield return new WaitForSeconds(DelayShow);
                thisCanvasGroup.DOFade(showAlpha, TimeShow).SetEase(easeShow).OnComplete(() =>
                {
                    ThisMenuItemState = MenuItemState.Showed;

                });
            }
        }
        [Sirenix.OdinInspector.Button]
        public override void StartHide()
        {
            if (this.gameObject.activeSelf)
                StartCoroutine(IEStartHide());
        }

        public override IEnumerator IEStartHide()
        {
            ThisMenuItemState = MenuItemState.Hiding;

            yield return new WaitForSeconds(DelayHide);
            if (thisCanvasGroup != null)
            {
                thisCanvasGroup.blocksRaycasts = false;
                thisCanvasGroup.interactable = false;
                thisCanvasGroup.DOFade(hideAlpha, TimeHide).SetEase(easeHide).OnComplete(() =>
                {
                    ThisMenuItemState = MenuItemState.Hidden;
                });
            }
        }

        public override void PreviewHide()
        {
            if (thisCanvasGroup != null)
            {
                thisCanvasGroup.alpha = hideAlpha;
                thisCanvasGroup.blocksRaycasts = false;
                thisCanvasGroup.interactable = false;
                ThisMenuItemState = MenuItemState.Hidden;
            }
        }

        public override void PreviewShow()
        {
            if (thisCanvasGroup != null)
            {
                thisCanvasGroup.alpha = showAlpha;
                thisCanvasGroup.blocksRaycasts = true;
                thisCanvasGroup.interactable = true;
                ThisMenuItemState = MenuItemState.Showed;
            }
        }

        public override void SetThisAsShow()
        {
            if (thisCanvasGroup != null)
                showAlpha = thisCanvasGroup.alpha;
        }

        public override void SetThisAsHide()
        {
            if (thisCanvasGroup != null)
                hideAlpha = thisCanvasGroup.alpha;
        }
    }
}