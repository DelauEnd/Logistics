using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Entities.Configuration
{
    public class Configuration
    {
        public static bool ShouldAddInitialData()
            => ConfigurationManager.AppSettings["AddInitialDataOnDatabaseCreate"] == "true";
    }
}
