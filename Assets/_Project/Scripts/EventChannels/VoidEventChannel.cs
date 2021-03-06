using UnityEngine;
using UnityEngine.Events;

namespace HuntTheMonster.EventChannels
{
    [CreateAssetMenu(menuName = "Events/Void Event Channel")]
    public class VoidEventChannel : ScriptableObject
    {
        public UnityAction OnEventRaised;

        public void RaiseEvent()
        {
            OnEventRaised?.Invoke();
        }
    }
}