using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldenPigs.Entity;
using MySql.Data.MySqlClient;

namespace GoldenPigs.DAL
{
    public class BasketRqResultDAL
    {
        public void InsertBasketRqResultList(List<BasketRqResult> results)
        {
            int total = 0;
            string conStr = "server=localhost;User Id=root;database=aicai;" +
                            "Password=root;Character Set=utf8;";
            using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
            {
                mySqlCon.Open();
                foreach (BasketRqResult result in results)
                {
                    using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                    {
                        //                        string sqlStr = @"
                        //
                        //INSERT INTO basketyuce(title, result,zhudui,kedui,xingqi,bianhao,operator,operatetime,bisairiqi,url)
                        //VALUES(?title, ?result,?zhudui,?kedui,?xingqi,?bianhao,?operator,?operatetime,?bisairiqi,?url)
                        //";
                        string sqlStr =
                            @"INSERT INTO basketrqresult(riqi,changci,saishi,zhuangtai,zhudui,bifen,kedui,zhusheng,rangfen,kesheng,rqresult,creator,createtime)
SELECT ?riqi,?changci,?saishi,?zhuangtai,?zhudui,?bifen,?kedui,?zhusheng,?rangfen,?kesheng,?rqresult,?creator,?createtime
FROM DUAL WHERE NOT EXISTS(SELECT * FROM basketrqresult WHERE riqi = ?riqi AND changci = ?changci);
";

                        mySqlCom.CommandText = sqlStr;
                        mySqlCom.Parameters.AddWithValue("?riqi", result.riqi);
                        mySqlCom.Parameters.AddWithValue("?changci", result.changci);
                        mySqlCom.Parameters.AddWithValue("?saishi", result.saishi);
                        mySqlCom.Parameters.AddWithValue("?zhuangtai", result.zhuangtai);
                        mySqlCom.Parameters.AddWithValue("?zhudui", result.zhudui);
                        mySqlCom.Parameters.AddWithValue("?bifen", result.bifen);
                        mySqlCom.Parameters.AddWithValue("?kedui", result.kedui);
                        mySqlCom.Parameters.AddWithValue("?zhusheng", result.zhusheng);
                        mySqlCom.Parameters.AddWithValue("?rangfen", result.rangfen);
                        mySqlCom.Parameters.AddWithValue("?kesheng", result.kesheng);
                        mySqlCom.Parameters.AddWithValue("?rqresult", result.rqresult);
                        mySqlCom.Parameters.AddWithValue("?creator", result.creator);
                        mySqlCom.Parameters.AddWithValue("?createtime", result.createtime);
                        Convert.ToInt32(mySqlCom.ExecuteScalar());
                    }
                }
            }

            //return total == 0 ? true : false;
        }
    }
}
