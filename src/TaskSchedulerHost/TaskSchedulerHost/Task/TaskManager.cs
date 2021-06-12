using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSchedulerModel.Models;
using TaskSchedulerRespository.Respositorys;

namespace TaskSchedulerHost.Task
{
    public class TaskManager
    {
        private static readonly List<TaskInfo> _tasks = new List<TaskInfo>();
        private TaskRepository _repository;
        public TaskManager(TaskRepository respository)
        {
            this._repository = respository;
        }

        /// <summary>
        /// 添加单个任务
        /// </summary>
        /// <param name="task"></param>
        public void Add(TaskInfo task)
        {
            if (task == null)
                return;

            lock (_tasks)
            {
                _tasks.Add(task);
            }
        }

        /// <summary>
        /// 添加多个任务
        /// </summary>
        /// <param name="tasks"></param>
        public void Add(List<TaskInfo> tasks)
        {
            if (tasks == null)
                return;
            lock (_tasks)
            {
                _tasks.AddRange(tasks);
            }
        }

        /// <summary>
        /// 移除单个任务
        /// </summary>
        /// <param name="task"></param>
        public void Remove(TaskInfo task)
        {
            if(task == null)
                return;
            lock (_tasks)
            {
                _tasks.Remove(task);
            }
        }

        /// <summary>
        /// 按条件移除任务
        /// </summary>
        /// <param name="task"></param>
        public void Remove(Func<TaskInfo, bool> whereCase)
        {
            if (whereCase == null)
                return;
            lock (_tasks)
            {
                var target = _tasks.Where(whereCase).ToList();
                foreach (var item in target)
                {
                    _tasks.Remove(item);
                }
            }
        }

        /// <summary>
        /// 移除多个任务
        /// </summary>
        /// <param name="tasks"></param>
        public void Remove(List<TaskInfo> tasks)
        {
            if (tasks == null)
                return;
            lock (_tasks)
            {
                foreach (var item in tasks)
                {
                    _tasks.Remove(item);
                }
            }
        }

        /// <summary>
        /// 与数据库同步数据
        /// </summary>
        public void ReFulsh()
        {
            var tasks = _repository.FindAll();
            foreach (var item in tasks)
            {
                var info = _tasks.FirstOrDefault(n => n.Id == item.Id);
                if (info == null)
                {
                    //添加数据库中存在内存中没有的任务
                    Add(item);
                }
                else if (!info.IsRuning)
                {
                    info.Process = null;
                    info.Name = item.Name;
                    info.ExecFile = item.ExecFile;
                    info.UpdateTime = item.UpdateTime;
                }
            }
            //移除没有运行且数据库已经删除的任务
            foreach(var item in _tasks)
            {
                if (!item.IsRuning && !tasks.Exists(n => n.Id == item.Id))
                {
                    Remove(item);
                }
            }
        }

        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <returns>任务列表</returns>
        public List<TaskInfo> GetTasks()
        {
            return _tasks.ToList();
        }

        /// <summary>
        /// 按条件获取任务列表
        /// </summary>
        /// <returns>任务列表</returns>
        public List<TaskInfo> GetTasks(Func<TaskInfo, bool> whereCase)
        {
            if (whereCase == null)
                return new List<TaskInfo>();
            return _tasks.Where(whereCase).ToList();
        }


        public static void Init(IServiceProvider serverProvider)
        {
            if (_tasks.Count > 0)
                return;

            using (var scope = serverProvider.CreateScope())
            {
                var taskManager = new TaskManager(scope.ServiceProvider.GetService<TaskRepository>());
                taskManager.ReFulsh();
            }
        }
    }
}
