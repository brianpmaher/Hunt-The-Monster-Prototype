using System.Collections.Generic;

namespace HuntTheMonster.LevelGeneration
{
    public class Room
    {
        public List<IEntity> Entities;

        public Room()
        {
            Entities = new List<IEntity>();
        }
    }
}