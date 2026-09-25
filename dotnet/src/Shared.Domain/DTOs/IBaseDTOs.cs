namespace Shared.Domain.DTOs
{
    public interface IBaseDTOs
    {
        int Id { get; set; }
        DateTime CreatedDate { get; set; }
        string CreatedBy { get; set; }
    }
}
