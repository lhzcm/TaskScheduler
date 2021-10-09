using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskSchedulerModel.Models;
using TaskSchedulerRepository.Repositorys;

namespace TaskSchedulerHost.Task
{
    public class TaskLoggerPerformer
    {
        private TaskLogger _logger = new TaskLogger();
        private ILogger<TaskLoggerPerformer> _log4;
        public bool IsStopNamedPipe { get; set; }
        public bool IsStopLogin { get; set; }

        public TaskLoggerPerformer(IServiceProvider provider, ILogger<TaskLoggerPerformer> log4)
        {
            IsStopNamedPipe = false;
            IsStopLogin = false;

            this._log4 = log4;

            Thread thread = new Thread(UseNamedPipe);
            thread.Start();

            thread = new Thread(() => { Log(provider); });
            thread.Start();
        }

        private void UseNamedPipe()
        {
            while (!IsStopNamedPipe)
            {
                try
                {
                    using (NamedPipeServerStream pipe = new NamedPipeServerStream("taskloggerpipe", PipeDirection.InOut, 1))
                    {
                        pipe.WaitForConnection();
                        pipe.ReadMode = PipeTransmissionMode.Byte;
                        using (StreamReader reader = new StreamReader(pipe))
                        {
                            string str = reader.ReadToEnd();
                            LogInfo loginfo = LHZ.FastJson.JsonConvert.Deserialize<LogInfo>(str);
                            _logger.Add(loginfo);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + ex.StackTrace);
                    _log4.LogError(ex.Message + ex.StackTrace);
                }
            }
        }

        private void Log(IServiceProvider provider)
        {
            while (!IsStopLogin)
            {
                try
                {
                    var list = _logger.GetLogs();
                    if (list.Count > 0)
                    {
                        using (var scope = provider.CreateScope())
                        {
                            var logRespository = scope.ServiceProvider.GetService<LogRepository>();
                            if (logRespository.Insert(list) > 0)
                            {
                                _logger.Remove(list);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + ex.StackTrace);
                    _log4.LogError(ex.Message + ex.StackTrace);
                }
                Thread.Sleep(1000);
            }
        }

        public static TaskLoggerPerformer Startup(IServiceProvider provider)
        {
            var log4 = provider.GetService<ILogger<TaskLoggerPerformer>>();
            if (log4 == null)
            {
                throw new Exception("获取日志对象失败");
            }
            return new TaskLoggerPerformer(provider, log4);

        }
    }
}
