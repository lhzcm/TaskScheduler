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
            Console.WriteLine(System.Environment.CurrentDirectory);
            var array = System.Environment.CurrentDirectory.Split('\\').ToList();
            array.RemoveAt(array.Count - 1);


            var path = string.Join("\\", array) + "\\TaskScheduler\\bin\\Debug\\net5.0\\TaskScheduler22.exe";
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.FileName = path;
            process.Start();

            //while (true)
            //{
            //    Console.WriteLine(process.ProcessName);
            //}
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
