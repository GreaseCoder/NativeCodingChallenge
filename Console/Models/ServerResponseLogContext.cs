using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Console.Models
{
    public partial class ServerResponseLogContext : DbContext
    {
        public virtual DbSet<ServerResponseLog> ServerResponseLog { get; set; }

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
            modelBuilder.Entity<ServerResponseLog>(entity =>
            {
                entity.HasKey(e => e.LogID)
                    .HasName("pk__server_response_log__LogID")
                    .ForSqlServerIsClustered(false);

                entity.ToTable("server_response_log");

                entity.HasIndex(e => e.StartTime)
                    .HasName("nix__server_response_log__StartTime_ResponseText");

                entity.Property(e => e.LogID)
                    .HasColumnName("LogID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime2");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime2");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("(GetUTCDate())");
            });

        }
    }
}
