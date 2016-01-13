using GoldenPigs.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPigs.DAL
{
    public class YuceLanqiuDAL
    {

        public static void InsertKaijiangLanqiuList(List<Kaijianglanqiu> results)
        {
            int total = 0;
            string conStr = "server=localhost;User Id=root;database=aicai;" +
                            "Password=root;Character Set=utf8;";
            using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
            {
                mySqlCon.Open();
                foreach (Kaijianglanqiu result in results)
                {
                    using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                    {
                        //                        string sqlStr = @"
                        //
                        //INSERT INTO basketyuce(title, result,zhudui,kedui,xingqi,bianhao,operator,operatetime,bisairiqi,url)
                        //VALUES(?title, ?result,?zhudui,?kedui,?xingqi,?bianhao,?operator,?operatetime,?bisairiqi,?url)
                        //";
                        try
                        {

                        
                        string sqlStr =
                            @"INSERT INTO kaijiang_lanqiu(riqi,liansai,bianhao,bisaishijian,zhudui,kedui,zhufen,kefen,fencha,zongfen,rangfen,zhufusp,zhushengsp,yushezongfen,dafensp,xiaofensp,rqresult,daxiaofenresult,operatetime)
VALUES(?riqi,?liansai,?bianhao,?bisaishijian,?zhudui,?kedui,?zhufen,?kefen,?fencha,?zongfen,?rangfen,?zhufusp,?zhushengsp,?yushezongfen,?dafensp,?xiaofensp,?rqresult,?daxiaofenresult,?operatetime);
";

                        mySqlCom.CommandText = sqlStr;
                        mySqlCom.Parameters.AddWithValue("?riqi", result.riqi);
                        mySqlCom.Parameters.AddWithValue("?liansai", result.liansai);
                        mySqlCom.Parameters.AddWithValue("?bianhao", result.bianhao);
                        mySqlCom.Parameters.AddWithValue("?bisaishijian", result.bisaishijian);
                        mySqlCom.Parameters.AddWithValue("?zhudui", result.zhudui);
                        mySqlCom.Parameters.AddWithValue("?kedui", result.kedui);
                        mySqlCom.Parameters.AddWithValue("?zhufen", result.zhufen);
                        mySqlCom.Parameters.AddWithValue("?kefen", result.kefen);
                        mySqlCom.Parameters.AddWithValue("?fencha", result.fencha);
                        mySqlCom.Parameters.AddWithValue("?zongfen", result.zongfen);
                        mySqlCom.Parameters.AddWithValue("?rangfen", result.rangfen);
                        mySqlCom.Parameters.AddWithValue("?zhufusp", result.zhufusp);
                        mySqlCom.Parameters.AddWithValue("?zhushengsp", result.zhushengsp);
                        mySqlCom.Parameters.AddWithValue("?yushezongfen", result.yushezongfen);
                        mySqlCom.Parameters.AddWithValue("?dafensp", result.dafensp);
                        mySqlCom.Parameters.AddWithValue("?xiaofensp", result.xiaofensp);
                        mySqlCom.Parameters.AddWithValue("?rqresult", result.rqresult);
                        mySqlCom.Parameters.AddWithValue("?daxiaofenresult", result.daxiaofenresult);
                        mySqlCom.Parameters.AddWithValue("?operatetime", result.operatetime);
                        total = Convert.ToInt32(mySqlCom.ExecuteScalar());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }

            //return total == 0 ? true : false;
        }

        public static void DeleteKaijiangLanqiu(string riqi)
        {
            string conStr = "server=localhost;User Id=root;database=aicai;" +
                           "Password=root;Character Set=utf8;";

            using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
            {
                mySqlCon.Open();
                using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                {
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    string sqlStr = @"delete from kaijiang_lanqiu where riqi = ?riqi";
                    mySqlCom.CommandText = sqlStr;

                    mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    mySqlCom.ExecuteScalar();

                }
            }
        }

        public static void InsertYuceLanqiuList(List<Yucelanqiu> results)
        {
            int total = 0;
            string conStr = "server=localhost;User Id=root;database=aicai;" +
                            "Password=root;Character Set=utf8;";
            using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
            {
                mySqlCon.Open();
                foreach (Yucelanqiu result in results)
                {
                    using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                    {
                        //                        string sqlStr = @"
                        //
                        //INSERT INTO basketyuce(title, result,zhudui,kedui,xingqi,bianhao,operator,operatetime,bisairiqi,url)
                        //VALUES(?title, ?result,?zhudui,?kedui,?xingqi,?bianhao,?operator,?operatetime,?bisairiqi,?url)
                        //";
                        try
                        {


                            string sqlStr =
                                @"
insert into yuce_lanqiu(riqi,bianhao,liansai,dayofweek,zhudui,kedui,daxiaofenresult,rqresult,operatetime,title,url,operperson,spfrawresult)
SELECT ?riqi,?bianhao,?liansai,?dayofweek,?zhudui,?kedui,?daxiaofenresult,?rqresult,?operatetime,?title,?url,?operperson,?spfrawresult
FROM DUAL WHERE NOT EXISTS (SELECT * FROM yuce_lanqiu WHERE url = ?url)
";

                            mySqlCom.CommandText = sqlStr;
                            mySqlCom.Parameters.AddWithValue("?riqi", result.riqi);
                            mySqlCom.Parameters.AddWithValue("?liansai", result.liansai);
                            mySqlCom.Parameters.AddWithValue("?bianhao", result.bianhao);
                            mySqlCom.Parameters.AddWithValue("?dayofweek", result.dayofweek);
                            mySqlCom.Parameters.AddWithValue("?zhudui", result.zhudui);
                            mySqlCom.Parameters.AddWithValue("?kedui", result.kedui);
                            mySqlCom.Parameters.AddWithValue("?daxiaofenresult", result.daxiaofenresult);
                            mySqlCom.Parameters.AddWithValue("?rqresult", result.rqresult);
                            mySqlCom.Parameters.AddWithValue("?operatetime", result.operatetime);
                            mySqlCom.Parameters.AddWithValue("?title", result.title);
                            mySqlCom.Parameters.AddWithValue("?url", result.url);
                            mySqlCom.Parameters.AddWithValue("?operperson", result.operperson);
                            mySqlCom.Parameters.AddWithValue("?spfrawresult", result.spfrawresult);
                            total = Convert.ToInt32(mySqlCom.ExecuteScalar());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }

            //return total == 0 ? true : false;
        }

        public static void DeleteYuceLanqiu(string riqi)
        {
            string conStr = "server=localhost;User Id=root;database=aicai;" +
                           "Password=root;Character Set=utf8;";

            using (MySqlConnection mySqlCon = new MySqlConnection(conStr))
            {
                mySqlCon.Open();
                using (MySqlCommand mySqlCom = mySqlCon.CreateCommand())
                {
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    string sqlStr = @"delete from yuce_lanqiu where riqi = ?riqi";
                    mySqlCom.CommandText = sqlStr;

                    mySqlCom.Parameters.AddWithValue("?riqi", riqi);
                    mySqlCom.ExecuteScalar();

                }
            }
        }
    }
}
