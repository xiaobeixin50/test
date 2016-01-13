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
using ZedGraph;

namespace GoldenPigs._0630
{
    public partial class DataAnalysisForm : Form
    {
        public DataAnalysisForm()
        {
            InitializeComponent();
        }

        private void btnYuceSuccess_Click(object sender, EventArgs e)
        {
            DataTable dt = StrategyDAL.GetAnalysis1Data();

            Dictionary<String, List<DataRow>> riqiDict = new Dictionary<string,List<DataRow>>();

            //按照日期分类
            foreach (DataRow row in dt.Rows)
            {
                String riqi = row["riqi"].ToString();
                List<DataRow> rows ;
                if (riqiDict.TryGetValue(riqi, out rows))
                {
                    rows.Add(row);
                }
                else
                {
                    rows = new List<DataRow>();
                    rows.Add(row);
                    riqiDict[riqi] = rows;
                }
            }

            List<AnalysisResult> matchshouyi = new List<AnalysisResult>();

            AnalysisResult shouyi = new AnalysisResult();
            double totalCount = 0.0;
            double totalLucky = 0.0;
            //遍历字典
            foreach (KeyValuePair<string, List<DataRow>> keyvalue in riqiDict)
            {
                shouyi = new AnalysisResult();
                shouyi.riqi = keyvalue.Key;

                List<DataRow> rows = keyvalue.Value;
                double sum = 0;
                int luckysum = 0;
                foreach (DataRow row in rows)
                {
                    string lucky = row["lucky"].ToString();
                    string luckycount = row["luckycount"].ToString();
                    if(lucky == "1")
                    {
                        luckysum = Convert.ToInt32(luckycount);
                    }
                    sum += Convert.ToInt32(luckycount);
                }

                shouyi.ratio = luckysum / sum;
                shouyi.yucecount = sum;
                totalCount += sum;
                totalLucky += luckysum;
                shouyi.sumratio = totalLucky / totalCount;
                matchshouyi.Add(shouyi);
            }
            

            dataGridView1.DataSource = matchshouyi;

            PrintPicture(matchshouyi);
        }


        private void PrintPicture(List<AnalysisResult> matches)
        {
            zedGraphControl1.GraphPane.CurveList.Clear();
            GraphPane myPane = zedGraphControl1.GraphPane;
            // get a reference to the GraphPane  
            myPane.XAxis.Type = ZedGraph.AxisType.Date;
            // Set the Titles  
            myPane.Title.Text = "彩票收益曲线";
            myPane.XAxis.Title.Text = "时间";
            myPane.YAxis.Title.Text = "收益";

            // Make up some data arrays based on the Sine function  

            double x, y1, y2;


            PointPairList list1 = new PointPairList();



            // symbols, and "Porsche" in the legend  
            PointPairList list2 = new PointPairList();
            foreach (AnalysisResult match in matches)
            {
                list1.Add((double)new XDate(Convert.ToDateTime(match.riqi)), match.ratio);
                list2.Add((double)new XDate(Convert.ToDateTime(match.riqi)), match.sumratio);
            }

            LineItem myCurve = myPane.AddCurve("单日成功曲线", list1, Color.Red, SymbolType.None);
            LineItem myCurve2 = myPane.AddCurve("累积成功曲线", list2, Color.Blue, SymbolType.None);

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void btnShougailv_Click(object sender, EventArgs e)
        {

        }

       
    }

   
}
