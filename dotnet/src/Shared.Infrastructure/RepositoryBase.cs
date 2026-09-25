using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Domain;
using Shared.Domain.Base;
using System.Linq.Expressions;

namespace Shared.Infrastructure
{
    public class RepositoryBase<TEntity, TContext> : IRepository<TEntity>
    where TEntity : EntityBase
    where TContext : DbContext
    {
        protected readonly DbFactoryBase<TContext> _dbFactory;
        protected DbSet<TEntity> _dbSet;
        private readonly IHttpContextAccessor _accessor;
        protected string CurrentUser => _accessor.HttpContext?.User?.Identity?.Name ?? "Default";
        protected DbSet<TEntity> DbSet
        {
            get => _dbSet ?? (_dbSet = _dbFactory.Context.Set<TEntity>());
        }

        public RepositoryBase(
            DbFactoryBase<TContext> dbFactory
            , IHttpContextAccessor accessor
            )
        {
            _dbFactory = dbFactory;
            _accessor = accessor;
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<TEntity?> GetEntNoTracking(Guid id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null)
        {
            var query = DbSet.AsQueryable().AsNoTracking();

            if (include != null)
                query = include(query);
            return await query.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<TEntity?> GetEntNoTracking(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null)
        {
            var query = DbSet.AsQueryable().AsNoTracking();

            if (include != null)
                query = include(query);
            return await query.Where(x => !x.IsDeleted).FirstOrDefaultAsync(expression);
        }

        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>>? expression)
        {
            if (expression == null)
                return DbSet.Where(x => !x.IsDeleted);
            return DbSet.Where(x => !x.IsDeleted).Where(expression);
        }

        public IQueryable<TEntity> GetQueryableWithDeleted(Expression<Func<TEntity, bool>>? expression)
        {
            if (expression == null)
                return DbSet.Where(x => x.IsDeleted);

            return DbSet.Where(x => x.IsDeleted).Where(expression);
        }
        public void Add(TEntity entity)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(TEntity)))
            {
                ((IAuditEntity)entity).UpdatedAt = ((IAuditEntity)entity).CreatedAt = DateTime.UtcNow;
                ((IAuditEntity)entity).CreatedBy = ((IAuditEntity)entity).UpdatedBy = CurrentUser;
            }
            DbSet.Add(entity);
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(TEntity)))
            {
                ((IAuditEntity)entity).UpdatedAt = ((IAuditEntity)entity).CreatedAt = DateTime.UtcNow;
                ((IAuditEntity)entity).CreatedBy = ((IAuditEntity)entity).UpdatedBy = CurrentUser;
            }
            await DbSet.AddAsync(entity);
            return entity;
        }
        public async Task AddAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (typeof(IAuditEntity).IsAssignableFrom(typeof(TEntity)))
                {
                    ((IAuditEntity)entity).UpdatedAt = ((IAuditEntity)entity).CreatedAt = DateTime.UtcNow;
                    ((IAuditEntity)entity).CreatedBy = ((IAuditEntity)entity).UpdatedBy = CurrentUser;
                }
            }
            await DbSet.AddRangeAsync(entities);
        }
        public void Update(TEntity entity)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(TEntity)))
            {
                ((IAuditEntity)entity).UpdatedAt = DateTime.UtcNow;
                ((IAuditEntity)entity).UpdatedBy = CurrentUser;
            }
            DbSet.Update(entity);
        }
        public void Update(IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
            {
                if (typeof(IAuditEntity).IsAssignableFrom(typeof(TEntity)))
                {
                    ((IAuditEntity)item).UpdatedAt = DateTime.UtcNow;
                    ((IAuditEntity)item).UpdatedBy = CurrentUser;
                }
            }
            DbSet.UpdateRange(entities);
        }
        public void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            DbSet.Update(entity);
        }
        public Task DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetListFromStored(string storedName, object parameters)
        {
            var data = DbSet.FromSqlRaw("EXECUTE " + storedName, parameters)
                            .AsNoTracking()
                            .ToList();
            return data;
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? expression = null)
        {
            if (expression == null)
                return await DbSet.Where(x => !x.IsDeleted).CountAsync();
            return await DbSet.Where(x => !x.IsDeleted).CountAsync(expression);
        }

        public async Task<List<TEntity>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            if (ids == null || !ids.Any())
            {
                return new List<TEntity>(); // ✅ danh sách rỗng an toàn
            }
            return await DbSet.Where(x => ids.Contains(x.Id))
                .ToListAsync();
        }
    }
}
