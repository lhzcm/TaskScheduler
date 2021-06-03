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
using System.Diagnostics;

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

        [HttpPost("Run")]
        public Result Run(int Id)
        {
            try
            {
                var task = _manager.GetTasks(n => n.Id == Id).FirstOrDefault();
                if (task == null)
                {
                    return Fail("运行失败，没有找到任务");
                }
                if (task.IsRuning)
                {
                    return Fail("运行失败，任务正在运行中");
                }
                if (task.Process == null)
                {
                    List<string> args = new List<string>();
                    args.Add(task.Id.ToString());
                    var path = Path.Combine(Environment.CurrentDirectory, task.ExecFile.Replace("./", "").Replace("/", "\\"));
                    path = Path.GetDirectoryName(path);
                    args.Add(path);

                    task.Process = Process.Start(task.ExecFile, String.Join(" ", args));
                }
                else
                {
                    task.Process.Start();
                }
                return Success("程序运行成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }

        [HttpPost("Kill")]
        public Result Kill(int Id)
        {
            try
            {
                var task = _manager.GetTasks(n => n.Id == Id).FirstOrDefault();
                if (task == null)
                {
                    return Fail("退出失败，没有找到任务");
                }
                if (!task.IsRuning)
                {
                    return Fail("退出失败，任务没有运行");
                }
                task.Process.Kill();

                return Success("程序退出成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }

        [HttpPost("Add")]
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
                    _respository.DbContext.Database.RollbackTransaction();
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
                _respository.DbContext.Database.BeginTransaction();
                if (_respository.Delete(n => n.Id == Id) <= 0)
                {
                    _respository.DbContext.Database.RollbackTransaction();
                    return Fail("删除失败");
                }
                var path = _config.TaskAppPath + task.TaskGuid.ToString("N");
                if (Directory.Exists(path))
                {
                    try
                    {
                        Directory.Delete(path, true);
                    }
                    catch (Exception ex)
                    {
                        _respository.DbContext.Database.RollbackTransaction();
                        _logger.LogError(ex.Message + ex.StackTrace);
                        return Fail("删除失败, 删除文件夹（"+path+"）失败！");
                    }
                }
                _respository.DbContext.Database.CommitTransaction();
                return Success(null, "删除成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }

        [HttpPatch]
        public Result UpdateLib(int Id, IFormFile file)
        {
            try
            {
                TaskInfo task = _manager.GetTasks(n=>n.Id == Id).FirstOrDefault();
                if (task == null)
                {
                    return Fail("上传更新失败，没有找到改任务");
                }
                if (task.IsRuning)
                {
                    return Fail("上传更新失败,程序正在运行，请停止后更新");
                }

                var path = _config.TaskAppPath + task.TaskGuid.ToString("N");

                if (Directory.Exists(path))
                {
                    try
                    {
                        Directory.Delete(path, true);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message + ex.StackTrace);
                        return Fail("更新失败，删除旧文件失败");
                    }
                }
                try
                {
                    using (ZipArchive archive = new ZipArchive(file.OpenReadStream()))
                    {
                        System.IO.Directory.CreateDirectory(path);
                        archive.ExtractToDirectory(path);
                    }
                }
                catch (Exception ex)
                {
                    if (System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.Delete(path, true);
                    }
                    _logger.LogError(ex.Message + ex.StackTrace);
                    return Fail("解压文件失败");
                }
                try
                {
                    var execFile = path + "/" + task.Name + Path.GetExtension(_config.ExecAppFile);
                    System.IO.File.Copy(_config.ExecAppFile, execFile);
                }
                catch (Exception ex)
                {
                    if (System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.Delete(path, true);
                    }
                    _logger.LogError(ex.Message + ex.StackTrace);
                    return Fail("复制执行文件失败");
                }

                task.UpdateTime = DateTime.Now;
                _respository.Update(n => n.Id == task.Id, n=> new TaskInfo { UpdateTime = DateTime.Now});
                return Success(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }
    }
}
