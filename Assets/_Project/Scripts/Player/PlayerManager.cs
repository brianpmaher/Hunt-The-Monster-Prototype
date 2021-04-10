using UnityEngine;

namespace HuntTheMonster.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static Transform PlayerTransform { get; private set; }

        private void Awake()
        {
            PlayerTransform = transform;
        }
    }
}