using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSchedulerHost.Models;
using TaskSchedulerModel.Models;
using TaskSchedulerRepository.Repositorys;

namespace TaskSchedulerHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogController : BaseController
    {
        private LogRepository _repository;
        private ILogger<LogController> _logger;
        public LogController(LogRepository respository, ILogger<LogController> logger)
        {
            this._repository = respository;
            this._logger = logger;
        }

        [HttpGet("{taskId}/{page}/{pagesize}")]
        public Result GetLog(int taskId, int page = 1, int pagesize = 20)
        {
            try
            {
                if ((GetAccess(taskId) & HandleAccess.SelectTask) != HandleAccess.SelectTask)
                {
                    return Fail("您还未拥有权限操作");
                }

                int total = 0;
                var logList = _repository.Find(page, pagesize, n => n.TaskId == taskId, out total, n => n.Id, false);
                return Success(new { List = logList, Total = total });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }

            //TaskManage user = new TaskManage();
            //user.Access = HandleAccess.AddTask | HandleAccess.UpdateTask | HandleAccess.DeleteTask;

            //if ((user.Access & HandleAccess.AddTask) == HandleAccess.AddTask)
            //{

            //}
        }
    }
}
