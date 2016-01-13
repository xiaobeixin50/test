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
    public partial class YuceDataAnalysisForm : Form
    {
        public YuceDataAnalysisForm()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime touzhushijian = dtpTouzhushijian.Value.Date;
            BindGrid(touzhushijian);
        }

        
        private void SetCellColor(DataGridViewCell cell)
        {
            if (cell.Style.BackColor == Color.Red)
            {
                cell.Style.BackColor = Color.Blue;
            }
            else
            {
                cell.Style.BackColor = Color.Red;
            }
        }
        private void BindGrid()
        {
            DataSet ds = new YucerawdataDAL().GetAllYuceAnalysis();
            dgvResult.DataSource = ds.Tables[0];
            SetGridColor();
        }
        private void BindGrid(DateTime touzhushijian)
        {
            DataSet ds = new YucerawdataDAL().GetYucerawdataAnalysis(touzhushijian);
            dgvResult.DataSource = ds.Tables[0];

            SetGridColor();
        }
        private void SetGridColor()
        {
            foreach(DataGridViewRow row in dgvResult.Rows)
            {
                if (row.Cells["bianhao"].Value != null && row.Cells["bianhao"].Value != DBNull.Value)
                {
                    int lucky = Convert.ToInt32(row.Cells["lucky"].Value);
                    if (lucky == 1)
                    {
                        string hasranqiu = row.Cells["hasrangqiu"].Value.ToString();
                        if (hasranqiu == "0")
                        {
                            row.Cells["spfsp"].Style.BackColor = Color.Red;
                        }
                        else
                        {
                            row.Cells["rqspfsp"].Style.BackColor = Color.Red;
                        }
                    }
                }
            }
        }
        private void YuceDataAnalysisForm_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void btnUpdateLucky_Click(object sender, EventArgs e)
        {
            //更新预测是否中奖,现在只能更新竞彩足球的结果 
            DateTime touzhushijian = dtpTouzhushijian.Value.Date;

            new YucerawdataDAL().UpdateZhongjiangResult(touzhushijian);

            MessageBox.Show("更新中奖信息成功！");
        }

       


        public bool NotLargeThanArray(int num, int[] count)
        {
            int[] result = new int[count.Length];
            int index = 0;
            while (num != 0)
            {
                int mod = num % 10;
                result[count.Length - 1 - index] = mod;
                index++;
                num = num / 10;

            }
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] > count[i])
                {
                    return false;
                }
            }
            return true;
        }

        public int GetNoZeroCount(int num, ref int[] digits)
        {
            int result = 0;
            int index = 0;
            while (num != 0)
            {
                if (num % 10 != 0)
                {
                    result++;
                    digits[digits.Length - 1 - index] = num % 10;
                }
                num = num / 10;
                index++;
            }
            return result;
        }

        public int GetNoZeroCount(int num)
        {
            int result = 0;
            while (num != 0)
            {
                if (num % 10 != 0)
                {
                    result++;

                }
                num = num / 10;
            }
            return result;
        }
    }
}
