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
    public class TaocanDetailDAL
    {
        public bool InsertTaocanDetail(TaocanDetail taocandetail)
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
                    string sqlStr = @"insert into taocan_detail(
taocanid,zhudui1,zhuduiluck1,zhuduishengfu1,zhudui2,zhuduiluck2,zhuduishengfu2,beishu,jiangjin,lucky,operator,operatedate,tiaozhengflag
)
VALUES
(
?taocanid,?zhudui1,?zhuduiluck1,?zhuduishengfu1,?zhudui2,?zhuduiluck2,?zhuduishengfu2,?beishu,?jiangjin,?lucky,?operator,?operatedate,?tiaozhengflag
)

";
                    //"SELECT COUNT(*) FROM BlogsUsers WHERE BlogsName=?BlogsName";
                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?taocanid", taocandetail.TaocanID);
                    mySqlCom.Parameters.AddWithValue("?zhudui1", taocandetail.Zhudui1);
                    mySqlCom.Parameters.AddWithValue("?zhuduiluck1", taocandetail.Zhuduilucky1);
                    mySqlCom.Parameters.AddWithValue("?zhuduishengfu1", taocandetail.Zhuduishengfu1);
                    mySqlCom.Parameters.AddWithValue("?zhudui2", taocandetail.Zhudui2);
                    mySqlCom.Parameters.AddWithValue("?zhuduiluck2", taocandetail.Zhuduilucky2);
                    mySqlCom.Parameters.AddWithValue("?zhuduishengfu2", taocandetail.Zhuduishengfu2);
                    mySqlCom.Parameters.AddWithValue("?beishu", taocandetail.Beishu);
                    mySqlCom.Parameters.AddWithValue("?jiangjin", taocandetail.Jiangjin);
                    mySqlCom.Parameters.AddWithValue("?lucky", taocandetail.Lucky);
                    mySqlCom.Parameters.AddWithValue("?operator", taocandetail.Operator);
                    mySqlCom.Parameters.AddWithValue("?operatedate", taocandetail.OperateTime);
                    mySqlCom.Parameters.AddWithValue("?tiaozhengflag",taocandetail.TiaozhengFlag);


                    total = Convert.ToInt32(mySqlCom.ExecuteScalar());
                }
            }

            return total == 0 ? true : false;
                 }
            catch (Exception ex){
                Console.WriteLine(ex.Message);
            }
            return false;
        }


        public bool InsertTaocanDetailVote(TaocanDetail taocandetail)
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
                        string sqlStr = @"insert into taocan_detail_vote(
taocanid,zhudui1,zhuduiluck1,zhuduishengfu1,zhudui2,zhuduiluck2,zhuduishengfu2,beishu,jiangjin,lucky,operator,operatedate
)
VALUES
(
?taocanid,?zhudui1,?zhuduiluck1,?zhuduishengfu1,?zhudui2,?zhuduiluck2,?zhuduishengfu2,?beishu,?jiangjin,?lucky,?operator,?operatedate
)

";
                        //"SELECT COUNT(*) FROM BlogsUsers WHERE BlogsName=?BlogsName";
                        mySqlCom.CommandText = sqlStr;
                        mySqlCom.Parameters.AddWithValue("?taocanid", taocandetail.TaocanID);
                        mySqlCom.Parameters.AddWithValue("?zhudui1", taocandetail.Zhudui1);
                        mySqlCom.Parameters.AddWithValue("?zhuduiluck1", taocandetail.Zhuduilucky1);
                        mySqlCom.Parameters.AddWithValue("?zhuduishengfu1", taocandetail.Zhuduishengfu1);
                        mySqlCom.Parameters.AddWithValue("?zhudui2", taocandetail.Zhudui2);
                        mySqlCom.Parameters.AddWithValue("?zhuduiluck2", taocandetail.Zhuduilucky2);
                        mySqlCom.Parameters.AddWithValue("?zhuduishengfu2", taocandetail.Zhuduishengfu2);
                        mySqlCom.Parameters.AddWithValue("?beishu", taocandetail.Beishu);
                        mySqlCom.Parameters.AddWithValue("?jiangjin", taocandetail.Jiangjin);
                        mySqlCom.Parameters.AddWithValue("?lucky", taocandetail.Lucky);
                        mySqlCom.Parameters.AddWithValue("?operator", taocandetail.Operator);
                        mySqlCom.Parameters.AddWithValue("?operatedate", taocandetail.OperateTime);


                        total = Convert.ToInt32(mySqlCom.ExecuteScalar());
                    }
                }

                return total == 0 ? true : false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }


        public DataSet GetTaocanDetail(string riqi)
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
    taocan.id,
	taocan.riqi,
	taocan.Bianhao1,
	taocan.Bianhao2,
	taocan.type,
	taocan.lucky,
	taocan.zhudui1,
	taocan.Kedui1,
	taocan_detail.zhuduishengfu1,
	taocan_detail.Zhuduiluck1,
	taocan.Zhudui2,
	taocan.kedui2,
	taocan_detail.zhuduishengfu2,
	taocan_detail.Zhuduiluck2,
	taocan_detail.TiaozhengFlag,
	taocan_detail.TiaozhengQiushu1,
	taocan_detail.TiaozhengQiushu2,
    taocan_detail.id as detailid
FROM
	taocan
INNER JOIN taocan_detail ON taocan.ID = taocan_detail.TaocanID
WHERE
	riqi = ?riqi";

                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);
                }
            }
            return ds;
        }
        public DataSet GetTaocanDetail(string startdate , string enddate)
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
    taocan.id,
	taocan.riqi,
	taocan.Bianhao1,
	taocan.Bianhao2,
	taocan.type,
	taocan.lucky,
	taocan.zhudui1,
	taocan.Kedui1,
	taocan_detail.zhuduishengfu1,
	taocan_detail.Zhuduiluck1,
	taocan.Zhudui2,
	taocan.kedui2,
	taocan_detail.zhuduishengfu2,
	taocan_detail.Zhuduiluck2,
	taocan_detail.TiaozhengFlag,
	taocan_detail.TiaozhengQiushu1,
	taocan_detail.TiaozhengQiushu2,
    taocan_detail.id as detailid
FROM
	taocan
INNER JOIN taocan_detail ON taocan.ID = taocan_detail.TaocanID
WHERE
	riqi >= ?startdate
and riqi <= ?enddate
";

                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?startdate", startdate);
                    mySqlCom.Parameters.AddWithValue("?enddate", enddate);
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);
                }
            }
            return ds;
        }

        public void UpdateBianhao(int detailid, int xuhao, string zhudui, string kedui,string riqi)
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
AND zhudui = ?zhudui
AND kedui = ?kedui";

                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    mySqlCom.Parameters.AddWithValue("?zhudui", zhudui);
                    mySqlCom.Parameters.AddWithValue("?kedui", kedui);
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);

                    string bianhao = "000";
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        bianhao = ds.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        using (MySqlCommand mySqlCom2 = mySqlCon.CreateCommand())
                        {

                              DataSet ds2 = new DataSet();
                            //string sqlStr = @"Select bianhao,liansai,zhudui,kedui,rangqiu,shengsp,pingsp,fusp,riqi from peilv order by bianhao";
                            string sqlStr2 = @"SELECT
	*
FROM
	kaijiang k
LEFT JOIN qiuduimapping AS q1 ON k.zhudui = q1.originname
LEFT JOIN qiuduimapping AS q2 ON k.kedui = q2.originname
WHERE
	k.riqi = ?riqi
AND q1.TargetName = ?zhudui
AND q2.TargetName = ?kedui";

                            mySqlCom2.CommandText = sqlStr2;
                            mySqlCom2.Parameters.AddWithValue("?riqi", riqi);
                            mySqlCom2.Parameters.AddWithValue("?zhudui", zhudui);
                            mySqlCom2.Parameters.AddWithValue("?kedui", kedui);
                            da.SelectCommand = mySqlCom2;
                            da.Fill(ds2);
                            if (ds2.Tables[0].Rows.Count != 0)
                            {
                                bianhao = ds2.Tables[0].Rows[0][0].ToString();

                            }
                        }

                    }
                    string str3 = @"";
                    if (xuhao == 1)
                    {
                        str3 = @"
                        update taocan
set bianhao1 = ?bianhao
where id = ?id;";
                    }
                    else if (xuhao == 2)
                    {
                        str3 = @"
                        update taocan
set bianhao2 = ?bianhao
where id = ?id;";
                    }
                    using (MySqlCommand mySqlCom3 = mySqlCon.CreateCommand())
                    {
                        mySqlCom3.CommandText = str3;
                        mySqlCom3.Parameters.AddWithValue("?id", detailid);
                        mySqlCom3.Parameters.AddWithValue("?bianhao", bianhao);
                        mySqlCom3.ExecuteNonQuery();
                    }
                }
            }
        }


        public void UpdateTiaozheng(int detailid, int tiaozheng1, int tiaozheng2)
        {
            try
            {
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
UPDATE taocan_detail
SET TiaozhengFlag = 1,
 tiaozhengqiushu1 = ?tiaozhengqiushu1,
 tiaozhengqiushu2 = ?tiaozhengqiushu2
WHERE
	id = ?detailid
";
                        //"SELECT COUNT(*) FROM BlogsUsers WHERE BlogsName=?BlogsName";
                        mySqlCom.CommandText = sqlStr;
                        mySqlCom.Parameters.AddWithValue("?tiaozhengqiushu1", tiaozheng1);
                        mySqlCom.Parameters.AddWithValue("?tiaozhengqiushu2", tiaozheng2);
                        mySqlCom.Parameters.AddWithValue("?detailid", detailid);
                        mySqlCom.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
