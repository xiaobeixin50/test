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
    public class XinshuiDAL
    {
    
         public void InsertXinshuiGaopeiWhenNotExist(Xinshui xinshui)
         {
             try
            {
                int total = 0;
                string conStr = "server=localhost;User Id=root;database=aicai;" +
                               "Password=root;Character Set=utf8;";
                using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
                {
                    mySqlCon.Open();
                    using (MySqlCommand existCom = mySqlCon.CreateCommand())
                    {
                        string existSql = @"select count(*) from xinshuituijian_gaopei where riqi = ?riqi and bianhao1 = ?bianhao1 and bianhao2=?bianhao2";
                        existCom.CommandText = existSql;
                        existCom.Parameters.AddWithValue("?riqi", xinshui.Riqi);
                        existCom.Parameters.AddWithValue("?bianhao1", xinshui.Bianhao1);
                        existCom.Parameters.AddWithValue("?bianhao2", xinshui.Bianhao2);
                        object obj = existCom.ExecuteScalar();
                        int resultCount = Convert.ToInt32(obj);
                        if (resultCount != 0)
                        {
                            return;
                        }

                    }
                    using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                    {
                        //这里需要注意的是在 Sql 语句中有一个参数是 ?BlogsName 
                        //很明显，这个参数有点古怪，和我们一直使用的 @ 不一样， 
                        //这是因为在 MySql 以前的提供的 .NET Connector 中 
                        //都是采用 ? 来标志一个参数的，而现在的 .NET Connector 呢， 
                        //您既可以采用 ? 来标志一个参数，您也可以使用一个 @ 符号来标志一个参数 
                        //新版的 .NET Connector 对此都是支持的 
                        string sqlStr = @"
insert into xinshuituijian_gaopei(
riqi,beishu,bianhao1,liansai1,zhudui1,kedui1,rangqiushu1,result1,bianhao2,liansai2,zhudui2,kedui2,rangqiushu2,result2,realresult1,realresult2,realresultsp1,realresultsp2,
lucky,jiangjin,exclude,operator,operatetime
)
values(
?riqi,?beishu,?bianhao1,?liansai1,?zhudui1,?kedui1,?rangqiushu1,?result1,?bianhao2,?liansai2,?zhudui2,
?kedui2,?rangqiushu2,?result2,?realresult1,?realresult2,?realresultsp1,?realresultsp2,?lucky,?jiangjin,?exclude,?operator,?operatetime
)
";
                        //"SELECT COUNT(*) FROM BlogsUsers WHERE BlogsName=?BlogsName";
                        mySqlCom.CommandText = sqlStr;
                        mySqlCom.Parameters.AddWithValue("?riqi", xinshui.Riqi);
                        mySqlCom.Parameters.AddWithValue("?beishu", xinshui.Beishu);
                        mySqlCom.Parameters.AddWithValue("?bianhao1", xinshui.Bianhao1);
                        mySqlCom.Parameters.AddWithValue("?liansai1", xinshui.Liansai1);
                        mySqlCom.Parameters.AddWithValue("?zhudui1", xinshui.Zhudui1);
                        mySqlCom.Parameters.AddWithValue("?kedui1", xinshui.Kedui1);
                        mySqlCom.Parameters.AddWithValue("?rangqiushu1", xinshui.Rangqiushu1);
                        mySqlCom.Parameters.AddWithValue("?result1", xinshui.Result1);
                        mySqlCom.Parameters.AddWithValue("?bianhao2", xinshui.Bianhao2);
                        mySqlCom.Parameters.AddWithValue("?liansai2", xinshui.Liansai2);
                        mySqlCom.Parameters.AddWithValue("?zhudui2", xinshui.Zhudui2);
                        mySqlCom.Parameters.AddWithValue("?kedui2", xinshui.Kedui2);
                        mySqlCom.Parameters.AddWithValue("?rangqiushu2", xinshui.Rangqiushu2);
                        mySqlCom.Parameters.AddWithValue("?result2", xinshui.Result2);
                        mySqlCom.Parameters.AddWithValue("?realresult1", xinshui.RealResult1);
                        mySqlCom.Parameters.AddWithValue("?realresult2", xinshui.RealResult2);
                        mySqlCom.Parameters.AddWithValue("?realresultsp1", xinshui.RealResultSp1);
                        mySqlCom.Parameters.AddWithValue("?realresultsp2", xinshui.RealResultSp2);
                        mySqlCom.Parameters.AddWithValue("?lucky", xinshui.Lucky);
                        mySqlCom.Parameters.AddWithValue("?jiangjin", xinshui.Jiangjin);
                        mySqlCom.Parameters.AddWithValue("?exclude", xinshui.Exclude);
                        mySqlCom.Parameters.AddWithValue("?operator", xinshui.Operator);
                        mySqlCom.Parameters.AddWithValue("?operatetime", xinshui.OperateTime);
                        mySqlCom.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
         }
        public void InsertXinshuiWhenNotExist(Xinshui xinshui)
        {
            try
            {
                int total = 0;
                string conStr = "server=localhost;User Id=root;database=aicai;" +
                               "Password=root;Character Set=utf8;";
                using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
                {
                    mySqlCon.Open();
                    using (MySqlCommand existCom = mySqlCon.CreateCommand())
                    {
                        string existSql = @"select count(*) from xinshuituijian where riqi = ?riqi and bianhao1 = ?bianhao1 and bianhao2=?bianhao2";
                        existCom.CommandText = existSql;
                        existCom.Parameters.AddWithValue("?riqi", xinshui.Riqi);
                        existCom.Parameters.AddWithValue("?bianhao1", xinshui.Bianhao1);
                        existCom.Parameters.AddWithValue("?bianhao2", xinshui.Bianhao2);
                        object obj = existCom.ExecuteScalar();
                        int resultCount = Convert.ToInt32(obj);
                        if (resultCount != 0)
                        {
                            return;
                        }

                    }
                    using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                    {
                        //这里需要注意的是在 Sql 语句中有一个参数是 ?BlogsName 
                        //很明显，这个参数有点古怪，和我们一直使用的 @ 不一样， 
                        //这是因为在 MySql 以前的提供的 .NET Connector 中 
                        //都是采用 ? 来标志一个参数的，而现在的 .NET Connector 呢， 
                        //您既可以采用 ? 来标志一个参数，您也可以使用一个 @ 符号来标志一个参数 
                        //新版的 .NET Connector 对此都是支持的 
                        string sqlStr = @"
insert into xinshuituijian(
riqi,beishu,bianhao1,liansai1,zhudui1,kedui1,rangqiushu1,result1,bianhao2,liansai2,zhudui2,kedui2,rangqiushu2,result2,realresult1,realresult2,realresultsp1,realresultsp2,
lucky,jiangjin,exclude,operator,operatetime
)
values(
?riqi,?beishu,?bianhao1,?liansai1,?zhudui1,?kedui1,?rangqiushu1,?result1,?bianhao2,?liansai2,?zhudui2,
?kedui2,?rangqiushu2,?result2,?realresult1,?realresult2,?realresultsp1,?realresultsp2,?lucky,?jiangjin,?exclude,?operator,?operatetime
)
";
                        //"SELECT COUNT(*) FROM BlogsUsers WHERE BlogsName=?BlogsName";
                        mySqlCom.CommandText = sqlStr;
                        mySqlCom.Parameters.AddWithValue("?riqi", xinshui.Riqi);
                        mySqlCom.Parameters.AddWithValue("?beishu", xinshui.Beishu);
                        mySqlCom.Parameters.AddWithValue("?bianhao1", xinshui.Bianhao1);
                        mySqlCom.Parameters.AddWithValue("?liansai1", xinshui.Liansai1);
                        mySqlCom.Parameters.AddWithValue("?zhudui1", xinshui.Zhudui1);
                        mySqlCom.Parameters.AddWithValue("?kedui1", xinshui.Kedui1);
                        mySqlCom.Parameters.AddWithValue("?rangqiushu1", xinshui.Rangqiushu1);
                        mySqlCom.Parameters.AddWithValue("?result1", xinshui.Result1);
                        mySqlCom.Parameters.AddWithValue("?bianhao2", xinshui.Bianhao2);
                        mySqlCom.Parameters.AddWithValue("?liansai2", xinshui.Liansai2);
                        mySqlCom.Parameters.AddWithValue("?zhudui2", xinshui.Zhudui2);
                        mySqlCom.Parameters.AddWithValue("?kedui2", xinshui.Kedui2);
                        mySqlCom.Parameters.AddWithValue("?rangqiushu2", xinshui.Rangqiushu2);
                        mySqlCom.Parameters.AddWithValue("?result2", xinshui.Result2);
                        mySqlCom.Parameters.AddWithValue("?realresult1", xinshui.RealResult1);
                        mySqlCom.Parameters.AddWithValue("?realresult2", xinshui.RealResult2);
                        mySqlCom.Parameters.AddWithValue("?realresultsp1", xinshui.RealResultSp1);
                        mySqlCom.Parameters.AddWithValue("?realresultsp2", xinshui.RealResultSp2);
                        mySqlCom.Parameters.AddWithValue("?lucky", xinshui.Lucky);
                        mySqlCom.Parameters.AddWithValue("?jiangjin", xinshui.Jiangjin);
                        mySqlCom.Parameters.AddWithValue("?exclude", xinshui.Exclude);
                        mySqlCom.Parameters.AddWithValue("?operator", xinshui.Operator);
                        mySqlCom.Parameters.AddWithValue("?operatetime", xinshui.OperateTime);
                        mySqlCom.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }

        public  void InsertXinshui(Xinshui xinshui)
        {
            try
            {
                int total = 0;
                string conStr = "server=localhost;User Id=root;database=aicai;" +
                               "Password=root;Character Set=utf8;";
                using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
                {
                    mySqlCon.Open();
                    using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                    {
                        //这里需要注意的是在 Sql 语句中有一个参数是 ?BlogsName 
                        //很明显，这个参数有点古怪，和我们一直使用的 @ 不一样， 
                        //这是因为在 MySql 以前的提供的 .NET Connector 中 
                        //都是采用 ? 来标志一个参数的，而现在的 .NET Connector 呢， 
                        //您既可以采用 ? 来标志一个参数，您也可以使用一个 @ 符号来标志一个参数 
                        //新版的 .NET Connector 对此都是支持的 
                        string sqlStr = @"
insert into xinshuituijian(
riqi,beishu,bianhao1,liansai1,zhudui1,kedui1,rangqiushu1,result1,bianhao2,liansai2,zhudui2,kedui2,rangqiushu2,result2,realresult1,realresult2,realresultsp1,realresultsp2,
lucky,jiangjin,exclude,operator,operatetime
)
values(
?riqi,?beishu,?bianhao1,?liansai1,?zhudui1,?kedui1,?rangqiushu1,?result1,?bianhao2,?liansai2,?zhudui2,
?kedui2,?rangqiushu2,?result2,?realresult1,?realresult2,?realresultsp1,?realresultsp2,?lucky,?jiangjin,?exclude,?operator,?operatetime
)
";
                        //"SELECT COUNT(*) FROM BlogsUsers WHERE BlogsName=?BlogsName";
                        mySqlCom.CommandText = sqlStr;
                        mySqlCom.Parameters.AddWithValue("?riqi", xinshui.Riqi);
                        mySqlCom.Parameters.AddWithValue("?beishu", xinshui.Beishu);
                        mySqlCom.Parameters.AddWithValue("?bianhao1", xinshui.Bianhao1);
                        mySqlCom.Parameters.AddWithValue("?liansai1", xinshui.Liansai1);
                        mySqlCom.Parameters.AddWithValue("?zhudui1", xinshui.Zhudui1);
                        mySqlCom.Parameters.AddWithValue("?kedui1", xinshui.Kedui1);
                        mySqlCom.Parameters.AddWithValue("?rangqiushu1", xinshui.Rangqiushu1);
                        mySqlCom.Parameters.AddWithValue("?result1", xinshui.Result1);
                        mySqlCom.Parameters.AddWithValue("?bianhao2", xinshui.Bianhao2);
                        mySqlCom.Parameters.AddWithValue("?liansai2", xinshui.Liansai2);
                        mySqlCom.Parameters.AddWithValue("?zhudui2", xinshui.Zhudui2);
                        mySqlCom.Parameters.AddWithValue("?kedui2", xinshui.Kedui2);
                        mySqlCom.Parameters.AddWithValue("?rangqiushu2", xinshui.Rangqiushu2);
                        mySqlCom.Parameters.AddWithValue("?result2", xinshui.Result2);
                        mySqlCom.Parameters.AddWithValue("?realresult1", xinshui.RealResult1);
                        mySqlCom.Parameters.AddWithValue("?realresult2", xinshui.RealResult2);
                        mySqlCom.Parameters.AddWithValue("?realresultsp1", xinshui.RealResultSp1);
                        mySqlCom.Parameters.AddWithValue("?realresultsp2", xinshui.RealResultSp2);
                        mySqlCom.Parameters.AddWithValue("?lucky", xinshui.Lucky);
                        mySqlCom.Parameters.AddWithValue("?jiangjin", xinshui.Jiangjin);
                        mySqlCom.Parameters.AddWithValue("?exclude", xinshui.Exclude);
                        mySqlCom.Parameters.AddWithValue("?operator", xinshui.Operator);
                        mySqlCom.Parameters.AddWithValue("?operatetime", xinshui.OperateTime);
                        mySqlCom.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           

           
        }

        public DataSet GetAllXinshui()
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
select * from xinshuituijian  order by riqi;";

                    mySqlCom.CommandText = sqlStr;
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);

                }
            }
            return ds;

        }

        public DataSet GetValidXinshui()
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
select * from xinshuituijian where exclude = 0 order by riqi;";

                    mySqlCom.CommandText = sqlStr;                  
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);

                }
            }
            return ds;

        }


        public DataSet GetPeilv(string riqi, string bianhao)
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
select * from peilv where riqi = ?riqi and bianhao = ?bianhao;";

                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    mySqlCom.Parameters.AddWithValue("?bianhao", bianhao);
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);

                }
            }
            return ds;
        }

        public DataSet GetKaijiang(string riqi, string bianhao)
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
select * from kaijiang where riqi = ?riqi and bianhao = ?bianhao;";

                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    mySqlCom.Parameters.AddWithValue("?bianhao", bianhao);
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);

                }
            }
            return ds;
        }

        public void UpdateXinshui(Xinshui xinshui)
        {
            string conStr = "server=localhost;User Id=root;database=aicai;" +
                         "Password=root;Character Set=utf8;";
            using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
            {
                mySqlCon.Open();
                using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                {
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    //string sqlStr = @"Select bianhao,liansai,zhudui,kedui,rangqiu,shengsp,pingsp,fusp,riqi from peilv order by bianhao";
                    string sqlStr = @"
update xinshuituijian
set beishu = ?beishu,
Touru = ?Touru,
RealResult1 = ?RealResult1,
RealResult2 = ?RealResult2,
RealResultSp1 = ?RealResultSp1,
RealResultSp2 = ?RealResultSp2,
TouzhuSp1 = ?TouzhuSp1,
TouzhuSp2 = ?TouzhuSp2,
Lucky = ?Lucky,
Jiangjin = ?Jiangjin
where id = ?id";

                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?beishu", xinshui.Beishu);
                    mySqlCom.Parameters.AddWithValue("?Touru", xinshui.Touru);
                    mySqlCom.Parameters.AddWithValue("?RealResult1", xinshui.RealResult1);
                    mySqlCom.Parameters.AddWithValue("?RealResult2", xinshui.RealResult2);
                    mySqlCom.Parameters.AddWithValue("?RealResultSp1", xinshui.RealResultSp1);
                    mySqlCom.Parameters.AddWithValue("?RealResultSp2", xinshui.RealResultSp2);
                    mySqlCom.Parameters.AddWithValue("?TouzhuSp1", xinshui.TouzhuSp1);
                    mySqlCom.Parameters.AddWithValue("?TouzhuSp2", xinshui.TouzhuSp2);
                    mySqlCom.Parameters.AddWithValue("?Lucky", xinshui.Lucky);
                    mySqlCom.Parameters.AddWithValue("?Jiangjin", xinshui.Jiangjin);
                    mySqlCom.Parameters.AddWithValue("?id", xinshui.ID);
                    mySqlCom.ExecuteScalar();

                }
            }
           
        }

        public void ExcludeXinshui(int xinshuiID,int exclude)
        {
            string conStr = "server=localhost;User Id=root;database=aicai;" +
                         "Password=root;Character Set=utf8;";
            using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
            {
                mySqlCon.Open();
                using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                {
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    //string sqlStr = @"Select bianhao,liansai,zhudui,kedui,rangqiu,shengsp,pingsp,fusp,riqi from peilv order by bianhao";
                    string sqlStr = @"
update xinshuituijian
set exclude = ?exclude
where id = ?id";

                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?exclude", exclude);
                    mySqlCom.Parameters.AddWithValue("?id", xinshuiID);
                    mySqlCom.ExecuteScalar();

                }
            }

        }

        public void ResetBeishu(int xinshuiID)
        {
            string conStr = "server=localhost;User Id=root;database=aicai;" +
                        "Password=root;Character Set=utf8;";
            using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
            {
                mySqlCon.Open();
                using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                {
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    //string sqlStr = @"Select bianhao,liansai,zhudui,kedui,rangqiu,shengsp,pingsp,fusp,riqi from peilv order by bianhao";
                    string sqlStr = @"
update xinshuituijian
set beishu = 0
where id = ?id";

                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?id", xinshuiID);
                    mySqlCom.ExecuteScalar();

                }
            }
        }
    }
}
