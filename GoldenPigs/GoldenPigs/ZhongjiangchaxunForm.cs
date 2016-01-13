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
    public partial class ZhongjiangchaxunForm : Form
    {
        public ZhongjiangchaxunForm()
        {
            InitializeComponent();

        }

        private void BindGrid()
        {
            DataSet ds = new TouzhuSpfDAL().SearchTouzhuSpf("");
            dgTouzhuSpf.DataSource = ds.Tables[0];

        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string riqi = dtpTouzhuDate.Value.Date.ToString("yyyy-MM-dd");
            DataSet ds = new TouzhuSpfDAL().SearchTouzhuSpf(riqi);
            dgTouzhuSpf.DataSource = ds.Tables[0];
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string riqi = dtpTouzhuDate.Value.Date.ToString("yyyy-MM-dd");
            new TouzhuSpfDAL().UpdateZhongjiangResult(riqi);
            MessageBox.Show("更新成功！");
        }

        private void dgTouzhuSpf_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //Console.WriteLine(e.RowCount);
        }

        private void dgTouzhuSpf_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            
            if (e.RowIndex == -1) return;
            if (e.ColumnIndex == -1) return;
            if(e.ColumnIndex == 7)
            {
                if (dgTouzhuSpf.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                {
                    return;
                }
                string value = dgTouzhuSpf.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                if (value == "-1")
                {
                    e.CellStyle.BackColor = Color.Blue;
                }
                if (value == "1")
                {
                    e.CellStyle.BackColor = Color.Red;
                }
            }

        }

        private void btnPaijiang_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgTouzhuSpf.Rows)
            {
                long batchid = Convert.ToInt64(row.Cells["id"].Value);
                string type = "竞彩足球";
                double jiangjin = Convert.ToDouble(row.Cells["jiangjin"].Value);
                if(jiangjin != 0)
                {
                    Income income = new Income();
                    income.Amount = jiangjin;
                    income.IncomeType = "中奖";
                    income.TouzhuID = batchid;
                    income.TouzhuType = type;
                    income.Operator = "吴林";
                    income.OperateTime = DateTime.Now;
                    income.id = batchid;
                    IncomeDAL dal = new IncomeDAL();

                    //如果没有排过奖
                    if(dal.HasPaijiang(income)==0)
                    {
                        dal.InsertIncome(income);
                    }

                }
                
            }
            MessageBox.Show("派奖成功!");
        }

        private void ZhongjiangchaxunForm_Load(object sender, EventArgs e)
        {

            BindGrid();
        }
    }
}
