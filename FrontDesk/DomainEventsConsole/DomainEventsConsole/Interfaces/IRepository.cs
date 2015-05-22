using System;
using System.Collections.Generic;

namespace DomainEventsConsole.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        TEntity GetById(Guid id);

        IList<TEntity> GetAll<TEntity>();

        void Save(TEntity entity);
    }
}