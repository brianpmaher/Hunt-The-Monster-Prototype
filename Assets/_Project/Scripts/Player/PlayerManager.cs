using UnityEngine;

namespace HuntTheMonster.Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private GameObject playerCamera;
        
        public static Transform PlayerTransform { get; private set; }
        public GameObject PlayerCamera => playerCamera;
        
        private void Awake()
        {
            PlayerTransform = transform;
        }
    }
}