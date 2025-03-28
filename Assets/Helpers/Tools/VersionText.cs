using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AntiStress.UI.SettingPopup
{
    public class VersionText : MonoBehaviour
    {
        TextMeshProUGUI text;
        [ShowInInspector]
        TextMeshProUGUI ThisText
        {
            get
            {
                if (text == null)
                    text = GetComponent<TextMeshProUGUI>();
                return text;
            }
        }

        private void OnEnable()
        {
            //I2.Loc.LocalizationManager.OnLocalizeEvent += LocalizeVersionText;
            //ThisText.text = I2.Loc.ScriptLocalization.Version + " " + Application.version;
            ThisText.text = "Version " + Application.version;
        }

        private void OnDisable()
        {
            //I2.Loc.LocalizationManager.OnLocalizeEvent -= LocalizeVersionText;
        }

        private void LocalizeVersionText()
        {
            //ThisText.text = I2.Loc.ScriptLocalization.Version + " " + Application.version;
        }
    }
}