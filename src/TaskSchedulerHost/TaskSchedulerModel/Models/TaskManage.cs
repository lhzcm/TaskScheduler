using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerModel.Models
{
    /// <summary>
    /// 操作权限
    /// </summary>
    public enum HandleAccess
    {
        AddTask = 1,
        UpdateTask = 2,
        DeleteTask = 4,
        RunTask = 8,
        HandleCommand = 16,
        HandleConfig = 32,
        SelectTask = 64
    }

    [Table("t_task_manage")]
    public class TaskManage : BaseModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int TaskId { get; set; }

        [Column(TypeName = "int")]
        public HandleAccess Access { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        [NotMapped]
        public string TaskName { get; set; }
    }
}
