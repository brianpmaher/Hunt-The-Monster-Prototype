using UnityEngine;

namespace HuntTheMonster
{
    public class PlayerManagerBehavior : MonoBehaviour
    {
        public static Transform PlayerTransform { get; private set; }

        private void Awake()
        {
            PlayerTransform = transform;
        }
    }
}