using Shared.Domain.Common;

namespace Shared.Domain.DTOs
{
    public class CountItemDto
    {
        public int Id { get; set; }
        public int Count { get; set; }
    }

    public record CountItemDto<T>(T Id, int Count) where T : Enum
    {
        public string Name => Id.GetDisplayName();
    };
}
