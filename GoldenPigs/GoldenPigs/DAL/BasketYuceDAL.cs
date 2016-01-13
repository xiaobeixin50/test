using GoldenPigs.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GoldenPigs.DAL
{
    public class BasketYuceDAL
    {
        public void InsertBasketYuceList(List<BasketYuce> results)
        {
            int total = 0;
            string conStr = "server=localhost;User Id=root;database=aicai;" +
                            "Password=root;Character Set=utf8;";
            using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
            {
                mySqlCon.Open();
                foreach (BasketYuce result in results)
                {
                    using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                    {
//                        string sqlStr = @"
//
//INSERT INTO basketyuce(title, result,zhudui,kedui,xingqi,bianhao,operator,operatetime,bisairiqi,url)
//VALUES(?title, ?result,?zhudui,?kedui,?xingqi,?bianhao,?operator,?operatetime,?bisairiqi,?url)
//";
                        string sqlStr = @"INSERT INTO basketyuce(title, result,zhudui,kedui,xingqi,bianhao,operator,operatetime,bisairiqi,url)
select ?title, ?result,?zhudui,?kedui,?xingqi,?bianhao,?operator,?operatetime,?bisairiqi,?url from dual
where NOT EXISTS (SELECT * FROM basketyuce WHERE url = ?url)
";

                        mySqlCom.CommandText = sqlStr;
                        mySqlCom.Parameters.AddWithValue("?title", result.title);
                        mySqlCom.Parameters.AddWithValue("?result", result.result);
                        mySqlCom.Parameters.AddWithValue("?zhudui", result.zhudui);
                        mySqlCom.Parameters.AddWithValue("?kedui", result.kedui);
                        mySqlCom.Parameters.AddWithValue("?xingqi", result.xingqi);
                        mySqlCom.Parameters.AddWithValue("?bianhao", result.bianhao);
                        mySqlCom.Parameters.AddWithValue("?operator", result.operPerson);
                        mySqlCom.Parameters.AddWithValue("?operatetime", result.operateTime);
                        mySqlCom.Parameters.AddWithValue("?bisairiqi", result.bisairiqi); 
                        mySqlCom.Parameters.AddWithValue("?url", result.url);
                        total = Convert.ToInt32(mySqlCom.ExecuteScalar());
                    }
                }
            }

            //return total == 0 ? true : false;
        }
    }
}
