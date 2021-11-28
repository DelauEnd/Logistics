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

        public static async Task ExecuteQueryAsync(string query)
        {
            try
            {
                await ExecuteAsync(query);
            }
            catch { }           
        }

        private async static Task ExecuteAsync(string query)
        {
            using (var db = new SqlConnection(GetConString()))
            {
               await db.QueryAsync(query);
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
