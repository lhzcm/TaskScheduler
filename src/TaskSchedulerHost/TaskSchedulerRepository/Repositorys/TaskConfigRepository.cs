using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerModel.Models;
using TaskSchedulerRepository.DbContexts;

namespace TaskSchedulerRepository.Repositorys
{
    public class TaskConfigRepository : BaseRepository<TaskSchedulerDbContext, TaskConfig>
    {
        public TaskConfigRepository(TaskSchedulerDbContext db) : base(db)
        {
            
        }
    }
}
