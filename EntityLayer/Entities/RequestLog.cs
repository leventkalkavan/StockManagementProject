using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities
{
    public class RequestLog 
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public string? RequestPath { get; set; }

        public string? HttpMethod { get; set; }

        public string? QueryString { get; set; }

        public string? RequestBody { get; set; }

        public int StatusCode { get; set; }

        [MaxLength(32)]
        public string? LogLevel { get; set; }

        public string? ErrorMessage { get; set; }
        public string? StackTrace { get; set; }

        public string? UserName { get; set; }

        public string? ClientIp { get; set; }

        public string? UserAgent { get; set; }

        public string? CorrelationId { get; set; }

        public long? DurationMs { get; set; }
    }
}
