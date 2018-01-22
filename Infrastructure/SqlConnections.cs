//using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace Infrastructure
{
    public class SqlConnections
    {
        public IConfiguration Configuration { get; set; }

        public SqlConnections(IConfiguration config)
        {
            Configuration = config;
        }

        string ConnectionString()
        {
            return Configuration.GetConnectionString("ArtTv");
        }

        public static IDbConnection ArtTv()
        {
            
            //ConfigurationManager.ConnectionStrings["ArtTv"].ConnectionString
            return new SqlConnection("Data Source=77.223.152.100;Initial Catalog=Ekavart.tv;User ID=web;Password=1qaz2wsx!");
        }
    }
}
