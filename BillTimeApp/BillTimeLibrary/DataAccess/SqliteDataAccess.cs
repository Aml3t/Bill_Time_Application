using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillTimeLibrary.DataAccess
{
    public static class SqliteDataAccess
    {
        public static List<T> LoadData<T>(string sqlStatement, Dictionary<string, object> parameters, string connectionName = "Default")
        {
            parameters.ToDynamicParameters();
            //DynamicParameters p = new DynamicParameters();

            //parameters.ToList().ForEach(x => p.Add(x.Key, x.Value)); // Oneliner but more compact that the other manual labour.
            //foreach (var param in parameters)
            //{
            //    p.Add(param.Key, param.Value);
            //}
            using (IDbConnection connection = new SQLiteConnection(DataAccessHelpers.LoadCoonnectionString(connectionName)))
            {
                var rows = connection.Query<T>(sqlStatement, p);
                return rows.ToList();
            }
        }

        public static void SaveData(string sqlStatement, Dictionary<string, object> parameters, string connectionName = "Default")
        {
            DynamicParameters p = new DynamicParameters();

            parameters.ToList().ForEach(x => p.Add(x.Key, x.Value));

            using (IDbConnection connection = new SQLiteConnection(DataAccessHelpers.LoadCoonnectionString(connectionName)))
            {
                var rows = connection.Execute(sqlStatement, p);
            }
        }

        private static DynamicParameters ToDynamicParameters(this Dictionary<string, object> parameters)
        {
            DynamicParameters output = new DynamicParameters();

            parameters.ToList().ForEach(x => output.Add(x.Key, x.Value));

            return output;
        }


    }
}
