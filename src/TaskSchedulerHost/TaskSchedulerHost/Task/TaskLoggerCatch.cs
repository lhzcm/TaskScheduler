using System;
using System.Collections.Generic;
using System.Linq;
using TaskSchedulerModel.Models;

namespace TaskSchedulerHost.Task
{
    /// <summary>
    /// 日志缓存类
    /// </summary>
    public class TaskLoggerCache
    {
        private static readonly Dictionary<int, Queue<LogInfo>> _logCache = new Dictionary<int, Queue<LogInfo>>(256);


        /// <summary>
        /// 把日志添加到缓存中
        /// </summary>
        /// <param name="logInfo"></param>
        public void AddToCache(LogInfo logInfo)
        {
            if (logInfo == null)
            {
                return;
            }

            Queue<LogInfo> queue;
            if (!_logCache.TryGetValue(logInfo.TaskId, out queue))
            {
                queue = new Queue<LogInfo>();
                _logCache.Add(logInfo.TaskId, queue);
            }
            queue.Enqueue(logInfo);
            if (queue.Count > 100)
            {
                queue.Dequeue();
            }
        }

        /// <summary>
        /// 移除任务日志缓存
        /// </summary>
        /// <param name="taskId">任务id</param>
        public void RemoveCache(int taskId)
        {
            _logCache.Remove(taskId);
        }

        /// <summary>
        /// 通过任务id获取缓存中的日志列表
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public List<LogInfo> GetLogsByCache(int taskId)
        {
            Queue<LogInfo> queue;
            if (!_logCache.TryGetValue(taskId, out queue))
            {
                return new List<LogInfo>();
            }
            return queue.ToList();
        }
    }
}
