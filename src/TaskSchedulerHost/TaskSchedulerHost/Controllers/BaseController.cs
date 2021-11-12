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
        private UserInfo _user;

        public new UserInfo User 
        {
           
            get
            {
                if (_user != null)
                {
                    return _user;
                }
                object user = null;
                if (HttpContext.Items.TryGetValue("user", out user))
                {
                    _user = (user as UserInfo) ?? new UserInfo();
                }
                else
                {
                    _user = new UserInfo();
                }
                return _user;
            }
        }

        public int UserId => User.Id;

        protected Result Fail(string msg, object data = null)
        {
            return new Result { Code = Code.Fail, Msg = msg, Data = data };
        }

        protected Result Success(object data, string msg = "请求成功")
        {
            return new Result { Code = Code.Success, Msg = msg, Data = data };
        }

        /// <summary>
        /// 获取用户对任务的管理权限
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public static bool GetAccess(int userid, int taskid, HandleAccess access)
        {
            bool flag = false;
            UserInfoRepository userinfo = new UserInfoRepository(new TaskSchedulerRepository.DbContexts.TaskSchedulerDbContext());
            UserInfo user = userinfo.FindFirst(n => n.Id == userid) ?? new UserInfo();
            if (user.Super)
                return user.Super;
            TaskManageRepository task = new TaskManageRepository(new TaskSchedulerRepository.DbContexts.TaskSchedulerDbContext());
            TaskManage manage = task.FindFirst(n => n.UserId == userid && n.TaskId == taskid) ?? new TaskManage();
            if ((access == HandleAccess.AddTask) && (manage.Access & HandleAccess.AddTask) == HandleAccess.AddTask)
                flag = true;
            if ((access == HandleAccess.UpdateTask) && (manage.Access & HandleAccess.UpdateTask) == HandleAccess.UpdateTask)
                flag = true;
            if ((access == HandleAccess.DeleteTask) && (manage.Access & HandleAccess.DeleteTask) == HandleAccess.DeleteTask)
                flag = true;
            if ((access == HandleAccess.RunTask) && (manage.Access & HandleAccess.RunTask) == HandleAccess.RunTask)
                flag = true;
            if ((access == HandleAccess.HandleCommand) && (manage.Access & HandleAccess.HandleCommand) == HandleAccess.HandleCommand)
                flag = true;
            if ((access == HandleAccess.HandleConfig) && (manage.Access & HandleAccess.HandleConfig) == HandleAccess.HandleConfig)
                flag = true;
            if ((access == HandleAccess.SelectTask) && (manage.Access & HandleAccess.SelectTask) == HandleAccess.SelectTask)
                flag = true;
            return flag;
        }
    }
}
