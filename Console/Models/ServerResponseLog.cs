using System;
using System.Collections.Generic;
using System.Text;

namespace Console.Models
{
    public class ServerResponseLog
    {
        public Guid LogID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int HttpStatusCode { get; set; }
        public string ResponseText { get; set; }
        public int ErrorCode { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
