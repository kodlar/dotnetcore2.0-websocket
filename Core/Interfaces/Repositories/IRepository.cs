using Core.Entitites;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Core.Interfaces.Repositories
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
