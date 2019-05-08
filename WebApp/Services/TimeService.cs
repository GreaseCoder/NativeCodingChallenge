using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Interfaces;

namespace WebApp.Services
{
    public class TimeService : ITimeService
    {
        public async Task<string> GetTimeAsync()
        {
            return await Task.Run(() => DateTime.Now.ToString(@"G"));
        }
    }
}
