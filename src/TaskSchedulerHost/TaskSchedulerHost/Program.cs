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
using log4net;
using TaskSchedulerRespository.Respositorys;
using Microsoft.Extensions.DependencyInjection;
using TaskSchedulerHost.Task;
using TaskSchedulerHost.Controllers;

namespace TaskSchedulerHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host =CreateHostBuilder(args).Build();
            TaskLoggerPerformer.Startup(host.Services);
            TaskManager.Init(host.Services);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureLogging((context, loggingBuilder) =>
                {
                    loggingBuilder.AddFilter("System", LogLevel.Information);
                    loggingBuilder.AddFilter("Microsoft", LogLevel.Information);
                    var path = context.HostingEnvironment.ContentRootPath;
                    loggingBuilder.AddLog4Net($"{path}/log4net.config");//ÅäÖÃÎÄ¼þ
                })
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
