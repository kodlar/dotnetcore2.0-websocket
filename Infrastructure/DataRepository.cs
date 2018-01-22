using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Dapper;
namespace Infrastructure
{
    public class DataRepository : IDataRepository
    {
        
        public List<Video> GetAll()
        {
            List<Video> lst = new List<Video>();
            try
            {
                
                using (IDbConnection db = SqlConnections.ArtTv())
                {
                    db.Query<Video>("Select Id From Video").AsList();
                }
            }
            catch(Exception ex)
            {
                lst = null;
                Debug.WriteLine(ex.Message);
            }
           
            return lst;
        }

      
    }
}
