using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application_DataRepository.RepositoryInterface;
using Aregak_Framework_SystemLogger.SystemLogger.NLogLogger;
using Aregak_Framework_SystemLogger.SystemLogger;
using System.Reflection;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Reflection.Emit;
using System.Data.Common;

namespace Application_DataRepository.DapperDataRepository
{
    public partial class DataRepository : IDisposable, IDataRepository
    {
        #region var
        IDbConnection _connection;
        IDbTransaction _transaction;
        bool _isTransable = false;
        bool _isTransCommited = false;
        bool _isTransRolledBack = false;
        const string _classFullName = "Aregak_Framework_DataRepository.DapperDataRepository.DataRepository<TEntity>";
        #endregion
        #region prop

        #endregion
        #region ctor
        public DataRepository(IDbConnection dbConnection)
        {
            _connection = dbConnection;
        }
        public DataRepository(IDbConnection dbConnection, IDbTransaction dbTransaction) : this(dbConnection)
        {
            _transaction = dbTransaction;
        }
        public DataRepository(IDbConnection dbConnection, IDbTransaction dbTransaction,bool isTransable):this(dbConnection, dbTransaction)
        {
            _isTransable = isTransable;
        }
        #endregion
        #region func_public
        public void BeginTransaction() => _transaction = _connection.BeginTransaction();


        public void Commit() => _transaction.Commit();

        public void RollBack() => _transaction.Rollback();

        public void ConnectionOpen()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }
        public void CloseConnection()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
        public int Add<TEntity>(TEntity entity, bool isRowEffected=false) where TEntity : class, new()
        {
            int _returnedEffectedRow = 0;
            var _dbconnection = _connection;

            //if (_dbconnection.State == ConnectionState.Closed)
            //{
            //    _dbconnection.Open();
            //}
            try
            {

                _returnedEffectedRow = (int)_connection.Insert<TEntity>(entity, transaction: _transaction);


            }
            catch (Exception ex)
            {

                NlogLoggerClient _client = new NlogLoggerClient(new NLogFileLogger(_classFullName));
                _client.ActionLoggerAsync(LogginLevels.LoggingLevels.Error, MethodBase.GetCurrentMethod().Name, ex.Message, ex).Wait();

            }
            finally
            {
                if (_dbconnection.State == ConnectionState.Open)
                {
                    _dbconnection.Close();
                }
            }


            return _returnedEffectedRow;
        }

        public async Task<int> AddAsync<TEntity>(TEntity entity, bool isRowEffected=false) where TEntity : class, new()
        {
            int _returnedEffectedRow = 0;
            var _dbconnection = _connection;

            //if (_dbconnection.State == ConnectionState.Closed)
            //{
            //    _dbconnection.Open();
            //}

            try
            {

                _returnedEffectedRow = await _connection.InsertAsync<TEntity>(entity, transaction: _transaction,isRowEffected: isRowEffected);

            }
            catch (Exception ex)
            {

                NlogLoggerClient _client = new NlogLoggerClient(new NLogFileLogger(_classFullName));
             await   _client.ActionLoggerAsync(LogginLevels.LoggingLevels.Error, MethodBase.GetCurrentMethod().Name, ex.Message, ex);
            }
            finally
            {

                if (_dbconnection.State == ConnectionState.Open)
                {
                    //   _dbconnection.Close();
                }
            }



            return _returnedEffectedRow;
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string Query, params object[] _params) where TEntity : class, new()
        {
            IEnumerable<TEntity> _entity = null;
            var _dbconnection = _connection;

            //if (_dbconnection.State == ConnectionState.Closed)
            //{
            //    _dbconnection.Open();
            //}

            try
            {

                if (_params.Length == 0)
                {
                    _entity = _connection.Query<TEntity>(Query, null, transaction: _transaction);
                }
                else
                {
                    var _dynamicpara = new DynamicParameters();
                    DbParameter[] sqlParameters = (DbParameter[])_params;
                    foreach (var item in sqlParameters)
                    {
                        _dynamicpara.Add(item.ParameterName, item.Value, item.DbType);

                    }

                    _entity = _connection.Query<TEntity>(Query, _params, transaction: _transaction);
                }


            }
            catch (Exception ex)
            {
                NlogLoggerClient _client = new NlogLoggerClient(new NLogFileLogger(_classFullName));
                _client.ActionLoggerAsync(LogginLevels.LoggingLevels.Error, MethodBase.GetCurrentMethod().Name, ex.Message, ex).Wait();
            }
            //finally
            //{
            //    if (_dbconnection.State == ConnectionState.Open)
            //    {
            //        _dbconnection.Close();
            //    }
            //}


            return _entity;
        }

        public async Task<IEnumerable<TEntity>> ExecuteQueryAsync<TEntity>(string Query, params object[] _params) where TEntity : class, new()
        {
            IEnumerable<TEntity> _entity = null;
            var _dbconnection = _connection;

            //if (_dbconnection.State == ConnectionState.Closed)
            //{
            //    _dbconnection.Open();
            //}

            try
            {

                if (_params.Length == 0)
                {
                    _entity = await _connection.QueryAsync<TEntity>(Query, null, transaction: _transaction);
                }
                else
                {
                    var _dynamicpara = new DynamicParameters();
                    DbParameter[] sqlParameters = (DbParameter[])_params;
                    foreach (var item in sqlParameters)
                    {
                        _dynamicpara.Add(item.ParameterName, item.Value, item.DbType);

                    }

                    _entity = await _connection.QueryAsync<TEntity>(Query, _dynamicpara, transaction: _transaction);
                }


            }
            catch (Exception ex)
            {

                NlogLoggerClient _client = new NlogLoggerClient(new NLogFileLogger(_classFullName));
             await   _client.ActionLoggerAsync(LogginLevels.LoggingLevels.Error, MethodBase.GetCurrentMethod().Name, ex.Message, ex);
            }
            //finally
            //{
            //    if (_dbconnection.State == ConnectionState.Open)
            //    {
            //        _dbconnection.Close();
            //    }
            //}




            return _entity;
        }

        public IEnumerable<TEntity> FindAll<TEntity>() where TEntity : class, new()
        {
            IEnumerable<TEntity> _entity = null;
            var _dbconnection = _connection;

            //if (_dbconnection.State == ConnectionState.Closed)
            //{
            //    _dbconnection.Open();
            //}

            try
            {

                _entity = _connection.GetAll<TEntity>(transaction: _transaction);

            }
            catch (Exception ex)
            {

                NlogLoggerClient _client = new NlogLoggerClient(new NLogFileLogger(_classFullName));
                _client.ActionLoggerAsync(LogginLevels.LoggingLevels.Error, MethodBase.GetCurrentMethod().Name, ex.Message, ex).Wait();
            }
            //finally
            //{
            //    if (_dbconnection.State == ConnectionState.Open)
            //    {
            //        _dbconnection.Close();
            //    }
            //}



            return _entity;
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync<TEntity>() where TEntity : class, new()
        {
            IEnumerable<TEntity> _entity = null;
            var _dbconnection = _connection;

            //if (_dbconnection.State == ConnectionState.Closed)
            //{
            //    _dbconnection.Open();
            //}

            try
            {

                _entity = await _connection.GetAllAsync<TEntity>(transaction: _transaction);

            }
            catch (Exception ex)
            {

                NlogLoggerClient _client = new NlogLoggerClient(new NLogFileLogger(_classFullName));
              await  _client.ActionLoggerAsync(LogginLevels.LoggingLevels.Error, MethodBase.GetCurrentMethod().Name, ex.Message, ex);
            }
            //finally
            //{
            //    if (_dbconnection.State == ConnectionState.Open)
            //    {
            //        _dbconnection.Close();
            //    }
            //}



            return _entity;
        }

        public TEntity FindByID<TEntity>(object ID) where TEntity : class, new()
        {
            TEntity _entity = null;
            var _dbconnection = _connection;

            //if (_dbconnection.State == ConnectionState.Closed)
            //{
            //    _dbconnection.Open();
            //}

            try
            {

                _entity = _connection.Get<TEntity>(ID, transaction: _transaction);

            }
            catch (Exception ex)
            {

                NlogLoggerClient _client = new NlogLoggerClient(new NLogFileLogger(_classFullName));
                _client.ActionLoggerAsync(LogginLevels.LoggingLevels.Error, MethodBase.GetCurrentMethod().Name, ex.Message, ex).Wait();
            }
            //finally
            //{
            //    if (_dbconnection.State == ConnectionState.Open)
            //    {
            //        _dbconnection.Close();
            //    }
            //}



            return _entity;
        }

        public async Task<TEntity> FindByIDAsync<TEntity>(object id) where TEntity : class, new()
        {
            TEntity _entity = null;
            var _dbconnection = _connection;

            //if (_dbconnection.State == ConnectionState.Closed)
            //{
            //    _dbconnection.Open();
            //}

            try
            {

                _entity = await _connection.GetAsync<TEntity>(id, transaction: _transaction);

            }
            catch (Exception ex)
            {

                NlogLoggerClient _client = new NlogLoggerClient(new NLogFileLogger(_classFullName));
              await  _client.ActionLoggerAsync(LogginLevels.LoggingLevels.Error, MethodBase.GetCurrentMethod().Name, ex.Message, ex);
            }
            //finally
            //{
            //    if (_dbconnection.State == ConnectionState.Open)
            //    {
            //        _dbconnection.Close();
            //    }
            //}



            return _entity;
        }

        public IEnumerable<TEntity> FindByKey<TEntity>(Expression<Func<TEntity, bool>> keyFilter = null) where TEntity : class, new()
        {
            IEnumerable<TEntity> _entity = null;
            var _dbconnection = _connection;

            //if (_dbconnection.State == ConnectionState.Closed)
            //{
            //    _dbconnection.Open();
            //}

            try
            {

                _entity = _connection.Query<TEntity>(new ConvertTEntityToRawSql<TEntity>().BuildRawSQLScript(SQLQuereyOptions._SQLQuereyOptions.GetByKeyFilter, keyFilter), null, transaction: _transaction);

            }
            catch (Exception ex)
            {

                NlogLoggerClient _client = new NlogLoggerClient(new NLogFileLogger(_classFullName));
                _client.ActionLoggerAsync(LogginLevels.LoggingLevels.Error, MethodBase.GetCurrentMethod().Name, ex.Message, ex).Wait();
            }
            //finally
            //{
            //    if (_dbconnection.State == ConnectionState.Open)
            //    {
            //        _dbconnection.Close();
            //    }
            //}


            return _entity;
        }

        public async Task<IEnumerable<TEntity>> FindByKeyAsync<TEntity>(Expression<Func<TEntity, bool>> keyFilter = null) where TEntity : class, new()
        {
            IEnumerable<TEntity> _entity = null;
            var _dbconnection = _connection;

            //if (_dbconnection.State == ConnectionState.Closed)
            //{
            //    _dbconnection.Open();
            //}

            try
            {

                _entity = await _connection.QueryAsync<TEntity>(new ConvertTEntityToRawSql<TEntity>().BuildRawSQLScript(SQLQuereyOptions._SQLQuereyOptions.GetByKeyFilter, keyFilter), null, transaction: _transaction);

            }
            catch (Exception ex)
            {

                NlogLoggerClient _client = new NlogLoggerClient(new NLogFileLogger(_classFullName));
              await  _client.ActionLoggerAsync(LogginLevels.LoggingLevels.Error, MethodBase.GetCurrentMethod().Name, ex.Message, ex);
            }
            //finally
            //{
            //    if (_dbconnection.State == ConnectionState.Open)
            //    {
            //        _dbconnection.Close();
            //    }
            //}



            return _entity;
        }

        public int Remove<TEntity>(TEntity entity, object id) where TEntity : class, new()
        {
            bool _returnedEffectedRow = false;
            var _dbconnection = _connection;

            //if (_dbconnection.State == ConnectionState.Closed)
            //{
            //    _dbconnection.Open();
            //}

            try
            {

                _returnedEffectedRow = _connection.Delete<TEntity>(entity, transaction: _transaction);

            }
            catch (Exception ex)
            {

                NlogLoggerClient _client = new NlogLoggerClient(new NLogFileLogger(_classFullName));
                _client.ActionLoggerAsync(LogginLevels.LoggingLevels.Error, MethodBase.GetCurrentMethod().Name, ex.Message, ex).Wait();
            }
            //finally
            //{
            //    if (_dbconnection.State == ConnectionState.Open)
            //    {
            //        _dbconnection.Close();
            //    }
            //}



            return _returnedEffectedRow ? 1 : 0;
        }

        public async Task<int> RemoveAsync<TEntity>(TEntity entity, object id) where TEntity : class, new()
        {
            bool _returnedEffectedRow = false;
            var _dbconnection = _connection;

            //if (_dbconnection.State == ConnectionState.Closed)
            //{
            //    _dbconnection.Open();
            //}

            try
            {

                _returnedEffectedRow = await _connection.DeleteAsync<TEntity>(entity, transaction: _transaction);

            }
            catch (Exception ex)
            {

                NlogLoggerClient _client = new NlogLoggerClient(new NLogFileLogger(_classFullName));
               await _client.ActionLoggerAsync(LogginLevels.LoggingLevels.Error, MethodBase.GetCurrentMethod().Name, ex.Message, ex);
            }
            //finally
            //{
            //    if (_dbconnection.State == ConnectionState.Open)
            //    {
            //        _dbconnection.Close();
            //    }
            //}



            return _returnedEffectedRow ? 1 : 0;
        }
        public int RemoveByKeyFilter<TEntity>(Expression<Func<TEntity, bool>> keyFilter = null) where TEntity : class, new()
        {
            int _entity = 0;
            var _dbconnection = _connection;

            //if (_dbconnection.State == ConnectionState.Closed)
            //{
            //    _dbconnection.Open();
            //}

            try
            {

                _entity = _connection.ExecuteScalar<int>(new ConvertTEntityToRawSql<TEntity>().BuildRawSQLScript(SQLQuereyOptions._SQLQuereyOptions.Delete, keyFilter), null, transaction: _transaction);

            }
            catch (Exception ex)
            {

                NlogLoggerClient _client = new NlogLoggerClient(new NLogFileLogger(_classFullName));
                _client.ActionLoggerAsync(LogginLevels.LoggingLevels.Error, MethodBase.GetCurrentMethod().Name, ex.Message, ex).Wait();
            }
            //finally
            //{
            //    if (_dbconnection.State == ConnectionState.Open)
            //    {
            //        _dbconnection.Close();
            //    }
            //}


            return _entity;
        }

        public async Task<int> RemoveByKeyFilterAsync<TEntity>(Expression<Func<TEntity, bool>> keyFilter = null) where TEntity : class, new()
        {
            int _entity = 0;
            var _dbconnection = _connection;

            //if (_dbconnection.State == ConnectionState.Closed)
            //{
            //    _dbconnection.Open();
            //}

            try
            {

                _entity = await _connection.ExecuteScalarAsync<int>(new ConvertTEntityToRawSql<TEntity>().BuildRawSQLScript(SQLQuereyOptions._SQLQuereyOptions.Delete, keyFilter), null, transaction: _transaction);

            }
            catch (Exception ex)
            {

                NlogLoggerClient _client = new NlogLoggerClient(new NLogFileLogger(_classFullName));
                await _client.ActionLoggerAsync(LogginLevels.LoggingLevels.Error, MethodBase.GetCurrentMethod().Name, ex.Message, ex);
            }
            //finally
            //{
            //    if (_dbconnection.State == ConnectionState.Open)
            //    {
            //        _dbconnection.Close();
            //    }
            //}



            return _entity;
        }
        public int Update<TEntity>(TEntity entity, object id) where TEntity : class, new()
        {
            bool _returnedEffectedRow = false;
            var _dbconnection = _connection;

            //if (_dbconnection.State == ConnectionState.Closed)
            //{
            //    _dbconnection.Open();
            //}

            try
            {

                _returnedEffectedRow = _connection.Update<TEntity>(entity, transaction: _transaction);

            }
            catch (Exception ex)
            {

                NlogLoggerClient _client = new NlogLoggerClient(new NLogFileLogger(_classFullName));
                _client.ActionLoggerAsync(LogginLevels.LoggingLevels.Error, MethodBase.GetCurrentMethod().Name, ex.Message, ex).Wait();
            }
            //finally
            //{
            //    if (_dbconnection.State == ConnectionState.Open)
            //    {
            //        _dbconnection.Close();
            //    }
            //}



            return _returnedEffectedRow ? 1 : 0;
        }

        public async Task<int> UpdateAsync<TEntity>(TEntity entity, object id) where TEntity : class, new()
        {
            bool _returnedEffectedRow = false;
            var _dbconnection = _connection;

            //if (_dbconnection.State == ConnectionState.Closed)
            //{
            //    _dbconnection.Open();
            //}

            try
            {

                _returnedEffectedRow = await _connection.UpdateAsync<TEntity>(entity, transaction: _transaction);


            }
            catch (Exception ex)
            {

                NlogLoggerClient _client = new NlogLoggerClient(new NLogFileLogger(_classFullName));
              await  _client.ActionLoggerAsync(LogginLevels.LoggingLevels.Error, MethodBase.GetCurrentMethod().Name, ex.Message, ex);
            }
            //finally
            //{
            //    if (_dbconnection.State == ConnectionState.Open)
            //    {
            //        _dbconnection.Close();
            //    }
            //}



            return _returnedEffectedRow ? 1 : 0;
        }

        public void Dispose()
        {
            if (_isTransable)
            {
                if (_isTransCommited || _isTransRolledBack)
                {
                    Dispose(true);
                }
                else
                {
                    Dispose(false);
                }
            }
            else
            {
                Dispose(true);
            }
            
        }
        private void Dispose(bool isDisposeabl)
        {
            if (isDisposeabl)
            {
                _connection?.Dispose();
                _transaction?.Dispose();
                GC.SuppressFinalize(this);
            }
        }
        #endregion

    }
}
