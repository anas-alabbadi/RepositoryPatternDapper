using Aregak_Framework_SystemLogger.SystemLogger;
using Aregak_Framework_SystemLogger.SystemLogger.NLogLogger;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Application_DataRepository.DapperDataRepository.Extensions;
using System.Linq.Expressions;

namespace Application_DataRepository.DapperDataRepository
{
    internal partial class ConvertTEntityToRawSql<TEntity> where TEntity : class, new()
    {
        #region var
        StringBuilder _queryBuilder;
        #endregion
        #region prop

        #endregion
        #region ctor
        public ConvertTEntityToRawSql()
        {
            _queryBuilder = new StringBuilder();
        }
      
        #endregion
        #region func_public
        public string BuildRawSQLScript( SQLQuereyOptions._SQLQuereyOptions quereyOptions, Expression<Func<TEntity, bool>> keyFilter = null)
        {

            string _executebleQuerey = string.Empty;
            try
            {
                
                switch (quereyOptions)
                {
                    case SQLQuereyOptions._SQLQuereyOptions.Insert:
                        break;
                    case SQLQuereyOptions._SQLQuereyOptions.Update:
                        break;
                    case SQLQuereyOptions._SQLQuereyOptions.Delete:
                        _executebleQuerey = DeleteWithKeyFilter(keyFilter);
                        break;
                    case SQLQuereyOptions._SQLQuereyOptions.GetAll:
                        break;
                    case SQLQuereyOptions._SQLQuereyOptions.GetByID:
                        break;
                    case SQLQuereyOptions._SQLQuereyOptions.GetByKeyFilter:
                        _executebleQuerey= SelectWithKeyFilter(keyFilter);
                        break;
                    default:
                        break;
                }
               
            }
            catch (Exception ex)
            {
                NlogLoggerClient _client = new NlogLoggerClient(new NLogFileLogger("Application_DataRepository.DapperDataRepository.DataRepository<TEntity>"));
                _client.ActionLoggerAsync(LogginLevels.LoggingLevels.Error, MethodBase.GetCurrentMethod().Name, ex.Message, ex).Wait();
            }
            return _executebleQuerey;
        }
        #endregion
        #region func_private
        private string SelectWithKeyFilter(Expression<Func<TEntity, bool>> keyFilter)
        {
            string _queryResult = string.Empty;
            try
            {
                Type _type = typeof(TEntity);
                if (keyFilter != null)
                {
                    _queryResult= _queryBuilder.Append("Select * From ").Append($"{_type.Name} where {keyFilter.ExpressionTreeToString()}").ToString();
                   
                       
                }
                else
                {
                    throw new Exception("Expression Factor Can't Be Null Or Empty");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message,ex);
            }
            return _queryResult;
        }

        private string DeleteWithKeyFilter(Expression<Func<TEntity, bool>> keyFilter)
        {
            string _queryResult = string.Empty;
            try
            {
                Type _type = typeof(TEntity);

                _queryResult = _queryBuilder.Append("DELETE From ").Append($"{_type.Name} where {keyFilter.ExpressionTreeToString()}").Append("Select @@ROWCOUNT").ToString();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message,ex);
            }
            return _queryResult;
        }
        #endregion
    }
}
