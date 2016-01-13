using GoldenPigs.DAL;
using GoldenPigs.Entity;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoldenPigs
{
    public partial class XinshuituijianForm : Form
    {
        public XinshuituijianForm()
        {
            InitializeComponent();
        }

        private void XinshuituijianForm_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void btnTianjian_Click(object sender, EventArgs e)
        {
            Xinshui xinshui = new Xinshui();
            xinshui.Riqi = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd");
            xinshui.Bianhao1 = textBox1.Text;
            xinshui.Liansai1 = textBox2.Text;
            xinshui.Zhudui1 = textBox3.Text;
            xinshui.Rangqiushu1 = textBox4.Text;
            xinshui.Kedui1 = textBox5.Text;            
            xinshui.Result1 = textBox6.Text;

            
            xinshui.Bianhao2 = textBox7.Text;
            xinshui.Liansai2 = textBox8.Text;
            xinshui.Zhudui2 = textBox9.Text;
            xinshui.Rangqiushu2 = textBox10.Text;
            xinshui.Kedui2 = textBox11.Text;
            xinshui.Result2 = textBox12.Text;

            xinshui.OperateTime = DateTime.Now;
            xinshui.Operator = "吴林";
            xinshui.Lucky = -1;
            xinshui.Exclude = 0;
            

            new XinshuiDAL().InsertXinshui(xinshui);
            BindGrid();
            MessageBox.Show("添加成功！");
        }

        private void BindGrid()
        {
           DataSet ds = new XinshuiDAL().GetAllXinshui();
           dataGridView1.DataSource = ds.Tables[0];

           foreach (DataGridViewRow row in dataGridView1.Rows)
           {
               if (row.Index == dataGridView1.Rows.Count - 1)
               {
                   continue;
               }
               if (row.Cells["exclude"].Value.ToString() == "1")
               {
                   row.DefaultCellStyle.BackColor = Color.Red;
               }
           }
        }

        private void btnTouzhu_Click(object sender, EventArgs e)
        {
            XinshuiDAL dal = new XinshuiDAL();
            double shouyi = Convert.ToDouble(textBox13.Text);
            double needtowin = shouyi;
            //从头开始算
            DataSet dsXinshui = new XinshuiDAL().GetValidXinshui();
            foreach(DataRow row in dsXinshui.Tables[0].Rows){
                int beishu = Convert.ToInt32(row["beishu"]);
                //如果倍数为0，说明该条记录未开奖过
                if (beishu == 0)
                {
                    //如果为空，说明当前行没有进行过投注，需要投注，投注需要获取赔率，获取以前行的投注记录
                    string bianhao1 = row["bianhao1"].ToString();
                    string bianhao2 = row["bianhao2"].ToString();
                    string rangqiushu1 = row["rangqiushu1"].ToString();
                    string rangqiushu2 = row["rangqiushu2"].ToString();
                    string result1 = row["result1"].ToString();
                    string result2 = row["result2"].ToString();
                    string riqi = row["riqi"].ToString();
                    int id = Convert.ToInt32(row["id"].ToString());
                    string realresult1 = "";
                    string realresult2 = "";
                    string realresultsp1 = "";
                    string realresultsp2 = "";
                    string touzhusp1 = "";
                    string touzhusp2 = "";
                    //获取赔率
                    DataSet ds = dal.GetPeilv(riqi, bianhao1);
                    DataSet ds2 = dal.GetPeilv(riqi, bianhao2);
                    List<double> peilv1 = new List<double>();
                    List<double> peilv2 = new List<double>();
                    foreach (DataRow peilvrow in ds.Tables[0].Rows)
                    {

                        string rangqiushu = peilvrow["rangqiu"].ToString();
                        if (rangqiushu == "0" && rangqiushu1 == "0")
                        {
                           
                            foreach (char ch in result1.ToCharArray())
                            {
                                string  spString = "0";
                                switch(ch)
                                {
                                    case '3': spString = peilvrow["shengsp"].ToString(); break;
                                    case '1': spString = peilvrow["pingsp"].ToString(); break;
                                    case '0': spString = peilvrow["fusp"].ToString(); break;
                                }
                                peilv1.Add(Convert.ToDouble(spString));
                            }
                           
                        }
                        else if (rangqiushu != "0" && rangqiushu1 != "0")
                        {
                            
                            foreach (char ch in result1.ToCharArray())
                            {
                                string spString = "0";
                                switch (ch)
                                {
                                    case '3': spString = peilvrow["shengsp"].ToString(); break;
                                    case '1': spString = peilvrow["pingsp"].ToString(); break;
                                    case '0': spString = peilvrow["fusp"].ToString(); break;
                                }
                                peilv1.Add(Convert.ToDouble(spString));
                            }
                           
                        }

                    }
                    foreach (DataRow peilvrow in ds2.Tables[0].Rows)
                    {
                        string rangqiushu = peilvrow["rangqiu"].ToString();
                        if (rangqiushu == "0" && rangqiushu2 == "0")
                        {
                            
                            foreach (char ch in result2.ToCharArray())
                            {
                                string spString = "0";
                                switch (ch)
                                {
                                    case '3': spString = peilvrow["shengsp"].ToString(); break;
                                    case '1': spString = peilvrow["pingsp"].ToString(); break;
                                    case '0': spString = peilvrow["fusp"].ToString(); break;
                                }
                                peilv2.Add(Convert.ToDouble(spString));
                            }
                        }
                        else if (rangqiushu != "0" && rangqiushu2 != "0")
                        {                          
                            
                            foreach (char ch in result2.ToCharArray())
                            {
                                string spString = "0";
                                switch (ch)
                                {
                                    case '3': spString = peilvrow["shengsp"].ToString(); break;
                                    case '1': spString = peilvrow["pingsp"].ToString(); break;
                                    case '0': spString = peilvrow["fusp"].ToString(); break;
                                }
                                peilv2.Add(Convert.ToDouble(spString));
                            }
                        }
                    }

                    double sp1 = peilv1.Min();
                    double sp2 = peilv2.Min();
                    touzhusp1 = sp1.ToString();
                    touzhusp2 = sp2.ToString();
                    //这里得修改为result的长度的乘积
                    //int needBeishu = (int)(shouyi /2/ (sp1 * sp2 - 1));
                    int needBeishu = (int)(shouyi / 2 / (sp1 * sp2 - peilv1.Count * peilv2.Count));
                    int touru = needBeishu * peilv1.Count * peilv2.Count * 2;
                    double jiangjin = 0;
                    
                    //获取开奖信息
                    DataSet kaijiang1 = dal.GetKaijiang(riqi, bianhao1);
                    DataSet kaijiang2 = dal.GetKaijiang(riqi, bianhao2);

                    //如果没有开奖信息则报异常，且退出程序
                    //这里需要修改一下，不是退出，而是算出一个倍数，然后提示
                    if (kaijiang1.Tables[0].Rows.Count == 0 || kaijiang2.Tables[0].Rows.Count == 0)
                    {
                        lblRiqi.Text = riqi;
                        lblBeishu.Text = needBeishu.ToString();
                        lblShouyi.Text = shouyi.ToString();
                        MessageBox.Show("日期为"+ riqi + "应该投注" + needBeishu + "倍，预期收益为" + shouyi);
                        //riqi
                        //MessageBox.Show("缺少" + riqi + "的开奖信息，请检查数据再操作！");
                        return;
                    }
                    //获取开奖信息
                    DataRow kaijiangrow1 = kaijiang1.Tables[0].Rows[0];
                    DataRow kaijiangrow2 = kaijiang2.Tables[0].Rows[0];
                    

                    bool zhongjiang1 = false;
                    bool zhongjiang2 = false;

                    if (rangqiushu1 != "0")
                    {
                        string rqspfresult1 = kaijiangrow1["rqspfresult"].ToString();
                        if(result1.IndexOf(rqspfresult1) != -1){
                            zhongjiang1 = true;
                        }
                        realresult1 = rqspfresult1;
                        realresultsp1 = kaijiangrow1["rqspfsp"].ToString();
                    }
                    else
                    {
                        string spfresult1 = kaijiangrow1["spfresult"].ToString();
                        if(result1.IndexOf(spfresult1) != -1){
                            zhongjiang1 = true;
                        }
                        realresult1 = spfresult1;
                        realresultsp1 = kaijiangrow1["spfsp"].ToString();
                    }

                    if (rangqiushu2 != "0")
                    {
                        string rqspfresult2 = kaijiangrow2["rqspfresult"].ToString();
                        if (result2.IndexOf(rqspfresult2) != -1)
                        {
                            zhongjiang2 = true;
                        }
                        realresult2 = rqspfresult2;
                        realresultsp2 = kaijiangrow2["rqspfsp"].ToString();
                    }
                    else
                    {
                        string spfresult2 = kaijiangrow2["spfresult"].ToString();
                        if (result2.IndexOf(spfresult2) != -1)
                        {
                            zhongjiang2 = true;
                        }
                        realresult2 = spfresult2;
                        realresultsp2 = kaijiangrow2["spfsp"].ToString();
                    }
                    //判断是否中奖，如果没有中奖则要加入收益，作为下一行的收益
                    int reallucky = 0;
                    if(zhongjiang1 && zhongjiang2)
                    {
                        reallucky = 1;
                    }
                    if (reallucky != 1)
                    {
                        shouyi = shouyi + touru;
                    }
                    else
                    {
                        //如果中奖，则收益reset为原始收益
                        shouyi = Convert.ToDouble(textBox13.Text);
                        jiangjin = touru * Convert.ToDouble(touzhusp1) * Convert.ToDouble(touzhusp2) /peilv1.Count /peilv2.Count;
                    }
                    
                    //更新当前行数据
                    Xinshui xinshui = new Xinshui();
                    xinshui.ID = id;
                    xinshui.Beishu = needBeishu;
                    xinshui.Touru = touru;
                    xinshui.Lucky = reallucky;

                    xinshui.Jiangjin = jiangjin;
                    xinshui.RealResult1 = realresult1;
                    xinshui.RealResult2 = realresult2;
                    xinshui.RealResultSp1 = realresultsp1;
                    xinshui.RealResultSp2 = realresultsp2;
                    xinshui.TouzhuSp1 = touzhusp1;
                    xinshui.TouzhuSp2 = touzhusp2;
                    dal.UpdateXinshui(xinshui);

                }
                else
                {
                    //如果倍数不为空，则说明已经处理过，只需要判断该条记录是否中奖，判断lucky标志，如果lucky为2，则终止，说明有异常数据
                    int lucky = Convert.ToInt32(row["lucky"].ToString());
                    if(lucky == 2 || lucky == -1){
                        MessageBox.Show("有异常数据，请检查数据再执行。");
                        return;
                    }
                    else if(lucky == 1)
                    {
                        shouyi = Convert.ToDouble(textBox13.Text);

                    }
                    else if(lucky == 0)
                    {
                        int realbeishu = Convert.ToInt32(row["beishu"].ToString());
                        string result1 = row["result1"].ToString();
                        string result2 = row["result2"].ToString();
                        int touru = realbeishu * result1.Length * result2.Length;
                        shouyi = shouyi + touru;
                    }
                }
                
                
            }
            MessageBox.Show("自动投注成功！");
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            string url = @"http://zx.aicai.com/zx/zc";
            string htmlData = GetHtmlFromUrl(url);

            htmlData = htmlData.Replace("</option>;","</option>");
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(htmlData);
            HtmlNode rootNode = document.DocumentNode;
            HtmlNode zjBackSelect = rootNode.SelectSingleNode(@"//div[@class='zjbackSelect']");
            //这里不知道为什么取不到innertext，只能换策略
            string selectText = zjBackSelect.InnerText;
            selectText = selectText.Substring(selectText.IndexOf("战绩回查") + 5).Trim();
            string[] options = selectText.Replace("2014"," 2014").Trim().Split(' ');
            //HtmlNodeCollection options = zjBackSelect.SelectNodes(@"./select/option[@selected='selected']");
            HtmlNode tjList = rootNode.SelectSingleNode(@"//div[@class='tjLis']");
            HtmlNodeCollection tables = tjList.SelectNodes(@"./table[@class='ke-zeroborder']");

            for (int i = 0; i < options.Length; i++)
            {
                String option = options[i];
                HtmlNode table = tables[i];

                //获取相关的记录插入心水推荐
                string riqi = DateTime.ParseExact(option, "yyyyMMdd", CultureInfo.CurrentCulture).ToString("yyyy-MM-dd");
                HtmlNodeCollection trs = table.SelectNodes(@"./tbody/tr");
                HtmlNode tr1 = trs[0];
                HtmlNode tr2 = trs[1];

                HtmlNodeCollection tds1 = tr1.SelectNodes(@"./td");
                HtmlNodeCollection tds2 = tr2.SelectNodes(@"./td");

                Xinshui xinshui = new Xinshui();
                xinshui.Riqi = riqi;
                string preBianhao1 = tds1[0].InnerText;

                xinshui.Bianhao1 = preBianhao1.Substring(preBianhao1.Length - 3);
                xinshui.Liansai1 = tds1[1].InnerText;
                xinshui.Zhudui1 = tds1[2].InnerText;
                xinshui.Rangqiushu1 = tds1[3].InnerText;
                xinshui.Kedui1 = tds1[4].InnerText;
                string preResult1 = tds1[5].InnerText;
                xinshui.Result1 = preResult1.Substring(preResult1.IndexOf("：") + 1);

                string preBianhao2 = tds2[0].InnerText;
                xinshui.Bianhao2 = preBianhao2.Substring(preBianhao2.Length - 3);
                xinshui.Liansai2 = tds2[1].InnerText;
                xinshui.Zhudui2 = tds2[2].InnerText;
                xinshui.Rangqiushu2 = tds2[3].InnerText;
                xinshui.Kedui2 = tds2[4].InnerText;
                string preResult2 = tds2[5].InnerText;
                xinshui.Result2 = preResult2.Substring(preResult2.IndexOf("：") + 1);

                xinshui.Exclude = 0;
                xinshui.Operator = "自动导入";
                xinshui.OperateTime = DateTime.Now;
               
                XinshuiDAL dal = new XinshuiDAL();

                //增加投注sp1和投注sp2的逻辑
                DataSet ds = dal.GetPeilv(riqi, xinshui.Bianhao1);
                DataSet ds2 = dal.GetPeilv(riqi, xinshui.Bianhao2);


                dal.InsertXinshuiWhenNotExist(xinshui);

            }
            MessageBox.Show("导入成功！");

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




        private void btnExclude_Click(object sender, EventArgs e)
        {
            XinshuiDAL dal = new XinshuiDAL();
            if(dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count > 0){
                int xinshuiID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                int exclude = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["exclude"].Value);
                if (exclude == 1)
                {
                    dal.ExcludeXinshui(xinshuiID, 0);
                }
                else
                {
                    dal.ExcludeXinshui(xinshuiID, 1);
                }
                
            }
            BindGrid();
            
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            XinshuiDAL dal = new XinshuiDAL();
            //if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count > 0)
            //{
            //    int xinshuiID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

            //    dal.ResetBeishu(xinshuiID);
            //}

            //处理多行数据
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                int xinshuiID = Convert.ToInt32(row.Cells[0].Value);
                dal.ResetBeishu(xinshuiID);
            }
            BindGrid();
        }

        //追加算法
        private void btnAddTouzhu_Click(object sender, EventArgs e)
        {
            XinshuiDAL dal = new XinshuiDAL();
            double shouyi = Convert.ToDouble(textBox13.Text);
            double needtowin = shouyi;
            int loseDays = 1;
            //从头开始算
            DataSet dsXinshui = new XinshuiDAL().GetValidXinshui();
            foreach (DataRow row in dsXinshui.Tables[0].Rows)
            {
                int beishu = Convert.ToInt32(row["beishu"]);
                //如果倍数为0，说明该条记录未开奖过
                if (beishu == 0)
                {
                    //如果为空，说明当前行没有进行过投注，需要投注，投注需要获取赔率，获取以前行的投注记录
                    string bianhao1 = row["bianhao1"].ToString();
                    string bianhao2 = row["bianhao2"].ToString();
                    string rangqiushu1 = row["rangqiushu1"].ToString();
                    string rangqiushu2 = row["rangqiushu2"].ToString();
                    string result1 = row["result1"].ToString();
                    string result2 = row["result2"].ToString();
                    string riqi = row["riqi"].ToString();
                    int id = Convert.ToInt32(row["id"].ToString());
                    string realresult1 = "";
                    string realresult2 = "";
                    string realresultsp1 = "";
                    string realresultsp2 = "";
                    string touzhusp1 = "";
                    string touzhusp2 = "";
                    //获取赔率
                    DataSet ds = dal.GetPeilv(riqi, bianhao1);
                    DataSet ds2 = dal.GetPeilv(riqi, bianhao2);
                    List<Double> peilv1 = new List<double>();
                    List<double> peilv2 = new List<double>();
                    foreach (DataRow peilvrow in ds.Tables[0].Rows)
                    {

                        string rangqiushu = peilvrow["rangqiu"].ToString();
                        if (rangqiushu == "0" && rangqiushu1 == "0")
                        {

                            foreach (char ch in result1.ToCharArray())
                            {
                                string spString = "0";
                                switch (ch)
                                {
                                    case '3': spString = peilvrow["shengsp"].ToString(); break;
                                    case '1': spString = peilvrow["pingsp"].ToString(); break;
                                    case '0': spString = peilvrow["fusp"].ToString(); break;
                                }
                                peilv1.Add(Convert.ToDouble(spString));
                            }

                        }
                        else if (rangqiushu != "0" && rangqiushu1 != "0")
                        {

                            foreach (char ch in result1.ToCharArray())
                            {
                                string spString = "0";
                                switch (ch)
                                {
                                    case '3': spString = peilvrow["shengsp"].ToString(); break;
                                    case '1': spString = peilvrow["pingsp"].ToString(); break;
                                    case '0': spString = peilvrow["fusp"].ToString(); break;
                                }
                                peilv1.Add(Convert.ToDouble(spString));
                            }

                        }

                    }
                    foreach (DataRow peilvrow in ds2.Tables[0].Rows)
                    {
                        string rangqiushu = peilvrow["rangqiu"].ToString();
                        if (rangqiushu == "0" && rangqiushu2 == "0")
                        {

                            foreach (char ch in result2.ToCharArray())
                            {
                                string spString = "0";
                                switch (ch)
                                {
                                    case '3': spString = peilvrow["shengsp"].ToString(); break;
                                    case '1': spString = peilvrow["pingsp"].ToString(); break;
                                    case '0': spString = peilvrow["fusp"].ToString(); break;
                                }
                                peilv2.Add(Convert.ToDouble(spString));
                            }
                        }
                        else if (rangqiushu != "0" && rangqiushu2 != "0")
                        {

                            foreach (char ch in result2.ToCharArray())
                            {
                                string spString = "0";
                                switch (ch)
                                {
                                    case '3': spString = peilvrow["shengsp"].ToString(); break;
                                    case '1': spString = peilvrow["pingsp"].ToString(); break;
                                    case '0': spString = peilvrow["fusp"].ToString(); break;
                                }
                                peilv2.Add(Convert.ToDouble(spString));
                            }
                        }
                    }

                    double sp1 = peilv1.Min();
                    double sp2 = peilv2.Min();
                    touzhusp1 = sp1.ToString();
                    touzhusp2 = sp2.ToString();
                    //int needBeishu = (int)(shouyi / 2 / (sp1 * sp2 - 1));
                    int needBeishu = (int)(shouyi / 2 / (sp1 * sp2 - peilv1.Count*peilv2.Count));
                    int touru = needBeishu * peilv1.Count * peilv2.Count * 2;
                    double jiangjin = 0;

                    //获取开奖信息
                    DataSet kaijiang1 = dal.GetKaijiang(riqi, bianhao1);
                    DataSet kaijiang2 = dal.GetKaijiang(riqi, bianhao2);

                    //如果没有开奖信息则报异常，且退出程序
                    //这里需要修改一下，不是退出，而是算出一个倍数，然后提示
                    if (kaijiang1.Tables[0].Rows.Count == 0 || kaijiang2.Tables[0].Rows.Count == 0)
                    {
                        lblRiqi.Text = riqi;
                        lblBeishu.Text = needBeishu.ToString();
                        lblShouyi.Text = shouyi.ToString();
                        MessageBox.Show("日期为" + riqi + "应该投注" + needBeishu + "倍，预期收益为" + shouyi);
                        //riqi
                        //MessageBox.Show("缺少" + riqi + "的开奖信息，请检查数据再操作！");
                        return;
                    }
                    //获取开奖信息
                    DataRow kaijiangrow1 = kaijiang1.Tables[0].Rows[0];
                    DataRow kaijiangrow2 = kaijiang2.Tables[0].Rows[0];


                    bool zhongjiang1 = false;
                    bool zhongjiang2 = false;

                    if (rangqiushu1 != "0")
                    {
                        string rqspfresult1 = kaijiangrow1["rqspfresult"].ToString();
                        if (result1.IndexOf(rqspfresult1) != -1)
                        {
                            zhongjiang1 = true;
                        }
                        realresult1 = rqspfresult1;
                        realresultsp1 = kaijiangrow1["rqspfsp"].ToString();
                    }
                    else
                    {
                        string spfresult1 = kaijiangrow1["spfresult"].ToString();
                        if (result1.IndexOf(spfresult1) != -1)
                        {
                            zhongjiang1 = true;
                        }
                        realresult1 = spfresult1;
                        realresultsp1 = kaijiangrow1["spfsp"].ToString();
                    }

                    if (rangqiushu2 != "0")
                    {
                        string rqspfresult2 = kaijiangrow2["rqspfresult"].ToString();
                        if (result2.IndexOf(rqspfresult2) != -1)
                        {
                            zhongjiang2 = true;
                        }
                        realresult2 = rqspfresult2;
                        realresultsp2 = kaijiangrow2["rqspfsp"].ToString();
                    }
                    else
                    {
                        string spfresult2 = kaijiangrow2["spfresult"].ToString();
                        if (result2.IndexOf(spfresult2) != -1)
                        {
                            zhongjiang2 = true;
                        }
                        realresult2 = spfresult2;
                        realresultsp2 = kaijiangrow2["spfsp"].ToString();
                    }
                    //判断是否中奖，如果没有中奖则要加入收益，作为下一行的收益
                    int reallucky = 0;
                    if (zhongjiang1 && zhongjiang2)
                    {
                        reallucky = 1;
                        
                    }
                    if (reallucky != 1)
                    {                        
                        shouyi = shouyi  + touru + needtowin;
                        loseDays = loseDays + 1;
                    }
                    else
                    {
                        //如果中奖，则收益reset为原始收益
                        shouyi = Convert.ToDouble(textBox13.Text);
                        loseDays = 1;
                        jiangjin = touru * Convert.ToDouble(touzhusp1) * Convert.ToDouble(touzhusp2);
                    }

                    //更新当前行数据
                    Xinshui xinshui = new Xinshui();
                    xinshui.ID = id;
                    xinshui.Beishu = needBeishu;
                    xinshui.Touru = touru;
                    xinshui.Lucky = reallucky;

                    xinshui.Jiangjin = jiangjin;
                    xinshui.RealResult1 = realresult1;
                    xinshui.RealResult2 = realresult2;
                    xinshui.RealResultSp1 = realresultsp1;
                    xinshui.RealResultSp2 = realresultsp2;
                    xinshui.TouzhuSp1 = touzhusp1;
                    xinshui.TouzhuSp2 = touzhusp2;
                    dal.UpdateXinshui(xinshui);

                }
                else
                {
                    //如果倍数不为空，则说明已经处理过，只需要判断该条记录是否中奖，判断lucky标志，如果lucky为2，则终止，说明有异常数据
                    int lucky = Convert.ToInt32(row["lucky"].ToString());
                    if (lucky == 2 || lucky == -1)
                    {
                        MessageBox.Show("有异常数据，请检查数据再执行。");
                        return;
                    }
                    else if (lucky == 1)
                    {
                        shouyi = Convert.ToDouble(textBox13.Text);
                        

                    }
                    else if (lucky == 0)
                    {
                        int realbeishu = Convert.ToInt32(row["beishu"].ToString());
                        string result1 = row["result1"].ToString();
                        string result2 = row["result2"].ToString();
                        int touru = realbeishu * result1.Length * result2.Length;
                        shouyi = shouyi + touru;
                    }
                }


            }
            MessageBox.Show("追加算法投注成功！");
        }

        private void btnZhishuSuanfa_Click(object sender, EventArgs e)
        {
            XinshuiDAL dal = new XinshuiDAL();
            double shouyi = Convert.ToDouble(textBox13.Text);
            double needtowin = shouyi;
            int loseDays = 1;
            //从头开始算
            DataSet dsXinshui = new XinshuiDAL().GetValidXinshui();
            foreach (DataRow row in dsXinshui.Tables[0].Rows)
            {
                int beishu = Convert.ToInt32(row["beishu"]);
                //如果倍数为0，说明该条记录未开奖过
                if (beishu == 0)
                {
                    //如果为空，说明当前行没有进行过投注，需要投注，投注需要获取赔率，获取以前行的投注记录
                    string bianhao1 = row["bianhao1"].ToString();
                    string bianhao2 = row["bianhao2"].ToString();
                    string rangqiushu1 = row["rangqiushu1"].ToString();
                    string rangqiushu2 = row["rangqiushu2"].ToString();
                    string result1 = row["result1"].ToString();
                    string result2 = row["result2"].ToString();
                    string riqi = row["riqi"].ToString();
                    int id = Convert.ToInt32(row["id"].ToString());
                    string realresult1 = "";
                    string realresult2 = "";
                    string realresultsp1 = "";
                    string realresultsp2 = "";
                    string touzhusp1 = "";
                    string touzhusp2 = "";
                    //获取赔率
                    DataSet ds = dal.GetPeilv(riqi, bianhao1);
                    DataSet ds2 = dal.GetPeilv(riqi, bianhao2);
                    List<Double> peilv1 = new List<double>();
                    List<double> peilv2 = new List<double>();
                    foreach (DataRow peilvrow in ds.Tables[0].Rows)
                    {

                        string rangqiushu = peilvrow["rangqiu"].ToString();
                        if (rangqiushu == "0" && rangqiushu1 == "0")
                        {

                            foreach (char ch in result1.ToCharArray())
                            {
                                string spString = "0";
                                switch (ch)
                                {
                                    case '3': spString = peilvrow["shengsp"].ToString(); break;
                                    case '1': spString = peilvrow["pingsp"].ToString(); break;
                                    case '0': spString = peilvrow["fusp"].ToString(); break;
                                }
                                peilv1.Add(Convert.ToDouble(spString));
                            }

                        }
                        else if (rangqiushu != "0" && rangqiushu1 != "0")
                        {

                            foreach (char ch in result1.ToCharArray())
                            {
                                string spString = "0";
                                switch (ch)
                                {
                                    case '3': spString = peilvrow["shengsp"].ToString(); break;
                                    case '1': spString = peilvrow["pingsp"].ToString(); break;
                                    case '0': spString = peilvrow["fusp"].ToString(); break;
                                }
                                peilv1.Add(Convert.ToDouble(spString));
                            }

                        }

                    }
                    foreach (DataRow peilvrow in ds2.Tables[0].Rows)
                    {
                        string rangqiushu = peilvrow["rangqiu"].ToString();
                        if (rangqiushu == "0" && rangqiushu2 == "0")
                        {

                            foreach (char ch in result2.ToCharArray())
                            {
                                string spString = "0";
                                switch (ch)
                                {
                                    case '3': spString = peilvrow["shengsp"].ToString(); break;
                                    case '1': spString = peilvrow["pingsp"].ToString(); break;
                                    case '0': spString = peilvrow["fusp"].ToString(); break;
                                }
                                peilv2.Add(Convert.ToDouble(spString));
                            }
                        }
                        else if (rangqiushu != "0" && rangqiushu2 != "0")
                        {

                            foreach (char ch in result2.ToCharArray())
                            {
                                string spString = "0";
                                switch (ch)
                                {
                                    case '3': spString = peilvrow["shengsp"].ToString(); break;
                                    case '1': spString = peilvrow["pingsp"].ToString(); break;
                                    case '0': spString = peilvrow["fusp"].ToString(); break;
                                }
                                peilv2.Add(Convert.ToDouble(spString));
                            }
                        }
                    }

                    double sp1 = peilv1.Min();
                    double sp2 = peilv2.Min();
                    touzhusp1 = sp1.ToString();
                    touzhusp2 = sp2.ToString();
                    //int needBeishu = (int)(shouyi / 2 / (sp1 * sp2 - 1));
                    int needBeishu = (int)(shouyi / 2 / (sp1 * sp2 - peilv1.Count * peilv2.Count));
                    int touru = needBeishu * peilv1.Count * peilv2.Count * 2;
                    double jiangjin = 0;

                    //获取开奖信息
                    DataSet kaijiang1 = dal.GetKaijiang(riqi, bianhao1);
                    DataSet kaijiang2 = dal.GetKaijiang(riqi, bianhao2);

                    //如果没有开奖信息则报异常，且退出程序
                    //这里需要修改一下，不是退出，而是算出一个倍数，然后提示
                    if (kaijiang1.Tables[0].Rows.Count == 0 || kaijiang2.Tables[0].Rows.Count == 0)
                    {
                        lblRiqi.Text = riqi;
                        lblBeishu.Text = needBeishu.ToString();
                        lblShouyi.Text = shouyi.ToString();
                        MessageBox.Show("日期为" + riqi + "应该投注" + needBeishu + "倍，预期收益为" + shouyi);
                        //riqi
                        //MessageBox.Show("缺少" + riqi + "的开奖信息，请检查数据再操作！");
                        return;
                    }
                    //获取开奖信息
                    DataRow kaijiangrow1 = kaijiang1.Tables[0].Rows[0];
                    DataRow kaijiangrow2 = kaijiang2.Tables[0].Rows[0];


                    bool zhongjiang1 = false;
                    bool zhongjiang2 = false;

                    if (rangqiushu1 != "0")
                    {
                        string rqspfresult1 = kaijiangrow1["rqspfresult"].ToString();
                        if (result1.IndexOf(rqspfresult1) != -1)
                        {
                            zhongjiang1 = true;
                        }
                        realresult1 = rqspfresult1;
                        realresultsp1 = kaijiangrow1["rqspfsp"].ToString();
                    }
                    else
                    {
                        string spfresult1 = kaijiangrow1["spfresult"].ToString();
                        if (result1.IndexOf(spfresult1) != -1)
                        {
                            zhongjiang1 = true;
                        }
                        realresult1 = spfresult1;
                        realresultsp1 = kaijiangrow1["spfsp"].ToString();
                    }

                    if (rangqiushu2 != "0")
                    {
                        string rqspfresult2 = kaijiangrow2["rqspfresult"].ToString();
                        if (result2.IndexOf(rqspfresult2) != -1)
                        {
                            zhongjiang2 = true;
                        }
                        realresult2 = rqspfresult2;
                        realresultsp2 = kaijiangrow2["rqspfsp"].ToString();
                    }
                    else
                    {
                        string spfresult2 = kaijiangrow2["spfresult"].ToString();
                        if (result2.IndexOf(spfresult2) != -1)
                        {
                            zhongjiang2 = true;
                        }
                        realresult2 = spfresult2;
                        realresultsp2 = kaijiangrow2["spfsp"].ToString();
                    }
                    //判断是否中奖，如果没有中奖则要加入收益，作为下一行的收益
                    int reallucky = 0;
                    if (zhongjiang1 && zhongjiang2)
                    {
                        reallucky = 1;

                    }
                    if (reallucky != 1)
                    {
                        loseDays = loseDays + 1;
                        shouyi = shouyi + touru + needtowin* Math.Pow(2,loseDays-1);
                        
                    }
                    else
                    {
                        //如果中奖，则收益reset为原始收益
                        shouyi = Convert.ToDouble(textBox13.Text);
                        loseDays = 1;
                        jiangjin = touru * Convert.ToDouble(touzhusp1) * Convert.ToDouble(touzhusp2);
                    }

                    //更新当前行数据
                    Xinshui xinshui = new Xinshui();
                    xinshui.ID = id;
                    xinshui.Beishu = needBeishu;
                    xinshui.Touru = touru;
                    xinshui.Lucky = reallucky;

                    xinshui.Jiangjin = jiangjin;
                    xinshui.RealResult1 = realresult1;
                    xinshui.RealResult2 = realresult2;
                    xinshui.RealResultSp1 = realresultsp1;
                    xinshui.RealResultSp2 = realresultsp2;
                    xinshui.TouzhuSp1 = touzhusp1;
                    xinshui.TouzhuSp2 = touzhusp2;
                    dal.UpdateXinshui(xinshui);

                }
                else
                {
                    //如果倍数不为空，则说明已经处理过，只需要判断该条记录是否中奖，判断lucky标志，如果lucky为2，则终止，说明有异常数据
                    int lucky = Convert.ToInt32(row["lucky"].ToString());
                    if (lucky == 2 || lucky == -1)
                    {
                        MessageBox.Show("有异常数据，请检查数据再执行。");
                        return;
                    }
                    else if (lucky == 1)
                    {
                        shouyi = Convert.ToDouble(textBox13.Text);
                        loseDays = 1;

                    }
                    else if (lucky == 0)
                    {
                        loseDays = loseDays + 1;
                        int realbeishu = Convert.ToInt32(row["beishu"].ToString());
                        string result1 = row["result1"].ToString();
                        string result2 = row["result2"].ToString();
                        int touru = realbeishu * result1.Length * result2.Length * 2;
                        shouyi = shouyi + touru + needtowin * Math.Pow(2, loseDays - 1);
                    }
                }


            }
            MessageBox.Show("指数算法投注成功！");
        }

        private string GetZhongwenResult(string result)
        {
            switch (result)
            {
                case "3": return "胜"; 
                case "1": return "平"; 
                case "0": return "负"; 
            }
            return "未知";
        }
        private void btnXiazhu_Click(object sender, EventArgs e)
        {
            long batchid = Convert.ToInt64(DateTime.Now.ToString("MMddhhmmss") + DateTime.Now.Millisecond);
            //二串一
            string zhudui = "";
            string kedui = "";
            string riqi = "";
            string rangqiu = "";
            string peilv = "";
            string result = "";
            string rangqiu1 = "";
            string rangqiu2 = "";
            string bianhao1 = "";
            string bianhao2 = "";
            DataGridViewRow touzhurow = null;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if(row.Cells["beishu"].Value.ToString() == "0")
                {
                    touzhurow = row;
                    zhudui = row.Cells["zhudui1"].Value.ToString() + "," + row.Cells["zhudui2"].Value.ToString();
                    kedui = row.Cells["kedui1"].Value.ToString() + "," + row.Cells["kedui2"].Value.ToString();
                    riqi = row.Cells["riqi"].Value.ToString() + "," + row.Cells["riqi"].Value.ToString();
                    rangqiu = row.Cells["rangqiushu1"].Value.ToString() +"," +row.Cells["rangqiushu2"].Value.ToString();
                    //peilv = row.Cells["touzhusp1"].Value.ToString() + "," + row.Cells["touzhusp2"].Value.ToString();
                    rangqiu1 = row.Cells["rangqiushu1"].Value.ToString();
                    rangqiu2 = row.Cells["rangqiushu2"].Value.ToString();

                    string result1= row.Cells["result1"].Value.ToString();
                    string result2 = row.Cells["result2"].Value.ToString();
                    foreach (char ch1 in result1.ToCharArray())
                    {
                        foreach (char ch2 in result2.ToCharArray())
                        {
                            double peilv1 = GetPeilv(bianhao1, riqi, rangqiu1, ch1.ToString());
                            double peilv2 = GetPeilv(bianhao2, riqi, rangqiu2, ch2.ToString());
                            peilv = peilv1 + "," + peilv2;
                            result = GetZhongwenResult(ch1.ToString()) + "," + GetZhongwenResult(ch2.ToString());

                            TouzhuSpf touspf = new TouzhuSpf();
                            touspf.Beishu = Convert.ToInt32(lblBeishu.Text);
                            touspf.Lucky = -1; //-1表示未验证是否中奖
                            touspf.Operator = "吴林";
                            touspf.OperateTime = DateTime.Now;
                            touspf.Jiangjin = "0";
                            touspf.Zhudui = zhudui;
                            touspf.Kedui = kedui;
                            touspf.Result = result;
                            touspf.Peilv = peilv;
                            touspf.Rangqiu = rangqiu;
                            touspf.Riqi = riqi;

                            touspf.BatchID = batchid;
                            new TouzhuSpfDAL().InsertTouzhuSpf(touspf);
                        }
                    }
                    
                    break;
                }
            }
            if (touzhurow != null)
            {
                //TouzhuSpf touspf = new TouzhuSpf();
                //touspf.Beishu = Convert.ToInt32(lblBeishu.Text);
                //touspf.Lucky = -1; //-1表示未验证是否中奖
                //touspf.Operator = "吴林";
                //touspf.OperateTime = DateTime.Now;
                //touspf.Jiangjin = "0";
                //touspf.Zhudui = zhudui;
                //touspf.Kedui = kedui;
                //touspf.Result = result;
                //touspf.Peilv = peilv;
                //touspf.Rangqiu = rangqiu;
                //touspf.Riqi = riqi;

                //touspf.BatchID = batchid;
                //new TouzhuSpfDAL().InsertTouzhuSpf(touspf);

                //这里插入income数据
                Income income = new Income();
                income.Amount = Convert.ToDouble(lblBeishu.Text) * touzhurow.Cells["result1"].Value.ToString().Length *touzhurow.Cells["result2"].Value.ToString().Length * 2;
                income.IncomeType = "购奖";
                income.OperateTime = DateTime.Now;
                income.Operator = "吴林";
                income.TouzhuType = "竞彩足球";
                income.TouzhuID = batchid;
                new IncomeDAL().InsertIncome(income);
                MessageBox.Show("投注成功！");
            }
            else
            {
                MessageBox.Show("没有需要投注的比赛！");
            }
            
        }

        private double GetPeilv(string bianhao,string riqi,string rangqiu,string result)
        {
            double peilv = 0.0;
            DataRow dataRow = null;
            DataSet ds = new XinshuiDAL().GetPeilv(riqi,bianhao);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (rangqiu == "0" && row["rangqiu"].ToString() == "0")
                {
                    dataRow = row;
                }
                if (rangqiu != "0" && row["rangqiu"].ToString() != "0")
                {
                    dataRow = row;
                }
            }
                if(dataRow!= null){
                    switch(result){
                        case "3": peilv = Convert.ToDouble(dataRow["ShengSp"]); break;
                        case "1": peilv = Convert.ToDouble(dataRow["PingSp"]); break;
                        case "0": peilv = Convert.ToDouble(dataRow["FuSp"]); break;
                    }
                }
                return peilv;
        }
        
    }
}
