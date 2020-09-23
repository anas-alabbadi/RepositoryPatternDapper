using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using Application_DataRepository.DapperDataRepository.Attributes;
namespace Application_DataRepository.DapperDataRepository.Extensions
{
   public static class SqlDBTypeExtension
    {
        public static DbType getSqlDbType(this string Extension, object SqlDbTypeValue)
        {
            try
            {
                DbType? ReturnedValue = null;
                // Type type = SqlDbTypeValue.GetType();
                PropertyInfo fieledinfo =(PropertyInfo) SqlDbTypeValue;
                SqlDataTypeAttribute[] SqlDBTypeArr = fieledinfo.GetCustomAttributes(typeof(SqlDataTypeAttribute), false) as SqlDataTypeAttribute[];
                if (SqlDBTypeArr.Length > 0)
                {
                    ReturnedValue = SqlDBTypeArr[0].SqlDbType;
                }
                return ReturnedValue ?? 0;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
