//using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;

namespace Infrastructure
{
    public class SqlConnections
    {
        
        private readonly IConfiguration _configuration;
        private readonly string _connectionStringKey;
        public SqlConnections(IConfiguration configuration, string connectionStringKey)
        {
            _configuration = configuration;
            _connectionStringKey = connectionStringKey;
        }

        //string ConnectionString()
        //{
        //    return Configuration.GetConnectionString("ArtTv");
        //}

        public IDbConnection ArtTv()
        {
            
            //ConfigurationManager.ConnectionStrings["ArtTv"].ConnectionString
            //return new SqlConnection("Data Source=77.223.152.100;Initial Catalog=Ekavart.tv;User ID=web;Password=1qaz2wsx!");
            return new SqlConnection(_configuration[_connectionStringKey]);
        }
    }
}
