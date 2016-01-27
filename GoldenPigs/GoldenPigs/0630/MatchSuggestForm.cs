using GoldenPigs.DAL;
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
    public partial class MatchSuggestForm : Form
    {
        public MatchSuggestForm()
        {
            InitializeComponent();
        }

        private void btnSuperbifaSuggest_Click(object sender, EventArgs e)
        {
            //获取数据

            //绑定datagrid
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string riqi = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            //获取数据
            DataTable dtYuce = new KaijiangDAL().GetKaijiangDateYuce1(riqi);
            //绑定datagrid
            dataGridView1.DataSource = dtYuce;
            //设置颜色
            //row.Cells[2].Style.BackColor = Color.Red;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                string spfresult = "-1";
                if (row.Cells["spfresult"].Value != null)
                {
                    spfresult = row.Cells["spfresult"].Value.ToString();
                }

                switch (spfresult)
                {
                    case "3": 
                        row.Cells[5].Style.BackColor = Color.Red; 
                        break;
                    case "1": 
                        row.Cells[6].Style.BackColor = Color.Yellow;
                        break;
                    case "0": 
                        row.Cells[7].Style.BackColor = Color.Blue;
                        break;

                }

                string rqspfresult = "-1";
                if (row.Cells["rqspfresult"].Value != null)
                {
                    rqspfresult = row.Cells["rqspfresult"].Value.ToString();
                }

                switch (rqspfresult)
                {
                    case "3":
                        row.Cells[8].Style.BackColor = Color.Red;
                        break;
                    case "1":
                        row.Cells[9].Style.BackColor = Color.Yellow;
                        break;
                    case "0":
                        row.Cells[10].Style.BackColor = Color.Green;
                        break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string riqi = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            //获取数据
            DataTable dtYuce = new KaijiangDAL().GetKaijiangDateYuce2(riqi);
            //绑定datagrid
            dataGridView1.DataSource = dtYuce;

            //设置grid颜色
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                string spfresult = "-1";
                if (row.Cells["spfresult"].Value != null)
                {
                    spfresult = row.Cells["spfresult"].Value.ToString();
                }

                switch (spfresult)
                {
                    case "3":
                        row.Cells[5].Style.BackColor = Color.Red;
                        break;
                    case "1":
                        row.Cells[6].Style.BackColor = Color.Yellow;
                        break;
                    case "0":
                        row.Cells[7].Style.BackColor = Color.Blue;
                        break;

                }

                string rqspfresult = "-1";
                if (row.Cells["rqspfresult"].Value != null)
                {
                    rqspfresult = row.Cells["rqspfresult"].Value.ToString();
                }

                switch (rqspfresult)
                {
                    case "3":
                        row.Cells[8].Style.BackColor = Color.Red;
                        break;
                    case "1":
                        row.Cells[9].Style.BackColor = Color.Yellow;
                        break;
                    case "0":
                        row.Cells[10].Style.BackColor = Color.Green;
                        break;
                }
            }


        }
    }
}
