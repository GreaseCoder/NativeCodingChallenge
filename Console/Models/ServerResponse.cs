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
    }
}
