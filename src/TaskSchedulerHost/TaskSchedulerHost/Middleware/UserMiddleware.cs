using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSchedulerHost.Models;
using TaskSchedulerHost.Utility;
using TaskSchedulerModel.Models;
using TaskSchedulerRepository.Repositorys;

namespace TaskSchedulerHost.Middleware
{
    public static class UserMiddleware
    {
        public static IApplicationBuilder UserIdentity(this IApplicationBuilder builder)
        {
            return builder.Use( async (httpContext, next) =>
            {
                int userid; DateTime time;
                string token = httpContext.Request.Cookies["token"] ?? "";
                try
                {
                    if (UserUtility.GetIdByToken(token, out userid, out time))
                    {
                        UserInfo user = null;
                        IMemoryCache cache = httpContext.RequestServices.GetService(typeof(IMemoryCache)) as IMemoryCache;
                        if (cache != null)
                        {
                            user = cache.Get<UserInfo>("user:" + userid);
                            if (user == null)
                            {
                                UserInfoRepository repository = httpContext.RequestServices.GetService(typeof(UserInfoRepository)) as UserInfoRepository;
                                if (repository != null)
                                {
                                    user = repository.FindFirst(n => n.Id == userid);
                                }
                            }
                            if (user != null)
                            {
                                httpContext.Items.Add("user", user);
                                cache.Set<UserInfo>("user:" + user.Id, user, new DateTimeOffset().AddHours(2));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + ex.StackTrace);
                }

                await next.Invoke();
            });
        }
    }
}
