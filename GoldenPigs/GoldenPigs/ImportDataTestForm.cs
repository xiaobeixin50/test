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
using HtmlAgilityPack;
using GoldenPigs.Entity;
using GoldenPigs.DAL;  

namespace GoldenPigs
{
    public partial class ImportDataTestForm : Form
    {
        private const string CategoryListXPath = "//html[1]/body[1]/div[1]/div[1]/div[2]/div[1]/div[1]/dl[1]/dd[3]/div[1]/ul[1]/li";
        private const string VoteCode = "//div[@votecode]";
        //CreateAt[@type='zh-cn']
        public ImportDataTestForm()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
             //WebClient wc = new WebClient();
             //wc.Encoding = Encoding.UTF8;
             //string uri = txtUrl.Text;
             //Stream stream = wc.OpenRead(uri);
             //StreamReader sr = new StreamReader(stream);
             //string strLine = "";
             //StringBuilder sb = new StringBuilder();
             //while ((strLine = sr.ReadLine()) != null)
             //{
             //    sb.Append(strLine);
             //}
             //sr.Close();
             //txtHtml.Text = sb.ToString();
             ////MessageBox.Show("获取成功！");
             //String html = new AiCaiExtractor().ExtracrHtml(txtHtml.Text);

             //HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
             //document.LoadHtml(html);
             //HtmlNode rootNode = document.DocumentNode;



        }

        private void button1_Click(object sender, EventArgs e)
        {
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            
            document.Load("HTMLPage4.html", Encoding.UTF8);
            HtmlNode rootNode = document.DocumentNode;

            //<div class="pub_mod_hd">
            HtmlNodeCollection typeList = rootNode.SelectNodes(@"//div[@class='pub_mod_i mt10']");

            HtmlNode temp = null;

            foreach(HtmlNode typeNode in typeList)
            {
                //MessageBox.Show(typeNode.InnerText);
                temp = HtmlNode.CreateNode(typeNode.OuterHtml);
                HtmlNodeCollection strong = temp.SelectNodes("//div[@class='pub_mod_hd']/strong[@class='pub_mod_title yahei']");

                String type = strong[0].InnerText;
                //MessageBox.Show(strong[0].InnerText);

               //HtmlNodeCollection strong  = typeNode.FirstChild.SelectNodes("//div[@class='pub_mod_hd']/strong[@class='pub_mod_title yahei']");
               //MessageBox.Show(strong[0].InnerText);
                //MessageBox.Show(type);

                HtmlNodeCollection voteCodeList = temp.SelectNodes(VoteCode);
                HtmlNode temp2 = null;
                foreach (HtmlNode node in voteCodeList)
                {
                    //MessageBox.Show(node.XPath);
                    //HtmlNodeCollection nodes = node.SelectNodes("/div");
                    //foreach (HtmlNode nd in nodes)
                    //{
                    //    MessageBox.Show(nd.InnerHtml);
                    //}

                    temp2 = HtmlNode.CreateNode(node.OuterHtml);
                    //回报率
                    HtmlNode game_info = temp2.SelectSingleNode(@"//div[@class='game_info']/strong[2]");
                    double huibaolv = Convert.ToDouble(game_info.InnerText);
                    //MessageBox.Show(game_info.InnerText);
                    //获取主队1，客队1，主队2，客队2
                    HtmlNodeCollection duimings = temp2.SelectNodes("//div[@class='game_dz']/span");
                    string zhudui1 = duimings[0].InnerText;
                    string kedui1 = duimings[1].InnerText;
                    string zhudui2 = duimings[2].InnerText;
                    string kedui2 = duimings[3].InnerText;


                    //foreach (HtmlNode duiming in duimings)
                    //{
                    //    MessageBox.Show(duiming.InnerText);
                    //}
                    //获取投入和奖金 game_money
                    HtmlNodeCollection gameMoney = temp2.SelectNodes("//div[@class='game_money']/span");
                    int touru = Convert.ToInt32(gameMoney[0].InnerText);
                    double jiangjin = Convert.ToDouble(gameMoney[1].InnerText);


                    Taocan taocan = new Taocan();
                    taocan.Gailv = "88+%";
                    taocan.Huibaolv = huibaolv;
                    taocan.Qishu = GetQishu();
                    taocan.Riqi = DateTime.Now.Date;
                    taocan.Touru = touru;
                    taocan.Jiangjin = jiangjin;
                    taocan.Type = type;
                    taocan.Lucky = 0;
                    taocan.Zhudui1 = zhudui1;
                    taocan.Zhudui1 = zhudui2;
                    taocan.Kedui1 = kedui1;
                    taocan.Kedui2 = kedui2;

                    new TaocanDAL().InsertTaocan(taocan);


                }
            }
            

        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            //String html = new AiCaiExtractor().ExtracrHtml(txtHtml.Text);
            //HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            //document.LoadHtml(html);

            ////document.Load("HTMLPage4.html", Encoding.UTF8);


            //HtmlNode rootNode = document.DocumentNode;
            ////class="pub_mod_bd pub_hc_wzj  clearfix"> 
            ////<div class="pub_mod_hd">
            //HtmlNodeCollection typeList = rootNode.SelectNodes(@"//div[@class='pub_mod_i mt10']");

            //HtmlNode temp = null;

            //foreach (HtmlNode typeNode in typeList)
            //{
            //    //MessageBox.Show(typeNode.InnerText);
            //    temp = HtmlNode.CreateNode(typeNode.OuterHtml);

            //    HtmlNodeCollection strong = temp.SelectNodes("//div[@class='pub_mod_hd']/strong[@class='pub_mod_title yahei']");

            //    String type = strong[0].InnerText;
            //    //MessageBox.Show(strong[0].InnerText);

            //    //HtmlNodeCollection strong  = typeNode.FirstChild.SelectNodes("//div[@class='pub_mod_hd']/strong[@class='pub_mod_title yahei']");
            //    //MessageBox.Show(strong[0].InnerText);
            //    //MessageBox.Show(type);

            //    HtmlNodeCollection voteCodeList = temp.SelectNodes(@"//div[@class='pub_mod_bd pub_hc_wzj  clearfix']");
            //        //temp.SelectNodes(VoteCode);
            //    HtmlNode temp2 = null;
            //    foreach (HtmlNode node in voteCodeList)
            //    {
            //        //MessageBox.Show(node.XPath);
            //        //HtmlNodeCollection nodes = node.SelectNodes("/div");
            //        //foreach (HtmlNode nd in nodes)
            //        //{
            //        //    MessageBox.Show(nd.InnerHtml);
            //        //}

            //        temp2 = HtmlNode.CreateNode(node.OuterHtml);

            //        //概率
            //        //class="pstCell"
            //        HtmlNode gailv = temp2.SelectSingleNode(@"//div[@class='pstCell']");
            //        string gailvStr = gailv.InnerText;
            //        //回报率
            //        HtmlNode game_info = temp2.SelectSingleNode(@"//div[@class='game_info']/strong[2]");
            //        double huibaolv = Convert.ToDouble(game_info.InnerText);
            //        //MessageBox.Show(game_info.InnerText);
            //        //获取主队1，客队1，主队2，客队2
            //        HtmlNodeCollection duimings = temp2.SelectNodes("//div[@class='game_dz']/span");
            //        string zhudui1 = duimings[0].InnerText;
            //        string kedui1 = duimings[1].InnerText;
            //        string zhudui2 = duimings[2].InnerText;
            //        string kedui2 = duimings[3].InnerText;


            //        //foreach (HtmlNode duiming in duimings)
            //        //{
            //        //    MessageBox.Show(duiming.InnerText);
            //        //}
            //        //获取投入和奖金 game_money
            //        HtmlNodeCollection gameMoney = temp2.SelectNodes("//div[@class='game_money']/span");
            //        int touru = Convert.ToInt32(gameMoney[0].InnerText);
            //        double jiangjin = Convert.ToDouble(gameMoney[1].InnerText);


            //        Taocan taocan = new Taocan();
            //        taocan.Gailv = gailvStr;
            //        taocan.Huibaolv = huibaolv;
            //        taocan.Qishu = GetQishu();
            //        taocan.Riqi = GetRiqi();
            //        taocan.Touru = touru;
            //        taocan.Jiangjin = jiangjin;
            //        taocan.Type = type;
            //        taocan.Lucky = 0;
            //        taocan.Zhudui1 = zhudui1;
            //        taocan.Zhudui1 = zhudui2;
            //        taocan.Kedui1 = kedui1;
            //        taocan.Kedui2 = kedui2;

            //        new TaocanDAL().InsertTaocan(taocan);


            //    }
            //}

            //MessageBox.Show("导入成功！");
        }

        private int GetQishu()
        {
            if(!cbImportDate.Checked)
            {
                DateTime dtNow = DateTime.Now;
                string strNow = dtNow.ToString("yyyyMMdd");
                return Convert.ToInt32(strNow);
            }
            else
            {
                DateTime date = Convert.ToDateTime(txtImportDate.Text);
                string strNow = date.ToString("yyyyMMdd");
                return Convert.ToInt32(strNow);
            }
            
        }

        private DateTime GetRiqi()
        {
            if (cbImportDate.Checked)
            {
                DateTime date = Convert.ToDateTime(txtImportDate.Text);
                return date;
            }
            else
            {
                return DateTime.Now.Date;
            }
        }

        private void ImportData_Load(object sender, EventArgs e)
        {
            txtImportDate.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
        }

        private void btnImportJifen_Click(object sender, EventArgs e)
        {
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            //document.LoadHtml(html);
            string fileName = "html/积分榜日职联.html";
            switch (comLiansai.Text)
            {
                case "日职联":
                    fileName = "html/积分榜日职联.html";
                    break;
                case "日职乙":
                    fileName = "html/积分榜日职乙.html";
                    break;

            }

            document.Load(fileName, Encoding.UTF8);
            
            HtmlNode rootNode = document.DocumentNode;

            HtmlNodeCollection trs = rootNode.SelectNodes(@"//tbody[@id='jq_rank_tbody']/tr");

            foreach (HtmlNode tr in trs)
            {
                //MessageBox.Show(node.InnerHtml);
                //foreach (HtmlNode td in table.ChildNodes)
                //{
                //    MessageBox.Show(td.InnerHtml);
                //}
                HtmlNodeCollection tds = tr.SelectNodes("./td");
                //foreach (HtmlNode td in tds)
                //{
                //    MessageBox.Show(td.InnerHtml);
                //}

                Jifenbang jifenbang = new Jifenbang();
                jifenbang.Liansai = comLiansai.Text;
                jifenbang.Paiming = Convert.ToInt32(tds[0].InnerText);
                jifenbang.Qiudui = tds[1].InnerText;
                jifenbang.Yisai = Convert.ToInt32(tds[2].InnerText);
                jifenbang.Sheng = Convert.ToInt32(tds[3].InnerText);
                jifenbang.Ping = Convert.ToInt32(tds[4].InnerText);
                jifenbang.Fu = Convert.ToInt32(tds[5].InnerText);
                string deshiqiu = tds[6].InnerText;
                string[] qiu = deshiqiu.Split('/');

                jifenbang.Deqiu = Convert.ToInt32(qiu[0]);
                jifenbang.Shiqiu = Convert.ToInt32(qiu[1]);
                jifenbang.Jingshengqiu = Convert.ToInt32(tds[7].InnerText);
                jifenbang.Junde = Convert.ToDouble(tds[8].InnerText);
                jifenbang.Junshi = Convert.ToDouble(tds[9].InnerText);
                jifenbang.Shenglv = Convert.ToDouble(tds[10].InnerText);
                jifenbang.Pinglv = Convert.ToDouble(tds[11].InnerText);
                jifenbang.Fulv = Convert.ToDouble(tds[12].InnerText);
                jifenbang.Jifen = Convert.ToInt32(tds[13].InnerText);
                jifenbang.Operator = "吴林";
                jifenbang.Operatetime = DateTime.Now;

                new JifenbangDAL().InsertJifenbang(jifenbang);

            }
            MessageBox.Show("导入联赛积分榜成功！");


        }

        private void btnImportResult_Click(object sender, EventArgs e)
        {
            //从开奖结果全里面导入开奖数据

            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.Load("html/开奖结果全0805.html", Encoding.UTF8);

            HtmlNode rootNode = document.DocumentNode;
             //class="gameTime"

            HtmlNode gameTime = rootNode.SelectSingleNode("//td[@class='gameTime']");
            //MessageBox.Show(gameTime.InnerText);
            String xinqi = gameTime.InnerText.Substring(0, 3);
            String riqi = gameTime.InnerText.Substring(4, 10);

            //第一层tr节点
            //class="jq_gdhh_match_select_tr evenTr" 偶数节点
            //class="jq_gdhh_match_select_tr" 基数节点
            HtmlNodeCollection matches = rootNode.SelectNodes("//tr[@class='jq_gdhh_match_select_tr']");

            //class="phaoTd"
            //class="saiTd"
            foreach (HtmlNode match in matches)
            {

                Kaijiang kaijiang = new Kaijiang();
                kaijiang.Xingqi = xinqi;
                kaijiang.Riqi = riqi;
                HtmlNode phaoTd = match.SelectSingleNode("./td[@class='phaoTd']");
                kaijiang.Bianhao = phaoTd.InnerText;
                //MessageBox.Show(phaoTd.InnerText);
                HtmlNode saiTd = match.SelectSingleNode("./td[@class='saiTd']");
                kaijiang.Liansai = saiTd.InnerText;
                //MessageBox.Show(saiTd.InnerText);
                //class="zhuTeamTd"
                HtmlNode zhuTeamTd = match.SelectSingleNode("./td[@class='zhuTeamTd']");
                kaijiang.Zhudui = zhuTeamTd.SelectSingleNode("./div/span/a").InnerText;
                //MessageBox.Show(zhuTeamTd.InnerText);
                //class="keTeamTd"
                HtmlNode keTeamTd = match.SelectSingleNode("./td[@class='keTeamTd']");
                kaijiang.Kedui = keTeamTd.SelectSingleNode("./div/span/a").InnerText ;

                HtmlNode brother = match.NextSibling.NextSibling;
                HtmlNodeCollection trs = brother.SelectNodes("./td/div/table/tbody/tr/td/div[@class='chanArea betChoose chanAreaRed']");

                
                kaijiang.SpfResult = trs[0].SelectSingleNode("./p").ChildNodes[0].InnerText;
                kaijiang.SpfSp = Convert.ToDouble(trs[0].SelectSingleNode("./p/span").InnerText);

                kaijiang.RqspfResult = trs[1].SelectSingleNode("./p").ChildNodes[0].InnerText;
                kaijiang.RqspfSp = Convert.ToDouble(trs[1].SelectSingleNode("./p/span").InnerText);

                kaijiang.QcbfResult = trs[2].SelectSingleNode("./p").InnerText;
                kaijiang.QcbfSp = Convert.ToDouble(trs[2].SelectSingleNode("./span").InnerText);

                kaijiang.ZjqResult = trs[3].SelectSingleNode("./p").InnerText;
                kaijiang.ZjqSp = Convert.ToDouble(trs[3].SelectSingleNode("./span").InnerText);

                kaijiang.BqcResult = trs[4].SelectSingleNode("./p").InnerText;
                kaijiang.BqcSp = Convert.ToDouble(trs[4].SelectSingleNode("./span").InnerText);

                kaijiang.OperateTime = DateTime.Now;
                kaijiang.Operator = "吴林";

                kaijiang.Bisaishijian = DateTime.MinValue ;
                kaijiang.Zhuduiliansai = "";
                kaijiang.Keduiliansai = "";
                new KaijiangDAL().InsertKaijiang(kaijiang);
            }

            HtmlNodeCollection matchesEven = rootNode.SelectNodes("//tr[@class='jq_gdhh_match_select_tr evenTr']");

            //class="phaoTd"
            //class="saiTd"
            foreach (HtmlNode match in matchesEven)
            {

                Kaijiang kaijiang = new Kaijiang();
                kaijiang.Xingqi = xinqi;
                kaijiang.Riqi = riqi;
                HtmlNode phaoTd = match.SelectSingleNode("./td[@class='phaoTd']");
                kaijiang.Bianhao = phaoTd.InnerText;
                //MessageBox.Show(phaoTd.InnerText);
                HtmlNode saiTd = match.SelectSingleNode("./td[@class='saiTd']");
                kaijiang.Liansai = saiTd.InnerText;
                //MessageBox.Show(saiTd.InnerText);
                //class="zhuTeamTd"
                HtmlNode zhuTeamTd = match.SelectSingleNode("./td[@class='zhuTeamTd']");
                kaijiang.Zhudui = zhuTeamTd.SelectSingleNode("./div/span/a").InnerText;
                //MessageBox.Show(zhuTeamTd.InnerText);
                //class="keTeamTd"
                HtmlNode keTeamTd = match.SelectSingleNode("./td[@class='keTeamTd']");
                kaijiang.Kedui = keTeamTd.SelectSingleNode("./div/span/a").InnerText;

                HtmlNode brother = match.NextSibling.NextSibling;
                HtmlNodeCollection trs = brother.SelectNodes("./td/div/table/tbody/tr/td/div[@class='chanArea betChoose chanAreaRed']");


                kaijiang.SpfResult = trs[0].SelectSingleNode("./p").ChildNodes[0].InnerText;
                kaijiang.SpfSp = Convert.ToDouble(trs[0].SelectSingleNode("./p/span").InnerText);

                kaijiang.RqspfResult = trs[1].SelectSingleNode("./p").ChildNodes[0].InnerText;
                kaijiang.RqspfSp = Convert.ToDouble(trs[1].SelectSingleNode("./p/span").InnerText);

                kaijiang.QcbfResult = trs[2].SelectSingleNode("./p").InnerText;
                kaijiang.QcbfSp = Convert.ToDouble(trs[2].SelectSingleNode("./span").InnerText);

                kaijiang.ZjqResult = trs[3].SelectSingleNode("./p").InnerText;
                kaijiang.ZjqSp = Convert.ToDouble(trs[3].SelectSingleNode("./span").InnerText);

                kaijiang.BqcResult = trs[4].SelectSingleNode("./p").InnerText;
                kaijiang.BqcSp = Convert.ToDouble(trs[4].SelectSingleNode("./span").InnerText);

                kaijiang.OperateTime = DateTime.Now;
                kaijiang.Operator = "吴林";

                kaijiang.Bisaishijian = DateTime.MinValue ;
                kaijiang.Zhuduiliansai = "";
                kaijiang.Keduiliansai = "";
                new KaijiangDAL().InsertKaijiang(kaijiang);
            }
            MessageBox.Show("导入数据成功！");
        }


        private void GetShengpingfu(HtmlNode node)
        {
            HtmlNode redNode = node.SelectSingleNode("./td/div[@class='chanArea betChoose chanAreaRed']");
            MessageBox.Show(redNode.SelectSingleNode("./p").ChildNodes[0].InnerText);
            MessageBox.Show(redNode.SelectSingleNode("./p/span").InnerText);
            //MessageBox.Show(redNode.InnerHtml);
        }

        private void btnImportCaikeDetail_Click(object sender, EventArgs e)
        {
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();

            document.Load("html/彩客咨询明细4.html", Encoding.UTF8);
            HtmlNode rootNode = document.DocumentNode;

            HtmlNodeCollection articleContent = rootNode.SelectNodes(@"//div[@class='articleContent']");
            HtmlNodeCollection p = articleContent[0].SelectNodes(@"./p");
            //Console.WriteLine(p.Count);
            Yucerawdata data = null;

            foreach (HtmlNode pChild in p)
            {
                string pText = pChild.InnerText.Trim();
                if (pText.IndexOf("VS")!= -1 || pText.IndexOf("vs")!= -1)
                {
                    //如果data没有初始化，说明没有添加信息
                    if (data == null)
                    {
                        data = new Yucerawdata();
                        data.OperateTime = DateTime.Now;
                        data.Operator = "吴林";
                        data.P2 = pText;
                        data.P1 = pChild.PreviousSibling.PreviousSibling.InnerText.Trim();
                    }
                    else
                    {
                        //添加到数据库，并设为null
                        new YucerawdataDAL().InsertYucerawdata(data);
                        data = new Yucerawdata();
                        data.OperateTime = DateTime.Now;
                        data.Operator = "吴林";
                        data.P2 = pText;
                        data.P1 = pChild.PreviousSibling.PreviousSibling.InnerText.Trim();
                    }
                   
                    int vsIndex = pText.IndexOf("VS");
                    string zhudui = pText.Substring(0, vsIndex).Trim();
                    string kedui = pText.Substring(vsIndex+2).Trim();
                    Console.WriteLine(zhudui);
                    Console.WriteLine(kedui);
                }
                if (pText.IndexOf("比赛时间") != -1)
                {
                    data.P3 = pText;

                    int bisaiIndex = pText.Trim().IndexOf("比赛时间");
                    string bisaishijian = pText.Trim().Substring(5);
                    Console.WriteLine(bisaishijian);
                }
                if (pText.IndexOf("欧赔") != -1)
                {
                    data.P4 = pText;
                }
                if (pText.IndexOf("盘口") != -1)
                {
                    data.P5 = pText;
                }
                if (pText.IndexOf("盘口") != -1)
                {
                    data.P5 = pText;
                }

                if (pText.IndexOf("赛事分析") != -1)
                {
                    data.P6 = pText;
                }
                if(pText.IndexOf ("盘赔数据分析") != -1)
                {
                    data.P7 = pText;
                }
                if (pText.IndexOf("推荐") != -1) //&& pText.IndexOf("竞彩") != -1
                {
                    data.P8 = pText;
                    //int tuijianIndex = pText.Trim().IndexOf("推荐");
                    int tuijianIndex = pText.Trim().IndexOf("：");
                    string tuijian = pText.Trim().Substring(tuijianIndex);
                    Console.WriteLine(tuijian);
                    

                }
               

            }
            if(data != null)
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

    }
}
