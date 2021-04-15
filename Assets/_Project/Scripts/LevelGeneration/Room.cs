using System;
using System.Collections.Generic;
using System.Linq;
using HuntTheMonster.Environment;

namespace HuntTheMonster.LevelGeneration
{
    public class Room
    {
        public List<IEntity> Entities;
        public List<Markable> MarkableDoors;

        public bool HasAnyEntities => Entities.Count > 0;

        public Room()
        {
            Entities = new List<IEntity>();
            MarkableDoors = new List<Markable>();
        }

        public bool HasEntity(Type entityType)
        {
            return Entities.Any(entity => entity.GetType() == entityType);
        }
    }
}