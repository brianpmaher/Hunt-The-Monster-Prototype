using System.Collections.Generic;
using System.Linq;

namespace HuntTheMonster.LevelGeneration
{
    public class Room
    {
        public List<IEntity> Entities;

        public Room()
        {
            Entities = new List<IEntity>();
        }

        public bool HasPlayerEntity()
        {
            return Entities.Any(entity => entity.GetType() == typeof(PlayerEntity));
        }
        
        public bool HasMonsterEntity()
        {
            return Entities.Any(entity => entity.GetType() == typeof(MonsterEntity));
        }
    }
}