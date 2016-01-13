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
using ZedGraph;

namespace GoldenPigs._0630
{
    public partial class DataVisualizationForm : Form
    {
        public DataVisualizationForm()
        {
            InitializeComponent();
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            GraphPane myPane = zedGraphControl1.GraphPane;
            // get a reference to the GraphPane  

            // Set the Titles  
            myPane.Title.Text = "彩票收益曲线";
            myPane.XAxis.Title.Text = "时间";
            myPane.YAxis.Title.Text = "收益";

            // Make up some data arrays based on the Sine function  

            double x, y1, y2;


            PointPairList list1 = new PointPairList();

            DataSet bifa = new SuperBifaDAL().GetAllBifa();

            DateTime startDate = dateTimePicker1.Value;

            double threshold = Convert.ToDouble(textBox1.Text);

            double totalTouru = 0.0;

            double totalShouyi = 0.0;

            double totalPrize = 0.0;

            DateTime markedStartDate = new DateTime(1900, 1, 1);

            foreach (DataRow row in bifa.Tables[0].Rows)
            {
                
                //按照一天来算更有意义
                DateTime date = Convert.ToDateTime(row["riqi"].ToString());
                if (startDate > date)
                {
                    continue;
                }
                double first_sp = Convert.ToDouble(row["first_sp"].ToString());
                if (first_sp == 0 || first_sp >= threshold)
                {
                    continue;
                }
                int dae_prize_rank = Convert.ToInt32(row["dae_prize_rank"].ToString());
                double second_sp = Convert.ToDouble(row["second_sp"].ToString());

                totalTouru += 1;
                if (dae_prize_rank == 2)
                {
                    totalPrize += 1 * second_sp;

                }
                
                totalShouyi = totalPrize - totalTouru;
                if (markedStartDate != date)
                {
                    if (markedStartDate.ToString("yyyy-MM-dd") == "1900-01-01")
                    {
                        list1.Add(-1, 0);
                    }else
                    {
                        TimeSpan ts = date - startDate;
                        list1.Add(ts.Days, totalShouyi);
                    }
                    markedStartDate = date;
                   
                }
                

            }

            //PointPairList list2 = new PointPairList();
            
            
            
            
            
            //for (int i = 0; i < 36; i++)
            //{
            //    x = (double)i + 5;
            //    y1 = 1.5 + Math.Sin((double)i * 0.2);
            //    y2 = 3.0 * (1.5 + Math.Sin((double)i * 0.2));
            //    list1.Add(x, y1);
               
            //}

            // Generate a red curve with diamond  

            // symbols, and "Porsche" in the legend  

            LineItem myCurve = myPane.AddCurve("收益曲线", list1, Color.Red, SymbolType.Diamond);

            // Generate a blue curve with circle  

            // symbols, and "Piper" in the legend  

            //LineItem myCurve2 = myPane.AddCurve("Piper", list2, Color.Blue, SymbolType.None);

            // Tell ZedGraph to refigure the  

            // axes since the data have changed  

            // zedGraphControl1.GraphPane.AddCurve(" ", b, d, Color.Green, SymbolType.Triangle);  
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();  
        }
    }
}
