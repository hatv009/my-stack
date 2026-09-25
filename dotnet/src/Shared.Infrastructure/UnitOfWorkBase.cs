using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Domain;
using Shared.Domain.Base;

namespace Shared.Infrastructure
{
    public class UnitOfWorkBase<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        protected readonly DbFactoryBase<TContext> _dbFactory;
        private readonly IMediator _mediator;

        public UnitOfWorkBase(
            DbFactoryBase<TContext> dbFactory
            , IMediator mediator)
        {
            _dbFactory = dbFactory;
            _mediator = mediator;
        }

        public TContext Context { get; }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            // 1. Lấy event trước khi Save
            var aggregates = _dbFactory.Context.ChangeTracker
                .Entries<IDomainEventEntity>()
                .Where(e => e.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = aggregates
                .SelectMany(e => e.Entity.DomainEvents)
                .ToList();

            aggregates.ForEach(e => e.Entity.ClearDomainEvents());

            // 2. SaveChanges (commit xong mới giải phóng transaction)
            await _dbFactory.Context.SaveChangesAsync(cancellationToken);

            // 3. Lúc này publish -> an toàn hơn
            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
        }
    }
}
