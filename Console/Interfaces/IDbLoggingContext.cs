using Console.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Console.Interfaces
{
    public interface IDbLoggingContext
    {
        Task AddLogEntry(ServerResponse requestResponse);
    }
}
