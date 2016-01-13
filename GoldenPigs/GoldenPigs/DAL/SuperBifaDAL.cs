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
    public class SuperBifaDAL
    {
        public bool InsertSuperBifa(SuperBifa bifa)
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
                        string sqlStr = @"insert into superbifa(riqi,xingqi,bianhao,liansai,zhudui,kedui,bifen,kaisaishijian,bifajiawei_sheng,bifajiawei_ping,bifajiawei_fu,bifazhishu_sheng,bifazhishu_ping,
bifazhishu_fu,baijiaoupei_sheng,baijiaoupei_ping,baijiaoupei_fu,chengjiaoe,sheng,ping,fu,dae_sheng,dae_ping,dae_fu,inserttime,prize_rank,dae_prize_rank,first_sp,second_sp,third_sp)
values(?riqi,?xingqi,?bianhao,?liansai,?zhudui,?kedui,?bifen,?kaisaishijian,?bifajiawei_sheng,?bifajiawei_ping,?bifajiawei_fu,?bifazhishu_sheng,?bifazhishu_ping,
?bifazhishu_fu,?baijiaoupei_sheng,?baijiaoupei_ping,?baijiaoupei_fu,?chengjiaoe,?sheng,?ping,?fu,?dae_sheng,?dae_ping,?dae_fu,?inserttime,?prize_rank,?dae_prize_rank,?first_sp,?second_sp,?third_sp)
";
                        //"SELECT COUNT(*) FROM BlogsUsers WHERE BlogsName=?BlogsName";
                        mySqlCom.CommandText = sqlStr;
                        mySqlCom.Parameters.AddWithValue("?riqi", bifa.riqi);
                        mySqlCom.Parameters.AddWithValue("?xingqi", bifa.xingqi);
                        mySqlCom.Parameters.AddWithValue("?bianhao", bifa.bianhao);
                        mySqlCom.Parameters.AddWithValue("?liansai", bifa.liansai);
                        mySqlCom.Parameters.AddWithValue("?zhudui", bifa.zhudui);
                        mySqlCom.Parameters.AddWithValue("?kedui", bifa.kedui);
                        mySqlCom.Parameters.AddWithValue("?bifen", bifa.bifen);
                        mySqlCom.Parameters.AddWithValue("?kaisaishijian", bifa.kaisaishijian);
                        mySqlCom.Parameters.AddWithValue("?bifajiawei_sheng", bifa.bifajiawei_sheng);
                        mySqlCom.Parameters.AddWithValue("?bifajiawei_ping", bifa.bifajiawei_ping);
                        mySqlCom.Parameters.AddWithValue("?bifajiawei_fu", bifa.bifajiawei_fu);
                        mySqlCom.Parameters.AddWithValue("?bifazhishu_sheng", bifa.bifazhishu_sheng);
                        mySqlCom.Parameters.AddWithValue("?bifazhishu_ping", bifa.bifazhishu_ping);
                        mySqlCom.Parameters.AddWithValue("?bifazhishu_fu", bifa.bifazhishu_fu);
                        mySqlCom.Parameters.AddWithValue("?baijiaoupei_sheng", bifa.baijiaoupei_sheng);
                        mySqlCom.Parameters.AddWithValue("?baijiaoupei_ping", bifa.baijiaoupei_ping);
                        mySqlCom.Parameters.AddWithValue("?baijiaoupei_fu", bifa.baijiaoupei_fu);
                        mySqlCom.Parameters.AddWithValue("?chengjiaoe", bifa.chengjiaoe);
                        mySqlCom.Parameters.AddWithValue("?sheng", bifa.sheng);
                        mySqlCom.Parameters.AddWithValue("?ping", bifa.ping);
                        mySqlCom.Parameters.AddWithValue("?fu", bifa.fu);
                        mySqlCom.Parameters.AddWithValue("?dae_sheng", bifa.dae_sheng);
                        mySqlCom.Parameters.AddWithValue("?dae_ping", bifa.dae_ping);
                        mySqlCom.Parameters.AddWithValue("?dae_fu", bifa.dae_fu);
                        mySqlCom.Parameters.AddWithValue("?inserttime", bifa.inserttime);
                        mySqlCom.Parameters.AddWithValue("?prize_rank", bifa.prize_rank);
                        mySqlCom.Parameters.AddWithValue("?dae_prize_rank", bifa.dae_prize_rank);
                        mySqlCom.Parameters.AddWithValue("?first_sp", bifa.first_sp);
                        mySqlCom.Parameters.AddWithValue("?second_sp", bifa.second_sp);
                        mySqlCom.Parameters.AddWithValue("?third_sp", bifa.third_sp);

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



        public void DeleteBifa(string riqi)
        {
            string conStr = "server=localhost;User Id=root;database=aicai;" +
                           "Password=root;Character Set=utf8;";
            
            using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
            {
                mySqlCon.Open();
                using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                {
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    string sqlStr = @"delete from superbifa where riqi = ?riqi";
                    mySqlCom.CommandText = sqlStr;

                    mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    mySqlCom.ExecuteScalar();

                }
            }
        }

        public DataSet GetBifaAndKaijiang()
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
                    string sqlStr = @"select kaijiang.riqi,kaijiang.bianhao,kaijiang.liansai,kaijiang.zhudui,kaijiang.kedui,kaijiang.spfresult,kaijiang.shengsp,
kaijiang.pingsp,kaijiang.fusp,kaijiang.rqshengsp,kaijiang.rqpingsp,kaijiang.rqfusp,superbifa.bifen,superbifa.sheng,superbifa.ping,superbifa.fu,superbifa.dae_sheng,
superbifa.dae_ping,superbifa.dae_fu,superbifa.prize_rank,superbifa.dae_prize_rank
 from kaijiang inner join superbifa on kaijiang.riqi = superbifa.riqi and kaijiang.bianhao = superbifa.bianhao
order by kaijiang.riqi, kaijiang.bianhao";

                    mySqlCom.CommandText = sqlStr;
                    //mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);

                }
            }
            return ds;
        }

        public DataSet GetAllBifa()
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
                    string sqlStr = @"select * from superbifa order by riqi, bianhao";

                    mySqlCom.CommandText = sqlStr;
                    //mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);

                }
            }
            return ds;
        }
    }
}
