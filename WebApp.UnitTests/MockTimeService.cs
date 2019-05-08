using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApp.Interfaces;

namespace WebApp.UnitTests
{
    class MockTimeService : ITimeService
    {
        private readonly string timestamp = "5/8/2019 2:22:00 PM";

        public MockTimeService(string timestamp)
        {
            this.timestamp = timestamp;
        }

        public async Task<string> GetTimeAsync()
        {
            return await Task.Run(() => { return timestamp; });
        }
    }
}
