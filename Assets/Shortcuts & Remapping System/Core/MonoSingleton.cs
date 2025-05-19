using UnityEngine;

namespace RedRats.Core
{
    /// <summary>
    /// Turns a MonoBehaviour class, that inherits this into a Singleton.
    /// </summary>
    /// <typeparam name="T">Any component type.</typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : Component
    {
        private static readonly object instanceLock = new();
        private static T instance;
        private static bool isQuiting;

        protected virtual void Awake()
        {
            if (instance == null) instance = gameObject.GetComponent<T>();
            else if (instance.GetInstanceID() != GetInstanceID())
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnApplicationQuit() => isQuiting = true;

        public static T Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance != null || isQuiting) return instance;
                    instance = FindFirstObjectByType<T>();
                    
                    if (instance != null) return instance;
                    instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                    return instance;
                }
            }
        }
    }
}