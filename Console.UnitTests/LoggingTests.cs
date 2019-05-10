using Console.Models;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Console.UnitTests
{
    class LoggingTests : SqlDatabaseTestClass
    {

    }

    public class DatabaseTest
    {
        public void ConfigureHost(IHostBuilder builder)
        {
            var options = new DbContextOptionsBuilder<ServerResponseLogContext>()
                .UseInMemoryDatabase(databaseName: "ae_code_challange")
                .Options;

            using (var context = new ServerResponseLogContext(options))
            {
                
            }
        }
    }
}
