using System;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure
{
    public class ConnectionFactory : IConnectionFactory
    {
        //private readonly string connectionString = WebConfigurationManager.ConnectionStrings["MyCon"].ConnectionString; 
        //string connectionString = WebConfigurationManager.AppSettings["StorageConnectionString"];
        //string conString = Microsoft.Extensions.Configuration.ConfigurationExtensions.GetConnectionString(this.Configuration, "DefaultConnection");
        public IDbConnection GetConnection()
        {
            IDbConnection db = new SqlConnection("connection");
            db.Open();
            return db;
             
        }
    }
}
