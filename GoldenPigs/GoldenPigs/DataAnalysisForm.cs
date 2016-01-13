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

namespace GoldenPigs
{
    public partial class DataAnalysisForm : Form
    {
        public DataAnalysisForm()
        {
            InitializeComponent();
        }


        private void DataAnalysisForm_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void BindGrid()
        {
            string riqi = dtpStartDate.Value.Date.ToString("yyyy-MM-dd");
            string endDate = dtpEndDate.Value.Date.ToString("yyyy-MM-dd");

            DataSet ds = new TaocanDetailDAL().GetTaocanDetail(riqi, endDate);

            dgvTaocanDetail.DataSource = ds.Tables[0];

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void btnUpdateBianhao_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvTaocanDetail.Rows)
            {
                if (row.Index != dgvTaocanDetail.Rows.Count-1)
                {
                    int detailid = Convert.ToInt32(row.Cells["id"].Value);
                    string zhudui1 = row.Cells["zhudui1"].Value.ToString();
                    string kedui1 = row.Cells["kedui1"].Value.ToString();
                    string zhudui2 = row.Cells["zhudui2"].Value.ToString();
                    string kedui2 = row.Cells["kedui2"].Value.ToString();
                    string bianhao1 = row.Cells["bianhao1"].Value.ToString();
                    string bianhao2 = row.Cells["bianhao2"].Value.ToString();
                    string riqi = Convert.ToDateTime(row.Cells["riqi"].Value).ToString("yyyy-MM-dd");
                    if (string.IsNullOrEmpty(bianhao1) || bianhao1 == "000")
                    {
                        new TaocanDetailDAL().UpdateBianhao(detailid, 1, zhudui1, kedui1, riqi);
                    }
                    if (string.IsNullOrEmpty(bianhao2) || bianhao2 == "000")
                    {
                        new TaocanDetailDAL().UpdateBianhao(detailid, 2, zhudui2, kedui2, riqi);
                    }
                }
                
            }
            MessageBox.Show("更新成功！");
        }




        private void btnUpdateTiaozheng_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvTaocanDetail.Rows)
            {
                if (row.Index != dgvTaocanDetail.Rows.Count - 1)
                {
                    int detailid = Convert.ToInt32(row.Cells["detailid"].Value);
                    string bianhao1 = row.Cells["bianhao1"].Value.ToString();
                    string bianhao2 = row.Cells["bianhao2"].Value.ToString();
                    string riqi = Convert.ToDateTime(row.Cells["riqi"].Value).ToString("yyyy-MM-dd");
                    string zhuduishengfu1 = row.Cells["zhuduishengfu1"].Value.ToString();
                    string zhuduishengfu2 = row.Cells["zhuduishengfu2"].Value.ToString();

                    string bifen1 = GetKaijiangResult(riqi,bianhao1);
                    string bifen2 = GetKaijiangResult(riqi, bianhao2); 
                    if(bifen1 != null && bifen2 != null)
                    {
                        int tiaozheng1 = GetTiaozheng(bifen1, zhuduishengfu1);
                        int tiaozheng2 = GetTiaozheng(bifen2, zhuduishengfu2);
                        UpdateTiaozheng(detailid,tiaozheng1,tiaozheng2);
                    }
                    else
                    {
                        Console.WriteLine("id为"+ detailid+"的套餐明细没有查询开奖结果，日期为"+ riqi);
                    }
                    
                }
            }
            MessageBox.Show("更新成功！");
        }
        private void UpdateTiaozheng(int detailid, int tiaozheng1,int tiaozheng2)
        {
            new TaocanDetailDAL().UpdateTiaozheng(detailid, tiaozheng1, tiaozheng2);
        }
        private string GetKaijiangResult(string riqi,string bianhao)
        {
            return new KaijiangDAL().GetKaijangBifen(riqi, bianhao);
        }
        private int GetTiaozheng(string bifen, string result)
        {
            int index = bifen.IndexOf(':');

            int zhuduijinqiu = Convert.ToInt32(bifen.Substring(0, index));
            int keduijinqiu = Convert.ToInt32(bifen.Substring(index+1));
            int rangqiu = 0;
            if (result.Length != 1)
            {
                rangqiu = Convert.ToInt32(result.Substring(0, result.Length - 1));

            }
            string shengfu = result.Substring(result.Length - 1, 1);
            int resultqiu = 0;
            switch (shengfu)
            {
                case "胜":
                    if (zhuduijinqiu + rangqiu <= keduijinqiu)
                    {
                        resultqiu = keduijinqiu - zhuduijinqiu - rangqiu + 1;
                    }

                    break;
                case "平":
                    resultqiu = keduijinqiu - zhuduijinqiu - rangqiu;
                    break;
                case "负":
                    if (zhuduijinqiu + rangqiu >= keduijinqiu)
                    {
                        resultqiu = keduijinqiu - zhuduijinqiu - rangqiu - 1;
                    }
                    break;
            }
            return resultqiu;
            

        }

        
    }
}
