using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Data.Core.Interfaces.Repositories;
using Dapper;
using Data.Provider.MsSql.Extensions;
using Data.Core.Entitites;


namespace Data.Provider.MsSql
{
        public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly string _schemaName;
        private readonly string _tableName;
        private readonly IConfiguration _configuration;
        private readonly string _connectionStringKey;

        protected BaseRepository(IConfiguration configuration, string connectionStringKey, string tableName, string schemaName = "dbo")
        {
            _schemaName = schemaName;
            _tableName = tableName;
            _configuration = configuration;
            _connectionStringKey = connectionStringKey;
        }
        public IDbConnection GetConnection()
        {
            //_configuration[_connectionStringKey]
            return new SqlConnection(_configuration.GetConnectionString(_connectionStringKey));
            
        }

        public string GetConnectionString()
        {
            //return _configuration[_connectionStringKey];
            return _configuration.GetConnectionString(_connectionStringKey);
        }
        

        public IList<TEntity> GetAll()
        {
            IList<TEntity> items = null;

            using (IDbConnection cn = GetConnection())
            {
                //cn.Open();
                items = cn.Query<TEntity>($"SELECT * FROM [{_schemaName}].[{_tableName}]").ToList();
            }

            return items;
        }

        public TEntity GetById(int id)
        {
            TEntity item;

            using (IDbConnection cnn = GetConnection())
            {
                //cnn.Open();
                item = cnn.Query<TEntity>($"SELECT * FROM [{_schemaName}].[{_tableName}] WHERE Id = @Id", new { Id = id }).SingleOrDefault();
            }

            return item;
        }

        public void Execute(Action<IDbConnection> query)
        {
            using (IDbConnection db = GetConnection())
            {
                query.Invoke(db);
            }
        }

        public bool Insert(TEntity entity)
        {
            using (IDbConnection cnn = GetConnection())
            {
                var parameters = (object)Mapping(entity);
                //cnn.Open();
                return cnn.Insert<int>(_tableName, parameters) != -1;
            }
        }
        internal virtual dynamic Mapping(TEntity entity)
        {
            return entity;
        }
        public bool Update(TEntity entity)
        {
            using (IDbConnection cnn = GetConnection())
            {
                var parameters = (object)Mapping(entity);
                //cnn.Open();
                cnn.Update(_tableName, parameters);
                return true;
            }
        }

        public bool Delete(TEntity entity)
        {
            using (IDbConnection cnn = GetConnection())
            {
                //cnn.Open();
                cnn.Query<TEntity>($"DELETE FROM [{_schemaName}].[{_tableName}] WHERE Id = @Id", new { Id = entity.Id });
                return true;
            }
        }

        public async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration[_connectionStringKey]))
                {
                    await connection.OpenAsync(); // Asynchronously open a connection to the database
                    return await getData(connection); // Asynchronously execute getData, which has been passed in as a Func<IDBConnection, Task<T>>
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }


    }
}
