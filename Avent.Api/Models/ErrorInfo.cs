using System;

namespace Avent.Api.Models
{
    public class ErrorInfo
    {
        public int StatusCode { get; set; }
        public Guid TraceId { get; set; }
        public string Message { get; set; }
    }
}
