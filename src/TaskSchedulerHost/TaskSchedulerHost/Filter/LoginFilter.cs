using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSchedulerHost.Models;

namespace TaskSchedulerHost.Filter
{
    public class LoginAttribute : ActionFilterAttribute
    {
        public override System.Threading.Tasks.Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Items.ContainsKey("user"))
            {
                return context.HttpContext.Response.WriteAsJsonAsync(new Result() { Code = Code.NoLogin, Msg="请先登陆！" });
            }
            else
            {
                return next.Invoke();
            }
        }
    }
}
