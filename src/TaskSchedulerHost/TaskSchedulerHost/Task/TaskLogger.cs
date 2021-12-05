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
        private static readonly List<LogInfo> _logQueue = new List<LogInfo>();
        private TaskLoggerCache _logCache = new TaskLoggerCache();

        /// <summary>
        /// 把日志信息添加到队列
        /// </summary>
        /// <param name="logInfo">日志信息</param>
        public void Add(LogInfo logInfo)
        {
            if (logInfo == null)
                return;

            if (logInfo.Message == null)
            {
                logInfo.Message = "[null]";
            }

            lock (_logQueue)
            {
                _logQueue.Add(logInfo);
                _logCache.AddToCache(logInfo);
            }
        }

        /// <summary>
        /// 把日志信息从队列中移除
        /// </summary>
        /// <param name="logInfo">日志信息</param>
        public void Remove(LogInfo logInfo)
        {
            lock (_logQueue)
            {
                _logQueue.Remove(logInfo);
            }
        }

        /// <summary>
        /// 把日志信息列表从队列中移除
        /// </summary>
        /// <param name="logInfos">日志列表</param>
        public void Remove(List<LogInfo> logInfos)
        {
            if (logInfos == null || logInfos.Count <= 0)
                return;
            lock (_logQueue)
            {
                foreach (var item in logInfos)
                {
                    _logQueue.Remove(item);
                }
            }
        }
        /// <summary>
        /// 获取队列中的日志
        /// </summary>
        /// <returns>日志列表</returns>
        public List<LogInfo> GetLogs()
        {
            return _logQueue.ToList();
        }

    }
}
