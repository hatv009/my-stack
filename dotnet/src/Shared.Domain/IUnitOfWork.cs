using Microsoft.EntityFrameworkCore;

namespace Shared.Domain
{
    public interface IUnitOfWork<TContext> where TContext : DbContext
    {
        Task CommitAsync(CancellationToken cancellationToken = default);
        TContext Context { get; }
    }
}
