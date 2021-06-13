using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerModel.Models;
using TaskSchedulerRespository.DbContexts;
using TaskSchedulerRespository.Respositorys;

namespace TaskSchedulerRepository.Repositorys
{
    public class TaskCommandRepository : BaseRepository<TaskSchedulerDbContext, TaskCommand>
    {
        public TaskCommandRepository(TaskSchedulerDbContext db) : base(db)
        {
            
        }
    }
}
