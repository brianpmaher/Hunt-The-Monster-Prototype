using UnityEngine;

namespace HuntTheMonster
{
    public class WarningBehavior : MonoBehaviour
    {
        [SerializeField] private string message;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log(message);
            }
        }
    }
}