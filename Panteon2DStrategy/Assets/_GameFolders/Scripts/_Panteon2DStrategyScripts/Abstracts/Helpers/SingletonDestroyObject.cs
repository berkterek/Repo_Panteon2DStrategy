using UnityEngine;

namespace Panteon2DStrategy.Abstracts.Helpers
{
    public abstract class SingletonDestroyObject<T> : SingletonMonoObject<T> where T : Component
    {
        protected override void SetSingleton(T instance)
        {
            if (Instance == null)
            {
                Instance = instance;
            }
        }

        protected virtual void OnDestroy()
        {
            Instance = null;
        }
    }
}