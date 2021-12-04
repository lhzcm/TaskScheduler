using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSchedulerHost.Filter;
using TaskSchedulerHost.Models;
using TaskSchedulerHost.Task;
using TaskSchedulerModel.Models;
using TaskSchedulerRepository.Repositorys;

namespace TaskSchedulerHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Login]
    public class LogController : BaseController
    {
        private LogRepository _repository;
        private ILogger<LogController> _logger;
        private TaskLoggerCache _taskLoggerCache;

        public LogController(LogRepository respository, ILogger<LogController> logger, TaskLoggerCache taskLoggerCache)
        {
            this._repository = respository;
            this._logger = logger;
            this._taskLoggerCache = taskLoggerCache;
        }

        /// <summary>
        /// 获取日志信息列表
        /// </summary>
        /// <param name="taskId">任务id</param>
        /// <param name="page">页码</param>
        /// <param name="pagesize">页面大小</param>
        /// <returns></returns>
        [HttpGet("{taskId}/{page}/{pagesize}")]
        public Result GetLogs(int taskId, int page = 1, int pagesize = 20)
        {
            try
            {
                if (!GetAccess(User.Id, taskId, HandleAccess.SelectTask))
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
        }

        /// <summary>
        /// 获取缓存中的日志信息列表
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpGet("{taskId}")]
        public Result GetLogsByCatch(int taskId)
        {
            try
            {
                if (!GetAccess(User.Id, taskId, HandleAccess.SelectTask))
                {
                    return Fail("您还未拥有权限操作");
                }

                var logList = _taskLoggerCache.GetLogsByCache(taskId);
                return Success(new { List = logList});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }
    }
}
