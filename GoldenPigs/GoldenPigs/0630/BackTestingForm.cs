using GoldenPigs.DAL;
using GoldenPigs.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace GoldenPigs._0630
{
    public partial class BackTestingForm : Form
    {
        public BackTestingForm()
        {
            InitializeComponent();
        }

        private void btnStrategy1_Click(object sender, EventArgs e)
        {
            //回测策略1：
            //选出一定牛里面让球数为0，且预测结果为3的比赛，每天找出赔率最接近某个常数（比如1.5）进行投注
            //每天的收益预期是100，算出最大回撤

            //1.获取比赛
            DataTable dtStrategy = StrategyDAL.GetStrategy1Data();

            List<MatchShouyi> matches = new List<MatchShouyi>();

            string preriqi = "";
            string curriqi = "";
            double basepeilv = Convert.ToDouble(txtBasePeilv.Text);

            double currentMoney = 0.0;

            double maxHuiche = 0.0;
            double totalshouyi = 0.0;
            double staticshouyi = 100.0;
            


            double bestPeilv = 0.0;
            string bestLucky = "";
            preriqi = dtStrategy.Rows[0]["riqi"].ToString();

            int maxFailed = 0;


            int continuedFailed = 0;

            int maxSuccess = 0;
            int continuedSuccess = 0;

            string bestriqi = "";
            string bestbianhao = "";
            foreach(DataRow row in dtStrategy.Rows)
            {
                

                string riqi = row["riqi"].ToString();
                double shengsp = Convert.ToDouble(row["shengsp"]);
                string lucky = row["lucky"].ToString();
                string bianhao = row["bianhao"].ToString();

                curriqi = riqi;

                if (curriqi == preriqi)
                {
                    double diff = Math.Abs(shengsp - basepeilv);
                    double diff2 = Math.Abs(bestPeilv - basepeilv);
                    if (diff < diff2)
                    {
                        bestPeilv = shengsp;
                        bestLucky = lucky;
                        bestriqi = riqi;
                        bestbianhao = bianhao;
                    }

                }
                else
                {
                    totalshouyi = totalshouyi + 100;

                    double touru = totalshouyi / (bestPeilv - 1);
                    currentMoney -= touru;
                    if (currentMoney < maxHuiche)
                    {
                        maxHuiche = currentMoney;
                    }
                    if (bestLucky == "1")
                    {
                        currentMoney = touru * bestPeilv + currentMoney;
                        totalshouyi = 0.0;
                        continuedFailed = 0;

                        continuedSuccess++;
                        if (continuedSuccess > maxSuccess)
                        {
                            maxSuccess = continuedSuccess;

                        }

                    }else
                    {
                        totalshouyi += touru;
                        continuedFailed++;
                        if (continuedFailed > maxFailed)
                        {
                            maxFailed = continuedFailed;

                        }

                        continuedSuccess = 0;
                    }

                    MatchShouyi match = new MatchShouyi();
                    match.riqi = bestriqi;
                    match.bianhao = bestbianhao;
                    match.lucky = bestLucky;
                    match.shouyi = currentMoney;
                    match.totaltouru = touru;
                    match.totalprize = bestPeilv;
                    //match.huiche = match.shouyi - match.totaltouru;

                    matches.Add(match);

                    //重置为每一天的第一行数据
                    bestPeilv = shengsp;
                    bestLucky = lucky;
                    bestriqi = riqi;
                    bestbianhao = bianhao;

                }

                preriqi = riqi;
            
            }
            //计算一下回撤
            for (int i = 1; i < matches.Count; i++)
            {
                MatchShouyi match = matches[i];
                MatchShouyi preMatch = matches[i - 1];
                match.huiche = preMatch.shouyi - match.totaltouru;
            }
            dataGridView1.DataSource = matches;
            PrintPicture(matches);
            lblResult.Text = "最终的收益为" + currentMoney + "最大回撤为" + maxHuiche + "最大连续失败次数" + maxFailed + "最大连续成功次数" + maxSuccess;
            //MessageBox.Show("最终的收益为" + currentMoney + "最大回撤为" + maxHuiche + "最大连续失败次数" + maxFailed + "最大连续成功次数" + maxSuccess);

        }

        private void btnStrategy2_Click(object sender, EventArgs e)
        {
            //回测策略1：
            //选出一定牛里面让球数为0，且预测结果为3的比赛，每天找出赔率最接近某个常数（比如1.5）进行投注
            //每天的收益预期是100，算出最大回撤

            //1.获取比赛
            DataTable dtStrategy = StrategyDAL.GetStrategy1Data();

            List<MatchShouyi> matches = new List<MatchShouyi>();

            string preriqi = "";
            string curriqi = "";
            double basepeilv = Convert.ToDouble(txtBasePeilv.Text);
            double thresholdPeilv = Convert.ToDouble(txtThresholdPeilv.Text);

            double currentMoney = 0.0;

            double maxHuiche = 0.0;
            double totalshouyi = 0.0;
            double staticshouyi = 100.0;



            double bestPeilv = 0.0;
            string bestLucky = "";
            preriqi = "";

            int maxFailed = 0;


            int continuedFailed = 0;

            int maxSuccess = 0;
            int continuedSuccess = 0;

            string bestriqi = "";
            string bestbianhao = "";
            foreach (DataRow row in dtStrategy.Rows)
            {

               
                string riqi = row["riqi"].ToString();
                double shengsp = Convert.ToDouble(row["shengsp"]);
                string lucky = row["lucky"].ToString();
                string bianhao = row["bianhao"].ToString();

                if (shengsp < thresholdPeilv)
                {
                    
                    //preriqi = riqi;
                    continue;
                }
                else
                {
                    if (preriqi == "")
                    {
                        preriqi = riqi;
                    }
                }
               
                curriqi = riqi;

                if (curriqi == preriqi)
                {
                    double diff = Math.Abs(shengsp - basepeilv);
                    double diff2 = Math.Abs(bestPeilv - basepeilv);
                    if (diff < diff2)
                    {
                        bestPeilv = shengsp;
                        bestLucky = lucky;
                        bestriqi = riqi;
                        bestbianhao = bianhao;
                    }

                }
                else
                {
                    totalshouyi = totalshouyi + 100;

                    double touru = totalshouyi / (bestPeilv - 1);
                    currentMoney -= touru;
                    if (currentMoney < maxHuiche)
                    {
                        maxHuiche = currentMoney;
                    }
                    if (bestLucky == "1")
                    {
                        currentMoney = touru * bestPeilv + currentMoney;
                        totalshouyi = 0.0;
                        continuedFailed = 0;

                        continuedSuccess++;
                        if (continuedSuccess > maxSuccess)
                        {
                            maxSuccess = continuedSuccess;

                        }

                    }
                    else
                    {
                        totalshouyi += touru;
                        continuedFailed++;
                        if (continuedFailed > maxFailed)
                        {
                            maxFailed = continuedFailed;

                        }

                        continuedSuccess = 0;
                    }

                    MatchShouyi match = new MatchShouyi();
                    match.riqi = bestriqi;
                    match.bianhao = bestbianhao;
                    match.lucky = bestLucky;
                    match.shouyi = currentMoney;
                    match.totaltouru = touru;
                    match.totalprize = bestPeilv;
                    //match.huiche = match.shouyi - match.totaltouru;
                    matches.Add(match);

                    //重置为每一天的第一行数据
                    bestPeilv = shengsp;
                    bestLucky = lucky;
                    bestriqi = riqi;
                    bestbianhao = bianhao;

                }

                preriqi = riqi;

            }
            //计算一下回撤
            for (int i = 1; i < matches.Count; i++)
            {
                MatchShouyi match = matches[i];
                MatchShouyi preMatch = matches[i - 1];
                match.huiche = preMatch.shouyi - match.totaltouru;
            }
            dataGridView1.DataSource = matches;
            PrintPicture(matches);
            lblResult.Text = "最终的收益为" + currentMoney + "最大回撤为" + maxHuiche + "最大连续失败次数" + maxFailed + "最大连续成功次数" + maxSuccess;
            //MessageBox.Show("最终的收益为" + currentMoney + "最大回撤为" + maxHuiche + "最大连续失败次数" + maxFailed + "最大连续成功次数" + maxSuccess);

        }

        private void PrintPicture(List<MatchShouyi> matches)
        {
            zedGraphControl1.GraphPane.CurveList.Clear();
            GraphPane myPane = zedGraphControl1.GraphPane;
            // get a reference to the GraphPane  
            myPane.XAxis.Type = ZedGraph.AxisType.Date;
            // Set the Titles  
            myPane.Title.Text = "彩票收益曲线";
            myPane.XAxis.Title.Text = "时间";
            myPane.YAxis.Title.Text = "收益";

            // Make up some data arrays based on the Sine function  

            double x, y1, y2;


            PointPairList list1 = new PointPairList();

           

            // symbols, and "Porsche" in the legend  
            PointPairList list2 = new PointPairList();
            foreach (MatchShouyi match in matches)
            {
                list1.Add((double)new XDate(Convert.ToDateTime(match.riqi)), match.shouyi);
                list2.Add((double)new XDate(Convert.ToDateTime(match.riqi)), match.huiche);
            }
            
            LineItem myCurve = myPane.AddCurve("收益曲线", list1, Color.Red, SymbolType.None);
            LineItem myCurve2 = myPane.AddCurve("回撤曲线", list2, Color.Blue, SymbolType.None);

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();  
        }
    

        private void btnStrategy3_Click(object sender, EventArgs e)
        {
            //得加入风险控制

            //回测策略1：
            //选出一定牛里面让球数为0，且预测结果为3的比赛，每天找出赔率最接近某个常数（比如1.5）进行投注
            //每天的收益预期是100，算出最大回撤

            //把纯利的百分之三十拿出来实现快速盈利

            //1.获取比赛
            DataTable dtStrategy = StrategyDAL.GetStrategy1Data();
            List<MatchShouyi> matches = new List<MatchShouyi>();

            string preriqi = "";
            string curriqi = "";
            double basepeilv = Convert.ToDouble(txtBasePeilv.Text);
            double thresholdPeilv = Convert.ToDouble(txtThresholdPeilv.Text);
            double currentMoney = 10000.0;
            double maxHuiche = 0.0;
            double totalshouyi = 0.0;
            double staticshouyi = 100.0;
            double bestPeilv = 0.0;
            string bestLucky = "";
            preriqi = dtStrategy.Rows[0]["riqi"].ToString();
            int maxFailed = 0;
            int continuedFailed = 0;
            int maxSuccess = 0;
            int continuedSuccess = 0;

            string bestriqi = "";
            string bestbianhao = "";

            double addPercent = Convert.ToDouble(txtAddedPercent.Text);

            foreach (DataRow row in dtStrategy.Rows)
            {
                string riqi = row["riqi"].ToString();
                double shengsp = Convert.ToDouble(row["shengsp"]);
                string lucky = row["lucky"].ToString();
                string bianhao = row["bianhao"].ToString();

                if (shengsp < thresholdPeilv)
                {
                    continue;
                }
                curriqi = riqi;

                if (curriqi == preriqi)
                {
                    double diff = Math.Abs(shengsp - basepeilv);
                    double diff2 = Math.Abs(bestPeilv - basepeilv);
                    if (diff < diff2)
                    {
                        bestPeilv = shengsp;
                        bestLucky = lucky;
                        bestriqi = riqi;
                        bestbianhao = bianhao;
                    }

                }
                else
                {
                    totalshouyi = totalshouyi + 100;
                    //if( totalshouyi / (bestPeilv - 1) > currentMoney)
                    //{
                    //    totalshouyi = totalshouyi * 0.5;
                    //}

                    //if (currentMoney < -100)
                    //{
                    //    totalshouyi = currentMoney * - 1 ;
                    //}
                    double touru = totalshouyi / (bestPeilv - 1);
                    if (currentMoney > 0)
                    {
                        touru += currentMoney * addPercent;

                    }

                    currentMoney -= touru;                   

                    if (currentMoney < maxHuiche)
                    {
                        maxHuiche = currentMoney;
                    }
                    if (bestLucky == "1")
                    {
                        currentMoney = touru * bestPeilv + currentMoney;
                        totalshouyi = 0.0;
                        continuedFailed = 0;
                        continuedSuccess++;
                        if (continuedSuccess > maxSuccess)
                        {
                            maxSuccess = continuedSuccess;
                        }
                    }
                    else
                    {
                        totalshouyi += touru;

                        continuedFailed++;
                        if (continuedFailed > maxFailed)
                        {
                            maxFailed = continuedFailed;
                        }
                        continuedSuccess = 0;
                    }

                    MatchShouyi match = new MatchShouyi();
                    match.riqi = bestriqi;
                    match.bianhao = bestbianhao;
                    match.lucky = bestLucky;
                    match.shouyi = currentMoney;
                    match.totaltouru = touru;
                    match.totalprize = bestPeilv;
                    //match.huiche = match.shouyi - match.totaltouru;
                    matches.Add(match);

                    //重置为每一天的第一行数据
                    bestPeilv = shengsp;
                    bestLucky = lucky;
                    bestriqi = riqi;
                    bestbianhao = bianhao;

                }

                preriqi = riqi;

            }
            //计算一下回撤
            for (int i = 1; i < matches.Count; i++)
            {
                MatchShouyi match = matches[i];
                MatchShouyi preMatch = matches[i - 1];
                match.huiche = preMatch.shouyi - match.totaltouru;
            }
            dataGridView1.DataSource = matches;
            PrintPicture(matches);
            lblResult.Text = "最终的收益为" + currentMoney + "最大回撤为" + maxHuiche + "最大连续失败次数" + maxFailed + "最大连续成功次数" + maxSuccess;
            //MessageBox.Show("最终的收益为" + currentMoney + "最大回撤为" + maxHuiche + "最大连续失败次数" + maxFailed + "最大连续成功次数" + maxSuccess);

        }

        private void btnStrategy4_Click(object sender, EventArgs e)
        {
            //得加入风险控制

            //回测策略1：
            //选出一定牛里面让球数为0，且预测结果为3的比赛，每天找出赔率最接近某个常数（比如1.5）进行投注
            //每天的收益预期是100，算出最大回撤

            
            //把纯利的百分比拿出来实现快速盈利
            //引入投资周期的概念

            //盈利跟回撤有一个投资比，比如说3：1
            //或者说回撤一旦小于0，就结束该周期，开始新周期



            //1.获取比赛
            DataTable dtStrategy = StrategyDAL.GetStrategy1Data();
            List<MatchShouyi> matches = new List<MatchShouyi>();

            string preriqi = "";
            string curriqi = "";
            double basepeilv = Convert.ToDouble(txtBasePeilv.Text);
            double thresholdPeilv = Convert.ToDouble(txtThresholdPeilv.Text);
            double currentMoney = 1000.0;
            double maxHuiche = 0.0;
            double totalshouyi = 0.0;
            double staticshouyi = 100.0;
            double bestPeilv = 0.0;
            string bestLucky = "";
            preriqi = dtStrategy.Rows[0]["riqi"].ToString();
            int maxFailed = 0;
            int continuedFailed = 0;
            int maxSuccess = 0;
            int continuedSuccess = 0;

            string bestriqi = "";
            string bestbianhao = "";

            double addPercent = Convert.ToDouble(txtAddedPercent.Text);

            foreach (DataRow row in dtStrategy.Rows)
            {
                string riqi = row["riqi"].ToString();
                double shengsp = Convert.ToDouble(row["shengsp"]);
                string lucky = row["lucky"].ToString();
                string bianhao = row["bianhao"].ToString();

                if (shengsp < thresholdPeilv)
                {
                    continue;
                }
                curriqi = riqi;

                if (curriqi == preriqi)
                {
                    double diff = Math.Abs(shengsp - basepeilv);
                    double diff2 = Math.Abs(bestPeilv - basepeilv);
                    if (diff < diff2)
                    {
                        bestPeilv = shengsp;
                        bestLucky = lucky;
                        bestriqi = riqi;
                        bestbianhao = bianhao;
                    }

                }
                else
                {
                    totalshouyi = totalshouyi + 100;

                    double touru = totalshouyi / (bestPeilv - 1);
                    
                    if (currentMoney  - touru > 0)
                    {
                        touru += (currentMoney -touru) * addPercent;

                    }

                    currentMoney -= touru;

                    if (currentMoney < maxHuiche)
                    {
                        maxHuiche = currentMoney;
                    }
                    if (bestLucky == "1")
                    {
                        currentMoney = touru * bestPeilv + currentMoney;
                        totalshouyi = 0.0;
                        continuedFailed = 0;
                        continuedSuccess++;
                        if (continuedSuccess > maxSuccess)
                        {
                            maxSuccess = continuedSuccess;
                        }
                    }
                    else
                    {
                        totalshouyi += touru;

                        continuedFailed++;
                        if (continuedFailed > maxFailed)
                        {
                            maxFailed = continuedFailed;
                        }
                        continuedSuccess = 0;
                    }

                    MatchShouyi match = new MatchShouyi();
                    match.riqi = bestriqi;
                    match.bianhao = bestbianhao;
                    match.lucky = bestLucky;
                    match.shouyi = currentMoney;
                    match.totaltouru = touru;
                    match.totalprize = bestPeilv;
                    //match.huiche = match.shouyi - match.totaltouru;
                    matches.Add(match);

                    //重置为每一天的第一行数据
                    bestPeilv = shengsp;
                    bestLucky = lucky;
                    bestriqi = riqi;
                    bestbianhao = bianhao;

                }

                preriqi = riqi;

            }
            //计算一下回撤
            for (int i = 1; i < matches.Count; i++)
            {
                MatchShouyi match = matches[i];
                MatchShouyi preMatch = matches[i - 1];
                match.huiche = preMatch.shouyi - match.totaltouru;
            }
            dataGridView1.DataSource = matches;
            PrintPicture(matches);
            lblResult.Text = "最终的收益为" + currentMoney + "最大回撤为" + maxHuiche + "最大连续失败次数" + maxFailed + "最大连续成功次数" + maxSuccess;
            //MessageBox.Show("最终的收益为" + currentMoney + "最大回撤为" + maxHuiche + "最大连续失败次数" + maxFailed + "最大连续成功次数" + maxSuccess);

        }

        private void btnStrategy5_Click(object sender, EventArgs e)
        {
            //回测策略5，获取sp整数部分为15的比赛，然后每天随机选取一场比赛作为投注


            //1.获取比赛
            DataTable dtStrategy = StrategyDAL.GetStrategy5Data();
            List<MatchShouyi> matches = new List<MatchShouyi>();

            string preriqi = "";
            string curriqi = "";
            double basepeilv = Convert.ToDouble(txtBasePeilv.Text);
            double thresholdPeilv = Convert.ToDouble(txtThresholdPeilv.Text);
            double originalMoney = 10000.0;
            double currentMoney = 10000.0;
            double maxHuiche = 0.0;
            double totalshouyi = 0.0;
            double staticshouyi = 100.0;
            double bestPeilv = 0.0;
            string bestLucky = "";
            preriqi = dtStrategy.Rows[0]["riqi"].ToString();
            int maxFailed = 0;
            int continuedFailed = 0;
            int maxSuccess = 0;
            int continuedSuccess = 0;

            string bestriqi = "";
            string bestbianhao = "";

            double addPercent = Convert.ToDouble(txtAddedPercent.Text);
            List<FootMatch> dayMatches = new List<FootMatch>();


            foreach (DataRow row in dtStrategy.Rows)
            {
                string riqi = row["riqi"].ToString();
                double shengsp = Convert.ToDouble(row["shengsp"]);
                string lucky = "2";
                string spfresult = row["spfresult"].ToString();
                if (spfresult == "3")
                {
                    lucky = "1";
                }
                string bianhao = row["bianhao"].ToString();

                FootMatch curMatch = new FootMatch();
                curMatch.Riqi = riqi;
                curMatch.Bianhao = bianhao;
                curMatch.RealPeilv = shengsp;
                curMatch.Lucky = lucky;

                curriqi = riqi;

                if (curriqi == preriqi)
                {
                    dayMatches.Add(curMatch);

                    //double diff = Math.Abs(shengsp - basepeilv);
                    //double diff2 = Math.Abs(bestPeilv - basepeilv);
                    //if (diff < diff2)
                    //{
                    //    bestPeilv = shengsp;
                    //    bestLucky = lucky;
                    //    bestriqi = riqi;
                    //    bestbianhao = bianhao;
                    //}

                }
                else
                {
                    //选出代表这一天的比赛
                    //这个策略使用随机算法
                    Random rnd = new Random();
                    int matchCount = dayMatches.Count;

                    //a 第一种取随机
                    //int matchIndex = rnd.Next(matchCount);
                    //b 第二种取第一个
                    //int matchIndex = 0;
                    //c 第三种取最后一个
                    //int matchIndex = matchCount - 1;
                    //d 第四种取中间一个
                    int matchIndex = matchCount/2;

                    FootMatch selMatch = dayMatches[matchIndex];
                    bestPeilv = selMatch.RealPeilv;
                    bestriqi = selMatch.Riqi;
                    bestbianhao = selMatch.Bianhao;
                    bestLucky = selMatch.Lucky;

                    //double yuqiShouyi = 0.0;
                    //if (currentMoney < 600)
                    //{
                    //    yuqiShouyi = originalMoney + currentMoney * -1;
                    //}
                    //else
                    //{
                    //    yuqiShouyi = currentMoney * 0.3;
                    //}
                    double yuqiShouyi = 100;
                    totalshouyi = totalshouyi + yuqiShouyi;

                    
                    //totalshouyi = yuqiShouyi;
                    double touru = totalshouyi / (bestPeilv - 1);

                    if (currentMoney - touru > 0)
                    {
                        touru += (currentMoney - touru) * addPercent;

                    }

                    currentMoney -= touru;

                    if (currentMoney < maxHuiche)
                    {
                        maxHuiche = currentMoney;
                    }
                    if (bestLucky == "1")
                    {
                        currentMoney = touru * bestPeilv + currentMoney;
                        totalshouyi = 0.0;
                        continuedFailed = 0;
                        continuedSuccess++;
                        if (continuedSuccess > maxSuccess)
                        {
                            maxSuccess = continuedSuccess;
                        }
                    }
                    else
                    {
                        totalshouyi += touru;
                        continuedFailed++;
                        if (continuedFailed > maxFailed)
                        {
                            maxFailed = continuedFailed;
                        }
                        continuedSuccess = 0;
                    }

                    MatchShouyi match = new MatchShouyi();
                    match.riqi = bestriqi;
                    match.bianhao = bestbianhao;
                    match.lucky = bestLucky;
                    match.shouyi = currentMoney;
                    match.totaltouru = touru;
                    match.totalprize = bestPeilv;
                    //match.huiche = match.shouyi - match.totaltouru;
                    matches.Add(match);

                  

                    dayMatches = new List<FootMatch>();
                    dayMatches.Add(curMatch);
                }

                preriqi = riqi;

            }
            //计算一下回撤
            for (int i = 1; i < matches.Count; i++)
            {
                MatchShouyi match = matches[i];
                MatchShouyi preMatch = matches[i - 1];
                match.huiche = preMatch.shouyi - match.totaltouru;
            }
            dataGridView1.DataSource = matches;
            PrintPicture(matches);
            lblResult.Text = "最终的收益为" + currentMoney + "最大回撤为" + maxHuiche + "最大连续失败次数" + maxFailed + "最大连续成功次数" + maxSuccess;

        }

        private void btnStrategy6_Click(object sender, EventArgs e)
        {
            //回测策略6，获取sp整数部分为15的比赛，然后每天随机选取一场比赛作为投注
            //引入周期的概念

            //遇到没中奖，则重启一个新周期



            //获取比赛
            DataTable dtStrategy = StrategyDAL.GetStrategy5Data();
            List<MatchShouyi> matches = new List<MatchShouyi>();

            string preriqi = "";
            string curriqi = "";
            double basepeilv = Convert.ToDouble(txtBasePeilv.Text);
            double thresholdPeilv = Convert.ToDouble(txtThresholdPeilv.Text);
            double originalMoney = 1000.0;
            double currentMoney = 1000.0;

            double maxHuiche = 0.0;
            double totalshouyi = 0.0;
            double staticshouyi = 100.0;
            double bestPeilv = 0.0;
            string bestLucky = "";
            preriqi = dtStrategy.Rows[0]["riqi"].ToString();
            int maxFailed = 0;
            int continuedFailed = 0;
            int maxSuccess = 0;
            int continuedSuccess = 0;

            string bestriqi = "";
            string bestbianhao = "";

            double addPercent = Convert.ToDouble(txtAddedPercent.Text);
            List<FootMatch> dayMatches = new List<FootMatch>();


            foreach (DataRow row in dtStrategy.Rows)
            {
                string riqi = row["riqi"].ToString();
                double shengsp = Convert.ToDouble(row["shengsp"]);
                string lucky = "2";
                string spfresult = row["spfresult"].ToString();
                if (spfresult == "3")
                {
                    lucky = "1";
                }
                string bianhao = row["bianhao"].ToString();

                FootMatch curMatch = new FootMatch();
                curMatch.Riqi = riqi;
                curMatch.Bianhao = bianhao;
                curMatch.RealPeilv = shengsp;
                curMatch.Lucky = lucky;

                curriqi = riqi;

                if (curriqi == preriqi)
                {
                    dayMatches.Add(curMatch);
                }
                else
                {
                    //选出代表这一天的比赛
                    //这个策略使用随机算法
                    Random rnd = new Random();
                    int matchCount = dayMatches.Count;

                    //a 第一种取随机
                    //int matchIndex = rnd.Next(matchCount);
                    //b 第二种取第一个
                    //int matchIndex = 0;
                    //c 第三种取最后一个
                    //int matchIndex = matchCount - 1;
                    //d 第四种取中间一个
                    int matchIndex = matchCount / 2;

                    FootMatch selMatch = dayMatches[matchIndex];
                    bestPeilv = selMatch.RealPeilv;
                    bestriqi = selMatch.Riqi;
                    bestbianhao = selMatch.Bianhao;
                    bestLucky = selMatch.Lucky;

                    //double yuqiShouyi = 0.0;
                    //if (currentMoney < 600)
                    //{
                    //    yuqiShouyi = originalMoney + currentMoney * -1;
                    //}
                    //else
                    //{
                    //    yuqiShouyi = currentMoney * 0.3;
                    //}
                    double yuqiShouyi = 100;
                    totalshouyi = totalshouyi + yuqiShouyi;


                    //totalshouyi = yuqiShouyi;
                    double touru = totalshouyi / (bestPeilv - 1);

                    if (currentMoney - touru > 0)
                    {
                        touru += (currentMoney - touru) * addPercent;

                    }

                    currentMoney -= touru;

                    if (currentMoney < maxHuiche)
                    {
                        maxHuiche = currentMoney;
                    }
                    if (bestLucky == "1")
                    {
                        currentMoney = touru * bestPeilv + currentMoney;
                        totalshouyi = 0.0;
                        continuedFailed = 0;
                        continuedSuccess++;
                        if (continuedSuccess > maxSuccess)
                        {
                            maxSuccess = continuedSuccess;
                        }
                    }
                    else
                    {
                        totalshouyi += touru;
                        continuedFailed++;
                        if (continuedFailed > maxFailed)
                        {
                            maxFailed = continuedFailed;
                        }
                        continuedSuccess = 0;
                    }

                    MatchShouyi match = new MatchShouyi();
                    match.riqi = bestriqi;
                    match.bianhao = bestbianhao;
                    match.lucky = bestLucky;
                    match.shouyi = currentMoney;
                    match.totaltouru = touru;
                    match.totalprize = bestPeilv;
                    //match.huiche = match.shouyi - match.totaltouru;
                    matches.Add(match);



                    dayMatches = new List<FootMatch>();
                    dayMatches.Add(curMatch);
                }

                preriqi = riqi;

            }
            //计算一下回撤
            for (int i = 1; i < matches.Count; i++)
            {
                MatchShouyi match = matches[i];
                MatchShouyi preMatch = matches[i - 1];
                match.huiche = preMatch.shouyi - match.totaltouru;
            }
            dataGridView1.DataSource = matches;
            PrintPicture(matches);
            lblResult.Text = "最终的收益为" + currentMoney + "最大回撤为" + maxHuiche + "最大连续失败次数" + maxFailed + "最大连续成功次数" + maxSuccess;

        }

        private void btnStrategy7_Click(object sender, EventArgs e)
        {
            //在策略3的基础上，增加模式识别功能
            //回测策略5，获取sp整数部分为15的比赛，然后每天随机选取一场比赛作为投注


            //1.获取比赛
            DataTable dtStrategy = StrategyDAL.GetStrategy5Data();
            List<MatchShouyi> matches = new List<MatchShouyi>();

            string preriqi = "";
            string curriqi = "";
            double basepeilv = Convert.ToDouble(txtBasePeilv.Text);
            double thresholdPeilv = Convert.ToDouble(txtThresholdPeilv.Text);
            double originalMoney = 1000.0;
            double currentMoney = 1000.0;
            double maxHuiche = 0.0;
            double totalshouyi = 0.0;
            double staticshouyi = 100.0;
            double bestPeilv = 0.0;
            string bestLucky = "";
            preriqi = dtStrategy.Rows[0]["riqi"].ToString();
            int maxFailed = 0;
            int continuedFailed = 0;
            int maxSuccess = 0;
            int continuedSuccess = 3;

            string bestriqi = "";
            string bestbianhao = "";

            double addPercent = Convert.ToDouble(txtAddedPercent.Text);
            List<FootMatch> dayMatches = new List<FootMatch>();

            int maxWaitWin = 2;

            foreach (DataRow row in dtStrategy.Rows)
            {
                string riqi = row["riqi"].ToString();
                double shengsp = Convert.ToDouble(row["shengsp"]);
                string lucky = "2";
                string spfresult = row["spfresult"].ToString();
                if (spfresult == "3")
                {
                    lucky = "1";
                }
                string bianhao = row["bianhao"].ToString();

                FootMatch curMatch = new FootMatch();
                curMatch.Riqi = riqi;
                curMatch.Bianhao = bianhao;
                curMatch.RealPeilv = shengsp;
                curMatch.Lucky = lucky;

                curriqi = riqi;

                if (curriqi == preriqi)
                {
                    dayMatches.Add(curMatch);
                }
                else
                {
                    
                    //选出代表这一天的比赛
                    //这个策略使用随机算法
                    Random rnd = new Random();
                    int matchCount = dayMatches.Count;

                    //a 第一种取随机
                    //int matchIndex = rnd.Next(matchCount);
                    //b 第二种取第一个
                    //int matchIndex = 0;
                    //c 第三种取最后一个
                    //int matchIndex = matchCount - 1;
                    //d 第四种取中间一个
                    int matchIndex = matchCount / 2;

                    FootMatch selMatch = dayMatches[matchIndex];
                    bestPeilv = selMatch.RealPeilv;
                    bestriqi = selMatch.Riqi;
                    bestbianhao = selMatch.Bianhao;
                    bestLucky = selMatch.Lucky;

                    double touru = 0.0;
                    double yuqiShouyi = 100;
                    totalshouyi = totalshouyi + yuqiShouyi;

                    if (bestLucky == "1")
                    {
                        continuedSuccess++;
                        if (continuedSuccess > maxWaitWin)
                        {   
                            //totalshouyi = yuqiShouyi;
                            touru = totalshouyi / (bestPeilv - 1);

                            if (currentMoney - touru > 0)
                            {
                                touru += (currentMoney - touru) * addPercent;

                            }

                            currentMoney -= touru;

                            if (currentMoney < maxHuiche)
                            {
                                maxHuiche = currentMoney;
                            }
                            if (bestLucky == "1")
                            {

                                currentMoney = touru * bestPeilv + currentMoney;
                                totalshouyi = 0.0;
                                continuedFailed = 0;
                                //continuedSuccess++;
                                if (continuedSuccess > maxSuccess)
                                {
                                    maxSuccess = continuedSuccess;
                                }
                            }
                            else
                            {
                                totalshouyi += touru;
                                continuedFailed++;
                                if (continuedFailed > maxFailed)
                                {
                                    maxFailed = continuedFailed;
                                }
                                continuedSuccess = 0;
                            }

                            
                            MatchShouyi match = new MatchShouyi();
                            match.riqi = bestriqi;
                            match.bianhao = bestbianhao;
                            match.lucky = bestLucky;
                            match.shouyi = currentMoney;
                            match.totaltouru = touru;
                            match.totalprize = bestPeilv;
                            match.IsSkip = 0; //设置跳过符号

                            //match.huiche = match.shouyi - match.totaltouru;
                            matches.Add(match);
                        }
                        else 
                        {
                            //如果没有过3，则需要等等，跳过该场比赛
                            MatchShouyi match = new MatchShouyi();
                            match.riqi = bestriqi;
                            match.bianhao = bestbianhao;
                            match.lucky = bestLucky;
                            match.shouyi = currentMoney;
                            match.totaltouru = touru;
                            match.totalprize = bestPeilv;
                            match.IsSkip = 1; //设置跳过符号

                            //match.huiche = match.shouyi - match.totaltouru;
                            matches.Add(match);
                        }
                       

                        
                    }
                    else //if bestlucky = 1
                    {
                        if (continuedSuccess > maxWaitWin - 1)
                        {
                            
                            totalshouyi = totalshouyi + yuqiShouyi;


                            //totalshouyi = yuqiShouyi;
                            touru = totalshouyi / (bestPeilv - 1);

                            if (currentMoney - touru > 0)
                            {
                                touru += (currentMoney - touru) * addPercent;

                            }

                            currentMoney -= touru;

                            if (currentMoney < maxHuiche)
                            {
                                maxHuiche = currentMoney;
                            }
                            MatchShouyi match = new MatchShouyi();
                            match.riqi = bestriqi;
                            match.bianhao = bestbianhao;
                            match.lucky = bestLucky;
                            match.shouyi = currentMoney;
                            match.totaltouru = touru;
                            match.totalprize = bestPeilv;
                            //match.huiche = match.shouyi - match.totaltouru;
                            matches.Add(match);

                            totalshouyi = totalshouyi + touru;
                        }
                        else
                        {
                            MatchShouyi match = new MatchShouyi();
                            match.riqi = bestriqi;
                            match.bianhao = bestbianhao;
                            match.lucky = bestLucky;
                            match.shouyi = currentMoney;
                            match.totaltouru = touru;
                            match.totalprize = bestPeilv;
                            match.IsSkip = 1;
                            //match.huiche = match.shouyi - match.totaltouru;
                            matches.Add(match);
                        }
                        continuedSuccess = 0;
                        //continuedFailed++;
                        //if (continuedFailed > maxFailed)
                        //{
                        //    maxFailed = continuedFailed;
                        //}
                    }
                   
                    dayMatches = new List<FootMatch>();
                    dayMatches.Add(curMatch);
                }

                preriqi = riqi;

            }
            //计算一下回撤
            for (int i = 1; i < matches.Count; i++)
            {
                MatchShouyi match = matches[i];
                MatchShouyi preMatch = matches[i - 1];
                match.huiche = preMatch.shouyi - match.totaltouru;
            }
            dataGridView1.DataSource = matches;
            PrintPicture(matches);
            lblResult.Text = "最终的收益为" + currentMoney + "最大回撤为" + maxHuiche + "最大连续失败次数" + maxFailed + "最大连续成功次数" + maxSuccess;


        }

       

        private void btnStrategy8_Click(object sender, EventArgs e)
        {
            //在策略7的基础上，增加All in功能，当出现连续3个模式后可以执行all in,
            //注意：该策略可能有问题，不能all in就成负数了




        }

        private void btnStrategy9_Click(object sender, EventArgs e)
        {
            //策略9的取值逻辑发生变化，使用赢赔率是15，双选压注31，这样的总概率是82.03，
            //投注比例使用获利一样来计算


            DataTable dtStrategy = StrategyDAL.GetStrategy9Data();
            List<MatchShouyi> matches = new List<MatchShouyi>();

            List<FootMatch> dayMatches = new List<FootMatch>();

            string preriqi = dtStrategy.Rows[0]["riqi"].ToString() ;
            string curriqi = "";
            double maxHuiche = 0.0;
            double totalshouyi = 0.0;
            double staticshouyi = 100.0;
            double currentMoney = 0.0;

            int maxFailed = 0;
            int continuedFailed = 0;
            int maxSuccess = 0;
            int continuedSuccess = 0;

            foreach (DataRow row in dtStrategy.Rows)
            {
                string riqi = row["riqi"].ToString();
                double shengsp = Convert.ToDouble(row["shengsp"]);
                double pingsp = Convert.ToDouble(row["pingsp"]);
                string lucky = "2";
                string spfresult = row["spfresult"].ToString();
                if (spfresult == "3" || spfresult == "1")
                {
                    lucky = "1";
                }
                string bianhao = row["bianhao"].ToString();

                double realsp = shengsp * pingsp / (shengsp + pingsp);

              

                FootMatch curMatch = new FootMatch();
                curMatch.Riqi = riqi;
                curMatch.Bianhao = bianhao;
                curMatch.RealPeilv = realsp;
                curMatch.Lucky = lucky;

                curriqi = riqi;

                if (curriqi == preriqi)
                {
                    dayMatches.Add(curMatch);
                }
                else
                {
                    //选出代表这一天的比赛
                    //这个策略使用随机算法
                    Random rnd = new Random();
                    int matchCount = dayMatches.Count;

                    //a 第一种取随机
                    int matchIndex = rnd.Next(matchCount);
                    //b 第二种取第一个
                    //int matchIndex = 0;
                    //c 第三种取最后一个
                    //int matchIndex = matchCount - 1;
                    //d 第四种取中间一个
                    //int matchIndex = matchCount / 2;

                    FootMatch selMatch = dayMatches[matchIndex];


                    double yuqiShouyi = 100;
                    totalshouyi = totalshouyi + yuqiShouyi;


                    //totalshouyi = yuqiShouyi;
                    double touru = totalshouyi / (selMatch.RealPeilv - 1);

                    //if (currentMoney - touru > 0)
                    //{
                    //    touru += (currentMoney - touru) * addPercent;

                    //}

                    currentMoney -= touru;

                    if (currentMoney < maxHuiche)
                    {
                        maxHuiche = currentMoney;
                    }
                    if (selMatch.Lucky == "1")
                    {
                        currentMoney = touru * selMatch.RealPeilv + currentMoney;
                        totalshouyi = 0.0;
                        continuedFailed = 0;
                        continuedSuccess++;
                        if (continuedSuccess > maxSuccess)
                        {
                            maxSuccess = continuedSuccess;
                        }
                    }
                    else
                    {
                        totalshouyi += touru;
                        continuedFailed++;
                        if (continuedFailed > maxFailed)
                        {
                            maxFailed = continuedFailed;
                        }
                        continuedSuccess = 0;
                    }

                  


                    MatchShouyi match = new MatchShouyi();
                    match.riqi = selMatch.Riqi;
                    match.bianhao = selMatch.Bianhao;
                    match.lucky = selMatch.Lucky;
                    match.shouyi = currentMoney;
                    match.totaltouru = touru;
                    match.totalprize = selMatch.RealPeilv;
                    //match.huiche = match.shouyi - match.totaltouru;
                    matches.Add(match);



                    dayMatches = new List<FootMatch>();
                    dayMatches.Add(curMatch);
                }
                preriqi = riqi;
            }
            //计算回撤
            for (int i = 1; i < matches.Count; i++)
            {
                MatchShouyi match = matches[i];
                MatchShouyi preMatch = matches[i - 1];
                match.huiche = preMatch.shouyi - match.totaltouru;
            }
            dataGridView1.DataSource = matches;
            PrintPicture(matches);
            lblResult.Text = "最终的收益为" + currentMoney + "最大回撤为" + maxHuiche + "最大连续失败次数" + maxFailed + "最大连续成功次数" + maxSuccess;


        }

        private void btnStrategy10_Click(object sender, EventArgs e)
        {
            //策略9的取值逻辑发生变化，使用赢赔率是15，双选压注31，这样的总概率是82.03，
            //投注比例使用获利一样来计算,不在递加收入，
            //结果并不理想


            DataTable dtStrategy = StrategyDAL.GetStrategy9Data();
            List<MatchShouyi> matches = new List<MatchShouyi>();

            List<FootMatch> dayMatches = new List<FootMatch>();

            string preriqi = dtStrategy.Rows[0]["riqi"].ToString();
            string curriqi = "";
            double maxHuiche = 0.0;
            double totalshouyi = 0.0;
            double staticshouyi = 100.0;
            double currentMoney = 0.0;

            int maxFailed = 0;
            int continuedFailed = 0;
            int maxSuccess = 0;
            int continuedSuccess = 0;

            foreach (DataRow row in dtStrategy.Rows)
            {
                string riqi = row["riqi"].ToString();
                double shengsp = Convert.ToDouble(row["shengsp"]);
                double pingsp = Convert.ToDouble(row["pingsp"]);
                string lucky = "2";
                string spfresult = row["spfresult"].ToString();
                if (spfresult == "3" || spfresult == "1")
                {
                    lucky = "1";
                }
                string bianhao = row["bianhao"].ToString();

                double realsp = shengsp * pingsp / (shengsp + pingsp);



                FootMatch curMatch = new FootMatch();
                curMatch.Riqi = riqi;
                curMatch.Bianhao = bianhao;
                curMatch.RealPeilv = realsp;
                curMatch.Lucky = lucky;

                curriqi = riqi;

                if (curriqi == preriqi)
                {
                    dayMatches.Add(curMatch);
                }
                else
                {
                    //选出代表这一天的比赛
                    //这个策略使用随机算法
                    Random rnd = new Random();
                    int matchCount = dayMatches.Count;

                    //a 第一种取随机
                    //int matchIndex = rnd.Next(matchCount);
                    //b 第二种取第一个
                    //int matchIndex = 0;
                    //c 第三种取最后一个
                    //int matchIndex = matchCount - 1;
                    //d 第四种取中间一个
                    int matchIndex = matchCount / 2;

                    FootMatch selMatch = dayMatches[matchIndex];


                    double yuqiShouyi = 100;
                    //totalshouyi = totalshouyi + yuqiShouyi;
                    totalshouyi =  yuqiShouyi;

                    //totalshouyi = yuqiShouyi;
                    double touru = totalshouyi / (selMatch.RealPeilv - 1);

                    //if (currentMoney - touru > 0)
                    //{
                    //    touru += (currentMoney - touru) * addPercent;

                    //}

                    currentMoney -= touru;

                    if (currentMoney < maxHuiche)
                    {
                        maxHuiche = currentMoney;
                    }
                    if (selMatch.Lucky == "1")
                    {
                        currentMoney = touru * selMatch.RealPeilv + currentMoney;
                        totalshouyi = 0.0;
                        continuedFailed = 0;
                        continuedSuccess++;
                        if (continuedSuccess > maxSuccess)
                        {
                            maxSuccess = continuedSuccess;
                        }
                    }
                    else
                    {
                        totalshouyi += touru;
                        continuedFailed++;
                        if (continuedFailed > maxFailed)
                        {
                            maxFailed = continuedFailed;
                        }
                        continuedSuccess = 0;
                    }

                    MatchShouyi match = new MatchShouyi();
                    match.riqi = selMatch.Riqi;
                    match.bianhao = selMatch.Bianhao;
                    match.lucky = selMatch.Lucky;
                    match.shouyi = currentMoney;
                    match.totaltouru = touru;
                    match.totalprize = selMatch.RealPeilv;
                    //match.huiche = match.shouyi - match.totaltouru;
                    matches.Add(match);



                    dayMatches = new List<FootMatch>();
                    dayMatches.Add(curMatch);
                }
                preriqi = riqi;
            }
            //计算回撤
            for (int i = 1; i < matches.Count; i++)
            {
                MatchShouyi match = matches[i];
                MatchShouyi preMatch = matches[i - 1];
                match.huiche = preMatch.shouyi - match.totaltouru;
            }
            dataGridView1.DataSource = matches;
            PrintPicture(matches);
            lblResult.Text = "最终的收益为" + currentMoney + "最大回撤为" + maxHuiche + "最大连续失败次数" + maxFailed + "最大连续成功次数" + maxSuccess;

        }

        private void btnStrategy11_Click(object sender, EventArgs e)
        {
            //策略9的取值逻辑发生变化，使用赢赔率是15，双选压注31，这样的总概率是82.03，
            //投注比例使用低概率保本，高概率盈利模型


            DataTable dtStrategy = StrategyDAL.GetStrategy9Data();
            List<MatchShouyi> matches = new List<MatchShouyi>();

            List<FootMatch> dayMatches = new List<FootMatch>();

            string preriqi = dtStrategy.Rows[0]["riqi"].ToString();
            string curriqi = "";
            double maxHuiche = 0.0;
            double totalshouyi = 0.0;
            double staticshouyi = 100.0;
            double currentMoney = 0.0;

            int maxFailed = 0;
            int continuedFailed = 0;
            int maxSuccess = 0;
            int continuedSuccess = 0;

            int shengcount = 0;
            int pingcount = 0;
            int fucount = 0;
            double shengspsum = 0.0;
            double pingspsum = 0.0;
            double fuspsum = 0.0;


            foreach (DataRow row in dtStrategy.Rows)
            {
                string riqi = row["riqi"].ToString();
                double shengsp = Convert.ToDouble(row["shengsp"]);
                double pingsp = Convert.ToDouble(row["pingsp"]);
                double fusp = Convert.ToDouble(row["fusp"]);
                string lucky = "2";
                string spfresult = row["spfresult"].ToString();
                
                if (spfresult == "3" || spfresult == "1")
                {
                    lucky = "1";
                }
                string bianhao = row["bianhao"].ToString();

                double realsp = pingsp - pingsp/shengsp;                 
                    



                FootMatch curMatch = new FootMatch();
                curMatch.Riqi = riqi;
                curMatch.Bianhao = bianhao;
                curMatch.RealPeilv = realsp;
                curMatch.Lucky = spfresult;
                curMatch.PingPeilv = shengsp;
                curMatch.ShengPeilv = shengsp;
                curMatch.FuPeilv = fusp;

                curriqi = riqi;

                if (curriqi == preriqi)
                {
                    dayMatches.Add(curMatch);
                }
                else
                {
                    //选出代表这一天的比赛
                    //这个策略使用随机算法
                    Random rnd = new Random();
                    int matchCount = dayMatches.Count;

                    //a 第一种取随机
                    int matchIndex = rnd.Next(matchCount);
                    //b 第二种取第一个
                    //int matchIndex = 0;
                    //c 第三种取最后一个
                    //int matchIndex = matchCount - 1;
                    //d 第四种取中间一个
                    //int matchIndex = matchCount / 2;

                    FootMatch selMatch = dayMatches[matchIndex];


                    double yuqiShouyi = 300;
                    //totalshouyi = totalshouyi + yuqiShouyi;
                    totalshouyi = yuqiShouyi;
                    double touru = 300;

                    //totalshouyi = yuqiShouyi;
                    double tourudi = totalshouyi / selMatch.PingPeilv;
                    double tourugao = touru - tourudi;

                    //if (currentMoney - touru > 0)
                    //{
                    //    touru += (currentMoney - touru) * addPercent;

                    //}

                    currentMoney -= touru;

                    if (currentMoney < maxHuiche)
                    {
                        maxHuiche = currentMoney;
                    }
                    if (selMatch.Lucky == "3")
                    {
                        currentMoney = tourudi * selMatch.PingPeilv + currentMoney;
                        totalshouyi = 0.0;
                        continuedFailed = 0;
                        continuedSuccess++;
                        if (continuedSuccess > maxSuccess)
                        {
                            maxSuccess = continuedSuccess;
                        }
                        shengcount++;
                        shengspsum += selMatch.ShengPeilv;
                    }
                    else if (selMatch.Lucky == "1")
                    {
                        currentMoney = touru * selMatch.RealPeilv + currentMoney;
                        totalshouyi = 0.0;
                        continuedFailed = 0;
                        continuedSuccess++;
                        if (continuedSuccess > maxSuccess)
                        {
                            maxSuccess = continuedSuccess;
                        }
                        pingcount++;
                        pingspsum += selMatch.PingPeilv;
                    }
                    else
                    {
                        totalshouyi += touru;
                        continuedFailed++;
                        if (continuedFailed > maxFailed)
                        {
                            maxFailed = continuedFailed;
                        }
                        continuedSuccess = 0;

                        fucount++;
                        fuspsum += selMatch.FuPeilv;
                    }

                    MatchShouyi match = new MatchShouyi();
                    match.riqi = selMatch.Riqi;
                    match.bianhao = selMatch.Bianhao;
                    match.lucky = selMatch.Lucky;
                    match.shouyi = currentMoney;
                    match.totaltouru = touru;
                    match.totalprize = selMatch.RealPeilv;
                    //match.huiche = match.shouyi - match.totaltouru;
                    matches.Add(match);



                    dayMatches = new List<FootMatch>();
                    dayMatches.Add(curMatch);
                }
                preriqi = riqi;
            }
            //计算回撤
            for (int i = 1; i < matches.Count; i++)
            {
                MatchShouyi match = matches[i];
                MatchShouyi preMatch = matches[i - 1];
                match.huiche = preMatch.shouyi - match.totaltouru;
            }
            dataGridView1.DataSource = matches;
            PrintPicture(matches);
            lblResult.Text = "最终的收益为" + currentMoney + "最大回撤为" + maxHuiche + "最大连续失败次数" + maxFailed + "最大连续成功次数" + maxSuccess ;
            lblAdditionInfo.Text = "总的比赛数为：" + matches.Count + "次数" + shengcount + "," +pingcount+ "," +fucount+ "," + "胜sp和" + shengspsum + "," +pingspsum+ "," + fuspsum;

        }

        private void btnStrategy12_Click(object sender, EventArgs e)
        {
            //和策略2相同，只不过改进该算法，先分类
            
            //选出一定牛里面让球数为0，且预测结果为3的比赛，每天找出赔率最接近某个常数（比如1.5）进行投注
            //每天的收益预期是100，算出最大回撤

            //获取比赛
            DataTable dtStrategy = StrategyDAL.GetStrategy1Data();
            Dictionary<String, List<DataRow>> riqiDict = new Dictionary<string, List<DataRow>>();
            double thresholdPeilv = Convert.ToDouble(txtThresholdPeilv.Text);
            //按照日期分类
            foreach (DataRow row in dtStrategy.Rows)
            {
                String riqi = row["riqi"].ToString();
                //得先进行赔率的过滤
                double shengsp = Convert.ToDouble(row["shengsp"]);
                if (shengsp < thresholdPeilv)
                {
                    continue;
                }


                List<DataRow> rows;
                if (riqiDict.TryGetValue(riqi, out rows))
                {
                    rows.Add(row);
                }
                else
                {
                    rows = new List<DataRow>();
                    rows.Add(row);
                    riqiDict[riqi] = rows;
                }
            }

            List<MatchShouyi> matches = new List<MatchShouyi>();
            double basepeilv = Convert.ToDouble(txtBasePeilv.Text);
            //double thresholdPeilv = Convert.ToDouble(txtThresholdPeilv.Text);

            double currentMoney = 0.0;
            double maxHuiche = 0.0;
            double totalshouyi = 0.0;
            double staticshouyi = 100.0;
            double bestPeilv = 0.0;
            string bestLucky = "";

            int maxFailed = 0;
            int continuedFailed = 0;
            int maxSuccess = 0;
            int continuedSuccess = 0;

            string bestriqi = "";
            string bestbianhao = "";

            DataRow bestMatchRow;

            //MatchShouyi shouyi;

            foreach (KeyValuePair<string, List<DataRow>> keyvalue in riqiDict)
            {
                //shouyi = new MatchShouyi();
                //shouyi.riqi = keyvalue.Key;

                List<DataRow> rows = keyvalue.Value;
                DataRow firstRow = rows[0];

                bestriqi = firstRow["riqi"].ToString();
                bestPeilv = Convert.ToDouble(firstRow["shengsp"]);
                bestLucky = firstRow["lucky"].ToString();
                bestbianhao = firstRow["bianhao"].ToString();

                foreach (DataRow row in rows)
                {
                    string riqi = row["riqi"].ToString();
                    double shengsp = Convert.ToDouble(row["shengsp"]);
                    string lucky = row["lucky"].ToString();
                    string bianhao = row["bianhao"].ToString();

                    double diff = Math.Abs(shengsp - basepeilv);
                    double diff2 = Math.Abs(bestPeilv - basepeilv);

                    if (diff < diff2)
                    {
                        bestMatchRow = row;
                        bestPeilv = shengsp;
                        bestLucky = lucky;
                        bestriqi = riqi;
                        bestbianhao = bianhao;
                    }
                }

                //开始计算收益

                totalshouyi = totalshouyi + 100;

                double touru = totalshouyi / (bestPeilv - 1);
                currentMoney -= touru;
                if (currentMoney < maxHuiche)
                {
                    maxHuiche = currentMoney;
                }
                if (bestLucky == "1")
                {
                    currentMoney = touru * bestPeilv + currentMoney;
                    totalshouyi = 0.0;
                    continuedFailed = 0;

                    continuedSuccess++;
                    if (continuedSuccess > maxSuccess)
                    {
                        maxSuccess = continuedSuccess;

                    }

                }
                else
                {
                    totalshouyi += touru;
                    continuedFailed++;
                    if (continuedFailed > maxFailed)
                    {
                        maxFailed = continuedFailed;

                    }

                    continuedSuccess = 0;
                }

                MatchShouyi match = new MatchShouyi();
                match.riqi = bestriqi;
                match.bianhao = bestbianhao;
                match.lucky = bestLucky;
                match.shouyi = currentMoney;
                match.totaltouru = touru;
                match.totalprize = bestPeilv;
                //match.huiche = match.shouyi - match.totaltouru;
                matches.Add(match);

            } 
           
            //计算一下回撤
            for (int i = 1; i < matches.Count; i++)
            {
                MatchShouyi match = matches[i];
                MatchShouyi preMatch = matches[i - 1];
                match.huiche = preMatch.shouyi - match.totaltouru;
            }
            dataGridView1.DataSource = matches;
            PrintPicture(matches);
            lblResult.Text = "最终的收益为" + currentMoney + "最大回撤为" + maxHuiche + "最大连续失败次数" + maxFailed + "最大连续成功次数" + maxSuccess;
            
        }

        private void btnStrategy13_Click(object sender, EventArgs e)
        {
            //策略算法基本跟12一致，只不过去数据改用新的数据取法

            //从super必发里面找出必发价位小于某个值，比如1.7的比赛


            //获取比赛
            DataTable dtStrategy = StrategyDAL.GetStrategy13Data();
            Dictionary<String, List<DataRow>> riqiDict = new Dictionary<string, List<DataRow>>();
            double thresholdPeilv = Convert.ToDouble(txtThresholdPeilv.Text);
            //按照日期分类
            foreach (DataRow row in dtStrategy.Rows)
            {
                String riqi = row["riqi"].ToString();
                //得先进行赔率的过滤
                double shengsp = Convert.ToDouble(row["shengsp"]);
                if (shengsp < thresholdPeilv)
                {
                    continue;
                }


                List<DataRow> rows;
                if (riqiDict.TryGetValue(riqi, out rows))
                {
                    rows.Add(row);
                }
                else
                {
                    rows = new List<DataRow>();
                    rows.Add(row);
                    riqiDict[riqi] = rows;
                }
            }

            List<MatchShouyi> matches = new List<MatchShouyi>();
            double basepeilv = Convert.ToDouble(txtBasePeilv.Text);
            //double thresholdPeilv = Convert.ToDouble(txtThresholdPeilv.Text);

            double currentMoney = 0.0;
            double maxHuiche = 0.0;
            double totalshouyi = 0.0;
            double staticshouyi = 100.0;
            double bestPeilv = 0.0;
            string bestLucky = "";

            int maxFailed = 0;
            int continuedFailed = 0;
            int maxSuccess = 0;
            int continuedSuccess = 0;

            string bestriqi = "";
            string bestbianhao = "";

            DataRow bestMatchRow;

            //MatchShouyi shouyi;

            foreach (KeyValuePair<string, List<DataRow>> keyvalue in riqiDict)
            {
                //shouyi = new MatchShouyi();
                //shouyi.riqi = keyvalue.Key;

                List<DataRow> rows = keyvalue.Value;
                DataRow firstRow = rows[0];

                bestriqi = firstRow["riqi"].ToString();
                bestPeilv = Convert.ToDouble(firstRow["shengsp"]);
                bestLucky = firstRow["lucky"].ToString();
                bestbianhao = firstRow["bianhao"].ToString();

                foreach (DataRow row in rows)
                {
                    string riqi = row["riqi"].ToString();
                    double shengsp = Convert.ToDouble(row["shengsp"]);
                    string lucky = row["lucky"].ToString();
                    string bianhao = row["bianhao"].ToString();

                    double diff = Math.Abs(shengsp - basepeilv);
                    double diff2 = Math.Abs(bestPeilv - basepeilv);

                    if (diff < diff2)
                    {
                        bestMatchRow = row;
                        bestPeilv = shengsp;
                        bestLucky = lucky;
                        bestriqi = riqi;
                        bestbianhao = bianhao;
                    }
                }

                //开始计算收益

                totalshouyi = totalshouyi + 100;

                double touru = totalshouyi / (bestPeilv - 1);
                currentMoney -= touru;
                if (currentMoney < maxHuiche)
                {
                    maxHuiche = currentMoney;
                }
                if (bestLucky == "1")
                {
                    currentMoney = touru * bestPeilv + currentMoney;
                    totalshouyi = 0.0;
                    continuedFailed = 0;

                    continuedSuccess++;
                    if (continuedSuccess > maxSuccess)
                    {
                        maxSuccess = continuedSuccess;

                    }

                }
                else
                {
                    totalshouyi += touru;
                    continuedFailed++;
                    if (continuedFailed > maxFailed)
                    {
                        maxFailed = continuedFailed;

                    }

                    continuedSuccess = 0;
                }

                MatchShouyi match = new MatchShouyi();
                match.riqi = bestriqi;
                match.bianhao = bestbianhao;
                match.lucky = bestLucky;
                match.shouyi = currentMoney;
                match.totaltouru = touru;
                match.totalprize = bestPeilv;
                //match.huiche = match.shouyi - match.totaltouru;
                matches.Add(match);

            }

            //计算一下回撤
            for (int i = 1; i < matches.Count; i++)
            {
                MatchShouyi match = matches[i];
                MatchShouyi preMatch = matches[i - 1];
                match.huiche = preMatch.shouyi - match.totaltouru;
            }
            dataGridView1.DataSource = matches;
            PrintPicture(matches);
            lblResult.Text = "最终的收益为" + currentMoney + "最大回撤为" + maxHuiche + "最大连续失败次数" + maxFailed + "最大连续成功次数" + maxSuccess;
            
        }

        private void btnStrategy14_Click(object sender, EventArgs e)
        {
            //主要是把samuel的数据和一定牛的数据进行综合，找出有效的相同的数据，预测结果为胜平负均可，其他算法跟策略12一样

            //和策略12相同，只不过改进该算法，先分类

          
            //每天的收益预期是100，算出最大回撤

            //获取比赛
            DataTable dtStrategy = StrategyDAL.GetStrategy14Data();
            Dictionary<String, List<DataRow>> riqiDict = new Dictionary<string, List<DataRow>>();
            double thresholdPeilv = Convert.ToDouble(txtThresholdPeilv.Text);
            //按照日期分类
            foreach (DataRow row in dtStrategy.Rows)
            {
                String riqi = row["riqi"].ToString();
                string realresult = row["realresult"].ToString();

                //得先进行赔率的过滤
                double shengsp = Convert.ToDouble(row["shengsp"]);
                //修正一下赔率的值
                switch (realresult)
                {
                    case "3": shengsp = Convert.ToDouble(row["shengsp"]); break;
                    case "1": shengsp = Convert.ToDouble(row["pingsp"]); break;
                    case "0": shengsp = Convert.ToDouble(row["fusp"]); break;

                }
                if (shengsp < thresholdPeilv)
                {
                    continue;
                }


                List<DataRow> rows;
                if (riqiDict.TryGetValue(riqi, out rows))
                {
                    rows.Add(row);
                }
                else
                {
                    rows = new List<DataRow>();
                    rows.Add(row);
                    riqiDict[riqi] = rows;
                }
            }

            List<MatchShouyi> matches = new List<MatchShouyi>();
            double basepeilv = Convert.ToDouble(txtBasePeilv.Text);
            //double thresholdPeilv = Convert.ToDouble(txtThresholdPeilv.Text);

            double currentMoney = 0.0;
            double maxHuiche = 0.0;
            double totalshouyi = 0.0;
            double staticshouyi = 100.0;
            double bestPeilv = 0.0;
            string bestLucky = "";

            int maxFailed = 0;
            int continuedFailed = 0;
            int maxSuccess = 0;
            int continuedSuccess = 0;

            string bestriqi = "";
            string bestbianhao = "";

            DataRow bestMatchRow;

            //MatchShouyi shouyi;

            foreach (KeyValuePair<string, List<DataRow>> keyvalue in riqiDict)
            {
                //shouyi = new MatchShouyi();
                //shouyi.riqi = keyvalue.Key;

                List<DataRow> rows = keyvalue.Value;
                DataRow firstRow = rows[0];

                bestriqi = firstRow["riqi"].ToString();
                bestPeilv = Convert.ToDouble(firstRow["shengsp"]);
                bestLucky = firstRow["lucky"].ToString();
                bestbianhao = firstRow["bianhao"].ToString();

                foreach (DataRow row in rows)
                {
                    string riqi = row["riqi"].ToString();
                    double shengsp = Convert.ToDouble(row["shengsp"]);
                    string lucky = row["lucky"].ToString();
                    string bianhao = row["bianhao"].ToString();

                    double diff = Math.Abs(shengsp - basepeilv);
                    double diff2 = Math.Abs(bestPeilv - basepeilv);

                    if (diff < diff2)
                    {
                        bestMatchRow = row;
                        bestPeilv = shengsp;
                        bestLucky = lucky;
                        bestriqi = riqi;
                        bestbianhao = bianhao;
                    }
                }

                //开始计算收益

                totalshouyi = totalshouyi + 100;

                double touru = totalshouyi / (bestPeilv - 1);
                currentMoney -= touru;
                if (currentMoney < maxHuiche)
                {
                    maxHuiche = currentMoney;
                }
                if (bestLucky == "1")
                {
                    currentMoney = touru * bestPeilv + currentMoney;
                    totalshouyi = 0.0;
                    continuedFailed = 0;

                    continuedSuccess++;
                    if (continuedSuccess > maxSuccess)
                    {
                        maxSuccess = continuedSuccess;

                    }

                }
                else
                {
                    totalshouyi += touru;
                    continuedFailed++;
                    if (continuedFailed > maxFailed)
                    {
                        maxFailed = continuedFailed;

                    }

                    continuedSuccess = 0;
                }

                MatchShouyi match = new MatchShouyi();
                match.riqi = bestriqi;
                match.bianhao = bestbianhao;
                match.lucky = bestLucky;
                match.shouyi = currentMoney;
                match.totaltouru = touru;
                match.totalprize = bestPeilv;
                //match.huiche = match.shouyi - match.totaltouru;
                matches.Add(match);

            }

            //计算一下回撤
            for (int i = 1; i < matches.Count; i++)
            {
                MatchShouyi match = matches[i];
                MatchShouyi preMatch = matches[i - 1];
                match.huiche = preMatch.shouyi - match.totaltouru;
            }
            dataGridView1.DataSource = matches;
            PrintPicture(matches);
            lblResult.Text = "最终的收益为" + currentMoney + "最大回撤为" + maxHuiche + "最大连续失败次数" + maxFailed + "最大连续成功次数" + maxSuccess;

        }

        private void btnStrategy15_Click(object sender, EventArgs e)
        {
            //和策略12相同，只不过改进该算法，先分类

            //选出一定牛里面预测结果为3的比赛，每天找出赔率最接近某个常数（比如1.5）进行投注
            //每天的收益预期是100，算出最大回撤

            //获取比赛
            DataTable dtStrategy = StrategyDAL.GetStrategy15Data();
            Dictionary<String, List<DataRow>> riqiDict = new Dictionary<string, List<DataRow>>();
            double thresholdPeilv = Convert.ToDouble(txtThresholdPeilv.Text);
            //按照日期分类
            foreach (DataRow row in dtStrategy.Rows)
            {
                String riqi = row["riqi"].ToString();
                //得先进行赔率的过滤
                double shengsp = Convert.ToDouble(row["shengsp"]);
                if (shengsp < thresholdPeilv)
                {
                    continue;
                }


                List<DataRow> rows;
                if (riqiDict.TryGetValue(riqi, out rows))
                {
                    rows.Add(row);
                }
                else
                {
                    rows = new List<DataRow>();
                    rows.Add(row);
                    riqiDict[riqi] = rows;
                }
            }

            List<MatchShouyi> matches = new List<MatchShouyi>();
            double basepeilv = Convert.ToDouble(txtBasePeilv.Text);
            //double thresholdPeilv = Convert.ToDouble(txtThresholdPeilv.Text);

            double currentMoney = 0.0;
            double maxHuiche = 0.0;
            double totalshouyi = 0.0;
            double staticshouyi = 100.0;
            double bestPeilv = 0.0;
            string bestLucky = "";

            int maxFailed = 0;
            int continuedFailed = 0;
            int maxSuccess = 0;
            int continuedSuccess = 0;

            string bestriqi = "";
            string bestbianhao = "";

            DataRow bestMatchRow;

            //MatchShouyi shouyi;

            foreach (KeyValuePair<string, List<DataRow>> keyvalue in riqiDict)
            {
                //shouyi = new MatchShouyi();
                //shouyi.riqi = keyvalue.Key;

                List<DataRow> rows = keyvalue.Value;
                DataRow firstRow = rows[0];

                bestriqi = firstRow["riqi"].ToString();
                bestPeilv = Convert.ToDouble(firstRow["shengsp"]);
                bestLucky = firstRow["lucky"].ToString();
                bestbianhao = firstRow["bianhao"].ToString();

                foreach (DataRow row in rows)
                {
                    string riqi = row["riqi"].ToString();
                    double shengsp = Convert.ToDouble(row["shengsp"]);
                    string lucky = row["lucky"].ToString();
                    string bianhao = row["bianhao"].ToString();

                    double diff = Math.Abs(shengsp - basepeilv);
                    double diff2 = Math.Abs(bestPeilv - basepeilv);

                    if (diff < diff2)
                    {
                        bestMatchRow = row;
                        bestPeilv = shengsp;
                        bestLucky = lucky;
                        bestriqi = riqi;
                        bestbianhao = bianhao;
                    }
                }

                //开始计算收益

                totalshouyi = totalshouyi + 100;

                double touru = totalshouyi / (bestPeilv - 1);
                currentMoney -= touru;
                if (currentMoney < maxHuiche)
                {
                    maxHuiche = currentMoney;
                }
                if (bestLucky == "1")
                {
                    currentMoney = touru * bestPeilv + currentMoney;
                    totalshouyi = 0.0;
                    continuedFailed = 0;

                    continuedSuccess++;
                    if (continuedSuccess > maxSuccess)
                    {
                        maxSuccess = continuedSuccess;

                    }

                }
                else
                {
                    totalshouyi += touru;
                    continuedFailed++;
                    if (continuedFailed > maxFailed)
                    {
                        maxFailed = continuedFailed;

                    }

                    continuedSuccess = 0;
                }

                MatchShouyi match = new MatchShouyi();
                match.riqi = bestriqi;
                match.bianhao = bestbianhao;
                match.lucky = bestLucky;
                match.shouyi = currentMoney;
                match.totaltouru = touru;
                match.totalprize = bestPeilv;
                //match.huiche = match.shouyi - match.totaltouru;
                matches.Add(match);

            }

            //计算一下回撤
            for (int i = 1; i < matches.Count; i++)
            {
                MatchShouyi match = matches[i];
                MatchShouyi preMatch = matches[i - 1];
                match.huiche = preMatch.shouyi - match.totaltouru;
            }
            dataGridView1.DataSource = matches;
            PrintPicture(matches);
            lblResult.Text = "最终的收益为" + currentMoney + "最大回撤为" + maxHuiche + "最大连续失败次数" + maxFailed + "最大连续成功次数" + maxSuccess;
            
        }

        private void btnStrategy16_Click(object sender, EventArgs e)
        {
            //该策略在策略12的基础上，使用优化百分比参数，在策略12的基础上，追求最大利益化（这里的前提是失败最大次数不能太多，小于等于3是可以考虑的）
            //经充分测试，较好的前提参数是基准赔率1.8，过滤赔率1.6
            //这里策略的选择其实比较麻烦
            //试验三种策略，1，每次都把当前的收益，拿出来进行投资
            //2.只有上轮中奖才拿出来
            //3.只有上轮不中奖才拿出来

            //策略12的代码

            //和策略2相同，只不过改进该算法，先分类

            //选出一定牛里面让球数为0，且预测结果为3的比赛，每天找出赔率最接近某个常数（比如1.5）进行投注
            //每天的收益预期是100，算出最大回撤

            //获取比赛
            DataTable dtStrategy = StrategyDAL.GetStrategy1Data();
            Dictionary<String, List<DataRow>> riqiDict = new Dictionary<string, List<DataRow>>();
            double thresholdPeilv = Convert.ToDouble(txtThresholdPeilv.Text);
            //按照日期分类
            foreach (DataRow row in dtStrategy.Rows)
            {
                String riqi = row["riqi"].ToString();
                //得先进行赔率的过滤
                double shengsp = Convert.ToDouble(row["shengsp"]);
                if (shengsp < thresholdPeilv)
                {
                    continue;
                }


                List<DataRow> rows;
                if (riqiDict.TryGetValue(riqi, out rows))
                {
                    rows.Add(row);
                }
                else
                {
                    rows = new List<DataRow>();
                    rows.Add(row);
                    riqiDict[riqi] = rows;
                }
            }

            List<MatchShouyi> matches = new List<MatchShouyi>();
            double basepeilv = Convert.ToDouble(txtBasePeilv.Text);
            //double thresholdPeilv = Convert.ToDouble(txtThresholdPeilv.Text);

            double currentMoney = 0.0;
            double maxHuiche = 0.0;
            double totalshouyi = 0.0;
            double staticshouyi = 100.0; //默认的固定收益
            double bestPeilv = 0.0;
            string bestLucky = "";

            int maxFailed = 0;
            int continuedFailed = 0;
            int maxSuccess = 0;
            int continuedSuccess = 0;

            string bestriqi = "";
            string bestbianhao = "";

            double addedPercent = Convert.ToDouble(txtAddedPercent.Text);
            double gudingPercent = Convert.ToDouble(txtGudingPercent.Text); 
            DataRow bestMatchRow;

            //MatchShouyi shouyi;

            foreach (KeyValuePair<string, List<DataRow>> keyvalue in riqiDict)
            {
                //shouyi = new MatchShouyi();
                //shouyi.riqi = keyvalue.Key;

                List<DataRow> rows = keyvalue.Value;
                DataRow firstRow = rows[0];

                bestriqi = firstRow["riqi"].ToString();
                bestPeilv = Convert.ToDouble(firstRow["shengsp"]);
                bestLucky = firstRow["lucky"].ToString();
                bestbianhao = firstRow["bianhao"].ToString();

                foreach (DataRow row in rows)
                {
                    string riqi = row["riqi"].ToString();
                    double shengsp = Convert.ToDouble(row["shengsp"]);
                    string lucky = row["lucky"].ToString();
                    string bianhao = row["bianhao"].ToString();

                    double diff = Math.Abs(shengsp - basepeilv);
                    double diff2 = Math.Abs(bestPeilv - basepeilv);

                    if (diff < diff2)
                    {
                        bestMatchRow = row;
                        bestPeilv = shengsp;
                        bestLucky = lucky;
                        bestriqi = riqi;
                        bestbianhao = bianhao;
                    }
                }

                //开始计算收益
                int tempStaticShouyi = (int)(currentMoney * gudingPercent);
                if (tempStaticShouyi < 100)
                {
                    staticshouyi = 100;
                }
                else
                {
                    staticshouyi = tempStaticShouyi;
                }
                totalshouyi = totalshouyi + staticshouyi;

                

                double touru = totalshouyi / (bestPeilv - 1);
                currentMoney -= touru;
                
                //如果当前值大于0，则追加百分比做投入
                if (currentMoney > 0)
                {
                    touru = touru + currentMoney * addedPercent;
                    currentMoney = currentMoney *(1 - addedPercent);
                }

                if (currentMoney < maxHuiche)
                {
                    maxHuiche = currentMoney;
                }
                if (bestLucky == "1")
                {
                    currentMoney = touru * bestPeilv + currentMoney;
                    totalshouyi = 0.0;
                    continuedFailed = 0;

                    continuedSuccess++;
                    if (continuedSuccess > maxSuccess)
                    {
                        maxSuccess = continuedSuccess;

                    }

                }
                else
                {
                    totalshouyi += touru;
                    continuedFailed++;
                    if (continuedFailed > maxFailed)
                    {
                        maxFailed = continuedFailed;

                    }

                    continuedSuccess = 0;
                }

                MatchShouyi match = new MatchShouyi();
                match.riqi = bestriqi;
                match.bianhao = bestbianhao;
                match.lucky = bestLucky;
                match.shouyi = currentMoney;
                match.totaltouru = touru;
                match.totalprize = bestPeilv;
                //match.huiche = match.shouyi - match.totaltouru;
                matches.Add(match);

            }

            //计算一下回撤
            for (int i = 1; i < matches.Count; i++)
            {
                MatchShouyi match = matches[i];
                MatchShouyi preMatch = matches[i - 1];
                match.huiche = preMatch.shouyi - match.totaltouru;
            }
            dataGridView1.DataSource = matches;
            PrintPicture(matches);
            lblResult.Text = "最终的收益为" + currentMoney + "最大回撤为" + maxHuiche + "最大连续失败次数" + maxFailed + "最大连续成功次数" + maxSuccess;

        }

        private void btnStrategy17_Click(object sender, EventArgs e)
        {
            //跟策略16一样，但是收益都用百分比来表示，比如刚开始是1
            //每天期待收益0.1


            //获取比赛
            DataTable dtStrategy = StrategyDAL.GetStrategy1Data();
            Dictionary<String, List<DataRow>> riqiDict = new Dictionary<string, List<DataRow>>();
            double thresholdPeilv = Convert.ToDouble(txtThresholdPeilv.Text);
            //按照日期分类
            foreach (DataRow row in dtStrategy.Rows)
            {
                String riqi = row["riqi"].ToString();
                //得先进行赔率的过滤
                double shengsp = Convert.ToDouble(row["shengsp"]);
                if (shengsp < thresholdPeilv)
                {
                    continue;
                }


                List<DataRow> rows;
                if (riqiDict.TryGetValue(riqi, out rows))
                {
                    rows.Add(row);
                }
                else
                {
                    rows = new List<DataRow>();
                    rows.Add(row);
                    riqiDict[riqi] = rows;
                }
            }

            List<MatchShouyi> matches = new List<MatchShouyi>();
            double basepeilv = Convert.ToDouble(txtBasePeilv.Text);
            //double thresholdPeilv = Convert.ToDouble(txtThresholdPeilv.Text);

            double currentMoney = 1;
            double maxHuiche = 0.0;
            double totalshouyi = 0.0;
            double staticshouyi = Convert.ToDouble(txtGudingPercent.Text) ; //默认的固定收益
            double bestPeilv = 0.0;
            string bestLucky = "";

            int maxFailed = 0;
            int continuedFailed = 0;
            int maxSuccess = 0;
            int continuedSuccess = 0;

            string bestriqi = "";
            string bestbianhao = "";

            double addedPercent = Convert.ToDouble(txtAddedPercent.Text);
            DataRow bestMatchRow;

            //MatchShouyi shouyi;

            foreach (KeyValuePair<string, List<DataRow>> keyvalue in riqiDict)
            {
                //shouyi = new MatchShouyi();
                //shouyi.riqi = keyvalue.Key;

                List<DataRow> rows = keyvalue.Value;
                DataRow firstRow = rows[0];

                bestriqi = firstRow["riqi"].ToString();
                bestPeilv = Convert.ToDouble(firstRow["shengsp"]);
                bestLucky = firstRow["lucky"].ToString();
                bestbianhao = firstRow["bianhao"].ToString();

                foreach (DataRow row in rows)
                {
                    string riqi = row["riqi"].ToString();
                    double shengsp = Convert.ToDouble(row["shengsp"]);
                    string lucky = row["lucky"].ToString();
                    string bianhao = row["bianhao"].ToString();

                    double diff = Math.Abs(shengsp - basepeilv);
                    double diff2 = Math.Abs(bestPeilv - basepeilv);

                    if (diff < diff2)
                    {
                        bestMatchRow = row;
                        bestPeilv = shengsp;
                        bestLucky = lucky;
                        bestriqi = riqi;
                        bestbianhao = bianhao;
                    }
                }

                //开始计算收益
                //int tempStaticShouyi = (int)(currentMoney * addedPercent);
                //if (tempStaticShouyi < 100)
                //{
                //    staticshouyi = 100;
                //}
                totalshouyi = totalshouyi + staticshouyi;



                double touru = totalshouyi / (bestPeilv - 1);
                currentMoney -= touru;

                //如果当前值大于0，则追加百分比做投入
                if (currentMoney > 0)
                {
                    touru = touru + currentMoney * addedPercent;
                    currentMoney = currentMoney * (1 - addedPercent);
                }

                if (currentMoney < maxHuiche)
                {
                    maxHuiche = currentMoney;
                }
                if (bestLucky == "1")
                {
                    currentMoney = touru * bestPeilv + currentMoney;
                    totalshouyi = 0.0;
                    continuedFailed = 0;

                    continuedSuccess++;
                    if (continuedSuccess > maxSuccess)
                    {
                        maxSuccess = continuedSuccess;

                    }

                }
                else
                {
                    totalshouyi += touru;
                    continuedFailed++;
                    if (continuedFailed > maxFailed)
                    {
                        maxFailed = continuedFailed;

                    }

                    continuedSuccess = 0;
                }

                MatchShouyi match = new MatchShouyi();
                match.riqi = bestriqi;
                match.bianhao = bestbianhao;
                match.lucky = bestLucky;
                match.shouyi = currentMoney;
                match.totaltouru = touru;
                match.totalprize = bestPeilv;
                //match.huiche = match.shouyi - match.totaltouru;
                matches.Add(match);

            }

            //计算一下回撤
            for (int i = 1; i < matches.Count; i++)
            {
                MatchShouyi match = matches[i];
                MatchShouyi preMatch = matches[i - 1];
                match.huiche = preMatch.shouyi - match.totaltouru;
            }
            dataGridView1.DataSource = matches;
            PrintPicture(matches);
            lblResult.Text = "最终的收益为" + currentMoney + "最大回撤为" + maxHuiche + "最大连续失败次数" + maxFailed + "最大连续成功次数" + maxSuccess;
        }
    }
}
