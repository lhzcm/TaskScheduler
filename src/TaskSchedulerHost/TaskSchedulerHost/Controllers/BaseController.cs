using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSchedulerHost.Models;

namespace TaskSchedulerHost.Controllers
{
    public class BaseController : ControllerBase
    {
        protected Result Fail(string msg, object data = null)
        {
            return new Result { Code = Code.Fail, Msg = msg, Data = data };
        }

        protected Result Success(object data, string msg = "请求成功")
        {
            return new Result { Code = Code.Success, Msg = msg, Data = data };
        }
    }
}
