using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Console.Models
{
    [Table("server_response_log", Schema = "dbo")]
    public class ServerResponseLog
    {
        [NotMapped]
        public Guid LogID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int HttpStatusCode { get; set; }
        public string ResponseText { get; set; }
        [NotMapped]
        public int ErrorCode { get; set; }
        [NotMapped]
        public DateTime InsertDateUTC { get; set; }
    }
}
