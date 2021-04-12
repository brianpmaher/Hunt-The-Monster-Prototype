using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        private bool HasPlayerEntityBeenPlaced()
        {
            for (var x = 0; x < Rooms.GetLength(0); x++)
            {
                for (var y = 0; y < Rooms.GetLength(1); y++)
                {
                    var room = Rooms[x, y];
                    
                    if (room.Entities.Any(entity => entity.GetType() == typeof(PlayerEntity)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}