using System;
using System.Configuration;

namespace MapAPI.Helpers
{
    // Helper functions
    public class DBHelpers
    {
        /**
         * Resolve where to get the connection string from depending on the environment
         * ConfirguationManager gets from Connection.Config
         * SQLCONNSTR are for SQL Servers
         * SQLAZURECONNSTR are for Azure SQL servers
         */
        public static string GetSqlConnectionString(string name)
        {
            string connStr = Environment.GetEnvironmentVariable($"ConnectionStrings:{name}", EnvironmentVariableTarget.Process);
            if (string.IsNullOrEmpty(connStr))
                connStr = ConfigurationManager.ConnectionStrings[name].ConnectionString;    // localhost accessing Connection.Config
            if (string.IsNullOrEmpty(connStr)) // Azure Functions App Service naming convention
                connStr = Environment.GetEnvironmentVariable($"SQLCONNSTR_{name}", EnvironmentVariableTarget.Process);  // should be used on SQL Server
            if (string.IsNullOrEmpty(connStr))
                connStr = Environment.GetEnvironmentVariable($"SQLAZURECONNSTR_{name}", EnvironmentVariableTarget.Process);  // should be used on Azure
            return connStr;
        }
    }
}
