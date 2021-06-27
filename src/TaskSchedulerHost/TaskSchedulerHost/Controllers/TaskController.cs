using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TaskSchedulerHost.Models;
using TaskSchedulerModel.Models;
using TaskSchedulerRepository.Repositorys;
using System.IO.Compression;
using Microsoft.Extensions.Logging;
using TaskSchedulerHost.Task;
using System.Diagnostics;
using TaskSchedulerHost.Task.Extend;

namespace TaskSchedulerHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : BaseController
    {
        private TaskRepository _repository;
        private Config _config;
        private ILogger<TaskController> _logger;
        private TaskManager _manager;
        public TaskController(TaskRepository repository, Config config, ILogger<TaskController> logger, TaskManager manager)
        {
            this._repository = repository;
            this._config = config;
            this._logger = logger;
            this._manager = manager;
        }

        /// <summary>
        /// 运行任务 
        /// </summary>
        /// <param name="Id">任务id</param>
        [HttpPost("Run")]
        public Result Run([FromForm] int Id)
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
                //启动任务
                if (!task.Start())
                {
                    return Fail("任务启动失败！");
                }
                task.HasExistCommand = false;
                return Success("程序运行成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }

        /// <summary>
        /// 停止任务
        /// </summary>
        /// <param name="Id">任务id</param>
        [HttpPost("Kill")]
        public Result Kill([FromForm] int Id)
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
                task.HasExistCommand = true;
                return Success("程序退出成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="name">任务名称</param>
        /// <param name="file">任务程序集zip包</param>
        [HttpPost("Add")]
        public Result Add([FromForm]string name, [FromForm]IFormFile file)
        {
            try
            {
                if (name == null)
                {
                    return Fail("名称不能为空");
                }
                if (file == null)
                {
                    return Fail("文件不能为空");
                }

                _repository.DbContext.Database.BeginTransaction();
                TaskInfo task = new TaskInfo { Name = name, ExecFile = "" };
                _repository.Insert(task);

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
                    _repository.DbContext.Database.RollbackTransaction();
                    if (System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.Delete(path, true);
                    }
                    _logger.LogError(ex.Message + ex.StackTrace);
                    return Fail("解压文件失败");
                }
                try
                {
                    //复制可执行文件
                    task.ExecFile = path + "/" + task.Name + Path.GetExtension(_config.ExecAppFile);
                    System.IO.File.Copy(_config.ExecAppFile, task.ExecFile, true);

                    //复制配置文件
                    var configFilePath = task.ExecFile + Path.GetExtension(_config.ExecAppConfig);
                    System.IO.File.Copy(_config.ExecAppConfig, configFilePath, true);

                    //复制类库
                    if (_config.ExecLibFile != null)
                    {
                        foreach (var item in _config.ExecLibFile)
                        {
                            var libFile = path + "/" + Path.GetFileName(item);
                            System.IO.File.Copy(item, libFile, true);
                        }
                    }
                }
                catch(Exception ex)
                {
                    _repository.DbContext.Database.RollbackTransaction();
                    if (System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.Delete(path, true);
                    }
                    _logger.LogError(ex.Message + ex.StackTrace);
                    return Fail("复制执行文件失败");
                }
                if (_repository.Update(task) <= 0)
                {
                    _repository.DbContext.Database.RollbackTransaction();
                    return Fail("更新执行文件路径失败");
                }

                _repository.DbContext.Database.CommitTransaction();

                _manager.Add(task);
                return Success(task, "添加成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }


        /// <summary>
        /// 获取所有任务
        /// </summary>
        [HttpGet("{page}/{pagesize}/{name?}")]
        public Result TaskList(int page, int pagesize, string? name)
        {
            try
            {
                List<TaskInfo> list;
                if (name == null)
                {
                    list = _manager.GetTasks().ToList();
                }
                else
                {
                    list = _manager.GetTasks().Where(n => n.Name.Contains(name)).ToList();
                }
                var result = list.Skip((page - 1) * pagesize).Take(pagesize).ToList();
                return Success(new { list = result, total = list.Count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }

        /// <summary>
        /// 获取任务详情
        /// </summary>
        /// <param name="Id">任务id</param>
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

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="Id">任务id</param>
        [HttpDelete]
        public Result TaskDel([FromForm] int Id)
        {
            try
            {
                var task = _manager.GetTasks(n => n.Id == Id).FirstOrDefault();
                if (task != null && task.IsRuning)
                {
                    return Fail("删除失败，程序正在运行，请先停止程序");
                }
                _repository.DbContext.Database.BeginTransaction();
                if (_repository.Delete(n => n.Id == Id) <= 0)
                {
                    _repository.DbContext.Database.RollbackTransaction();
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
                        _repository.DbContext.Database.RollbackTransaction();
                        _logger.LogError(ex.Message + ex.StackTrace);
                        return Fail("删除失败, 删除文件夹（"+path+"）失败！");
                    }
                }
                _repository.DbContext.Database.CommitTransaction();
                _manager.Remove(task);
                return Success(null, "删除成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }

        /// <summary>
        /// 更新任务dll
        /// </summary>
        /// <param name="Id">任务id</param>
        /// <param name="file">任务程序集zip包</param>
        [HttpPatch]
        public Result UpdateLib([FromForm] int Id, IFormFile file)
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
                    //复制可执行文件
                    var execFile = path + "/" + task.Name + Path.GetExtension(_config.ExecAppFile);
                    System.IO.File.Copy(_config.ExecAppFile, execFile, true);

                    //复制配置文件
                    var configFilePath = task.ExecFile + Path.GetExtension(_config.ExecAppConfig);
                    System.IO.File.Copy(_config.ExecAppConfig, configFilePath, true);

                    //复制类库
                    if (_config.ExecLibFile != null)
                    {
                        foreach (var item in _config.ExecLibFile)
                        {
                            execFile = path + "/" + Path.GetFileName(item);
                            System.IO.File.Copy(item, execFile, true);
                        }
                    }
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
                _repository.Update(n => n.Id == task.Id, n=> new TaskInfo { UpdateTime = DateTime.Now});
                return Success(task, "更新成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }
    }
}
