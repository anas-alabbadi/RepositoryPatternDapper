using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Application_DataRepository.DapperDataRepository.Attributes;
namespace Application_DataRepository.DapperDataRepository.Extensions
{
    public static class ParameterExtension
    {
        public static string getParameterValue(this string Param, Enum ParameterValue)
        {
            try
            {
                string ReturnedValue = string.Empty;
                Type InfoType = ParameterValue.GetType();
                FieldInfo fieldInfo = InfoType.GetField(ParameterValue.ToString());
                ParameterAttribute[] parmArray = fieldInfo.GetCustomAttributes(typeof(ParameterAttribute), false) as ParameterAttribute[];
                if (parmArray.Length > 0)
                {
                    ReturnedValue = parmArray[0].value;
                }
                return ReturnedValue;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
