using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace MapAPI.Helpers
{
    public class DBHelpers
    {
        public static string GetSqlConnectionString(string name)
        {
            string connStr = Environment.GetEnvironmentVariable($"ConnectionStrings:{name}", EnvironmentVariableTarget.Process);
            if (string.IsNullOrEmpty(connStr)) // Azure Functions App Service naming convention
                connStr = Environment.GetEnvironmentVariable($"SQLCONNSTR_{name}", EnvironmentVariableTarget.Process);  // should be used on SQL Server
            if (string.IsNullOrEmpty(connStr))
                connStr = Environment.GetEnvironmentVariable($"SQLAZURECONNSTR_{name}", EnvironmentVariableTarget.Process);  // should be used on Azure
            //if (string.IsNullOrEmpty(connStr)) 
            //    connStr = ConfigurationManager.ConnectionStrings[name].ConnectionString;    // localhost accessing Connection.Config
            return connStr;
        }
    }
}
