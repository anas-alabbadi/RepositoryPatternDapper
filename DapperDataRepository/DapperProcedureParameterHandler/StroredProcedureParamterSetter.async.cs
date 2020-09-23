using Application_DataRepository.DapperDataRepository.Extensions;
using Aregak_Framework_SystemLogger.SystemLogger;
using Aregak_Framework_SystemLogger.SystemLogger.NLogLogger;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application_DataRepository.DapperDataRepository.DapperProcedureParameterHandler
{
    public partial class StroredProcedureParamterSetter
    {
        #region public_func
        #region AsyncSetterFunction
        //public async Task<List<SqlParameter>> SqlParametersSetterAsync(object paramObject)
        //{
        //    List<SqlParameter> _listSqlParamter = new List<SqlParameter>();
        //    string _sqldatatype = string.Empty;
        //    try
        //    {
        //        _listSqlParamter = await Task.Run(() => {
        //            List<SqlParameter> _list = new List<SqlParameter>();
        //            Type _paramType = paramObject.GetType();
        //            PropertyInfo[] _paramPropertyInfo = _paramType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //            foreach (PropertyInfo prop in _paramPropertyInfo)
        //            {
        //                _list.Add(new SqlParameter() { ParameterName = string.Format("@{0}", prop.Name), SqlDbType = _sqldatatype.getSqlDbType(prop), Value = prop.GetValue(paramObject) == null ? DBNull.Value : prop.GetValue(paramObject) });
        //            }
        //            return _list;

        //        });
                
        //        return _listSqlParamter;
        //    }
        //    catch (Exception ex)
        //    {
        //        NlogLoggerClient _client = new NlogLoggerClient(new NLogFileLogger("Application_DataRepository.DapperDataRepository.DapperProcedureParameterHandler.StroredProcedureParamterSetter"));
        //        _client.ActionLoggerAsync(LogginLevels.LoggingLevels.Error, MethodBase.GetCurrentMethod().Name, ex.Message, ex).Wait();
        //    }
        //    return null;
        //}
        #endregion
        #endregion
    }
}
