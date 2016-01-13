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
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Taocan taocan = new Taocan();
            taocan.Qishu = 20140803;
            taocan.Riqi = new DateTime(2014, 8, 3);
            taocan.Huibaolv = 225;
            taocan.Gailv = "80+%";
            taocan.Jiangjin = 500;
            taocan.Lucky = 0;
            taocan.Type = "专家推荐套餐";
            taocan.Remark = "test"; 
            new TaocanDAL().InsertTaocan(taocan);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //TaocanDetail detail = new TaocanDetail();
            //detail.Beishu = 12;
            //detail.Jiangjin = 200;
            //detail.Kedui1 = "客队1";
            //detail.Kedui2 = "客队2";
            //detail.Keduipaiming1 = 1;
            //detail.Keduipaiming2 = 2;
            //detail.Liansai1 = 1;
            //detail.Liansai2 = 2;
            //detail.Lucky = 0;
            //detail.TaocanID = 1;
            //detail.Zhudui1 = "主队1";
            //detail.Zhudui2 = "主队2";
            //detail.Zhuduipaiming1 = 1;
            //detail.Zhuduipaiming2 = 2;

            //new TaocanDetailDAL().InsertTaocanDetail(detail);
        }
    }
}
