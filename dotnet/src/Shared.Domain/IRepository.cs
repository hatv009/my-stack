using Shared.Domain.Base;
using System.Linq.Expressions;

namespace Shared.Domain
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? expression = null);
        void Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        Task AddAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<List<TEntity>> GetByIdsAsync(IEnumerable<Guid> ids);
        Task<TEntity?> GetEntNoTracking(Guid id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null);
        Task<TEntity?> GetEntNoTracking(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null);
        IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>>? expression = null);
        IQueryable<TEntity> GetQueryableWithDeleted(Expression<Func<TEntity, bool>>? expression = null);
        //Task<IList<TEntity>> ListPaging(SearchOptions options);
        IEnumerable<TEntity> GetListFromStored(string storedName, object parameters);
    }
}
