using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerModel.Models;

namespace TaskSchedulerHost.Task.Extend
{
    public static class TaskExtends
    {
        /// <summary>
        /// 任务启动
        /// </summary>
        /// <param name="task">任务</param>
        /// <returns>是否成功启动</returns>
        public static bool Start(this TaskInfo task, List<TaskConfig> configs)
        {
            //任务参数
            List<string> args = new List<string>();

            //任务ID参数
            args.Add(task.Id.ToString());

            //配置匿名管道
            task.Pipe = new AnonymousPipeServerStream(PipeDirection.Out, HandleInheritability.Inheritable);
            string clientHandle = task.Pipe.GetClientHandleAsString();
            args.Add(clientHandle);

            //任务配置参数
            if (configs != null && configs.Count > 0)
            {
                args.Add(String.Join(",", configs.Select(n => n.Key + ":" + n.Value)));
            }

            try
            {
                if (task.Process != null)
                {
                    task.Process.StartInfo.Arguments = string.Join(" ", args);
                    if (!task.Process.Start())
                    {
                        task.Pipe.Close();
                        task.Pipe.Dispose();
                        return false;
                    }
                    return true;
                }

                ProcessStartInfo startInfo = new ProcessStartInfo(task.ExecFile);

                //设置任务工作路径
                var path = Path.Combine(Environment.CurrentDirectory, task.ExecFile.Replace("./", "").Replace("/", "\\"));
                path = Path.GetDirectoryName(path);
                startInfo.WorkingDirectory = path;


                //启动任务
                startInfo.Arguments = string.Join(" ", args);
                task.Process = Process.Start(startInfo);
                if (task.Process == null)
                {
                    task.Pipe.Close();
                    task.Pipe.Dispose();
                    return false;
                }
                //task.Pipe.DisposeLocalCopyOfClientHandle();

                //退出事件
                task.Process.Exited += (object? sender, EventArgs e) =>
                {

                    //关闭匿名管道
                    task.Pipe.Close();
                    task.Pipe.Dispose();

                    if (task.Process.ExitCode == 0)
                        return;

                    TaskLogger taskLogger = new TaskLogger();

                    if (task.HasExistCommand)
                    {
                        taskLogger.Add(new LogInfo { TaskId = task.Id, Level = TaskSchedulerModel.Models.LogLevel.Info, Message = "【任务手动退出成功】ExistCode:" + task.Process.ExitCode });
                    }
                    else
                    {
                        taskLogger.Add(new LogInfo { TaskId = task.Id, Level = TaskSchedulerModel.Models.LogLevel.Error, Message = "【任务异常退出】ExistCode:" + task.Process.ExitCode });
                    }
                };
            }
            catch (Exception ex)
            {
                task.Pipe.Close();
                task.Pipe.Dispose();
            }
            return true;
        }

        /// <summary>
        /// 给任务发送命令
        /// </summary>
        /// <param name="task">任务</param>
        /// <param name="command">命令</param>
        public static void Command(this TaskInfo task, string command)
        {
            byte[] commandByte = Encoding.UTF8.GetBytes(command);
            task.Pipe.Write(commandByte, 0, commandByte.Length);
            task.Pipe.Flush();
        }

    }
}
