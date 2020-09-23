using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application_DataRepository.RepositoryInterface
{
    public interface IDataRepository 
    {
        int Add<TEntity>(TEntity entity, bool isRowEffected) where TEntity : class, new();
        Task<int> AddAsync<TEntity>(TEntity entity,bool isRowEffected) where TEntity : class, new();
        int Update<TEntity>(TEntity entity, object id) where TEntity : class, new();
        Task<int> UpdateAsync<TEntity>(TEntity entity, object id) where TEntity : class, new();
        IEnumerable<TEntity> FindAll<TEntity>() where TEntity : class, new();
        Task<IEnumerable<TEntity>> FindAllAsync<TEntity>() where TEntity : class, new();
        TEntity FindByID<TEntity>(Object ID) where TEntity : class, new();
        Task<TEntity> FindByIDAsync<TEntity>(Object id) where TEntity : class, new();
        IEnumerable<TEntity> FindByKey<TEntity>(Expression<Func<TEntity, bool>> keyFilter = null) where TEntity : class, new();
        Task<IEnumerable<TEntity>> FindByKeyAsync<TEntity>(Expression<Func<TEntity, bool>> keyFilter = null) where TEntity : class, new();
        IEnumerable<TEntity> ExecuteQuery<TEntity>(string Query, params object[] _params) where TEntity : class, new();
        Task<IEnumerable<TEntity>> ExecuteQueryAsync<TEntity>(string Query, params object[] _params) where TEntity : class, new();
        int Remove<TEntity>(TEntity entity, object id) where TEntity : class, new();
        Task<int> RemoveAsync<TEntity>(TEntity entity, object id) where TEntity : class, new();
        int RemoveByKeyFilter<TEntity>(Expression<Func<TEntity, bool>> keyFilter = null) where TEntity : class, new();
        Task<int> RemoveByKeyFilterAsync<TEntity>(Expression<Func<TEntity, bool>> keyFilter = null) where TEntity : class, new();


    }
}
