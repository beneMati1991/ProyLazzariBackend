using Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DbContext : IDbContext 
    {

        ApiDbContext _ctx;

        public DbContext(ApiDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> Count<T>() where T : class, IEntityBase
        {
            return await _ctx.Set<T>().CountAsync<T>();
        }

        public async Task<int> CountWhere<T>(Expression<Func<T, bool>> predicate) where T : class, IEntityBase
        {
            return await _ctx.Set<T>().Where(predicate).CountAsync<T>();
        }

        public async Task Delete<T> (int id) where T : class, IEntityBase
        {
            var item = await _ctx.Set<T>().Where(i => i.Id.Equals(id)).FirstOrDefaultAsync();
            item.Activo = false;
            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : class, IEntityBase
        {
            return await _ctx.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllPaginado<T>(IPaginationFilter filter) where T : class, IEntityBase
        {
            return await _ctx.Set<T>().Skip((filter.PageNumber - 1) * filter.PageSize)
                               .Take(filter.PageSize)
                               .ToListAsync();
        }

        public async Task<T> GetById<T>(int id) where T : class, IEntityBase
        {
            var items = _ctx.Set<T>();
            return await items.FirstOrDefaultAsync(i => i.Id.Equals(id));
        }

       
        public async Task Save<T>(T entity) where T : class, IEntityBase
        {
            await _ctx.Set<T>().AddAsync(entity);

            await _ctx.SaveChangesAsync();
        }

        public Task Update<T>(T entity) where T : class, IEntityBase
        {
            if(entity is not null)
            {
                _ctx.Entry(entity).State = EntityState.Modified;
            }

            return _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetWhere<T> (Expression<Func<T,bool>> predicate) where T : class, IEntityBase
        {
            return await _ctx.Set<T>().Where(predicate).ToListAsync();
        }

        public bool ExistsEntity<T>(Expression<Func<T, bool>> predicate) where T : class, IEntityBase => _ctx.Set<T>().Any(predicate);

        public async Task<bool> ExistsEntityAsync<T>(Expression<Func<T, bool>> predicate) where T : class, IEntityBase => await _ctx.Set<T>().AnyAsync(predicate);

        public T FindFirst<T>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) where T : class, IEntityBase
        {
            IQueryable<T> query = _ctx.Set<T>();
            foreach (var include in includes)
            {
                if (include.Body.NodeType == ExpressionType.MemberAccess)
                {
                    query = query.Include(include);
                }
            }

            return query.FirstOrDefault(predicate);
        }

        public Task Remove<T> (T entity) where T : class, IEntityBase
        {
            if (entity != null)
            {
                _ctx.Remove(entity);
            }

            return _ctx.SaveChangesAsync();
        }

        public IQueryable<T> GetQuery<T> (params Expression<Func<T, object>>[] includes) where T : class, IEntityBase
        {
            IQueryable<T> query = _ctx.Set<T>();
            foreach (var include in includes)
            {
                if (include.Body.NodeType == ExpressionType.MemberAccess)
                {
                    query = query.Include(include);
                }      
            }

            return query.AsQueryable();
        }

        public IQueryable<T> GetQueryPaginado<T> (Expression<Func<T, bool>> predicate, IPaginationFilter filter, params Expression<Func<T, object>>[] includes) where T : class, IEntityBase
        {
            IQueryable<T> query = _ctx.Set<T>();

            foreach (var include in includes)
            {
                if (include.Body.NodeType == ExpressionType.MemberAccess)
                {
                    query = query.Include(include);
                }
            }

            return query
                .Where(predicate)
                .Skip(filter.PageSize * (filter.PageNumber - 1))
                .Take(filter.PageSize)
                .AsQueryable();
        }


    }
}
