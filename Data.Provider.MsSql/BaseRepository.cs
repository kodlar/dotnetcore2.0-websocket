using Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Data.Provider.MsSql
{
    //apiv2.canliskor.com.tr / LiveScore.Data.Provider.MsSql / BaseRepository.cs
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : TEntity
    {
        private readonly string _schemaName;
        private readonly string _tableName;
        private readonly IConfiguration _configuration;
        private readonly string _connectionStringKey;

        public bool Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Execute(Action<IDbConnection> query)
        {
            throw new NotImplementedException();
        }

        public IList<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public TEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
