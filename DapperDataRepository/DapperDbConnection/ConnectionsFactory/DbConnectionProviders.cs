using System;
using System.Collections.Generic;
using System.Text;

namespace Application_DataRepository.DapperDataRepository.DapperDbConnection.ConnectionsFactory
{
    public partial class DbConnectionProviders
    {
        public enum DbProvidersOptions
        {
            MsSqlServer,
            Oracle,
            Sybase,
            SQLLite,
            MySql

        }
    }
}
