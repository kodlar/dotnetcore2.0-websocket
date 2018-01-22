using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface  IDataRepository
    {
        List<Video> GetAll();
    }
}
