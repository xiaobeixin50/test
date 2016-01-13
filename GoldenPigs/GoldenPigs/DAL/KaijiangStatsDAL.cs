using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldenPigs.Entity;
using MySql.Data.MySqlClient;

namespace GoldenPigs.DAL
{
    public class KaijiangStatsDAL
    {
        public void RefreshKaijiangStats()
        {
//            SELECT shengspint,pingspint,fuspint ,COUNT(*) AS spcount FROM kaijiang
//GROUP BY shengspint,pingspint,fuspint
//ORDER BY spcount;
            //首先获取需要分组结果
            string conStr = "server=localhost;User Id=root;database=aicai;" +
                            "Password=root;Character Set=utf8;";
            DataSet dsTotal = new DataSet();
            using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
            {
                mySqlCon.Open();
                using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                {
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    //string sqlStr = @"Select bianhao,liansai,zhudui,kedui,rangqiu,shengsp,pingsp,fusp,riqi from peilv order by bianhao";
                    string sqlStr = @" 
SELECT shengspint,pingspint,fuspint ,COUNT(*) AS spcount FROM kaijiang
GROUP BY shengspint,pingspint,fuspint
ORDER BY spcount;";

                    mySqlCom.CommandText = sqlStr;

                    da.SelectCommand = mySqlCom;
                    da.Fill(dsTotal);
                }
                List<KaijiangStats> stats = new List<KaijiangStats>();
                foreach (DataRow row in dsTotal.Tables[0].Rows)
                {
                    KaijiangStats stat = new KaijiangStats();
                    stat.shengspint = Convert.ToInt32(row["shengspint"]);
                    stat.pingspint = Convert.ToInt32(row["pingspint"]);
                    stat.fuspint = Convert.ToInt32(row["fuspint"]);
                    stat.totalcount = Convert.ToInt32(row["spcount"]);

                    stats.Add(stat);

                    DataSet dsDetail = new DataSet();
                    using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                    {
                        MySqlDataAdapter da = new MySqlDataAdapter();
                        //string sqlStr = @"Select bianhao,liansai,zhudui,kedui,rangqiu,shengsp,pingsp,fusp,riqi from peilv order by bianhao";
                        string sqlStr = @" 
SELECT spfresult,COUNT(*) as spfcount FROM kaijiang
WHERE shengspint = ?shengspint	
AND PingSpInt = ?pingspint
AND FuSpint = ?fuspint		
GROUP BY spfresult";

                        mySqlCom.CommandText = sqlStr;
                        mySqlCom.Parameters.AddWithValue("?shengspint", stat.shengspint);
                        mySqlCom.Parameters.AddWithValue("?pingspint", stat.pingspint);
                        mySqlCom.Parameters.AddWithValue("?fuspint", stat.fuspint);
                        da.SelectCommand = mySqlCom;
                        da.Fill(dsDetail);
                    }
                    stat.shengcount = getSpCount(dsDetail.Tables[0],3);
                    stat.pingcount = getSpCount(dsDetail.Tables[0],1);
                    stat.fucount = getSpCount(dsDetail.Tables[0], 0);
                    stat.shenglv = stat.shengcount*1.0/stat.totalcount;
                    stat.pinglv = stat.pingcount*1.0/stat.totalcount;
                    stat.fulv = stat.fucount*1.0/stat.totalcount;



                }

                foreach (KaijiangStats stat in stats)
                {
                    InsertKaijiangStat(stat);
                }

            }


        }

        private void InsertKaijiangStat(KaijiangStats stat)
        {
            string conStr = "server=localhost;User Id=root;database=aicai;" +
                            "Password=root;Character Set=utf8;";
            object objresult = 0;
            using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
            {
                mySqlCon.Open();
                using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                {
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    //判断是否存在，不存在再添加，存在则修改
                    string sqlStr = @" 
SELECT COUNT(*) as spfcount FROM kaijiang_stats
WHERE shengspint = ?shengspint	
AND PingSpInt = ?pingspint
AND FuSpint = ?fuspint	";


                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?shengspint", stat.shengspint);
                    mySqlCom.Parameters.AddWithValue("?pingspint", stat.pingspint);
                    mySqlCom.Parameters.AddWithValue("?fuspint", stat.fuspint);
                    objresult  = mySqlCom.ExecuteScalar();
                }
                

                using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                {
                    //判断是否存在，不存在再添加，存在则修改
                    if (Convert.ToInt32(objresult) == 0)
                    {
                        string strInsert = @"INSERT INTO kaijiang_stats(shengspint,pingspint,fuspint,totalcount,shengcount,pingcount,fucount,shenglv,pinglv,fulv)
VALUES(?shengspint,?pingspint,?fuspint,?totalcount,?shengcount,?pingcount,?fucount,?shenglv,?pinglv,?fulv)";
                        mySqlCom.CommandText = strInsert;
                        mySqlCom.Parameters.AddWithValue("?shengspint", stat.shengspint);
                        mySqlCom.Parameters.AddWithValue("?pingspint", stat.pingspint);
                        mySqlCom.Parameters.AddWithValue("?fuspint", stat.fuspint);

                        mySqlCom.Parameters.AddWithValue("?totalcount", stat.totalcount);
                        mySqlCom.Parameters.AddWithValue("?shengcount", stat.shengcount);
                        mySqlCom.Parameters.AddWithValue("?pingcount", stat.pingcount);
                        mySqlCom.Parameters.AddWithValue("?fucount", stat.fucount);
                        mySqlCom.Parameters.AddWithValue("?shenglv", stat.shenglv);
                        mySqlCom.Parameters.AddWithValue("?pinglv", stat.pinglv);
                        mySqlCom.Parameters.AddWithValue("?fulv", stat.fulv);

                    }
                    else
                    {
                        string strUpdate = @"UPDATE kaijiang_stats
SET totalcount = ?totalcount,
shengcount = ?shengcount,
pingcount = ?pingcount,
fucount = ?fucount,
shenglv = ?shenglv,
pinglv = ?pinglv,
fulv = ?fulv
WHERE shengspint = ?shengspint,
pingspint = ?pingspint,
fuspint = ?fuspint";
                        mySqlCom.CommandText = strUpdate;
                        mySqlCom.Parameters.AddWithValue("?shengspint", stat.shengspint);
                        mySqlCom.Parameters.AddWithValue("?pingspint", stat.pingspint);
                        mySqlCom.Parameters.AddWithValue("?fuspint", stat.fuspint);
                        mySqlCom.Parameters.AddWithValue("?totalcount", stat.totalcount);
                        mySqlCom.Parameters.AddWithValue("?shengcount", stat.shengcount);
                        mySqlCom.Parameters.AddWithValue("?pingcount", stat.pingcount);
                        mySqlCom.Parameters.AddWithValue("?fucount", stat.fucount);
                        mySqlCom.Parameters.AddWithValue("?shenglv", stat.shenglv);
                        mySqlCom.Parameters.AddWithValue("?pinglv", stat.pinglv);
                        mySqlCom.Parameters.AddWithValue("?fulv", stat.fulv);

                    }
                    mySqlCom.ExecuteNonQuery();
                }

            }
        }

        private int getSpCount(DataTable dt, int spfResult)
        {
            int result = 0;
            foreach (DataRow row in dt.Rows)
            {
                int spf = Convert.ToInt32(row[0]);
                if (spfResult == spf)
                {
                    result = Convert.ToInt32(row[1]);

                }
            }
            return result;
        }

        public DataSet SearchKaijianStats()
        {
            string conStr = "server=localhost;User Id=root;database=aicai;" +
                            "Password=root;Character Set=utf8;";
            DataSet ds = new DataSet();
            using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
            {
                mySqlCon.Open();
                using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                {
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    //string sqlStr = @"Select bianhao,liansai,zhudui,kedui,rangqiu,shengsp,pingsp,fusp,riqi from peilv order by bianhao";
                    string sqlStr = @"
select * from kaijiang_stats;";

                    mySqlCom.CommandText = sqlStr;

                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);
                }
            }
            return ds;
        }
    }
}