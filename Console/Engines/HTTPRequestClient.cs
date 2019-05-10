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

        /// <summary>
        /// Performs a request for the provided Url.
        /// </summary>
        public async Task<ServerResponse> MakeRequestAsync(string url)
        {
            string responseContent = null;
            var startTime = DateTime.Now;

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
                    Response = responseContent
                };
            }

        }

    }
}
