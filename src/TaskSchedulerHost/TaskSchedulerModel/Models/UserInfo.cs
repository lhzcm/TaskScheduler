using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerModel.Models
{

    [Table("t_user_info")]
    public class UserInfo: BaseModel
    {
        [Column(TypeName = "varchar(32)")]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "varchar(18)")]
        [Required]
        public string Password { get; set; }


    }
}
