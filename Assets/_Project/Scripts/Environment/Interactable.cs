using UnityEngine;
using UnityEngine.Events;

namespace HuntTheMonster.Environment
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private UnityEvent onInteract;

        public void Interact()
        {
            onInteract.Invoke();
        }
    }
}