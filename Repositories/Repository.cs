using Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class Repository : IRepository
    {
        private readonly IDbContext _ctx;

        public Repository(IDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> Count<T>() where T : class, IEntityBase
        {
            return await _ctx.Count<T>();
        }

        public bool ExistsEntity<T>(Expression<Func<T, bool>> predicate) where T : class, IEntityBase
        {
            return _ctx.ExistsEntity<T>(predicate);
        }

        public async Task<bool> ExistsEntityAsync<T>(Expression<Func<T, bool>> predicate) where T : class, IEntityBase
        {
            return await _ctx.ExistsEntityAsync<T>(predicate);
        }

        public T FindFirst<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class, IEntityBase
        {
            return _ctx.FindFirst<T>(predicate, includes);
        }

        public async Task<int> CountWhere<T>(Expression<Func<T, bool>> predicate) where T : class, IEntityBase
        {
            return await _ctx.CountWhere<T>(predicate);
        }

        public async Task Delete<T>(int id) where T : class, IEntityBase
        {
            await _ctx.Delete<T>(id);
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : class, IEntityBase
        {
            return await _ctx.GetAll<T>();
        }


        public async Task<IEnumerable<T>> GetAllPaginado<T>(IPaginationFilter filter) where T : class, IEntityBase
        {
            return await _ctx.GetAllPaginado<T>(filter);
        }

        public Task<T> GetById<T>(int id) where T : class, IEntityBase
        {
            return _ctx.GetById<T>(id);
        }

        public Task Save<T>(T entity) where T : class, IEntityBase
        {
            return _ctx.Save(entity);
        }

        public Task Update<T>(T entity) where T : class, IEntityBase
        {
            return _ctx.Update(entity);
        }

        public Task Remove<T>(T entity) where T : class, IEntityBase
        {
            return _ctx.Remove(entity);
        }

        public IQueryable<T> GetQuery<T>(params Expression<Func<T, object>>[] includes) where T : class, IEntityBase
        {
            return _ctx.GetQuery(includes);
        }

        public IQueryable<T> GetQueryPaginado<T>(Expression<Func<T, bool>> predicate, IPaginationFilter filter, params Expression<Func<T, object>>[] includes) where T : class, IEntityBase
        {
            return _ctx.GetQueryPaginado(predicate, filter, includes);
        }

        public async Task<IEnumerable<T>> GetWhere<T>(Expression<Func<T, bool>> predicate) where T : class, IEntityBase
        {
            return await _ctx.GetWhere(predicate);
        }

    }
}
