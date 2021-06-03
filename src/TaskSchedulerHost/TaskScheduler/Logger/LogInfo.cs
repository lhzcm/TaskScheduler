using System;
using System.Collections.Generic;
using System.Text;

namespace TaskScheduler.Logger
{
    public class LogInfo
    {
        /// <summary>
        /// 日志Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 任务ID
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 日志等级
        /// </summary>
        public LogLevel Level { get; set; }

        /// <summary>
        /// 日志信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime WriteTime { get; set; }


    }
}
