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

namespace TaskSchedulerHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : BaseController
    {
        private TaskRespository _respository;
        private Config _config;
        public TaskController(TaskRespository respository, Config config)
        {
            this._respository = respository;
            this._config = config;
        }

        [HttpPost]
        public Stream Add(string Name, IFormFile file)
        {
            //try
            //{
            //    if (_respository.Insert(task) > 0)
            //    {
            //        return Success(task, "添加成功");
            //    }
            //    else
            //    {
            //        return Fail("添加失败");
            //    }
            //}
            //catch (Exception)
            //{
            //    return Fail("系统错误");
            //}
            var a = file.OpenReadStream();
            var config = _config;
            return a;
        
        }
    }
}
