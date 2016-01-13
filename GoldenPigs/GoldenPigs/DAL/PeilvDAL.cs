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
    public class PeilvDAL
    {
        public int InsertPeilv(Peilv peilv)
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
                    string sqlStr = @"insert into peilv
( 
riqi,liansai,bianhao,zhudui,zhuduipaiming,kedui,keduipaiming,shengsp,pingsp,fusp,rangqiu,operator,operatetime,endtime
)
values
(
?riqi,?liansai,?bianhao,?zhudui,?zhuduipaiming,?kedui,?keduipaiming,?shengsp,?pingsp,?fusp,?rangqiu,?operator,?operatetime,?endtime
)
";
                        //"SELECT COUNT(*) FROM BlogsUsers WHERE BlogsName=?BlogsName";
                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?riqi", peilv.Riqi);
                    mySqlCom.Parameters.AddWithValue("?liansai", peilv.Liansai);
                    mySqlCom.Parameters.AddWithValue("?bianhao", peilv.Bianhao);
                    mySqlCom.Parameters.AddWithValue("?zhudui", peilv.Zhudui);
                    mySqlCom.Parameters.AddWithValue("?zhuduipaiming", peilv.Zhuduipaiming);
                    mySqlCom.Parameters.AddWithValue("?kedui", peilv.Kedui);
                    mySqlCom.Parameters.AddWithValue("?keduipaiming", peilv.Keduipaiming);
                    mySqlCom.Parameters.AddWithValue("?shengsp", peilv.ShengSp);
                    mySqlCom.Parameters.AddWithValue("?pingsp", peilv.PingSp);
                    mySqlCom.Parameters.AddWithValue("?fusp", peilv.FuSp);
                    mySqlCom.Parameters.AddWithValue("?rangqiu", peilv.Rangqiu);
                    mySqlCom.Parameters.AddWithValue("?operator", peilv.Operator);
                    mySqlCom.Parameters.AddWithValue("?operatetime", peilv.OperateTime);

                    mySqlCom.Parameters.AddWithValue("?endtime", peilv.OperateTime);

                    total = Convert.ToInt32(mySqlCom.ExecuteScalar());
                }
            }

            return total; 
        }

        /// <summary>
        /// 从开奖数据里获得赔率数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetPeilvDataFromKaijiang()
        {
            string riqi = DateTime.Now.Date.ToString("yyyy-MM-dd");
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
	peilv.bianhao,
	peilv.liansai,
	peilv.zhudui,
	peilv.kedui,
	peilv.rangqiu,
	peilv.shengsp,
	peilv.pingsp,
	peilv.fusp,
	peilv.riqi
FROM
	 peilv
WHERE
	peilv.riqi = ?riqi
order by peilv.bianhao";

                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);

                }
            }
            return ds;
        }

        public DataSet GetPeilvData()
        {
            string riqi = DateTime.Now.Date.ToString("yyyy-MM-dd");
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
	peilv.bianhao,
	peilv.liansai,
	peilv.zhudui,
	peilv.kedui,
	peilv.rangqiu,
	peilv.shengsp,
	peilv.pingsp,
	peilv.fusp,
	peilv.riqi,
	j1.paiming,
	j2.paiming
FROM
	peilv
LEFT JOIN (
	SELECT
		j.*, q1.targetname,
		ifnull(q1.targetname, j.qiudui) AS qiuduireal
	FROM
		jifenbang AS j
	LEFT JOIN qiuduimapping AS q1 ON j.qiudui = q1.originname
) AS j1 ON peilv.Zhudui = j1.qiuduireal
LEFT JOIN (
	SELECT
		j.*, q1.targetname,
		ifnull(q1.targetname, j.qiudui) AS qiuduireal
	FROM
		jifenbang AS j
	LEFT JOIN qiuduimapping AS q1 ON j.qiudui = q1.originname
) AS j2 ON peilv.Kedui = j2.qiuduireal
WHERE
	peilv.riqi = ?riqi
order by bianhao";

                     mySqlCom.CommandText = sqlStr;
                     mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                     da.SelectCommand = mySqlCom;
                     da.Fill(ds);
                     
                 }
             }
             return ds;
        }

        /// <summary>
        /// 从kaijiang表里查询赔率
        /// </summary>
        /// <param name="riqi"></param>
        /// <param name="liansai"></param>
        /// <returns></returns>
        public DataSet SearchPeilvFromKaijiang(string riqi, string liansai)
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
                    if (liansai != "全部")
                    {
                        //string sqlStr = @"Select bianhao,liansai,zhudui,kedui,rangqiu,shengsp,pingsp,fusp,riqi from peilv where riqi=?riqi and liansai = ?liansan order by bianhao ";
                        string sqlStr = @"
SELECT
	peilv.bianhao,
	peilv.liansai,
	peilv.zhudui,
	peilv.kedui,
	peilv.rangqiu,
	peilv.shengsp,
	peilv.pingsp,
	peilv.fusp,
	peilv.riqi
FROM
	peilv

WHERE
	peilv.riqi = ?riqi
    and peilv.liansai = ?liansai
order by peilv.bianhao";

                        mySqlCom.CommandText = sqlStr;
                        mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                        mySqlCom.Parameters.AddWithValue("?liansai", liansai);
                    }
                    else
                    {
                        //string sqlStr = @"Select bianhao,liansai,zhudui,kedui,rangqiu,shengsp,pingsp,fusp,riqi from peilv  where riqi=?riqi order by bianhao";
                        string sqlStr = @"
SELECT
	peilv.bianhao,
	peilv.liansai,
	peilv.zhudui,
	peilv.kedui,
	peilv.rangqiu,
	peilv.shengsp,
	peilv.pingsp,
	peilv.fusp,
	peilv.riqi
FROM
	peilv

WHERE
	peilv.riqi = ?riqi
order by peilv.bianhao ";
                        mySqlCom.CommandText = sqlStr;
                        mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    }
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);

                }
            }
            return ds;
        }

        public DataSet SearchPeilv(string riqi,string liansai)
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
                    if (liansai != "全部")
                    {
                        //string sqlStr = @"Select bianhao,liansai,zhudui,kedui,rangqiu,shengsp,pingsp,fusp,riqi from peilv where riqi=?riqi and liansai = ?liansan order by bianhao ";
                        string sqlStr = @"
SELECT
	peilv.bianhao,
	peilv.liansai,
	peilv.zhudui,
	peilv.kedui,
	peilv.rangqiu,
	peilv.shengsp,
	peilv.pingsp,
	peilv.fusp,
	peilv.riqi,
	j1.paiming,
	j2.paiming
FROM
	peilv
LEFT JOIN (
	SELECT
		j.*, q1.targetname,
		ifnull(q1.targetname, j.qiudui) AS qiuduireal
	FROM
		jifenbang AS j
	LEFT JOIN qiuduimapping AS q1 ON j.qiudui = q1.originname
) AS j1 ON peilv.Zhudui = j1.qiuduireal
LEFT JOIN (
	SELECT
		j.*, q1.targetname,
		ifnull(q1.targetname, j.qiudui) AS qiuduireal
	FROM
		jifenbang AS j
	LEFT JOIN qiuduimapping AS q1 ON j.qiudui = q1.originname
) AS j2 ON peilv.Kedui = j2.qiuduireal
WHERE
	peilv.riqi = ?riqi
    and peilv.liansai = ?liansai
order by bianhao";

                        mySqlCom.CommandText = sqlStr;
                        mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                        mySqlCom.Parameters.AddWithValue("?liansai", liansai);
                    }
                    else
                    {
                        //string sqlStr = @"Select bianhao,liansai,zhudui,kedui,rangqiu,shengsp,pingsp,fusp,riqi from peilv  where riqi=?riqi order by bianhao";
                        string sqlStr = @"
SELECT
	peilv.bianhao,
	peilv.liansai,
	peilv.zhudui,
	peilv.kedui,
	peilv.rangqiu,
	peilv.shengsp,
	peilv.pingsp,
	peilv.fusp,
	peilv.riqi,
	j1.paiming,
	j2.paiming
FROM
	peilv
LEFT JOIN (
	SELECT
		j.*, q1.targetname,
		ifnull(q1.targetname, j.qiudui) AS qiuduireal
	FROM
		jifenbang AS j
	LEFT JOIN qiuduimapping AS q1 ON j.qiudui = q1.originname
) AS j1 ON peilv.Zhudui = j1.qiuduireal
LEFT JOIN (
	SELECT
		j.*, q1.targetname,
		ifnull(q1.targetname, j.qiudui) AS qiuduireal
	FROM
		jifenbang AS j
	LEFT JOIN qiuduimapping AS q1 ON j.qiudui = q1.originname
) AS j2 ON peilv.Kedui = j2.qiuduireal
WHERE
	peilv.riqi = ?riqi
order by bianhao ";
                        mySqlCom.CommandText = sqlStr;
                        mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    }
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);

                }
            }
            return ds;
        }
        public void DeletePeilv(string riqi)
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
                    string sqlStr = @"delete from peilv where riqi = ?riqi";
                    mySqlCom.CommandText = sqlStr;

                    mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    mySqlCom.ExecuteScalar();

                }
            }
        }

        public DataSet GetPeilvLiansai(String riqi)
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
select distinct liansai from peilv
where riqi = ?riqi
order by Liansai";
                     mySqlCom.CommandText = sqlStr;
                     mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                     da.SelectCommand = mySqlCom;
                     da.Fill(ds);
                 }
             }
             return ds;

        }

        /// <summary>
        /// 从kaijiang表里取得相应数据，peilv表废弃不用
        /// </summary>
        /// <param name="riqi"></param>
        /// <returns></returns>
        public DataSet GetPeilvLiansaiFromKaijiang(String riqi)
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
select distinct liansai from peilv
where riqi = ?riqi
order by Liansai";
                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);
                }
            }
            return ds;

        }

        //该方法未完成
        public void InsertPeilvFromKaijiang(string riqi)
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
                    string sqlStr = @"delete from peilv where riqi = ?riqi";
                    mySqlCom.CommandText = sqlStr;

                    mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    mySqlCom.ExecuteScalar();

                }
            }
        }
    }
}
