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

namespace GoldenPigs._0630
{
    public partial class DataStaticsForm : Form
    {
        public DataStaticsForm()
        {
            InitializeComponent();
        }

        private void btnCalcShouyiDaily_Click(object sender, EventArgs e)
        {
            double minShouyi = double.MaxValue;
            double maxShouyi = double.MinValue;
            string mindate = "";
            string maxdate = "";
            DataSet yuce = new AppYuceDAL().GetAllYuce();
            DataSet kaijiang = new KaijiangDAL().GetAllKaijiang();

            double totalshouyi = 0.0;
            double defaultTouru = 200;

            string currentRiqi = "";
            List<DailyShouyi> dailyShouyis = new List<DailyShouyi>();

            DailyShouyi dailyshouyi = null;
            foreach (DataRow yuceRow in yuce.Tables[0].Rows)
            {
                

                string yuceriqi = yuceRow["riqi"].ToString();

                if (currentRiqi != yuceriqi)
                {
                    if (dailyshouyi != null)
                    {
                        dailyshouyi.shouyi = dailyshouyi.totalprize - dailyshouyi.totaltouru;
                    }
                    dailyshouyi = new DailyShouyi();
                    dailyshouyi.riqi = yuceriqi;

                    dailyShouyis.Add(dailyshouyi);
                    currentRiqi = yuceriqi;
                }
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
                    dailyshouyi.totaltouru += defaultTouru;


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

                                dailyshouyi.totalprize += defaultTouru * spfresultsp; 
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

                                dailyshouyi.totalprize += firstsp * (defaultTouru - defaultTouru / secondsp);

                            }
                            else
                            {
                                totalshouyi = totalshouyi + defaultTouru;

                                dailyshouyi.totalprize += defaultTouru;
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
            dataGridView1.DataSource = dailyShouyis;
        }

        private void btnCalcShouyiMatch_Click(object sender, EventArgs e)
        {
            double minShouyi = double.MaxValue;
            double maxShouyi = double.MinValue;
            string mindate = "";
            string maxdate = "";
            DataSet yuce = new AppYuceDAL().GetAllYuce();
            DataSet kaijiang = new KaijiangDAL().GetAllKaijiang();

            double totalshouyi = 0.0;
            double defaultTouru = 200;

            string currentRiqi = "";
            List<MatchShouyi> dailyShouyis = new List<MatchShouyi>();

            MatchShouyi dailyshouyi = null;
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
                    dailyshouyi = new MatchShouyi();
                    dailyshouyi.riqi = yuceriqi;
                    dailyshouyi.bianhao = yucebianhao;

                    dailyShouyis.Add(dailyshouyi);
                    dailyshouyi.totaltouru += defaultTouru;


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

                                dailyshouyi.totalprize += defaultTouru * spfresultsp;
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

                                dailyshouyi.totalprize += firstsp * (defaultTouru - defaultTouru / secondsp);

                            }
                            else
                            {
                                totalshouyi = totalshouyi + defaultTouru;

                                dailyshouyi.totalprize += defaultTouru;
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

                    dailyshouyi.shouyi = dailyshouyi.totalprize - dailyshouyi.totaltouru;
                }
            }
            dataGridView1.DataSource = dailyShouyis;
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

        private void btnCalcBifa_Click(object sender, EventArgs e)
        {

        }
    }
}
