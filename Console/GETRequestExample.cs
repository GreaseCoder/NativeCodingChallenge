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
        private readonly IDbLoggingContext logContext;

        public GETRequestExample(IRequestClient client, IDbLoggingContext logContext)
        {
            this.client = client;
            this.logContext = logContext;
        }

        public async Task MakeRequestAsync(string url, int? httpStatusCodeOverride = null)
        {
            var fullUrl = httpStatusCodeOverride == null ? url : $"{url}?statuscode={httpStatusCodeOverride}";
            System.Console.WriteLine($"Calling {fullUrl}");

            var response = await client.MakeRequestAsync(fullUrl);
            logContext.AddLogEntry(response).Wait();
        }

    }
}
