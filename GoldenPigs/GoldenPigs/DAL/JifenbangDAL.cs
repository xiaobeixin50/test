using GoldenPigs.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPigs.DAL
{
    public class JifenbangDAL
    {
        public bool InsertJifenbang(Jifenbang jifenbang)
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
                    string sqlStr = @"insert into Jifenbang(
liansai,
paiming,
qiudui,
yisai,
sheng,
ping,
fu,
deqiu,
shiqiu ,
jingshengqiu,
junde,
junshi,
shenglv,
pinglv,
fulv,
jifen,
operator,
operatetime
)
values
(
?liansai,
?paiming,
?qiudui,
?yisai,
?sheng,
?ping,
?fu,
?deqiu,
?shiqiu ,
?jingshengqiu,
?junde,
?junshi,
?shenglv,
?pinglv,
?fulv,
?jifen,
?operator,
?operatetime
)

";

                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?liansai", jifenbang.Liansai);
                    mySqlCom.Parameters.AddWithValue("?paiming", jifenbang.Paiming);
                    mySqlCom.Parameters.AddWithValue("?qiudui", jifenbang.Qiudui);
                    mySqlCom.Parameters.AddWithValue("?yisai", jifenbang.Yisai);
                    mySqlCom.Parameters.AddWithValue("?sheng", jifenbang.Sheng);
                    mySqlCom.Parameters.AddWithValue("?ping", jifenbang.Ping);
                    mySqlCom.Parameters.AddWithValue("?fu", jifenbang.Fu);
                    mySqlCom.Parameters.AddWithValue("?deqiu", jifenbang.Deqiu);
                    mySqlCom.Parameters.AddWithValue("?shiqiu", jifenbang.Shiqiu);
                    mySqlCom.Parameters.AddWithValue("?jingshengqiu", jifenbang.Jingshengqiu);
                    mySqlCom.Parameters.AddWithValue("?junde", jifenbang.Junde);
                    mySqlCom.Parameters.AddWithValue("?junshi", jifenbang.Junshi);
                    mySqlCom.Parameters.AddWithValue("?shenglv", jifenbang.Shenglv);
                    mySqlCom.Parameters.AddWithValue("?pinglv", jifenbang.Pinglv);
                    mySqlCom.Parameters.AddWithValue("?fulv", jifenbang.Fulv);
                    mySqlCom.Parameters.AddWithValue("?jifen", jifenbang.Jifen);
                    mySqlCom.Parameters.AddWithValue("?operator", jifenbang.Operator);
                    mySqlCom.Parameters.AddWithValue("?operatetime", jifenbang.Operatetime);

                    total = Convert.ToInt32(mySqlCom.ExecuteScalar());
                }
            }

            return total == 0 ? true : false;
        }
    }
}