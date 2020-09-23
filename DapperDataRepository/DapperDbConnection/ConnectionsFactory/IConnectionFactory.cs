using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using static Application_DataRepository.DapperDataRepository.DapperDbConnection.ConnectionsFactory.DbConnectionProviders;
namespace Application_DataRepository.DapperDataRepository.DapperDbConnection.ConnectionsFactory
{
    public interface IConnectionFactory
    {
        DbConnection Create(DbProvidersOptions dbProvidersOptions);
    }
}
