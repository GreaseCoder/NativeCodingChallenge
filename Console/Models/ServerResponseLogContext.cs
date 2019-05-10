using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Console.Models
{
    public partial class ServerResponseLogContext : DbContext
    {
        public DbQuery<ServerResponseErrorCodeCounts> ErrorCodeCounts { get; set; }
        public DbSet<ServerResponseLog> ServerResponseLogs { get; set; }

        public ServerResponseLogContext()
        {
        }

        public ServerResponseLogContext(DbContextOptions<ServerResponseLogContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LocalHost;Database=ae_code_challange;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Query<ServerResponseErrorCodeCounts>().ToView("ServerResponseLog_GetHourlyErrorCodeCounts");

            modelBuilder.Entity<ServerResponseLog>(entity =>
            {
                entity.ToTable("server_response_log", schema: "dbo");

                entity.HasKey(e => e.LogID)
                    .HasName("pk__server_response_log__LogID")
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.StartTime)
                    .HasName("nix__server_response_log__StartTime_ResponseText");
            });
        }

        public IEnumerable<ServerResponseLog> GetLatestLogs(DateTime startDate, DateTime endDate)
        {
            var start = new SqlParameter("@StartTime", startDate);
            var end = new SqlParameter("@EndTime", endDate);

            return ServerResponseLogs
                    .AsNoTracking()
                    .FromSql("EXEC dbo.ServerResponseLog_GetLatestResponses @StartTime, @EndTime", start, end);
        }

    }
}
