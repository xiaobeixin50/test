using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GoldenPigs.ZnewForms;

namespace GoldenPigs
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        private void 批量导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new btnImportDataFromUrlForm().ShowDialog();
        }

        private void 竞彩足球ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ZhongjiangchaxunForm().ShowDialog();
        }

        private void 竞彩足球ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new TouzhuForm().ShowDialog();
        }

        private void 导入积分榜ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ImportDataTestForm().ShowDialog();
        }

        private void 专家套餐ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TaocanTouzhuForm().ShowDialog();
        }

        private void 套餐数据分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new DataAnalysisForm().ShowDialog();

        }

        private void 我的账户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AccountStaticsForm().ShowDialog();
        }

        private void 彩客网预测ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new YuceDataAnalysisForm().ShowDialog();
        }

        private void 彩客网自动投注ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new YuceTouzhuForm().ShowDialog();
        }

        private void 添加心水推荐ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new XinshuituijianForm().ShowDialog();
        }

        private void 请支持ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AbountForm().ShowDialog();
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 心水账户ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 彩客网预测查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new YucePeilvAnalysisForm().ShowDialog();
        }

        private void 心水推荐高赔管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new XinshuituijiangaopeiForm().ShowDialog();
        }

        private void 胜平负单场ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 时时彩ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 一定牛足彩ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new AppYuceForm().ShowDialog();
        }

        private void 开奖赔率统计分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new KaijiangPeilvStats().ShowDialog();
        }

        private void 单场数据分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new DanChangAnalysis().ShowDialog();
        }
    }
}
