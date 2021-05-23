using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerModel.Models
{
    [Table("t_task")]
    public class TaskInfo : BaseModel
    {
        /// <summary>
        /// 任务全局ID
        /// </summary>
        public Guid TaskGuid { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        [Column(TypeName = "varchar(128)")]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 可执行文件路径
        /// </summary>
        [Column(TypeName = "varchar(512)")]
        [Required]
        public string ExecFile { get; set; }

        /// <summary>
        /// 更新时间时间
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 状态（0：正常，-1：已删除）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime WriteTime { get; set; }
    }
}
