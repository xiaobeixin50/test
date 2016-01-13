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

namespace GoldenPigs
{
    public partial class YucePeilvAnalysisForm : Form
    {
        public YucePeilvAnalysisForm()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime touzhushijian = dateTimePicker1.Value.Date;
            YucerawdataDAL dal = new YucerawdataDAL();
            DataSet ds = dal.GetYucePeilv(touzhushijian);

            
            DataTable dt = new DataTable();
            dt.Columns.Add("主队", typeof(string));
            dt.Columns.Add("客队", typeof(string));
            dt.Columns.Add("时间", typeof(string));
            dt.Columns.Add("预测结果", typeof(string));
            dt.Columns.Add("赔率", typeof(string));
            dt.Columns.Add("让球数", typeof(string));
            dt.Columns.Add("是否中奖", typeof(string));

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string zhudui = row["zhuduireal"].ToString();
                string kedui = row["keduireal"].ToString();
                string riqi = row["riqi"].ToString();
                string rangqiu = row["rangqiu"].ToString();
                string yucespf = row["yucespf"].ToString();
                string lucky = row["lucky"].ToString();
                foreach (char ch in yucespf.ToCharArray())
                {
                    if (ch <= '9' && ch >= '0')
                    {
                        DataRow newrow = dt.NewRow();
                        newrow["主队"] = zhudui;
                        newrow["客队"] = kedui;
                        newrow["时间"] = riqi;
                        newrow["让球数"] = rangqiu;
                        string spfresult = row["spfresult1"].ToString();
                        if (rangqiu == "0")
                        {
                            spfresult = row["spfresult1"].ToString();
                        }
                        else 
                        {
                            spfresult = row["rqspfresult1"].ToString();
                        }
                        //if (lucky == "0")
                        //{
                        //    newrow["是否中奖"] = "未中奖";
                        //}
                        //else if (lucky == "1")
                        //{
                        //    newrow["是否中奖"] = "已中奖";
                        //}

                        switch (ch)
                        {
                            case '3':
                                if (!string.IsNullOrEmpty(spfresult))
                                {
                                    if (spfresult == "3")
                                    {
                                        newrow["是否中奖"] = "已中奖";
                                    }
                                    else
                                    {
                                        newrow["是否中奖"] = "未中奖";
                                    }
                                }
                                newrow["预测结果"] = "胜";
                                newrow["赔率"] = row["shengsp"].ToString();
                                break;
                            case '1':
                                if (!string.IsNullOrEmpty(spfresult))
                                {
                                    if (spfresult == "1")
                                    {
                                        newrow["是否中奖"] = "已中奖";
                                    }
                                    else
                                    {
                                        newrow["是否中奖"] = "未中奖";
                                    }
                                }
                                newrow["赔率"] = row["pingsp"].ToString();
                                newrow["预测结果"] = "平";
                                break;
                            case '0':
                                if (!string.IsNullOrEmpty(spfresult))
                                {
                                    if (spfresult == "0")
                                    {
                                        newrow["是否中奖"] = "已中奖";
                                    }
                                    else
                                    {
                                        newrow["是否中奖"] = "未中奖";
                                    }
                                }
                                newrow["预测结果"] = "负";
                                newrow["赔率"] = row["fusp"].ToString();
                                break;
                        }
                        dt.Rows.Add(newrow);
                    }
                    
                }
            }
            DataView dv = dt.DefaultView;
            dv.Sort = "赔率 Asc ,主队 Asc ";
            DataTable dt2 = dv.ToTable();

            dataGridView1.DataSource = dt2;
            
            //修改grid样式用于看清
            foreach (DataGridViewRow gridRow in dataGridView1.Rows)
            {

                if (gridRow.Cells["是否中奖"].Value != null && gridRow.Cells["是否中奖"].Value.ToString() == "已中奖")
                {
                    gridRow.DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private void btnRandomTouzhu_Click(object sender, EventArgs e)
        {
            double maxSp = double.MinValue;
            int validCount = 0;
            //重置背景色
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }
            //随机投注，选取sp不超过2，或者最高sp的一般的记录来投注
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["赔率"].Value != "")
                {
                    double sp = Convert.ToDouble(row.Cells["赔率"].Value);
                    if (sp > maxSp)
                    {
                        maxSp = sp;
                    }

                }
                
            }
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["赔率"].Value != "" && row.Cells["赔率"].Value != null)
                {
                    double sp = Convert.ToDouble(row.Cells["赔率"].Value);
                    if (sp < maxSp/2)
                    {
                        //maxSp = sp;
                        validCount++;
                    }

                }
            }

            //随机选择两注
            Random random = new Random();
            int random1 = random.Next(validCount);
            int random2 = random.Next(validCount);
            if (random1 != random2 && dataGridView1.Rows[random1].Cells["主队"].Value.ToString() != dataGridView1.Rows[random2].Cells["主队"].Value.ToString())
            {
                dataGridView1.Rows[random1].DefaultCellStyle.BackColor = Color.Green;
                dataGridView1.Rows[random2].DefaultCellStyle.BackColor = Color.Green;
            }
            lblTouzhuMsg.Text = "随机投注信息：" + "最高赔率" + maxSp + "，有效记录数为" + validCount + "随机数为" + random1 + "和" + random2 + "。";
            
        }

        private void btnTouzhu_Click(object sender, EventArgs e)
        {
            //先获取选中的两行
            List<DataGridViewRow> rows = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.DefaultCellStyle.BackColor == Color.Green)
                {
                    rows.Add(row);
                }
            }
            TouzhuSpfDAL dal = new TouzhuSpfDAL();
            TouzhuSpf spf = new TouzhuSpf();

            long batchid = Convert.ToInt64(DateTime.Now.ToString("MMddhhmmss") + DateTime.Now.Millisecond);
            spf.BatchID = batchid;
            spf.Beishu = 1000;
            spf.Riqi = rows[0].Cells["时间"].Value.ToString() + "," + rows[1].Cells["时间"].Value.ToString();
            spf.Zhudui = rows[0].Cells["主队"].Value.ToString() + "," + rows[1].Cells["主队"].Value.ToString();
            spf.Kedui = rows[0].Cells["客队"].Value.ToString() + "," + rows[1].Cells["客队"].Value.ToString();
            spf.Result = rows[0].Cells["预测结果"].Value.ToString() + "," + rows[1].Cells["预测结果"].Value.ToString();
            spf.Rangqiu = rows[0].Cells["让球数"].Value.ToString() + "," + rows[1].Cells["让球数"].Value.ToString();
            spf.Peilv = rows[0].Cells["赔率"].Value.ToString() + "," + rows[1].Cells["赔率"].Value.ToString();

            spf.Lucky = -1;
            spf.OperateTime = DateTime.Now;
            spf.Operator = "系统随机推荐";
            dal.InsertTouzhuSpf(spf);

            MessageBox.Show("投注成功!");
        }
    }
}
