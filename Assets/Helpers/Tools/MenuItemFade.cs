using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dev_Tools.UIAnimation
{
    public class MenuItemFade : MenuItem
    {
        [Button, BoxGroup("Tween setting")] public float showAlpha;
        [Button, BoxGroup("Tween setting")] public float hideAlpha;
        [Button, BoxGroup("Tween setting")] public Ease easeShow = Ease.Linear;
        [Button, BoxGroup("Tween setting")] public Ease easeHide = Ease.Linear;
        private Image thisImage
        {
            get
            {
                return GetComponent<Image>();
            }
        }

        private TextMeshProUGUI thisText
        {
            get
            {
                return GetComponent<TextMeshProUGUI>();
            }
        }

        public override void StartShow()
        {
            if (this.gameObject.activeSelf)
                StartCoroutine(IEStartShow());
        }

        public override IEnumerator IEStartShow()
        {
            ThisMenuItemState = MenuItemState.Showing;
            Color _tempColor;
            if (thisImage != null)
            {
                _tempColor = thisImage.color;
                _tempColor.a = hideAlpha;
                thisImage.color = _tempColor;

                yield return new WaitForSeconds(DelayShow);
                thisImage.DOFade(showAlpha, TimeShow).SetEase(easeShow).OnComplete(() =>
                {
                    ThisMenuItemState = MenuItemState.Showed;

                });
            }
            else if(thisText != null)
            {
                _tempColor = thisText.color;
                _tempColor.a = hideAlpha;
                thisText.color = _tempColor;

                yield return new WaitForSeconds(DelayShow);
                thisText.DOFade(showAlpha, TimeShow).SetEase(easeShow).OnComplete(() =>
                {
                    ThisMenuItemState = MenuItemState.Showed;

                });
            }
        }

        public override void StartHide()
        {
            if (this.gameObject.activeSelf)
                StartCoroutine(IEStartHide());
        }

        public override IEnumerator IEStartHide()
        {
            ThisMenuItemState = MenuItemState.Hiding;

            yield return new WaitForSeconds(DelayHide);
            if(thisImage != null)
                thisImage.DOFade(hideAlpha, TimeHide).SetEase(easeHide).OnComplete(() =>
                {
                    ThisMenuItemState = MenuItemState.Hidden;
                });
            else if(thisText != null)
                thisText.DOFade(hideAlpha, TimeHide).SetEase(easeHide).OnComplete(() =>
                {
                    ThisMenuItemState = MenuItemState.Hidden;
                });
        }

        public override void PreviewHide()
        {
            Color _tempColor;
            if (thisImage != null)
            {
                _tempColor = thisImage.color;
                _tempColor.a = hideAlpha;
                thisImage.color = _tempColor;

                ThisMenuItemState = MenuItemState.Hidden;
            }
            else if (thisText != null)
            {
                _tempColor = thisText.color;
                _tempColor.a = hideAlpha;
                thisText.color = _tempColor;

                ThisMenuItemState = MenuItemState.Hidden;
            }
        }

        public override void PreviewShow()
        {
            Color _tempColor;
            if(thisImage != null)
            {
                _tempColor = thisImage.color;
                _tempColor.a = showAlpha;
                thisImage.color = _tempColor;
                ThisMenuItemState = MenuItemState.Showed;
            }
            else if(thisText != null)
            {
                _tempColor = thisText.color;
                _tempColor.a = showAlpha;
                thisText.color = _tempColor;
                ThisMenuItemState = MenuItemState.Showed;
            }
        }

        public override void SetThisAsShow()
        {
            if(thisImage != null)
                showAlpha = thisImage.color.a;
            else if(thisText != null)
                showAlpha = thisText.color.a;
        }

        public override void SetThisAsHide()
        {
            if (thisImage != null)
                hideAlpha = thisImage.color.a;
            else if (thisText != null)
                hideAlpha = thisText.color.a;
        }
    }
}