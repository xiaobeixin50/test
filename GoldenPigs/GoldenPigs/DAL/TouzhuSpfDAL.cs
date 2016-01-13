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
    public class TouzhuSpfDAL
    {
        public int InsertTouzhuSpf(TouzhuSpf spf)
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
                        
                        string sqlStr = @"
insert into touzhu_spf
(
batchid,riqi,zhudui,kedui,result,peilv,beishu,rangqiu,lucky,jiangjin,operator,operatetime

)
values
(
?batchid,?riqi,?zhudui,?kedui,?result,?peilv,?beishu,?rangqiu,?lucky,?jiangjin,?operator,?operatetime
)
";
                        mySqlCom.CommandText = sqlStr;
                        mySqlCom.Parameters.AddWithValue("?batchid", spf.BatchID);
                        mySqlCom.Parameters.AddWithValue("?riqi", spf.Riqi);
                        mySqlCom.Parameters.AddWithValue("?zhudui", spf.Zhudui);
                        mySqlCom.Parameters.AddWithValue("?kedui", spf.Kedui);
                        mySqlCom.Parameters.AddWithValue("?result", spf.Result);
                        mySqlCom.Parameters.AddWithValue("?peilv", spf.Peilv);
                        mySqlCom.Parameters.AddWithValue("?beishu", spf.Beishu);
                        mySqlCom.Parameters.AddWithValue("?rangqiu", spf.Rangqiu);
                        mySqlCom.Parameters.AddWithValue("?lucky", spf.Lucky);
                        mySqlCom.Parameters.AddWithValue("?jiangjin", spf.Jiangjin);
                        mySqlCom.Parameters.AddWithValue("?operator", spf.Operator);
                        mySqlCom.Parameters.AddWithValue("?operatetime", spf.OperateTime);
                        mySqlCom.ExecuteScalar();
                        return 0;

                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            
        }

        public DataSet SearchTouzhuSpf(string riqi)
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
                    string sql = @"select riqi ,zhudui,kedui,result,peilv,beishu,rangqiu,lucky,jiangjin,luckydetail,batchid,id from touzhu_spf where 1=1 ";
                    if (!String.IsNullOrEmpty(riqi))
                    {
                        sql = sql + " and operatetime >= '" + riqi + "' and operatetime <= '" + riqi + " 23:59:59'";
                    }
                    //if (liansai != "全部")
                    //{
                    //    string sqlStr = @"Select bianhao,liansai,zhudui,kedui,rangqiu,shengsp,pingsp,fusp,riqi from peilv where riqi=?riqi and liansai = ?liansan order by bianhao ";
                    //    mySqlCom.CommandText = sqlStr;
                    //    mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    //    mySqlCom.Parameters.AddWithValue("?liansan", liansai);
                    //}
                    //else
                    //{
                    //    string sqlStr = @"Select bianhao,liansai,zhudui,kedui,rangqiu,shengsp,pingsp,fusp,riqi from peilv  where riqi=?riqi order by bianhao";
                    //    mySqlCom.CommandText = sqlStr;
                    //    mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    //}
                    mySqlCom.CommandText = sql;
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);
                }
            }
            return ds;
        }

        public void UpdateZhongjiangResult(String riqi)
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
                    string sql = @"select id, riqi ,zhudui,kedui,result,peilv,beishu,rangqiu,lucky,jiangjin,luckydetail from touzhu_spf where 1=1 ";
                    if (!String.IsNullOrEmpty(riqi))
                    {
                        sql = sql + " and operatetime >= '" + riqi + "' and operatetime <= '" + riqi + " 23:59:59'";
                    }
                    mySqlCom.CommandText = sql;
                    da.SelectCommand = mySqlCom;
                    da.Fill(ds);
                }
            }
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string riqiresult = row["riqi"].ToString();
                string peilv = row["peilv"].ToString();
                string zhudui = row["zhudui"].ToString();
                string kedui = row["kedui"].ToString();
                string lucky = row["lucky"].ToString();
                string rangqiu = row["rangqiu"].ToString();
                string result = row["result"].ToString();
                string id = row["id"].ToString();
                if (lucky == "-1")
                {

                    string[] riqis = riqiresult.Split(',');
                    string[] zhuduis = zhudui.Split(',');
                    string[] keduis = kedui.Split(',');
                    string[] rangqius = rangqiu.Split(',');
                    string[] results = result.Split(',');
                    string[] peilvs = peilv.Split(',');
                    bool zhongjiang = true;
                    double peilvTotal = 1.0;
                    string resultTotal = "";
                    for(int i = 0; i<riqis.Length; i++)
                    {
                        string riqiPara = riqis[i];
                        string zhuduiPara = zhuduis[i];
                        string keduiPara = keduis[i];
                        string rangqiuPara = rangqius[i];
                        string resultPara = results[i];
                        string peilvPara = peilvs[i];
                        peilvTotal *= Convert.ToDouble(peilvPara);
                        int luckyResult = IsLucky(riqiPara, zhuduiPara, keduiPara, rangqiuPara, resultPara);
                        if (luckyResult==1)
                        {
                            if(zhongjiang){
                                zhongjiang = true;
                            }
                            resultTotal += "中,";
                            
                        }
                        else if(luckyResult == 0)
                        {                            
                            zhongjiang = false;  
                            resultTotal += "不中,";
                        }
                        else if (luckyResult == -1)
                        {
                            zhongjiang = false;
                            resultTotal += "未开奖,";
                        }
                    }
                    UpdateLucky(id, zhongjiang, peilvTotal,resultTotal);
                    
                   // string 
                }

            }
        }
        public void UpdateLucky(string id, bool zhongjiang, double peilv, string luckydetail)
        {
            if (!zhongjiang)
            {
                peilv = 0.0;
            }
              string conStr = "server=localhost;User Id=root;database=aicai;" +
                          "Password=root;Character Set=utf8;";

              using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
              {
                  mySqlCon.Open();

                  using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                  {
                      string sql = @"
UPDATE touzhu_spf
SET Lucky = ?Lucky,
Luckydetail = ?luckydetail,
 Jiangjin = Beishu * 2 *?peilv
WHERE id = ?id
";
                      mySqlCom.CommandText = sql;
                      mySqlCom.Parameters.AddWithValue("?peilv", peilv);
                      mySqlCom.Parameters.AddWithValue("?Lucky", zhongjiang);
                      mySqlCom.Parameters.AddWithValue("?Luckydetail", luckydetail);
                      mySqlCom.Parameters.AddWithValue("?id", id);
                      mySqlCom.ExecuteScalar();
                  }
              }
        }

        public int IsLucky(string riqi,string zhudui, string kedui, string rangqiu,string result)
        {
            //映射结果到数字
            if (result == "胜")
            {
                result = "3";
            }
            else if (result == "平")
            {
                result = "1";

            }
            else if (result == "负")
            {
                result = "0";
            }

             string conStr = "server=localhost;User Id=root;database=aicai;" +
                          "Password=root;Character Set=utf8;";
             DataSet ds = new DataSet();
             using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
             {
                 mySqlCon.Open();

                 using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                 {
//                     string sql = @"SELECT
//	count(*)
//FROM
//	kaijiang
//WHERE
//	riqi = ?riqi
//AND zhudui = ?zhudui
//AND kedui = ?kedui";

//                     if (rangqiu == "0")
//                     {
//                         sql = sql + " AND spfresult = ?spfresult";
//                         mySqlCom.CommandText = sql;
//                         mySqlCom.Parameters.AddWithValue("?riqi", riqi);
//                         mySqlCom.Parameters.AddWithValue("?zhudui", zhudui);
//                         mySqlCom.Parameters.AddWithValue("?kedui", kedui);
//                         mySqlCom.Parameters.AddWithValue("?spfresult", result);
//                     }
//                     else
//                     {
//                         sql = sql + " AND RqspfResult = ?RqspfResult";
//                         mySqlCom.CommandText = sql;
//                         mySqlCom.Parameters.AddWithValue("?riqi", riqi);
//                         mySqlCom.Parameters.AddWithValue("?zhudui", zhudui);
//                         mySqlCom.Parameters.AddWithValue("?kedui", kedui);
//                         mySqlCom.Parameters.AddWithValue("?RqspfResult", result);


//                     }


                     //object obj = mySqlCom.ExecuteScalar();
                     //int count = Convert.ToInt32(obj);
                     //if(count >0)
                     //{
                     //    return 1;
                     //}
                     //return 0;

                     try
                     {
                         MySqlDataAdapter da = new MySqlDataAdapter();
                         string sql = @"
SELECT
	spfresult,RqspfResult
FROM
	kaijiang
WHERE
	riqi = ?riqi
AND zhudui = ?zhudui
AND kedui = ?kedui";
                         mySqlCom.CommandText = sql;
                         mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                         mySqlCom.Parameters.AddWithValue("?zhudui", zhudui);
                         mySqlCom.Parameters.AddWithValue("?kedui", kedui);
                         //修改逻辑，如果查不到数据提示“未开奖”
                         da.SelectCommand = mySqlCom;
                         da.Fill(ds);
                     }
                     catch (Exception ex)
                     {
                         Console.WriteLine(ex.Message);
                     }
                    
                   
                 }
             
             }
             if (ds.Tables[0].Rows.Count == 0)
             {
                 return -1;
             }
             DataRow row = ds.Tables[0].Rows[0];
             String spfResult = row["spfresult"].ToString();
             string rqspfResult = row["RqspfResult"].ToString();
             if (rangqiu == "0")
             {
                 if (result == spfResult)
                 {
                     return 1;
                 }
             }
             else
             {
                 if (result == rqspfResult)
                 {
                     return 1;
                 }
             }
            return 0;

        }

    }
}
