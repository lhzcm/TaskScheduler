using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSchedulerHost.Filter
{
    public class LoginAttribute : ActionFilterAttribute
    {
        public override System.Threading.Tasks.Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Items.ContainsKey("user"))
            {
                return next.Invoke();
            }
            else
            {
                return next.Invoke();
            }
        }

        /// <summary>
        /// 记录Http请求上下文
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task OnResourceExecutedAsync(ResourceExecutingContext context)
        {

        }
    }
}
