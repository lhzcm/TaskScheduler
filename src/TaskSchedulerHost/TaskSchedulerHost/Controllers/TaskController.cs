using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public TaskController(TaskRespository respository)
        {
            this._respository = respository;
        }

        [HttpPost]
        public Result Add(TaskInfo task)
        {
            try
            {
                if (_respository.Insert(task) > 0)
                {
                    return Success(task, "添加成功");
                }
                else
                {
                    return Fail("添加失败");
                }
            }
            catch (Exception)
            {
                return Fail("系统错误");
            }
        
        }
    }
}
