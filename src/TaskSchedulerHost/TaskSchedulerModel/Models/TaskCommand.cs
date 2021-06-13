using System;
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
        public int TaskId { get; set; }

        [Required]
        [Column(TypeName = "varchar(128)")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "varchar(64)")]
        public string Command { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime WriteTime { get; set; }
    }
}
