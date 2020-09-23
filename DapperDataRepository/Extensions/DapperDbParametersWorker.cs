using Application_DataRepository.DapperDataRepository.Attributes;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application_DataRepository.DapperDataRepository.Extensions
{
    public static partial class DapperDbParametersWorker
    {
        public static async Task<List<DbParameter>> UseDbParameterGenerator<TParameter>(this object sender) where TParameter: DbParameter, new ()
        {
            List<DbParameter> _dbParameters = new List<DbParameter>();
            try
            {
                _dbParameters = await Task.Run(() => {
                    List<DbParameter> _list = new List<DbParameter>();
                    PropertyInfo[] _paramPropertyInfo = sender.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (PropertyInfo item in _paramPropertyInfo)
                    {
                        SqlDataTypeAttribute _sqlDataTypeAttribute = (SqlDataTypeAttribute)item.GetCustomAttribute(typeof(SqlDataTypeAttribute));
                        ParameterAttribute _parameterAttribute = (ParameterAttribute)item.GetCustomAttribute(typeof(ParameterAttribute));
                        var _value = item.GetValue(sender);
                        DbParameter _dbParameter = (DbParameter)new TParameter();
                        _dbParameter.DbType = _sqlDataTypeAttribute.SqlDbType;
                        _dbParameter.ParameterName = _parameterAttribute.value;
                        _dbParameter.Value = _value == null ? DBNull.Value : _value;
                        _list.Add(_dbParameter);

                    }
                    return _list;



                });
                return _dbParameters;
            }
            catch (Exception ex)
            {
                throw new Exception(""); 
            }
           
        }
    }
}
