using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using TaskSchedulerModel.Models;

namespace TaskSchedulerHost.Utility
{
    public class CacheUtility
    {
        /// <summary>
        /// 在缓存中获取用户信息
        /// </summary>
        /// <param name="Userid">用户id</param>
        /// <param name="Cache">缓存</param>
        /// <returns>用户信息</returns>
        public static UserInfo GetUser(int Userid, IMemoryCache Cache)
        {
            if (Cache == null)
            {
                return null;
            }
            return Cache.Get<UserInfo>("user:" + Userid);
        }

        /// <summary>
        /// 缓存用户信息
        /// </summary>
        /// <param name="User">用户信息</param>
        /// <param name="Cache">缓存</param>
        public static void SetUser(UserInfo User, IMemoryCache Cache)
        {
            if (Cache != null && User != null)
            {
                Cache.Set<UserInfo>("user:" + User.Id, User, new TimeSpan(2, 0, 0));
            }
        }
    }
}
