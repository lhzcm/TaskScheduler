using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSchedulerHost.Models;
using TaskSchedulerModel.Models;
using TaskSchedulerRepository.Repositorys;

namespace TaskSchedulerHost.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected Result Fail(string msg, object data = null)
        {
            return new Result { Code = Code.Fail, Msg = msg, Data = data };
        }

        protected Result Success(object data, string msg = "请求成功")
        {
            return new Result { Code = Code.Success, Msg = msg, Data = data };
        }

        protected UserInfo user = new UserInfo()
        {
            Id = 33316,
            Password = "36965816"
        };

        protected HandleAccess GetAccess(int taskid)
        {
            TaskManageRepository task = new TaskManageRepository(new TaskSchedulerRepository.DbContexts.TaskSchedulerDbContext());
            TaskManage task1;
            task1 = task.FindFirst(n => n.UserId == user.Id && n.TaskId == taskid) ?? new TaskManage();
            return HandleAccess.RunTask | HandleAccess.DeleteTask;
        }
    }
}
