using System;
using System.Data;

namespace Infrastructure
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();
    }
}
