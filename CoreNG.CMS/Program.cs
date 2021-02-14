using Core5Ng.Core.EF;
using Core5Ng.Core.EF.Core;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreNG.CMS
{
    public class Program
    {
        public static async  Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var dpContext = services.GetRequiredService<DataProtectionKeyContext>();
                    var functionSvc = services.GetRequiredService<IFunctionalSvc>();
                    //var countrySvc = services.GetRequiredService<ICountrySvc>();

                    await DbContextInitializer.InitializeDb(dpContext, context, functionSvc);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while seeding the database");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
