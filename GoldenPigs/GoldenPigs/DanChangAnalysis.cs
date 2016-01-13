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

namespace GoldenPigs
{
    public partial class DanChangAnalysis : Form
    {
        public DanChangAnalysis()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SoccerSingleDAL dal = new SoccerSingleDAL();
                DataSet ds = dal.SearchDanchang();
                List<SoccerSingleAnalysis> danChangAnalyses = new List<SoccerSingleAnalysis>();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    SoccerSingleAnalysis analysis = new SoccerSingleAnalysis();

                    analysis.riqi = row["riqi"].ToString();
                    analysis.bianhao = row["bianhao"].ToString();
                    analysis.liansai = row["liansai"].ToString();
                    analysis.shengsp = row["shengsp"].ToString();
                    analysis.pingsp = row["pingsp"].ToString();
                    analysis.fusp = row["fusp"].ToString();
                    analysis.rqshengsp = row["rqshengsp"].ToString();
                    analysis.rqpingsp = row["rqpingsp"].ToString();
                    analysis.rqfusp = row["rqfusp"].ToString();
                    analysis.spfresult = row["spfresult"].ToString();
                    analysis.rqspfresult = row["rqspfresult"].ToString();

                    analysis.spfrank = GetSpfRank(analysis);
                    analysis.rqspfrank = GetRqSpfRank(analysis);

                    danChangAnalyses.Add(analysis);
                }

                dataGridView1.DataSource = danChangAnalyses;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }    
        }

        private string GetSpfRank(SoccerSingleAnalysis analysis)
        {

            int rank = 0;
            String tempSp = "0";
            switch (analysis.spfresult)
            {
                case "3":
                    tempSp = analysis.shengsp;
                    break;
                case "1":
                    tempSp = analysis.pingsp;
                    break;
                case "0":
                    tempSp = analysis.fusp;
                    break;

            }
            if (analysis.shengsp.Equals("0"))
            {
                return "0";
            }
            if (Convert.ToDouble(tempSp) >= Convert.ToDouble(analysis.shengsp))
            {
                rank ++;
            }
            if (Convert.ToDouble(tempSp) >= Convert.ToDouble(analysis.pingsp))
            {
                rank++;
            }
            if (Convert.ToDouble(tempSp) >= Convert.ToDouble(analysis.fusp))
            {
                rank++;
            }
            return rank.ToString();
        }

        private string GetRqSpfRank(SoccerSingleAnalysis analysis)
        {

            int rank = 0;
            String tempSp = "0";
            switch (analysis.rqspfresult)
            {
                case "3":
                    tempSp = analysis.rqshengsp;
                    break;
                case "1":
                    tempSp = analysis.rqpingsp;
                    break;
                case "0":
                    tempSp = analysis.rqfusp;
                    break;

            }
            if (analysis.rqshengsp.Equals("0"))
            {
                return "0";
            }
            if (Convert.ToDouble(tempSp) >= Convert.ToDouble(analysis.rqshengsp))
            {
                rank++;
            }
            if (Convert.ToDouble(tempSp) >= Convert.ToDouble(analysis.rqpingsp))
            {
                rank++;
            }
            if (Convert.ToDouble(tempSp) >= Convert.ToDouble(analysis.rqfusp))
            {
                rank++;
            }
            return rank.ToString();
        }
    }
}
