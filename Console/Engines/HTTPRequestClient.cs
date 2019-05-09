using Console.Interfaces;
using Console.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Console.Engines
{
    class HTTPRequestClient : IRequestClient
    {
        private readonly HttpClient client = new HttpClient();

        public HTTPRequestClient()
        {
        }

        public void Dispose()
        {
            client.Dispose();
        }

        public async Task<ServerResponse> MakeRequestAsync(string url)
        {
            var startTime = DateTime.Now;
            string responseContent = null;

            using (var response = await client.GetAsync(url))
            {
                var endTime = DateTime.Now;
                using (var content = response.Content)
                {
                    var data = await content.ReadAsStringAsync();

                    if (data != null)
                    {
                        System.Console.WriteLine(data);
                        responseContent = data;
                    }
                }

                return new ServerResponse()
                {
                    StartTime = startTime,
                    EndTime = endTime,
                    HttpStatusCode = (int)response.StatusCode,
                    Response = responseContent,
                    ErrorCode = ErrorCodes.Success
                };
            }

        }

    }
}
