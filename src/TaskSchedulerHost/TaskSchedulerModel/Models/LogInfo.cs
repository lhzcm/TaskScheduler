using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerModel.Attribute;

namespace TaskSchedulerModel.Models
{
    /// <summary>
    /// 普通日志
    /// </summary>
    [Table("t_log")]
    public class LogInfo : BaseModel
    {
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
        [Column(TypeName = "varchar(max)")]
        [Required]
        public string Message { get; set; }


        private DateTime _time;
        /// <summary>
        /// 添加时间
        /// </summary>
        [Column(TypeName = "datetime")]
        [JsonConverterDateTime]
        public DateTime WriteTime { 
            get 
            {
                if (_time == DateTime.MinValue)
                    return DateTime.Now;
                else
                    return _time;
            } 
            set
            {
                this._time = value;
            } 
        }

    }
}
