using UnityEngine;

namespace Code.Scripts.Core.DesignPatterns.Singleton
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;
        public static T Instance => GetInstance();

        private static T GetInstance()
        {
            if (_instance is not null) return _instance;

            var search = GameObject.FindObjectsOfType<T>();
            if (search.Length != 0)
            {
                _instance = search[0];
                return _instance;
            }

            var container = new GameObject();
            _instance = container.AddComponent<T>();
            return _instance;
        }
    }
}
