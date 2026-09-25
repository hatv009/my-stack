namespace Shared.Domain.Base
{
    public interface IEntityBase
    {
        Guid Id { get; set; }
        bool IsDeleted { get; set; }
        void Delete();
    }

    public interface IAuditBase
    {
        DateTimeOffset CreatedAt { get; set; }
        string CreatedBy { get; set; }
        DateTimeOffset? UpdatedAt { get; set; }
        string? UpdatedBy { get; set; }
    }
    public interface IAuditEntity : IAuditBase, IEntityBase
    {
    }

    public interface IPublicEntity
    {
        bool ShowWebsite { get; }
        void ToggleShowWebsite();
    }
}
