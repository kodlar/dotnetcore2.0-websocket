using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace Data.Provider.MsSql.Extensions
{
    public static class DapperExtensions
    {
        public static T Insert<T>(this IDbConnection cnn, string tableName, dynamic param)
        {
            IEnumerable<T> result = SqlMapper.Query<T>(cnn, DynamicQuery.GetInsertQuery(tableName, param), param);
            return result.First();
        }

        public static void Update(this IDbConnection cnn, string tableName, dynamic param)
        {
            SqlMapper.Execute(cnn, DynamicQuery.GetUpdateQuery(tableName, param), param);
        }

        //TODO: Kullanılmayabilir
        public static DataTable ToArrayTvp(this IEnumerable<int> values, string tableName)
        {
            var table = new DataTable();
            table.TableName = tableName;
            var idColumn = new DataColumn("Item", typeof(int));
            table.Columns.Add(idColumn);

            if (values != null)
            {
                foreach (var value in values)
                {
                    var row = table.NewRow();
                    row[idColumn] = value;
                    table.Rows.Add(row);
                }
            }

            return table;
        }
    }
}
