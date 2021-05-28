using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TaskSchedulerHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //ConfigurationBuilder bulid = new ConfigurationBuilder();
                    //bulid.AddJsonFile("appsettings.json");
                    //var config = bulid.Build();
                    //webBuilder.UseConfiguration(config);

                    webBuilder.UseStartup<Startup>();
                });
    }
}
