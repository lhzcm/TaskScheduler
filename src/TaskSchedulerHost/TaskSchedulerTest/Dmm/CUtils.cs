using Lexun.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerTest.Dmm
{
    public class CUtils
    {
        public static string ReadPage(string url, string ipproxy, int Timeout)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)192;
            HttpWebRequest rqst = null;
            HttpWebResponse response = null;
            Stream responseStream = null;
            StreamReader reader = null;
            Uri PageUrl = new Uri(url);
            try
            {
                rqst = (HttpWebRequest)WebRequest.Create(PageUrl);
                if (!string.IsNullOrEmpty(ipproxy))
                {
                    WebProxy proxy = new WebProxy(ipproxy);
                    rqst.Proxy = proxy;
                }
                if (Timeout > 0)
                    rqst.Timeout = Timeout;
                rqst.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.54 Safari/537.36";
                //rqst.Method = "POST";
                rqst.ContentType = "application/x-www-form-urlencoded";
                rqst.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
                rqst.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9,en;q=0.8");
                rqst.Headers.Add("Accept-Encoding", "gzip, deflate");
                rqst.Headers.Add("Cache-Control", "max-age=0");
                rqst.Headers.Add("Cookie", "HMF_CI=847baef5a3a891d50832edd2cb517c6541a2565cd04e1d7ee2705ac8a830242d96; 21_vq=9");
                rqst.Headers.Add("Upgrade-Insecure-Requests", "1");
                response = (HttpWebResponse)rqst.GetResponse();
                Encoding cding = System.Text.Encoding.Default;
                if (response.ContentEncoding == "gzip")
                {
                    responseStream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
                }
                else
                {
                    int ix = response.ContentType.ToLower().IndexOf("charset=");
                    if (ix != -1)
                    {
                        try
                        {
                            cding = System.Text.Encoding.GetEncoding(response.ContentType.Substring(ix + "charset".Length + 1));
                        }
                        catch
                        {
                            cding = Encoding.Default;
                        }
                    }
                    responseStream = response.GetResponseStream();
                }
                reader = new StreamReader(responseStream, Encoding.UTF8);
                return reader.ReadToEnd();
            }
            catch (WebException ex)
            {
                CLog.WriteLog("访问页面" + url + "出错：\r\n" + ex.Message + "\r\n" + ex.StackTrace);
                return ex.Message;
            }
            finally
            {
                if (response != null)
                    response.Close();
                if (responseStream != null)
                    responseStream.Dispose();
                if (reader != null)
                    reader.Dispose();
            }
        }

        /// <summary>
        /// 反序列化为对象
        /// </summary>
        /// <param name="jsonstr"></param>
        /// <returns></returns>
        public static T DeserialiseFromJson<T>(string jsonstr) where T : class
        {
            if (string.IsNullOrWhiteSpace(jsonstr))
                return null;

            jsonstr = jsonstr.Replace("\0", "");
            jsonstr = jsonstr.Replace("\n", "");
            //T msg = JsonMapper.ToObject<T>(jsonstr);
            T msg = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonstr);
            return msg;
        }
    }
}
