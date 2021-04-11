using UnityEngine;

namespace HuntTheMonster.LevelGeneration
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private int levelWidth = 5;
        [SerializeField] private int levelLength = 5;
        [SerializeField] private float roomWidth = 10f;
        [SerializeField] private float roomLength = 10f;
        [SerializeField] private GameObject rootLevel;
        [SerializeField] private GameObject roomFloor;
        
        private Transform _rootTransform;
        private Level _level;
        
        private void Awake()
        {
            _rootTransform = rootLevel.transform;
            _level = new Level(levelWidth, levelLength);
        }

        private void Start()
        {
            PlaceFloors();
        }

        private void PlaceFloors()
        {
            for (var x = 0; x < _level.rooms.GetLength(0); x++)
            {
                for (var z = 0; z < _level.rooms.GetLength(1); z++)
                {
                    var position = new Vector3(x * roomWidth, 0, z * roomLength);
                    Instantiate(roomFloor, position, Quaternion.identity, _rootTransform);
                }
            }
        }
    }
}