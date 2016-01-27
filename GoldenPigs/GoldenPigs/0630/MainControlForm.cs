using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GoldenPigs.DAL;
using GoldenPigs.Entity;
using GoldenPigs.Entity.JsonObject;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Globalization;
using System.Json;

namespace GoldenPigs._0630
{
    public partial class MainControlForm : Form
    {
        public MainControlForm()
        {
            InitializeComponent();
        }

        private void btnImportKaijianMul_Click(object sender, EventArgs e)
        {
            DateTime StartDate = dtpStartDate.Value.Date;
            DateTime EndDate = dtpEndDate.Value.Date;
            int count = 0;
            while (StartDate <= EndDate)
            {
                //先删除当前日期的开奖结果，避免重复
                string riqi = StartDate.Date.ToString("yyyy-MM-dd");
                new KaijiangDAL().DeleteKaijiang(riqi);

                ImportKaijiang(StartDate);
                StartDate = StartDate.AddDays(1);
                count++;
            }
            MessageBox.Show("导入成功,导入了" + count + "条开奖结果");
        }
        private void ImportKaijiang(DateTime importdate)
        {
            try
            {
                DateTime importDate = importdate;
                string url = GetKaijiangUrl(importDate);
                string jsonData = GetHtmlFromUrl(url);

                Dictionary<string, object> dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);
                String raceList = dict["raceList"].ToString();
                Dictionary<string, object> dict2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(raceList);
                String weekDay = dict["weekDay"].ToString();
                string weekDate = dict["weekDate"].ToString();

                foreach (string key in dict2.Keys)
                {
                    string value = dict2[key].ToString();
                    Race race = JsonConvert.DeserializeObject<Race>(value);
                    Kaijiang kaijiang = new Kaijiang(key, race);
                    kaijiang.Xingqi = weekDay;
                    kaijiang.Riqi = weekDate;
                    
                    //DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();

                    //dtFormat.ShortDatePattern = "yyyyMMdd";
                    //dtFormat.LongDatePattern = "yyyyMMdd hh:mm";
                    DateTime temp = DateTime.ParseExact("20" + race.matchDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    string bisaishijian = temp.ToString("yyyy-MM-dd") + " " + race.matchTime + ":00";
                    //kaijiang.Bisaishijian = DateTime.ParseExact(bisaishijian, "yyyyMMdd hh:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                    kaijiang.Bisaishijian = Convert.ToDateTime(bisaishijian);
                    kaijiang.EndTime = Convert.ToDateTime(race.endTime);
                    new KaijiangDAL().InsertKaijiang(kaijiang);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private string GetKaijiangUrl(DateTime date)
        {
            string importDate = date.ToString("yyMMdd");
            string urlTemp =
                @"http://www.aicai.com/lotnew/jc/getMatchByDate.htm?lotteryType=jczq&cate=gd&dataStr={0}&time=1408156959982";
            return string.Format(urlTemp, importDate);
        }

        private String GetHtmlFromUrl(string url)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                Stream stream = wc.OpenRead(url);
                StreamReader sr = new StreamReader(stream);
                string strLine = "";
                StringBuilder sb = new StringBuilder();
                while ((strLine = sr.ReadLine()) != null)
                {
                    sb.Append(strLine);
                }
                sr.Close();
                wc.Dispose();
                return sb.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        private void btnImportBifaIndex_Click(object sender, EventArgs e)
        {

            DateTime StartDate = dtpStartDate.Value;
            DateTime EndDate = dtpEndDate.Value;
            int count = 0;
            while (StartDate <= EndDate)
            {
                //先删除当前日期的开奖结果，避免重复
                string riqi = StartDate.Date.ToString("yyyy-MM-dd");
                //new KaijiangDAL().DeleteKaijiang(riqi);
                new SuperBifaDAL().DeleteBifa(riqi);

                ImportBifa(StartDate);
                StartDate = StartDate.AddDays(1);
                count++;
            }
            MessageBox.Show("导入成功,导入了" + count + "条必发结果");      

        }

        private void ImportBifa(DateTime importDate)
        {
            string bifaUrl = "http://live.aicai.com/jsbf/timelyscore!dynamicBfDataFromPage.htm?lotteryType=zc&issue=" + importDate.ToString("yyyyMMdd");
            //string bifaUrl = "http://live.aicai.com/jsbf/timelyscore!dynamicBfDataFromPage.htm?lotteryType=zc&issue=20150628";
            string result = GetHtmlFromUrl(bifaUrl);

            BifaJsonEntity bifaJsonEntity = JsonConvert.DeserializeObject<BifaJsonEntity>(result);
            //MessageBox.Show(bifaJsonEntity.result.bf_page);

            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(bifaJsonEntity.result.bf_page);

            HtmlNode rootNode = document.DocumentNode;

            SuperBifa bifa = new SuperBifa();
            bifa.riqi = importDate.ToString("yyyy-MM-dd");
            //HtmlNode parentNode = rootNode.SelectSingleNode(@"//div[@id='jq_bf_refresh_time_div']");
            HtmlNodeCollection bifaNodes = rootNode.SelectNodes("./div[@class='md_data_box css_league']");

            foreach (HtmlNode bifaNode in bifaNodes)
            {
                //HtmlNode div = bifaNode.SelectSingleNode("./div[@class='md_data_box css_league']");
                //HtmlNode div2 = bifaNode.SelectSingleNode("./div/div");
                //HtmlNode div3 = bifaNode.SelectSingleNode("./div/div/span");
                //HtmlNode div4 = bifaNode.SelectSingleNode("./div/div/span/span[@class='c_yellow']");

                string xingqiandbianhao = bifaNode.SelectSingleNode(@"./div/span/span[@class='c_yellow']").InnerText;
                string liansai = bifaNode.SelectSingleNode(@"./div/span[@class='c_dgreen']").InnerText;
                //这里得增加一个判断，比分是否有值，没有的话以 -1:-1为默认值
                string zhudui = "";
                string bifen = "";
                string kedui = "";
                if (bifaNode.SelectNodes(@"./div/span[@class='c_yellow']/span").Count == 3)
                {
                    zhudui = bifaNode.SelectNodes(@"./div/span[@class='c_yellow']/span")[0].InnerText;
                    bifen = bifaNode.SelectNodes(@"./div/span[@class='c_yellow']/span")[1].InnerText;
                    kedui = bifaNode.SelectNodes(@"./div/span[@class='c_yellow']/span")[2].InnerText;
                }
                else
                {
                    zhudui = bifaNode.SelectNodes(@"./div/span[@class='c_yellow']/span")[0].InnerText;
                    bifen = "-1:-1";
                    kedui = bifaNode.SelectNodes(@"./div/span[@class='c_yellow']/span")[1].InnerText;
                }
              

                string bisaishijian = bifaNode.SelectSingleNode("./div/span[@class='md_ks_time']/span").InnerText;

                HtmlNode tbody = bifaNode.SelectSingleNode("./div/div/table/tbody");
                HtmlNode tr1 = tbody.SelectNodes("./tr")[0];
                HtmlNodeCollection tds1 = tr1.SelectNodes("./td");
                string bifajiawei_sheng = tds1[1].InnerText;
                string bifazhishu_sheng = tds1[2].InnerText;
                string baijiaoupei_sheng = tds1[3].InnerText;

                HtmlNode tr2 = tbody.SelectNodes("./tr")[1];
                HtmlNodeCollection tds2 = tr2.SelectNodes("./td");
                string bifajiawei_ping = tds2[1].InnerText;
                string bifazhishu_ping = tds2[2].InnerText;
                string baijiaoupei_ping = tds2[3].InnerText;

                HtmlNode tr3 = tbody.SelectNodes("./tr")[2];
                HtmlNodeCollection tds3 = tr3.SelectNodes("./td");
                string bifajiawei_fu = tds3[1].InnerText;
                string bifazhishu_fu = tds3[2].InnerText;
                string baijiaoupei_fu = tds3[3].InnerText;

                string chengjiaoliang = bifaNode.SelectSingleNode("./div/div/div[@class='proba_total']/p/strong[@class='c_orange']").InnerText;

                string sheng = bifaNode.SelectNodes("./div/div/div[@class='proba_data']/p/span[@class='c_orange']")[0].InnerText;
                string ping = bifaNode.SelectNodes("./div/div/div[@class='proba_data']/p/span[@class='c_green']")[0].InnerText;
                string fu = bifaNode.SelectNodes("./div/div/div[@class='proba_data']/p/span[@class='c_blue']")[0].InnerText;

                string dae_sheng = bifaNode.SelectNodes("./div/div/div[@class='proba_data']/p/span[@class='c_orange']")[1].InnerText;
                string dae_ping = bifaNode.SelectNodes("./div/div/div[@class='proba_data']/p/span[@class='c_green']")[1].InnerText;
                string dae_fu = bifaNode.SelectNodes("./div/div/div[@class='proba_data']/p/span[@class='c_blue']")[1].InnerText;


                bifa.xingqi = GetXingqiFromData(xingqiandbianhao);
                bifa.bianhao = GetBianhaoFromData(xingqiandbianhao);

                bifa.liansai = liansai;
                bifa.zhudui = zhudui;
                bifa.kedui = kedui;
                bifa.bifen = bifen;
                bifa.kaisaishijian = bisaishijian;
                bifa.bifajiawei_sheng = Convert.ToDouble(bifajiawei_sheng);
                bifa.bifajiawei_ping = Convert.ToDouble(bifajiawei_ping);
                bifa.bifajiawei_fu = Convert.ToDouble(bifajiawei_fu);

                bifa.bifazhishu_sheng = Convert.ToDouble(bifazhishu_sheng);
                bifa.bifazhishu_ping = Convert.ToDouble(bifazhishu_ping);
                bifa.bifazhishu_fu = Convert.ToDouble(bifazhishu_fu);

                bifa.baijiaoupei_sheng = Convert.ToDouble(baijiaoupei_sheng);
                bifa.baijiaoupei_ping = Convert.ToDouble(baijiaoupei_ping);
                bifa.baijiaoupei_fu = Convert.ToDouble(baijiaoupei_fu);

                bifa.chengjiaoe = Convert.ToInt32(chengjiaoliang);

                bifa.sheng = Convert.ToDouble(sheng.Substring(0, sheng.Length - 1));
                bifa.ping = Convert.ToDouble(ping.Substring(0, ping.Length - 1));
                bifa.fu = Convert.ToDouble(fu.Substring(0, fu.Length - 1));

                bifa.dae_sheng = Convert.ToDouble(dae_sheng.Substring(0, dae_sheng.Length - 1));
                bifa.dae_ping = Convert.ToDouble(dae_ping.Substring(0, dae_ping.Length - 1));
                bifa.dae_fu = Convert.ToDouble(dae_fu.Substring(0, dae_fu.Length - 1));

                bifa.inserttime = DateTime.Now;

                string[] bifens = bifa.bifen.Split(':');
                int zhuScore = Convert.ToInt32(bifens[0]);
                int keScore = Convert.ToInt32(bifens[1]);

                if (zhuScore > keScore)
                {
                    //int prize_rank = GetRank(bifa.sheng, bifa.ping, bifa.fu);
                    //int dae_prize_rank = GetRank(bifa.dae_sheng, bifa.dae_ping, bifa.dae_fu);
                    int prize_rank = GetRank(3, bifa.sheng, bifa.sheng, bifa.ping, bifa.fu);
                    int dae_prize_rank = GetRank(3, bifa.dae_sheng, bifa.dae_sheng,bifa.dae_ping, bifa.dae_fu);
                    bifa.prize_rank = prize_rank;
                    bifa.dae_prize_rank = dae_prize_rank;
                }
                else if(zhuScore == keScore)
                {
                    //int prize_rank = GetRank(bifa.ping, bifa.sheng, bifa.fu);
                    //int dae_prize_rank = GetRank(bifa.dae_ping, bifa.dae_sheng, bifa.dae_fu);
                    int prize_rank = GetRank(1, bifa.ping, bifa.sheng, bifa.ping, bifa.fu);
                    int dae_prize_rank = GetRank(1, bifa.dae_ping, bifa.dae_sheng, bifa.dae_ping, bifa.dae_fu);
                    bifa.prize_rank = prize_rank;
                    bifa.dae_prize_rank = dae_prize_rank;
                }
                else
                {
                    //int prize_rank = GetRank(bifa.fu, bifa.ping, bifa.sheng);
                    //int dae_prize_rank = GetRank(bifa.dae_fu, bifa.dae_ping, bifa.dae_sheng);
                    int prize_rank = GetRank(0, bifa.fu, bifa.sheng, bifa.ping, bifa.fu);
                    int dae_prize_rank = GetRank(0, bifa.dae_fu, bifa.dae_sheng, bifa.dae_ping, bifa.dae_fu);
                    bifa.prize_rank = prize_rank;
                    bifa.dae_prize_rank = dae_prize_rank;
                }
                //获取赔率

                Kaijiang kaijiang = new KaijiangDAL().GetKaijiangByRiqiAndBianhao(bifa.riqi, bifa.bianhao);

                if (kaijiang != null)
                {
                    //MessageBox.Show("数据有错！");
                    bifa.first_sp = GetFirstSp(kaijiang, bifa.dae_sheng, bifa.dae_ping, bifa.dae_fu);
                    bifa.second_sp = GetSecondSp(kaijiang, bifa.dae_sheng, bifa.dae_ping, bifa.dae_fu);
                    bifa.third_sp = GetThirdSp(kaijiang, bifa.dae_sheng, bifa.dae_ping, bifa.dae_fu);
                }
                else
                {
                    bifa.first_sp = 0.0;
                    bifa.second_sp = 0.0;
                    bifa.third_sp = 0.0;
                }
                
                new SuperBifaDAL().InsertSuperBifa(bifa);

            }
        }

        private double GetFirstSp(Kaijiang kaijiang, double shengsp,double pingsp,double fusp)
        {
            double result = 0;
            if (shengsp > pingsp && shengsp > fusp)
            {
                result = kaijiang.ShengSp;
            }
            if (pingsp > shengsp && pingsp > fusp)
            {
                result = kaijiang.PingSp;
            }
            if (fusp > shengsp && fusp > pingsp)
            {
                result = kaijiang.FuSp;
            }
            return result;
        }

        private double GetSecondSp(Kaijiang kaijiang, double shengsp, double pingsp, double fusp)
        {
            double result = 0;
            if (shengsp >= pingsp && shengsp < fusp  || shengsp >= fusp && shengsp < pingsp)
            {

                result = kaijiang.ShengSp;
            }
            if (pingsp > shengsp && pingsp <= fusp || pingsp  >= fusp && pingsp <= shengsp  )
            {
                result = kaijiang.PingSp;
            }
            if (fusp > shengsp && fusp < pingsp  || fusp > pingsp && fusp < shengsp)
            {
                result = kaijiang.FuSp;
            }
            return result;
        }
        private double GetThirdSp(Kaijiang kaijiang, double shengsp, double pingsp, double fusp)
        {
            double result = 0;
            if (shengsp < pingsp && shengsp < fusp)
            {

                result = kaijiang.ShengSp;
            }
            if (pingsp <= shengsp && pingsp < fusp)
            {
                result = kaijiang.PingSp;
            }
            if (fusp <= shengsp && fusp <= pingsp)
            {
                result = kaijiang.FuSp;
            }
            return result;
        }
        private int GetRank(int spfresult, double sp, double shengsp,double pingsp,double fusp)
        {
            int result = 1;
            switch (spfresult)
            {
                case 3:
                    if (sp < pingsp)
                    {
                        result++;
                    }
                    if (sp < fusp)
                    {
                        result++;
                    }
                    break;
                case 1:
                    if (sp <= shengsp)
                    {
                        result++;
                    }
                    if (sp < fusp)
                    {
                        result++;
                    }
                    break;
                case 0:
                    if (sp <= shengsp)
                    {
                        result++;
                    }
                    if (sp <= pingsp)
                    {
                        result++;
                    }
                    break;
            }
            return result;
        }
        private int GetRank(double p1, double p2, double p3)
        {
            int result = 1;
            if (p1 < p2)
            {
                result++;
            }
            if (p1 < p3)
            {
                result++;
            }
            return result;
        }

        private string GetBianhaoFromData(string xingqiandbianhao)
        {
            return xingqiandbianhao.Substring(2, 3);
        }

        private string GetXingqiFromData(string xingqiandbianhao)
        {
            return xingqiandbianhao.Substring(0, 2);
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            //计算收益和最大回撤

            //1.获取需要计算的数据
            DataSet ds = new SuperBifaDAL().GetBifaAndKaijiang();

            int singletouzhu = 200;
            double totalShouyi = 0.0;
            foreach(DataRow row in ds.Tables[0].Rows){
                double dae_sheng = Convert.ToDouble(row["dae_sheng"]);
                double dae_ping = Convert.ToDouble(row["dae_ping"]);
                double dae_fu = Convert.ToDouble(row["dae_fu"]);
                double shengsp = Convert.ToDouble(row["shengsp"]);
                double pingsp = Convert.ToDouble(row["pingsp"]);
                double fusp = Convert.ToDouble(row["fusp"]);
                if (dae_sheng == 0 && dae_ping == 0 && dae_fu == 0)
                {
                    continue;
                }
                if (shengsp == 0 && pingsp == 0 && fusp == 0)
                {
                    continue;
                }
                totalShouyi = totalShouyi - singletouzhu;              

                double dae_prize_rank = Convert.ToDouble(row["dae_prize_rank"]);
                //先用最基本策略计算，1概率来赚钱，2概率来保本
                double p1 = GetZhuanqianSp(shengsp, pingsp, fusp, dae_sheng, dae_ping, dae_fu);
                double p2 = GetBaobenSp(shengsp, pingsp, fusp, dae_sheng, dae_ping, dae_fu);

                if(dae_prize_rank == 2){
                    totalShouyi = totalShouyi + 200;
                }else if(dae_prize_rank == 1)
                {
                    totalShouyi = totalShouyi + 200 * (1 - 1 / p2) * p1;
                }
                

            }

            MessageBox.Show("最后收益为" + totalShouyi);

        }

        public double GetZhuanqianSp(double shengsp, double pingsp, double fusp, double sheng, double ping, double fu)
        {
            double result = 0;
            string first = GetFirstGailv(sheng, ping, fu);
            switch (first)
            {
                case "sheng": result = shengsp; break;
                case "ping": result = pingsp; break;
                case "fu": result = fusp; break;

            }
            return result;
        }

        public double GetBaobenSp(double shengsp, double pingsp, double fusp, double sheng, double ping, double fu)
        {
            double result = 0;
            string first = GetSecondGailv(sheng, ping, fu);
            switch (first)
            {
                case "sheng": result = shengsp; break;
                case "ping": result = pingsp; break;
                case "fu": result = fusp; break;

            }
            return result;
        }

        public string GetFirstGailv(double sheng, double ping, double fu)
        {
            if (sheng > ping && sheng > fu)
            {
                return "sheng";
            }
            if (ping > sheng && ping > fu)
            {
                return "ping";
            }
            if (fu > sheng && fu > ping)
            {
                return "fu";
            }
            return "";
        }

        public string GetSecondGailv(double sheng, double ping, double fu)
        {
            if (sheng > ping && sheng > fu)
            {
                if (ping >= fu)
                {
                    return "ping";
                }
                else
                {
                    return "fu";
                }
                
            }
            if (ping > sheng && ping > fu)
            {
                if (sheng >= fu)
                {
                    return "sheng";
                }
                else
                {
                    return "fu";

                }
                
            }
            if (fu > sheng && fu > ping)
            {
                if (sheng >= ping)
                {
                    return "sheng";
                }
                else
                {
                    return "ping";

                }
            }
            return "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //计算收益和最大回撤

            //1.获取需要计算的数据
            DataSet ds = new SuperBifaDAL().GetBifaAndKaijiang();

            int singletouzhu = 200;
            double totalShouyi = 0.0;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                double dae_sheng = Convert.ToDouble(row["dae_sheng"]);
                double dae_ping = Convert.ToDouble(row["dae_ping"]);
                double dae_fu = Convert.ToDouble(row["dae_fu"]);
                double shengsp = Convert.ToDouble(row["shengsp"]);
                double pingsp = Convert.ToDouble(row["pingsp"]);
                double fusp = Convert.ToDouble(row["fusp"]);
                if (dae_sheng == 0 && dae_ping == 0 && dae_fu == 0)
                {
                    continue;
                }
                if (shengsp == 0 && pingsp == 0 && fusp == 0)
                {
                    continue;
                }
                totalShouyi = totalShouyi - singletouzhu;

                double dae_prize_rank = Convert.ToDouble(row["dae_prize_rank"]);
                //先用最基本策略计算，1概率来赚钱，2概率来保本
                double p2 = GetZhuanqianSp(shengsp, pingsp, fusp, dae_sheng, dae_ping, dae_fu);
                double p1 = GetBaobenSp(shengsp, pingsp, fusp, dae_sheng, dae_ping, dae_fu);

                if (dae_prize_rank == 1)
                {
                    totalShouyi = totalShouyi + 200;
                }
                else if (dae_prize_rank == 2)
                {
                    totalShouyi = totalShouyi + 200 * (1 - 1 / p2) * p1;
                }


            }

            MessageBox.Show("最后收益为" + totalShouyi);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //计算收益和最大回撤

            //1.获取需要计算的数据
            DataSet ds = new SuperBifaDAL().GetBifaAndKaijiang();

            int singletouzhu = 200;
            double totalShouyi = 0.0;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                double dae_sheng = Convert.ToDouble(row["dae_sheng"]);
                double dae_ping = Convert.ToDouble(row["dae_ping"]);
                double dae_fu = Convert.ToDouble(row["dae_fu"]);
                double shengsp = Convert.ToDouble(row["shengsp"]);
                double pingsp = Convert.ToDouble(row["pingsp"]);
                double fusp = Convert.ToDouble(row["fusp"]);
                if (dae_sheng == 0 && dae_ping == 0 && dae_fu == 0)
                {
                    continue;
                }
                if (shengsp == 0 && pingsp == 0 && fusp == 0)
                {
                    continue;
                }
                totalShouyi = totalShouyi - singletouzhu;

                double dae_prize_rank = Convert.ToDouble(row["dae_prize_rank"]);
                //先用最基本策略计算，1概率来赚钱，2概率来保本
                double p2 = GetZhuanqianSp(shengsp, pingsp, fusp, dae_sheng, dae_ping, dae_fu);
                double p1 = GetBaobenSp(shengsp, pingsp, fusp, dae_sheng, dae_ping, dae_fu);

                if (dae_prize_rank == 1)
                {
                    totalShouyi = totalShouyi + 200 * p1 * p2 / (p1 + p2);
                }
                else if (dae_prize_rank == 2)
                {
                    totalShouyi = totalShouyi + 200 * p1 * p2 / (p1 + p2);
                }


            }

            MessageBox.Show("最后收益为" + totalShouyi);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int startPage = Convert.ToInt32(txtPage.Text);
                int endPage = Convert.ToInt32(txtPageEnd.Text);

                for (int i = startPage; i <= endPage; i++)
                {
                    string prefix = "http://www.ydniu.com";
                    String page = i.ToString();
                    String tempurl = @"http://www.ydniu.com/info/jczq/cpyc/{0}/";
                    string totalUrl = string.Format(tempurl, page);

                    string totalHtmlData = GetHtmlFromUrl(totalUrl);
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                    document.LoadHtml(totalHtmlData);
                    HtmlNode rootNode = document.DocumentNode;

                    HtmlNode parentNode = rootNode.SelectSingleNode(@"//ul[@class='zx_list']");
                    HtmlNodeCollection dds = parentNode.SelectNodes("./li/div/a");

                    List<string> detailUrls = new List<string>();
                    foreach (HtmlNode dd in dds)
                    {
                        //HtmlNode a = dd.ChildNodes[0];
                        String detailUrl = dd.Attributes["href"].Value;
                        detailUrls.Add(detailUrl);
                    }

                    List<AppYuce> yuces = new List<AppYuce>();
                    String realUrl = "";
                    String title = "";
                    foreach (String detailUrl in detailUrls)
                    {
                        try
                        {

                        realUrl = prefix + detailUrl;
                        string detailHtml = GetHtmlFromUrl(realUrl);

                        //HtmlAgilityPack.HtmlDocument detailDocument = new HtmlAgilityPack.HtmlDocument();
                        document.LoadHtml(detailHtml);
                        HtmlNode detailRootNode = document.DocumentNode;

                        //找到title
                        title = detailRootNode.SelectSingleNode(@"//h1[@class='title']").InnerText.Trim();
                        //预处理title，把空格带进来
                        title = title.Replace("　"," ");
                        //如果不是以“周”开头的可以不用处理 //如果找不到VS，也是错误的url
                        if (title.Substring(0, 1) != "周" || title.Replace("vs","VS").IndexOf("VS") == -1 || title.IndexOf("推荐分析") == -1 || title.IndexOf(" ") == -1)
                        {
                            //需要加入手工处理表
                            AppYuceBadUrl badUrl = new AppYuceBadUrl();
                            badUrl.title = title;
                            badUrl.url = realUrl;
                            badUrl.prefix = prefix;
                            badUrl.creator = "system";
                            badUrl.createtime = DateTime.Now;
                            new AppYuceDAL().InsertBadUrl(badUrl);
                            continue;
                        }

                        //提取星期，编号，主队，客队数据
                        string xingqi = title.Substring(0, 2);
                        string bianhao = "0" + title.Substring(2, 2);
                        int spaceindex = title.IndexOf(" ");
                        string liansai = title.Substring(4, spaceindex - 4);
                        int vsIndex = title.Replace("vs","VS").IndexOf("VS");
                        int tuijianIndex = title.IndexOf("推荐分析");
                        string zhudui = title.Substring(spaceindex, vsIndex - spaceindex);
                        string kedui = title.Substring(vsIndex + 2, tuijianIndex - vsIndex - 2);

                        AppYuce yuce = new AppYuce();
                        yuce.bianhao = bianhao;
                        yuce.weekday = xingqi;
                        yuce.liansai = liansai;
                        yuce.zhudui = zhudui;
                        yuce.kedui = kedui;

                        yuce.title = title;
                        yuce.url = realUrl;
                        yuce.operateTime = DateTime.Now;
                        yuce.operPerson = "wulin";

                        string time = detailRootNode.SelectSingleNode("//div[@class='zx_article']/span[@class='time']").InnerText;
                        string dateString = time.Trim().Substring(0, 10);

                        yuce.riqi = GetRiqiFromWeekday(dateString, xingqi);

                        //获取预测结果
                        HtmlNode spfNode = detailRootNode.SelectSingleNode("//div[@class='zx_article']/p/a/span");
                        string yucespfstring = "";
                        if (spfNode != null)
                        {
                            yucespfstring = spfNode.InnerText;
                        }
                        else
                        {
                            HtmlNode spfnode2 = detailRootNode.SelectSingleNode("//div[@class='zx_article']/p/a");
                            if (spfnode2 != null && spfnode2.InnerText.Contains("推荐："))
                            {
                                yucespfstring = spfnode2.InnerText;
                            }
                            else
                            {
                                HtmlNode spfnode3 = detailRootNode.SelectSingleNode("//div[@class='zx_article']/p/strong/span");
                                if (spfnode3 != null)
                                {
                                    yucespfstring = spfnode3.InnerText;
                                }
                                else
                                {
                                    HtmlNode spfnode4 = detailRootNode.SelectSingleNode("//div[@class='zx_article']/p/span/a");
                                    if (spfnode4 != null)
                                    {
                                        yucespfstring = spfnode4.InnerText;
                                    }
                                    else
                                    {
                                        HtmlNode spfnode5 = detailRootNode.SelectSingleNode("//div[@class='zx_article']/p/a/strong/span");
                                        if (spfnode5 != null)
                                        {
                                            yucespfstring = spfnode5.InnerText;
                                        }
                                        else
                                        {

                                            HtmlNode spfnode6 = detailRootNode.SelectSingleNode("//div[@class='zx_article']/p/strong/strong/span");
                                            if (spfnode6 != null)
                                            {
                                                yucespfstring = spfnode6.InnerText;
                                            }
                                            else
                                            {
                                                HtmlNode spfnode7 = detailRootNode.SelectSingleNode("//div[@class='zx_article']/blockquote/p/a/span");
                                                if (spfnode7 != null)
                                                {
                                                    yucespfstring = spfnode7.InnerText;
                                                }
                                                else
                                                {
                                                    HtmlNode spfnode81 = detailRootNode.SelectSingleNode("//div[@class='zx_article']/p/span/strong/span");
                                                    
                                                    if (spfnode81 != null)
                                                    {
                                                        yucespfstring = spfnode81.InnerText;
                                                    }
                                                    else
                                                    {

                                                    
                                                    HtmlNode spfnode8 = detailRootNode.SelectSingleNode("//div[@class='zx_article']/p/span");
                                                    if (spfnode8 != null && (spfnode8.InnerText.Contains("推荐：")|| spfnode8.InnerText.Contains("推荐:")))
                                                    {
                                                        yucespfstring = spfnode8.InnerText;
                                                    }
                                                    else
                                                    {
                                                        //这里需要遍历所有p节点
                                                        HtmlNodeCollection hnc = detailRootNode.SelectNodes("//div[@class='zx_article']/p");
                                                        foreach (HtmlNode node in hnc)
                                                        {
                                                            if (node.InnerText.Contains("竞彩推荐"))
                                                            {
                                                                yucespfstring = node.InnerText.Trim();
                                                                break;
                                                            }
                                                        }
                                                    }
                                                        }
                                                   
                                                }

                                            }


                                        }
                                    }

                                }

                            }

                        }



                        yuce.spfrawresult = yucespfstring;

                        //预处理数据，将(变为中文括号
                        yucespfstring = yucespfstring.Replace("(", "（").Replace(")", "）").Replace("/", "");
                        yuce.spfresult = yucespfstring.Substring(yucespfstring.IndexOf("：") + 1);
                        yuce.rangqiushu = "0";
                        if (yucespfstring.IndexOf("（") != -1)
                        {
                            yuce.rangqiushu = yucespfstring.Substring(yucespfstring.IndexOf("（") + 1, yucespfstring.IndexOf("）") - yucespfstring.IndexOf("（") - 1);
                        }
                        yuces.Add(yuce);
                        }
                        catch(Exception ex)
                        {
                            AppYuceBadUrl badUrl = new AppYuceBadUrl();
                            badUrl.title = title;
                            badUrl.url = realUrl;
                            badUrl.prefix = prefix;
                            badUrl.creator = "system";
                            badUrl.createtime = DateTime.Now;
                            new AppYuceDAL().InsertBadUrl(badUrl);
                        }
                    }
                    new AppYuceDAL().InsertAppYuceList(yuces);
                    Console.WriteLine("一定牛第" + i.ToString() + "页处理完毕");
                }

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("操作成功！");
        }

        private string GetRiqiFromWeekday2(string dateString, string xingqi,string time)
        {
            
            int desWeekday = 0;
            switch (xingqi)
            {
                case "星期一": desWeekday = 1; break;
                case "星期二": desWeekday = 2; break;
                case "星期三": desWeekday = 3; break;
                case "星期四": desWeekday = 4; break;
                case "星期五": desWeekday = 5; break;
                case "星期六": desWeekday = 6; break;
                case "星期日": desWeekday = 0; break;
            }
            DateTime date = Convert.ToDateTime(dateString);

            for (int i = 0; i < 7; i++)
            {
                int weekday = (int)date.DayOfWeek;
                if (weekday == desWeekday)
                {
                    //return date.ToString("yyyy-MM-dd");
                    break;
                }
                date = date.AddDays(1);
            }

            if(string.Compare(time,"12:00") < 0)
            {
                date = date.AddDays(-1);
            }
            return date.ToString("yyyy-MM-dd");
        }


        private string GetRiqiFromWeekday(string dateString, string xingqi)
        {
            int desWeekday = 0;
            switch (xingqi)
            {
                case "周一": desWeekday = 1;  break;
                case "周二": desWeekday = 2; break;
                case "周三": desWeekday = 3; break;
                case "周四": desWeekday = 4; break;
                case "周五": desWeekday = 5; break;
                case "周六": desWeekday = 6; break;
                case "周日": desWeekday = 0; break;
            }
            DateTime date = Convert.ToDateTime(dateString);            

            for (int i = 0; i < 7; i++)
            {
                int weekday = (int)date.DayOfWeek;
                if (weekday == desWeekday)
                {
                    return date.ToString("yyyy-MM-dd");
                }
                date = date.AddDays(1);
            }
            return date.ToString("yyyy-MM-dd");
        }

        private void btnCalcYDN_Click(object sender, EventArgs e)
        {
            double minShouyi = double.MaxValue;
            double maxShouyi = double.MinValue;
            string mindate = "";
            string maxdate = "";
            DataSet yuce = new AppYuceDAL().GetAllYuce();
            DataSet kaijiang = new KaijiangDAL().GetAllKaijiang();

            double totalshouyi = 0.0;
            double defaultTouru = 200;
            foreach (DataRow yuceRow in yuce.Tables[0].Rows)
            {
                string yuceriqi = yuceRow["riqi"].ToString();
                string yucebianhao = yuceRow["bianhao"].ToString();
                string yucerangqiushu = yuceRow["rangqiushu"].ToString();
                string yucespfresult = yuceRow["spfresult"].ToString();

                DataRow matchedRow = null;
                foreach (DataRow kaijiangrow in kaijiang.Tables[0].Rows)
                {
                    if (kaijiangrow["riqi"].ToString() == yuceriqi && kaijiangrow["bianhao"].ToString() == yucebianhao)
                    {
                        matchedRow = kaijiangrow;
                        break;
                    }
                }
                if (matchedRow != null)
                {
                    totalshouyi = totalshouyi - defaultTouru;
                    double shengsp = 0.0;
                    double pingsp = 0.0;
                    double fusp = 0.0;
                    int spfresult = 0;
                    double spfresultsp = 0.0;
                    if (yucerangqiushu == "0")
                    {
                        shengsp = Convert.ToDouble(matchedRow["shengsp"]);
                        pingsp = Convert.ToDouble(matchedRow["pingsp"]);
                        fusp = Convert.ToDouble(matchedRow["fusp"]);
                        spfresult = Convert.ToInt32(matchedRow["spfresult"]);
                        spfresultsp = Convert.ToDouble(matchedRow["spfsp"]);

                    }else
                    {
                        shengsp = Convert.ToDouble(matchedRow["rqshengsp"]);
                        pingsp = Convert.ToDouble(matchedRow["rqpingsp"]);
                        fusp = Convert.ToDouble(matchedRow["rqfusp"]);
                        spfresult = Convert.ToInt32(matchedRow["rqspfresult"]);
                        spfresultsp = Convert.ToDouble(matchedRow["rqspfsp"]);
                    }
                    //首先要判断是否中奖
                    if(yucespfresult.Trim().IndexOf(spfresult.ToString()) != -1)
                    {
                        if (yucespfresult.Trim().Length == 1)
                        {
                            if (yucespfresult == spfresult.ToString())
                            {

                                totalshouyi = totalshouyi + defaultTouru * spfresultsp;
                            }
                        }
                        else
                        {
                            //第一个预测结果中奖的情况
                            string firstString = yucespfresult.Substring(0, 1);
                            double firstsp = GetMatchedSp(firstString, shengsp, pingsp, fusp);

                            string secondString = yucespfresult.Substring(1, 1);
                            double secondsp = GetMatchedSp(secondString, shengsp, pingsp, fusp);
                            if (secondsp == 0)
                            {
                                MessageBox.Show("数据不完整");
                            }
                            if (firstString == spfresult.ToString())
                            {
                                totalshouyi = totalshouyi + firstsp * (defaultTouru - defaultTouru / secondsp);

                            }else
                            {
                                totalshouyi = totalshouyi + 200;
                            }
                           

                        }
                    }
                    if (totalshouyi > maxShouyi)
                    {
                        maxShouyi = totalshouyi;
                        maxdate = yuceriqi;
                    }
                    if (totalshouyi < minShouyi)
                    {
                        minShouyi = totalshouyi;
                        mindate = yuceriqi;
                    }
                }


            }

            MessageBox.Show("总收益为" + totalshouyi + ",过程最大收益为" + maxShouyi  + "日期为" + maxdate + "，过程最小收益为" + minShouyi + "日期为" + mindate);

        }

        private double GetMatchedSp(string firstString, double shengsp, double pingsp, double fusp)
        {
            double result = 0.0;
            switch (firstString)
            {
                case "3": result = shengsp; break;
                case "1": result = pingsp; break;
                case "0": result = fusp; break;

            }
            return result;
        }


        private void btnCalcYDN2_Click(object sender, EventArgs e)
        {
            double minShouyi = double.MaxValue;
            double maxShouyi = double.MinValue;
            string mindate = "";
            string maxdate = "";
            DataSet yuce = new AppYuceDAL().GetAllYuce();
            DataSet kaijiang = new KaijiangDAL().GetAllKaijiang();

            double totalshouyi = 0.0;
            double defaultTouru = 200;
            foreach (DataRow yuceRow in yuce.Tables[0].Rows)
            {
                string yuceriqi = yuceRow["riqi"].ToString();
                string yucebianhao = yuceRow["bianhao"].ToString();
                string yucerangqiushu = yuceRow["rangqiushu"].ToString();
                string yucespfresult = yuceRow["spfresult"].ToString();

                DataRow matchedRow = null;
                foreach (DataRow kaijiangrow in kaijiang.Tables[0].Rows)
                {
                    if (kaijiangrow["riqi"].ToString() == yuceriqi && kaijiangrow["bianhao"].ToString() == yucebianhao)
                    {
                        matchedRow = kaijiangrow;
                        break;
                    }
                }
                if (matchedRow != null)
                {
                    totalshouyi = totalshouyi - defaultTouru;
                    double shengsp = 0.0;
                    double pingsp = 0.0;
                    double fusp = 0.0;
                    int spfresult = 0;
                    double spfresultsp = 0.0;
                    if (yucerangqiushu == "0")
                    {
                        shengsp = Convert.ToDouble(matchedRow["shengsp"]);
                        pingsp = Convert.ToDouble(matchedRow["pingsp"]);
                        fusp = Convert.ToDouble(matchedRow["fusp"]);
                        spfresult = Convert.ToInt32(matchedRow["spfresult"]);
                        spfresultsp = Convert.ToDouble(matchedRow["spfsp"]);

                    }
                    else
                    {
                        shengsp = Convert.ToDouble(matchedRow["rqshengsp"]);
                        pingsp = Convert.ToDouble(matchedRow["rqpingsp"]);
                        fusp = Convert.ToDouble(matchedRow["rqfusp"]);
                        spfresult = Convert.ToInt32(matchedRow["rqspfresult"]);
                        spfresultsp = Convert.ToDouble(matchedRow["rqspfsp"]);
                    }
                    //首先要判断是否中奖
                    if (yucespfresult.Trim().IndexOf(spfresult.ToString()) != -1)
                    {
                        if (yucespfresult.Trim().Length == 1)
                        {
                            if (yucespfresult == spfresult.ToString())
                            {

                                totalshouyi = totalshouyi + defaultTouru * spfresultsp;
                            }
                        }
                        else
                        {
                            string firstString = yucespfresult.Substring(0, 1);
                            double firstsp = GetMatchedSp(firstString, shengsp, pingsp, fusp);

                            string secondString = yucespfresult.Substring(1, 1);
                            double secondsp = GetMatchedSp(secondString, shengsp, pingsp, fusp);
                            if (secondsp == 0)
                            {
                                MessageBox.Show("数据不完整");
                            }
                            totalshouyi = totalshouyi + defaultTouru * (firstsp * secondsp) / (firstsp + secondsp);

                        }
                    }
                    if (totalshouyi > maxShouyi)
                    {
                        maxShouyi = totalshouyi;
                        maxdate = yuceriqi;
                    }
                    if (totalshouyi < minShouyi)
                    {
                        minShouyi = totalshouyi;
                        mindate = yuceriqi;
                    }
                }


            }

            MessageBox.Show("总收益为" + totalshouyi + ",过程最大收益为" + maxShouyi + "日期为" + maxdate + "，过程最小收益为" + minShouyi + "日期为" + mindate);

        }

        private void btnCalcYDN3_Click(object sender, EventArgs e)
        {
            double minShouyi = double.MaxValue;
            double maxShouyi = double.MinValue;
            string mindate = "";
            string maxdate = "";
            DataSet yuce = new AppYuceDAL().GetAllYuce();
            DataSet kaijiang = new KaijiangDAL().GetAllKaijiang();

            double totalshouyi = 0.0;
            double defaultTouru = 200;
            foreach (DataRow yuceRow in yuce.Tables[0].Rows)
            {
                string yuceriqi = yuceRow["riqi"].ToString();
                string yucebianhao = yuceRow["bianhao"].ToString();
                string yucerangqiushu = yuceRow["rangqiushu"].ToString();
                string yucespfresult = yuceRow["spfresult"].ToString();

                DataRow matchedRow = null;
                foreach (DataRow kaijiangrow in kaijiang.Tables[0].Rows)
                {
                    if (kaijiangrow["riqi"].ToString() == yuceriqi && kaijiangrow["bianhao"].ToString() == yucebianhao)
                    {
                        matchedRow = kaijiangrow;
                        break;
                    }
                }
                if (matchedRow != null)
                {
                    totalshouyi = totalshouyi - defaultTouru;
                    double shengsp = 0.0;
                    double pingsp = 0.0;
                    double fusp = 0.0;
                    int spfresult = 0;
                    double spfresultsp = 0.0;
                    if (yucerangqiushu == "0")
                    {
                        shengsp = Convert.ToDouble(matchedRow["shengsp"]);
                        pingsp = Convert.ToDouble(matchedRow["pingsp"]);
                        fusp = Convert.ToDouble(matchedRow["fusp"]);
                        spfresult = Convert.ToInt32(matchedRow["spfresult"]);
                        spfresultsp = Convert.ToDouble(matchedRow["spfsp"]);

                    }
                    else
                    {
                        shengsp = Convert.ToDouble(matchedRow["rqshengsp"]);
                        pingsp = Convert.ToDouble(matchedRow["rqpingsp"]);
                        fusp = Convert.ToDouble(matchedRow["rqfusp"]);
                        spfresult = Convert.ToInt32(matchedRow["rqspfresult"]);
                        spfresultsp = Convert.ToDouble(matchedRow["rqspfsp"]);
                    }
                    //首先要判断是否中奖
                    if (yucespfresult.Trim().IndexOf(spfresult.ToString()) != -1)
                    {
                        if (yucespfresult.Trim().Length == 1)
                        {
                            if (yucespfresult == spfresult.ToString())
                            {

                                totalshouyi = totalshouyi + defaultTouru * spfresultsp;
                            }
                        }
                        else
                        {
                            //取中奖的sp来计算，假设每次都赚钱的理想情况
                            string firstString = yucespfresult.Substring(0, 1);
                            string secondString = yucespfresult.Substring(1, 1);
                            double firstsp = 0.0;
                            double secondsp = 0.0;
                            if (firstString == spfresult.ToString())
                            {
                                 firstsp = GetMatchedSp(firstString, shengsp, pingsp, fusp);                           
                                 secondsp = GetMatchedSp(secondString, shengsp, pingsp, fusp);
                            }
                            else
                            {
                                firstsp = GetMatchedSp(secondString, shengsp, pingsp, fusp);
                                secondsp = GetMatchedSp(firstString, shengsp, pingsp, fusp);
                            }
                            //double firstsp = GetMatchedSp(firstString, shengsp, pingsp, fusp);                           
                            //double secondsp = GetMatchedSp(secondString, shengsp, pingsp, fusp);
                            if (secondsp == 0)
                            {
                                MessageBox.Show("数据不完整");
                            }
                            totalshouyi = totalshouyi + firstsp * (defaultTouru - defaultTouru / secondsp);
                        }
                    }
                    if (totalshouyi > maxShouyi)
                    {
                        maxShouyi = totalshouyi;
                        maxdate = yuceriqi;
                    }
                    if (totalshouyi < minShouyi)
                    {
                        minShouyi = totalshouyi;
                        mindate = yuceriqi;
                    }
                }


            }

            MessageBox.Show("总收益为" + totalshouyi + ",过程最大收益为" + maxShouyi + "日期为" + maxdate + "，过程最小收益为" + minShouyi + "日期为" + mindate);

        }

        private void btnCalcYDN4_Click(object sender, EventArgs e)
        {
            double minShouyi = double.MaxValue;
            double maxShouyi = double.MinValue;
            string mindate = "";
            string maxdate = "";
            DataSet yuce = new AppYuceDAL().GetAllYuce();
            DataSet kaijiang = new KaijiangDAL().GetAllKaijiang();

            double totalshouyi = 0.0;
            double defaultTouru = 200;
            foreach (DataRow yuceRow in yuce.Tables[0].Rows)
            {
                string yuceriqi = yuceRow["riqi"].ToString();
                string yucebianhao = yuceRow["bianhao"].ToString();
                string yucerangqiushu = yuceRow["rangqiushu"].ToString();
                string yucespfresult = yuceRow["spfresult"].ToString();

                DataRow matchedRow = null;
                foreach (DataRow kaijiangrow in kaijiang.Tables[0].Rows)
                {
                    if (kaijiangrow["riqi"].ToString() == yuceriqi && kaijiangrow["bianhao"].ToString() == yucebianhao)
                    {
                        matchedRow = kaijiangrow;
                        break;
                    }
                }
                if (matchedRow != null)
                {
                    totalshouyi = totalshouyi - defaultTouru;
                    double shengsp = 0.0;
                    double pingsp = 0.0;
                    double fusp = 0.0;
                    int spfresult = 0;
                    double spfresultsp = 0.0;
                    if (yucerangqiushu == "0")
                    {
                        shengsp = Convert.ToDouble(matchedRow["shengsp"]);
                        pingsp = Convert.ToDouble(matchedRow["pingsp"]);
                        fusp = Convert.ToDouble(matchedRow["fusp"]);
                        spfresult = Convert.ToInt32(matchedRow["spfresult"]);
                        spfresultsp = Convert.ToDouble(matchedRow["spfsp"]);

                    }
                    else
                    {
                        shengsp = Convert.ToDouble(matchedRow["rqshengsp"]);
                        pingsp = Convert.ToDouble(matchedRow["rqpingsp"]);
                        fusp = Convert.ToDouble(matchedRow["rqfusp"]);
                        spfresult = Convert.ToInt32(matchedRow["rqspfresult"]);
                        spfresultsp = Convert.ToDouble(matchedRow["rqspfsp"]);
                    }
                    //首先要判断是否中奖
                    if (yucespfresult.Trim().IndexOf(spfresult.ToString()) != -1)
                    {
                        if (yucespfresult.Trim().Length == 1)
                        {
                            if (yucespfresult == spfresult.ToString())
                            {

                                totalshouyi = totalshouyi + defaultTouru * spfresultsp;
                            }
                        }
                        else
                        {
                            //取大sp来赚钱，需要判断当前结果是否为大sp
                            string firstString = yucespfresult.Substring(0, 1);
                            string secondString = yucespfresult.Substring(1, 1);
                            //double firstsp = 0.0;
                            //double secondsp = 0.0;
                            //if (firstString == spfresult.ToString())
                            //{
                            //    firstsp = GetMatchedSp(firstString, shengsp, pingsp, fusp);
                            //    secondsp = GetMatchedSp(secondString, shengsp, pingsp, fusp);
                            //}
                            //else
                            //{
                            //    firstsp = GetMatchedSp(secondString, shengsp, pingsp, fusp);
                            //    secondsp = GetMatchedSp(firstString, shengsp, pingsp, fusp);
                            //}
                            double firstsp = GetMatchedSp(firstString, shengsp, pingsp, fusp);                           
                            double secondsp = GetMatchedSp(secondString, shengsp, pingsp, fusp);

                            if (secondsp == 0)
                            {
                                MessageBox.Show("数据不完整");
                            }
                            if (firstsp >= secondsp)
                            {
                                if (spfresult.ToString() == firstString)
                                {
                                    totalshouyi = totalshouyi + firstsp * (defaultTouru - defaultTouru / secondsp);
                                }
                                else
                                {
                                    totalshouyi = totalshouyi + defaultTouru;
                                }
                            }
                            else
                            {
                                if (spfresult.ToString() == secondString)
                                {
                                    totalshouyi = totalshouyi + secondsp * (defaultTouru - defaultTouru / firstsp);
                                }
                                else
                                {
                                    totalshouyi = totalshouyi + defaultTouru;
                                }
                            }
                            
                        }
                    }
                    if (totalshouyi > maxShouyi)
                    {
                        maxShouyi = totalshouyi;
                        maxdate = yuceriqi;
                    }
                    if (totalshouyi < minShouyi)
                    {
                        minShouyi = totalshouyi;
                        mindate = yuceriqi;
                    }
                }


            }

            MessageBox.Show("总收益为" + totalshouyi + ",过程最大收益为" + maxShouyi + "日期为" + maxdate + "，过程最小收益为" + minShouyi + "日期为" + mindate);

        }

        private void btnCalcYdnAndBifa_Click(object sender, EventArgs e)
        {
            DataSet yuce = new AppYuceDAL().GetAllYuce();
            DataSet kaijiang = new KaijiangDAL().GetAllKaijiang();
            DataSet bifa = new SuperBifaDAL().GetAllBifa();

            double minShouyi = double.MaxValue;
            double maxShouyi = double.MinValue;
            string mindate = "";
            string maxdate = "";
           

            double totalshouyi = 0.0;
            double defaultTouru = 200;
            foreach (DataRow yuceRow in yuce.Tables[0].Rows)
            {
                string yuceriqi = yuceRow["riqi"].ToString();
                string yucebianhao = yuceRow["bianhao"].ToString();
                string yucerangqiushu = yuceRow["rangqiushu"].ToString();
                string yucespfresult = yuceRow["spfresult"].ToString();

                //如果让球数不为0，则不考虑进来
                if(yucerangqiushu != "0")
                {
                    continue;
                }


                DataRow matchedRow = null;
                foreach (DataRow kaijiangrow in kaijiang.Tables[0].Rows)
                {
                    if (kaijiangrow["riqi"].ToString() == yuceriqi && kaijiangrow["bianhao"].ToString() == yucebianhao)
                    {
                        matchedRow = kaijiangrow;
                        break;
                    }
                }

                DataRow matchRowBifa = null;
                foreach (DataRow bifarow in bifa.Tables[0].Rows)
                {
                    if (bifarow["riqi"].ToString() == yuceriqi && bifarow["bianhao"].ToString() == yucebianhao)
                    {
                        matchRowBifa = bifarow;
                        break;
                    }
                }
                //如果找不到记录
                if (matchedRow == null || matchRowBifa == null)
                {
                    continue;
                }
                double dae_sheng = Convert.ToDouble(matchRowBifa["dae_sheng"]);
                double dae_ping = Convert.ToDouble(matchRowBifa["dae_ping"]);
                double dae_fu = Convert.ToDouble(matchRowBifa["dae_fu"]);


                bool flag = IsSameResult(yucespfresult, dae_sheng,dae_ping,dae_fu);
                if (flag)
                {
                    totalshouyi = totalshouyi - defaultTouru;
                    double shengsp = 0.0;
                    double pingsp = 0.0;
                    double fusp = 0.0;
                    int spfresult = 0;
                    double spfresultsp = 0.0;
                    if (yucerangqiushu == "0")
                    {
                        shengsp = Convert.ToDouble(matchedRow["shengsp"]);
                        pingsp = Convert.ToDouble(matchedRow["pingsp"]);
                        fusp = Convert.ToDouble(matchedRow["fusp"]);
                        spfresult = Convert.ToInt32(matchedRow["spfresult"]);
                        spfresultsp = Convert.ToDouble(matchedRow["spfsp"]);

                    }
                    else
                    {
                        shengsp = Convert.ToDouble(matchedRow["rqshengsp"]);
                        pingsp = Convert.ToDouble(matchedRow["rqpingsp"]);
                        fusp = Convert.ToDouble(matchedRow["rqfusp"]);
                        spfresult = Convert.ToInt32(matchedRow["rqspfresult"]);
                        spfresultsp = Convert.ToDouble(matchedRow["rqspfsp"]);
                    }
                    //首先要判断是否中奖
                    if (yucespfresult.Trim().IndexOf(spfresult.ToString()) != -1)
                    {
                        if (yucespfresult.Trim().Length == 1)
                        {
                            if (yucespfresult == spfresult.ToString())
                            {

                                totalshouyi = totalshouyi + defaultTouru * spfresultsp;
                            }
                        }
                        else
                        {
                            //第一个预测结果中奖的情况
                            string firstString = yucespfresult.Substring(0, 1);
                            double firstsp = GetMatchedSp(firstString, shengsp, pingsp, fusp);

                            string secondString = yucespfresult.Substring(1, 1);
                            double secondsp = GetMatchedSp(secondString, shengsp, pingsp, fusp);
                            if (secondsp == 0)
                            {
                                MessageBox.Show("数据不完整");
                            }
                            if (firstString == spfresult.ToString())
                            {
                                totalshouyi = totalshouyi + firstsp * (defaultTouru - defaultTouru / secondsp);

                            }
                            else
                            {
                                totalshouyi = totalshouyi + 200;
                            }


                        }
                    }
                    if (totalshouyi > maxShouyi)
                    {
                        maxShouyi = totalshouyi;
                        maxdate = yuceriqi;
                    }
                    if (totalshouyi < minShouyi)
                    {
                        minShouyi = totalshouyi;
                        mindate = yuceriqi;
                    }
                }                

            }

            MessageBox.Show("总收益为" + totalshouyi + ",过程最大收益为" + maxShouyi + "日期为" + maxdate + "，过程最小收益为" + minShouyi + "日期为" + mindate);

        }

        private bool IsSameResult(string yucespfresult, double dae_sheng, double dae_ping, double dae_fu)
        {
            bool result = false;
            if (yucespfresult.Length == 1)
            {
                
                switch (yucespfresult)
                {
                    case "3": 
                        if(dae_sheng > dae_ping && dae_sheng > dae_fu)
                        {
                            result = true;
                        }
                        break;
                    case "1":
                        if (dae_ping > dae_sheng && dae_ping > dae_fu)
                        {
                            result = true;
                        }
                        break;
                    case "0":
                        if (dae_fu > dae_sheng && dae_fu > dae_ping)
                        {
                            result = true;
                        }
                        break;
                }
            }
            else
            {
                if (yucespfresult.IndexOf("3") != -1 && yucespfresult.IndexOf("1") != -1)
                {
                    if (dae_fu <= dae_sheng && dae_fu <= dae_fu)
                    {
                        result = true;
                    }
                }
                if (yucespfresult.IndexOf("3") != -1 && yucespfresult.IndexOf("0") != -1)
                {
                    if (dae_ping <= dae_sheng && dae_ping < dae_fu)
                    {
                        result = true;
                    }
                }
                if (yucespfresult.IndexOf("1") != -1 && yucespfresult.IndexOf("0") != -1)
                {
                    if (dae_sheng < dae_ping && dae_sheng < dae_fu)
                    {
                        result = true;
                    }
                }
            }




            return result;
        }

        private void btnUpdateYuceResult_Click(object sender, EventArgs e)
        {

            DateTime StartDate = dtpStartDate.Value;
            DateTime EndDate = dtpEndDate.Value;
            int count = 0;
            while (StartDate <= EndDate)
            {

                new AppYuceDAL().UpdateZhongjiangResult(StartDate);
                StartDate = StartDate.AddDays(1);
            }
            MessageBox.Show("操作成功！");
        }

        private void 回测策略ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new BackTestingForm().ShowDialog();

        }

        private void 数据分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new DataAnalysisForm().ShowDialog();
        }

        private void 比赛推荐ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new MatchSuggestForm().ShowDialog();
        }

        private void btnImportLanqiu_Click(object sender, EventArgs e)
        {
            DateTime StartDate = dtpStartDate.Value;
            DateTime EndDate = dtpEndDate.Value;
            int count = 0;
            while (StartDate <= EndDate)
            {
                //先删除当前日期的开奖结果，避免重复
                string riqi = StartDate.Date.ToString("yyyy-MM-dd");
                //new KaijiangDAL().DeleteKaijiang(riqi);
                //new SuperBifaDAL().DeleteBifa(riqi);
                YuceLanqiuDAL.DeleteKaijiangLanqiu(riqi);
                ImportLanqiu(StartDate);
                StartDate = StartDate.AddDays(1);
                count++;
            }
            MessageBox.Show("导入成功,导入了" + count + "条篮球开奖结果");
        }


        private void ImportLanqiu(DateTime importDate)
        {
            string bifaUrl = "http://live.aicai.com/lc/jsbf/bkscore!dynamicLcDataFromPage.htm?&issueNo=" + importDate.ToString("yyyyMMdd");
            //string bifaUrl = "http://live.aicai.com/jsbf/timelyscore!dynamicBfDataFromPage.htm?lotteryType=zc&issue=20150628";
            string result = GetHtmlFromUrl(bifaUrl);

            LancaiJsonEntity bifaJsonEntity = JsonConvert.DeserializeObject<LancaiJsonEntity>(result);
            //MessageBox.Show(bifaJsonEntity.result.lc_page);


            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(bifaJsonEntity.result.lc_page);

            HtmlNode rootNode = document.DocumentNode;
            List<Kaijianglanqiu> yucelanqius = new List<Kaijianglanqiu>();

            

            HtmlNodeCollection lanqiuNodes = rootNode.SelectNodes("./div[@class='section css_league']");

            if (lanqiuNodes == null)
            {
                return;
            }
            foreach (HtmlNode lanqiuNode in lanqiuNodes)
            {

                Kaijianglanqiu lanqiu = new Kaijianglanqiu();
                lanqiu.riqi = importDate.ToString("yyyy-MM-dd");

                HtmlNode div = lanqiuNode.SelectSingleNode("./div/div[@class='sectl_tit']");
                string liansai = div.SelectNodes("./span")[0].InnerText;

                string bisaishijian = div.SelectNodes("./span")[1].InnerText;

                HtmlNodeCollection lis = lanqiuNode.SelectNodes("./div/div[@class='sectl_con']/div/div/ul/li");
                string kefen = lis[0].InnerText;
                string zhufen = lis[2].InnerText;

                string fencha = lanqiuNode.SelectNodes("./div/div[@class='sectl_con']/div/div[@class='fc_zf']/span")[0].InnerText;
                string zongfen = lanqiuNode.SelectNodes("./div/div[@class='sectl_con']/div/div[@class='fc_zf']/span")[1].InnerText;

                string kedui = lanqiuNode.SelectNodes("./div/div[@class='sectl_con']/div[@class='team_box l_teamb']/p")[0].InnerText;
                string zhudui = lanqiuNode.SelectNodes("./div/div[@class='sectl_con']/div[@class='team_box r_teamb']/p")[0].InnerText;

                HtmlNode peilvul = lanqiuNode.SelectSingleNode("./div[@class='sect_r f_r']/div[@class='sectr_con']/div/ul");
                //string rangfen = lanqiuNode.SelectNodes("./div[@class='sect_r f_r']/div[@class='sectr_con']/div/ul/li/p")[1].InnerText;
                //string zhufusp = lanqiuNode.SelectNodes("./div[@class='sect_r f_r']/div[@class='sectr_con']/div/ul/li/p/span")[0].InnerText;
                //string zhushengsp = lanqiuNode.SelectNodes("./div[@class='sect_r f_r']/div[@class='sectr_con']/div/ul/li/p/span")[1].InnerText;
                //string rqresult = lanqiuNode.SelectNodes("./div[@class='sect_r f_r']/div[@class='sectr_con']/div/ul/li/p/span[@class='c_red']")[0].InnerText;

                string rangfen = peilvul.SelectNodes("./li")[0].SelectNodes("./p")[1].InnerText;
                string zhufusp = peilvul.SelectNodes("./li")[0].SelectNodes("./p/span")[0].InnerText;
                string zhushengsp = peilvul.SelectNodes("./li")[0].SelectNodes("./p/span")[1].InnerText;
                string rqresult = peilvul.SelectNodes("./li")[0].SelectNodes("./p/span[@class='c_red']")[0].InnerText;
                
                string yushezongfen = "";
                string dafensp = "";
                string xiaofensp = "";
                string daxiaofenresult = "";

                yushezongfen = peilvul.SelectNodes("./li")[1].SelectNodes("./p")[1].InnerText;
                dafensp = peilvul.SelectNodes("./li")[1].SelectNodes("./p/span")[0].InnerText;
                xiaofensp  = peilvul.SelectNodes("./li")[1].SelectNodes("./p/span")[1].InnerText;
                daxiaofenresult = peilvul.SelectNodes("./li")[1].SelectNodes("./p/span[@class='c_red']")[0].InnerText;

                lanqiu.liansai = liansai;
                lanqiu.bianhao = "";
                lanqiu.bisaishijian = bisaishijian;
                lanqiu.zhudui = zhudui;
                lanqiu.kedui = kedui;
                int tempint;
                if (int.TryParse(zhufen, out tempint))
                {
                    lanqiu.zhufen = Convert.ToInt32(zhufen);
                }
                if (int.TryParse(kefen, out tempint))
                {
                    lanqiu.kefen = Convert.ToInt32(kefen);
                }
               
                lanqiu.fencha = fencha;
                lanqiu.zongfen = zongfen;
                lanqiu.rangfen = rangfen;
                lanqiu.zhufusp = zhufusp;
                lanqiu.zhushengsp = zhushengsp;
                lanqiu.yushezongfen = yushezongfen;
                lanqiu.dafensp = dafensp;
                lanqiu.xiaofensp = xiaofensp;
                lanqiu.rqresult = rqresult;
                lanqiu.daxiaofenresult = daxiaofenresult;
                lanqiu.operatetime = DateTime.Now;

                yucelanqius.Add(lanqiu);
            }
            //插入数据库
            YuceLanqiuDAL.InsertKaijiangLanqiuList(yucelanqius);

        }

        private void btnImportYDNLanqiu_Click(object sender, EventArgs e)
        {
            try
            {
                string prefix = "http://www.ydniu.com";
                String page = txtPage.Text;
                String tempurl = @"http://www.ydniu.com/info/jclq/cpyc/{0}/";
                string totalUrl = string.Format(tempurl, page);

                string totalHtmlData = GetHtmlFromUrl(totalUrl);
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(totalHtmlData);
                HtmlNode rootNode = document.DocumentNode;

                HtmlNode parentNode = rootNode.SelectSingleNode(@"//ul[@class='zx_list']");
                HtmlNodeCollection dds = parentNode.SelectNodes("./li/div/a");

                List<string> detailUrls = new List<string>();
                foreach (HtmlNode dd in dds)
                {
                    //HtmlNode a = dd.ChildNodes[0];
                    String detailUrl = dd.Attributes["href"].Value;
                    detailUrls.Add(detailUrl);
                }

                List<Yucelanqiu> yuces = new List<Yucelanqiu>();
                foreach (String detailUrl in detailUrls)
                {
                    String realUrl = prefix + detailUrl;
                    string detailHtml = GetHtmlFromUrl(realUrl);

                    //HtmlAgilityPack.HtmlDocument detailDocument = new HtmlAgilityPack.HtmlDocument();
                    document.LoadHtml(detailHtml);
                    HtmlNode detailRootNode = document.DocumentNode;

                    //找到title
                    String title = detailRootNode.SelectSingleNode(@"//h1[@class='title']").InnerText.Trim();

                    //如果不是以“周”开头的可以不用处理 //如果找不到VS，也是错误的url
                    if (title.Substring(0, 1) != "周" || title.IndexOf("VS") == -1 || title.IndexOf("推荐分析") == -1 || title.IndexOf(" ") == -1)
                    {
                        //需要加入手工处理表
                        AppYuceBadUrl badUrl = new AppYuceBadUrl();
                        badUrl.title = title;
                        badUrl.url = realUrl;
                        badUrl.prefix = prefix;
                        badUrl.creator = "system";
                        badUrl.createtime = DateTime.Now;
                        new AppYuceDAL().InsertBadUrl(badUrl);
                        continue;
                    }

                    //提取星期，编号，主队，客队数据
                    string xingqi = title.Substring(0, 2);
                    string bianhao =  title.Substring(2, 3);
                    int spaceindex = title.IndexOf(" ");
                    string liansai = title.Substring(5, spaceindex - 4);
                    int vsIndex = title.IndexOf("VS");
                    int tuijianIndex = title.IndexOf("推荐分析");
                    string zhudui = title.Substring(spaceindex, vsIndex - spaceindex);
                    string kedui = title.Substring(vsIndex + 2, tuijianIndex - vsIndex - 2);

                    Yucelanqiu yuce = new Yucelanqiu();
                    yuce.bianhao = bianhao;
                    yuce.dayofweek = xingqi;
                    yuce.liansai = liansai;
                    yuce.zhudui = zhudui;
                    yuce.kedui = kedui;

                    yuce.title = title;
                    yuce.url = realUrl;
                    yuce.operatetime = DateTime.Now;
                    yuce.operperson = "wulin";

                    string time = detailRootNode.SelectSingleNode("//div[@class='zx_article']/span[@class='time']").InnerText;
                    string dateString = time.Trim().Substring(0, 10);

                    yuce.riqi = GetRiqiFromWeekday(dateString, xingqi);

                    //获取预测结果
                    HtmlNode spfNode = detailRootNode.SelectSingleNode("//div[@class='zx_article']/p/span");
                    string yucespfstring = "";
                    if (spfNode != null)
                    {
                        yucespfstring = spfNode.InnerText;
                    }
                    yuce.spfrawresult = yucespfstring;

                    //获取大小分推荐结果
                    int daxiaoIndex = yucespfstring.IndexOf("大小分推荐");
                    if (daxiaoIndex != -1)
                    {
                        string daxiaofenresult = yucespfstring.Substring(daxiaoIndex + 1);
                        yuce.daxiaofenresult = daxiaofenresult;
                    }
                   
                    
                    //获取让球的结果
                    int rqIndex = yucespfstring.IndexOf("让分推荐");
                    if (rqIndex == -1)
                    {
                        rqIndex = yucespfstring.IndexOf("竞彩推荐");
                    }
                    if (rqIndex != -1)
                    {
                        int symbolIndex1 = yucespfstring.IndexOf("+");
                        int symbolIndex2 = yucespfstring.IndexOf("-");
                        int symbolIndex = 0;
                        if (symbolIndex1 != -1)
                        {
                            symbolIndex = symbolIndex1;
                        }
                        if (symbolIndex2 != -1)
                        {
                            symbolIndex = symbolIndex2;
                        }

                        if (symbolIndex != 0)
                        {
                            string rqresult = yucespfstring.Substring(rqIndex + 5, symbolIndex - rqIndex - 5);
                            yuce.rqresult = rqresult;
                        }
                        else
                        {
                            string rqresult = yucespfstring.Substring(rqIndex + 5);
                            yuce.rqresult = rqresult;
                        }


                        
                    }

                   
                   

                    ////预处理数据，将(变为中文括号
                    //yucespfstring = yucespfstring.Replace("(", "（").Replace(")", "）").Replace("/", "");
                    //yuce.spfresult = yucespfstring.Substring(yucespfstring.IndexOf("：") + 1);
                    //yuce.rangqiushu = "0";
                    //if (yucespfstring.IndexOf("（") != -1)
                    //{
                    //    yuce.rangqiushu = yucespfstring.Substring(yucespfstring.IndexOf("（") + 1, yucespfstring.IndexOf("）") - yucespfstring.IndexOf("（") - 1);
                    //}
                    yuces.Add(yuce);
                }
                YuceLanqiuDAL.InsertYuceLanqiuList(yuces);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("操作成功！");
        }

        private void btnImport310_Click(object sender, EventArgs e)
        {
            try
            {
                int startPage = Convert.ToInt32(txtPage.Text);
                int endPage = Convert.ToInt32(txtPageEnd.Text);

                for (int i = startPage; i <= endPage; i++)
                {
                    string prefix = "http://www.310win.com";
                    String page = txtPage.Text;
                    String tempurl = @"http://www.310win.com/tag/Samuel/P{0}/";
                    string totalUrl = string.Format(tempurl, i);

                    string totalHtmlData = GetHtmlFromUrl(totalUrl);
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                    document.LoadHtml(totalHtmlData);
                    HtmlNode rootNode = document.DocumentNode;

                    HtmlNode parentNode = rootNode.SelectSingleNode(@"//table[@class='htbList']");
                    HtmlNodeCollection trs = parentNode.SelectNodes("./tr[@class='']");

                    List<string> detailUrls = new List<string>();
                    foreach (HtmlNode tr in trs)
                    {
                        string href = tr.SelectNodes("./td/a")[0].Attributes["href"].Value;
                        if (href == "/jingcaizuqiu/")
                        {
                            String detailUrl = tr.SelectNodes("./td/a")[1].Attributes["href"].Value;
                            detailUrls.Add(detailUrl);

                        }

                    }

                    //开始获取详细数据
                    List<Yuce310> yuce310s = new List<Yuce310>();
                    foreach (String detailUrl in detailUrls)
                    {
                        String realUrl = prefix + detailUrl;
                        string detailHtml = GetHtmlFromUrl(realUrl);
                        document.LoadHtml(detailHtml);
                        HtmlNode detailRootNode = document.DocumentNode;

                        //找到title
                        String title = detailRootNode.SelectSingleNode(@"//div[@class='articleTitle']").InnerText.Trim();

                        string aInfo = detailRootNode.SelectSingleNode(@"//div[@class='aInfo']").InnerText.Trim();
                        int riqiIndex = aInfo.IndexOf("发表于：");
                        string yuceriqi = aInfo.Substring(riqiIndex + 4, 10);

                        HtmlNodeCollection ps = detailRootNode.SelectSingleNode("//div[@class='articleContent']").SelectNodes("./p");

                        string riqi = "";
                        string bqctuijian = "";
                        string bifentuijian = "";
                        string beidanspf = "";
                        string beidanrqshu = "";
                        string rqspf = "";
                        string rqshu = "";
                        string spfresult = "";

                        string zhudui = "";
                        string kedui = "";
                        foreach (HtmlNode p in ps)
                        {
                            if (p.InnerText.IndexOf("比赛时间") != -1)
                            {
                                int index1 = p.InnerText.IndexOf("比赛时间");
                                string[] infos = p.InnerText.Substring(index1 + 5).Replace(" ", "").Replace("&nbsp;&nbsp;&nbsp;", ",").Replace("&nbsp;&nbsp;", ",").Replace("&nbsp;", ",").Split(',');
                                if (infos.Length == 2)
                                {
                                    riqi = GetRiqiFromWeekday2(infos[0], infos[1].Substring(0,3), infos[1].Substring(3));
                                }
                                else
                                {
                                    riqi = GetRiqiFromWeekday2(infos[0], infos[1], infos[2]);
                                }
                                
                                continue;
                            }
                            //获取主队和客队名称
                            if (p.InnerText.IndexOf("亚洲盘口：") != -1)
                            {
                                int index1 = p.InnerText.IndexOf("亚洲盘口：");
                                string[] infos = p.InnerText.Substring(index1 + 5).Replace(" ", "").Replace("&nbsp;&nbsp;&nbsp;&nbsp;", ",").Replace("&nbsp;&nbsp;&nbsp;", ",").Replace("&nbsp;&nbsp;", ",").Split(',');
                                zhudui = infos[1];
                                kedui = infos[3];
                                continue;
                            }

                            if (p.InnerText.IndexOf("半全场推荐") != -1)
                            {
                                int index2 = p.InnerText.IndexOf("推荐：");
                                bqctuijian = p.InnerText.Substring(index2 + 3);
                                continue;
                            }
                            if (p.InnerText.IndexOf("比分推荐") != -1)
                            {
                                int index3 = p.InnerText.IndexOf("推荐：");
                                bifentuijian = p.InnerText.Substring(index3 + 3);
                                continue;
                            }
                            if (p.InnerText.IndexOf("北京单场") != -1)
                            {
                                int index3 = p.InnerText.IndexOf("推荐：");
                                beidanspf = p.InnerText.Substring(index3 + 3);
                                int index31 = p.InnerText.IndexOf("(");
                                if (index31 != -1)
                                {
                                    beidanrqshu = p.InnerText.Substring(index31 + 1, 2);
                                }
                                continue;
                            }
                            if (p.InnerText.IndexOf("竞彩让球") != -1)
                            {
                                int index4 = p.InnerText.IndexOf("推荐：");
                                rqspf = p.InnerText.Substring(index4 + 3);
                                int index41 = p.InnerText.IndexOf("(");
                                if (index41 != -1)
                                {
                                    rqshu = p.InnerText.Substring(index41 + 1, 2);
                                }
                                continue;
                            }
                            if (p.InnerText.IndexOf("竞彩足球") != -1)
                            {
                                int index5 = p.InnerText.IndexOf("推荐：");
                                spfresult = p.InnerText.Substring(index5 + 3);
                                continue;
                            }
                        }

                        Yuce310 yuce310 = new Yuce310();
                        yuce310.title = title;
                        yuce310.url = realUrl;
                        yuce310.bqctuijian = bqctuijian;
                        yuce310.bifentuijian = bifentuijian;
                        yuce310.beidanspfresult = beidanspf;
                        yuce310.beidanrqshu = beidanrqshu;
                        yuce310.rqspf = rqspf;
                        yuce310.rqshu = rqshu;
                        yuce310.yuceriqi = yuceriqi;
                        yuce310.spfresult = spfresult;
                        yuce310.riqi = riqi;
                        yuce310.name = "Samuel";

                        yuce310.zhudui = zhudui;
                        yuce310.kedui = kedui;

                        yuce310s.Add(yuce310);

                    }

                    new AppYuceDAL().InsertAppYuce310List(yuce310s);


                    Console.WriteLine("读取数据成功，页码为" + i);
                }               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("操作成功！");
        }

        private void btnUpdate310Bianhao_Click(object sender, EventArgs e)
        {
            //1.取得所有没有编号的行记录

            DataSet dsNullBianhao = new AppYuceDAL().GetNullBianhaoData();

            //2.通过主队或者客队名称和日期从kaijiang表里取得数据
            foreach (DataRow row in dsNullBianhao.Tables[0].Rows)
            {
                string id =row["id"].ToString();
                string riqi = row["riqi"].ToString();
                string zhudui = row["zhudui"].ToString();
                string kedui = row["kedui"].ToString();

                string bianhao = new KaijiangDAL().GetKaijiangBianhao(riqi,zhudui,kedui);

                //3.更新编号数据
                new AppYuceDAL().UpdateYuce310Bianhao(id, bianhao);
                Console.WriteLine("更新编号成功！");
            }
            MessageBox.Show("操作成功！");
            
        }

        private void btnUpdate310Yuce_Click(object sender, EventArgs e)
        {
            //1.取得所有没有更新lucky数据的记录

            DataSet dsNullLucky = new AppYuceDAL().GetYuce310NullLuckyData();

            //2.通过日期和编号获取开奖表中的记录,qcbfresult, bqcresult
            foreach (DataRow row in dsNullLucky.Tables[0].Rows)
            {
                string id = row["id"].ToString();
                string bianhao = row["bianhao"].ToString();
                string riqi = row["riqi"].ToString();

                //如果是当前时间，不进行更新操作
                string curDate = DateTime.Now.ToString("yyyy-MM-dd");
                if (riqi == curDate)
                {
                    continue;
                }
                Kaijiang kaijiang = new KaijiangDAL().GetKaijiangByRiqiAndBianhao(riqi, bianhao);
                //这里有可能预测的比赛还没有开始
                if (kaijiang == null)
                {
                    continue;
                }
                string bqctuijian = row["bqctuijian"].ToString();
                string bifentuijian = row["bifentuijian"].ToString();
                string spfresult = row["spfresult"].ToString();
                string rqspf = row["rqspf"].ToString();

                string lucky = "0";
                string rqlucky = "0";
                string bifenlucky = "0";
                string bqclucky = "0";

                //3.对比获得四个lucky数据
                if (spfresult.IndexOf(kaijiang.SpfResult) != -1)
                {
                    lucky = "1";
                }
                else
                {
                    lucky = "2";
                }
                if(rqspf.IndexOf(kaijiang.RqspfResult)  != -1)
                {
                    rqlucky = "1";
                }
                else
                {
                    rqlucky = "2";
                }
                if (bifentuijian.IndexOf(kaijiang.QcbfResult) != -1)
                {
                    bifenlucky = "1";
                }
                else
                {
                    bifenlucky = "2";
                }
                //string bqctuijianHandle = bqctuijian.Replace("/", "").Replace("胜", "3").Replace("平", "1").Replace("负", "0");
                if (bqctuijian.Replace("/","").Replace("胜","3").Replace("平","1").Replace("负","0").IndexOf(kaijiang.BqcResult) != -1)
                {
                    bqclucky = "1";
                }
                else
                {
                    bqclucky = "2";
                }
                //4.更新lucky数据
                new AppYuceDAL().UpdateYuce310Lucky(id, lucky, rqlucky, bifenlucky, bqclucky,kaijiang);


                Console.WriteLine("更新数据成功");

            }
            MessageBox.Show("操作成功");
        }

        private void 购买统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ActualBuyForm().ShowDialog();

        }

        private void 赔率分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new DataPeilvAnalysisForm().ShowDialog();
        }

        private void btnGet007_Click(object sender, EventArgs e)
        {
        //    //通过post请求获取数据
            ServicePointManager.Expect100Continue = false;

            WebClient w = new WebClient();
            w.Headers.Add("Accept", "*/*");
            w.Headers.Add("Accept-Encoding", "gzip,deflate,sdch");
            w.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
            w.Headers.Add("X-Requested-With", "XMLHttpRequest");
            w.Headers.Add("Origin", "http://news.win007.com");
            w.Headers.Add("Referer", "http://news.win007.com/football.html");
            w.Headers.Add("X-MicrosoftAjax", "Delta=true");
            w.Headers.Add("Cookie", "CNZZDATA768824=cnzz_eid%3D1104376160-1448438491-%26ntime%3D1448951231");
            w.Headers.Add("Cache-Control", "no-cache");
            w.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36");
            w.Headers.Add("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
            
            
            string viewState = @"%2FwEPDwUKLTIwNTk4MTMwOA8WAh4HY2xhc3NJRAUFMTAxMzEWAgIFD2QWDAIBD2QWAgIJD2QWAmYPZBYEAhsPFgIeC18hSXRlbUNvdW50Ag8WHmYPZBYCZg8VCQhmb290YmFsbAY2NDMxMjg25YmR5a6i56ue5b2p6Laz55CD5ZGo5LiAMDA377ya5Z%2BD5Zug6ZyN5ripRkMgVlMg5Z%2BD6ZeoDOeQg%2BaOoue8lui%2BkRAyMDE1LTExLTMwIDA4OjA1BDI3MzN2PGltZyBzcmM9Imh0dHA6Ly9waWMud2luMDA3LmNvbS9GaWxlcy9OZXdzL2JldDAwNy8zODBhNTQwYy01MjY5LTRiYmQtOTEzNy1hYjNmZjUwYzRhMTYuanBnIiB3aWR0aD0iMTcwIiBoZWlnaHQ9Ijc1IiAvPv4B5Lik6Zif5a6e5Yqb5pyJ5LiA5a6a55qE5beu6Led77yM5Z%2BD5Zug6ZyN5ripRkPov5jmi6XmnInkuLvlnLrkvJjlir%2FvvIzogIzln4Ppl6jlrqLlnLrmiJjnu6nljYHliIbkuI3nkIbmg7PvvIzkuLvpmJ%2Fov5HmnJ%2FnirbmgIHpgJDmuJDlm57ljYfvvIzlnKjkuLvlnLrku43lhbflpIfkuIDlrprnmoTmiJjmlpflipvvvIzogIzln4Ppl6jlrqLlnLrmiJjnu6nmg6josIjvvIzmnKzlnLrmr5TotZvpppbpgInkuLvog5zvvIzmrKHpgInlubPlsYDjgIK3AjxkaXYgY2xhc3M9J2FydGljbGVUYWdMbmsnPjxiIHN0eWxlPSdjb2xvcjojOTk5Oyc%2B5qCH562%2B77yaPC9iPjxhIGhyZWY9J2h0dHA6Ly93d3cud2luMDA3LmNvbS90YWcvamlhbmtlLycgdGFyZ2V0PSdfYmxhbmsnICBzdHlsZT0nY29sb3I6Ymx1ZTsnPuWJkeWuojwvYT4mbmJzcDsmbmJzcDs8YSBocmVmPSdodHRwOi8vd3d3LndpbjAwNy5jb20vdGFnL2pjenEvJyB0YXJnZXQ9J19ibGFuaycgIHN0eWxlPSdjb2xvcjpibHVlOyc%2B56ue5b2p6Laz55CDPC9hPiZuYnNwOyZuYnNwO%2BWfg%2BWboOmcjea4qUZDJm5ic3A7Jm5ic3A75Z%2BD6ZeoPC9kaXY%2BZAIBD2QWAmYPFQkIZm9vdGJhbGwGNjQzMTIxPeWJkeWuouernuW9qei2s%2BeQg%2BWRqOS4gDAwNu%2B8muWkmuW%2Bt%2BWLkuaUryBWUyDpmL%2FlsJTmooXli5Lln44M55CD5o6i57yW6L6REDIwMTUtMTEtMzAgMDg6MDYEMjI5NnY8aW1nIHNyYz0iaHR0cDovL3BpYy53aW4wMDcuY29tL0ZpbGVzL05ld3MvYmV0MDA3L2U5MjMzNTVmLWRlM2EtNDc0ZS1hYzU4LWVhNDdmNWQ5ODllYS5qcGciIHdpZHRoPSIxNzAiIGhlaWdodD0iNzUiIC8%2BzQLkuKTpmJ%2Flrp7lipvmnInkuIDlrprnmoTlt67ot53vvIzlpJrlvrfli5LmlK%2Fov5jmi6XmnInkuLvlnLrkvJjlir%2FvvIzmraTnlarlr7npmLXov5HmnJ%2FkvY7ov7fnmoTpmL%2FlsJTmooXli5Lln47lnKjkuLvlnLrlv4Xlrprlhajlipvmi7%2FliLAz5YiG77yM5qyn6LWU5pa56Z2i5byA5Ye6MS42MiAzLjgxIDQuOTfvvIzkuLvpmJ%2Fov5HmnJ%2FnirbmgIHpgJDmuJDlm57ljYfvvIzlnKjkuLvlnLrku43lhbflpIfkuIDlrprnmoTmiJjmlpflipvvvIzogIzpmL%2FlsJTmooXli5Lln47lrqLlnLrmiJjnu6nmg6josIjvvIzmnKzlnLrmr5TotZvpppbpgInkuLvog5zvvIzmrKHpgInlubPlsYDjgIK%2BAjxkaXYgY2xhc3M9J2FydGljbGVUYWdMbmsnPjxiIHN0eWxlPSdjb2xvcjojOTk5Oyc%2B5qCH562%2B77yaPC9iPjxhIGhyZWY9J2h0dHA6Ly93d3cud2luMDA3LmNvbS90YWcvamlhbmtlLycgdGFyZ2V0PSdfYmxhbmsnICBzdHlsZT0nY29sb3I6Ymx1ZTsnPuWJkeWuojwvYT4mbmJzcDsmbmJzcDs8YSBocmVmPSdodHRwOi8vd3d3LndpbjAwNy5jb20vdGFnL2pjenEvJyB0YXJnZXQ9J19ibGFuaycgIHN0eWxlPSdjb2xvcjpibHVlOyc%2B56ue5b2p6Laz55CDPC9hPiZuYnNwOyZuYnNwO%2BWkmuW%2Bt%2BWLkuaUryZuYnNwOyZuYnNwO%2BmYv%2BWwlOaiheWLkuWfjjwvZGl2PmQCAg9kFgJmDxUJCGZvb3RiYWxsBjY0MzExNEDliZHlrqLnq57lvanotrPnkIPlkajkuIAwMDXvvJrnmbvljZrmgJ0gVlMg5Z%2BD5Zug6ZyN5rip6Z2S5bm06ZifDOeQg%2BaOoue8lui%2BkRAyMDE1LTExLTMwIDA4OjA2BDE3Nzh2PGltZyBzcmM9Imh0dHA6Ly9waWMud2luMDA3LmNvbS9GaWxlcy9OZXdzL2JldDAwNy9mMGJjMjRiZC0xZjdkLTQ4OTQtOTAwNi02NzE5YTZjZjgxNWUuanBnIiB3aWR0aD0iMTcwIiBoZWlnaHQ9Ijc1IiAvPp4C5Lik6Zif56ev5YiG5LuF5beuMeWIhu%2B8jOasp%2Bi1lOaWuemdouW8gOWHujEuNzEgMy42OCA0LjQy77yM5Li76Zif6L%2BR5pyf6Jm96YGt6YGHMui%2Fnui0pe%2B8jOS9huWcqOS4u%2BWcuuS7jeWFt%2BWkh%2BS4gOWumueahOaImOaWl%2BWKm%2B%2B8jOiAjOi0n%2Bi1lOmZjeW5heS4jeWwj%2B%2B8jOaImOaEj%2BWNgei2s%2BeahOWuoumYn%2BWBmuWHuumYsuiMg%2BOAgue7vOWQiOWIhuaekO%2B8jOeZu%2BWNmuaAneatpOW9ueS4jeS8muepuuaJi%2BiAjOW9ku%2B8jOacrOWcuuavlOi1m%2BmmlumAieS4u%2BiDnO%2B8jOasoemAieW5s%2BWxgOOAguIBPGRpdiBjbGFzcz0nYXJ0aWNsZVRhZ0xuayc%2BPGIgc3R5bGU9J2NvbG9yOiM5OTk7Jz7moIfnrb7vvJo8L2I%2BPGEgaHJlZj0naHR0cDovL3d3dy53aW4wMDcuY29tL3RhZy9qaWFua2UvJyB0YXJnZXQ9J19ibGFuaycgIHN0eWxlPSdjb2xvcjpibHVlOyc%2B5YmR5a6iPC9hPiZuYnNwOyZuYnNwO%2BernuW9qei2s%2BeQg%2BeZu%2BWNmuaAnSZuYnNwOyZuYnNwO%2BWfg%2BWboOmcjea4qemdkuW5tOmYnzwvZGl2PmQCAw9kFgJmDxUJCGZvb3RiYWxsBjY0MzEwOEnmnpflhJLlmInnq57lvanotrPnkIPlkajkuIAwMDLvvJrojqvmlq%2Fnp5Hov6rnurPmkakgVlMg6I6r5pav56eR54Gr6L2m5aS0DOeQg%2BaOoue8lui%2BkRAyMDE1LTExLTI5IDExOjUzBDMyNzN2PGltZyBzcmM9Imh0dHA6Ly9waWMud2luMDA3LmNvbS9GaWxlcy9OZXdzL2JldDAwNy82MGZjNWY2NC03MDYwLTQxOTktYTVmNy00OWE4ZmM0NTA5ODMuanBnIiB3aWR0aD0iMTcwIiBoZWlnaHQ9Ijc1IiAvPoEC5Lqa55uY5byA5Ye65bmz5omL5Lit6auY5rC077yM5ZCO5biC5Li76Zif5rC05L2N5oyB57ut5Y2H6auY77yb5qyn6LWU5bmz5Z2H5oyH5pWwMi43MSAzLjEzIDIuNTPvvIzlj6%2Fop4HluoTlrrbpnZ7luLjosKjmhY7vvIzlubbml6DkvZzlh7rlgL7lkJHmgKfnmoTpgInmi6nvvJvkuKTpmJ%2Fljoblj7LkuqTplIvkuK3vvIzov5HljYHlnLrmr5TotZvojqvmlq%2Fnp5Hov6rnurPmkanlj5blvpc06IOcM%2BW5szPotJ%2FnmoTmiJjnu6nvvIznqI3ljaDkuIrpo46GAzxkaXYgY2xhc3M9J2FydGljbGVUYWdMbmsnPjxiIHN0eWxlPSdjb2xvcjojOTk5Oyc%2B5qCH562%2B77yaPC9iPjxhIGhyZWY9J2h0dHA6Ly93d3cud2luMDA3LmNvbS90YWcvbHJqaWEvJyB0YXJnZXQ9J19ibGFuaycgIHN0eWxlPSdjb2xvcjpibHVlOyc%2B5p6X5YSS5ZiJPC9hPiZuYnNwOyZuYnNwOzxhIGhyZWY9J2h0dHA6Ly93d3cud2luMDA3LmNvbS90YWcvamN6cS8nIHRhcmdldD0nX2JsYW5rJyAgc3R5bGU9J2NvbG9yOmJsdWU7Jz7nq57lvanotrPnkIM8L2E%2BJm5ic3A7Jm5ic3A7PGEgaHJlZj0naHR0cDovL3d3dy53aW4wMDcuY29tL3RhZy9zcGYvJyB0YXJnZXQ9J19ibGFuaycgIHN0eWxlPSdjb2xvcjpibHVlOyc%2B6IOc5bmz6LSfPC9hPiZuYnNwOyZuYnNwO%2BS%2FhOi2hTwvZGl2PmQCBA9kFgJmDxUJCGZvb3RiYWxsBjY0MzEwNkPmnpflhJLlmInnq57lvanotrPnkIPlkajkuIAwMDPvvJrlloDlsbHpsoHlrr4gVlMg6I6r5pav56eR5pav5be06L6%2BDOeQg%2BaOoue8lui%2BkRAyMDE1LTExLTMwIDA4OjA2BDMyMzZ2PGltZyBzcmM9Imh0dHA6Ly9waWMud2luMDA3LmNvbS9GaWxlcy9OZXdzL2JldDAwNy8xZTgxMTU5NS05OWJhLTQ3YWUtOTFiYS0xYWNjNjhlZmNiNmMuanBnIiB3aWR0aD0iMTcwIiBoZWlnaHQ9Ijc1IiAvPv8C5Lqa55uY5byA5Ye65bmz5omL5L2O5rC077yM5ZCO5biC5Li76Zif5rC05L2N5oyB57ut5LiK5Y2H77yb5qyn6LWU5bmz5Z2H5oyH5pWwMi41MyAzLjE3IDIuNjfvvIzlj6%2Fop4HluoTlrrbpnZ7luLjosKjmhY7vvIzlubbml6DkvZzlh7rlgL7lkJHmgKfnmoTpgInmi6nvvJvkuKTpmJ%2Fljoblj7LkuqTplIvkuK3vvIzov5HljYHlnLrlloDlsbHpsoHlrr7ku4Xlj5blvpfkuIDog5zvvIzlvoDnu6nlrozlhajljaDmja7kuIvpo47vvJvov5HkupTkuKrkuLvlnLrpnaLlr7nojqvmlq%2Fnp5Hmlq%2Flt7Tovr7mnKrlsJ3og5znu6nvvIzlj5HmjKXnm7jlvZPns5%2Fns5XvvJvmnKzlnLrkuKrkurrnnIvlpb3ojqvmlq%2Fnp5Hmlq%2Flt7Tovr7lrqLmiJjkuI3otKXlhajouqvogIzpgIDjgIKGAzxkaXYgY2xhc3M9J2FydGljbGVUYWdMbmsnPjxiIHN0eWxlPSdjb2xvcjojOTk5Oyc%2B5qCH562%2B77yaPC9iPjxhIGhyZWY9J2h0dHA6Ly93d3cud2luMDA3LmNvbS90YWcvbHJqaWEvJyB0YXJnZXQ9J19ibGFuaycgIHN0eWxlPSdjb2xvcjpibHVlOyc%2B5p6X5YSS5ZiJPC9hPiZuYnNwOyZuYnNwOzxhIGhyZWY9J2h0dHA6Ly93d3cud2luMDA3LmNvbS90YWcvamN6cS8nIHRhcmdldD0nX2JsYW5rJyAgc3R5bGU9J2NvbG9yOmJsdWU7Jz7nq57lvanotrPnkIM8L2E%2BJm5ic3A7Jm5ic3A7PGEgaHJlZj0naHR0cDovL3d3dy53aW4wMDcuY29tL3RhZy9zcGYvJyB0YXJnZXQ9J19ibGFuaycgIHN0eWxlPSdjb2xvcjpibHVlOyc%2B6IOc5bmz6LSfPC9hPiZuYnNwOyZuYnNwO%2BS%2FhOi2hTwvZGl2PmQCBQ9kFgJmDxUJCGZvb3RiYWxsBjY0MzEwNEHmnpflhJLlmInnq57lvanotrPnkIPlkajkuIAwMjDvvJrnk6blpYflt7TmiZggIFZTIOW4leiOseaWr%2BiSguivugznkIPmjqLnvJbovpEQMjAxNS0xMS0zMCAwODowNwQxNDMydjxpbWcgc3JjPSJodHRwOi8vcGljLndpbjAwNy5jb20vRmlsZXMvTmV3cy9iZXQwMDcvOGY4ZDQ5NTktYjNiOS00YjNhLWExNjMtMTg5YzhhNGMwODM3LmpwZyIgd2lkdGg9IjE3MCIgaGVpZ2h0PSI3NSIgLz7YAuS6muebmOW8gOWHuuW5s%2BaJi%2BmrmOawtO%2B8jOWQjuW4guawtOS9jeW5tuaXoOWkquWkp%2BazouWKqO%2B8jOasp%2Bi1lOW5s%2BWdh%2BaMh%2BaVsDIuNTYgMy4yOSAyLjQ477yM5Y%2Bv6KeB5bqE5a626Z2e5bi46LCo5oWO77yM5Li75a6i5Lik6Zif55qE6LWU546H6Z2e5bi46IO2552A77yb5Zyo5Lik6Zif5Y6G5Y%2By5Lqk6ZSL5Lit77yM6L%2BR5Y2B5Zy65biV6I6x5pav6JKC6K%2B65Y%2BW5b6XNeiDnDPlubMy6LSf55qE5oiY57up77yM5Y2g5o2u5LyY5Yq%2F77yb55Om5aWH5be06L%2BR6YeR5YWt5Liq5Li75Zy66Z2i5a%2B55biV6I6x5pav6JKC6K%2B65Y%2Bq5pS26I635LiA6IOc77yM5b6A57up5aSE5LqO5piO5pi%2B5LiL6aOOiQM8ZGl2IGNsYXNzPSdhcnRpY2xlVGFnTG5rJz48YiBzdHlsZT0nY29sb3I6Izk5OTsnPuagh%2Betvu%2B8mjwvYj48YSBocmVmPSdodHRwOi8vd3d3LndpbjAwNy5jb20vdGFnL2xyamlhLycgdGFyZ2V0PSdfYmxhbmsnICBzdHlsZT0nY29sb3I6Ymx1ZTsnPuael%2BWEkuWYiTwvYT4mbmJzcDsmbmJzcDs8YSBocmVmPSdodHRwOi8vd3d3LndpbjAwNy5jb20vdGFnL2pjenEvJyB0YXJnZXQ9J19ibGFuaycgIHN0eWxlPSdjb2xvcjpibHVlOyc%2B56ue5b2p6Laz55CDPC9hPiZuYnNwOyZuYnNwOzxhIGhyZWY9J2h0dHA6Ly93d3cud2luMDA3LmNvbS90YWcvc3BmLycgdGFyZ2V0PSdfYmxhbmsnICBzdHlsZT0nY29sb3I6Ymx1ZTsnPuiDnOW5s%2Bi0nzwvYT4mbmJzcDsmbmJzcDvmmbrliKnnlLI8L2Rpdj5kAgYPZBYCZg8VCQhmb290YmFsbAY2NDMxMDM35bCP6Zu%2B56ue5b2p6Laz55CD5ZGo5LiAMDIx77ya5biM6YeR5pavIFZTIOenkeW4g%2Bmbt%2Be0ogznkIPmjqLnvJbovpEQMjAxNS0xMS0zMCAwODowNwQxNjE0djxpbWcgc3JjPSJodHRwOi8vcGljLndpbjAwNy5jb20vRmlsZXMvTmV3cy9iZXQwMDcvZjkzOGVlNmItODlhNC00OThjLThmOGMtZjA5NzZiY2U2YjdmLmpwZyIgd2lkdGg9IjE3MCIgaGVpZ2h0PSI3NSIgLz7aAuS6muebmOW8gOWHuuS4u%2BiuqeWNii%2FkuIDkvY7msLTvvIzlkI7luILkuLvpmJ%2FmsLTkvY3mjIHnu63kuIvpmY3vvJvmrKfotZTlubPlnYfmjIfmlbAxLjUzIDMuOTIgNS4zM%2B%2B8jOW8gOWHujEuNTPnmoTotoXkvY7kuLvog5zotZTnjofvvIzlj6%2Fop4HluoTlrrbkuIDoh7TnnIvlpb3luIzph5Hmlq%2FkuLvlnLrlhajlj5bkuInliIbvvJvkuKTpmJ%2Fljoblj7LkuqTplIvkuK3vvIzov5HljYHlnLrluIzph5Hmlq%2FmlLbojrc06IOcMeW5szXotJ%2FnqI3ljaDkuIvpo47vvJvljZXov5HkupTkuKrkuLvlnLrlj6rmnInkuIDotKXvvIznp5HluIPpm7fntKLlvojpmr7lnKjluIzph5Hmlq%2FkuLvlnLrljaDliLDkvJjlir%2ByAjxkaXYgY2xhc3M9J2FydGljbGVUYWdMbmsnPjxiIHN0eWxlPSdjb2xvcjojOTk5Oyc%2B5qCH562%2B77yaPC9iPuWwj%2BmbviZuYnNwOyZuYnNwOzxhIGhyZWY9J2h0dHA6Ly93d3cud2luMDA3LmNvbS90YWcvamN6cS8nIHRhcmdldD0nX2JsYW5rJyAgc3R5bGU9J2NvbG9yOmJsdWU7Jz7nq57lvanotrPnkIM8L2E%2BJm5ic3A7Jm5ic3A7PGEgaHJlZj0naHR0cDovL3d3dy53aW4wMDcuY29tL3RhZy9zcGYvJyB0YXJnZXQ9J19ibGFuaycgIHN0eWxlPSdjb2xvcjpibHVlOyc%2B6IOc5bmz6LSfPC9hPiZuYnNwOyZuYnNwO%2BaZuuWIqeeUsjwvZGl2PmQCBw9kFgJmDxUJCGZvb3RiYWxsBjY0MzEwMDflsI%2Fpm77nq57lvanotrPnkIPlkajkuIAwMDnvvJrnpo%2FkvKbkuLkgVlMg5YmN6L%2Bb5LmL6bmwDOeQg%2BaOoue8lui%2BkRAyMDE1LTExLTMwIDA4OjA3BDI0NzZ2PGltZyBzcmM9Imh0dHA6Ly9waWMud2luMDA3LmNvbS9GaWxlcy9OZXdzL2JldDAwNy9mODk0MjU3ZS1lN2Y4LTRhNjYtYmZjYS1iNjdkNDIyZWJjOWIuanBnIiB3aWR0aD0iMTcwIiBoZWlnaHQ9Ijc1IiAvPpID5Lqa55uY5byA5Ye65Li76K6p5bmzL%2BWNiuS9juawtO%2B8jOWQjuW4guS4u%2BmYn%2BawtOS9jeaMgee7reS4iuWNh%2B%2B8m%2Basp%2Bi1lOW5s%2BWdh%2BaMh%2BaVsDIuNDQgMy4zOCAyLjY077yM5bqE5a626Z2e5bi46LCo5oWO77yM5bm25peg5L2c5Ye65YC%2B5ZCR5oCn55qE6YCJ5oup77yb5Lik6Zif5Y6G5Y%2By5Lqk6ZSL5Lit77yM6L%2BR5Y2B5Zy65YmN6L%2Bb5LmL6bmw5Y%2BW5b6XNeiDnDLlubMz6LSf55qE5oiY57up77yM56iN5Y2g5LiK6aOO77yb5L2G6L%2BR5LqU5Liq5a6i5Zy66Z2i5a%2B556aP5Lym5Li577yM5YmN6L%2Bb5LmL6bmw5Y%2Bq5Y%2BW5b6X5LiA6IOc77yM5a6i5oiY57u0572X5bC85Y2h55CD5Zy65b6A5b6A5Yqb5LiN5LuO5b%2BD77yM5pys5Zy65Zyo5ZCO5biC5Li76Zif5rC05L2N5Y2H6auY55qE5oOF5Ya15LiLrwI8ZGl2IGNsYXNzPSdhcnRpY2xlVGFnTG5rJz48YiBzdHlsZT0nY29sb3I6Izk5OTsnPuagh%2Betvu%2B8mjwvYj7lsI%2Fpm74mbmJzcDsmbmJzcDs8YSBocmVmPSdodHRwOi8vd3d3LndpbjAwNy5jb20vdGFnL2pjenEvJyB0YXJnZXQ9J19ibGFuaycgIHN0eWxlPSdjb2xvcjpibHVlOyc%2B56ue5b2p6Laz55CDPC9hPiZuYnNwOyZuYnNwOzxhIGhyZWY9J2h0dHA6Ly93d3cud2luMDA3LmNvbS90YWcvc3BmLycgdGFyZ2V0PSdfYmxhbmsnICBzdHlsZT0nY29sb3I6Ymx1ZTsnPuiDnOW5s%2Bi0nzwvYT4mbmJzcDsmbmJzcDvojbfkuZk8L2Rpdj5kAggPZBYCZg8VCQhmb290YmFsbAY2NDMwOTlJ5bCP6Zu%2B56ue5b2p6Laz55CD5ZGo5LiAMDEx77ya6Zi%2F6LS%2B5YWL5pav6Z2S5bm06ZifIFZTIOm5v%2BeJueS4ueaWr%2BW3tOi%2BvgznkIPmjqLnvJbovpEQMjAxNS0xMS0zMCAwODowNwMzOTF2PGltZyBzcmM9Imh0dHA6Ly9waWMud2luMDA3LmNvbS9GaWxlcy9OZXdzL2JldDAwNy9lZmVlYzIzMS05OTFkLTQwNTctODRiNy04OWRkOTQyMjdhODEuanBnIiB3aWR0aD0iMTcwIiBoZWlnaHQ9Ijc1IiAvPugC5Lqa55uY5byA5Ye65Li75Y%2BX6K6p5Y2KL%2BS4gOS4remrmOawtO%2B8jOS4u%2BmYn%2BawtOS9jeaMgee7reS4iuWNh%2B%2B8jOasp%2Bi1lOW5s%2BWdh%2BaMh%2BaVsDQuNDIgMy43NSAxLjY577yM5byA5Ye6MS42OeeahOesrOWuouiDnOi1lOeOh%2B%2B8jOWPr%2BingeW6hOWutuWvueWuoumYn%2BaMgeacieS%2FoeW%2Fg%2B%2B8m%2BS4pOmYn%2BWOhuWPsuS6pOmUi%2BS4re%2B8jOi%2FkeWbm%2BWcuumYv%2Bi0vuWFi%2BaWr%2BmdkuW5tOmYn%2BWPluW%2BlzPog5wx6LSf55qE5oiY57up77yM5b6A57up5Y2g5LyY77yb5L2G5pys5Zy65ZCO5biC5Li76Zif5rC05L2N5oyB57ut5LiK5Y2H55qE5oOF5Ya15LiL77yM5Y%2Bv6KeB5bqE5a625a%2B55a6i6IOc5L%2Bh5b%2BD5LiN5pat5aKe5by677ybrwI8ZGl2IGNsYXNzPSdhcnRpY2xlVGFnTG5rJz48YiBzdHlsZT0nY29sb3I6Izk5OTsnPuagh%2Betvu%2B8mjwvYj7lsI%2Fpm74mbmJzcDsmbmJzcDs8YSBocmVmPSdodHRwOi8vd3d3LndpbjAwNy5jb20vdGFnL2pjenEvJyB0YXJnZXQ9J19ibGFuaycgIHN0eWxlPSdjb2xvcjpibHVlOyc%2B56ue5b2p6Laz55CDPC9hPiZuYnNwOyZuYnNwOzxhIGhyZWY9J2h0dHA6Ly93d3cud2luMDA3LmNvbS90YWcvc3BmLycgdGFyZ2V0PSdfYmxhbmsnICBzdHlsZT0nY29sb3I6Ymx1ZTsnPuiDnOW5s%2Bi0nzwvYT4mbmJzcDsmbmJzcDvojbfkuZk8L2Rpdj5kAgkPZBYCZg8VCQhmb290YmFsbAY2NDI4OTE35a2k5b2x56ue5b2p6Laz55CD5ZGo5pelMDQz77ya5be05YuS6I6rIFZTIOWwpOaWh%2BWbvuaWrwznkIPmjqLnvJbovpEQMjAxNS0xMS0yOSAwODozNQQ0MTAwdjxpbWcgc3JjPSJodHRwOi8vcGljLndpbjAwNy5jb20vRmlsZXMvTmV3cy9iZXQwMDcvMDZhYzk0NjYtNWQ0Yy00YTQ4LWFiMmUtODJiNjcxZjU5YmYwLmpwZyIgd2lkdGg9IjE3MCIgaGVpZ2h0PSI3NSIgLz6OAuS6muebmOW8gOWHuuS4u%2BWPl%2BiuqeWNii%2FkuIDpq5jmsLTliJ3nm5jvvIzlkI7mgKXljYfnm5jpmY3msLToh7Plj5forqnkuIDnkIPkvY7msLTvvIzluoTlrrbmraTkuL7mmI7mmL7pmLvkuIror7HkuIvvvJvnhLbogIzlnKjlr7notZvlvoDnu6nmlrnpnaLvvIzlsKTmloflm77mlq%2Flt7Llj5Y26L%2Be6IOc77yb5Liq5Lq66K6k5Li677yM5pys5Zy65bCk5paH5Zu%2B5pav5a6i5Zy66LWi55CD6LWi55uY5LiN5oiQ6Zeu6aKY77yM5a%2B56LWbN%2Bi%2FnuiDnOaJi%2BWIsOaTkuadpeOAgvACPGRpdiBjbGFzcz0nYXJ0aWNsZVRhZ0xuayc%2BPGIgc3R5bGU9J2NvbG9yOiM5OTk7Jz7moIfnrb7vvJo8L2I%2BPGEgaHJlZj0naHR0cDovL3d3dy53aW4wMDcuY29tL3RhZy9neWluZy8nIHRhcmdldD0nX2JsYW5rJyAgc3R5bGU9J2NvbG9yOmJsdWU7Jz7lraTlvbE8L2E%2BJm5ic3A7Jm5ic3A7PGEgaHJlZj0naHR0cDovL3d3dy53aW4wMDcuY29tL3RhZy9qY3pxLycgdGFyZ2V0PSdfYmxhbmsnICBzdHlsZT0nY29sb3I6Ymx1ZTsnPuernuW9qei2s%2BeQgzwvYT4mbmJzcDsmbmJzcDs8YSBocmVmPSdodHRwOi8vd3d3LndpbjAwNy5jb20vdGFnL3lpamlhLycgdGFyZ2V0PSdfYmxhbmsnICBzdHlsZT0nY29sb3I6Ymx1ZTsnPuaEj%2BeUsjwvYT48L2Rpdj5kAgoPZBYCZg8VCQhmb290YmFsbAY2NDI4ODc05a2k5b2x56ue5b2p6Laz55CD5ZGo5pelMDMy77ya5oGp5rOi5YipIFZTIOaLiem9kOWlpQznkIPmjqLnvJbovpEQMjAxNS0xMS0yOSAwODozNQQzMjEwdjxpbWcgc3JjPSJodHRwOi8vcGljLndpbjAwNy5jb20vRmlsZXMvTmV3cy9iZXQwMDcvMTExNTY3MDItNTE5Yy00NWQyLWI3YzQtYTcwMWU4NjVhNDdjLmpwZyIgd2lkdGg9IjE3MCIgaGVpZ2h0PSI3NSIgLz6IAuS6muebmOW8gOWHuuS4u%2BWPl%2BiuqeW5sy%2FljYrpq5jmsLTliJ3nm5jvvIzlkI7mgKXpmY3kuLrkvY7msLTvvIzluoTlrrbmmI7mmL7mmK%2FlhbPms6jliLDmi4npvZDlpaXms6jph43mna%2FotZvov5nkuIDngrnvvIzlm6DmraTmnInmiYDpmLLojIPvvJvogIzlnKjlr7notZvlvoDnu6nlsYLpnaLkuIrvvIzmganms6LliKnov5Ez5Zy6MuiDnO%2B8jOW%2Fg%2BeQhuacieS4gOWumuS8mOWKv%2B%2B8m%2BS4quS6uuiupOS4uu%2B8jOaBqeazouWIqeacieacm%2BS4u%2BWcuuS4jei0peOAgvACPGRpdiBjbGFzcz0nYXJ0aWNsZVRhZ0xuayc%2BPGIgc3R5bGU9J2NvbG9yOiM5OTk7Jz7moIfnrb7vvJo8L2I%2BPGEgaHJlZj0naHR0cDovL3d3dy53aW4wMDcuY29tL3RhZy9neWluZy8nIHRhcmdldD0nX2JsYW5rJyAgc3R5bGU9J2NvbG9yOmJsdWU7Jz7lraTlvbE8L2E%2BJm5ic3A7Jm5ic3A7PGEgaHJlZj0naHR0cDovL3d3dy53aW4wMDcuY29tL3RhZy9qY3pxLycgdGFyZ2V0PSdfYmxhbmsnICBzdHlsZT0nY29sb3I6Ymx1ZTsnPuernuW9qei2s%2BeQgzwvYT4mbmJzcDsmbmJzcDs8YSBocmVmPSdodHRwOi8vd3d3LndpbjAwNy5jb20vdGFnL3lpamlhLycgdGFyZ2V0PSdfYmxhbmsnICBzdHlsZT0nY29sb3I6Ymx1ZTsnPuaEj%2BeUsjwvYT48L2Rpdj5kAgsPZBYCZg8VCQhmb290YmFsbAY2NDI4ODVA5a2k5b2x56ue5b2p6Laz55CD5ZGo5pelMDI577ya6KW%2F5rGJ5aeG6IGUIFZTIOilv%2BW4g%2Be9l%2BWnhue7tOWlhwznkIPmjqLnvJbovpEQMjAxNS0xMS0yOSAwODozNQQzNTAzdjxpbWcgc3JjPSJodHRwOi8vcGljLndpbjAwNy5jb20vRmlsZXMvTmV3cy9iZXQwMDcvY2EzZGQyY2MtM2Q2Yy00NGI4LThkMGYtNWJkMDE5NGJkYTkzLmpwZyIgd2lkdGg9IjE3MCIgaGVpZ2h0PSI3NSIgLz6tAuS6muebmOW8gOWHuuS4u%2BiuqeWNii%2FkuIDpq5jmsLTliJ3nm5jvvIzlkI7mgKXpmY3nm5jljYfmsLToh7PljYrnkIPkuK3pq5jmsLTvvIzluoTlrrbmmI7mmL7mmK%2Flr7nkuLvpmJ%2Fkv6Hlv4PkuI3otrPvvJvnhLbogIzlnKjlr7notZvlvoDnu6nmlrnpnaLvvIzopb%2FmsYnlp4bogZTkuLvlnLrmnKrlsJ3otKXnu6nvvIzov5nmiJbmmK%2Fku5bku6zlj6%2Fku6XngqvogIDnmoTmlbDmja7vvJvkvYbkuKrkurrorqTkuLrvvIzmnKzlnLrkuIvnm5jlvIDlh7rlh6DnjofovoPlpKfvvIzpppbpgInlrqLog5zvvIzmrKHpgInlubPlsYDjgILzAjxkaXYgY2xhc3M9J2FydGljbGVUYWdMbmsnPjxiIHN0eWxlPSdjb2xvcjojOTk5Oyc%2B5qCH562%2B77yaPC9iPjxhIGhyZWY9J2h0dHA6Ly93d3cud2luMDA3LmNvbS90YWcvZ3lpbmcvJyB0YXJnZXQ9J19ibGFuaycgIHN0eWxlPSdjb2xvcjpibHVlOyc%2B5a2k5b2xPC9hPiZuYnNwOyZuYnNwOzxhIGhyZWY9J2h0dHA6Ly93d3cud2luMDA3LmNvbS90YWcvamN6cS8nIHRhcmdldD0nX2JsYW5rJyAgc3R5bGU9J2NvbG9yOmJsdWU7Jz7nq57lvanotrPnkIM8L2E%2BJm5ic3A7Jm5ic3A7PGEgaHJlZj0naHR0cDovL3d3dy53aW4wMDcuY29tL3RhZy95aW5nY2hhby8nIHRhcmdldD0nX2JsYW5rJyAgc3R5bGU9J2NvbG9yOmJsdWU7Jz7oi7HotoU8L2E%2BPC9kaXY%2BZAIMD2QWAmYPFQkIZm9vdGJhbGwGNjQyODE2POmCueeOiem%2BmeernuW9qei2s%2BeQg%2BWRqOaXpTA0Mjog5aGe57u05Yip5LqaIFZTIOW3tOS8puilv%2BS6mgznkIPmjqLnvJbovpEQMjAxNS0xMS0yOSAwODozNQQzNzI1djxpbWcgc3JjPSJodHRwOi8vcGljLndpbjAwNy5jb20vRmlsZXMvTmV3cy9iZXQwMDcvMThmZmZjYjctYzhlMi00MWYyLWJjMjQtYjk2NTg3ODEwOTQ1LmpwZyIgd2lkdGg9IjE3MCIgaGVpZ2h0PSI3NSIgLz7YAeebruWJjeS4iuebmOawtOS9jeWkhOWcqDAuODDlt6blj7PjgILmrKfotZTmlrnpnaLnu5nlh7rkuoYxLjg0IDMuNjYgMy45M%2B%2B8jOWQjuW4gui1lOeOh%2BWPmOWMluS4jeWkp%2B%2B8jOS4pOmYn%2Bi%2Fkeacn%2BeKtuaAgemDveS4jemUme%2B8jOatpOW9ueWPiOaYr%2BS4u%2BWcuu%2B8jOeQhuW6lOmrmOeci%2B%2B8jOS4quS6uuacrOWcuuavlOi1m%2BmmlumAieS4u%2BiDnO%2B8jOasoemAieW5s%2BWxgOOAgr8CPGRpdiBjbGFzcz0nYXJ0aWNsZVRhZ0xuayc%2BPGIgc3R5bGU9J2NvbG9yOiM5OTk7Jz7moIfnrb7vvJo8L2I%2BPGEgaHJlZj0naHR0cDovL3d3dy53aW4wMDcuY29tL3RhZy96eXVsb25nLycgdGFyZ2V0PSdfYmxhbmsnICBzdHlsZT0nY29sb3I6Ymx1ZTsnPumCueeOiem%2BmTwvYT4mbmJzcDsmbmJzcDs8YSBocmVmPSdodHRwOi8vd3d3LndpbjAwNy5jb20vdGFnL2pjenEvJyB0YXJnZXQ9J19ibGFuaycgIHN0eWxlPSdjb2xvcjpibHVlOyc%2B56ue5b2p6Laz55CDPC9hPiZuYnNwOyZuYnNwO%2BWhnue7tOWIqeS6miZuYnNwOyZuYnNwO%2BW3tOS8puilv%2BS6mjwvZGl2PmQCDQ9kFgJmDxUJCGZvb3RiYWxsBjY0MjgxNTPpgrnnjonpvpnnq57lvanotrPnkIPlkajml6UwNDQ6IOmprOi1myBWUyDmkannurPlk6UM55CD5o6i57yW6L6REDIwMTUtMTEtMjkgMDg6MzUEMjUyNXY8aW1nIHNyYz0iaHR0cDovL3BpYy53aW4wMDcuY29tL0ZpbGVzL05ld3MvYmV0MDA3L2VkODdlMWU5LTU0YjItNDBjZS1hYTFlLTBiMzllOWM0MDI0Yi5qcGciIHdpZHRoPSIxNzAiIGhlaWdodD0iNzUiIC8%2BngPmraTmiJjlj4zmlrnlkI3msJTpg73lvojlpKfvvIzogIzph43ngrnmlrnpnaLlnKjkuo7ln7rmnKzpnaLkuIrpqazotZvmmK%2FlpITkuo7kuIvpo47vvIzogIzmmJPog5zmraTkuL7mmK%2FpmLvkuIrnmoTmiYvms5XvvIzogIzlhbbku5blhazlj7jnmoTljYfmsLTkuZ%2FmnoHkuLrlkLvlkIjjgILlho3nnIvmrKfotZTmlrnpnaLlubPlnYfotZTnjofkuIrkuLvog5znqI3kvZzkuIvosIPvvIzogIzlrqLog5zmj5Dpq5jvvIzmnKzmiJjlr7nlhrPpqazotZvmnInmnLrkvJrnu5PmnZ%2FpnaLlr7nlr7nmiYvov57otKXnmoTorrDlvZXjgILvvIzmlbTkvZPlvaLosaHlpb3kuo7pqazotZvvvIznnIvlpb3mkannurPlk6XlrqLlnLrluKbotbDkuInliIbvvIzkuKrkurrmnKzlnLrmr5TotZvpppbpgInlrqLog5zvvIzmrKHpgInlubPlsYDjgIKHAzxkaXYgY2xhc3M9J2FydGljbGVUYWdMbmsnPjxiIHN0eWxlPSdjb2xvcjojOTk5Oyc%2B5qCH562%2B77yaPC9iPjxhIGhyZWY9J2h0dHA6Ly93d3cud2luMDA3LmNvbS90YWcvenl1bG9uZy8nIHRhcmdldD0nX2JsYW5rJyAgc3R5bGU9J2NvbG9yOmJsdWU7Jz7pgrnnjonpvpk8L2E%2BJm5ic3A7Jm5ic3A7PGEgaHJlZj0naHR0cDovL3d3dy53aW4wMDcuY29tL3RhZy9qY3pxLycgdGFyZ2V0PSdfYmxhbmsnICBzdHlsZT0nY29sb3I6Ymx1ZTsnPuernuW9qei2s%2BeQgzwvYT4mbmJzcDsmbmJzcDs8YSBocmVmPSdodHRwOi8vd3d3LndpbjAwNy5jb20vdGFnL21zLycgdGFyZ2V0PSdfYmxhbmsnICBzdHlsZT0nY29sb3I6Ymx1ZTsnPumprOi1mzwvYT4mbmJzcDsmbmJzcDvmkannurPlk6U8L2Rpdj5kAg4PZBYCZg8VCQhmb290YmFsbAY2NDI4MTRC6YK5546J6b6Z56ue5b2p6Laz55CD5ZGo5pelMDMzOiDlt7TliJfljaHor7ogVlMg5q%2BV5bCU5be06YSC56ue5oqADOeQg%2BaOoue8lui%2BkRAyMDE1LTExLTI5IDA4OjM0BDI4ODV2PGltZyBzcmM9Imh0dHA6Ly9waWMud2luMDA3LmNvbS9GaWxlcy9OZXdzL2JldDAwNy9hYmIzODUzYy1iY2VlLTQ2NTctYjQ5Ny05OTdmMTczMjdjMDUuanBnIiB3aWR0aD0iMTcwIiBoZWlnaHQ9Ijc1IiAvPoQC5be05YiX5Y2h6K%2B66LW35LyP5LiN5a6a77yM6L6T6L6T6LWi6LWi77yM5q%2BV5bCU5be06YSC56ue5oqA6L%2BRNuWcuuS7hTHotKXvvIzmr5Xnq5%2Fmr5XlsJTlt7TphILnq57mioDmmK%2FkuIDnm7TpnZ7luLjlh7roibLnmoTnkIPpmJ%2FvvIzmlbTkvZPlvaLosaHlpb3kuo7lt7TliJfljaHor7rvvIznnIvlpb3mr5XlsJTlt7TphILnq57mioDlrqLlnLrluKbotbDkuInliIbvvIzkuKrkurrmnKzlnLrmr5TotZvpppbpgInlrqLog5zvvIzmrKHpgInlubPlsYDjgILFAjxkaXYgY2xhc3M9J2FydGljbGVUYWdMbmsnPjxiIHN0eWxlPSdjb2xvcjojOTk5Oyc%2B5qCH562%2B77yaPC9iPjxhIGhyZWY9J2h0dHA6Ly93d3cud2luMDA3LmNvbS90YWcvenl1bG9uZy8nIHRhcmdldD0nX2JsYW5rJyAgc3R5bGU9J2NvbG9yOmJsdWU7Jz7pgrnnjonpvpk8L2E%2BJm5ic3A7Jm5ic3A7PGEgaHJlZj0naHR0cDovL3d3dy53aW4wMDcuY29tL3RhZy9qY3pxLycgdGFyZ2V0PSdfYmxhbmsnICBzdHlsZT0nY29sb3I6Ymx1ZTsnPuernuW9qei2s%2BeQgzwvYT4mbmJzcDsmbmJzcDvlt7TliJfljaHor7ombmJzcDsmbmJzcDvmr5XlsJTlt7TphILnq57mioA8L2Rpdj5kAhwPDxYEHgtSZWNvcmRjb3VudAKSdR4QQ3VycmVudFBhZ2VJbmRleAIDZGQCBA8WAh8BAgIWBGYPZBYCZg8VBCdodHRwOi8vYmExLndpbjAwNy5jb20vMjA5Ny8zMDk2NjAzLmh0bWwYQUPnsbPlhbAgVlMg5YWL572X5omY5YaFQC9GaWxlcy9TcG9ydHNDb2x1bW5zU2V0LzgwMGMwOWUxLWY0OGMtNGRlOC1hOTlkLThjMzM3N2JlMGRmZC5qcGcYQUPnsbPlhbAgVlMg5YWL572X5omY5YaFZAIBD2QWAmYPFQQnaHR0cDovL2JhMS53aW4wMDcuY29tLzIwOTcvMzA5NjYwMS5odG1sEOWNl%2BeJuSBWUyDph4zmmIJAL0ZpbGVzL1Nwb3J0c0NvbHVtbnNTZXQvMmE4MWQwZTQtNzU4OC00NTU5LTk2MTgtNjA4YWRhYWFiN2RkLmpwZxDljZfnibkgVlMg6YeM5piCZAIFDxYCHwECChYUZg9kFgJmDxUCMGh0dHA6Ly9hcnRpY2xlLndpbjAwNy5jb20vVG9waWMvZm9vdGJhbGwvMTQwMjI4Ly7np4vmnqswMjnmnJ%2Fog5zotJ%2FlvanvvJrmi5zku4HkuLvlnLrlhajlj5Yz5YiGZAIBD2QWAmYPFQIwaHR0cDovL2FydGljbGUud2luMDA3LmNvbS9Ub3BpYy9mb290YmFsbC8xMzEyMTAvMjE3N%2Bacn%2BiDnOi0n%2BW9qTE05Zy65o6o6I2Q77ya5Y2X6ZSh5pyb5Yev5peL6ICM5ZueZAICD2QWAmYPFQIwaHR0cDovL2FydGljbGUud2luMDA3LmNvbS9Ub3BpYy9mb290YmFsbC8xMzExMTMvM%2BiQp%2BWTsueAmjE2MOacn%2BiDnOi0n%2BW9qe%2B8muiRoeiQhOeJmeefpeiAu%2BiAjOWQjuWLh2QCAw9kFgJmDxUCMGh0dHA6Ly9hcnRpY2xlLndpbjAwNy5jb20vVG9waWMvZm9vdGJhbGwvMTMxMTA2Ly3np4vmnqsxNTbmnJ%2FotrPlvanmjqjojZDvvJrng63liLrpq5jmrYznjJvov5tkAgQPZBYCZg8VAjBodHRwOi8vYXJ0aWNsZS53aW4wMDcuY29tL1RvcGljL2Zvb3RiYWxsLzEzMTEwNC8t56eL5p6rMTU15pyf6Laz5b2p5o6o6I2Q77ya6JOd5Yab5YWo5Y%2BW5LiJ5YiGZAIFD2QWAmYPFQIwaHR0cDovL2FydGljbGUud2luMDA3LmNvbS9Ub3BpYy9mb290YmFsbC8xMzExMDEvMOWui%2BWYieaZqDE1M%2Bacn%2Bi2s%2BW9qeaOqOiNkO%2B8mue6oumtlOWuouWcuuWPluWIhmQCBg9kFgJmDxUCMGh0dHA6Ly9hcnRpY2xlLndpbjAwNy5jb20vVG9waWMvZm9vdGJhbGwvMTMxMDIxLyfokKflk7LngJoxNDbmnJ%2Fku7vkuZ3vvJrmnqrmiYvmmL7npZ7lqIFkAgcPZBYCZg8VAjBodHRwOi8vYXJ0aWNsZS53aW4wMDcuY29tL1RvcGljL2Zvb3RiYWxsLzEzMTAxOC8t6JCn5ZOy54CaMTQ15pyf6IOc6LSf5b2p77ya5bCk5paH56We5YuH6Zq%2B5oyhZAIID2QWAmYPFQIwaHR0cDovL2FydGljbGUud2luMDA3LmNvbS9Ub3BpYy9mb290YmFsbC8xMzEwMDgvMTIwMTMxNDHmnJ%2Fog5zotJ%2FlvanmjqjojZDvvJrniLHlsJTlhbDlh7rnur%2Fml6DmnJtkAgkPZBYCZg8VAjBodHRwOi8vYXJ0aWNsZS53aW4wMDcuY29tL1RvcGljL2Zvb3RiYWxsLzEzMDkyNS82MTM05pyf6Laz5b2p5Lu75Lmd5o6o6I2Q77ya5ouc5LuB5be06JCo6LWi55CD5peg6Zq%2B5bqmZAIGDxYCHwECAhYEZg9kFgJmDxUEJWh0dHA6Ly93d3cuMzEwdHYuY29tL3ZpZGVvLzIzMTU5Lmh0bWwz44CQ6Laz5Y2P5p2v56ys5LiJ6L2u44CR6Z2S5bKb5Lit6IO9My0y6ZW%2F5pil5Lqa5rOwQC9GaWxlcy9TcG9ydHNDb2x1bW5zU2V0LzY4ODRjZDExLTI2NGYtNGU0MS1hZDc5LThhZDljZWI2N2MzOC5qcGcb44CQ6Laz5Y2P5p2v56ys5LiJ6L2u44CR6Z2SZAIBD2QWAmYPFQQlaHR0cDovL3d3dy4zMTB0di5jb20vdmlkZW8vMjMxNjAuaHRtbDPjgJDotrPljY%2Fmna%2FnrKzkuInova7jgJHmrabmsYnlro%2FlhbQ0LTPkuIrmtbfkuJzkuppAL0ZpbGVzL1Nwb3J0c0NvbHVtbnNTZXQvZjQ0ODg4MjUtYTRlYi00YmFlLWJiNjMtNDliOTc0NjRlNWVjLmpwZxvjgJDotrPljY%2Fmna%2FnrKzkuInova7jgJHmraZkAgcPFgIfAQIKFhRmD2QWAmYPFQIFMzA3OTM65oSP5aSn5Yip5p2v56ysNOi9ruebtOaSrSBBQ%2Bexs%2BWFsFZT5YWL572X5omY5YaFIOinhumikeWJjWQCAQ9kFgJmDxUCBTMwNzkyI%2BOAkOWOn%2BWIm%2BOAkeiLsei2heesrDE06L2u5LqU5L2z55CDZAICD2QWAmYPFQIFMzA3OTEm44CQ5Y6f5Yib44CR5rOV55Sy56ysMTXova7kupTkvbPmiZHnkINkAgMPZBYCZg8VAgUzMDc5MCPjgJDljp%2FliJvjgJHms5XnlLLnrKwxNei9ruS6lOS9s%2BeQg2QCBA9kFgJmDxUCBTMwNzg3NeOAkOaEj%2BeUsuesrDE06L2u5oiY5oql44CR6YKj5LiN5YuS5pavMi0x5Zu96ZmF57Gz5YWwZAIFD2QWAmYPFQIFMzA3ODY86Iux6LaF56ysMTTova7mnIDkvbPpmLXlrrkg5b635biD5Yqz5YaF5pC65b2T57qi5p2A5pif6aKG6KGUZAIGD2QWAmYPFQIFMzA3ODU26Iux6LaF56ysMTTova7mioDlt6fnp4Ag5aiB5buJ6ISa5bqV55Sf6Iqx5r2H5rSS6Ieq5aaCZAIHD2QWAmYPFQIFMzA3ODQ26Iux6LaF56ysMTTova7mnIDkvbPnkIPlkZgg5rC05pm25a6r5bid5pif5qKF5byA5LqM5bqmZAIID2QWAmYPFQIFMzA3NjMy44CQ5oSP55Sy56ysMTTova7miJjmiqXjgJHlt7Tli5LojqswLTPlsKTmloflm77mlq9kAgkPZBYCZg8VAgUzMDc2MjbjgJDlvrfnlLLnrKwxNOi9ruaImOaKpeOAkeWkmueJueiSmeW%2BtzQtMSDmlq%2Flm77liqDniblkAggPFgIeBFRleHQFsRY8bGFiZWwgc3R5bGU9Im1hcmdpbi1sZWZ0OjVweDttYXJnaW4tcmlnaHQ6NXB4OyI%2BPGEgaHJlZj0naHR0cDovL3d3dy53aW4wMDcuY29tL3RhZy9sYW9waS8nIHRhcmdldD0nX2JsYW5rJyBzdHlsZT0nY29sb3I6Ymx1ZTsnID7ogIHnmq48L2E%2BPC9sYWJlbD48bGFiZWwgc3R5bGU9Im1hcmdpbi1sZWZ0OjVweDttYXJnaW4tcmlnaHQ6NXB4OyI%2BPGEgaHJlZj0naHR0cDovL3d3dy53aW4wMDcuY29tL3RhZy94aW55b3VseC8nIHRhcmdldD0nX2JsYW5rJyBzdHlsZT0nY29sb3I6Ymx1ZTsnID7lv4PmnInngbXopb88L2E%2BPC9sYWJlbD48bGFiZWwgc3R5bGU9Im1hcmdpbi1sZWZ0OjVweDttYXJnaW4tcmlnaHQ6NXB4OyI%2BPGEgaHJlZj0naHR0cDovL3d3dy53aW4wMDcuY29tL3RhZy9keXhjLycgdGFyZ2V0PSdfYmxhbmsnIHN0eWxlPSdjb2xvcjpibHVlOycgPuWkp%2BiLsee7hui2hTwvYT48L2xhYmVsPjxsYWJlbCBzdHlsZT0ibWFyZ2luLWxlZnQ6NXB4O21hcmdpbi1yaWdodDo1cHg7Ij48YSBocmVmPSdodHRwOi8vd3d3LndpbjAwNy5jb20vdGFnL3hpYW5sdW8vJyB0YXJnZXQ9J19ibGFuaycgc3R5bGU9J2NvbG9yOmJsdWU7JyA%2B5LuZ572XPC9hPjwvbGFiZWw%2BPGxhYmVsIHN0eWxlPSJtYXJnaW4tbGVmdDo1cHg7bWFyZ2luLXJpZ2h0OjVweDsiPjxhIGhyZWY9J2h0dHA6Ly93d3cud2luMDA3LmNvbS90YWcveWlqaWEvJyB0YXJnZXQ9J19ibGFuaycgc3R5bGU9J2NvbG9yOmJsdWU7JyA%2B5oSP55SyPC9hPjwvbGFiZWw%2BPGxhYmVsIHN0eWxlPSJtYXJnaW4tbGVmdDo1cHg7bWFyZ2luLXJpZ2h0OjVweDsiPjxhIGhyZWY9J2h0dHA6Ly93d3cud2luMDA3LmNvbS90YWcvYWlmdWR1bi8nIHRhcmdldD0nX2JsYW5rJyBzdHlsZT0nY29sb3I6Ymx1ZTsnID7ln4PlvJfpob88L2E%2BPC9sYWJlbD48bGFiZWwgc3R5bGU9Im1hcmdpbi1sZWZ0OjVweDttYXJnaW4tcmlnaHQ6NXB4OyI%2BPGEgaHJlZj0naHR0cDovL3d3dy53aW4wMDcuY29tL3RhZy9tYW5jaGVuZy8nIHRhcmdldD0nX2JsYW5rJyBzdHlsZT0nY29sb3I6Ymx1ZTsnID7mm7zln448L2E%2BPC9sYWJlbD48bGFiZWwgc3R5bGU9Im1hcmdpbi1sZWZ0OjVweDttYXJnaW4tcmlnaHQ6NXB4OyI%2BPGEgaHJlZj0naHR0cDovL3d3dy53aW4wMDcuY29tL3RhZy95aW5nY2hhby8nIHRhcmdldD0nX2JsYW5rJyBzdHlsZT0nY29sb3I6Ymx1ZTsnID7oi7HotoU8L2E%2BPC9sYWJlbD48bGFiZWwgc3R5bGU9Im1hcmdpbi1sZWZ0OjVweDttYXJnaW4tcmlnaHQ6NXB4OyI%2BPGEgaHJlZj0naHR0cDovL3d3dy53aW4wMDcuY29tL3RhZy9scmppYS8nIHRhcmdldD0nX2JsYW5rJyBzdHlsZT0nY29sb3I6Ymx1ZTsnID7mnpflhJLlmIk8L2E%2BPC9sYWJlbD48bGFiZWwgc3R5bGU9Im1hcmdpbi1sZWZ0OjVweDttYXJnaW4tcmlnaHQ6NXB4OyI%2BPGEgaHJlZj0naHR0cDovL3d3dy53aW4wMDcuY29tL3RhZy9neWluZy8nIHRhcmdldD0nX2JsYW5rJyBzdHlsZT0nY29sb3I6Ymx1ZTsnID7lraTlvbE8L2E%2BPC9sYWJlbD48bGFiZWwgc3R5bGU9Im1hcmdpbi1sZWZ0OjVweDttYXJnaW4tcmlnaHQ6NXB4OyI%2BPGEgaHJlZj0naHR0cDovL3d3dy53aW4wMDcuY29tL3RhZy9saW53ZWljaGVuLycgdGFyZ2V0PSdfYmxhbmsnIHN0eWxlPSdjb2xvcjpibHVlOycgPuael%2BWogeiHozwvYT48L2xhYmVsPjxsYWJlbCBzdHlsZT0ibWFyZ2luLWxlZnQ6NXB4O21hcmdpbi1yaWdodDo1cHg7Ij48YSBocmVmPSdodHRwOi8vd3d3LndpbjAwNy5jb20vdGFnL2xhbnh1LycgdGFyZ2V0PSdfYmxhbmsnIHN0eWxlPSdjb2xvcjpibHVlOycgPuiTneagqTwvYT48L2xhYmVsPjxsYWJlbCBzdHlsZT0ibWFyZ2luLWxlZnQ6NXB4O21hcmdpbi1yaWdodDo1cHg7Ij48YSBocmVmPSdodHRwOi8vd3d3LndpbjAwNy5jb20vdGFnL3N1eXVuZmFuLycgdGFyZ2V0PSdfYmxhbmsnIHN0eWxlPSdjb2xvcjpibHVlOycgPuiLj%2BS6keWHoTwvYT48L2xhYmVsPjxsYWJlbCBzdHlsZT0ibWFyZ2luLWxlZnQ6NXB4O21hcmdpbi1yaWdodDo1cHg7Ij48YSBocmVmPSdodHRwOi8vd3d3LndpbjAwNy5jb20vdGFnL3h5dXhpbi8nIHRhcmdldD0nX2JsYW5rJyBzdHlsZT0nY29sb3I6Ymx1ZTsnID7lpI%2FkuZDppqg8L2E%2BPC9sYWJlbD48bGFiZWwgc3R5bGU9Im1hcmdpbi1sZWZ0OjVweDttYXJnaW4tcmlnaHQ6NXB4OyI%2BPGEgaHJlZj0naHR0cDovL3d3dy53aW4wMDcuY29tL3RhZy9xaWFuaGFvZGUvJyB0YXJnZXQ9J19ibGFuaycgc3R5bGU9J2NvbG9yOmJsdWU7JyA%2B6ZKx55qT5b63PC9hPjwvbGFiZWw%2BPGxhYmVsIHN0eWxlPSJtYXJnaW4tbGVmdDo1cHg7bWFyZ2luLXJpZ2h0OjVweDsiPjxhIGhyZWY9J2h0dHA6Ly93d3cud2luMDA3LmNvbS90YWcveHpoLycgdGFyZ2V0PSdfYmxhbmsnIHN0eWxlPSdjb2xvcjpibHVlOycgPuiQp%2BWTsueAmjwvYT48L2xhYmVsPjxsYWJlbCBzdHlsZT0ibWFyZ2luLWxlZnQ6NXB4O21hcmdpbi1yaWdodDo1cHg7Ij48YSBocmVmPSdodHRwOi8vd3d3LndpbjAwNy5jb20vdGFnL2ZlaWRlbmFuLycgdGFyZ2V0PSdfYmxhbmsnIHN0eWxlPSdjb2xvcjpibHVlOycgPui0ueW%2Bt%2BWNlzwvYT48L2xhYmVsPjxsYWJlbCBzdHlsZT0ibWFyZ2luLWxlZnQ6NXB4O21hcmdpbi1yaWdodDo1cHg7Ij48YSBocmVmPSdodHRwOi8vd3d3LndpbjAwNy5jb20vdGFnL3NvbmdqaWFjaGVuLycgdGFyZ2V0PSdfYmxhbmsnIHN0eWxlPSdjb2xvcjpibHVlOycgPuWui%2BWYieaZqDwvYT48L2xhYmVsPjxsYWJlbCBzdHlsZT0ibWFyZ2luLWxlZnQ6NXB4O21hcmdpbi1yaWdodDo1cHg7Ij48YSBocmVmPSdodHRwOi8vd3d3LndpbjAwNy5jb20vdGFnL2hhZGVuZy8nIHRhcmdldD0nX2JsYW5rJyBzdHlsZT0nY29sb3I6Ymx1ZTsnID7lk4jnmbs8L2E%2BPC9sYWJlbD5kZD0NtLSDmNyMSUQRa5CWT6z5n6p8";
            
            
            
            System.Collections.Specialized.NameValueCollection VarPost = new System.Collections.Specialized.NameValueCollection();
            VarPost.Add("ScriptManager1", "UpdatePanel1|AspNetPager2");//将textBox1中的数据变为用a标识的参数，并用POST传值方式传给网页 ­
            VarPost.Add("__EVENTTARGET", "AspNetPager2");
            VarPost.Add("__EVENTARGUMENT", "2");
            VarPost.Add("__VIEWSTATE", viewState);
            VarPost.Add("__ASYNCPOST", "true");
            //将参数列表VarPost中的所有数据用POST传值的方式传给http://申请好的域名或用IIs配置好的地址/Default.aspx，
            //并将从网页上返回的数据以字节流存放到byRemoteInfo中)(注：IIS配置的时候经常没配置好会提示错误，嘿) ­           
            byte[] byRemoteInfo = w.UploadValues("http://news.win007.com/NewsList.aspx?classID=1", "POST", VarPost);
            string sRemoteInfo = System.Text.Encoding.UTF8.GetString(byRemoteInfo);
            MessageBox.Show(sRemoteInfo);

            //HttpWebRequest requestCookies = (HttpWebRequest)WebRequest.Create(要登入的某网址);
            //requestCookies.ContentType = "application/x-www-form-urlencoded";
            //requestCookies.Referer = "要登入的某网址/login";
            //requestCookies.Headers.Set("Pragma", "no-cache");
            //requestCookies.Accept = "text/html, application/xhtml+xml, */*";
            //requestCookies.Headers.Set("Accept-Language", "zh-CN");
            //requestCookies.Headers.Set("Accept-Encoding", "gzip, deflate");
            //string temp = "PHPSESSID=22a234c094a7f36ba11e6d6767fc614c; cnzz_a30020080=0; sin30020080=; rtime30020080=0; ltime30020080=1330943336040; cnzz_eid30020080=78128217-1330939152-";
            //requestCookies.Headers.Set("cookie", temp);
            //requestCookies.Method = "POST";

            //Encoding encoding23 = Encoding.GetEncoding("utf-8");
            //byte[] bytesToPost = encoding23.GetBytes("charset=utf-8&jumpurl=%2F&username=帐号&password=密码&rememberme=1&input2=%E7%99%BB+%E5%BD%95");
            //requestCookies.ContentLength = bytesToPost.Length;
            //System.IO.Stream requestStream = requestCookies.GetRequestStream();
            //requestStream.Write(bytesToPost, 0, bytesToPost.Length);
            //requestStream.Close();
            //HttpWebResponse hwr = (HttpWebResponse)requestCookies.GetResponse();
            //WebHeaderCollection head = hwr.Headers;
        }

        private void btnImportDanchang_Click(object sender, EventArgs e)
        {
            DateTime StartDate = dtpStartDate.Value;
            DateTime EndDate = dtpEndDate.Value;
            int count = 0;
            while (StartDate <= EndDate)
            {
                //先删除当前日期的开奖结果，避免重复
                //string riqi = StartDate.Date.ToString("yyyy-MM-dd");
                //new KaijiangDAL().DeleteKaijiang(riqi);

                ImportDanchang(StartDate);
                StartDate = StartDate.AddDays(1);
                count++;
            }
            MessageBox.Show("导入成功,导入了" + count + "条单场结果");
        }

        private void ImportDanchang(DateTime importTime)
        {
            //http://www.aicai.com/static/no_cache/jc/zcnew/data/hist/150101fudongspfdata.js
            //http://www.aicai.com/static/no_cache/jc/zcnew/data/fudongspfdata.js
            //{'term':'2015-01-05','historyTerms':['2015-01-04', '2015-01-03', '2015-01-02', '2015-01-01', '2014-12-31', '2014-12-30'],'canBuyMatchDate':['150105', '150106'],'allMatchList':['150105001', '150105003', '150106001'],'allCanBuyMatchList':['150105001', '150105003', '150106001'],'allStopBuyMatchList':[],'canBuyArrangeInDate':{'150105':['150105001', '150105003'], '150106':['150106001']},'canBuyMatchDateName':{'150105':'星期一(01-05)', '150106':'星期二(01-06)'},'allMatchNameList':['澳超[2场]', '意甲[1场]'],'allMatchNameValue':{'意甲':['150105003'], '澳超':['150105001', '150106001']},'allStopMatchNameValue':{'意甲':[], '澳超':[]},'gudingspfList':['150105001', '150105003', '150106001'],'gudingspfNoList':[],'fudongspfList':['150105001', '150105003', '150106001'],'fudongspfNoList':[]}
            //{'canBuyMatchDate':['150104'],'allMatchList':['150104002', '150104003', '150104024', '150104033'],'allCanBuyMatchList':[],'allStopBuyMatchList':['150104002', '150104003', '150104024', '150104033'],'canBuyArrangeInDate':{'150104':[]},'canBuyMatchDateName':{'150104':'星期日(01-04)'},'allMatchNameList':['澳超[1场]', '西甲[3场]'],'allMatchNameValue':{'澳超':['150104002'], '西甲':['150104003', '150104024', '150104033']},'allStopMatchNameValue':{'澳超':['150104002'], '西甲':['150104003', '150104024', '150104033']},'gudingspfList':['150104002', '150104003', '150104024', '150104033'],'gudingspfNoList':[],'fudongspfList':['150104002', '150104003', '150104024', '150104033'],'fudongspfNoList':[]}
            try
            {
                string tempurl = @"http://www.aicai.com/static/no_cache/jc/zcnew/data/hist/{0}fudongspfdata.js";
                string totalUrl = string.Format(tempurl, importTime.ToString("yyMMdd"));
                string totalHtmlData = GetHtmlFromUrl(totalUrl);

                DanchangHistory danchang = JsonConvert.DeserializeObject<DanchangHistory>(totalHtmlData);
                List<SoccerSingle> singles = new List<SoccerSingle>();
                foreach (string match in danchang.allMatchList)
                {

                    SoccerSingle single = new SoccerSingle();
                    String riqiprefix = match.Substring(0, match.Length - 3);
                    String bianhao = match.Substring(match.Length - 3);

                    string riqi = "20" + riqiprefix;
                    int year = Convert.ToInt32(riqi.Substring(0, 4));
                    int month = Convert.ToInt32(riqi.Substring(4, 2));
                    int day = Convert.ToInt32(riqi.Substring(6, 2));

                    single.bianhao = bianhao;
                    single.riqi = new DateTime(year, month, day).ToString("yyyy-MM-dd");
                    single.creator = "system";
                    single.createtime = DateTime.Now;
                    singles.Add(single);
                }
                new SoccerSingleDAL().InsertSoccerSingleList(singles);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnUpdateDanchangPaiming_Click(object sender, EventArgs e)
        {
            //1.先获得需要更新的数据

            DataTable dtHandle = new SoccerSingleDAL().GetNotHandledSingle();
            
            //2.循环更新数据
           
            foreach (DataRow row in dtHandle.Rows)
            {
                int spfpaiming = 1;
                int rqspfpaiming = 1;
                string rowriqi = Convert.ToString(row["riqi"]);
                string bianhao = Convert.ToString(row["bianhao"]);
                double shengsp = Convert.ToDouble(row["shengsp"]);
                double pingsp = Convert.ToDouble(row["pingsp"]);
                double fusp = Convert.ToDouble(row["fusp"]);

                double rqshengsp = Convert.ToDouble(row["rqshengsp"]);
                double rqpingsp = Convert.ToDouble(row["rqpingsp"]);
                double rqfusp = Convert.ToDouble(row["rqfusp"]);

                
                int spfresult = -1;
                if (!int.TryParse(row["spfresult"].ToString(), out spfresult))
                {
                    spfresult = -1;
                }
                int rqspfresult = -1;
                if (!int.TryParse(row["rqspfresult"].ToString(), out rqspfresult))
                {
                    rqspfresult = -1;
                }
                switch (spfresult)
                {
                    case 3:
                        if (shengsp >= pingsp)
                        {
                            spfpaiming++;
                        }
                        if (shengsp >= fusp)
                        {
                            spfpaiming++;
                        }
                        break;
                    case 1:
                        if (pingsp > shengsp)
                        {
                            spfpaiming++;
                        }
                        if (pingsp >= fusp)
                        {
                            spfpaiming++;
                        }
                        break;
                    case 0:
                        if (fusp > shengsp)
                        {
                            spfpaiming++;
                        }
                        if (fusp > pingsp)
                        {
                            spfpaiming++;
                        }
                        break;
                    case -1:
                        spfpaiming = 0;
                        break;

                }

                switch (rqspfresult)
                {
                    case 3:
                        if (rqshengsp >= rqpingsp)
                        {
                            rqspfpaiming++;
                        }
                        if (rqshengsp >= fusp)
                        {
                            rqspfpaiming++;
                        }
                        break;
                    case 1:
                        if (rqpingsp > rqshengsp)
                        {
                            rqspfpaiming++;
                        }
                        if (rqpingsp >= rqfusp)
                        {
                            rqspfpaiming++;
                        }
                        break;
                    case 0:
                        if (rqfusp > rqshengsp)
                        {
                            rqspfpaiming++;
                        }
                        if (rqfusp > rqpingsp)
                        {
                            rqspfpaiming++;
                        }
                        break;
                    case -1:
                        rqspfpaiming = 0;
                        break;
                }

                new SoccerSingleDAL().UpdateSoccer_Single(spfpaiming,rqspfpaiming,rowriqi,bianhao);
                Console.WriteLine("更新单场数据成功！");
            }





        }

        private void btnSingleAnalysis_Click(object sender, EventArgs e)
        {
            //单场概率分析

            Dictionary<int, int> maxTimes = new Dictionary<int, int>();
            maxTimes.Add(0, 0);
            maxTimes.Add(1,0);
            maxTimes.Add(2,0);
            maxTimes.Add(3,0);


            DataTable dtData = new SoccerSingleDAL().GetAllSingle();
            int continueTimes = 1;
            int lastSpfPaiming = 0;
            int currentSpfPaiming = 0;
            foreach (DataRow row in dtData.Rows)
            {             
                currentSpfPaiming = Convert.ToInt32(row["spfpaiming"]);
                if (currentSpfPaiming != lastSpfPaiming)
                {
                    int currentMaxTimes = maxTimes[lastSpfPaiming];
                    if (currentMaxTimes < continueTimes)
                    {
                        maxTimes[lastSpfPaiming] = continueTimes;
                    }

                    lastSpfPaiming = currentSpfPaiming;
                    continueTimes = 1;
                }
                else
                {
                    continueTimes++;
                }                
            }

            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<int, int> kv in maxTimes)
            {
                sb.Append(kv.Key).Append(":").Append(kv.Value).AppendLine();
            }
            MessageBox.Show(sb.ToString());
        }

        private void btnGailvFenxiRq_Click(object sender, EventArgs e)
        {
            //单场概率分析

            Dictionary<int, int> maxTimes = new Dictionary<int, int>();
            maxTimes.Add(0, 0);
            maxTimes.Add(1, 0);
            maxTimes.Add(2, 0);
            maxTimes.Add(3, 0);


            DataTable dtData = new SoccerSingleDAL().GetAllSingle();
            int continueTimes = 1;
            int lastSpfPaiming = 0;
            int currentSpfPaiming = 0;
            foreach (DataRow row in dtData.Rows)
            {
                currentSpfPaiming = Convert.ToInt32(row["rqspfpaiming"]);
                if (currentSpfPaiming != lastSpfPaiming)
                {
                    int currentMaxTimes = maxTimes[lastSpfPaiming];
                    if (currentMaxTimes < continueTimes)
                    {
                        maxTimes[lastSpfPaiming] = continueTimes;
                    }

                    lastSpfPaiming = currentSpfPaiming;
                    continueTimes = 1;
                }
                else
                {
                    continueTimes++;
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<int, int> kv in maxTimes)
            {
                sb.Append(kv.Key).Append(":").Append(kv.Value).AppendLine();
            }
            MessageBox.Show(sb.ToString());
        }

        private void 模拟投注ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new GoldenPigs.TouzhuForm().ShowDialog();
        }

        private void 中奖查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ZhongjiangchaxunForm().ShowDialog();
        }

        private void btnImportPeilv_Click(object sender, EventArgs e)
        {
            DateTime StartDate = dtpStartDate.Value;
            DateTime EndDate = dtpEndDate.Value;
            int count = 0;
            while (StartDate <= EndDate)
            {
                //先删除当前日期的开奖结果，避免重复
                string riqi = StartDate.Date.ToString("yyyy-MM-dd");

                new PeilvDAL().DeletePeilv(riqi);
                //new KaijiangDAL().DeleteKaijiang(riqi);
                ImportPeilv(StartDate);
                //ImportKaijiang(StartDate);
                StartDate = StartDate.AddDays(1);
                count++;
            }
            MessageBox.Show("导入成功,导入了" + count + "条赔率结果");
        }
        private void ImportPeilv(DateTime importdate)
        {
            try
            {
                DateTime importDate = importdate;
                string url = GetKaijiangUrl(importDate);
                string jsonData = GetHtmlFromUrl(url);

                Dictionary<string, object> dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);
                String raceList = dict["raceList"].ToString();
                Dictionary<string, object> dict2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(raceList);
                String weekDay = dict["weekDay"].ToString();
                string weekDate = dict["weekDate"].ToString();

                foreach (string key in dict2.Keys)
                {
                    string value = dict2[key].ToString();
                    Race race = JsonConvert.DeserializeObject<Race>(value);
                    Peilv peilv = new Peilv();
                    peilv.Bianhao = key;
                    peilv.Liansai = race.matchName;
                    peilv.Zhudui = race.homeTeam;
                    peilv.Kedui = race.guestTeam;
                    peilv.Riqi = weekDate;
                    peilv.Operator = "开奖导入";
                    peilv.OperateTime = DateTime.Now;
                    peilv.EndTime = Convert.ToDateTime(race.endTime);

                    string[] xsp = race.xspfSp.Split('-');
                    string[] sp = race.spfSp.Split('-');
                    peilv.Rangqiu = 0;
                    peilv.ShengSp = Convert.ToDouble(xsp[0]);
                    peilv.PingSp = Convert.ToDouble(xsp[1]);
                    peilv.FuSp = Convert.ToDouble(xsp[2]);

                    PeilvDAL dal = new PeilvDAL();
                    dal.InsertPeilv(peilv);

                    peilv.Rangqiu = Convert.ToInt32(race.concede);
                    peilv.ShengSp = Convert.ToDouble(sp[0]);
                    peilv.PingSp = Convert.ToDouble(sp[1]);
                    peilv.FuSp = Convert.ToDouble(sp[2]);
                    dal.InsertPeilv(peilv);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void 账户总览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AccountStaticsForm().ShowDialog();
        }

        private void btnImportPaiming_Click(object sender, EventArgs e)
        {

            DateTime StartDate = dtpStartDate.Value.Date;
            DateTime EndDate = dtpEndDate.Value.Date;
            int count = 0;
            while (StartDate <= EndDate)
            {
                //先删除当前日期的开奖结果，避免重复
                string riqi = StartDate.Date.ToString("yyyy-MM-dd");
                //new KaijiangDAL().DeleteKaijiang(riqi);

                ImportPaiming(StartDate);
                StartDate = StartDate.AddDays(1);
                count++;
            }
            MessageBox.Show("导入成功,导入了" + count + "条排名记录");
           
            
        }

        private void ImportPaiming(DateTime importdate)
        {
            try
            {
                DateTime importDate = importdate;
                string url = GetPaimingUrl(importDate);
                string jsonData = GetHtmlFromUrl(url);
               
                Dictionary<string, object> dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);
                int index = 1;
                foreach (string key in dict.Keys)
                {
                    string value = dict[key].ToString();
                    Dictionary<string, object> dict2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(value);
                    string hrank = dict2["hrank"].ToString();
                    string grank = dict2["grank"].ToString();
                    string riqi = importdate.ToString("yyyy-MM-dd");                    
                    string bianhao = index.ToString().PadLeft(3,'0');

                    
                    string zhuduiliansai = "";
                    string keduiliansai = "";

                    //只有大于2才有可能有联赛内容
                    if (hrank.Length >= 2)
                    {
                        char[] chars = hrank.ToCharArray();
                        StringBuilder sb = new StringBuilder();
                        StringBuilder sb2 = new StringBuilder();
                        foreach (char ch in chars)
                        {
                           
                            if (ch >= '0' && ch <= '9')
                            {
                                sb.Append(ch);
                            }
                            else
                            {
                                sb2.Append(ch);
                            }
                        }
                        hrank = sb.ToString();
                        zhuduiliansai = sb2.ToString();
                    }
                    if (grank.Length >= 2)
                    {
                        char[] chars = grank.ToCharArray();
                        StringBuilder sb = new StringBuilder();
                        StringBuilder sb2 = new StringBuilder();
                        foreach (char ch in chars)
                        {

                            if (ch >= '0' && ch <= '9')
                            {
                                sb.Append(ch);
                            }
                            else
                            {
                                sb2.Append(ch);
                            }
                        }
                        grank = sb.ToString();
                        keduiliansai = sb2.ToString();
                    }
                    new KaijiangDAL().UpdatePaiming(riqi,bianhao,hrank,grank, zhuduiliansai,keduiliansai);
                    index++;

                }

              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + importdate.ToString("yyyy-MM-dd"));
            }
        }
        private string GetPaimingUrl(DateTime date)
        {
            string importDate = date.ToString("yyMMdd");
            string urlTemp =
                @"http://www.aicai.com/static/no_cache/jc/zcnew/data/hist/{0}zcRefer.js";
            return string.Format(urlTemp, importDate);
        }

        private void btnDoubleAnalysis_Click(object sender, EventArgs e)
        {

            //单场概率分析，分析连续多少天没有出现某个排名，以天为单位，同时分析两个

            Dictionary<int, int> maxTimes = new Dictionary<int, int>();
            maxTimes.Add(0, 0);
            maxTimes.Add(1, 0);
            maxTimes.Add(2, 0);
            maxTimes.Add(3, 0);


            DataTable dtData = new SoccerSingleDAL().GetAllSingle();
            int continueTimes = 0;
            //这里只搜索3的未出现最大天数
            int lastSpfPaiming = 1;
            int currentSpfPaiming = 0;
            int currentRqSpfPaiming = 0;
            string maxRiqi = "";
            string currentRiqi = "";
            foreach (DataRow row in dtData.Rows)
            {
                currentSpfPaiming = Convert.ToInt32(row["spfpaiming"]);
                currentRqSpfPaiming = Convert.ToInt32(row["rqspfpaiming"]);
                currentRiqi = row["riqi"].ToString();

                //如果这一天的排名都不是3
                if (currentSpfPaiming != lastSpfPaiming && currentRqSpfPaiming != lastSpfPaiming)
                {
                    continueTimes++;

                    int currentMaxTimes = maxTimes[lastSpfPaiming];
                    if (currentMaxTimes < continueTimes)
                    {
                        maxTimes[lastSpfPaiming] = continueTimes;
                        maxRiqi = currentRiqi;
                    }

                    //lastSpfPaiming = currentSpfPaiming;
                    //continueTimes = 1;
                }
                else
                {
                    continueTimes = 0;
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<int, int> kv in maxTimes)
            {
                sb.Append(kv.Key).Append(":").Append(kv.Value).AppendLine();
            }
            sb.AppendLine(maxRiqi);
            MessageBox.Show(sb.ToString());
        }

    }
}
