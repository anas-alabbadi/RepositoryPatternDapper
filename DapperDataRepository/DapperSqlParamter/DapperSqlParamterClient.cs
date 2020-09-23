using Aregak_Framework_SystemLogger.SystemLogger;
using Aregak_Framework_SystemLogger.SystemLogger.NLogLogger;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application_DataRepository.DapperDataRepository.DapperSqlParamter
{
    public partial class DapperSqlParamterClient
    {
        #region var
        private const string _classNameSpace = "Application_DataRepository.DapperDataRepository.DapperSqlParamter.DapperSqlParamterClient";
        IDapperSqlParameter _dapperSqlParameter;
        #endregion
        #region prop

        #endregion
        #region ctor
        public DapperSqlParamterClient(IDapperSqlParameter dapperSqlParameter)
        {
            _dapperSqlParameter = dapperSqlParameter;
        }
        #endregion
        #region func_public
        public List<DbParameter> DbParametersListGenerator(object model, DbParameter dbParameter)
        {
            List<DbParameter> _dbParameters = new List<DbParameter>();
            try
            {
                _dbParameters = _dapperSqlParameter.DbParametersListGenerator(model, dbParameter);
            }
            catch (Exception ex)
            {

                NlogLoggerClient _client = new NlogLoggerClient(new NLogFileLogger(_classNameSpace));
                _client.ActionLoggerAsync(LogginLevels.LoggingLevels.Error, MethodBase.GetCurrentMethod().Name, ex.Message, ex).Wait();
            }

            return _dbParameters;
        }

        public async Task<List<DbParameter>> DbParametersListGeneratorAsync(object model, DbParameter dbParameter)
        {
            List<DbParameter> _dbParameters = new List<DbParameter>();
            try
            {
                _dbParameters =await _dapperSqlParameter.DbParametersListGeneratorAsync(model, dbParameter);
            }
            catch (Exception ex)
            {

                NlogLoggerClient _client = new NlogLoggerClient(new NLogFileLogger(_classNameSpace));
                await _client.ActionLoggerAsync(LogginLevels.LoggingLevels.Error, MethodBase.GetCurrentMethod().Name, ex.Message, ex);
            }
            return _dbParameters;
        }
        #endregion
    }
}

