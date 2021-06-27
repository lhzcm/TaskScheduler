using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSchedulerHost.Models;
using TaskSchedulerHost.Task;
using TaskSchedulerHost.Task.Extend;
using TaskSchedulerModel.Models;
using TaskSchedulerRepository.Repositorys;

namespace TaskSchedulerHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskCommandController : BaseController
    {
        private TaskCommandRepository _repository;
        private ILogger<TaskCommandController> _logger;
        private TaskManager _manager;

        public TaskCommandController(TaskCommandRepository repository, ILogger<TaskCommandController> logger, TaskManager manager)
        {
            this._repository = repository;
            this._logger = logger;
            this._manager = manager;
        }

        /// <summary>
        /// 添加命令
        /// </summary>
        /// <param name="description">命令描述</param>
        /// <param name="command">命令</param>
        [HttpPost("Add")]
        public Result Add([FromForm] int taskId, [FromForm] string description, [FromForm] string command)
        {
            if (command == "quit")
            {
                return Fail("添加失败，quit为系统命令，无法手动添加");
            }
            try
            {
                if (!_manager.GetTasks().Any(n => n.Id == taskId))
                {
                    return Fail("添加失败，没有找到对应的任务");
                }

                var taskCommand = new TaskCommandInfo { Description = description, Command = command, TaskId = taskId };
                if (_repository.Insert(taskCommand) > 0)
                {
                    return Success(taskCommand, "添加成功");
                }
                else
                {
                    return Fail("添加失败");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }


        /// <summary>
        /// 获取所有命令
        /// </summary>
        [HttpGet("{taskId}")]
        public Result List(int taskId)
        {
            try
            {
                var list = _repository.Find(n=>n.TaskId == taskId);
                list.Add(new TaskCommandInfo { TaskId = taskId, Id = -1, Description = "停止", Command = "quit" });
                return Success(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }

        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="tcid">命令id</param>
        [HttpPost("Command")]
        public Result Command([FromForm] int taskId, [FromForm] int tcid)
        {
            try
            {
                TaskCommandInfo taskCommand;
                if (tcid == -1)
                {
                    taskCommand = new TaskCommandInfo { TaskId = taskId, Description = "停止", Command = "quit" };
                }
                else
                {
                    taskCommand = _repository.FindFirst(n => n.Id == tcid);
                }
                if (taskCommand == null)
                {
                    return Fail("发送失败，没有找到命令");
                }
                var task = _manager.GetTasks(n => n.Id == taskCommand.TaskId).FirstOrDefault();
                if (task == null)
                {
                    return Fail("发送失败，没有找到任务");
                }
                if (!task.IsRuning)
                {
                    return Fail("发送失败，任务没有运行");
                }
                task.Command(taskCommand.Command);
                return Success(taskCommand, "发送成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }

        /// <summary>
        /// 删除命令
        /// </summary>
        /// <param name="tcid">命令id</param>
        [HttpDelete]
        public Result CommandDel([FromForm] int tcid)
        {
            if (tcid == -1)
            {
                return Fail("删除失败，系统命令无法删除");
            }
            try
            {
                if (_repository.Delete(n => n.Id == tcid) > 0)
                {
                    return Success(null, "删除成功");
                }
                return Fail("删除失败");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }
    }
}
