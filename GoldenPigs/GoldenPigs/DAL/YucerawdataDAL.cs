using GoldenPigs.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPigs.DAL
{
    public class YucerawdataDAL
    {
        public void InsertYucerawdata(Yucerawdata data)
        {
            int total = 0;
            string conStr = "server=localhost;User Id=root;database=aicai;" +
                           "Password=root;Character Set=utf8;";
            using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
            {
                mySqlCon.Open();
                int recordCount = 0;
                using (MySqlCommand myCountCom = mySqlCon.CreateCommand())
                {
                    //有可能存在p2没有数据的情况，这时候只需要通过url进行比较就对了
                    if (string.IsNullOrEmpty(data.P2))
                    {
                        string sqlCountStr = @"
select count(*) from yuce_rawdata where url = ?url;";
                        myCountCom.CommandText = sqlCountStr;
                        myCountCom.Parameters.AddWithValue("?url", data.Url);
                        object obj = myCountCom.ExecuteScalar();
                        recordCount = Convert.ToInt32(obj);
                    }
                    else
                    {
                        string sqlCountStr = @"
select count(*) from yuce_rawdata where url = ?url and p2 = ?p2;";
                        myCountCom.CommandText = sqlCountStr;
                        myCountCom.Parameters.AddWithValue("?url", data.Url);
                        myCountCom.Parameters.AddWithValue("?p2", data.P2);
                        object obj = myCountCom.ExecuteScalar();
                        recordCount = Convert.ToInt32(obj);
                    }
                   
                }
                if (recordCount == 0)
                {
                    using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                    {
                        //这里需要注意的是在 Sql 语句中有一个参数是 ?BlogsName 
                        //很明显，这个参数有点古怪，和我们一直使用的 @ 不一样， 
                        //这是因为在 MySql 以前的提供的 .NET Connector 中 
                        //都是采用 ? 来标志一个参数的，而现在的 .NET Connector 呢， 
                        //您既可以采用 ? 来标志一个参数，您也可以使用一个 @ 符号来标志一个参数 
                        //新版的 .NET Connector 对此都是支持的 
                        string sqlStr = @"
insert into yuce_rawdata(caizhongtype,publishdate,bisaishijian,title,url,p1,p2,p3,p4,p5,p6,p7,p8,operator,operatetime)
values(?caizhongtype,?publishdate,?bisaishijian,?title,?url,?p1,?p2,?p3,?p4,?p5,?p6,?p7,?p8,?operator,?operatetime)
";

                        mySqlCom.CommandText = sqlStr;
                        mySqlCom.Parameters.AddWithValue("?caizhongtype", data.Caizhongtype);
                        mySqlCom.Parameters.AddWithValue("?publishdate", data.Publishdate);
                        mySqlCom.Parameters.AddWithValue("?bisaishijian", data.Bisaishijian);
                        mySqlCom.Parameters.AddWithValue("?title", data.Title);
                        mySqlCom.Parameters.AddWithValue("?url", data.Url);
                        mySqlCom.Parameters.AddWithValue("?p1", data.P1);
                        mySqlCom.Parameters.AddWithValue("?p2", data.P2);
                        mySqlCom.Parameters.AddWithValue("?p3", data.P3);
                        mySqlCom.Parameters.AddWithValue("?p4", data.P4);
                        mySqlCom.Parameters.AddWithValue("?p5", data.P5);
                        mySqlCom.Parameters.AddWithValue("?p6", data.P6);
                        mySqlCom.Parameters.AddWithValue("?p7", data.P7);
                        mySqlCom.Parameters.AddWithValue("?p8", data.P8);
                        mySqlCom.Parameters.AddWithValue("?operator", data.Operator);
                        mySqlCom.Parameters.AddWithValue("?operatetime", data.OperateTime);

                        mySqlCom.ExecuteScalar();
                    }
                }
                
            }

           
        }

        public DataSet GetYucerawdata()
        {
            //select caizhongtype, p2,p3,p8 from yuce_rawdata;
            string conStr = "server=localhost;User Id=root;database=aicai;" +
                          "Password=root;Character Set=utf8;";
            DataSet ds = new DataSet();
            using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
            {
                mySqlCon.Open();
                using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                {
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    string sqlStr = @"select url,caizhongtype,title, p2,p3,p8 from yuce_rawdata;";
                  
                    mySqlCom.CommandText = sqlStr;
                   
                    try
                    {
                        da.SelectCommand = mySqlCom;
                        da.Fill(ds);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                }
            }
            return ds;
        }

         public void InsertYuceanalysis(Yuceanalysis data)
        {
            int total = 0;
            string conStr = "server=localhost;User Id=root;database=aicai;" +
                           "Password=root;Character Set=utf8;";
            using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
            {
                mySqlCon.Open();
                int recordCount = 0;
                using (MySqlCommand myCountCom = mySqlCon.CreateCommand())
                {
                    string sqlCountStr = @"
select count(*) from yuce_rawdata_analysis
where zhudui = ?zhudui and kedui = ?kedui and url=?url;";
                    myCountCom.CommandText = sqlCountStr;
                    myCountCom.Parameters.AddWithValue("?url", data.Url);
                    myCountCom.Parameters.AddWithValue("?zhudui", data.Zhudui);
                    myCountCom.Parameters.AddWithValue("?kedui", data.Kedui);
                    object obj = myCountCom.ExecuteScalar();
                    recordCount = Convert.ToInt32(obj);
                }
                if (recordCount == 0)
                {
                    using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                    {
                        //这里需要注意的是在 Sql 语句中有一个参数是 ?BlogsName 
                        //很明显，这个参数有点古怪，和我们一直使用的 @ 不一样， 
                        //这是因为在 MySql 以前的提供的 .NET Connector 中 
                        //都是采用 ? 来标志一个参数的，而现在的 .NET Connector 呢， 
                        //您既可以采用 ? 来标志一个参数，您也可以使用一个 @ 符号来标志一个参数 
                        //新版的 .NET Connector 对此都是支持的 
                        string sqlStr = @"

insert into yuce_rawdata_analysis(url,zhudui,kedui,yucetype,yuceren,yucespf,hasrangqiu,rangqiushu,bisaishijian,touzhushijian,bianhao,operator,operatetime)
values(?url,?zhudui,?kedui,?yucetype,?yuceren,?yucespf,?hasrangqiu,?rangqiushu,?bisaishijian,?touzhushijian,?bianhao,?operator,?operatetime)
";

                        mySqlCom.CommandText = sqlStr;
                        mySqlCom.Parameters.AddWithValue("?url", data.Url);
                        mySqlCom.Parameters.AddWithValue("?zhudui", data.Zhudui);
                        mySqlCom.Parameters.AddWithValue("?kedui", data.Kedui);
                        mySqlCom.Parameters.AddWithValue("?yucetype", data.Yucetype);
                        mySqlCom.Parameters.AddWithValue("?yuceren", data.Yuceren);
                        mySqlCom.Parameters.AddWithValue("?yucespf", data.Yucespf);
                        mySqlCom.Parameters.AddWithValue("?hasrangqiu", data.Hasrangqiu);
                        mySqlCom.Parameters.AddWithValue("?rangqiushu", data.Rangqiushu);
                        mySqlCom.Parameters.AddWithValue("?bisaishijian", data.Bisaishijian);
                        mySqlCom.Parameters.AddWithValue("?touzhushijian", data.Touzhushijian);
                        mySqlCom.Parameters.AddWithValue("?bianhao", data.Bianhao);
                        mySqlCom.Parameters.AddWithValue("?operator", data.Operator);
                        mySqlCom.Parameters.AddWithValue("?operatetime", data.OperateTime);

                        mySqlCom.ExecuteScalar();
                    }
                }
            
            }

           
        }
         public DataSet GetAllYuceAnalysis()
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
                     string sqlStr = @"select * from yuce_rawdata_analysis order by touzhushijian desc, zhudui asc;";

                     mySqlCom.CommandText = sqlStr;
                     try
                     {
                         da.SelectCommand = mySqlCom;
                         da.Fill(ds);
                     }
                     catch (Exception ex)
                     {
                         Console.WriteLine(ex.Message);
                     }


                 }
             }
             return ds;
        }
         public DataSet GetYucerawdataAnalysisHasMingRen(DateTime touzhushijian)
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
                     //string sqlStr = @"select * from yuce_rawdata_analysis where touzhushijian = ?touzhushijian;";

                     string sqlStr = @"
SELECT
	y.*,
	ifnull(m1.TargetName, y.Zhudui) AS zhuduireal,
	ifnull(m2.TargetName, y.Kedui) AS keduireal
FROM
	yuce_rawdata_analysis y
LEFT JOIN qiuduimapping m1 ON y.Zhudui = m1.OriginName
LEFT JOIN qiuduimapping m2 ON y.Kedui = m2.OriginName
where y.Touzhushijian = ?touzhushijian
and y.yuceren != ''
order by zhuduireal ;
";

                     mySqlCom.CommandText = sqlStr;
                     mySqlCom.Parameters.AddWithValue("?touzhushijian", touzhushijian);
                     try
                     {
                         da.SelectCommand = mySqlCom;
                         da.Fill(ds);
                     }
                     catch (Exception ex)
                     {
                         Console.WriteLine(ex.Message);
                     }
                 }
             }
             return ds;
         }

         public DataSet GetYucerawdataAnalysis(DateTime touzhushijian)
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
                     //string sqlStr = @"select * from yuce_rawdata_analysis where touzhushijian = ?touzhushijian;";

                     string sqlStr = @"
SELECT
	y.*,
	ifnull(m1.TargetName, y.Zhudui) AS zhuduireal,
	ifnull(m2.TargetName, y.Kedui) AS keduireal
FROM
	yuce_rawdata_analysis y
LEFT JOIN qiuduimapping m1 ON y.Zhudui = m1.OriginName
LEFT JOIN qiuduimapping m2 ON y.Kedui = m2.OriginName
where y.Touzhushijian = ?touzhushijian
order by zhuduireal ;
";

                     mySqlCom.CommandText = sqlStr;
                     mySqlCom.Parameters.AddWithValue("?touzhushijian", touzhushijian);
                     try
                     {
                         da.SelectCommand = mySqlCom;
                         da.Fill(ds);
                     }
                     catch (Exception ex)
                     {
                         Console.WriteLine(ex.Message);
                     }
                 }
             }
             return ds;
         }

         public  void UpdateZhongjiangResult(DateTime touzhushijian)
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
	y.*,
	ifnull(m1.TargetName, y.Zhudui) AS zhuduireal,
	ifnull(m2.TargetName, y.Kedui) AS keduireal
FROM
	yuce_rawdata_analysis y
LEFT JOIN qiuduimapping m1 ON y.Zhudui = m1.OriginName
LEFT JOIN qiuduimapping m2 ON y.Kedui = m2.OriginName

where y.Touzhushijian = ?touzhushijian
 ";

                     //--and y.Yucetype = '[竞彩足球]'
                    
                     mySqlCom.CommandText = sql;
                     mySqlCom.Parameters.AddWithValue("?touzhushijian", touzhushijian);
                     da.SelectCommand = mySqlCom;
                     da.Fill(ds);
                 }
             }
             foreach (DataRow row in ds.Tables[0].Rows)
             {
                 string id = row["id"].ToString();
                 string zhudui = row["zhuduireal"].ToString();
                 string kedui = row["keduireal"].ToString();
                 string yucespf = row["yucespf"].ToString();
                 string hasrangqiu = row["hasrangqiu"].ToString();
                 string rangqiushu = row["rangqiushu"].ToString();
                 string touzhushijian2 = row["touzhushijian"].ToString();

                 Kaijiang kaijiang = new KaijiangDAL().GetKaijiang(touzhushijian2, zhudui, kedui);
                 if (kaijiang != null)
                 {
                     int lucky = 0;
                     if (hasrangqiu.Equals("0"))
                     {
                        
                         if (yucespf.IndexOf(kaijiang.SpfResult)!= -1)
                         {
                             lucky = 1;
                         }
                     }
                     else
                     {
                         if (yucespf.IndexOf(kaijiang.RqspfResult) != -1)
                         {
                             lucky = 1;
                         }
                     }

                     UpdateYuceDetail(id,lucky,kaijiang);
                
                 }


             }

         }

         private void UpdateYuceDetail(string id, int lucky, Kaijiang kaijiang)
         {
             string conStr = "server=localhost;User Id=root;database=aicai;" +
                          "Password=root;Character Set=utf8;";
             
             using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
             {
                 mySqlCon.Open();
                 using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                 {
                    
                     string sql = @"
UPDATE yuce_rawdata_analysis
SET bianhao = ?bianhao,
 lucky = ?lucky,
 SpfResult = ?SpfResult,
 spfsp = ?Spfsp,
 RqspfResult = ?RqspfResult,
 rqspfsp = ?RqspfSp,
 rqshu = ?rqshu
WHERE
	id = ?ID ";

                     mySqlCom.CommandText = sql;
                     mySqlCom.Parameters.AddWithValue("?bianhao", kaijiang.Bianhao);
                     mySqlCom.Parameters.AddWithValue("?lucky", lucky);
                     mySqlCom.Parameters.AddWithValue("?SpfResult", kaijiang.SpfResult);
                     mySqlCom.Parameters.AddWithValue("?Spfsp", kaijiang.SpfSp);
                     mySqlCom.Parameters.AddWithValue("?RqspfResult", kaijiang.RqspfResult);
                     mySqlCom.Parameters.AddWithValue("?RqspfSp", kaijiang.RqspfSp);
                     mySqlCom.Parameters.AddWithValue("?rqshu", kaijiang.RqShu);
                     mySqlCom.Parameters.AddWithValue("?ID", id);
                     mySqlCom.ExecuteScalar();
                 }
             }
         }


         public DataSet GetYucePeilv(DateTime touzhushijian)
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
                     //string sqlStr = @"select * from yuce_rawdata_analysis where touzhushijian = ?touzhushijian;";

                     string sqlStr = @"
SELECT
	*
FROM
	(
		SELECT
			y.*, ifnull(m1.TargetName, y.Zhudui) AS zhuduireal,
			ifnull(m2.TargetName, y.Kedui) AS keduireal
		FROM
			yuce_rawdata_analysis y
		LEFT JOIN qiuduimapping m1 ON y.Zhudui = m1.OriginName
		LEFT JOIN qiuduimapping m2 ON y.Kedui = m2.OriginName
		WHERE
			y.Touzhushijian = ?touzhushijian
	) yreal
LEFT JOIN peilv ON yreal.zhuduireal = peilv.Zhudui
AND yreal.keduireal = peilv.Kedui
AND peilv.Riqi = ?riqi

LEFT JOIN kaijiang ON yreal.zhuduireal = kaijiang.Zhudui
AND yreal.keduireal = kaijiang.Kedui
AND kaijiang.riqi = ?riqi
WHERE
	(
		yreal.rangqiushu = '0'
		AND peilv.rangqiu = '0'
	)
OR (
	yreal.rangqiushu != '0'
	AND peilv.rangqiu != '0'
)
";

                     mySqlCom.CommandText = sqlStr;
                     mySqlCom.Parameters.AddWithValue("?touzhushijian", touzhushijian);
                     mySqlCom.Parameters.AddWithValue("?riqi", touzhushijian.ToString("yyyy-MM-dd"));
                     try
                     {
                         da.SelectCommand = mySqlCom;
                         da.Fill(ds);
                     }
                     catch (Exception ex)
                     {
                         Console.WriteLine(ex.Message);
                     }
                 }
             }
             return ds;
         }
    }
}
