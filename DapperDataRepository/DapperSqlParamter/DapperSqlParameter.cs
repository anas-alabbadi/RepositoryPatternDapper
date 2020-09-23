using Aregak_Framework_SystemLogger.SystemLogger;
using Aregak_Framework_SystemLogger.SystemLogger.NLogLogger;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Application_DataRepository.DapperDataRepository.Attributes;
namespace Application_DataRepository.DapperDataRepository.DapperSqlParamter
{
    public partial class DapperSqlParameter<TParameter> : IDapperSqlParameter where TParameter:DbParameter,new()
    {
        #region var
        private const string _classNameSpace = "Application_DataRepository.DapperDataRepository.DapperSqlParamter.DapperSqlParameter<TDbParameter>";
        #endregion
        #region prop

        #endregion
        #region ctor

        #endregion
        #region func_public
        public List<DbParameter> DbParametersListGenerator(object model, DbParameter dbParameter)
        {
            List<DbParameter> _dbParameters = new List<DbParameter>();
            try
            {
                PropertyInfo[] _paramPropertyInfo = model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo item in _paramPropertyInfo)
                {
                    SqlDataTypeAttribute _sqlDataTypeAttribute =(SqlDataTypeAttribute) item.GetCustomAttribute(typeof(SqlDataTypeAttribute));
                    ParameterAttribute _parameterAttribute = (ParameterAttribute)item.GetCustomAttribute(typeof(ParameterAttribute));
                    var _value = item.GetValue(model);
                    DbParameter _dbParameter =(DbParameter) new TParameter();
                    _dbParameter.DbType = _sqlDataTypeAttribute.SqlDbType;
                    _dbParameter.ParameterName = _parameterAttribute.value;
                    _dbParameter.Value = _value == null ? DBNull.Value : _value;
                    _dbParameters.Add(_dbParameter);
                    
                }
            }
            catch (Exception ex)
            {
                NlogLoggerClient _client = new NlogLoggerClient(new NLogFileLogger(_classNameSpace));
                 _client.ActionLoggerAsync(LogginLevels.LoggingLevels.Error, MethodBase.GetCurrentMethod().Name, ex.Message, ex).Wait();
                throw new Exception(ex.Message, ex);
            }
            return _dbParameters;
        }

        public async Task<List<DbParameter>> DbParametersListGeneratorAsync(object model, DbParameter dbParameter)
        {
            List<DbParameter> _dbParameters = new List<DbParameter>();
            try
            {
                _dbParameters =await Task.Run(() => {
                    List<DbParameter> _list = new List<DbParameter>();
                    PropertyInfo[] _paramPropertyInfo = model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (PropertyInfo item in _paramPropertyInfo)
                    {
                        SqlDataTypeAttribute _sqlDataTypeAttribute = (SqlDataTypeAttribute)item.GetCustomAttribute(typeof(SqlDataTypeAttribute));
                        ParameterAttribute _parameterAttribute = (ParameterAttribute)item.GetCustomAttribute(typeof(ParameterAttribute));
                        var _value = item.GetValue(model);
                        DbParameter _dbParameter = (DbParameter)new TParameter();
                        _dbParameter.DbType = _sqlDataTypeAttribute.SqlDbType;
                        _dbParameter.ParameterName = _parameterAttribute.value;
                        _dbParameter.Value = _value == null? DBNull.Value : _value;
                        _list.Add(_dbParameter);

                    }
                    return _list;



                });
            }
            catch (Exception ex)
            {
                NlogLoggerClient _client = new NlogLoggerClient(new NLogFileLogger(_classNameSpace));
                await _client.ActionLoggerAsync(LogginLevels.LoggingLevels.Error, MethodBase.GetCurrentMethod().Name, ex.Message, ex);
                throw new Exception(ex.Message, ex);
            }
            return _dbParameters;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
        #endregion

    }
}
