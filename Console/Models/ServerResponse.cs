using System;
using System.Collections.Generic;
using System.Text;

namespace Console.Models
{
    public class ServerResponse
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int HttpStatusCode { get; set; }
        public string Response { get; set; }
        public ErrorCodes ErrorCode { get; set; }
    }

    public enum ErrorCodes
    {
        Unknown = 0,
        Success = 1,
        IAmATeaPot = 418,
        Timeout = -999
    }
}
