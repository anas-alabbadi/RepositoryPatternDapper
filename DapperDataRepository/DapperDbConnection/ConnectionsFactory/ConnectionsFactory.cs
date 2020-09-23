using AdoNetCore.AseClient;
using Aregak_Framework_SystemLogger.SystemLogger;
using Aregak_Framework_SystemLogger.SystemLogger.NLogLogger;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Reflection;
using System.Text;
using static Application_DataRepository.DapperDataRepository.DapperDbConnection.ConnectionsFactory.DbConnectionProviders;
using Application_DataRepository.DapperDataRepository.DapperDbConnection.ConnectionExceptionsHandler;
using Application_DataRepository.DapperDataRepository.DapperDbConnection.ConnectionServiceHandler;
namespace Application_DataRepository.DapperDataRepository.DapperDbConnection.ConnectionsFactory
{
    public partial class ConnectionsFactory : IConnectionFactory, IDisposable
    {
        #region var 
        private const string _classFullName = "Application_DataRepository.DapperDataRepository.DapperDbConnection.ConnectionsFactory.ConnectionsFactory";
        private DbConnection _dbConnection = null;
        #endregion
        #region func
        #region Create
        public DbConnection Create(DbProvidersOptions dbProvidersOptions)
        {
            try
            {
                switch (dbProvidersOptions)
                {
                    case DbProvidersOptions.MsSqlServer:
                        _dbConnection = new SqlConnection(GetConnectionString());
                        break;
                    case DbProvidersOptions.Oracle:
                        //_dbConnection = new System.Data.OracleClient.OracleConnection(GetConnectionString());
                        break;
                    case DbProvidersOptions.Sybase:
                        _dbConnection = new AseConnection(GetConnectionString());
                        break;
                    case DbProvidersOptions.SQLLite:
                        _dbConnection = new SQLiteConnection(GetConnectionString());
                        break;
                    case DbProvidersOptions.MySql:
                        _dbConnection = new MySqlConnection(GetConnectionString());
                        break;

                }
               
            }
            catch (Exception ex)
            {
                NlogLoggerClient _client = new NlogLoggerClient(new NLogFileLogger(_classFullName));
                _client.ActionLoggerAsync(LogginLevels.LoggingLevels.Error, MethodBase.GetCurrentMethod().Name, ex.Message, ex).Wait();
            }
            return _dbConnection;
        }
        #endregion
       
        #endregion
        #region private_func
        private string GetConnectionString()
        {
            string _connectionString = FrameworkConnectionStringHandler.serviceConnectionString;
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new ConnectionStringException("Connection String Can't Be Null Or Empty Please check App Configuration ");
            }
            return _connectionString;
        }
        #endregion
        #region dispose
        public void Dispose()
        {
            if (_dbConnection!=null)
            {
                _dbConnection.Dispose();
            }
            
        }
        #endregion
    }
}
