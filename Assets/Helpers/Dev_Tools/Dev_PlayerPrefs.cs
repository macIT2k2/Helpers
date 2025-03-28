using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Watermelon.SquadShooter
{
    public sealed class Dev_PlayerPrefs
    {
        public static void SetObjectValue<T>(string key, T value, bool saveImmediately = false) where T : class
        {
            string value2 = (value == null) ? string.Empty : JsonUtility.ToJson(value);
            SetString(key, value2, saveImmediately);
            //Debug.Log($"<color=green>SetObjectValue {value2}</color>");
        }

        public static T GetObjectValue<T>(string _key) where T : class
        {
            string @string = GetString(_key);
            //Debug.Log($"<color=green>Get {@string}</color>");
            //Debug.Log($"<color=green>Get {GetString(_key)}</color>");
            return (!string.IsNullOrEmpty(@string)) ? JsonUtility.FromJson<T>(@string) : ((T)((object)null));
        }

        public static void SetString(string _key, string _value, bool _isSaveImmediately = false)
        {
            PlayerPrefs.SetString(_key, _value);
            if (_isSaveImmediately)
                Save();
        }

        public static string GetString(string _key)
        {
            return GetString(_key, DEFAULT_STRING);
        }

        public static string GetString(string _key, string _defaultValue)
        {
            return PlayerPrefs.GetString(_key, _defaultValue);
        }

        public static void Save()
        {
            PlayerPrefs.Save();
        }

        public static readonly string DEFAULT_STRING = string.Empty;
    }



}