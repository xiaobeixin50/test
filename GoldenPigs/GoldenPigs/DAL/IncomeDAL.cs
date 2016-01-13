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
    public class IncomeDAL
    {
        public DataSet GetIncomeDetails(string incomeType)
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
SELECT * FROM income";
                   
                    if (incomeType != "全部")
                    {
                        sqlStr = @"SELECT * FROM income where incomeType = ?incomeType";

                        mySqlCom.CommandText = sqlStr;
                        mySqlCom.Parameters.AddWithValue("?incometype", incomeType);
                    }
                    else
                    {
                        mySqlCom.CommandText = sqlStr;
                    }
                    try
                    {
                        da.SelectCommand = mySqlCom;
                        da.Fill(ds);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                   

                }
            }
            return ds;
        }
        public bool InsertIncome(Income income)
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
                    string sqlStr = @"insert into income(amount,incometype,operator,operatetime,touzhuid,touzhutype)
value(?amount,?incometype,?operator,?operatetime,?touzhuid,?touzhutype)
";

                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?amount", income.Amount);
                    mySqlCom.Parameters.AddWithValue("?incometype", income.IncomeType);
                    mySqlCom.Parameters.AddWithValue("?operator", income.Operator);
                    mySqlCom.Parameters.AddWithValue("?operatetime", income.OperateTime);
                    mySqlCom.Parameters.AddWithValue("?touzhuid", income.id);
                    mySqlCom.Parameters.AddWithValue("?touzhutype", income.TouzhuType);

                    total = Convert.ToInt32(mySqlCom.ExecuteScalar());
                }
            }

            return total == 0 ? true : false;

        }


        public double GetTotalAmount()
        {
            double total = 0;
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
                    string sqlStr = @"select IFNULL(sum(amount),0) as TotalMoney from income;";

                    mySqlCom.CommandText = sqlStr;                 

                    total = Convert.ToDouble(mySqlCom.ExecuteScalar());
                }
            }

            return total ;
        }

        public double GetTotalAmount(string incomeType)
        {
            double total = 0;
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
                    string sqlStr = @"select IFNULL(sum(amount),0) as TotalMoney from income where incometype=?incometype;";

                    mySqlCom.CommandText = sqlStr;
                    mySqlCom.Parameters.AddWithValue("?incometype", incomeType);
                    total = Convert.ToDouble(mySqlCom.ExecuteScalar());
                }
            }

            return total;
        }

        /// <summary>
        /// 1表示已派奖，0表示未派奖
        /// </summary>
        /// <param name="income"></param>
        /// <returns></returns>
        public int HasPaijiang(Income income)
        {

            DataSet ds = new DataSet();
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
//                    string sqlStr = @"select * from income where TouzhuID = ?touzhuid
//and TouzhuType = ?touzhutype";
                    string sqlStr = @"select * from income where touzhuid = ?touzhuid and incometype = ?incometype";
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    mySqlCom.CommandText = sqlStr;
                    da.SelectCommand = mySqlCom;
                    mySqlCom.Parameters.AddWithValue("?touzhuid", income.id);
                    mySqlCom.Parameters.AddWithValue("?incometype", income.IncomeType);
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        return 0;
                    }
                }
            }

            return 1;
        }
    }
}
