using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSchedulerModel.Models;

namespace TaskSchedulerHost.Models
{
    public class TaskModel : TaskInfo
    {
        public IFormFile File { get; set; }
    }
}
