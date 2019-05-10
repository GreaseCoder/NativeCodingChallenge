using Console.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Console.UnitTests
{
    [TestFixture]
    class LoggingTests
    {
        [Test]
        public async Task AddRecordsToTable()
        {
            var options = new DbContextOptionsBuilder<ServerResponseLogContext>()     
                .UseSqlServer("Server=LocalHost;Database=ae_code_challange;Trusted_Connection=True;")
                .Options;

            using (var context = new ServerResponseLogContext(options))
            {
                cleanup(context);

                var now = DateTime.Now;
                var service = new ServerResponseLogService(context);

                await service.AddLogEntry(new ServerResponse()
                {
                    StartTime = now.AddDays(-2),
                    EndTime = now.AddDays(-2).AddMinutes(30),
                    HttpStatusCode = 200,
                    Response = "Unit Test Case 001"
                });

                await service.AddLogEntry(new ServerResponse()
                {
                    StartTime = now.AddDays(-2),
                    EndTime = now.AddDays(-2).AddMinutes(30),
                    HttpStatusCode = 500,
                    Response = "Unit Test Case 002"
                });

                await service.AddLogEntry(new ServerResponse()
                {
                    StartTime = now.AddDays(-2),
                    EndTime = now.AddDays(-2).AddMinutes(30),
                    HttpStatusCode = 408,
                    Response = "Unit Test Case 003"
                });

                context.SaveChanges();
            }

            using (var context = new ServerResponseLogContext(options))
            {
                checkView(context);
                checkLatestEntries(context);
                cleanup(context);
            }
        }

        private void cleanup(ServerResponseLogContext context)
        {
            // This should normally be avoided, but is used for the esence of time.
            context.Database.ExecuteSqlCommand("delete from dbo.server_response_log where ResponseText in ('Unit Test Case 001', 'Unit Test Case 002', 'Unit Test Case 003')");
        }

        private void checkView(ServerResponseLogContext context)
        {
            var viewResults = context.ErrorCodeCounts;
            Assert.AreEqual(3, viewResults.CountAsync().Result);
        }

        private void checkLatestEntries(ServerResponseLogContext context)
        {
            // Make sure they are returned when dates are in range
            var inRange = context.GetLatestLogs(DateTime.Now.AddDays(-5), DateTime.Now);
            Assert.AreEqual(3, inRange.Count());
            Assert.AreEqual(1, inRange.Where(x => x.HttpStatusCode == 200).Count());
            Assert.AreEqual(1, inRange.Where(x => x.HttpStatusCode == 408).Count());
            Assert.AreEqual(1, inRange.Where(x => x.HttpStatusCode == 500).Count());

            // Make sure they aren't returned when dates are out of range
            var outOfRange = context.GetLatestLogs(DateTime.Now.AddDays(-1), DateTime.Now);
            Assert.AreEqual(0, outOfRange.Count());
        }
    }

}
