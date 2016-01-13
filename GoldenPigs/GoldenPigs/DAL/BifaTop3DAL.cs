using GoldenPigs.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPigs.DAL
{
    public class BifaTop3DAL
    {
        public bool InsertBifaTop3(BifaTop3 bifaTop3)
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
                    string sqlStr = @"insert into bifatop3
(
riqi,paiming,zhudui,kedui,shijian,shuxing,peilv,chengjiaoe,lucky,operator,operatetime)
values
(
?riqi,?paiming,?zhudui,?kedui,?shijian,?shuxing,?peilv,?chengjiaoe,?lucky,?operator,?operatetime
)
";
                    
                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?riqi", bifaTop3.Riqi);
                    mySqlCom.Parameters.AddWithValue("?paiming", bifaTop3.Paiming);
                    mySqlCom.Parameters.AddWithValue("?zhudui", bifaTop3.Zhudui);
                    mySqlCom.Parameters.AddWithValue("?kedui", bifaTop3.Kedui);
                    mySqlCom.Parameters.AddWithValue("?shijian", bifaTop3.Shijian);
                    mySqlCom.Parameters.AddWithValue("?shuxing", bifaTop3.Shuxing);
                    mySqlCom.Parameters.AddWithValue("?peilv", bifaTop3.Peilv);
                    mySqlCom.Parameters.AddWithValue("?chengjiaoe", bifaTop3.Chengjiaoe);
                    mySqlCom.Parameters.AddWithValue("?lucky", bifaTop3.Lucky);
                    mySqlCom.Parameters.AddWithValue("?operator", bifaTop3.Operator);
                    mySqlCom.Parameters.AddWithValue("?operatetime", bifaTop3.OperateTime);

                    total = Convert.ToInt32(mySqlCom.ExecuteScalar());
                }
            }

            return total == 0 ? true : false;
        
        }
    }
}
