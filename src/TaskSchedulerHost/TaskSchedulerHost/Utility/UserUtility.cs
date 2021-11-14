using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerHost.Utility
{
    public static class UserUtility
    {
        /// <summary>
        /// 通过用户id获取token
        /// </summary>
        /// <param name="Userid">用户id</param>
        /// <returns>token</returns>
        public static string GetTokenById(int Userid)
        {

            return GetTokenById(Userid, DateTime.Now);
        }

        /// <summary>
        /// 通过用户id获取token
        /// </summary>
        /// <param name="Userid">用户id</param>
        /// <param name="AddTime">添加时间</param>
        /// <returns>token</returns>
        public static string GetTokenById(int Userid, DateTime AddTime)
        {

            string md5Str = App.Config.TokenMD5Str;
            string time = AddTime.ToString("yyyy-MM-dd HH:mm:ss");
            md5Str += time;
            md5Str += Userid;

            string md5 = ToMD5(md5Str);
            return md5 + "_" + Userid + "_" + time;

        }

        /// <summary>
        /// 通过Token获取用户id
        /// </summary>
        /// <param name="Token">token</param>
        /// <param name="Userid">用户id</param>
        /// <param name="AddTime">添加时间</param>
        /// <returns>token是否有效</returns>
        public static bool GetIdByToken(string Token, out int Userid, out DateTime AddTime)
        {
            Userid = 0;
            AddTime = DateTime.MinValue;

            string md5Str = App.Config.TokenMD5Str;
            string[] strs = Token.Split('_');
            if (strs.Length != 3)
            {
                return false;
            }
            string md5 = ToMD5(md5Str + strs[2] + strs[1]);
            if (md5 != strs[0])
            {
                return false;
            }
            if (!int.TryParse(strs[1], out Userid))
            {
                return false;
            }
            if (!DateTime.TryParse(strs[2], out AddTime))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 字符串转换成MD5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToMD5(string destin)
        {
            MD5 md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(destin));

            StringBuilder strBuilder = new StringBuilder(40);
            foreach (var item in bytes)
            {
                strBuilder.Append(item.ToString("X2"));
            }
            return strBuilder.ToString();
        }

    }
}
