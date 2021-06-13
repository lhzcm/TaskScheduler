using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerModel.Models;
using TaskSchedulerRepository.DbContexts;
using TaskSchedulerRepository.Repositorys;

namespace TaskSchedulerRepository.Repositorys
{
    public class TaskCommandRepository : BaseRepository<TaskSchedulerDbContext, TaskCommandInfo>
    {
        public TaskCommandRepository(TaskSchedulerDbContext db) : base(db)
        {
            
        }
    }
}
