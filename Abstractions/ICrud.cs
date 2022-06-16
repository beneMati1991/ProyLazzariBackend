using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions
{
    public interface ICrud
    {
        Task Save<T>(T entity) where T : class, IEntityBase; 
        Task<IEnumerable<T>> GetAll<T>() where T : class, IEntityBase;
        Task<IEnumerable<T>> GetAllPaginado<T>(IPaginationFilter filter) where T : class, IEntityBase;
        Task<T> GetById<T>(int id) where T : class, IEntityBase;
        Task Delete<T>(int id) where T : class, IEntityBase;
        Task Update<T>(T entity) where T : class, IEntityBase;
        Task<int> Count<T>() where T : class, IEntityBase;
        Task<int> CountWhere<T>(Expression<Func<T, bool>> predicate) where T : class, IEntityBase;
        Task Remove<T>(T entity) where T : class, IEntityBase;
        IQueryable<T> GetQuery<T>(params Expression<Func<T, object>>[] includes) where T : class, IEntityBase;
        IQueryable<T> GetQueryPaginado<T> (Expression<Func<T, bool>> predicate, IPaginationFilter filter, params Expression<Func<T, object>>[] includes) where T : class, IEntityBase;
        
        Task<IEnumerable<T>> GetWhere<T>(Expression<Func<T, bool>> predicate) where T : class, IEntityBase;

        bool ExistsEntity<T>(Expression<Func<T, bool>> predicate) where T : class, IEntityBase;
        Task<bool> ExistsEntityAsync<T>(Expression<Func<T, bool>> predicate) where T : class, IEntityBase;
        T FindFirst<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class, IEntityBase;

    }
}
