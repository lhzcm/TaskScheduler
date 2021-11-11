using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerModel.Models;
using TaskSchedulerRepository.DbContexts;

namespace TaskSchedulerRepository.Repositorys
{
    public class UserInfoRepository : BaseRepository<TaskSchedulerDbContext, UserInfo>
    {
        public UserInfoRepository(TaskSchedulerDbContext db) : base(db)
        {
        }
    }
}
