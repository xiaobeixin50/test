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
    public partial class DataPeilvAnalysisForm : Form
    {
        public DataPeilvAnalysisForm()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string riqi = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            DataTable dt = StrategyDAL.GetPeilvAnalysisData(riqi);
            List<DataPeilvAnalysis> peilvs = new List<DataPeilvAnalysis>();
            //样本数下线
            int countThreshold = Convert.ToInt32(txtYangben.Text);
            foreach (DataRow row in dt.Rows)
            {
                DataPeilvAnalysis peilv = new DataPeilvAnalysis();

                //获取每一行的赔率的分组和，一个是让球概率 ，一个是不让球概率
                int shengspfracint = Convert.ToInt32(row["shengspfracint"]);
                int pingspfracint = Convert.ToInt32(row["pingspfracint"]);
                int fuspfracint = Convert.ToInt32(row["fuspfracint"]);
                int rqshengspfracint = Convert.ToInt32(row["rqshengspfracint"]);
                int rqpingspfracint = Convert.ToInt32(row["rqpingspfracint"]);
                int rqfuspfracint = Convert.ToInt32(row["rqfuspfracint"]);

                string rowriqi = Convert.ToString(row["riqi"]);
                string bianhao = Convert.ToString(row["bianhao"]);
                string zhudui = Convert.ToString(row["zhudui"]);
                string kedui = Convert.ToString(row["kedui"]);
                int rqshu = Convert.ToInt32(row["rqshu"]);
                string spfresult = Convert.ToString(row["spfresult"]);
                string rqspfresult = Convert.ToString(row["rqspfresult"]);

                peilv.riqi = rowriqi;
                peilv.bianhao = bianhao;
                peilv.zhudui = zhudui;
                peilv.kedui = kedui;
                peilv.rqshu = rqshu;
                peilv.spfresult = spfresult;
                peilv.rqspfresult = rqspfresult;
                peilv.shengspfracint = shengspfracint;
                peilv.pingspfracint = pingspfracint;
                peilv.fuspfracint = fuspfracint;
                peilv.rqshengspfracint = rqshengspfracint;
                peilv.rqpingspfracint = rqpingspfracint;
                peilv.rqfuspfracint = rqfuspfracint;

                //这里的逻辑以后再改造
                if (shengspfracint == 0)
                {
                    peilv.tuijianspf = -1;
                    peilv.tuijianrqspf = -1;
                    peilvs.Add(peilv);
                    continue;
                }
                DataTable dtGroup = StrategyDAL.GetPeilvGroupData(shengspfracint, pingspfracint, fuspfracint, rowriqi);
                int shengCount = 0;
                int pingCount = 0;
                int fuCount = 0;

                foreach (DataRow groupRow in dtGroup.Rows)
                {
                    if (groupRow[0].ToString() == "3")
                    {
                        shengCount = Convert.ToInt32(groupRow[1]);
                    }
                    if (groupRow[0].ToString() == "1")
                    {
                        pingCount = Convert.ToInt32(groupRow[1]);
                    }
                    if (groupRow[0].ToString() == "0")
                    {
                        fuCount = Convert.ToInt32(groupRow[1]);
                    }
                }
                int totalCount = shengCount + pingCount + fuCount;
                double shenglv = 0;
                double pinglv = 0;
                double fulv = 0;

                if (totalCount != 0)
                {
                    shenglv = shengCount * 1.0 / totalCount;
                    pinglv = pingCount * 1.0 / totalCount;
                    fulv = fuCount * 1.0 / totalCount;
                }    

                DataTable dtRqGroup = StrategyDAL.GetRqPeilvGroupData(rqshengspfracint, rqpingspfracint, rqfuspfracint,rowriqi);

                int rqshengCount = 0;
                int rqpingCount = 0;
                int rqfuCount = 0;
                foreach (DataRow groupRqRow in dtRqGroup.Rows)
                {
                    if (groupRqRow[0].ToString() == "3")
                    {
                        rqshengCount = Convert.ToInt32(groupRqRow[1]);
                    }
                    if (groupRqRow[0].ToString() == "1")
                    {
                        rqpingCount = Convert.ToInt32(groupRqRow[1]);
                    }
                    if (groupRqRow[0].ToString() == "0")
                    {
                        rqfuCount = Convert.ToInt32(groupRqRow[1]);
                    }
                }
                double rqshenglv = 0;
                double rqpinglv = 0;
                double rqfulv = 0;
                int totalRqCount = rqshengCount + rqpingCount + rqfuCount;
                if (totalCount != 0)
                {
                    rqshenglv = rqshengCount * 1.0 / (rqshengCount + rqpingCount + rqfuCount);
                    rqpinglv = rqpingCount * 1.0 / (rqshengCount + rqpingCount + rqfuCount);
                    rqfulv = rqfuCount * 1.0 / (rqshengCount + rqpingCount + rqfuCount);
                }
                

                
                peilv.shenglv = shenglv;
                peilv.pinglv = pinglv;
                peilv.fulv = fulv;
                peilv.rqshenglv = rqshenglv;
                peilv.rqpinglv = rqpinglv;
                peilv.rqfulv = rqfulv;
                peilv.totalcount = totalCount;
                peilv.totalrqcount = totalRqCount;
                peilvs.Add(peilv);

                List<double> gailv = new List<double>();
                gailv.Add(shenglv);
                gailv.Add(pinglv);
                gailv.Add(fulv);

                int spfRank = 0;
                int rqspfRank = 0;

                //这里设置实际结果的概率排名
                switch(spfresult){
                    case "3":
                        spfRank = getRank(0, gailv);
                        break;

                    case "1":
                        spfRank = getRank(1, gailv);
                        break;

                    case "0":
                        spfRank = getRank(2, gailv);
                        break;

                }
                peilv.spfgailvrank = spfRank;

                List<double> rqgailv = new List<double>();
                rqgailv.Add(rqshenglv);
                rqgailv.Add(rqpinglv);
                rqgailv.Add(rqfulv);


                switch (rqspfresult)
                {
                    case "3":
                        rqspfRank = getRank(0, rqgailv);
                        break;

                    case "1":
                        rqspfRank = getRank(1, rqgailv);
                        break;

                    case "0":
                        rqspfRank = getRank(2, rqgailv);
                        break;

                }
                peilv.rqspfgailvrank = rqspfRank;


                //获取最近一场比赛的胜负，并获得概率排行
                DataTable dtLast = StrategyDAL.GetLastSpfData(shengspfracint, pingspfracint, fuspfracint, rowriqi);
                if (dtLast.Rows.Count > 0)
                {
                    //这里有可能出现第一条数据为空的情况
                    foreach (DataRow lastrow in dtLast.Rows)
                    {
                        if(String.IsNullOrEmpty(lastrow["spfresult"].ToString())){
                            continue;
                        }
                        else{
                            int lastspf = Convert.ToInt32(lastrow["spfresult"]);
                            peilv.lastspf = lastspf;
                            int lastspfRank = 0;
                            switch (lastspf)
                            {
                                case 3:
                                    lastspfRank = getRank(0, gailv);
                                    break;

                                case 1:
                                    lastspfRank = getRank(1, gailv);
                                    break;

                                case 0:
                                    lastspfRank = getRank(2, gailv);
                                    break;

                            }

                            //这里如果排名是概率里最后一名，可以考虑再往前推一场比赛，如果还是最后一名，则gailvrank可以加1
                            if (lastspfRank == gailv.Count)
                            {
                                if (dtLast.Rows.Count >= 2)
                                {
                                    if (String.IsNullOrEmpty(dtLast.Rows[1]["spfresult"].ToString()))
                                    {
                                        break;
                                    }
                                    int n2lastspf = Convert.ToInt32(dtLast.Rows[1]["spfresult"]);
                                    int n2lastspfRank = 0;
                                    switch (n2lastspf)
                                    {
                                        case 3:
                                            n2lastspfRank = getRank(0, gailv);
                                            break;

                                        case 1:
                                            n2lastspfRank = getRank(1, gailv);
                                            break;

                                        case 0:
                                            n2lastspfRank = getRank(2, gailv);
                                            break;
                                    }
                                    if (n2lastspfRank == gailv.Count)
                                    {
                                        lastspfRank++;
                                    }

                                }
                            }
                            peilv.lastspfgailvrank = lastspfRank;
                            break;
                        }
                    }
                   
                }
                else
                {
                    peilv.lastspf = -1;
                }


                DataTable dtLastRq = StrategyDAL.GetLastRqSpfData(rqshengspfracint, rqpingspfracint, rqfuspfracint, rowriqi);
                if (dtLastRq.Rows.Count > 0)
                {
                    //这里有可能出现第一条数据为空的情况
                    foreach (DataRow lastRqRow in dtLastRq.Rows)
                    {
                        if (String.IsNullOrEmpty(lastRqRow["rqspfresult"].ToString()))
                        {
                            continue;
                        }
                        else
                        {
                            int lastspf = Convert.ToInt32(lastRqRow["rqspfresult"]);
                            peilv.lastrqspf = lastspf;
                            int lastspfRank = 0;
                            switch (lastspf)
                            {
                                case 3:
                                    lastspfRank = getRank(0, rqgailv);
                                    break;

                                case 1:
                                    lastspfRank = getRank(1, rqgailv);
                                    break;

                                case 0:
                                    lastspfRank = getRank(2, rqgailv);
                                    break;

                            }
                            //这里如果排名是概率里最后一名，可以考虑再往前推一场比赛，如果还是最后一名，则gailvrank可以加1
                            if (lastspfRank == rqgailv.Count)
                            {
                                if (dtLastRq.Rows.Count >= 2)
                                {
                                    if (String.IsNullOrEmpty(dtLastRq.Rows[1]["rqspfresult"].ToString()))
                                    {
                                        break;
                                    } 
                                    int n2lastspf = Convert.ToInt32(dtLastRq.Rows[1]["rqspfresult"]);
                                    int n2lastspfRank = 0;
                                    switch (n2lastspf)
                                    {
                                        case 3:
                                            n2lastspfRank = getRank(0, rqgailv);
                                            break;

                                        case 1:
                                            n2lastspfRank = getRank(1, rqgailv);
                                            break;

                                        case 0:
                                            n2lastspfRank = getRank(2, rqgailv);
                                            break;
                                    }
                                    if (n2lastspfRank == rqgailv.Count)
                                    {
                                        lastspfRank++;
                                    }

                                }
                            }
                            peilv.lastrqspfgailvrank = lastspfRank;
                            break;
                        }
                    }
                    
                }
                else
                {
                    peilv.lastrqspf = -1;
                }
                //string maxspfresult = "";
                //double maxgailv = 0.0;
                //string maxrqspfresult = "";
                //double maxrqgailv = 0.0;

                //if (shenglv >= pinglv && shenglv >= fulv)
                //{
                //    maxspfresult = "3";
                //    maxgailv = shenglv;
                //}
                //if (pinglv > shenglv && pinglv >= fulv)
                //{
                //    maxspfresult = "1";
                //    maxgailv = pinglv;
                //}
                //if (fulv > shenglv && fulv > pinglv)
                //{
                //    maxspfresult = "0";
                //    maxgailv = fulv;
                //}

                //if (rqshenglv >= rqpinglv && rqshenglv >= rqfulv)
                //{
                //    maxrqspfresult = "3";
                //    maxrqgailv = rqshenglv;
                //}
                //if (rqpinglv > rqshenglv && rqpinglv >= rqfulv)
                //{
                //    maxrqspfresult = "1";
                //    maxrqgailv = rqpinglv;
                //}
                //if (rqfulv > rqshenglv && rqfulv > rqpinglv)
                //{
                //    maxrqspfresult = "0";
                //    maxrqgailv = rqfulv;
                //}

                //if(maxgailv == 0 || maxrqgailv == 0)
                //{
                //    peilv.tuijianspf = -1;
                //    peilv.tuijianrqspf = -1;

                //    //如果有为0的，说明三个值都为零，不能进行双确认
                //    continue;
                //}
                 
            //    //找到双概率确认的组合
            //    bool flag = IsConflict(rqshu, maxspfresult, maxrqspfresult);
            //    if (flag)
            //    {
            //        peilv.tuijianspf = -1;
            //        peilv.tuijianrqspf = -1;
            //    }
            //    else
            //    {
            //        //这里增加了0.5的过滤条件
            //        if (maxgailv >= maxrqgailv && maxgailv >= 0.5)
            //        {
            //            peilv.tuijianspf = Convert.ToInt32(maxspfresult);
            //            peilv.tuijianrqspf = -1;
            //        }
            //        else if(maxrqgailv > maxgailv  && maxrqgailv >= 0.5)
            //        {
            //            peilv.tuijianrqspf = Convert.ToInt32(maxrqspfresult);
            //            peilv.tuijianspf = -1;
            //        }
            //        else
            //        {
            //            peilv.tuijianrqspf = -1;
            //            peilv.tuijianspf = -1;
            //        }
            //    }


            }

            dgvPeilvAnalysis.DataSource = peilvs;
           

            //循环整个datagrid，对于概率进行颜色设置，从123分别为红黄蓝
            foreach (DataGridViewRow row in dgvPeilvAnalysis.Rows)
            {
                int spfresult = Convert.ToInt32(row.Cells["spfgailvrank"].Value);
                if (spfresult == 1)
                {
                    row.Cells[7].Style.BackColor = Color.Red;
                    row.Cells[8].Style.BackColor = Color.Red;
                    row.Cells[9].Style.BackColor = Color.Red;
                }
                else if(spfresult == 2)
                {
                    row.Cells[7].Style.BackColor = Color.Yellow;
                    row.Cells[8].Style.BackColor = Color.Yellow;
                    row.Cells[9].Style.BackColor = Color.Yellow;
                }
                else if(spfresult == 3)
                {
                    row.Cells[7].Style.BackColor = Color.Blue;
                    row.Cells[8].Style.BackColor = Color.Blue;
                    row.Cells[9].Style.BackColor = Color.Blue;
                }

                int rqspfresult = Convert.ToInt32(row.Cells["rqspfgailvrank"].Value);
                if (rqspfresult == 1)
                {
                    row.Cells[10].Style.BackColor = Color.Red;
                    row.Cells[11].Style.BackColor = Color.Red;
                    row.Cells[12].Style.BackColor = Color.Red;
                }
                else if (rqspfresult == 2)
                {
                    row.Cells[10].Style.BackColor = Color.Yellow;
                    row.Cells[11].Style.BackColor = Color.Yellow;
                    row.Cells[12].Style.BackColor = Color.Yellow;
                }
                else if(rqspfresult == 3)
                {
                    row.Cells[10].Style.BackColor = Color.Blue;
                    row.Cells[11].Style.BackColor = Color.Blue;
                    row.Cells[12].Style.BackColor = Color.Blue;
                }

                int lastrank = Convert.ToInt32(row.Cells["lastspfgailvrank"].Value);
                if (lastrank == 1)
                {
                    row.Cells[1].Style.BackColor = Color.Red;
                }
                else if (lastrank == 2)
                {
                    row.Cells[1].Style.BackColor = Color.Yellow;
                }
                else if (lastrank == 3)
                {
                    row.Cells[1].Style.BackColor = Color.Blue;
                }
                else if (lastrank == 4)
                {
                    row.Cells[1].Style.BackColor = Color.Green;
                }

                int lastrqrank = Convert.ToInt32(row.Cells["lastrqspfgailvrank"].Value);
                if (lastrqrank == 1)
                {
                    row.Cells[2].Style.BackColor = Color.Red;
                }
                else if (lastrqrank == 2)
                {
                    row.Cells[2].Style.BackColor = Color.Yellow;
                }
                else if (lastrqrank == 3)
                {
                    row.Cells[2].Style.BackColor = Color.Blue;
                }
                else if (lastrqrank == 4)
                {
                    row.Cells[2].Style.BackColor = Color.Green;
                }
            }

            //获取总结信息：不让球是3，让球是0 优先统计，这里的预测基本没什么用
            //int totalYuceCount = 0;
            //int totalYuceSuccCount = 0;
            //foreach (DataPeilvAnalysis pl in peilvs)
            //{

            //    if (pl.tuijianspf == 3 || pl.tuijianrqspf == 0)
            //    {
            //        totalYuceCount++;
            //        if (pl.spfresult == "3" && pl.tuijianspf == 3 || pl.rqspfresult == "0" && pl.tuijianrqspf == 0)
            //        {
            //            totalYuceSuccCount++;
            //        }
            //    }
            //}
            //lblResult.Text = "总共预测的比赛数为" + totalYuceCount + ",预测成功的比赛数" + totalYuceSuccCount;

            //获取统计信息概率
            int countOf1 = 0;
            int countOf2 = 0;
            int countOf3 = 0;
            foreach (DataPeilvAnalysis peilv in peilvs)
            {
                switch (peilv.spfgailvrank)
                {
                    case 1: countOf1++; break;
                    case 2: countOf2++; break;

                    case 3: countOf3++; break;
                }
                switch (peilv.rqspfgailvrank)
                {
                    case 1: countOf1++; break;
                    case 2: countOf2++; break;

                    case 3: countOf3++; break;
                }
            }

            //获胜概率
            double ratio = (countOf1 + countOf2) *1.0/ (countOf1 + countOf2 +countOf3);
            lblResult.Text = "获胜的概率为" + ratio;
        }

        private bool IsConflict(int  rqshu, string maxspfresult, string maxrqspfresult)
        {
            bool result = true;
            if (rqshu < 0)
            {
                switch (maxspfresult)
                {
                    case "3":
                        if (maxrqspfresult == "0")
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                        break;
                    case "1":
                        if (maxrqspfresult == "0")
                        {
                            result = false;
                        }
                        break;
                    case "0":
                        if (maxrqspfresult == "0")
                        {
                            result = false;
                        }
                        break;
                }
            }

            if (rqshu > 0)
            {
                switch (maxspfresult)
                {
                    case "3":
                        if (maxrqspfresult != "0" || maxrqspfresult != "1")
                        {
                            result = false;
                        }
                        break;
                    case "1":
                        if (maxrqspfresult != "0" || maxrqspfresult != "1")
                        {
                            result = false;
                        }
                        break;
                    case "0":
                        if (maxrqspfresult != "3")
                        {
                            result = false;
                        }                        
                        break;
                }
            }

            return result;
        }

        //获得在整个list的排名
        private int getRank(int spfIndex, List<double> spfList)
        {
            //都是零或者都是NaN
            bool zeroFlag = true;
            foreach (double db in spfList)
            {
                if (db != 0)
                {
                    zeroFlag = false;
                }
            }
            if (zeroFlag)
            {
                return 0;
            }

            bool nanFlag = true;
            foreach (double db in spfList)
            {
                if (!double.IsNaN(db))
                {
                    nanFlag = false;
                }
            }
            if (nanFlag)
            {
                return 0;
            }
            //获得当前值
            int largerCount = spfList.Count;
            double value = spfList[spfIndex];
            for (int i = 0; i < spfList.Count; i++)
            {
                if (value > spfList[i] ||  (value == spfList[i] && spfIndex < i))
                {
                    largerCount --;
                }
            }
            return largerCount;
        }
    }

}
