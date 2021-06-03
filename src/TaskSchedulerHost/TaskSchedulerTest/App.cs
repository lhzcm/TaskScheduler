using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskScheduler;

namespace TaskSchedulerTest
{
    public class App : TaskRunner
    {
        public override void Run()
        {

            for (int i = 0; i < 60; i++)
            {
                LogMessage(i.ToString());
                Thread.Sleep(1000);
            }
        }
    }
}
