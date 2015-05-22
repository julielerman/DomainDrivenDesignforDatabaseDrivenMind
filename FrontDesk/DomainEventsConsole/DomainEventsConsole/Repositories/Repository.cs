using DomainEventsConsole.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainEventsConsole.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        private readonly Dictionary<Guid, TEntity> entities = new Dictionary<Guid, TEntity>();
        public TEntity GetById(Guid id)
        {
            return entities[id];
        }

        public IList<TEntity> GetAll<TEntity>()
        {
            return (IList<TEntity>)entities.Values.ToList();
        }

        public void Save(TEntity entity)
        {
            entities[entity.Id] = entity;
            ConsoleWriter.FromRepositories("[DATABASE] Saved entity {0}", entity.Id.ToString());
        }
    }
}
