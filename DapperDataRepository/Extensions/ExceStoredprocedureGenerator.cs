using Application_DataRepository.DapperDataRepository.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application_DataRepository.DapperDataRepository.Extensions
{
    public static partial class ExceStoredprocedureGenerator
    {
        public static async Task<string> GeneratExecProcedureBody<T>(this string procedureBody)where T:class,new()
        {

            try
            {
                string procedureExecBody = await Task.Run(() => {
                    StringBuilder execProcedureStringBuilder = new StringBuilder();
                    execProcedureStringBuilder.Append("exec ");
                    Type mainInstanceType = new T().GetType();
                    ProcedureAttribute[] procedureAttribute = mainInstanceType.GetCustomAttributes(typeof(ProcedureAttribute), false) as ProcedureAttribute[];
                    // get the procedure name by calling ProcedureAttribute attached to class
                    if (procedureAttribute.Length == 0 || procedureAttribute.Length >1)
                    {
                        throw new Exception("ProcedureAttribute must be assigned to the attached type or it's assigned more then one time");
                    }
                    execProcedureStringBuilder.Append($"{procedureAttribute[0].name} ");
                    PropertyInfo[] mainInstanceProperties = mainInstanceType.GetProperties(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
                    foreach (PropertyInfo property in mainInstanceProperties)
                    {
                        ParameterAttribute[] parameterAttributes =property.GetCustomAttributes(typeof(ParameterAttribute), false)as ParameterAttribute[];
                        if (parameterAttributes.Length > 0)
                        {
                            foreach (ParameterAttribute param in parameterAttributes)
                            {
                                execProcedureStringBuilder.Append($"{param.value} ,");
                            }
                           
                        }

                    }
                    string procedureBodyText = execProcedureStringBuilder.ToString();
                    procedureBodyText= procedureBodyText.TrimEnd(',');

                    return procedureBodyText;
                });
                return procedureExecBody;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message,ex);
            }

        }
    }
}
