using System;
using HuntTheMonster.Environment;
using HuntTheMonster.Player;
using HuntTheMonster.UI;
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
        [SerializeField] private GameObject monsterWarningTrigger;
        [SerializeField] private GameObject trap;
        [SerializeField] private GameObject trapWarningTrigger;
        
        private Transform _rootTransform;
        private Level _level;
        
        private void Awake()
        {
            _rootTransform = rootLevel.transform;
        }

        private void Start()
        {
            GenerateLevelEntities();
            PlaceLevelObjects();
        }

        private void GenerateLevelEntities()
        {
            _level = new Level(levelWidth, levelLength);
            _level.PlacePlayerStart();
            _level.PlaceMonsterStart();
            _level.PlaceTraps(numTraps);
        }
        
        private void PlaceLevelObjects() 
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
                    var roomCoordinate = new Vector2Int(x, y);
                    var position = new Vector3(x * roomWidth, 0, y * roomLength);

                    if (room.HasEntity(typeof(TrapEntity)))
                    {
                        PlaceTrapFloor(position);
                    }
                    else
                    {
                        PlaceRegularFloor(position);
                    }
                }
            }
        }

        private void PlaceTrapFloor(Vector3 position)
        {
            var topBottomOffset = new Vector3(0, 0, roomLength);
            var leftRightOffset = new Vector3(roomWidth, 0, 0);
            
            Instantiate(trap, position, Quaternion.identity, _rootTransform);
            Instantiate(trapWarningTrigger, position + topBottomOffset, Quaternion.identity, _rootTransform);
            Instantiate(trapWarningTrigger, position - topBottomOffset, Quaternion.identity, _rootTransform);
            Instantiate(trapWarningTrigger, position + leftRightOffset, Quaternion.identity, _rootTransform);
            Instantiate(trapWarningTrigger, position - leftRightOffset, Quaternion.identity, _rootTransform);
        }

        private void PlaceRegularFloor(Vector3 position)
        {
            Instantiate(roomFloor, position, Quaternion.identity, _rootTransform);
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
                    var roomCoord = new Vector2Int(x, y);

                    PlaceTopWall(isTopEdge, roomPosition, roomCoord);
                    PlaceRightWall(isRightEdge, roomPosition, roomCoord);
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

        private void PlaceRightWall(bool isRightEdge, Vector3 roomPosition, Vector2Int roomCoord)
        {
            if (isRightEdge)
            {
                var wallRotation = Quaternion.Euler(0, 90, 0);
                Instantiate(solidWall, roomPosition, wallRotation, _rootTransform);
            }
            else
            {
                var wallRotation = Quaternion.Euler(0, 90, 0);
                var door = Instantiate(wallWithDoor, roomPosition, wallRotation, _rootTransform);
                var markable = door.GetComponent<WallWithDoor>().Door.GetComponent<Markable>();
                var room = _level.Rooms[roomCoord.x, roomCoord.y];
                var isThereRoomToRight = roomCoord.x < _level.Rooms.GetLength(0) - 1;
                
                room.MarkableDoors.Add(markable);

                if (isThereRoomToRight)
                {
                    var rightRoom = _level.Rooms[roomCoord.x + 1, roomCoord.y];
                    rightRoom.MarkableDoors.Add(markable);
                }
            }
        }

        private void PlaceTopWall(bool isTopEdge, Vector3 roomPosition, Vector2Int roomCoord)
        {
            if (isTopEdge)
            {
                Instantiate(solidWall, roomPosition, Quaternion.identity, _rootTransform);
            }
            else
            {
                var door = Instantiate(wallWithDoor, roomPosition, Quaternion.identity, _rootTransform);
                var markable = door.GetComponent<WallWithDoor>().Door.GetComponent<Markable>();
                var room = _level.Rooms[roomCoord.x, roomCoord.y];
                var isThereRoomToTop = roomCoord.y < _level.Rooms.GetLength(1) - 1;
                
                room.MarkableDoors.Add(markable);

                if (isThereRoomToTop)
                {
                    var topRoom = _level.Rooms[roomCoord.x, roomCoord.y];
                    topRoom.MarkableDoors.Add(markable);
                }
            }
        }

        private void PlacePlayer()
        {
            var playerRoomCoord = FindRoomWithEntity(typeof(PlayerEntity));
            var roomPosition = new Vector3(playerRoomCoord.x * roomWidth, 0, playerRoomCoord.y * roomLength);
            var playerInstance = Instantiate(player, roomPosition, Quaternion.identity, _rootTransform);
            var playerCamera = playerInstance.GetComponent<PlayerManager>().PlayerCamera;
            var crosshairContainer = FindObjectOfType<CrosshairContainer>();
            crosshairContainer.playerCamera = playerCamera.GetComponent<Camera>();
            var gameManager = FindObjectOfType<GameManager>();
            gameManager.cursorLockScript = playerInstance.GetComponent<CursorLock>();
        }

        private void PlaceMonster()
        {
            var monsterRoomCoord = FindRoomWithEntity(typeof(MonsterEntity));
            var position = new Vector3(monsterRoomCoord.x * roomWidth, 0, monsterRoomCoord.y * roomLength);
            var room = _level.Rooms[monsterRoomCoord.x, monsterRoomCoord.y];
            var topBottomOffset = new Vector3(0, 0, roomLength);
            var leftRightOffset = new Vector3(roomWidth, 0, 0);
            
            Instantiate(monster, position, Quaternion.identity, _rootTransform);
            room.MarkableDoors.ForEach(markable => markable.isMonsterDoor = true);
            Instantiate(monsterWarningTrigger, position + topBottomOffset, Quaternion.identity, _rootTransform);
            Instantiate(monsterWarningTrigger, position - topBottomOffset, Quaternion.identity, _rootTransform);
            Instantiate(monsterWarningTrigger, position + leftRightOffset, Quaternion.identity, _rootTransform);
            Instantiate(monsterWarningTrigger, position - leftRightOffset, Quaternion.identity, _rootTransform);
        }

        private Vector2Int FindRoomWithEntity(Type entityType)
        {
            for (var x = 0; x < _level.Rooms.GetLength(0); x++)
            {
                for (var y = 0; y < _level.Rooms.GetLength(1); y++)
                {
                    var room = _level.Rooms[x, y];

                    if (room.HasEntity(entityType))
                    {
                        return new Vector2Int(x, y);
                    }
                }
            }

            throw new Exception("Room with entity type " + entityType + " not found");
        }
    }
}