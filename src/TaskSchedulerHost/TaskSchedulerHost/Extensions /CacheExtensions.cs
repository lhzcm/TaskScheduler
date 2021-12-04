using System;
using Microsoft.Extensions.Caching.Memory;
using TaskSchedulerHost.Utility;
using TaskSchedulerModel.Models;
namespace TaskSchedulerHost.Extensions
{
    /// <summary>
    /// 缓存扩展方法类
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// 从缓存中获取用户信息
        /// </summary>
        /// <param name="Cache">缓存</param>
        /// <param name="Userid">用户ID</param>
        /// <returns>用户信息</returns>
        public static UserInfo GetUser(this IMemoryCache Cache, int Userid)
        {
            return CacheUtility.GetUser(Userid, Cache);
        }

        /// <summary>
        /// 缓存用户信息
        /// </summary>
        /// <param name="User">用户信息</param>
        /// <param name="Cache">缓存</param>
        public static void SetUser(this IMemoryCache Cache, UserInfo User)
        {
            CacheUtility.SetUser(User, Cache);
        }
    }
}
