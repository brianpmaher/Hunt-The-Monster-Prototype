using System;
using System.Collections.Generic;
using System.Linq;

namespace HuntTheMonster.LevelGeneration
{
    public class Room
    {
        public List<IEntity> Entities;

        public bool HasAnyEntities => Entities.Count > 0;

        public Room()
        {
            Entities = new List<IEntity>();
        }

        public bool HasEntity(Type entityType)
        {
            return Entities.Any(entity => entity.GetType() == entityType);
        }
    }
}