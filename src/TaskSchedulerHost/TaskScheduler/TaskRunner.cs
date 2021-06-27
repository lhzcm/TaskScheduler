using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskScheduler.Logger;

namespace TaskScheduler
{
    public abstract class TaskRunner : ITaskRunner
    {
        private List<LogInfo> _logQuenuen = new List<LogInfo>();
        public List<LogInfo> LogQueuen
        {
            get
            {
                lock (obj)
                {
                    return _logQuenuen.ToList();
                }
            }
        }
        private static object obj = new object();

        public bool Running { get; set; }

        public abstract void Run(int appId);

        public void Log(LogLevel level, string msg)
        {
            LogQueuenAdd(new LogInfo { Level = level, Message = msg, WriteTime = DateTime.Now });
        }

        public void LogError(string msg)
        {
            Log(LogLevel.Error, msg);
        }

        public void LogInfo(string msg)
        {
            Log(LogLevel.Info, msg);
        }

        public void LogMessage(string msg)
        {
            Log(LogLevel.Message, msg);
        }

        public void LogQueuenAdd(LogInfo logInfo)
        {
            lock (obj)
            {
                _logQuenuen.Add(logInfo);
            }
        }

        public void LogQueuenRemove(LogInfo logInfo)
        {
            lock (obj)
            {
                _logQuenuen.Remove(logInfo);
            }
        }

        public void LogWarring(string msg)
        {
            Log(LogLevel.Warring, msg);
        }

        public virtual void Command(string command)
        {
            
        }
    }
}
