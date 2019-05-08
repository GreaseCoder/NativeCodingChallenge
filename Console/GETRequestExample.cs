using Console.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    public class GETRequestExample
    {
        private readonly IRequestClient client;

        public GETRequestExample(IRequestClient client)
        {
            this.client = client;
        }

        public Task MakeRequest(string url)
        {
            return client.MakeRequest(url);
        }
    }
}
