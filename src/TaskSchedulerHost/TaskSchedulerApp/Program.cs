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
            string commandPipeHandle = args[1];
            string configs = null;
            if (args.Length >= 3)
            {
                configs = args[2];
            }

            TaskApp.Init(appId, commandPipeHandle, configs).Run();
            Environment.Exit(0);
        }
    }
}
