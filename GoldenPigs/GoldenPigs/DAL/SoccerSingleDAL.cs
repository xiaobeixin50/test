using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldenPigs.Entity;
using MySql.Data.MySqlClient;
using GoldenPigs.Helper;

namespace GoldenPigs.DAL
{
    public class SoccerSingleDAL
    {
        public DataSet SearchDanchang()
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
SELECT kaijiang.riqi,kaijiang.Bianhao,kaijiang.liansai,kaijiang.shengsp,kaijiang.PingSp,kaijiang.fusp, kaijiang.RqShengSp,kaijiang.RqPingSp,kaijiang.RqFuSp,kaijiang.QcbfResult
,kaijiang.SpfResult,kaijiang.RqspfResult
FROM kaijiang, soccer_single
WHERE kaijiang.riqi = soccer_single.riqi
AND kaijiang.Bianhao = soccer_single.bianhao 
ORDER BY riqi ,bianhao";

                    mySqlCom.CommandText = sqlStr;
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);

                }
            }
            return ds;
        }


        public void InsertSoccerSingleList(List<SoccerSingle> results)
        {
            string conStr = "server=localhost;User Id=root;database=aicai;" +
                           "Password=root;Character Set=utf8;";
            using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
            {
                mySqlCon.Open();
                foreach (SoccerSingle result in results)
                {
                    using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                    {
                        //                        string sqlStr = @"
                        //
                        //INSERT INTO basketyuce(title, result,zhudui,kedui,xingqi,bianhao,operator,operatetime,bisairiqi,url)
                        //VALUES(?title, ?result,?zhudui,?kedui,?xingqi,?bianhao,?operator,?operatetime,?bisairiqi,?url)
                        //";
                        string sqlStr =
                            @"INSERT INTO soccer_single(riqi,bianhao,creator,createtime)
SELECT ?riqi,?bianhao,?creator,?createtime
FROM DUAL WHERE NOT EXISTS(SELECT * FROM soccer_single WHERE riqi = ?riqi AND bianhao = ?bianhao);
";

                        mySqlCom.CommandText = sqlStr;
                        mySqlCom.Parameters.AddWithValue("?riqi", result.riqi);
                        mySqlCom.Parameters.AddWithValue("?bianhao", result.bianhao);
                        mySqlCom.Parameters.AddWithValue("?creator", result.creator);
                        mySqlCom.Parameters.AddWithValue("?createtime", result.createtime);
                        Convert.ToInt32(mySqlCom.ExecuteScalar());
                    }
                }
            }
        }


        //更新单场足球的赔率排名情况
        public void UpdateSoccer_Single(int spfPaiming, int rqspfPaiming, string riqi, string bianhao)
        {
            String sqlTemplete = "update soccer_single set spfpaiming = {0},rqspfpaiming = {1} where riqi = '{2}' and bianhao = '{3}'; ";
            String sql = string.Format(sqlTemplete,spfPaiming,rqspfPaiming,riqi,bianhao);
            SQLHelper.ExecuteNonQuery(sql);
        }

        //获得还未更新排名情况的单场比赛情况
        public DataTable GetNotHandledSingle()
        {
            String sql = @"select k.* from soccer_single as s inner join kaijiang as k
                            on s.riqi = k.riqi and s.bianhao = k.bianhao
                            where s.spfpaiming is null
                            or s.rqspfpaiming is null ;";

            return SQLHelper.GetDataTable(sql);
        }

        //获取单场比赛表数据
        public DataTable GetAllSingle()
        {
            string sql = "select * from soccer_single order by riqi, bianhao;";
            return SQLHelper.GetDataTable(sql);

        }
    }
}
