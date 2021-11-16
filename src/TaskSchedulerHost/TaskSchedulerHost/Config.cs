using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSchedulerHost
{
    public class Config
    {
        /// <summary>
        /// 任务App的路径
        /// </summary>
        public string TaskAppPath { get; set; }

        /// <summary>
        /// 可执行文件的路径
        /// </summary>
        public string ExecAppFile { get; set; }

        /// <summary>
        /// 可执行文件的配置文件路径
        /// </summary>
        public string ExecAppConfig { get; set; }

        /// <summary>
        /// 可执行文件类库路径
        /// </summary>
        public string[] ExecLibFile { get; set; }

        /// <summary>
        /// 跨域地址
        /// </summary>
        public string CORS { get; set; }

        /// <summary>
        /// Token MD5加密添加字符串
        /// </summary>
        public string TokenMD5Str { get; set; }

        /// <summary>
        /// Token 过期天数
        /// </summary>
        public int TokenDays { get; set; }
    }
}
