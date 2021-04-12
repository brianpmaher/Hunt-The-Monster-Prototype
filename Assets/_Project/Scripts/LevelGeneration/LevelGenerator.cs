using System.Linq;
using UnityEngine;

namespace HuntTheMonster.LevelGeneration
{
    public class LevelGenerator : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private int levelWidth = 5;
        [SerializeField] private int levelLength = 5;
        [SerializeField] private float roomWidth = 10f;
        [SerializeField] private float roomLength = 10f;
        [SerializeField] private GameObject rootLevel;
        [SerializeField] private int numTraps = 1;
        
        [Header("Level Components")]
        [SerializeField] private GameObject roomFloor;
        [SerializeField] private GameObject solidWall;
        [SerializeField] private GameObject wallWithDoor;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject monster;
        [SerializeField] private GameObject trap;
        
        private Transform _rootTransform;
        private Level _level;
        
        private void Awake()
        {
            _rootTransform = rootLevel.transform;
            _level = new Level(levelWidth, levelLength);
            _level.PlacePlayerStart();
            _level.PlaceMonsterStart();
            _level.PlaceTraps(numTraps);
        }

        private void Start()
        {
            PlaceFloors();
            PlaceWalls();
            PlacePlayer();
            PlaceMonster();
        }

        private void PlaceFloors()
        {
            for (var x = 0; x < _level.Rooms.GetLength(0); x++)
            {
                for (var y = 0; y < _level.Rooms.GetLength(1); y++)
                {
                    var room = _level.Rooms[x, y];
                    var position = new Vector3(x * roomWidth, 0, y * roomLength);

                    if (room.HasEntity(typeof(TrapEntity)))
                    {
                        Instantiate(trap, position, Quaternion.identity, _rootTransform);
                    }
                    else
                    {
                        Instantiate(roomFloor, position, Quaternion.identity, _rootTransform);
                    }
                }
            }
        }

        private void PlaceWalls()
        {
            for (var x = 0; x < _level.Rooms.GetLength(0); x++)
            {
                for (var y = 0; y < _level.Rooms.GetLength(1); y++)
                {
                    var roomPosition = new Vector3(x * roomWidth, 0, y * roomLength);
                    var isTopEdge = y == levelLength - 1;
                    var isRightEdge = x == levelWidth - 1;
                    var isBottomEdge = y == 0;
                    var isLeftEdge = x == 0;

                    PlaceTopWall(isTopEdge, roomPosition);
                    PlaceRightWall(isRightEdge, roomPosition);
                    PlaceBottomWall(isBottomEdge, roomPosition);
                    PlaceLeftWall(isLeftEdge, roomPosition);
                }
            }
        }

        private void PlaceLeftWall(bool isLeftEdge, Vector3 roomPosition)
        {
            if (isLeftEdge)
            {
                var wallRotation = Quaternion.Euler(0, 270, 0);
                Instantiate(solidWall, roomPosition, wallRotation, _rootTransform);
            }
        }

        private void PlaceBottomWall(bool isBottomEdge, Vector3 roomPosition)
        {
            if (isBottomEdge)
            {
                var wallRotation = Quaternion.Euler(0, 180, 0);
                Instantiate(solidWall, roomPosition, wallRotation, _rootTransform);
            }
        }

        private void PlaceRightWall(bool isRightEdge, Vector3 roomPosition)
        {
            if (isRightEdge)
            {
                var wallRotation = Quaternion.Euler(0, 90, 0);
                Instantiate(solidWall, roomPosition, wallRotation, _rootTransform);
            }
            else
            {
                var wallRotation = Quaternion.Euler(0, 90, 0);
                Instantiate(wallWithDoor, roomPosition, wallRotation, _rootTransform);
            }
        }

        private void PlaceTopWall(bool isTopEdge, Vector3 roomPosition)
        {
            if (isTopEdge)
            {
                Instantiate(solidWall, roomPosition, Quaternion.identity, _rootTransform);
            }
            else
            {
                Instantiate(wallWithDoor, roomPosition, Quaternion.identity, _rootTransform);
            }
        }

        private void PlacePlayer()
        {
            for (var x = 0; x < _level.Rooms.GetLength(0); x++)
            {
                for (var y = 0; y < _level.Rooms.GetLength(1); y++)
                {
                    var room = _level.Rooms[x, y];

                    if (room.HasEntity(typeof(PlayerEntity)))
                    {
                        var roomPosition = new Vector3(x * roomWidth, 0, y * roomLength);
                        Instantiate(player, roomPosition, Quaternion.identity, _rootTransform);
                        return;
                    }
                }
            }
        }

        private void PlaceMonster()
        {
            for (var x = 0; x < _level.Rooms.GetLength(0); x++)
            {
                for (var y = 0; y < _level.Rooms.GetLength(1); y++)
                {
                    var room = _level.Rooms[x, y];

                    if (room.HasEntity(typeof(MonsterEntity)))
                    {
                        var roomPosition = new Vector3(x * roomWidth, 0, y * roomLength);
                        Instantiate(monster, roomPosition, Quaternion.identity, _rootTransform);
                        return;
                    }
                }
            }
        }
    }
}