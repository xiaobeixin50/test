using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPigs.DAL
{
    public class StrategyDAL
    {
        

        static string conStr = "server=localhost;User Id=root;database=aicai;" +
                            "Password=root;Character Set=utf8;";


        public static DataTable GetStrategy1Data()
        {
            String sql = @"select yuce_app.*,yuce_app.riqi as riqi1, kaijiang.shengsp from yuce_app inner join kaijiang on yuce_app.riqi = kaijiang.riqi and yuce_app.bianhao = kaijiang.bianhao
  where yuce_app.spfresult = '3'  and yuce_app.rangqiushu = '0' and kaijiang.xingqi not in ( '星期六', '星期日') order by riqi, bianhao;
";
            return binding(sql, "strategy1");
        }

        public static DataTable GetStrategy5Data()
        {
            String sql = @"select * from kaijiang where shengspfracint = 15 order by riqi ;";
            return binding(sql, "strategy5");
        }

        public static DataTable GetStrategy9Data()
        {
            String sql = @"select * from kaijiang where shengspfracint = 24 order by riqi ;";
            return binding(sql, "strategy9");
        }

        public static DataTable GetStrategy13Data()
        {
            String sql = @"select kaijiang.riqi,kaijiang.bianhao,kaijiang.spfresult, 
case kaijiang.spfresult
when 3 then 1
else 2
end  as lucky,kaijiang.shengsp  from superbifa inner join kaijiang 
on superbifa.riqi = kaijiang.riqi and superbifa.bianhao = kaijiang.bianhao
where bifajiawei_sheng < 1.7 and bifajiawei_sheng > 0 order by superbifa.riqi  ,superbifa.bianhao;
";
            return binding(sql, "strategy13");
        }

        public static DataTable GetStrategy15Data()
        {
            String sql = @"select yuce_app.*,yuce_app.riqi as riqi1, kaijiang.shengsp from yuce_app inner join kaijiang on yuce_app.riqi = kaijiang.riqi and yuce_app.bianhao = kaijiang.bianhao
  where yuce_app.spfresult = '3'   order by riqi, bianhao;
";
            return binding(sql, "strategy1");
        }

        public static DataTable GetStrategy14Data()
        {
            String sql = @"select yuce_app.*, kaijiang.shengsp,kaijiang.pingsp,kaijiang.fusp from yuce_app inner join kaijiang on yuce_app.riqi = kaijiang.riqi and yuce_app.bianhao = kaijiang.bianhao
inner join yuce_samuel on yuce_app.riqi = yuce_samuel.riqi and yuce_app.bianhao = yuce_samuel.bianhao
where yuce_app.rangqiushu = 0 and yuce_app.spfresult in ('3','1','0') and yuce_app.spfresult = yuce_samuel.spfresult   order by yuce_app.riqi, yuce_app.bianhao;
";
            return binding(sql, "strategy1");
        }

        public static DataTable GetStrategy17Data(DateTime startdate)
        {
            String sql = @"select yuce_app.*,yuce_app.riqi as riqi1, kaijiang.shengsp from yuce_app inner join kaijiang on yuce_app.riqi = kaijiang.riqi and yuce_app.bianhao = kaijiang.bianhao
  where yuce_app.spfresult = '3'  and yuce_app.rangqiushu = '0'  and yuce_app.riqi > '" + startdate.ToString("yyyy-MM-dd") + "' order by yuce_app.riqi, yuce_app.bianhao";

            return binding(sql, "strategy1");
        }


        public static DataTable GetAnalysis1Data()
        {
            String sql = @"
select lucky, yuce_app.riqi,count(*) as luckycount from yuce_app inner join kaijiang on yuce_app.riqi = kaijiang.riqi and yuce_app.bianhao = kaijiang.bianhao
group by yuce_app.riqi,lucky 
order by riqi ;";
            return binding(sql, "analysis1");
        }

        public static DataTable GetPeilvAnalysisData(string riqi)
        {
            String sql = @"select * from kaijiang where riqi ='" + riqi + "'"; ;
            return binding(sql, "peilvanalysis");
        }
        public static DataTable GetPeilvGroupData(int shengspfracint, int pingspfracint, int fuspfracint, string riqi )
        {
            String sql = @"select spfresult, count(*)  from kaijiang where shengspfracint =" + shengspfracint + " and pingspfracint = " + pingspfracint + " and fuspfracint = " + fuspfracint + " and riqi < '" + riqi +  "' group by spfresult" ;
            return binding(sql, "peilvanalysis");
        }

        public static DataTable GetRqPeilvGroupData(int rqshengspfracint, int rqpingspfracint, int rqfuspfracint, string riqi )
        {
            String sql = @"select rqspfresult,count(*) from kaijiang where rqshengspfracint =" + rqshengspfracint + " and rqpingspfracint = " + rqpingspfracint + " and rqfuspfracint = " + rqfuspfracint + " and riqi < '" + riqi + "' group by rqspfresult" ;
            return binding(sql, "peilvanalysis");
        }

        //获取最近一场spf的比赛
        public static DataTable GetLastSpfData(int shengspfracint, int pingspfracint, int fuspfracint, string riqi)
        {
            String sql = @"select *  from kaijiang where shengspfracint =" + shengspfracint + " and pingspfracint = " + pingspfracint + " and fuspfracint = " + fuspfracint + " and riqi < '" + riqi + "' order by riqi desc, bianhao desc";
            return binding(sql, "peilvanalysis");
        }

        //获取最近一场rqspf的比赛
        public static DataTable GetLastRqSpfData(int shengspfracint, int pingspfracint, int fuspfracint, string riqi)
        {
            String sql = @"select *  from kaijiang where rqshengspfracint =" + shengspfracint + " and rqpingspfracint = " + pingspfracint + " and rqfuspfracint = " + fuspfracint + " and riqi < '" + riqi + "' order by riqi desc, bianhao desc";
            return binding(sql, "peilvanalysis");
        }
        //helper method
        static DataTable binding(string sqlsel, string tablename)
        {
            using (MySqlConnection conn = new MySqlConnection(conStr))
            {
                conn.Open();
                DataSet ds = new DataSet();
                MySqlDataAdapter da = new MySqlDataAdapter(sqlsel, conn);
                da.Fill(ds, tablename);
                DataTable dt = ds.Tables[tablename];
                conn.Close();
                return dt;
            }
        }



    }
}
