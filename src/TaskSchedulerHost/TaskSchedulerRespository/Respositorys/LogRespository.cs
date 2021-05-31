using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerModel.Models;
using TaskSchedulerRespository.DbContexts;

namespace TaskSchedulerRespository.Respositorys
{
    public class LogRespository : BaseRespository<TaskSchedulerDbContext, LogInfo>
    {
        public LogRespository(TaskSchedulerDbContext db) : base(db)
        {
            
        }
    }
}
