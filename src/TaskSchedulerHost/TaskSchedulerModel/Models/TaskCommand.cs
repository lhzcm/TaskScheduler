﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerModel.Models
{
    [Table("t_task_command")]
    public class TaskCommandInfo : BaseModel
    {
        /// <summary>
        /// 任务id
        /// </summary>
        public int TaskId { get; set; }
        
        /// <summary>
        /// 命令描述
        /// </summary>
        [Required]
        [Column(TypeName = "varchar(128)")]
        public string Description { get; set; }

        /// <summary>
        /// 命令
        /// </summary>
        [Required]
        [Column(TypeName = "varchar(64)")]
        public string Command { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime WriteTime { get; set; }
    }
}
