using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerModel.Models;
using TaskSchedulerRepository.DbContexts;

namespace TaskSchedulerRepository.Repositorys
{
    public class TaskManageRepository : BaseRepository<TaskSchedulerDbContext, TaskManage>
    {
        public TaskManageRepository(TaskSchedulerDbContext db) : base(db)
        {
        }
    }
}
