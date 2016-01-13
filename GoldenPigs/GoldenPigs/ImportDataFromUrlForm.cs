using GoldenPigs.DAL;
using GoldenPigs.Entity;
using GoldenPigs.Entity.JsonObject;
using GoldenPigs.Helper;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoldenPigs
{
    public partial class btnImportDataFromUrlForm : Form
    {
        //每秒检次一次线程池的状态  
        private RegisteredWaitHandle rhw = null;


        public btnImportDataFromUrlForm()
        {
            InitializeComponent();
        }

        private void btnImportTaocan_Click(object sender, EventArgs e)
        {
            //http://www.aicai.com/static/no_cache/jc/cpj/page/hist/140715cpj_votepak_hist.shtml

            DateTime importDate = Convert.ToDateTime(txtImportDate.Text);
            string uri = GetUrlFromDate(importDate);
            string htmlData = GetHtmlFromUrl(uri);
            // MessageBox.Show(htmlData);

            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(htmlData);
            HtmlNode rootNode = document.DocumentNode;
            HtmlNodeCollection typeList = rootNode.SelectNodes(@"//div[@class='pub_mod_i mt10']");
            foreach (HtmlNode typeNode in typeList)
            {
                HtmlNode strong =
                    typeNode.SelectSingleNode("./div[@class='pub_mod_hd']/strong[@class='pub_mod_title yahei']");
                String type = strong.InnerText;
                HtmlNodeCollection voteCodeList =
                    typeNode.SelectNodes(@"./div[@class='pub_mod_bd pub_hc_wzj  clearfix']");
                HtmlNodeCollection voteCodeList2 =
                    typeNode.SelectNodes(@"./div[@class='pub_mod_bd pub_hc_wzj mt20 clearfix']");
                if (voteCodeList2 != null)
                {
                    foreach (HtmlNode vCode in voteCodeList2)
                    {
                        voteCodeList.Add(vCode);
                    }
                }
                foreach (HtmlNode node in voteCodeList)
                {
                    //概率
                    HtmlNode gailv = node.SelectSingleNode(@"./div/div[@class='pstCell']");
                    string gailvStr = gailv.InnerText;
                    //回报率
                    HtmlNode game_info = node.SelectSingleNode(@"./div/div[@class='game_info']/strong[2]");
                    double huibaolv = Convert.ToDouble(game_info.InnerText);
                    //获取主队1，客队1，主队2，客队2
                    HtmlNodeCollection duimings = node.SelectNodes("./div/div[@class='game_dz']/span");
                    string zhudui1 = duimings[0].InnerText;
                    string kedui1 = duimings[1].InnerText;
                    string zhudui2 = duimings[2].InnerText;
                    string kedui2 = duimings[3].InnerText;
                    HtmlNode gameMoney1 = node.SelectSingleNode("./div/div[@class='game_money']");
                    int touru = 0;
                    double jiangjin = 0.0;
                    touru = Convert.ToInt32(gameMoney1.ChildNodes[1].InnerText);
                    if (gameMoney1.ChildNodes[3].NodeType == HtmlNodeType.Comment)
                    {
                        string comment = gameMoney1.ChildNodes[3].InnerText;
                        int startIndex = comment.IndexOf("\">");
                        int endIndex = comment.IndexOf("</span>");
                        string strJiangjin = comment.Substring(startIndex + 2, endIndex - startIndex - 2);
                        jiangjin = Convert.ToDouble(strJiangjin);
                    }
                    else
                    {
                        jiangjin = Convert.ToDouble(gameMoney1.ChildNodes[3].InnerText);
                    }
                    //foreach(HtmlNode child in gameMoney1.ChildNodes)
                    //{

                    //    MessageBox.Show(child.InnerHtml);
                    //}
                    ////获取投入和奖金 game_money
                    //HtmlNodeCollection gameMoney = node.SelectNodes("./div/div[@class='game_money']/span");
                    //int touru = Convert.ToInt32(gameMoney[0].InnerText);
                    //double jiangjin = Convert.ToDouble(gameMoney[1].InnerText);
                    //是否中奖
                    //<div class="bs_select yahei fs22">中奖金额</div>
                    //<div class="bs_select yahei fs22 c999 pt10">已截止</div>
                    HtmlNode jiezhi = node.SelectSingleNode("./div/div[@class='bs_select yahei fs22 c999 pt10']");
                    HtmlNode zhongjiang = node.SelectSingleNode("./div/div[@class='bs_select yahei fs22']");
                    int lucky = 0;
                    if (jiezhi != null)
                    {
                        lucky = 0;
                    }
                    if (zhongjiang != null)
                    {
                        lucky = 1;
                    }
                    Taocan taocan = new Taocan();
                    taocan.Gailv = gailvStr;
                    taocan.Huibaolv = huibaolv;
                    taocan.Qishu = GetQishu();
                    taocan.Riqi = importDate.Date; //GetRiqi();
                    taocan.Touru = touru;
                    taocan.Jiangjin = jiangjin;
                    taocan.Type = type;
                    taocan.Lucky = lucky;
                    taocan.Zhudui1 = zhudui1;
                    taocan.Zhudui2 = zhudui2;
                    taocan.Kedui1 = kedui1;
                    taocan.Kedui2 = kedui2;
                    int taocanID = new TaocanDAL().InsertTaocan(taocan);

                    HtmlNode xiangqing = node.SelectSingleNode("./div/div/span/div[@class='tcxq_box']");
                    HtmlNodeCollection details = xiangqing.SelectNodes("./table/tbody/tr");
                    foreach (HtmlNode detailNode in details)
                    {
                        TaocanDetail detail = new TaocanDetail();
                        detail.TaocanID = taocanID;
                        HtmlNodeCollection tds = detailNode.SelectNodes("./td");
                        HtmlNode td = tds[1];
                        HtmlNodeCollection spans = td.SelectNodes("./span");
                        if (spans[0].Attributes["class"].Value == "red")
                        {
                            detail.Zhuduilucky1 = 1;
                        }
                        else
                        {
                            detail.Zhuduilucky1 = 0;
                        }

                        if (spans[1].Attributes["class"].Value == "red")
                        {
                            detail.Zhuduilucky2 = 1;
                        }
                        else
                        {
                            detail.Zhuduilucky2 = 0;
                        }

                        String str1 = td.ChildNodes[0].InnerText;
                        string str2 = td.ChildNodes[1].InnerText;
                        string str3 = td.ChildNodes[2].InnerText.Substring(12);
                        string str4 = td.ChildNodes[3].InnerText;


                        detail.Zhudui1 = str1;
                        detail.Zhuduishengfu1 = str2;
                        detail.Zhudui2 = str3;
                        detail.Zhuduishengfu2 = str4;
                        detail.Beishu = Convert.ToInt32(tds[3].Attributes["unitmult"].Value);
                        detail.Jiangjin = Convert.ToDouble(tds[4].Attributes["unitprize"].Value);
                        detail.Operator = "吴林";
                        detail.OperateTime = DateTime.Now;
                        new TaocanDetailDAL().InsertTaocanDetail(detail);
                    }
                }
            }

            MessageBox.Show("导入成功！");
        }

        private void btnImportKaijiang_Click(object sender, EventArgs e)
        {
            //DateTime importDate = Convert.ToDateTime(txtImportDate.Text);
            //string uri = GetUrlFromDate(importDate);
            //string htmlData = GetHtmlFromUrl(uri);
            //MessageBox.Show(htmlData);

            //http://www.aicai.com/lotnew/jc/getMatchByDate.htm?lotteryType=jczq&cate=gd&dataStr=140814&time=1408156959982
            try
            {
                DateTime importDate = Convert.ToDateTime(txtImportDate.Text);
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

                    new KaijiangDAL().InsertKaijiang(kaijiang);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private string GetUrlFromDate(DateTime date)
        {
            //http://www.aicai.com/static/no_cache/jc/cpj/page/hist/140715cpj_votepak_hist.shtml
            string importDate = date.ToString("yyMMdd");
            string urlTemp = @"http://www.aicai.com/static/no_cache/jc/cpj/page/hist/{0}cpj_votepak_hist.shtml";
            return string.Format(urlTemp, importDate);
        }

        private int GetQishu()
        {
            DateTime date = Convert.ToDateTime(txtImportDate.Text);
            string strNow = date.ToString("yyyyMMdd");
            return Convert.ToInt32(strNow);
        }

        private int GetQishu(DateTime importdate)
        {
            DateTime date = importdate;
            string strNow = date.ToString("yyyyMMdd");
            return Convert.ToInt32(strNow);
        }

        private DateTime GetRiqi()
        {
            return DateTime.Now.Date;
        }

        private void ImportData(DateTime importdate)
        {
            DateTime importDate = importdate;
            string uri = GetUrlFromDate(importDate);
            string htmlData = GetHtmlFromUrl(uri);
            // MessageBox.Show(htmlData);

            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(htmlData);
            HtmlNode rootNode = document.DocumentNode;
            HtmlNodeCollection typeList = rootNode.SelectNodes(@"//div[@class='pub_mod_i mt10']");
            foreach (HtmlNode typeNode in typeList)
            {
                HtmlNode strong =
                    typeNode.SelectSingleNode("./div[@class='pub_mod_hd']/strong[@class='pub_mod_title yahei']");
                String type = strong.InnerText;
                HtmlNodeCollection voteCodeList =
                    typeNode.SelectNodes(@"./div[@class='pub_mod_bd pub_hc_wzj  clearfix']");
                HtmlNodeCollection voteCodeList2 =
                    typeNode.SelectNodes(@"./div[@class='pub_mod_bd pub_hc_wzj mt20 clearfix']");
                if (voteCodeList2 != null)
                {
                    foreach (HtmlNode vCode in voteCodeList2)
                    {
                        voteCodeList.Add(vCode);
                    }
                }

                foreach (HtmlNode node in voteCodeList)
                {
                    //概率
                    HtmlNode gailv = node.SelectSingleNode(@"./div/div[@class='pstCell']");
                    string gailvStr = gailv.InnerText;
                    //回报率
                    HtmlNode game_info = node.SelectSingleNode(@"./div/div[@class='game_info']/strong[2]");
                    double huibaolv = Convert.ToDouble(game_info.InnerText);
                    //获取主队1，客队1，主队2，客队2
                    HtmlNodeCollection duimings = node.SelectNodes("./div/div[@class='game_dz']/span");
                    string zhudui1 = duimings[0].InnerText;
                    string kedui1 = duimings[1].InnerText;
                    string zhudui2 = duimings[2].InnerText;
                    string kedui2 = duimings[3].InnerText;
                    HtmlNode gameMoney1 = node.SelectSingleNode("./div/div[@class='game_money']");
                    int touru = 0;
                    double jiangjin = 0.0;
                    touru = Convert.ToInt32(gameMoney1.ChildNodes[1].InnerText);
                    if (gameMoney1.ChildNodes[3].NodeType == HtmlNodeType.Comment)
                    {
                        string comment = gameMoney1.ChildNodes[3].InnerText;
                        int startIndex = comment.IndexOf("\">");
                        int endIndex = comment.IndexOf("</span>");
                        string strJiangjin = comment.Substring(startIndex + 2, endIndex - startIndex - 2);
                        jiangjin = Convert.ToDouble(strJiangjin);
                    }
                    else
                    {
                        jiangjin = Convert.ToDouble(gameMoney1.ChildNodes[3].InnerText);
                    }
                    //foreach(HtmlNode child in gameMoney1.ChildNodes)
                    //{

                    //    MessageBox.Show(child.InnerHtml);
                    //}
                    ////获取投入和奖金 game_money
                    //HtmlNodeCollection gameMoney = node.SelectNodes("./div/div[@class='game_money']/span");
                    //int touru = Convert.ToInt32(gameMoney[0].InnerText);
                    //double jiangjin = Convert.ToDouble(gameMoney[1].InnerText);
                    //是否中奖
                    //<div class="bs_select yahei fs22">中奖金额</div>
                    //<div class="bs_select yahei fs22 c999 pt10">已截止</div>
                    HtmlNode jiezhi = node.SelectSingleNode("./div/div[@class='bs_select yahei fs22 c999 pt10']");
                    HtmlNode zhongjiang = node.SelectSingleNode("./div/div[@class='bs_select yahei fs22']");
                    int lucky = 0;
                    if (jiezhi != null)
                    {
                        lucky = 0;
                    }
                    if (zhongjiang != null)
                    {
                        lucky = 1;
                    }
                    Taocan taocan = new Taocan();
                    taocan.Gailv = gailvStr;
                    taocan.Huibaolv = huibaolv;
                    taocan.Qishu = GetQishu(importDate);
                    taocan.Riqi = importdate.Date;
                    taocan.Touru = touru;
                    taocan.Jiangjin = jiangjin;
                    taocan.Type = type;
                    taocan.Lucky = lucky;
                    taocan.Zhudui1 = zhudui1;
                    taocan.Zhudui2 = zhudui2;
                    taocan.Kedui1 = kedui1;
                    taocan.Kedui2 = kedui2;
                    int taocanID = new TaocanDAL().InsertTaocan(taocan);

                    HtmlNode xiangqing = node.SelectSingleNode("./div/div/span/div[@class='tcxq_box']");
                    HtmlNodeCollection details = xiangqing.SelectNodes("./table/tbody/tr");
                    foreach (HtmlNode detailNode in details)
                    {
                        TaocanDetail detail = new TaocanDetail();
                        detail.TaocanID = taocanID;
                        HtmlNodeCollection tds = detailNode.SelectNodes("./td");
                        HtmlNode td = tds[1];
                        HtmlNodeCollection spans = td.SelectNodes("./span");
                        if (spans[0].Attributes["class"].Value == "red")
                        {
                            detail.Zhuduilucky1 = 1;
                        }
                        else
                        {
                            detail.Zhuduilucky1 = 0;
                        }

                        if (spans[1].Attributes["class"].Value == "red")
                        {
                            detail.Zhuduilucky2 = 1;
                        }
                        else
                        {
                            detail.Zhuduilucky2 = 0;
                        }

                        String str1 = td.ChildNodes[0].InnerText;
                        string str2 = td.ChildNodes[1].InnerText;
                        string str3 = td.ChildNodes[2].InnerText.Substring(12);
                        string str4 = td.ChildNodes[3].InnerText;


                        detail.Zhudui1 = str1;
                        detail.Zhuduishengfu1 = str2;
                        detail.Zhudui2 = str3;
                        detail.Zhuduishengfu2 = str4;
                        detail.Beishu = Convert.ToInt32(tds[3].Attributes["unitmult"].Value);
                        detail.Jiangjin = Convert.ToDouble(tds[4].Attributes["unitprize"].Value);
                        detail.Operator = "吴林";
                        detail.OperateTime = DateTime.Now;
                        //初始化时为-1
                        detail.TiaozhengFlag = -1;
                        new TaocanDetailDAL().InsertTaocanDetail(detail);
                    }
                }
            }
        }

        private void btnBatchImportTaocan_Click(object sender, EventArgs e)
        {
            DateTime StartDate = dtpStartDate.Value;
            DateTime EndDate = dtpEndDate.Value;
            int count = 0;
            while (StartDate <= EndDate)
            {
                ImportData(StartDate);
                StartDate = StartDate.AddDays(1);
                count++;
            }
            MessageBox.Show("导入成功,导入了" + count + "条套餐");
        }

        private void btnImportBifa_Click(object sender, EventArgs e)
        {
            DateTime StartDate = dtpStartDate.Value;
            DateTime EndDate = dtpEndDate.Value;
            int count = 0;
            while (StartDate <= EndDate)
            {
                ImportBifa(StartDate);
                StartDate = StartDate.AddDays(1);
                count++;
            }
            MessageBox.Show("导入成功,导入了" + count + "条必发结果");
        }

        private void ImportBifa(DateTime importdate)
        {
            try
            {
                DateTime importDate = importdate;
                string url = GetBifaUrl(importDate);
                string jsonData = GetHtmlFromUrl(url);
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(jsonData);
                HtmlNode rootNode = document.DocumentNode;

                HtmlNodeCollection sideDivLis = rootNode.SelectNodes("./div/div");
                foreach (HtmlNode sideDiv in sideDivLis)
                {
                    int number = Convert.ToInt32(sideDiv.SelectSingleNode("./p/i").InnerText);
                    HtmlNodeCollection spans = sideDiv.SelectNodes("./p")[0].SelectNodes("./em/span");
                    string zhudui = spans[0].InnerText.Trim();
                    string kedui = spans[2].InnerText.Trim();

                    string shijianandshuxing = sideDiv.SelectNodes("./p")[1].InnerText.Trim('\t');
                    int shuxingIndex = shijianandshuxing.IndexOf("属性:");
                    string shijian = importDate.Year + "-" + shijianandshuxing.Substring(3, shuxingIndex - 3).Trim('\t');
                    string shuxing = shijianandshuxing.Substring(shuxingIndex + 3).Trim('\t');


                    string includePeilv = sideDiv.SelectNodes("./p")[2].InnerText;
                    int endindex = includePeilv.IndexOf(",成交额");
                    string peilv = includePeilv.Substring(3, endindex - 3);
                    string chengjiaoe = sideDiv.SelectNodes("./p/span[@class='c_red']")[0].InnerText;

                    BifaTop3 bifa = new BifaTop3();
                    bifa.Paiming = number;
                    bifa.Riqi = importDate.Date;
                    bifa.Zhudui = zhudui;
                    bifa.Kedui = kedui;
                    bifa.Shijian = Convert.ToDateTime(shijian);
                    bifa.Shuxing = shuxing;
                    bifa.Peilv = Convert.ToDouble(peilv);
                    bifa.Chengjiaoe = Convert.ToInt32(chengjiaoe);
                    bifa.Lucky = -1;
                    bifa.Operator = "吴林";
                    bifa.OperateTime = DateTime.Now;

                    new BifaTop3DAL().InsertBifaTop3(bifa);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private string GetBifaUrl(DateTime date)
        {
            //http://www.aicai.com/static/no_cache/bf/top3/top3bf_20140820.html
            string importDate = date.ToString("yyyyMMdd");
            string urlTemp = @"http://www.aicai.com/static/no_cache/bf/top3/top3bf_{0}.html";
            return string.Format(urlTemp, importDate);
        }

        private void btnBatchImportKaijiang_Click(object sender, EventArgs e)
        {
            DateTime StartDate = dtpStartDate.Value;
            DateTime EndDate = dtpEndDate.Value;
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
                    kaijiang.Bisaishijian = Convert.ToDateTime(weekDate);

                    new KaijiangDAL().InsertKaijiang(kaijiang);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnImportPeilv_Click(object sender, EventArgs e)
        {
            try
            {
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                //document.LoadHtml(html);
                //string fileName = "html/赔率20140829.html";
                //document.Load(fileName, Encoding.UTF8);

                string url = @"http://www.aicai.com/jczq/";
                string jsonData = GetHtmlFromUrl(url);
                //预处理，某些页面的class='phaoTd' 带有空格
                if (jsonData.IndexOf(@"class=""phaoTd """) != -1)
                {
                    jsonData = jsonData.Replace(@"class=""phaoTd """, @"class=""phaoTd""");
                }
                if (jsonData.IndexOf(@"class=""phaoTd pr""") != -1)
                {
                    jsonData = jsonData.Replace(@"class=""phaoTd pr""", @"class=""phaoTd""");
                }
                document.LoadHtml(jsonData);

                HtmlNode rootNode = document.DocumentNode;
                HtmlNodeCollection tbodies = rootNode.SelectNodes("//tbody[@class='jq_gdhhspf_sort_table']");
                foreach (HtmlNode tbody in tbodies)
                {
                    HtmlNode gameTime = tbody.SelectSingleNode("./tr/td[@class='gameTime']");
                    String xinqi = gameTime.InnerText.Substring(0, 3);
                    String riqi = gameTime.InnerText.Substring(4, 10);

                    //增加前先删掉原来的数据
                    new PeilvDAL().DeletePeilv(riqi);

                    HtmlNodeCollection matches = tbody.SelectNodes("./tr[@class='jq_gdhhspf_match_select_tr']");
                    if (matches == null)
                    {
                        continue;
                    }
                    //增加偶数节点
                    HtmlNodeCollection matchesEven =
                        tbody.SelectNodes("./tr[@class='jq_gdhhspf_match_select_tr evenTr']");
                    //jq_gdhhspf_match_select_tr evenTr

                    foreach (HtmlNode m in matchesEven)
                    {
                        matches.Add(m);
                    }

                    foreach (HtmlNode match in matches)
                    {
                        Peilv peilv = new Peilv();
                        peilv.Riqi = riqi;
                        HtmlNode phaoTd = match.SelectSingleNode("./td[@class='phaoTd']");

                        //peilv.Bianhao = phaoTd.InnerText;
                        //编号的逻辑由于新版本，需要进行修改
                        peilv.Bianhao = phaoTd.SelectSingleNode("./a[@class='jq_gdhhspf_selectmatch']").InnerText;

                        HtmlNode saiTd = match.SelectSingleNode("./td[@class='saiTd']");
                        peilv.Liansai = saiTd.InnerText;
                        HtmlNode zhuTeamTd = match.SelectSingleNode("./td[@class='zhuTeamTd']");
                        peilv.Zhudui = zhuTeamTd.InnerText;
                        HtmlNode keTeamTd = match.SelectSingleNode("./td[@class='keTeamTd']");
                        peilv.Kedui = keTeamTd.InnerText;


                        HtmlNodeCollection betPanels = match.SelectNodes("./td/div[@class='betPanel']");
                        foreach (HtmlNode betPanel in betPanels)
                        {
                            string rangqiu = betPanel.ChildNodes[1].ChildNodes[0].InnerText;
                            peilv.Rangqiu = Convert.ToInt32(rangqiu);
                            HtmlNodeCollection sps = betPanel.SelectNodes("./div[@class='betChan betChoose ']");
                            if (sps == null)
                            {
                                peilv.ShengSp = 0;
                                peilv.PingSp = 0;
                                peilv.FuSp = 0;
                            }
                            else
                            {
                                peilv.ShengSp = Convert.ToDouble(sps[0].InnerText);
                                peilv.PingSp = Convert.ToDouble(sps[1].InnerText);
                                peilv.FuSp = Convert.ToDouble(sps[2].InnerText);
                            }

                            peilv.OperateTime = DateTime.Now;
                            peilv.Operator = "吴林";

                            new PeilvDAL().InsertPeilv(peilv);
                        }
                    }
                }

                MessageBox.Show("导入赔率成功！");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnImportCurrentTaocan_Click(object sender, EventArgs e)
        {
            DateTime importdate = DateTime.Now.Date;

            DateTime importDate = importdate;
            string uri = @"http://www.aicai.com/pages/lotnew/zq/index_vote.shtml";
            string htmlData = GetHtmlFromUrl(uri);
            // MessageBox.Show(htmlData);

            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(htmlData);
            HtmlNode rootNode = document.DocumentNode;
            HtmlNodeCollection typeList = rootNode.SelectNodes(@"//div[@class='pub_mod_i mt10']");
            foreach (HtmlNode typeNode in typeList)
            {
                HtmlNode strong =
                    typeNode.SelectSingleNode("./div[@class='pub_mod_hd']/strong[@class='pub_mod_title yahei']");
                String type = strong.InnerText;
                HtmlNodeCollection voteCodeList =
                    typeNode.SelectNodes(@"./div[@class='pub_mod_bd pub_hc_wzj  clearfix']");
                HtmlNodeCollection voteCodeList2 =
                    typeNode.SelectNodes(@"./div[@class='pub_mod_bd pub_hc_wzj mt20 clearfix']");
                if (voteCodeList2 != null)
                {
                    foreach (HtmlNode vCode in voteCodeList2)
                    {
                        voteCodeList.Add(vCode);
                    }
                }

                foreach (HtmlNode node in voteCodeList)
                {
                    //概率
                    HtmlNode gailv = node.SelectSingleNode(@"./div/div[@class='pstCell']");
                    string gailvStr = gailv.InnerText;
                    //回报率
                    HtmlNode game_info = node.SelectSingleNode(@"./div/div[@class='game_info']/strong[2]");
                    double huibaolv = Convert.ToDouble(game_info.InnerText);
                    //获取主队1，客队1，主队2，客队2
                    HtmlNodeCollection duimings = node.SelectNodes("./div/div[@class='game_dz']/span");
                    string zhudui1 = duimings[0].InnerText;
                    string kedui1 = duimings[1].InnerText;
                    string zhudui2 = duimings[2].InnerText;
                    string kedui2 = duimings[3].InnerText;
                    HtmlNode gameMoney1 = node.SelectSingleNode("./div/div[@class='game_money']");
                    int touru = 0;
                    double jiangjin = 0.0;
                    touru = Convert.ToInt32(gameMoney1.ChildNodes[1].InnerText);
                    if (gameMoney1.ChildNodes[3].NodeType == HtmlNodeType.Comment)
                    {
                        string comment = gameMoney1.ChildNodes[3].InnerText;
                        int startIndex = comment.IndexOf("\">");
                        int endIndex = comment.IndexOf("</span>");
                        string strJiangjin = comment.Substring(startIndex + 2, endIndex - startIndex - 2);
                        jiangjin = Convert.ToDouble(strJiangjin);
                    }
                    else
                    {
                        jiangjin = Convert.ToDouble(gameMoney1.ChildNodes[3].InnerText);
                    }
                    //foreach(HtmlNode child in gameMoney1.ChildNodes)
                    //{

                    //    MessageBox.Show(child.InnerHtml);
                    //}
                    ////获取投入和奖金 game_money
                    //HtmlNodeCollection gameMoney = node.SelectNodes("./div/div[@class='game_money']/span");
                    //int touru = Convert.ToInt32(gameMoney[0].InnerText);
                    //double jiangjin = Convert.ToDouble(gameMoney[1].InnerText);
                    //是否中奖
                    //<div class="bs_select yahei fs22">中奖金额</div>
                    //<div class="bs_select yahei fs22 c999 pt10">已截止</div>
                    HtmlNode jiezhi = node.SelectSingleNode("./div/div[@class='bs_select yahei fs22 c999 pt10']");
                    HtmlNode zhongjiang = node.SelectSingleNode("./div/div[@class='bs_select yahei fs22']");
                    int lucky = 0;
                    if (jiezhi != null)
                    {
                        lucky = 0;
                    }
                    if (zhongjiang != null)
                    {
                        lucky = 1;
                    }
                    Taocan taocan = new Taocan();
                    taocan.Gailv = gailvStr;
                    taocan.Huibaolv = huibaolv;
                    taocan.Qishu = GetQishu(importDate);
                    taocan.Riqi = importdate.Date;
                    taocan.Touru = touru;
                    taocan.Jiangjin = jiangjin;
                    taocan.Type = type;
                    taocan.Lucky = lucky;
                    taocan.Zhudui1 = zhudui1;
                    taocan.Zhudui2 = zhudui2;
                    taocan.Kedui1 = kedui1;
                    taocan.Kedui2 = kedui2;
                    int taocanID = new TaocanDAL().InsertTaocanVote(taocan);

                    HtmlNode xiangqing = node.SelectSingleNode("./div/div/span/div[@class='tcxq_box']");
                    HtmlNodeCollection details = xiangqing.SelectNodes("./table/tbody/tr");
                    foreach (HtmlNode detailNode in details)
                    {
                        TaocanDetail detail = new TaocanDetail();
                        detail.TaocanID = taocanID;
                        HtmlNodeCollection tds = detailNode.SelectNodes("./td");
                        HtmlNode td = tds[1];
                        HtmlNodeCollection spans = td.SelectNodes("./span");
                        if (spans[0].Attributes["class"].Value == "red")
                        {
                            detail.Zhuduilucky1 = 1;
                        }
                        else
                        {
                            detail.Zhuduilucky1 = 0;
                        }

                        if (spans[1].Attributes["class"].Value == "red")
                        {
                            detail.Zhuduilucky2 = 1;
                        }
                        else
                        {
                            detail.Zhuduilucky2 = 0;
                        }

                        String str1 = td.ChildNodes[0].InnerText;
                        string str2 = td.ChildNodes[1].InnerText;
                        string str3 = td.ChildNodes[2].InnerText.Substring(12);
                        string str4 = td.ChildNodes[3].InnerText;


                        detail.Zhudui1 = str1;
                        detail.Zhuduishengfu1 = str2;
                        detail.Zhudui2 = str3;
                        detail.Zhuduishengfu2 = str4;
                        detail.Beishu = Convert.ToInt32(tds[3].Attributes["unitmult"].Value);
                        detail.Jiangjin = Convert.ToDouble(tds[4].Attributes["unitprize"].Value);
                        detail.Operator = "吴林";
                        detail.OperateTime = DateTime.Now;
                        new TaocanDetailDAL().InsertTaocanDetailVote(detail);
                    }
                }
            }
        }

        private void ImportDataFromUrl_Load(object sender, EventArgs e)
        {
        }

        private void btnImportCaike_Click(object sender, EventArgs e)
        {
            string[] urls = new string[]
            {
                @"http://www.310win.com/jingcaizuqiu/info_t1sub2page3.html",
                @"http://www.310win.com/jingcaizuqiu/info_t1sub2page4.html",
                @"http://www.310win.com/beijingdanchang/info_t1sub2page3.html",
                @"http://www.310win.com/beijingdanchang/info_t1sub2page4.html"
            };

            //string uri = @"http://www.310win.com/jingcaizuqiu/info_t1sub2page1.html";
            //string uri = @"http://www.310win.com/beijingdanchang/info_t1sub2page1.html";
            foreach (string uri in urls)
            {
                string htmlData = GetHtmlFromUrl(uri);
                // MessageBox.Show(htmlData);

                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(htmlData);


                //HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();

                //document.Load("html/彩客咨询目录.html", Encoding.UTF8);

                HtmlNode rootNode = document.DocumentNode;
                HtmlNodeCollection htbList = rootNode.SelectNodes(@"//table[@class='htbList']/tr[@class='']");
                HtmlNodeCollection htbList2 = rootNode.SelectNodes(@"//table[@class='htbList']/tr[@class='listBrief']");
                for (int i = 0; i < htbList.Count; i++)
                {
                    HtmlNode tr1 = htbList[i];
                    HtmlNode tr2 = htbList2[i];
                    HtmlNodeCollection a = tr1.SelectNodes(@"./td/a");
                    string caizhongtype = a[0].InnerText.Trim();
                    string title = a[1].InnerText.Trim();
                    string url = a[1].Attributes["href"].Value.ToString().Trim();
                    //彩种类型
                    Console.WriteLine(a[0].InnerText.Trim());
                    //摘要 
                    Console.WriteLine(a[1].InnerText.Trim());
                    //网址
                    Console.WriteLine(a[1].Attributes["href"].Value.ToString().Trim());
                    HtmlNode td2 = tr1.SelectSingleNode(@"./td[2]");
                    DateTime publishdate = Convert.ToDateTime(td2.InnerText.Trim());
                    //发布日期
                    Console.WriteLine(td2.InnerText.Trim());
                    SaveYuceDetail(caizhongtype, url, title, publishdate);
                }
            }

            MessageBox.Show("导入成功！");
        }

        private void SaveYuceDetail(string caizhongtype, string url, string title, DateTime publishdate)
        {
            string urlTemplate = @"http://www.310win.com/{0}";
            url = string.Format(urlTemplate, url);
            string htmlData = GetHtmlFromUrl(url);
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(htmlData);
            HtmlNode rootNode = document.DocumentNode;
            HtmlNodeCollection articleContent = rootNode.SelectNodes(@"//div[@class='articleContent']");
            HtmlNodeCollection p = articleContent[0].SelectNodes(@"./p");
            //Console.WriteLine(p.Count);
            Yucerawdata data = null;
            bool alreadyTuijian = false;
            foreach (HtmlNode pChild in p)
            {
                string pText = pChild.InnerText.Trim();
                if (pText.IndexOf("VS") != -1 || pText.IndexOf("vs") != -1)
                {
                    //如果data没有初始化，说明没有添加信息
                    if (data == null)
                    {
                        data = new Yucerawdata();
                        data.OperateTime = DateTime.Now;
                        data.Operator = "吴林";
                        data.Title = title;
                        data.Url = url;
                        data.Publishdate = publishdate;
                        data.Caizhongtype = caizhongtype;
                        data.P2 = pText;
                        HtmlNode lastP = GetSiblingPNode(pChild);
                        if (lastP != null)
                        {
                            data.P1 = lastP.InnerText.Trim();
                        }
                        else
                        {
                            data.P1 = "";
                        }
                    }
                    else
                    {
                        //添加到数据库，并重设为初始值
                        new YucerawdataDAL().InsertYucerawdata(data);
                        alreadyTuijian = false;
                        data = new Yucerawdata();
                        data.OperateTime = DateTime.Now;
                        data.Operator = "吴林";
                        data.Title = title;
                        data.Url = url;
                        data.Publishdate = publishdate;
                        data.Caizhongtype = caizhongtype;
                        data.P2 = pText;
                        HtmlNode lastP = GetSiblingPNode(pChild);
                        if (lastP != null)
                        {
                            data.P1 = lastP.InnerText.Trim();
                        }
                        else
                        {
                            data.P1 = "";
                        }
                    }

                    //int vsIndex = pText.IndexOf("VS");
                    //string zhudui = pText.Substring(0, vsIndex).Trim();
                    //string kedui = pText.Substring(vsIndex + 2).Trim();
                    //Console.WriteLine(zhudui);
                    //Console.WriteLine(kedui);
                }
                if (pText.IndexOf("开赛时间") != -1)
                {
                    Console.WriteLine(pText);
                    string[] innerStrings = pText.Split('\t');
                    data.P3 = innerStrings[0].Replace("开赛时间", "比赛时间");
                    if (innerStrings.Length > 1)
                    {
                        data.P4 = innerStrings[1].Replace("&nbsp;", "");
                        data.P5 = innerStrings[2].Replace("&nbsp;", "");
                    }
                    if (innerStrings.Length >= 4)
                    {
                        data.P8 = innerStrings[3].Replace("&nbsp;", "");
                    }

                    if (innerStrings.Length >= 5)
                    {
                        data.P9 = innerStrings[4].Replace("&nbsp;", "");
                    }

                    alreadyTuijian = true;
                }
                else
                {
                    if (pText.IndexOf("比赛时间") != -1)
                    {
                        //如果这里为空，说明之前没有取到VS，继续处理没有意义，先忽略掉
                        if (data == null)
                        {
                            data = new Yucerawdata();
                            data.OperateTime = DateTime.Now;
                            data.Operator = "吴林";
                            data.Title = title;
                            data.Url = url;
                            data.Publishdate = publishdate;
                            data.Caizhongtype = caizhongtype;
                            data.P2 = pText;
                            break;
                        }
                        data.P3 = pText;

                        //int bisaiIndex = pText.Trim().IndexOf("比赛时间");
                        //string bisaishijian = pText.Trim().Substring(5);
                        //Console.WriteLine(bisaishijian);
                    }
                    if (pText.IndexOf("欧赔") != -1)
                    {
                        data.P4 = pText;
                    }
                    if (pText.IndexOf("盘口") != -1)
                    {
                        data.P5 = pText;
                    }

                    if (pText.IndexOf("赛事分析") != -1)
                    {
                        data.P6 = pText;
                    }
                    if (pText.IndexOf("盘赔数据分析") != -1)
                    {
                        data.P7 = pText;
                    }
                    if (pText.IndexOf("推荐") != -1 &&
                        (pText.IndexOf("竞彩") != -1 || pText.IndexOf("北京单场") != -1 || pText.IndexOf("北单") != -1))
                    {
                        if (!alreadyTuijian)
                        {
                            data.P8 = pText;
                            alreadyTuijian = true;
                        }
                    }
                }
            }
            if (data != null)
            {
                new YucerawdataDAL().InsertYucerawdata(data);
            }
        }

        private HtmlNode GetSiblingPNode(HtmlNode node)
        {
            HtmlNode previous = node.PreviousSibling;
            while (previous != null && previous.Name != "p")
            {
                previous = previous.PreviousSibling;
            }
            return previous;
        }

        private void btnAnalysisCaike_Click(object sender, EventArgs e)
        {
            //获取最新预测数据
            YucerawdataDAL dal = new YucerawdataDAL();
            DataSet ds = dal.GetYucerawdata();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string url = row["url"].ToString();
                string caizhongtype = row["caizhongtype"].ToString();
                string title = row["title"].ToString();
                string p2 = row["p2"].ToString();
                string p3 = row["p3"].ToString();
                string p8 = row["p8"].ToString();
                //分析数据
                int zhongkuohaoIndex = title.IndexOf("[");
                int zhongkuohaoIndex2 = title.IndexOf("]");
                string yuceren = "";
                if (zhongkuohaoIndex != -1)
                {
                    yuceren = title.Substring(zhongkuohaoIndex + 1, zhongkuohaoIndex2 - zhongkuohaoIndex - 1);
                }

                string zhudui = "";
                string kedui = "";
                if (p2.IndexOf("：") != -1)
                {
                    p2 = PreHandleData2(p2);
                    string[] splitP2 = p2.Split(' ');
                    kedui = splitP2[splitP2.Length - 1];
                    zhudui = splitP2[splitP2.Length - 3];
                    //去除主队和客队中包含的括号
                    kedui = DeleteKuohao(kedui);
                    zhudui = DeleteKuohao(zhudui);
                }
                else
                {
                    int vsIndex = p2.ToUpper().IndexOf("VS");
                    string preVs = p2.Substring(0, vsIndex).Replace("&nbsp;", "");
                    string postVs = p2.Substring(vsIndex + 2);
                    kedui = postVs.Replace("&nbsp;", "").Replace(" ", "");
                    int lastKonggeIndex = preVs.Trim().LastIndexOf(" ");
                    if (lastKonggeIndex != -1)
                    {
                        zhudui = preVs.Substring(lastKonggeIndex).Trim();
                    }
                    else
                    {
                        zhudui = preVs.Trim();
                    }

                    //去除主队和客队中包含的括号
                    kedui = DeleteKuohao(kedui);
                    zhudui = DeleteKuohao(zhudui);
                }

                //获取比赛时间
                string bisaishijian = "";
                p3 = PreHandleData3(p3);
                int bisaiIndex = p3.IndexOf("比赛时间:");
                bisaishijian = p3.Substring(bisaiIndex + 5);

                //获取推荐及让球数
                string spf = "";
                string rangqiushu = "0";
                int hasrangqiu = 0;
                p8 = PreHandleData8(p8);

                int maohaoIndex = p8.IndexOf("：");

                if (maohaoIndex == -1)
                {
                    maohaoIndex = p8.IndexOf("推荐") + 1;
                }
                int qiankuohaoIndex = p8.IndexOf("(");
                int houkuohaoIndex = p8.IndexOf(")");

                //这个分支适用于模式： 竞彩足球让球胜平负推荐：0(维冈竞技让1球)
                if (qiankuohaoIndex != -1 && qiankuohaoIndex > maohaoIndex && houkuohaoIndex == p8.Length - 1)
                {
                    spf = p8.Substring(maohaoIndex + 1, qiankuohaoIndex - maohaoIndex - 1);
                }
                else if (qiankuohaoIndex != -1 && qiankuohaoIndex > maohaoIndex && houkuohaoIndex != p8.Length - 1)
                {
                    spf = p8.Substring(houkuohaoIndex + 1);
                }
                else
                {
                    spf = p8.Substring(maohaoIndex + 1);
                }

                if (p8.IndexOf("无让球") != -1 || p8.IndexOf("没有让球") != -1 || p8.IndexOf("不让球") != -1)
                {
                }
                else
                {
                    if (p8.IndexOf("让球") != -1 || p8.IndexOf(")球") != -1)
                    {
                        //让球（-1）和让（-1）球模式
                        int rangIndex = p8.LastIndexOf("让");
                        int qiuIndex = p8.LastIndexOf("球");
                        if (rangIndex != -1 && qiuIndex > rangIndex + 1)
                        {
                            rangqiushu = p8.Substring(rangIndex + 1, qiuIndex - rangIndex - 1);
                            switch (rangqiushu)
                            {
                                case "一":
                                    rangqiushu = "1";
                                    break;
                                case "二":
                                    rangqiushu = "2";
                                    break;
                                case "三":
                                    rangqiushu = "3";
                                    break;
                                case "四":
                                    rangqiushu = "4";
                                    break;
                                case "五":
                                    rangqiushu = "5";
                                    break;
                                case "六":
                                    rangqiushu = "6";
                                    break;
                                case "七":
                                    rangqiushu = "7";
                                    break;
                                case "八":
                                    rangqiushu = "8";
                                    break;
                                case "九":
                                    rangqiushu = "9";
                                    break;
                                case "十":
                                    rangqiushu = "10";
                                    break;
                            }
                        }
                        else
                        {
                            rangqiushu = p8.Substring(qiankuohaoIndex + 1, houkuohaoIndex - qiankuohaoIndex - 1);
                        }
                    }
                    else
                    {
                        int rangIndex = p8.LastIndexOf("让");
                        int qiuIndex = p8.LastIndexOf("球");
                        if (rangIndex != -1)
                        {
                            rangqiushu = p8.Substring(rangIndex + 1, qiuIndex - rangIndex - 1);
                        }
                        else if (qiankuohaoIndex != -1)
                        {
                            rangqiushu = p8.Substring(qiankuohaoIndex + 1, houkuohaoIndex - qiankuohaoIndex - 1);
                        }
                    }
                }
                if (rangqiushu == "0")
                {
                    hasrangqiu = 0;
                }
                else
                {
                    hasrangqiu = 1;
                }


                //保存分析数据
                Yuceanalysis analy = new Yuceanalysis();
                analy.Url = url;
                analy.Zhudui = zhudui;
                analy.Kedui = kedui;
                analy.Yucetype = caizhongtype;
                analy.Bisaishijian = Convert.ToDateTime(bisaishijian);
                analy.Touzhushijian = Convert.ToDateTime(bisaishijian).AddHours(-11).Date;
                analy.Rangqiushu = rangqiushu;
                analy.Hasrangqiu = hasrangqiu;
                analy.OperateTime = DateTime.Now;
                analy.Operator = "吴林";
                analy.Yucespf = spf;
                analy.Yuceren = yuceren;
                dal.InsertYuceanalysis(analy);
            }

            MessageBox.Show("分析成功！");
        }

        private string DeleteKuohao(string originString)
        {
            int kuohaoindex = originString.IndexOf("(");
            if (kuohaoindex != -1 && kuohaoindex > 0)
            {
                originString = originString.Substring(0, kuohaoindex - 1);
            }
            return originString;
        }

        private string PreHandleData2(String data)
        {
            return data.Replace(" ", "").Replace("&nbsp;", "").ToUpper().Replace("VS", " VS ").Replace("：", "： ");
        }

        private string PreHandleData3(String data)
        {
            if (data.IndexOf("星期") != -1)
            {
                return
                    data.Replace("：", ":")
                        .Replace(" ", "")
                        .Replace("&nbsp;", "")
                        .Replace("星期一", " ")
                        .Replace("星期二", " ")
                        .Replace("星期三", " ")
                        .Replace("星期四", " ")
                        .Replace("星期五", " ")
                        .Replace("星期六", " ")
                        .Replace("星期日", " ")
                        .Replace("星期天", " ");
            }
            else
            {
            }
            return data.Replace("：", ":").Replace(". ", "").Replace("&nbsp;", "").ToUpper();
        }

        private string PreHandleData8(String data)
        {
            return data.Replace(" ", "").Replace("&nbsp;", "").ToUpper().Replace(":", "：");
        }


        private void btnImportPeilvFromKaijiang_Click(object sender, EventArgs e)
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

        private void btnImportYDN_Click(object sender, EventArgs e)
        {
            try
            {
                //http://www.ydniu.com/Zx/jingcai/List_3_2_1.aspx
                string prefix = @"http://www.ydniu.com";
                //获取url列表
                String page = txtPage.Text;
                String tempurl = @"http://www.ydniu.com/Zx/jingcai/List_3_2_{0}.aspx";
                string totalUrl = string.Format(tempurl, page);

                //String totalUrl = @"http://www.ydniu.com/Zx/jingcai/List_3_2_0.aspx";
                string totalHtmlData = GetHtmlFromUrl(totalUrl);
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(totalHtmlData);

                HtmlNode rootNode = document.DocumentNode;

                HtmlNode parentNode = rootNode.SelectSingleNode(@"//div[@class='zx_leftssq']");
                HtmlNodeCollection dds = parentNode.SelectNodes("./dl/dd");

                List<string> detailUrls = new List<string>();
                foreach (HtmlNode dd in dds)
                {
                    HtmlNode a = dd.ChildNodes[0];
                    String detailUrl = a.Attributes["href"].Value;
                    detailUrls.Add(detailUrl);
                }

                List<AppYuce> yuces = new List<AppYuce>();
                foreach (String detailUrl in detailUrls)
                {
                    String realUrl = prefix + detailUrl;
                    string detailHtml = GetHtmlFromUrl(realUrl);

                    //HtmlAgilityPack.HtmlDocument detailDocument = new HtmlAgilityPack.HtmlDocument();
                    document.LoadHtml(detailHtml);
                    HtmlNode detailRootNode = document.DocumentNode;
                    //先取title，再提取xingqi，提取主队和客队
                    //title包含的信息太多,编号，联赛
                    String title = detailRootNode.SelectSingleNode(@"//title").InnerText.Trim();
                    Console.WriteLine(detailUrl);
                    Console.WriteLine(title);

                    //如果不是以“周”开头的可以不用处理
                    if (title.IndexOf("预测分析") == -1)
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

                    int firstSpaceIndex = title.IndexOf(" ");
                    int lastSpaceIndex = title.LastIndexOf(" ");

                    String firstP = title.Substring(0, firstSpaceIndex);
                    String secondP = title.Substring(lastSpaceIndex);


                    string weekday = "";
                    string bianhao = "";
                    string liansai = "";

                    Regex reg = new Regex(@"\d{3}");
                    Match m = reg.Match(firstP);
                    if (m.Length != 0)
                    {
                        //编号为3位
                        bianhao = m.ToString();
                        weekday = firstP.Substring(0, 2);
                        liansai = firstP.Substring(5);
                    }
                    else
                    {
                        Regex reg2 = new Regex(@"\d{2}");
                        Match m2 = reg2.Match(firstP);
                        if (m2.Length != 0)
                        {
                            //编号为2位
                            bianhao = m2.ToString();
                            bianhao = "0" + bianhao;
                            weekday = firstP.Substring(0, 2);
                            liansai = firstP.Substring(4);
                        }
                        else
                        {
                            //没有编号
                            bianhao = "";
                            weekday = firstP.Substring(0, 2);
                            liansai = firstP.Substring(2);
                        }
                    }

                    AppYuce yuce = new AppYuce();
                    yuce.bianhao = bianhao;
                    yuce.weekday = weekday;
                    yuce.liansai = liansai;

                    int vsIndex = secondP.ToUpper().IndexOf("VS");
                    yuce.kedui = secondP.Substring(vsIndex + 2, secondP.Length - vsIndex - 6);
                    yuce.zhudui = secondP.Substring(0, vsIndex);

                    //获取操作时间
                    HtmlNode node = detailRootNode.SelectSingleNode(@"//div[@class='fuzu']");
                    string fuzuString = node.InnerText.Trim();
                    string timeString = fuzuString.Substring(0, 19);

                    DateTime publishTime = Convert.ToDateTime(timeString);
                    DateTime bisaiTime = GetMatchTime(publishTime, weekday).Date;
                    yuce.riqi = bisaiTime.ToString("yyyy-MM-dd");

                    yuce.author = GetAuthor(fuzuString);
                    yuce.url = realUrl;
                    yuce.title = title;

                    yuce.operPerson = "system";
                    yuce.operateTime = DateTime.Now;

                    //经检验，下面算法不适合用来获取推荐结果，只有小编蜗牛居适用
                    //比较准确的算法是先查找“竞彩推荐：”，再往后搜索2个字符，如果是数字即加入结果中，
                    //没有“竞彩推荐：”，则搜索“推荐：”，然后再往后搜索2个字符，获得结果
                    yuce.spfresult = GetSpfResult(detailHtml);
                    //获取推荐的结果
                    //int tuijianIndex = detailHtml.IndexOf("推荐：");
                    //int spanIndex = detailHtml.Substring(tuijianIndex).IndexOf("</span>");
                    //yuce.spfresult = detailHtml.Substring(tuijianIndex + 3 , spanIndex - 3);


                    yuces.Add(yuce);
                }

                //添加预测到数据库

                new AppYuceDAL().InsertAppYuceList(yuces);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            MessageBox.Show("操作成功!");
        }

        private string GetSpfResult(string detailHtml)
        {
            string result = "";
            //经检验，下面算法不适合用来获取推荐结果，只有小编蜗牛居适用
            //比较准确的算法是先查找“竞彩推荐：”，再往后搜索2个字符，如果是数字即加入结果中，
            //没有“竞彩推荐：”，则搜索“推荐：”，然后再往后搜索2个字符，获得结果
            int jingcaiIndex = detailHtml.IndexOf("竞彩推荐：");
            if (jingcaiIndex == -1)
            {
                jingcaiIndex = detailHtml.IndexOf("竞彩推荐:");
            }
            int tuijianIndex = detailHtml.IndexOf("推荐：");
            if (jingcaiIndex != -1)
            {
                string presult = detailHtml.Substring(jingcaiIndex + 5, 10).Trim();
                foreach (char ch in presult.ToCharArray())
                {
                    if (ch <= '9' && ch >= '0')
                    {
                        result += ch;
                    }
                    else
                    {
                        return result;
                    }
                }
            }
            else if (tuijianIndex != -1)
            {
                string presult = detailHtml.Substring(tuijianIndex + 3, 10).Trim();
                foreach (char ch in presult.ToCharArray())
                {
                    if (ch <= '9' && ch >= '0')
                    {
                        result += ch;
                    }
                    else
                    {
                        return result;
                    }
                }
            }
            return result;
        }

        private string GetAuthor(string fuzuString)
        {
            int zuozheIndex = fuzuString.IndexOf("作者：");
            int liulanIndex = fuzuString.IndexOf("已浏览");
            return fuzuString.Substring(zuozheIndex + 3, liulanIndex - zuozheIndex - 3).Trim();
        }

        private void btnImportLanqiu_Click(object sender, EventArgs e)
        {
            try
            {
                List<BasketResult> results = new List<BasketResult>();

                String uri =
                    @"http://www.aicai.com/lottery/jcReport!lcMatchResult.jhtml?lotteryType=4061&matchNames=&startMatchTime=2014-12-08&endMatchTime=2014-12-11";
                string htmlData = GetHtmlFromUrl(uri);
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(htmlData);

                HtmlNode rootNode = document.DocumentNode;
                //class="kjjc_tablebox"
                HtmlNode tableNode = rootNode.SelectSingleNode(@"//div[@class='kjjc_tablebox']");
                HtmlNodeCollection trs = tableNode.SelectNodes("./table/tr[contains(@id,'tra')]");

                foreach (HtmlNode node in trs)
                {
                    BasketResult result = GetBasketResult(node);
                    results.Add(result);
                }
                //这里进行数据的添加操作即可
                new BasketResultDAL().InsertBasketResultList(results);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            MessageBox.Show("操作完成！");
        }

        private BasketResult GetBasketResult(HtmlNode node)
        {
            BasketResult result = new BasketResult();
            HtmlNodeCollection tds = node.SelectNodes(@"./td");
            result.saishi = tds[0].InnerText.Trim();
            result.saishibianhao = Regex.Replace(tds[1].InnerText, @"\s", "");
            result.bisaishijian = tds[2].InnerText.Trim();
            result.kedui = tds[3].InnerText.Trim();
            result.zhudui = tds[4].InnerText.Trim();
            result.zhongchangbifen = tds[10].InnerText.Trim();
            result.createtime = DateTime.Now;
            result.creator = "system";
            result.riqi = result.bisaishijian.Substring(0, 10);
            result.bianhao = result.saishibianhao.Substring(result.saishibianhao.Length - 3);
            return result;
        }

        private void btnImportLanqiuYuce_Click(object sender, EventArgs e)
        {
            try
            {
                string prefix = @"http://www.ydniu.com";
                //获取url列表
                String totalUrl = @"http://www.ydniu.com/Zx/jclq/List_29_2_0.aspx";

                string totalHtmlData = GetHtmlFromUrl(totalUrl);
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(totalHtmlData);

                HtmlNode rootNode = document.DocumentNode;

                HtmlNode parentNode = rootNode.SelectSingleNode(@"//div[@class='zx_leftssq']");
                HtmlNodeCollection dds = parentNode.SelectNodes("./dl/dd");

                List<string> detailUrls = new List<string>();
                foreach (HtmlNode dd in dds)
                {
                    HtmlNode a = dd.ChildNodes[0];
                    String detailUrl = a.Attributes["href"].Value;
                    detailUrls.Add(detailUrl);
                }
                //div class="zx_leftssq
                //dl/dd
                //div class="fuzu"
                List<BasketYuce> yuces = new List<BasketYuce>();
                //循环每个url，获取网页数据，然后进行分析
                foreach (String detailUrl in detailUrls)
                {
                    String realUrl = prefix + detailUrl;
                    string detailHtml = GetHtmlFromUrl(realUrl);

                    //HtmlAgilityPack.HtmlDocument detailDocument = new HtmlAgilityPack.HtmlDocument();
                    document.LoadHtml(detailHtml);
                    HtmlNode detailRootNode = document.DocumentNode;
                    //先取title，再提取xingqi，提取主队和客队
                    //title包含的信息太多，有两种模式，1，带括号的 2，不带括号的
                    String title = detailRootNode.SelectSingleNode(@"//title").InnerText.Replace("  ", " ").Trim();
                    //title = @"周一NBA 克利夫兰骑士VS布鲁克林篮网（主）预测分析";
                    BasketYuce yuce = new BasketYuce();
                    yuce.url = realUrl;
                    yuce.title = title;
                    String xingqi = title.Substring(0, 2);
                    //先去掉空格前的字符
                    String[] bodys = title.Split(' ');
                    String body = title.Split(' ')[1].Trim();
                    int kuohaoIndex = body.IndexOf("（");
                    String mainBody = "";
                    if (kuohaoIndex == -1)
                    {
                        int fenxiIndex = body.IndexOf("预测分析");

                        mainBody = body.Substring(0, fenxiIndex);
                    }
                    else
                    {
                        mainBody = body.Substring(0, kuohaoIndex);
                    }
                    int vsIndex = mainBody.IndexOf("VS");


                    yuce.xingqi = xingqi;
                    yuce.zhudui = mainBody.Substring(vsIndex + 2);
                    yuce.kedui = mainBody.Substring(0, vsIndex);


                    int index = detailHtml.IndexOf("竞彩推荐");
                    String result = detailHtml.Substring(index + 5, 2);
                    yuce.result = result;
                    yuce.operPerson = "system";
                    yuce.operateTime = DateTime.Now;

                    //获取操作时间
                    HtmlNode node = detailRootNode.SelectSingleNode(@"//div[@class='fuzu']");
                    string timeString = node.InnerText.Trim().Substring(0, 19);

                    DateTime publishTime = Convert.ToDateTime(timeString);
                    DateTime bisaiTime = GetMatchTime(publishTime, xingqi).Date;
                    yuce.bisairiqi = bisaiTime.ToString("yyyy-MM-dd");


                    yuces.Add(yuce);
                }

                //保存在数据库
                new BasketYuceDAL().InsertBasketYuceList(yuces);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            MessageBox.Show("操作成功！");
        }

        private DateTime GetMatchTime(DateTime publishTime, string xingqi)
        {
            int publishWeekday = (int) publishTime.DayOfWeek;
            int weekday = 0;
            switch (xingqi)
            {
                case "周一":
                    weekday = 1;
                    break;
                case "周二":
                    weekday = 2;
                    break;
                case "周三":
                    weekday = 3;
                    break;
                case "周四":
                    weekday = 4;
                    break;
                case "周五":
                    weekday = 5;
                    break;
                case "周六":
                    weekday = 6;
                    break;
                case "周日":
                    weekday = 7;
                    break;
            }
            return publishTime.AddDays(weekday - publishWeekday);
        }

        private void btnUpdateLanqiuResult_Click(object sender, EventArgs e)
        {
            MessageBox.Show("操作成功！");
        }

        private void btnImportYdnMul_Click(object sender, EventArgs e)
        {
//            rhw = ThreadPool.RegisterWaitForSingleObject(new AutoResetEvent(false),
//                this.CheckThreadPool, null, 1000, false);
            String page = txtPage.Text;
            String tempurl = @"http://www.ydniu.com/Zx/jingcai/List_3_2_{0}.aspx";
            string totalUrl = string.Format(tempurl, page);
            //String totalUrl = @"http://www.ydniu.com/Zx/jingcai/List_3_2_1.aspx";
            string totalHtmlData = GetHtmlFromUrl(totalUrl);
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(totalHtmlData);

            HtmlNode rootNode = document.DocumentNode;

            HtmlNode parentNode = rootNode.SelectSingleNode(@"//div[@class='zx_leftssq']");
            HtmlNodeCollection dds = parentNode.SelectNodes("./dl/dd");

            List<string> detailUrls = new List<string>();
            foreach (HtmlNode dd in dds)
            {
                HtmlNode a = dd.ChildNodes[0];
                String detailUrl = a.Attributes["href"].Value;
                detailUrls.Add(detailUrl);
            }
            foreach (string detailUrl in detailUrls)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(this.GetYuceDetailInThread), detailUrl);
            }

            Console.WriteLine("插入线程任务完成！");
        }

        private void GetYuceDetailInThread(Object param)
        {
            try
            {
                string prefix = @"http://www.ydniu.com";
                string detailUrl = param.ToString();
                List<AppYuce> yuces = new List<AppYuce>();
                String realUrl = prefix + detailUrl;
                string detailHtml = GetHtmlFromUrl(realUrl);

                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(detailHtml);
                HtmlNode detailRootNode = document.DocumentNode;
                //先取title，再提取xingqi，提取主队和客队
                //title包含的信息太多,编号，联赛
                String title = detailRootNode.SelectSingleNode(@"//title").InnerText.Replace("　", " ").Trim();
                Console.WriteLine(detailUrl);
                Console.WriteLine(title);

                //如果不是以“周”开头的可以不用处理
                if (title.IndexOf("预测分析") == -1)
                {
                    //需要加入手工处理表
                    AppYuceBadUrl badUrl = new AppYuceBadUrl();
                    badUrl.title = title;
                    badUrl.url = realUrl;
                    badUrl.prefix = prefix;
                    badUrl.creator = "system";
                    badUrl.createtime = DateTime.Now;
                    new AppYuceDAL().InsertBadUrl(badUrl);
                    return;
                }

                int firstSpaceIndex = title.IndexOf(" ");
                int lastSpaceIndex = title.LastIndexOf(" ");

                String firstP = title.Substring(0, firstSpaceIndex);
                String secondP = title.Substring(lastSpaceIndex);


                string weekday = "";
                string bianhao = "";
                string liansai = "";

                Regex reg = new Regex(@"\d{3}");
                Match m = reg.Match(firstP);
                if (m.Length != 0)
                {
                    //编号为3位
                    bianhao = m.ToString();
                    weekday = firstP.Substring(0, 2);
                    liansai = firstP.Substring(5);
                }
                else
                {
                    Regex reg2 = new Regex(@"\d{2}");
                    Match m2 = reg2.Match(firstP);
                    if (m2.Length != 0)
                    {
                        //编号为2位
                        bianhao = m2.ToString();
                        bianhao = "0" + bianhao;
                        weekday = firstP.Substring(0, 2);
                        liansai = firstP.Substring(4);
                    }
                    else
                    {
                        //没有编号
                        bianhao = "";
                        weekday = firstP.Substring(0, 2);
                        liansai = firstP.Substring(2);
                    }
                }

                AppYuce yuce = new AppYuce();
                yuce.bianhao = bianhao;
                yuce.weekday = weekday;
                yuce.liansai = liansai;

                int vsIndex = secondP.ToUpper().IndexOf("VS");
                yuce.kedui = secondP.Substring(vsIndex + 2, secondP.Length - vsIndex - 6);
                yuce.zhudui = secondP.Substring(0, vsIndex);

                //获取操作时间
                HtmlNode node = detailRootNode.SelectSingleNode(@"//div[@class='fuzu']");
                string fuzuString = node.InnerText.Trim();
                string timeString = fuzuString.Substring(0, 19);

                DateTime publishTime = Convert.ToDateTime(timeString);
                DateTime bisaiTime = GetMatchTime(publishTime, weekday).Date;
                yuce.riqi = bisaiTime.ToString("yyyy-MM-dd");

                yuce.author = GetAuthor(fuzuString);
                yuce.url = realUrl;
                yuce.title = title;

                yuce.operPerson = "system";
                yuce.operateTime = DateTime.Now;

                //经检验，下面算法不适合用来获取推荐结果，只有小编蜗牛居适用
                //比较准确的算法是先查找“竞彩推荐：”，再往后搜索2个字符，如果是数字即加入结果中，
                //没有“竞彩推荐：”，则搜索“推荐：”，然后再往后搜索2个字符，获得结果
                yuce.spfresult = GetSpfResult(detailHtml);
                //获取推荐的结果
                //int tuijianIndex = detailHtml.IndexOf("推荐：");
                //int spanIndex = detailHtml.Substring(tuijianIndex).IndexOf("</span>");
                //yuce.spfresult = detailHtml.Substring(tuijianIndex + 3 , spanIndex - 3);


                yuces.Add(yuce);
                //添加预测到数据库

                new AppYuceDAL().InsertAppYuceList(yuces);
                Console.WriteLine(param.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void txtImportSingleUrl_Click(object sender, EventArgs e)
        {
            String detailUrl = txtUrl.Text;

            GetYuceDetailInThread(detailUrl);
        }

        private void ImportBasketRqResult(DateTime importDateTime)
        {
            try
            {
                string prefix = @"http://www.ydniu.com";
                //获取url列表
                String page = importDateTime.ToString("yyyy-MM-dd");
                String tempurl = @"http://www.310win.com/buy/JingCaiBasket.aspx?typeID=112&oddstype=2&date={0}";
                string totalUrl = string.Format(tempurl, page);


                string totalHtmlData = GetHtmlFromUrl(totalUrl);
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(totalHtmlData);

                HtmlNode rootNode = document.DocumentNode;

                HtmlNode tableNode = rootNode.SelectSingleNode(@"//table[@id='MatchTable']");
                HtmlNodeCollection dds = tableNode.SelectNodes("./tr");

                String riqi = DateTime.Now.ToString("yyyy年MM月dd日");
                List<BasketRqResult> results = new List<BasketRqResult>();
                //循环访问行
                foreach (HtmlNode tr in dds)
                {
                    if (tr.Attributes["class"].Value == "ttis")
                    {
                        continue;
                    }
                    if (tr.Attributes["class"].Value == "niDate")
                    {
                        riqi = tr.InnerText.Substring(0, 11);
                        continue;
                    }
                    if (tr.Attributes["class"].Value == "ni" || tr.Attributes["class"].Value == "ni2")
                    {
                        BasketRqResult result = new BasketRqResult();
                        HtmlNodeCollection tds = tr.SelectNodes("./td");

                        result.changci = tds[0].InnerText;
                        result.saishi = tds[1].InnerText;
                        result.zhuangtai = tds[3].InnerText;
                        result.zhudui = tds[4].InnerText.Trim();
                        result.bifen = tds[5].InnerText;
                        result.kedui = tds[6].InnerText.Trim();
                        result.zhusheng = tds[11].InnerText;
                        result.rangfen = tds[12].InnerText;
                        result.kesheng = tds[13].InnerText;
                        string rqresult = "1";
                        //rqresult
                        if (tds[11].Attributes["class"] != null && tds[11].Attributes["class"].Value == "bonus")
                        {
                            rqresult = "3";
                        }
                        if (tds[13].Attributes["class"] != null && tds[13].Attributes["class"].Value == "bonus")
                        {
                            rqresult = "0";
                        }
                        result.rqresult = rqresult;
                        result.riqi = riqi;
                        result.creator = "wulin";
                        result.createtime = DateTime.Now;
                        results.Add(result);
                    }
                }

                new BasketRqResultDAL().InsertBasketRqResultList(results);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnLanqiuRqResult_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime StartDate = dtpStartDate.Value;
                DateTime EndDate = dtpEndDate.Value;
                int count = 0;
                while (StartDate <= EndDate)
                {
                    //先删除当前日期的开奖结果，避免重复
                    //string riqi = StartDate.Date.ToString("yyyy-MM-dd");
                    //new KaijiangDAL().DeleteKaijiang(riqi);

                    ImportBasketRqResult(StartDate);
                    StartDate = StartDate.AddDays(1);
                    count++;
                }
                MessageBox.Show("导入成功,导入了" + count + "条篮球结果");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
                    single.riqi = new DateTime(year,month,day).ToString("yyyy-MM-dd");
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

        private void btnImportKaijianMul_Click(object sender, EventArgs e)
        {

        }
    }
}