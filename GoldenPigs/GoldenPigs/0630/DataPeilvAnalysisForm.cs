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


                string maxspfresult = "";
                double maxgailv = 0.0;
                string maxrqspfresult = "";
                double maxrqgailv = 0.0;

                if (shenglv >= pinglv && shenglv >= fulv)
                {
                    maxspfresult = "3";
                    maxgailv = shenglv;
                }
                if (pinglv > shenglv && pinglv >= fulv)
                {
                    maxspfresult = "1";
                    maxgailv = pinglv;
                }
                if (fulv > shenglv && fulv > pinglv)
                {
                    maxspfresult = "0";
                    maxgailv = fulv;
                }

                if (rqshenglv >= rqpinglv && rqshenglv >= rqfulv)
                {
                    maxrqspfresult = "3";
                    maxrqgailv = rqshenglv;
                }
                if (rqpinglv > rqshenglv && rqpinglv >= rqfulv)
                {
                    maxrqspfresult = "1";
                    maxrqgailv = rqpinglv;
                }
                if (rqfulv > rqshenglv && rqfulv > rqpinglv)
                {
                    maxrqspfresult = "0";
                    maxrqgailv = rqfulv;
                }

                if(maxgailv == 0 || maxrqgailv == 0)
                {
                    peilv.tuijianspf = -1;
                    peilv.tuijianrqspf = -1;

                    //如果有为0的，说明三个值都为零，不能进行双确认
                    continue;
                }

                //找到双概率确认的组合
                bool flag = IsConflict(rqshu, maxspfresult, maxrqspfresult);
                if (flag)
                {
                    peilv.tuijianspf = -1;
                    peilv.tuijianrqspf = -1;
                }
                else
                {
                    //这里增加了0.5的过滤条件
                    if (maxgailv >= maxrqgailv && maxgailv >= 0.5)
                    {
                        peilv.tuijianspf = Convert.ToInt32(maxspfresult);
                        peilv.tuijianrqspf = -1;
                    }
                    else if(maxrqgailv > maxgailv  && maxrqgailv >= 0.5)
                    {
                        peilv.tuijianrqspf = Convert.ToInt32(maxrqspfresult);
                        peilv.tuijianspf = -1;
                    }
                    else
                    {
                        peilv.tuijianrqspf = -1;
                        peilv.tuijianspf = -1;
                    }
                }


            }

            dgvPeilvAnalysis.DataSource = peilvs;
            //获取总结信息：不让球是3，让球是0 优先统计
            int totalYuceCount = 0;
            int totalYuceSuccCount = 0;
            foreach (DataPeilvAnalysis pl in peilvs)
            {

                if (pl.tuijianspf == 3 || pl.tuijianrqspf == 0)
                {
                    totalYuceCount++;
                    if (pl.spfresult == "3" && pl.tuijianspf == 3 || pl.rqspfresult == "0" && pl.tuijianrqspf == 0)
                    {
                        totalYuceSuccCount++;
                    }
                }
            }
            lblResult.Text = "总共预测的比赛数为" + totalYuceCount + ",预测成功的比赛数" + totalYuceSuccCount;
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
    }
}
