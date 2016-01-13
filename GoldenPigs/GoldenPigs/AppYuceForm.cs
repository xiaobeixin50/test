using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GoldenPigs.DAL;

namespace GoldenPigs
{
    public partial class AppYuceForm : Form
    {
        public AppYuceForm()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            String riqi = dtpTouzhushijian.Value.ToString("yyyy-MM-dd");
            BindGrid(riqi);
        }

        private void BindGrid(String riqi)
        {
            DataSet ds = new AppYuceDAL().GetYuceByRiqi(riqi);
            dataGridView1.DataSource = ds.Tables[0];
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Index == dataGridView1.Rows.Count - 1)
                {
                    continue;
                }
                if (row.Cells["lucky"].Value.ToString() == "1")
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private void BindGrid()
        {
            DataSet ds = new AppYuceDAL().GetAllYuce();
            dataGridView1.DataSource = ds.Tables[0];

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Index == dataGridView1.Rows.Count - 1)
                {
                    continue;
                }
                if (row.Cells["lucky"].Value.ToString() == "1")
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //更新信息
            //更新预测是否中奖,现在只能更新竞彩足球的结果 
            DateTime touzhushijian = dtpTouzhushijian.Value.Date;

            new AppYuceDAL().UpdateZhongjiangResult(touzhushijian);

            MessageBox.Show("更新中奖信息成功！");
        }
       
    }
}
