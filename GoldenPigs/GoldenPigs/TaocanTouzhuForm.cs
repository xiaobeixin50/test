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
    public partial class TaocanTouzhuForm : Form
    {
        public TaocanTouzhuForm()
        {
            InitializeComponent();
        }

        private void dgvTaocan_SelectionChanged(object sender, EventArgs e)
        {
            string taocanid = "0";
            if (dgvTaocan.SelectedRows != null && dgvTaocan.SelectedRows.Count > 0)
            {
                Console.WriteLine(dgvTaocan.SelectedRows[0].Index);
                taocanid = dgvTaocan.SelectedRows[0].Cells[0].Value.ToString();
            }


            ChangeTaocanDetail(taocanid); 
        }

        private void TaocanTouzhuForm_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            string riqi = dtpSearchDate.Value.Date.ToString("yyyy-MM-dd");
            DataSet ds = new TaocanVoteDAL().GetTaocanVote(riqi);
            dgvTaocan.DataSource = ds.Tables[0];
        }
        private void ChangeTaocanDetail(string taocanid)
        {
            lbTaocanDetail.Items.Clear();

            DataSet ds = new TaocanVoteDAL().GetTaocandetailVote(taocanid);
            foreach (DataRow row in ds.Tables[0].Rows)
            {

                string detail = row[2].ToString().PadRight(10, ' ') + row[4].ToString().PadRight(4, ' ') + row[5].ToString().PadRight(10, ' ') + row[7].ToString().PadRight(4, ' ') + " 倍数：" + row[8].ToString().PadRight(4, ' ') + " 奖金：" + row[9].ToString().PadRight(10, ' ');
                lbTaocanDetail.Items.Add(detail);
            }
        }

        private void btnTouzhu_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvTaocan.SelectedRows[0];

            TouzhuTaocan taocan = new TouzhuTaocan();
            string type = row.Cells["Type"].Value.ToString();
            int leixing = 0;
            if (type == "专家推荐套餐")
            {
                leixing = 0;
            }
            else if (type == "数据模型套餐")
            {
                leixing = 1;
            }
            taocan.Touzhuleixing = leixing;
            taocan.Touzhumingcheng = type;
            taocan.Touzhuid = Convert.ToInt32(row.Cells["ID"].Value.ToString()) ;
            int beishu = Convert.ToInt32(txtBeishu.Text);
            double touru = Convert.ToDouble(row.Cells["Touru"].Value);
            double jiangjin = Convert.ToDouble(row.Cells["Jiangjin"].Value);

            taocan.Touzhubeishu = beishu;
            taocan.Touzhujin = beishu*touru;
            taocan.Jiangjin = jiangjin* beishu;
            taocan.Lucky = -1;
            taocan.Touzhuqishu = Convert.ToInt32(row.Cells["Qishu"].Value.ToString());
            taocan.Touzhushijian = Convert.ToDateTime(row.Cells["riqi"].Value);
            taocan.OperateTime = DateTime.Now;
            taocan.Operator = "system";

            new TouzhuTaocanDAL().InsertTouzhutaocan(taocan);
            MessageBox.Show("投注成功!");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}
