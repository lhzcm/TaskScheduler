﻿using Microsoft.AspNetCore.Mvc;
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
    public class TaskManageController : BaseController
    {
        private TaskManageRepository _repository;
        private ILogger<TaskController> _logger;
        private TaskManager _manager;

        /// <summary>
        /// 获取用户所有任务权限
        /// </summary>
        [HttpGet("{page}/{pagesize}/{userId?}")]
        public Result TaskList(int page, int pagesize, int? userId)
        {
            try
            {
                List<TaskManage> list;
                if (userId <= 0)
                {
                    list = _repository.FindAll();
                }
                else
                {
                    list = _repository.Find(n => n.UserId == userId).ToList();
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
        /// 添加任务权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public Result Add([FromForm] int userId, [FromForm] int taskId)
        {
            if (userId <= 0)
            {
                return Fail("用户错误");
            }
            if (taskId <= 0)
            {
                return Fail("任务错误");
            }
            try
            {
                var task = _manager.GetTasks(n => n.Id == taskId).FirstOrDefault();
                if (task == null)
                {
                    return Fail("添加失败，没有找到任务");
                }
                if (_repository.Exists(n => n.TaskId == taskId && n.UserId == userId))
                {
                    return Fail("添加失败,该用户存在此任务的查看权限");
                }
                var manege = new TaskManage { UserId = userId, TaskId = taskId, Access = HandleAccess.SelectTask };
                if (_repository.Insert(manege) <= 0)
                {
                    return Fail("添加任务权限失败");
                }
                return Success(manege);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }

        /// <summary>
        /// 获取任务权限详情
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="taskId">任务ID</param>
        [HttpGet("{userId}/{taskId}")]
        public Result GetAll(int userId, int taskId)
        {
            try
            {
                var list = _repository.FindFirst(n => n.UserId == userId && n.TaskId == taskId);
                return Success(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }

        /// <summary>
        /// 更新用户任务权限
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="taskId">任务ID</param>
        /// <param name="access">权限</param>
        /// <returns></returns>
        [HttpPatch]
        public Result UpdateAccess([FromForm] int userId, [FromForm] int taskId, [FromForm] List<int> access)
        {
            if (userId <= 0)
            {
                return Fail("用户错误");
            }
            if (taskId <= 0)
            {
                return Fail("任务错误");
            }
            try
            {
                var manage = _repository.FindFirst(n => n.UserId == userId && n.TaskId == taskId);
                if (manage == null)
                {
                    return Fail("更新失败，该用户没有次任务权限");
                }

                HandleAccess access_update = HandleAccess.SelectTask;
                if (access.Exists(n => n == 1))
                    access_update |= HandleAccess.AddTask;
                if (access.Exists(n => n == 2))
                    access_update |= HandleAccess.UpdateTask;
                if (access.Exists(n => n == 4))
                    access_update |= HandleAccess.DeleteTask;
                if (access.Exists(n => n == 8))
                    access_update |= HandleAccess.RunTask;
                if (access.Exists(n => n == 16))
                    access_update |= HandleAccess.HandleCommand;
                if (access.Exists(n => n == 32))
                    access_update |= HandleAccess.HandleConfig;

                _repository.Update(n => n.UserId == userId && n.TaskId == taskId, n => new TaskManage { Access = access_update });
                return Success(manage, "更新成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }

        /// <summary>
        /// 删除权限任务
        /// </summary>
        /// <param name="tmid">权限任务id</param>
        [HttpDelete]
        public Result Del([FromForm] int tmid)
        {
            try
            {
                if (_repository.Delete(n => n.Id == tmid) <= 0)
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