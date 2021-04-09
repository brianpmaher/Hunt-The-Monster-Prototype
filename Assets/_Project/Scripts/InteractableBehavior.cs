using UnityEngine;
using UnityEngine.Events;

namespace HuntTheMonster
{
    public class InteractableBehavior : MonoBehaviour
    {
        [SerializeField] private UnityEvent onInteract;

        public void Interact()
        {
            onInteract.Invoke();
        }
    }
}