using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Application_DataRepository.DapperDataRepository.DapperSqlParamter
{
  public  interface IDapperSqlParameter
    {
        List<DbParameter> DbParametersListGenerator(object model,DbParameter dbParameter);
        Task<List<DbParameter>> DbParametersListGeneratorAsync(object model, DbParameter dbParameter);
    }
}
