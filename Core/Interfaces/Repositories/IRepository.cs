using Data.Core.Entitites;
using System;
using System.Collections.Generic;
using System.Data;


namespace Data.Core.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        bool Insert(TEntity entity);

        bool Update(TEntity entity);

        bool Delete(TEntity entity);

        IList<TEntity> GetAll();

        TEntity GetById(int id);

        void Execute(Action<IDbConnection> query);
    }
}
