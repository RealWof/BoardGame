using UnityEngine;

namespace GameCore
{
    public class SimpleSingleton<T> : MonoBehaviour where T : SimpleSingleton<T>
    {
        private static T _instance;
        public static T instance => _instance ?? (_instance = FindObjectOfType<T>());

        public static T I => instance;
    }
}