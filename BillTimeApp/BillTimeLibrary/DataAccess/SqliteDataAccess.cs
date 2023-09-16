using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillTimeLibrary.DataAccess
{
    public class SqliteDataAccess
    {
        public static List<T> LoadData<T>(string sqlStatement, Dictionary<string, object> parameters, string dbName = "Default")
        {
            DynamicParameters p = new DynamicParameters();

            parameters.ToList().ForEach(x => p.Add(x.Key, x.Value)); // One lines but compact that the other manual labour.

            //foreach (var param in parameters)
            //{
            //    p.Add(param.Key, param.Value);
            //}
        }
    }
}
