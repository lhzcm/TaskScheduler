using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerModel.Models
{
    [Table("t_task_config")]
    public class TaskConfig : BaseModel
    {
        /// <summary>
        /// 任务id
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 配置key
        /// </summary>
        [Required]
        [Column(TypeName = "varchar(256)")]
        public string Key { get; set; }

        /// <summary>
        /// 配置值
        /// </summary>
        [Required]
        [Column(TypeName = "varchar(2048)")]
        public string Value { get; set; }

        public DateTime WriteTime { get; set; }

    }
}
