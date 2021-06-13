using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskScheduler;

namespace TaskSchedulerTest
{
    public class App : TaskRunner
    {
        Random rand = new Random();
        List<Thread> threads = new List<Thread>();

        public void KillCore()
        {
            long num = 0;
            while (true)
            {
                num += rand.Next(1, 100);
                if (num > 10000000) { num = 0; }
            }
        }

        public void Main()
        {
            for(int i = 0; i<= 1; i++)
            {
                (new Thread(new ThreadStart(KillCore))).Start();
            }
        }

        public override void Run(int appId)
        {
            //Main();

            int i = 0;
            while (Running)
            {
                i++;
                LogMessage("执行第" + i.ToString() + "次");
                Thread.Sleep(500);
            }
        }


    }
}
