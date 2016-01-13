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
using GoldenPigs.Entity;

namespace GoldenPigs.ZnewForms
{
    public partial class KaijiangPeilvStats : Form
    {
        public KaijiangPeilvStats()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                new KaijiangStatsDAL().RefreshKaijiangStats();
                MessageBox.Show("操作成功！");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
