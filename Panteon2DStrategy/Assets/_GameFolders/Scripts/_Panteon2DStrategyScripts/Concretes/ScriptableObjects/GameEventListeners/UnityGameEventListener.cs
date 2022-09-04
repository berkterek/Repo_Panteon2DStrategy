using Panteon2DStrategy.Abstracts.GameEventListeners;
using UnityEngine;
using UnityEngine.Events;

namespace Panteon2DStrategy.ScriptableObjects.GameEventListeners
{
    public class UnityGameEventListener : BaseGameEventListener
    {
        [SerializeField] UnityEvent _untiyEvent;
        [SerializeField] UnityEvent<object> _untiyEventObject;

       public override void Notify()
        {
            _untiyEvent?.Invoke();
        }

       public override void Notify(object value)
       {
           _untiyEventObject?.Invoke(value);
       }
    }
}