using UnityEngine;

namespace HuntTheMonster.Environment
{
    public class WallWithDoor : MonoBehaviour
    {
        [SerializeField] private GameObject door;

        public GameObject Door => door;
    }
}