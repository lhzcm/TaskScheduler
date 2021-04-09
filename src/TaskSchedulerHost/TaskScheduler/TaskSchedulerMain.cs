using System;

namespace TaskScheduler
{
    /// <summary>
    /// 任务调度类
    /// </summary>
    public class TaskSchedulerMain
    {
        /// <summary>
        /// 任务调度运行方法
        /// </summary>
        public void Run() 
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("TaskScheduler");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(": Start\n");
        }
    }
}
