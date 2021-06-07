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
            TaskApp.Init(appId).Run();
            Environment.Exit(0);
        }
    }
}
