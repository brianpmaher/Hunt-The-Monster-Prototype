using UnityEngine;

namespace HuntTheMonster
{
    public class WarningBehavior : MonoBehaviour
    {
        [SerializeField] private BoolEventChannelSO eventChannel;

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