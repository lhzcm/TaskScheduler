using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSchedulerHost.Models;
using TaskSchedulerHost.Task;
using TaskSchedulerModel.Models;
using TaskSchedulerRepository.Repositorys;

namespace TaskSchedulerHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskConfigController : BaseController
    {
        private TaskConfigRepository _repository;
        private ILogger<TaskConfigController> _logger;
        private TaskManager _manager;

        public TaskConfigController(TaskConfigRepository repository, ILogger<TaskConfigController> logger, TaskManager manager)
        {
            this._repository = repository;
            this._logger = logger;
            this._manager = manager;
        }

        /// <summary>
        /// 添加配置
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        [HttpPost]
        public Result Add([FromForm] int taskId, [FromForm] string key, [FromForm] string value)
        {
            if (key == null || value == null)
            {
                return Fail("添加配置失败,Key和Value都不能为空");
            }

            var task = _manager.GetTasks(n => n.Id == taskId).FirstOrDefault();
            if (task == null)
            {
                return Fail("添加配置失败,不存在任务");
            }

            try
            {
                if (_repository.Exists(n => n.TaskId == taskId && n.Key == key))
                {
                    return Fail("添加配置失败,存在重复的Key");
                }
                var config = new TaskConfig { Key = key, TaskId = taskId, Value = value };
                if (_repository.Insert(config) <= 0)
                {
                    return Fail("添加配置失败，添加数据失败");
                }
                return Success(config);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }

        /// <summary>
        /// 获取配置列表
        /// </summary>
        /// <param name="taskId">任务ID</param>
        [HttpGet("{taskId}")]
        public Result GetAll(int taskId)
        {
            try
            {
                var list = _repository.Find(n => n.TaskId == taskId);
                return Success(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }

        /// <summary>
        /// 删除配置
        /// </summary>
        /// <param name="tcid">配置id</param>
        [HttpDelete]
        public Result Del([FromForm]int tcid)
        {
            try
            {
                if (_repository.Delete(n => n.Id == tcid) <= 0)
                {
                    return Fail("删除失败");
                }
                return Success(null, "删除成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }
    }
}
