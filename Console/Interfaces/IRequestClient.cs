using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Console.Interfaces
{
    public interface IRequestClient : IDisposable
    {
        Task MakeRequest(string url);
    }
}
