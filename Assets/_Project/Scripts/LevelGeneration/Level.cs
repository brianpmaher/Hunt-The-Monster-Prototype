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
                    var adjacentRooms = new List<Room>();

                    if (x > 0)
                    {
                        var westRoom = Rooms[x - 1, y];
                        adjacentRooms.Add(westRoom);
                    }

                    if (x < roomsWidth - 1)
                    {
                        var eastRoom = Rooms[x + 1, y];
                        adjacentRooms.Add(eastRoom);
                    }

                    if (y > 0)
                    {
                        var southRoom = Rooms[x, y - 1];
                        adjacentRooms.Add(southRoom);
                    }

                    if (y < roomsLength - 1)
                    {
                        var northRoom = Rooms[x, y + 1];
                        adjacentRooms.Add(northRoom);
                    }

                    if (room.HasPlayerEntity()) continue;
                    if (adjacentRooms.Any(r => r.HasPlayerEntity())) continue;
                    validMonsterRooms.Add(room);
                }
            }

            return validMonsterRooms;
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
    }
}