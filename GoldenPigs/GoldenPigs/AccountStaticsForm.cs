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
    public partial class AccountStaticsForm : Form
    {
        public AccountStaticsForm()
        {
            InitializeComponent();
        }

        private void btnChongzhi_Click(object sender, EventArgs e)
        {
            Income income = new Income();
            income.IncomeType = "充值";
            income.Amount = Convert.ToDouble(txtChongzhijine.Text);
            income.OperateTime = DateTime.Now;
            income.Operator = "吴林";
            new IncomeDAL().InsertIncome(income);

            //刷新下页面UI控件
            GetAllAmount();
            BindGrid("全部");
            MessageBox.Show("充值"+ txtChongzhijine.Text +"成功！");
        }

        private void AccountStaticsForm_Load(object sender, EventArgs e)
        {
            GetAllAmount();
            BindGrid("全部");
        }

        private void GetAllAmount()
        {
            IncomeDAL dal = new IncomeDAL();
            double chongzhijine = 
                 dal.GetTotalAmount("充值");
            double goujiangjine = dal.GetTotalAmount("购奖");
            double zhongjiangjine = dal.GetTotalAmount("中奖");
            lblZongjine.Text = chongzhijine.ToString();
            lblGoujiangjine.Text = goujiangjine.ToString();
            lblZhongjiangjine.Text = zhongjiangjine.ToString();
            lblYue.Text = (chongzhijine + zhongjiangjine - goujiangjine).ToString();

        }

        private void lilChongzhijine_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string incomeType = "充值";
            BindGrid(incomeType);
        }

        

        private void lilZhongjiangjine_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string incomeType = "中奖";
            BindGrid(incomeType);
        }

        private void lilGoujiangjine_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string incomeType = "购奖";
            BindGrid(incomeType);
        }

        private void lilYue_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string incomeType = "全部";
            BindGrid(incomeType);
        }
        private void BindGrid(string incomeType)
        {
            DataSet ds = new DataSet();
            ds = new IncomeDAL().GetIncomeDetails(incomeType);
            dgvIncome.DataSource = ds.Tables[0];

        }

    }
}
