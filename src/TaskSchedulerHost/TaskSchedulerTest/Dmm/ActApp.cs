using Lexun.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskScheduler;

namespace TaskSchedulerTest.Dmm
{
    public class ActApp : TaskRunner
    {
        public override void Run(int appId)
        {
            int i = 0;
            while (Running)
            {
                i++;
                GetData();
                Thread.Sleep(5000);
            }
        }

        private void GetData()
        {
            CDatabase db = null;
            try
            {
                db = new CDatabase("act_beast");
                if (Act20211030ResultAdo.p_act_20211030_unite_winning_detail(out DateTime dateflag, db) != null)
                {
                    return;
                }
                LogMessage(dateflag.ToString("MM-dd") + "结果抓取");
                List<Act20211030ResultInfo> infos = fc3d_Capture();
                if (!infos.Exists(n => n.dateflag == dateflag))
                    return;
                Act20211030ResultInfo info = infos.Where(n => n.dateflag == dateflag).First();
                string msg = "";
                if (info.result >= 0)
                {
                    LogMessage("今日结果抓取成功：" + info.result);
                    if (!Act20211030ResultAdo.p_act_20211030_unite_winning_insert(dateflag, info.result, out msg, db))
                    {
                        LogMessage("更新开奖结果失败" + msg);
                    }
                    else
                    {
                        LogMessage(msg + "-结果为：" + info.result);
                    }
                }
                else
                {
                    LogMessage("今日结果抓取失败");
                }
            }
            catch (Exception ex)
            {
                CLog.WriteLog(ex.Message + ex.StackTrace);
                LogMessage(ex.Message);
            }
            finally
            {
                db.close();
            }
        }

        private List<Act20211030ResultInfo> fc3d_Capture()
        {
            string url = "http://www.cwl.gov.cn/cwl_admin/front/cwlkj/search/kjxx/findDrawNotice?name=3d&issueCount=30&issueStart=&issueEnd=&dayStart=&dayEnd=";
            string data = string.Empty;
            List<Act20211030ResultInfo> list = new List<Act20211030ResultInfo>();
            try
            {
                data = CUtils.ReadPage(url, "", 10000);
            }
            catch (Exception ex)
            {
                LogMessage("获取fc3d彩票网数据出错:" + ex.Message + ex.StackTrace);
                CLog.WriteLog("获取fc3d彩票网数据出错:" + ex.Message + ex.StackTrace);
                return list;
            }
            try
            {
                fc3dlottery result = CUtils.DeserialiseFromJson<fc3dlottery>(data);

                if (result == null || result.result == null)
                    return list;

                foreach (var item in result.result)
                {
                    Act20211030ResultInfo info = new Act20211030ResultInfo();
                    info.dateflag = CTools.ToDateTime(item.date.Substring(0, 10));
                    info.result = CTools.ToInt(item.red.Substring(0, 1));
                    list.Add(info);
                }
                return list.OrderByDescending(p => p.dateflag).ToList();
            }
            catch (Exception ex)
            {
                CLog.WriteLog("获取OG311X5开奖网数据出错#2:" + data + ex.Message + ex.StackTrace);
                return list;
            }
        }
    }
}
