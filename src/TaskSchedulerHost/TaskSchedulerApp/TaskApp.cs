using LHZ.FastJson;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        private ITaskRunner _runner;
        private string _commandPipeHandle;

        public TaskApp(int appId, string commandPipeHandle)
        {
            AppId = appId;
            AppPath = Environment.CurrentDirectory;
            this._commandPipeHandle = commandPipeHandle;
        }

        /// <summary>
        /// 运行任务
        /// </summary>
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

                _runner = System.Activator.CreateInstance(types[0]) as ITaskRunner;
                if (_runner == null)
                {
                    throw new Exception("实例化对象失败");
                }
                //创建日志队列线程
                Thread thread = new Thread(()=>ExecLogQuenuen(_runner));
                thread.Start();
                //创建命令接送线程
                thread = new Thread(CommandListen);
                thread.Start();
                try
                {
                    SendLog(new LogInfo { Level = LogLevel.Info, Message = "【开始执行】", WriteTime = DateTime.Now });
                    _runner.Running = true;
                    _runner.Run(AppId);
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
                SendLog(new LogInfo { Level = LogLevel.Error, Message = ex.Message + ex.StackTrace, WriteTime = DateTime.Now });
            }

            //结束时把队列中的任务发送完
            if (_runner != null)
            {
                SendQuenuenLog(_runner);
            }
            
        }

        /// <summary>
        /// 初始化任务
        /// </summary>
        /// <param name="appId">任务id</param>
        /// <param name="commandPipeHandle">命令匿名管道话柄</param>
        /// <param name="configs">配置文件</param>
        /// <returns>任务对象</returns>
        public static TaskApp Init(int appId, string commandPipeHandle, string configs)
        {
            //初始化配置文件
            if (!string.IsNullOrEmpty(configs))
            {
                InitConfig(configs);
            }

            //创建任务app
            var taskApp = new TaskApp(appId, commandPipeHandle);
            return taskApp;
        }

        /// <summary>
        /// 初始化配置信息
        /// </summary>
        /// <param name="configs">配置字符串</param>
        public static void InitConfig(string configs)
        {
            string[] keyValueConfigs = configs.Split(',');
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);


            //删除之前的配置
            string[] oldKeys = config.AppSettings.Settings.AllKeys;
            foreach (string item in oldKeys)
            {
                config.AppSettings.Settings.Remove(item);
            }

            //新加配置
            foreach (var item in keyValueConfigs)
            {
                string[] keyValue = item.Split(':');
                if (keyValue.Length != 2)
                {
                    continue;
                }

                config.AppSettings.Settings.Add(Encoding.UTF8.GetString(Convert.FromBase64String(keyValue[0])),
                    Encoding.UTF8.GetString(Convert.FromBase64String(keyValue[1])));
            }
            config.Save();
        }

        /// <summary>
        /// 执行日志队列
        /// </summary>
        /// <param name="runner">运行的目标Task</param>
        public void ExecLogQuenuen(ITaskRunner runner)
        {
            while (true)
            {
                SendQuenuenLog(runner);
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// 发送队列中所有日志
        /// </summary>
        /// <param name="runner">运行的目标Task</param>
        public void SendQuenuenLog(ITaskRunner runner)
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

        /// <summary>
        /// 发送日志
        /// </summary>
        /// <param name="log">日志</param>
        /// <returns>是否发送成功</returns>
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
                        writer.WriteLine(JsonConvert.Serialize(log, new LHZ.FastJson.Json.Format.DateTimeJsonFormat("yyyy-MM-dd HH:mm:ss.fff")));
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

        /// <summary>
        /// 监听命令
        /// </summary>
        public void CommandListen()
        {
            using (AnonymousPipeClientStream pipe = new AnonymousPipeClientStream(PipeDirection.In, _commandPipeHandle))
            {
                byte[] commandByte = new byte[1024];
                while (true)
                {
                    int count = pipe.Read(commandByte, 0, commandByte.Length);
                    string command = Encoding.UTF8.GetString(commandByte, 0, count);
                    if (command == "quit")
                    {
                        _runner.Running = false;
                    }
                    try
                    {
                        SendLog(new LogInfo { TaskId = AppId, Level = LogLevel.Info, Message = "【执行命令】" + command });
                        _runner.Command(command);
                    }
                    catch (Exception ex)
                    {
                        SendLog(new LogInfo { TaskId = AppId, Level = LogLevel.Error, Message = "【执行命令出错】" + ex.Message + ex.StackTrace });
                    }
                }
            }
        }
    }
}
