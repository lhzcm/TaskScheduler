using System;
using System.Collections.Generic;
using TaskSchedulerModel.Models;

namespace TaskScheduler
{
    public class TaskManager
    {
        public static readonly List<TaskInfo> Tasks = new List<TaskInfo>();

        public static void ReFulsh(List<TaskInfo> tasks)
        {
            if (tasks == null || tasks.Count <= 0)
                return;
            foreach (var item in tasks)
            {
                if (!Tasks.Exists(n => n.Id == item.Id))
                {
                    Tasks.Add(item);
                }
            }
        }

        
    }
}
