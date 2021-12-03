using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class TriggersConfiguration
    {
        public static void Configure()
        {
            AddTriggers();
        }

        private static void AddTriggers()
        {
            DapperExecutor.ExecuteQuery(Queries.OnCargoUpdate);
            DapperExecutor.ExecuteQuery(Queries.OnCargoCreate);
            DapperExecutor.ExecuteQuery(Queries.SetCompletedOrderStatusIfAllCargoesAreCompleted);
        }
    }
}
