using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldenPigs.Entity;
using MySql.Data.MySqlClient;

namespace GoldenPigs.DAL
{
    public class AppYuceDAL
    {
       


        public void InsertAppYuce310List(List<Yuce310> results)
        {
            int total = 0;
            string conStr = "server=localhost;User Id=root;database=aicai;" +
                            "Password=root;Character Set=utf8;";
            using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
            {
                mySqlCon.Open();
                foreach (Yuce310 result in results)
                {
                    using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                    {
                        //                        string sqlStr = @"
                        //
                        //INSERT INTO basketyuce(title, result,zhudui,kedui,xingqi,bianhao,operator,operatetime,bisairiqi,url)
                        //VALUES(?title, ?result,?zhudui,?kedui,?xingqi,?bianhao,?operator,?operatetime,?bisairiqi,?url)
                        //";
                        string sqlStr =
                            @"insert into yuce_samuel(name,title,yuceriqi,url,riqi,bianhao,zhudui,kedui,bqctuijian,bifentuijian,beidanrqshu,beidanspfresult,spfresult,rqshu,rqspf,lucky)
SELECT ?name,?title,?yuceriqi,?url,?riqi,?bianhao,?zhudui,?kedui,?bqctuijian,?bifentuijian,?beidanrqshu,?beidanspfresult,?spfresult,?rqshu,?rqspf,?lucky
FROM DUAL WHERE NOT EXISTS (SELECT * FROM yuce_samuel WHERE url = ?url)
";

                        mySqlCom.CommandText = sqlStr;
                        mySqlCom.Parameters.AddWithValue("?name", result.name);
                        mySqlCom.Parameters.AddWithValue("?title", result.title);
                        mySqlCom.Parameters.AddWithValue("?url", result.url);
                        mySqlCom.Parameters.AddWithValue("?riqi", result.riqi);
                        mySqlCom.Parameters.AddWithValue("?yuceriqi", result.yuceriqi);
                        mySqlCom.Parameters.AddWithValue("?bianhao", result.bianhao);
                        mySqlCom.Parameters.AddWithValue("?zhudui", result.zhudui);
                        mySqlCom.Parameters.AddWithValue("?kedui", result.kedui);

                        mySqlCom.Parameters.AddWithValue("?bqctuijian", result.bqctuijian);
                        mySqlCom.Parameters.AddWithValue("?bifentuijian", result.bifentuijian);
                        mySqlCom.Parameters.AddWithValue("?beidanrqshu", result.beidanrqshu);
                        mySqlCom.Parameters.AddWithValue("?beidanspfresult", result.beidanspfresult);
                        mySqlCom.Parameters.AddWithValue("?spfresult", result.spfresult);
                        mySqlCom.Parameters.AddWithValue("?rqshu", result.rqshu);
                        mySqlCom.Parameters.AddWithValue("?rqspf", result.rqspf);
                        mySqlCom.Parameters.AddWithValue("?lucky", result.lucky);
                        total = Convert.ToInt32(mySqlCom.ExecuteScalar());
                    }
                }
            }

            //return total == 0 ? true : false;

        }
        public void InsertAppYuceList(List<AppYuce> results)
        {
            int total = 0;
            string conStr = "server=localhost;User Id=root;database=aicai;" +
                            "Password=root;Character Set=utf8;";
            using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
            {
                mySqlCon.Open();
                foreach (AppYuce result in results)
                {
                    using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                    {
                        //                        string sqlStr = @"
                        //
                        //INSERT INTO basketyuce(title, result,zhudui,kedui,xingqi,bianhao,operator,operatetime,bisairiqi,url)
                        //VALUES(?title, ?result,?zhudui,?kedui,?xingqi,?bianhao,?operator,?operatetime,?bisairiqi,?url)
                        //";
                        string sqlStr =
                            @"INSERT INTO yuce_app(title,url,riqi,WEEKDAY,bianhao,zhudui,kedui,spfresult,author,liansai,realresult,realresultsp,lucky,operator,operatetime,rangqiushu,spfrawresult)
SELECT ?title,?url,?riqi,?weekday,?bianhao,?zhudui,?kedui,?spfresult,?author,?liansai,?realresult,?realresultsp,?lucky,?operator,?operatetime,?rangqiushu,?spfrawresult
FROM DUAL WHERE NOT EXISTS (SELECT * FROM yuce_app WHERE url = ?url)
";

                        mySqlCom.CommandText = sqlStr;
                        mySqlCom.Parameters.AddWithValue("?title", result.title);
                        mySqlCom.Parameters.AddWithValue("?url", result.url);
                        mySqlCom.Parameters.AddWithValue("?riqi", result.riqi);
                        mySqlCom.Parameters.AddWithValue("?weekday", result.weekday);
                        mySqlCom.Parameters.AddWithValue("?bianhao", result.bianhao);
                        mySqlCom.Parameters.AddWithValue("?zhudui", result.zhudui);
                        mySqlCom.Parameters.AddWithValue("?kedui", result.kedui);
                        mySqlCom.Parameters.AddWithValue("?spfresult", result.spfresult);
                        mySqlCom.Parameters.AddWithValue("?author", result.author);
                        mySqlCom.Parameters.AddWithValue("?liansai", result.liansai);
                        mySqlCom.Parameters.AddWithValue("?realresult", result.realresult);
                        mySqlCom.Parameters.AddWithValue("?realresultsp", result.realresultsp);
                        mySqlCom.Parameters.AddWithValue("?lucky", result.lucky);
                        mySqlCom.Parameters.AddWithValue("?rangqiushu", result.rangqiushu);
                        mySqlCom.Parameters.AddWithValue("?spfrawresult", result.spfrawresult);
                        mySqlCom.Parameters.AddWithValue("?operator", result.operPerson);
                        mySqlCom.Parameters.AddWithValue("?operatetime", result.operateTime);
                        total = Convert.ToInt32(mySqlCom.ExecuteScalar());
                    }
                }
            }

            //return total == 0 ? true : false;
        }

        public void InsertBadUrl(AppYuceBadUrl result)
        {
            string conStr = "server=localhost;User Id=root;database=aicai;" +
                            "Password=root;Character Set=utf8;";
            using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
            {
                mySqlCon.Open();
                using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                {
                    //                        string sqlStr = @"
                    //
                    //INSERT INTO basketyuce(title, result,zhudui,kedui,xingqi,bianhao,operator,operatetime,bisairiqi,url)
                    //VALUES(?title, ?result,?zhudui,?kedui,?xingqi,?bianhao,?operator,?operatetime,?bisairiqi,?url)
                    //";
                    string sqlStr =
                        @"INSERT INTO yuce_bad_url(title,url,prefix,creator,createtime)
SELECT ?title,?url,?prefix,?creator,?createtime FROM DUAL
WHERE NOT EXISTS (SELECT * FROM yuce_bad_url WHERE url=?url)
";

                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?title", result.title);
                    mySqlCom.Parameters.AddWithValue("?url", result.url);
                    mySqlCom.Parameters.AddWithValue("?prefix", result.prefix);
                    mySqlCom.Parameters.AddWithValue("?creator", result.creator);
                    mySqlCom.Parameters.AddWithValue("?createtime", result.createtime);
                    mySqlCom.ExecuteScalar();
                }
            }
        }

        public DataSet GetYuceByRiqi(String riqi)
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
SELECT riqi,bianhao,liansai,zhudui,kedui,spfresult,author,realresult,realresultsp,lucky,url,title FROM yuce_app where riqi = ?riqi order by bianhao;";

                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);
                }
            }
            return ds;
        }
        public DataSet GetAllYuce()
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
SELECT * FROM yuce_app  ORDER BY riqi asc,bianhao;";

                    mySqlCom.CommandText = sqlStr;

                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);
                }
            }
            return ds;
        }


        public void UpdateZhongjiangResult(DateTime touzhushijian)
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
                    string sql = @"
SELECT
	*
FROM
	yuce_app

where riqi = ?touzhushijian
 ";

                    //--and y.Yucetype = '[竞彩足球]'

                    mySqlCom.CommandText = sql;
                    mySqlCom.Parameters.AddWithValue("?touzhushijian", touzhushijian.ToString("yyyy-MM-dd"));
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);
                }

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string id = row["id"].ToString();
                    string riqi = row["riqi"].ToString();
                    string bianhao = row["bianhao"].ToString();
                    string yucespf = row["spfresult"].ToString();
                    string rangqiushu = row["rangqiushu"].ToString();
                    Kaijiang kaijiang = new KaijiangDAL().GetKaijangSpfResult(riqi, bianhao);
                    if (kaijiang != null)
                    {
                        int lucky = 0;
                        if (String.IsNullOrEmpty(yucespf))
                        {
                            continue;
                            
                        }

                        if (!string.IsNullOrEmpty(kaijiang.SpfResult))
                        {

                            if (rangqiushu == "0")
                            {
                                if (yucespf.IndexOf(kaijiang.SpfResult) != -1)
                                {
                                    lucky = 1;
                                }
                                else
                                {
                                    lucky = 2; //不中奖
                                }
                            }
                            else
                            {
                                if (yucespf.IndexOf(kaijiang.RqspfResult) != -1)
                                {
                                    lucky = 1;
                                }
                                else
                                {
                                    lucky = 2; //不中奖
                                }
                            }    
                            UpdateYuceDetail(id, lucky, kaijiang,rangqiushu);
                        }
                    }
                }
            }
        }

        private void UpdateYuceDetail(string id, int lucky, Kaijiang kaijiang, string rangqiushu)
        {
            string conStr = "server=localhost;User Id=root;database=aicai;" +
                            "Password=root;Character Set=utf8;";

            using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
            {
                mySqlCon.Open();
                using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                {
                    string sql = @"
UPDATE yuce_app
SET 
 lucky = ?lucky,
 realresult = ?SpfResult,
 realresultsp = ?Spfsp,
 operatetime = ?operatetime
WHERE
	id = ?ID ";

                    mySqlCom.CommandText = sql;
                   
                    mySqlCom.Parameters.AddWithValue("?lucky", lucky);
                    if (rangqiushu == "0")
                    {
                        mySqlCom.Parameters.AddWithValue("?SpfResult", kaijiang.SpfResult);
                        mySqlCom.Parameters.AddWithValue("?Spfsp", kaijiang.SpfSp);
                    }
                    else
                    {
                        mySqlCom.Parameters.AddWithValue("?SpfResult", kaijiang.RqspfResult);
                        mySqlCom.Parameters.AddWithValue("?Spfsp", kaijiang.RqspfSp);
                    }
                    mySqlCom.Parameters.AddWithValue("?operatetime", DateTime.Now);
                    mySqlCom.Parameters.AddWithValue("?ID", id);

                    
                    mySqlCom.ExecuteScalar();
                }
            }
        }

        public DataSet GetNullBianhaoData()
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
select id,riqi,zhudui,kedui from yuce_samuel where bianhao is null or bianhao = '';
";
                    mySqlCom.CommandText = sqlStr;
                    //mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);
                }
            }
            return ds;
        }
        public DataSet GetYuce310NullLuckyData()
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
select id,riqi,bianhao,zhudui,kedui,bqctuijian,bifentuijian,spfresult,rqshu,rqspf from yuce_samuel where lucky is null;
";
                    mySqlCom.CommandText = sqlStr;
                    //mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);
                }
            }
            return ds;
        }

        public void UpdateYuce310Lucky(string id, string lucky,string rqlucky,string bifenlucky,string bqclucky,Kaijiang kaijiang)
        {
            string conStr = "server=localhost;User Id=root;database=aicai;" +
                            "Password=root;Character Set=utf8;";

            using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
            {
                mySqlCon.Open();
                using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                {
                    string sql = @"
UPDATE yuce_samuel
SET 
 lucky = ?lucky,
 rqlucky = ?rqlucky,
 bifenlucky = ?bifenlucky,
 bqclucky = ?bqclucky,
 bqcresult = ?bqcresult,
 bifenresult = ?bifenresult,
 spfresultreal = ?spfresultreal,
 rqspfreal = ?rqspfreal
WHERE
	id = ?ID ";

                    mySqlCom.CommandText = sql;


                    mySqlCom.Parameters.AddWithValue("?lucky", lucky);
                    mySqlCom.Parameters.AddWithValue("?rqlucky", rqlucky);
                    mySqlCom.Parameters.AddWithValue("?bifenlucky", bifenlucky);
                    mySqlCom.Parameters.AddWithValue("?bqclucky", bqclucky);
                    mySqlCom.Parameters.AddWithValue("?bqcresult", kaijiang.BqcResult);
                    mySqlCom.Parameters.AddWithValue("?bifenresult", kaijiang.QcbfResult);
                    mySqlCom.Parameters.AddWithValue("?spfresultreal", kaijiang.SpfResult);
                    mySqlCom.Parameters.AddWithValue("?rqspfreal", kaijiang.RqspfResult);
                    
                    mySqlCom.Parameters.AddWithValue("?ID", id);


                    mySqlCom.ExecuteScalar();
                }
            }
        }

        public void UpdateYuce310Bianhao(string id, string bianhao)
        {
            string conStr = "server=localhost;User Id=root;database=aicai;" +
                            "Password=root;Character Set=utf8;";

            using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
            {
                mySqlCon.Open();
                using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                {
                    string sql = @"
UPDATE yuce_samuel
SET 
 bianhao = ?bianhao
WHERE
	id = ?ID ";

                    mySqlCom.CommandText = sql;

                    
                    mySqlCom.Parameters.AddWithValue("?bianhao", bianhao);
                    mySqlCom.Parameters.AddWithValue("?ID", id);


                    mySqlCom.ExecuteScalar();
                }
            }
        }
    }
}