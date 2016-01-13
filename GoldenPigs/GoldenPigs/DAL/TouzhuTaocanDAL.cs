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
    public class TouzhuTaocanDAL
    {
        public int InsertTouzhutaocan(TouzhuTaocan touzhutaocan)
        {
            try
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
                        string sqlStr = @"
insert into touzhu_taocan(
touzhuleixing,touzhumingcheng,touzhuid,touzhubeishu,touzhujin,jiangjin,lucky,touzhuqishu,touzhushijian,operatetime,operator
)
VALUES
(
?touzhuleixing,?touzhumingcheng,?touzhuid,?touzhubeishu,?touzhujin,?jiangjin,?lucky,?touzhuqishu,?touzhushijian,?operatetime,?operator
)
";
                        mySqlCom.CommandText = sqlStr;

                        mySqlCom.Parameters.AddWithValue("?touzhuleixing", touzhutaocan.Touzhuleixing);
                        mySqlCom.Parameters.AddWithValue("?touzhumingcheng", touzhutaocan.Touzhumingcheng);
                        mySqlCom.Parameters.AddWithValue("?touzhuid", touzhutaocan.Touzhuid);
                        mySqlCom.Parameters.AddWithValue("?touzhubeishu", touzhutaocan.Touzhubeishu);
                        mySqlCom.Parameters.AddWithValue("?touzhujin", touzhutaocan.Touzhujin);
                        mySqlCom.Parameters.AddWithValue("?jiangjin", touzhutaocan.Jiangjin);
                        mySqlCom.Parameters.AddWithValue("?lucky", touzhutaocan.Lucky);
                        mySqlCom.Parameters.AddWithValue("?touzhuqishu", touzhutaocan.Touzhuqishu);
                        mySqlCom.Parameters.AddWithValue("?touzhushijian", touzhutaocan.Touzhushijian);
                        mySqlCom.Parameters.AddWithValue("?operator", touzhutaocan.Operator);
                        mySqlCom.Parameters.AddWithValue("?operatetime", touzhutaocan.OperateTime);
                        mySqlCom.ExecuteScalar();
                        return 0;

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return -1;
          
        }
    }
}
