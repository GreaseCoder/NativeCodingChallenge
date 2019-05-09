using Console.Interfaces;
using Console.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    public class GETRequestExample
    {
        private readonly IRequestClient client;
        private readonly DbContext logContext;

        public GETRequestExample(IRequestClient client, DbContext logContext)
        {
            this.client = client;
            this.logContext = logContext;
        }

        public async Task MakeRequestAsync(string url, int? httpStatusCodeOverride = null)
        {
            var fullUrl = httpStatusCodeOverride == null ? url : $"{url}?statuscode={httpStatusCodeOverride}";
            System.Console.WriteLine($"Calling {fullUrl}");

            var response = await client.MakeRequestAsync(fullUrl);
            LogRequest(response).Wait();
        }

        private async Task LogRequest(ServerResponse requestResponse)
        {
            var logEntry = new ServerResponseLog()
            {
                StartTime = requestResponse.StartTime,
                EndTime = requestResponse.EndTime,
                HttpStatusCode = requestResponse.HttpStatusCode,
                ResponseText = requestResponse.Response,
                ErrorCode = (int)requestResponse.ErrorCode
            };

            await logContext.AddAsync(logEntry);

            try
            {
                var resultCode = await logContext.SaveChangesAsync();
                System.Console.WriteLine($"Logged entry result: {resultCode}");
            }
            catch(DbUpdateException e)
            {
                System.Console.WriteLine($"Logged entry result failed: {e.InnerException.Message}");
            }
        }
    }
}
