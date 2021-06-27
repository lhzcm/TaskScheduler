using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskScheduler.Logger;

namespace TaskScheduler
{
    public interface ITaskRunner
    {
        List<LogInfo> LogQueuen { get; }
        bool Running { get; set; }

        void LogQueuenAdd(LogInfo logInfo);
        void LogQueuenRemove(LogInfo logInfo);
        void Log(LogLevel level, string msg);
        void LogError(string msg);
        void LogWarring(string msg);
        void LogInfo(string msg);
        void LogMessage(string msg);
        void Command(string command);

        void Run(int appId);
    }
}
