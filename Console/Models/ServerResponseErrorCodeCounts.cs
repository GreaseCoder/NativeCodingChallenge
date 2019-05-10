using System;
using System.Collections.Generic;
using System.Text;

namespace Console.Models
{
    public class ServerResponseErrorCodeCounts
    {
        public DateTime Time { get; private set; }
        public int ErrorCode { get; private set; }
        public int Count { get; private set; }
    }
}
