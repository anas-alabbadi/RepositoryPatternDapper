using System;
using System.Collections.Generic;
using System.Text;

namespace Application_DataRepository.DapperDataRepository.DapperDbConnection.ConnectionServiceHandler
{
    public static partial class FrameworkConnectionStringHandler
    {
        #region var
        private static string _connectionString = string.Empty;
        #endregion
        #region prop
        public static string serviceConnectionString => _connectionString;
        #endregion
        #region func
        public static void SetConnectionString(string connectionString) => _connectionString = connectionString;
        #endregion
    }
}
