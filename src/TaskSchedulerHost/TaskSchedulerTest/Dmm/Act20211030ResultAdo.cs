using Lexun.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerTest.Dmm
{
    public class Act20211030ResultAdo
    {
        /// <summary>
        /// 获取当天开奖结果
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static Act20211030ResultInfo p_act_20211030_unite_winning_detail(out DateTime dateflag, CDatabase db)
        {
            List<SqlParameter> parms = new List<SqlParameter>();

            SqlParameter pDateflag = new SqlParameter("@dateflag", SqlDbType.DateTime);
            pDateflag.Direction = ParameterDirection.Output;
            parms.Add(pDateflag);

            db.execute_procedure("p_act_20211030_unite_winning_detail", parms);

            dateflag = (pDateflag.Value == System.DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(pDateflag.Value);

            if (db.num_rows <= 0)
                return null;
            Act20211030ResultInfo c = new Act20211030ResultInfo();
            c.rid = (db.rows[0]["rid"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[0]["rid"].ToString());
            c.result = (db.rows[0]["result"] == System.DBNull.Value) ? 0 : Convert.ToInt32(db.rows[0]["result"].ToString());
            c.dateflag = (db.rows[0]["dateflag"] == System.DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(db.rows[0]["dateflag"].ToString());
            return c;
        }

        /// <summary>
        /// 添加开奖结果
        /// </summary>
        /// <param name="roundid"></param>
        /// <param name="rate"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static bool p_act_20211030_unite_winning_insert(DateTime dateflag, int result, out string outmsg, CDatabase db)
        {
            List<SqlParameter> parms = new List<SqlParameter>();

            parms.Add(new SqlParameter("@dateflag", dateflag));
            parms.Add(new SqlParameter("@result", result));

            SqlParameter pOutmsg = new SqlParameter("@outmsg", SqlDbType.VarChar, 256);
            pOutmsg.Direction = ParameterDirection.Output;
            parms.Add(pOutmsg);
            SqlParameter pRetval = new SqlParameter("@retval", SqlDbType.Int, 4);
            pRetval.Direction = ParameterDirection.ReturnValue;
            parms.Add(pRetval);

            db.execute_procedure("p_act_20211030_unite_winning_insert", parms);

            outmsg = (pOutmsg.Value == System.DBNull.Value) ? "" : Convert.ToString(pOutmsg.Value);
            int retval = (pRetval.Value == System.DBNull.Value) ? 0 : Convert.ToInt32(pRetval.Value);
            if (retval >= 1)
                return true;

            return false;
        }
    }
}
