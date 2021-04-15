using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace HuntTheMonster.LevelGeneration
{
    public class Level
    {
        public Room[,] Rooms { get; private set; }

        public Level(int width, int length)
        {
            InitializeRooms(width, length);
        }

        private void InitializeRooms(int width, int length)
        {
            Rooms = new Room[width, length];

            for (var x = 0; x < Rooms.GetLength(0); x++)
            {
                for (var y = 0; y < Rooms.GetLength(1); y++)
                {
                    Rooms[x, y] = new Room();
                }
            }
        }

        public void PlacePlayerStart()
        {
            if (HasPlayerEntityBeenPlaced()) return;
            var outerRooms = GetOuterRooms();
            var playerStartRoom = outerRooms[Random.Range(0, outerRooms.Count)];
            playerStartRoom.Entities.Add(new PlayerEntity());
        }

        public void PlaceMonsterStart()
        {
            if (HasMonsterEntityBeenPlaced()) return;
            var validMonsterRooms = GetValidMonsterRooms();
            var monsterStartRoom = validMonsterRooms[Random.Range(0, validMonsterRooms.Count)];
            monsterStartRoom.Entities.Add(new MonsterEntity());
        }

        public void PlaceTrap()
        {
            var validTrapRooms = GetValidTrapRooms();
            var monsterStartRoom = validTrapRooms[Random.Range(0, validTrapRooms.Count)];
            monsterStartRoom.Entities.Add(new TrapEntity());
        }

        public void PlaceTraps(int numTraps)
        {
            for (var i = 0; i < numTraps; i++)
            {
                PlaceTrap();
            }
        }

        private List<Room> GetOuterRooms()
        {
            var roomsWidth = Rooms.GetLength(0);
            var roomsLength = Rooms.GetLength(1);
            var outerRooms = new List<Room>();
            
            for (var x = 0; x < roomsWidth; x++)
            {
                for (var y = 0; y < roomsLength; y++)
                {
                    var isOuterRoom = x == 0 || x == roomsWidth - 1 || y == 0 || y == roomsLength - 1;

                    if (isOuterRoom)
                    {
                        outerRooms.Add(Rooms[x, y]);
                    }
                }
            }

            return outerRooms;
        }

        private List<Room> GetValidMonsterRooms()
        {
            var roomsWidth = Rooms.GetLength(0);
            var roomsLength = Rooms.GetLength(1);
            var validMonsterRooms = new List<Room>();
            
            for (var x = 0; x < roomsWidth; x++)
            {
                for (var y = 0; y < roomsLength; y++)
                {
                    var room = Rooms[x, y];

                    if (room.HasAnyEntities) continue;
                    if (AnyAdjacentRoomsHasPlayer(x, y)) continue;
                    
                    validMonsterRooms.Add(room);
                }
            }

            return validMonsterRooms;
        }
        
        private List<Room> GetValidTrapRooms()
        {
            var roomsWidth = Rooms.GetLength(0);
            var roomsLength = Rooms.GetLength(1);
            var validTrapRooms = new List<Room>();
            
            for (var x = 0; x < roomsWidth; x++)
            {
                for (var y = 0; y < roomsLength; y++)
                {
                    var room = Rooms[x, y];
                    
                    if (room.HasAnyEntities) continue;
                    if (AnyAdjacentRoomsHasPlayer(x, y)) continue;
                    
                    validTrapRooms.Add(room);
                }
            }

            return validTrapRooms;
        }

        private bool HasPlayerEntityBeenPlaced()
        {
            return HasEntityBeenPlaced(typeof(PlayerEntity));
        }

        private bool HasMonsterEntityBeenPlaced()
        {
            return HasEntityBeenPlaced(typeof(MonsterEntity));
        }

        private bool HasEntityBeenPlaced(Type entityType)
        {
            for (var x = 0; x < Rooms.GetLength(0); x++)
            {
                for (var y = 0; y < Rooms.GetLength(1); y++)
                {
                    var room = Rooms[x, y];
                    
                    if (room.Entities.Any(entity => entity.GetType() == entityType))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool AnyAdjacentRoomsHasPlayer(int roomX, int roomY)
        {
            return AnyAdjacentRoomsHasEntity(roomX, roomY, typeof(PlayerEntity));
        }

        private bool AnyAdjacentRoomsHasEntity(int roomX, int roomY, Type entityType)
        {
            var roomsWidth = Rooms.GetLength(0);
            var roomsLength = Rooms.GetLength(1);
            var adjacentRooms = new List<Room>();

            if (roomX > 0)
            {
                var westRoom = Rooms[roomX - 1, roomY];
                adjacentRooms.Add(westRoom);
            }

            if (roomX < roomsWidth - 1)
            {
                var eastRoom = Rooms[roomX + 1, roomY];
                adjacentRooms.Add(eastRoom);
            }

            if (roomY > 0)
            {
                var southRoom = Rooms[roomX, roomY - 1];
                adjacentRooms.Add(southRoom);
            }

            if (roomY < roomsLength - 1)
            {
                var northRoom = Rooms[roomX, roomY + 1];
                adjacentRooms.Add(northRoom);
            }

            return adjacentRooms.Any(r => r.HasEntity(entityType));
        }
    }
}