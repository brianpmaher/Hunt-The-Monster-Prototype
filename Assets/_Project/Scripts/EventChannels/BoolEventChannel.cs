using UnityEngine;
using UnityEngine.Events;

namespace HuntTheMonster.EventChannels
{
    [CreateAssetMenu(menuName = "Events/Bool Event Channel")]
    public class BoolEventChannel : ScriptableObject
    {
        public UnityAction<bool> OnEventRaised;

        public void RaiseEvent(bool value)
        {
            OnEventRaised?.Invoke(value);
        }
    }
}