using GoldenPigs.Entity;
using GoldenPigs.Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoldenPigs.DAL
{
    public class KaijiangDAL
    {
        public bool InsertKaijiang(Kaijiang kaijiang)
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
insert into kaijiang(
Xingqi,
Riqi,
Bianhao,
Liansai,
Bisaishijian,
EndTime,
Zhudui,
Zhuduiliansai,
Zhuduipaiming,
Kedui,
Keduiliansai,
Keduipaiming,
SpfResult,
SpfSp,
RqShu,
RqspfResult,
RqspfSp,
QcbfResult,
QcbfSp,
ZjqResult,
ZjqSp,
BqcResult,
BqcSp,
Operator,
OperateTime,
ShengSp,
PingSp,
FuSp,
RqShengSp,
RqPingSp,
RqFuSp,
ShengSpInt,
PingSpInt,
FuSpInt,
RqShengSpInt,
RqPingSpInt,
RqFuSpInt,
ShengSpFracInt,
PingSpFracInt,
FuSpFracInt,
RqShengSpFracInt,
RqPingSpFracInt,
RqFuSpFracInt
)
values
(
?Xingqi,
?Riqi,
?Bianhao,
?Liansai,
?Bisaishijian,
?EndTime,
?Zhudui,
?Zhuduiliansai,
?Zhuduipaiming,
?Kedui,
?Keduiliansai,
?Keduipaiming,
?SpfResult,
?SpfSp,
?RqShu,
?RqspfResult,
?RqspfSp,
?QcbfResult,
?QcbfSp,
?ZjqResult,
?ZjqSp,
?BqcResult,
?BqcSp,
?Operator,
?OperateTime,
?ShengSp,
?PingSp,
?FuSp,
?RqShengSp,
?RqPingSp,
?RqFuSp,
?ShengSpInt,
?PingSpInt,
?FuSpInt,
?RqShengSpInt,
?RqPingSpInt,
?RqFuSpInt,
?ShengSpFracInt,
?PingSpFracInt,
?FuSpFracInt,
?RqShengSpFracInt,
?RqPingSpFracInt,
?RqFuSpFracInt
)
";

                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?Xingqi", kaijiang.Xingqi);
                    mySqlCom.Parameters.AddWithValue("?Riqi", kaijiang.Riqi);
                    mySqlCom.Parameters.AddWithValue("?Bianhao", kaijiang.Bianhao);
                    mySqlCom.Parameters.AddWithValue("?Liansai", kaijiang.Liansai);
                    mySqlCom.Parameters.AddWithValue("?Bisaishijian", kaijiang.Bisaishijian);
                    mySqlCom.Parameters.AddWithValue("?EndTime", kaijiang.EndTime);
                    mySqlCom.Parameters.AddWithValue("?Zhudui", kaijiang.Zhudui);
                    mySqlCom.Parameters.AddWithValue("?Zhuduiliansai", kaijiang.Zhuduiliansai);
                    mySqlCom.Parameters.AddWithValue("?Zhuduipaiming", kaijiang.Zhuduipaiming);
                    mySqlCom.Parameters.AddWithValue("?Kedui", kaijiang.Kedui);
                    mySqlCom.Parameters.AddWithValue("?Keduiliansai", kaijiang.Keduiliansai);
                    mySqlCom.Parameters.AddWithValue("?Keduipaiming", kaijiang.Keduipaiming);
                    mySqlCom.Parameters.AddWithValue("?SpfResult", kaijiang.SpfResult);
                    mySqlCom.Parameters.AddWithValue("?SpfSp", kaijiang.SpfSp);
                    mySqlCom.Parameters.AddWithValue("?RqShu", kaijiang.RqShu);
                    mySqlCom.Parameters.AddWithValue("?RqspfResult", kaijiang.RqspfResult);
                    mySqlCom.Parameters.AddWithValue("?RqspfSp", kaijiang.RqspfSp);
                    mySqlCom.Parameters.AddWithValue("?QcbfResult", kaijiang.QcbfResult);
                    mySqlCom.Parameters.AddWithValue("?QcbfSp", kaijiang.QcbfSp);
                    mySqlCom.Parameters.AddWithValue("?ZjqResult", kaijiang.ZjqResult);
                    mySqlCom.Parameters.AddWithValue("?ZjqSp", kaijiang.ZjqSp);
                    mySqlCom.Parameters.AddWithValue("?BqcResult", kaijiang.BqcResult);
                    mySqlCom.Parameters.AddWithValue("?BqcSp", kaijiang.BqcSp);
                    mySqlCom.Parameters.AddWithValue("?Operator", kaijiang.Operator);
                    mySqlCom.Parameters.AddWithValue("?OperateTime", kaijiang.OperateTime);


                    mySqlCom.Parameters.AddWithValue("?ShengSp", kaijiang.ShengSp);
                    mySqlCom.Parameters.AddWithValue("?PingSp", kaijiang.PingSp);
                    mySqlCom.Parameters.AddWithValue("?FuSp", kaijiang.FuSp);
                    mySqlCom.Parameters.AddWithValue("?RqShengSp", kaijiang.RqShengSp);
                    mySqlCom.Parameters.AddWithValue("?RqPingSp", kaijiang.RqPingSp);
                    mySqlCom.Parameters.AddWithValue("?RqFuSp", kaijiang.RqFuSp);
                    mySqlCom.Parameters.AddWithValue("?ShengSpInt", kaijiang.ShengSpInt);
                    mySqlCom.Parameters.AddWithValue("?PingSpInt", kaijiang.PingSpInt);
                    mySqlCom.Parameters.AddWithValue("?FuSpInt", kaijiang.FuSpInt);
                    mySqlCom.Parameters.AddWithValue("?RqShengSpInt", kaijiang.RqShengSpInt);
                    mySqlCom.Parameters.AddWithValue("?RqPingSpInt", kaijiang.RqPingSpInt);
                    mySqlCom.Parameters.AddWithValue("?RqFuSpInt", kaijiang.RqFuSpInt);
                    mySqlCom.Parameters.AddWithValue("?ShengSpFracInt", kaijiang.ShengSpFracInt);
                    mySqlCom.Parameters.AddWithValue("?PingSpFracInt", kaijiang.PingSpFracInt);
                    mySqlCom.Parameters.AddWithValue("?FuSpFracInt", kaijiang.FuSpFracInt);
                    mySqlCom.Parameters.AddWithValue("?RqShengSpFracInt", kaijiang.RqShengSpFracInt);
                    mySqlCom.Parameters.AddWithValue("?RqPingSpFracInt", kaijiang.RqPingSpFracInt);
                    mySqlCom.Parameters.AddWithValue("?RqFuSpFracInt", kaijiang.RqFuSpFracInt);

                    try
                    {
                        total = Convert.ToInt32(mySqlCom.ExecuteScalar());
                    }
                    catch(Exception ex){
                        MessageBox.Show(ex.Message);
                    }
                    
                }
            }

            return total == 0 ? true : false;
        }

        public string GetKaijangBifen(string riqi, string bianhao)
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
SELECT
	QcbfResult
FROM
	kaijiang
WHERE
	riqi = ?riqi
AND bianhao = ?bianhao";

                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    mySqlCom.Parameters.AddWithValue("?bianhao", bianhao);
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);

                }
            }
            if (ds.Tables[0].Rows.Count != 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return null;
            }
     
        }
        public Kaijiang GetKaijangSpfResult(string riqi, string bianhao)
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
SELECT
	*
FROM
	kaijiang
WHERE
	riqi = ?riqi
AND bianhao = ?bianhao";

                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    mySqlCom.Parameters.AddWithValue("?bianhao", bianhao);
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);

                }
            }
            if (ds.Tables[0].Rows.Count != 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                Kaijiang kaijiang = new Kaijiang();
                kaijiang.Bianhao = row["bianhao"].ToString();
                kaijiang.SpfResult = row["SpfResult"].ToString();
                kaijiang.SpfSp = Convert.ToDouble(row["SpfSp"].ToString());
                kaijiang.RqShu = Convert.ToInt32(row["RqShu"].ToString());
                kaijiang.RqspfResult = row["RqspfResult"].ToString();
                kaijiang.RqspfSp = Convert.ToDouble(row["RqspfSp"].ToString());
                return kaijiang;
            }
            else
            {
                return null;
            }

        }

        public void DeleteKaijiang(string riqi)
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
                    string sqlStr = @"delete from kaijiang where riqi = ?riqi";
                    mySqlCom.CommandText = sqlStr;

                    mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    mySqlCom.ExecuteScalar();

                }
            }
        }
        
        public Kaijiang GetKaijiang(string riqi, string zhudui, string kedui)
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
SELECT
	*
FROM
	kaijiang
WHERE
	bisaishijian = ?riqi
AND zhudui = ?zhudui
AND kedui = ?kedui";

                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    mySqlCom.Parameters.AddWithValue("?zhudui", zhudui);
                    mySqlCom.Parameters.AddWithValue("?kedui", kedui);
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);

                }
            }
            if (ds.Tables[0].Rows.Count != 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                Kaijiang kaijiang = new Kaijiang();
                kaijiang.Bianhao = row["bianhao"].ToString();
                kaijiang.SpfResult = row["SpfResult"].ToString();
                kaijiang.SpfSp = Convert.ToDouble(row["SpfSp"].ToString());
                kaijiang.RqShu = Convert.ToInt32(row["RqShu"].ToString());
                kaijiang.RqspfResult = row["RqspfResult"].ToString();
                kaijiang.RqspfSp = Convert.ToDouble(row["RqspfSp"].ToString());
                return kaijiang;
            }
            else
            {
                return null;
            }

        }

        public DataSet GetAllKaijiang()
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
                    string sqlStr = @"select * from kaijiang order by riqi asc, bianhao asc;";

                    mySqlCom.CommandText = sqlStr;
                    //mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);

                }
            }
            return ds;
        }
        

        public Kaijiang GetKaijiangByRiqiAndBianhao(string riqi, string bianhao)
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
SELECT
	*
FROM
	kaijiang
WHERE
	riqi = ?riqi
AND bianhao = ?bianhao";

                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    mySqlCom.Parameters.AddWithValue("?bianhao", bianhao);
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);

                }
            }
            if (ds.Tables[0].Rows.Count != 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                Kaijiang kaijiang = new Kaijiang();
                kaijiang.Riqi = row["riqi"].ToString();
                kaijiang.Bianhao = row["bianhao"].ToString();
                kaijiang.SpfResult = row["SpfResult"].ToString();
                kaijiang.SpfSp = Convert.ToDouble(row["SpfSp"].ToString());
                kaijiang.RqShu = Convert.ToInt32(row["RqShu"].ToString());
                kaijiang.RqspfResult = row["RqspfResult"].ToString();
                kaijiang.RqspfSp = Convert.ToDouble(row["RqspfSp"].ToString());
                kaijiang.ShengSp = Convert.ToDouble(row["ShengSp"].ToString());
                kaijiang.PingSp = Convert.ToDouble(row["PingSp"].ToString());
                kaijiang.FuSp = Convert.ToDouble(row["FuSp"].ToString());
                kaijiang.BqcResult = row["BqcResult"].ToString();

                kaijiang.QcbfResult = row["QcbfResult"].ToString();
                return kaijiang;
            }
            else
            {
                return null;
            }
        }


        public string GetKaijiangBianhao(string riqi, string zhudui, string kedui)
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
SELECT
	bianhao
FROM
	kaijiang
WHERE
	riqi = ?riqi
AND 
(zhudui = ?zhudui
or kedui = ?kedui )";

                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    mySqlCom.Parameters.AddWithValue("?zhudui", zhudui);
                    mySqlCom.Parameters.AddWithValue("?kedui", kedui);
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);

                }
            }
            if (ds.Tables[0].Rows.Count != 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
               
                return row["bianhao"].ToString(); ;
            }
            else
            {
                return "";
            }

        }

        //更新排名数据
        public void UpdatePaiming(string riqi ,string bianhao, string zhuduipaiming, string keduipaiming, string zhuduiliansai, string keduiliansai)
        {
            string sqltemp = "update kaijiang set zhuduipaiming = '{0}' ,keduipaiming = '{1}',zhuduiliansai = '{4}',keduiliansai  = '{5}'  where riqi = '{2}' and bianhao = '{3}'";
            string sql = String.Format(sqltemp, zhuduipaiming,keduipaiming,riqi,bianhao,zhuduiliansai,keduiliansai);

            SQLHelper.ExecuteNonQuery(sql);

        }

        public DataTable GetKaijiangDateYuce1(string riqi)
        {
            string sqltemp = "select riqi, bianhao, rqshu ,zhudui,kedui, shengsp,pingsp ,fusp ,rqshengsp,rqpingsp,rqfusp,spfresult, rqspfresult from kaijiang where pingsp < fusp and shengsp < 1.6 and riqi = '{0}' ;";
            string sql = String.Format(sqltemp, riqi);

            return SQLHelper.GetDataTable(sql);

        }
        public DataTable GetKaijiangDateYuce2(string riqi)
        {
            string sqltemp = "select riqi, bianhao,rqshu, zhudui,kedui, shengsp,pingsp ,fusp ,rqshengsp,rqpingsp,rqfusp, spfresult,rqspfresult from kaijiang where rqfusp < 1.6 and riqi = '{0}';";
            string sql = String.Format(sqltemp, riqi);

            return SQLHelper.GetDataTable(sql);

        }
    }
}
