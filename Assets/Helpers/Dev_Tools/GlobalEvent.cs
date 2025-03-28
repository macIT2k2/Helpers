using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Watermelon.SquadShooter
{
    public static class GlobalEvent
    {
        public const string OnNewGearSpawn = "OnNewGearSpawn";
        public const string OnNewWeaponSpawn = "OnNewWeaponSpawn";
        public const string OnChangeStateGearItem = "OnChangeStateGearItem";
        public const string OnChangeStateWeaponItem = "OnChangeStateWeaponItem";
        private static Dictionary<string,System.Action> eventDictionary = new Dictionary<string,System.Action>();
        public static void StartListening(string eventName, System.Action listener)
        {
            if(eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] += listener;
            }
            else
            {
                eventDictionary.Add(eventName,listener);
            }

            //Debug.Log($"<color=green>StartListening {eventName} - {GetAmountOfListener(eventName)}</color>");
        }
        public static void StopListening(string eventName, System.Action listener)
        {
            if(eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] -= listener;
            }
           // Debug.Log($"<color=green>StopListening {eventName} - {GetAmountOfListener(eventName)}</color>");
        }
        public static void TriggerEvent(string eventName)
        {
            //Debug.Log($"<color=green>trigger {eventName}</color>");
            if(eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName]?.Invoke();
            }
        }

        public static void ClearAllListener(string eventName)
        {
            if(eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] = null;
            }
        }


        //return amount of listener from event name
        public static int GetAmountOfListener(string eventName)
        {
            if(eventDictionary.ContainsKey(eventName))
            {
                return eventDictionary[eventName]?.GetInvocationList().Length ?? 0;
            }
            return 0;
        }
    }
    public static class GlobalEvent<T> where T : class
    {
        private static Dictionary<string,System.Action<T>> eventDictionary = new Dictionary<string,System.Action<T>>();

        public static void StartListening(string eventName, System.Action<T> listener)
        {
            if(eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] += listener;
            }
            else
            {
                eventDictionary.Add(eventName,null);
            }
        }

        public static void StopListening(string eventName, System.Action<T> listener)
        {
            if(eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] -= listener;
            }
        }

        public static void TriggerEvent(string eventName, T arg)
        {
            if(eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName]?.Invoke(arg);
            }
        }
    }
    public static class GlobalEvent<T1,T2> where T1 : class where T2 : class
    {
        private static Dictionary<string,System.Action<T1,T2>> eventDictionary = new Dictionary<string,System.Action<T1,T2>>();

        public static void StartListening(string eventName, System.Action<T1,T2> listener)
        {
            if(eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] += listener;
            }
            else
            {
                eventDictionary.Add(eventName,null);
            }
        }

        public static void StopListening(string eventName, System.Action<T1,T2> listener)
        {
            if(eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] -= listener;
            }
        }

        public static void TriggerEvent(string eventName, T1 arg1, T2 arg2)
        {
            if(eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName]?.Invoke(arg1,arg2);
            }
        }
    }
}