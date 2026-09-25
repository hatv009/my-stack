namespace Shared.Domain.Interfaces
{
    public interface IDomainEvent
    {
        Guid EventId { get; }
        DateTimeOffset DateOccurred { get; }
    }
}
