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
            int i = 0;
            while (true)
            {
                i++;
                LogMessage("执行第" + i.ToString() + "次");
                Thread.Sleep(10000);
            }
        }
    }
}
