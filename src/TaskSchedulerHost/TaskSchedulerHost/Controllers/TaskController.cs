using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TaskSchedulerHost.Models;
using TaskSchedulerModel.Models;
using TaskSchedulerRespository.Respositorys;
using System.IO.Compression;
using Microsoft.Extensions.Logging;
using TaskSchedulerHost.Task;

namespace TaskSchedulerHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : BaseController
    {
        private TaskRespository _respository;
        private Config _config;
        private ILogger<TaskController> _logger;
        private TaskManager _manager;
        public TaskController(TaskRespository respository, Config config, ILogger<TaskController> logger, TaskManager manager)
        {
            this._respository = respository;
            this._config = config;
            this._logger = logger;
            this._manager = manager;
        }

        [HttpPost]
        public Result Add(string Name, IFormFile file)
        {
            try
            {
                _respository.DbContext.Database.BeginTransaction();
                TaskInfo task = new TaskInfo { Name = Name, ExecFile = "" };
                _respository.Insert(task);

                var path = _config.TaskAppPath + task.TaskGuid.ToString("N");
                try
                {
                    using (ZipArchive archive = new ZipArchive(file.OpenReadStream()))
                    {
                        if (!System.IO.Directory.Exists(path))
                        {
                            System.IO.Directory.CreateDirectory(path);
                        }
                        archive.ExtractToDirectory(path);
                    }
                }
                catch(Exception ex)
                {
                    _respository.DbContext.Database.RollbackTransaction();
                    if (System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.Delete(path, true);
                    }
                    _logger.LogError(ex.Message + ex.StackTrace);
                    return Fail("解压文件失败");
                }
                try
                {
                    task.ExecFile = path + "/" + task.Name + Path.GetExtension(_config.ExecAppFile);
                    System.IO.File.Copy(_config.ExecAppFile, task.ExecFile);
                }
                catch(Exception ex)
                {
                    _respository.DbContext.Database.RollbackTransaction();
                    if (System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.Delete(path, true);
                    }
                    _logger.LogError(ex.Message + ex.StackTrace);
                    return Fail("复制执行文件失败");
                }
                if (_respository.Update(task) <= 0)
                {
                    return Fail("更新执行文件路径失败");
                }

                _respository.DbContext.Database.CommitTransaction();

                _manager.Add(task);
                return Success(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }

        [HttpGet]
        public Result TaskList()
        {
            try
            {
                return Success(_manager.GetTasks());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }

        [HttpGet("{Id}")]
        public Result TaskInfo(int Id)
        {
            try
            {
                var task = _manager.GetTasks(n => n.Id == Id).FirstOrDefault();
                return Success(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }

        [HttpDelete]
        public Result TaskDel(int Id)
        {
            try
            {
                var task = _manager.GetTasks(n => n.Id == Id).FirstOrDefault();
                if (task != null && task.IsRuning)
                {
                    return Fail("删除失败，程序正在运行，请先停止程序");
                }
                if (_respository.Delete(n => n.Id == Id) > 0)
                {
                    
                    return Success(null, "删除成功");
                }
                else
                {
                    return Fail("删除失败");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }
    }
}
