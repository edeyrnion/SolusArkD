using UnityEngine;

namespace Matthias
{
    // Generic Singleton (Thread Safe)
    public class Singleton<T> : MonoBehaviour where T : class, new()
    {
        protected Singleton() { }

        public static T Instance
        {
            get
            {
                return SingletonCreator.instance;
            }
        }

        private class SingletonCreator
        {
            static SingletonCreator() { }

            internal static readonly T instance = new T();
        }
    }
}
