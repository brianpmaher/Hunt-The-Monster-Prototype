using HuntTheMonster.EventChannels;
using UnityEngine;

namespace HuntTheMonster.Environment
{
    public class WarningTrigger : MonoBehaviour
    {
        [SerializeField] private BoolEventChannel eventChannel;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                eventChannel.RaiseEvent(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                eventChannel.RaiseEvent(false);
            }
        }
    }
}