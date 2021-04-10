using HuntTheMonster.EventChannels;
using UnityEngine;

namespace HuntTheMonster.Environment
{
    public class TrapTrigger : MonoBehaviour
    {
        [SerializeField] private VoidEventChannel gameOverEventChannel;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                gameOverEventChannel.RaiseEvent();
            }
        }
    }
}