using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int appId = Convert.ToInt32(args[0]);
            string path = args[1];
            TaskApp.Init(appId, path).Run();
            Environment.Exit(0);
        }
    }
}
