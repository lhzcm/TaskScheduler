using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerModel.Models;
using TaskSchedulerRespository.DbContexts;

namespace TaskSchedulerRespository.Respositorys
{
    public class LogRepository : BaseRepository<TaskSchedulerDbContext, LogInfo>
    {
        public LogRepository(TaskSchedulerDbContext db) : base(db)
        {
            
        }
    }
}
