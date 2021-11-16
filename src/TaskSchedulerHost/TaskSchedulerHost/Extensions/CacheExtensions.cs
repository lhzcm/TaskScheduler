using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSchedulerHost.Utility;
using TaskSchedulerModel.Models;

namespace TaskSchedulerHost.Extensions
{
    public static class CacheExtensions
    {
        public static UserInfo GetUser(this IMemoryCache Cache, int Userid)
        {
            return CacheUtility.GetUser(Userid, Cache);
        }

        public static void SetUser(this IMemoryCache Cache, UserInfo User)
        {
            CacheUtility.SetUser(User, Cache);
        }
    }
}
