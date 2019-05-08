using Console.Interfaces;
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

        public async Task MakeRequestAsync(string url)
        {
            using (var response = await client.GetAsync(url))
            {
                using (var content = response.Content)
                {
                    var data = await content.ReadAsStringAsync();
                    if (data != null)
                    {
                        System.Console.WriteLine(data);
                    }
                }
            }
        }

    }
}
