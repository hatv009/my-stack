using System.Text.Json.Serialization;

namespace Shared.Domain.DTOs
{
    public class LogItem
    {
        [JsonPropertyName("@t")]
        public DateTime Time { get; set; } //utc time
        [JsonPropertyName("@mt")]
        public string? Format { get; set; } //format
        [JsonPropertyName("@x")]
        public string? Exception { get; set; } //exception
        public string? Scheme { get; set; }
        public string? Query { get; set; }
        public string? RequestMethod { get; set; }
        public string? RequestPath { get; set; }
        public string? StatusCode { get; set; }
        public string? Elapsed { get; set; }
        public string? Body { get; set; }

        public string? ConnectionId { get; set; }
        public string? RequestId { get; set; }

        public string? User { get; set; }
    }
}
