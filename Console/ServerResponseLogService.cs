using Console.Interfaces;
using Console.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    class ServerResponseLogService : IDbLoggingContext
    {
        private readonly ServerResponseLogContext context;

        public ServerResponseLogService(ServerResponseLogContext context)
        {
            this.context = context;
        }

        public async Task AddLogEntry(ServerResponse requestResponse)
        {
            var logEntry = new ServerResponseLog()
            {
                StartTime = requestResponse.StartTime,
                EndTime = requestResponse.EndTime,
                HttpStatusCode = requestResponse.HttpStatusCode,
                ResponseText = requestResponse.Response
            };

            await context.AddAsync(logEntry);

            try
            {
                var resultCode = await context.SaveChangesAsync();
                System.Console.WriteLine($"Logged entry result: {resultCode}");
            }
            catch (DbUpdateException e)
            {
                System.Console.WriteLine($"Logged entry result failed: {e.InnerException.Message}");
            }
        }
    }
}
