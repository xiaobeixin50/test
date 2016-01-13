using GoldenPigs.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPigs.DAL
{
    public class BasketResultDAL
    {
        public void InsertBasketResultList(List<BasketResult> results)
        {
            int total = 0;
            string conStr = "server=localhost;User Id=root;database=aicai;" +
                            "Password=root;Character Set=utf8;";
            using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
            {
                mySqlCon.Open();
                foreach (BasketResult result in results)
                {
                    using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                    {
                        string sqlStr = @"
DELETE FROM basketresult
WHERE saishibianhao = ?saishibianhao
AND bisaishijian = ?bisaishijian;
INSERT INTO basketresult(saishi,saishibianhao,bisaishijian,kedui,zhudui,zhongchangbifen	,creator,createtime,riqi,bianhao)
VALUES(?saishi,?saishibianhao,?bisaishijian,?kedui,?zhudui,?zhongchangbifen,?creator,?createtime,?riqi,?bianhao)
";

                        mySqlCom.CommandText = sqlStr;
                        mySqlCom.Parameters.AddWithValue("?saishi", result.saishi);
                        mySqlCom.Parameters.AddWithValue("?saishibianhao", result.saishibianhao);
                        mySqlCom.Parameters.AddWithValue("?bisaishijian", result.bisaishijian);
                        mySqlCom.Parameters.AddWithValue("?kedui", result.kedui);
                        mySqlCom.Parameters.AddWithValue("?zhudui", result.zhudui);
                        mySqlCom.Parameters.AddWithValue("?zhongchangbifen", result.zhongchangbifen);
                        mySqlCom.Parameters.AddWithValue("?creator", result.creator);
                        mySqlCom.Parameters.AddWithValue("?createtime", result.createtime);

                        mySqlCom.Parameters.AddWithValue("?riqi", result.riqi);
                        mySqlCom.Parameters.AddWithValue("?bianhao", result.bianhao);


                        total = Convert.ToInt32(mySqlCom.ExecuteScalar());
                    }
                }
            }

            //return total == 0 ? true : false;
        }
    }
}