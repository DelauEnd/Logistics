using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Entities
{
    public class DapperExecutor
    {
        private static string GetConString()
        {
            return ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        }

        public static void ExecuteQuery(string query)
        {
            try
            {
                Execute(query);
            }
            catch { }           
        }

        private static void Execute(string query)
        {
            using (var db = new SqlConnection(GetConString()))
            {
               db.Query(query);
            }
        }

        public async static Task<List<T>> GetExecutedValueAsync<T>(string query)
        {
            using (var db = new SqlConnection(GetConString()))
            {
                var executed = await db.QueryAsync<T>(query);
                return executed.ToList();
;            }
        }
    }
}
