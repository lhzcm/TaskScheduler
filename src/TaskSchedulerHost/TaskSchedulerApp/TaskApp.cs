using LHZ.FastJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using TaskScheduler;
using TaskScheduler.Logger;

namespace TaskSchedulerApp
{
    public class TaskApp
    {
        public readonly int AppId;
        public readonly string AppPath;

        public TaskApp(int appId, string path)
        {
            AppId = appId;
            AppPath = path;
        }

        public void Run()
        {
            try
            {
                var files = Directory.EnumerateFiles(AppPath);
                files = files.Where(n => Path.GetExtension(n).ToLower() == ".dll");

                List<Assembly> mainAssembly = new List<Assembly>();

                foreach (var item in files)
                {
                    Assembly assembly = Assembly.LoadFile(item);
                    Type[] astypes = assembly.GetTypes();
                    if (astypes.Any(n => n.BaseType == typeof(TaskRunner)))
                    {
                        mainAssembly.Add(assembly);
                    }
                }

                if (mainAssembly.Count == 0)
                {
                    throw new Exception("没有找到继承于ITaskRunner的类型");
                }

                if (mainAssembly.Count > 1)
                {
                    throw new Exception("找到多个存在继承于ITaskRunner的类型的程序集");
                }

                Type[] types = mainAssembly[0].GetTypes().Where(n => typeof(ITaskRunner).IsAssignableFrom(n)).ToArray();
                if (types.Length > 1)
                {
                    throw new Exception("找到多个存在继承于ITaskRunner的类型");
                }

                var runner = System.Activator.CreateInstance(types[0]) as ITaskRunner;
                if (runner == null)
                {
                    throw new Exception("实例化对象失败");
                }
                
                Thread thread = new Thread(()=>ExecLogQuenuen(runner));
                thread.Start();
                try
                {
                    SendLog(new LogInfo { Level = LogLevel.Info, Message = "【开始执行】", WriteTime = DateTime.Now });
                    runner.Run(AppId);
                    SendLog(new LogInfo { Level = LogLevel.Info, Message = "【执行结束并退出】", WriteTime = DateTime.Now });
                }
                catch (Exception ex)
                {
                    SendLog(new LogInfo { Level = LogLevel.Error, Message = "【异常退出】" + ex.Message + ex.StackTrace, WriteTime = DateTime.Now });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
                SendLog(new LogInfo { Level = LogLevel.Info, Message = ex.Message + ex.StackTrace, WriteTime = DateTime.Now });
            }
            
        }
        
        public static TaskApp Init(int appId, string path)
        {
            var taskApp = new TaskApp(appId, path);
            return taskApp;

            
          
        }

        public void ExecLogQuenuen(ITaskRunner runner)
        {
            while (true)
            {
                var quenuen = runner.LogQueuen;
                foreach (var item in quenuen)
                {
                    if (SendLog(item))
                    {
                        runner.LogQueuenRemove(item);
                    }
                }
                Thread.Sleep(1000);
            }
        }

        public bool SendLog(LogInfo log)
        {
            log.Id = 0;
            log.TaskId = AppId;
            try
            {
                using (NamedPipeClientStream pipe = new NamedPipeClientStream(".", "taskloggerpipe", PipeDirection.Out, PipeOptions.None, System.Security.Principal.TokenImpersonationLevel.None))
                {
                    pipe.Connect();
                    using (StreamWriter writer = new StreamWriter(pipe))
                    {
                        var proc = System.Diagnostics.Process.GetCurrentProcess();
                        writer.WriteLine(JsonConvert.Serialize(log));
                        writer.Flush();
                    }
                    pipe.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
            return false;
        }
    }
}
