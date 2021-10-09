using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskSchedulerModel.Models;

namespace TaskSchedulerHost.Task
{
    public class TaskLogger
    {
        private static object _obj = new object();
        private static readonly List<LogInfo> _logQueue = new List<LogInfo>();

        public void Add(LogInfo logInfo)
        {
            if (logInfo == null)
                return;

            if (logInfo.Message == null)
            {
                logInfo.Message = "[null]";
            }

            lock (_obj)
            {
                _logQueue.Add(logInfo);
            }
        }

        public void Remove(LogInfo logInfo)
        {
            lock (_obj)
            {
                _logQueue.Remove(logInfo);
            }
        }

        public void Remove(List<LogInfo> logInfos)
        {
            if (logInfos == null || logInfos.Count <= 0)
                return;
            lock (_obj)
            {
                foreach (var item in logInfos)
                {
                    _logQueue.Remove(item);
                }
            }
        }

        public List<LogInfo> GetLogs()
        {
            return _logQueue.ToList();
        }
    }
}
