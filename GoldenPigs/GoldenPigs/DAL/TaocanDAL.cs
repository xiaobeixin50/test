using GoldenPigs.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPigs.DAL
{
    public class TaocanDAL
    {
        public int InsertTaocan(Taocan taocan)
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
                    string sqlStr = @"insert into taocan(qishu,
riqi ,
gailv,
huibaolv,
touru,
jiangjin,
lucky,
type,
zhudui1,
kedui1,
zhudui2,
kedui2,
remark)
VALUES(
?qishu,
?riqi ,
?gailv,
?huibaolv,
?touru,
?jiangjin,
?lucky,
?type,
?zhudui1,
?kedui1,
?zhudui2,
?kedui2,
?remark
);
Select @@IDENTITY;
";
                        //"SELECT COUNT(*) FROM BlogsUsers WHERE BlogsName=?BlogsName";
                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?qishu", taocan.Qishu);
                    mySqlCom.Parameters.AddWithValue("?riqi", taocan.Riqi);
                    mySqlCom.Parameters.AddWithValue("?gailv", taocan.Gailv);
                    mySqlCom.Parameters.AddWithValue("?huibaolv", taocan.Huibaolv);
                    mySqlCom.Parameters.AddWithValue("?touru", taocan.Touru);
                    mySqlCom.Parameters.AddWithValue("?jiangjin", taocan.Jiangjin);
                    mySqlCom.Parameters.AddWithValue("?lucky", taocan.Lucky);
                    mySqlCom.Parameters.AddWithValue("?type", taocan.Type);
                    mySqlCom.Parameters.AddWithValue("?zhudui1", taocan.Zhudui1);
                    mySqlCom.Parameters.AddWithValue("?kedui1", taocan.Kedui1);
                    mySqlCom.Parameters.AddWithValue("?zhudui2", taocan.Zhudui2);
                    mySqlCom.Parameters.AddWithValue("?kedui2", taocan.Kedui2);
                    mySqlCom.Parameters.AddWithValue("?remark", taocan.Remark);

                    total = Convert.ToInt32(mySqlCom.ExecuteScalar());
                }
            }

            return total; 
        }


        public int InsertTaocanVote(Taocan taocan)
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
                    string sqlStr = @"insert into taocan_vote(qishu,
riqi ,
gailv,
huibaolv,
touru,
jiangjin,
lucky,
type,
zhudui1,
kedui1,
zhudui2,
kedui2,
remark)
VALUES(
?qishu,
?riqi ,
?gailv,
?huibaolv,
?touru,
?jiangjin,
?lucky,
?type,
?zhudui1,
?kedui1,
?zhudui2,
?kedui2,
?remark
);
Select @@IDENTITY;
";
                    //"SELECT COUNT(*) FROM BlogsUsers WHERE BlogsName=?BlogsName";
                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?qishu", taocan.Qishu);
                    mySqlCom.Parameters.AddWithValue("?riqi", taocan.Riqi);
                    mySqlCom.Parameters.AddWithValue("?gailv", taocan.Gailv);
                    mySqlCom.Parameters.AddWithValue("?huibaolv", taocan.Huibaolv);
                    mySqlCom.Parameters.AddWithValue("?touru", taocan.Touru);
                    mySqlCom.Parameters.AddWithValue("?jiangjin", taocan.Jiangjin);
                    mySqlCom.Parameters.AddWithValue("?lucky", taocan.Lucky);
                    mySqlCom.Parameters.AddWithValue("?type", taocan.Type);
                    mySqlCom.Parameters.AddWithValue("?zhudui1", taocan.Zhudui1);
                    mySqlCom.Parameters.AddWithValue("?kedui1", taocan.Kedui1);
                    mySqlCom.Parameters.AddWithValue("?zhudui2", taocan.Zhudui2);
                    mySqlCom.Parameters.AddWithValue("?kedui2", taocan.Kedui2);
                    mySqlCom.Parameters.AddWithValue("?remark", taocan.Remark);

                    total = Convert.ToInt32(mySqlCom.ExecuteScalar());
                }
            }

            return total;
        }
    }
}
