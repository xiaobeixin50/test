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
    public partial class XinshuituijiangaopeiForm : Form
    {
        public XinshuituijiangaopeiForm()
        {
            InitializeComponent();
        }
        private Xinshui ConvertToXinshui(DataRow row)
        {

            Xinshui xinshui = new Xinshui();
            xinshui.Riqi  = row["riqi"].ToString();
            //xinshui.Beishu =  Convert.ToInt32(row["beishu"].ToString());
            xinshui.Bianhao1 = row["bianhao1"].ToString();
            xinshui.Liansai1 = row["liansai1"].ToString();
            xinshui.Zhudui1 = row["zhuidui1"].ToString();
            xinshui.Kedui1 = row["kedui1"].ToString();
            xinshui.Rangqiushu1 = row["rangqiushu1"].ToString();
            xinshui.Result1 = row["result1"].ToString();
            xinshui.Bianhao2 = row["bianhao2"].ToString();
            xinshui.Liansai2 = row["liansai2"].ToString();
            xinshui.Zhudui2 = row["zhudui2"].ToString();
            xinshui.Kedui2 = row["kedui2"].ToString();
            xinshui.Rangqiushu2 = row["rangqiushu2"].ToString();
            xinshui.Result2 = row["result2"].ToString();
            //xinshui.RealResult1 = row["realresult1"].ToString();
            //xinshui.RealResult2 = row["realresult2"].ToString();
            //xinshui.RealResultSp1 = row["realresultsp1"].ToString();
            //xinshui.RealResultSp2 = row["realresultsp2"].ToString();
            //xinshui.Lucky = Convert.ToInt32(row["lucky"].ToString());
            //xinshui.Jiangjin = Convert.ToDouble(row["jiangjin"].ToString());
            //xinshui.Exclude = Convert.ToInt32(row["exclude"].ToString());
            xinshui.Operator = "自动导入";
            xinshui.OperateTime = DateTime.Now;
            return xinshui;
        }

        private double GetPeilv(DataRow row, string result)
        {
            double peilv = 0.0;
            switch (result)
            {
                case "3":  peilv = Convert.ToDouble(row["shengsp"]);break;
                case "1":  peilv = Convert.ToDouble(row["pingsp"]); break;
                case "0":  peilv = Convert.ToDouble(row["fusp"]); break;

            }
            return peilv;
        }
        private void btnImportGaopei_Click(object sender, EventArgs e)
        {
            //获取心水推荐的数据
            XinshuiDAL xinshuidal = new XinshuiDAL();
            DataSet dsXinshui = xinshuidal.GetAllXinshui();

            //判断多选的结果中哪个的赔率高
            foreach (DataRow row in dsXinshui.Tables[0].Rows)
            {
                string bianhao1 = row["bianhao1"].ToString();
                string bianhao2 = row["bianhao2"].ToString();
                string rangqiushu1 = row["rangqiushu1"].ToString();
                string rangqiushu2 = row["rangqiushu2"].ToString();
                string result1 = row["result1"].ToString();
                string result2 = row["result2"].ToString();
                string riqi = row["riqi"].ToString();
                //如果是单选，则不需要判断，只需要添加即可
                Xinshui xinshui = ConvertToXinshui(row);
                if (result1.Length == 2)
                {
                    DataSet ds = xinshuidal.GetPeilv(riqi, bianhao1);
                    foreach (DataRow peilvrow in ds.Tables[0].Rows)
                    {
                        string rangqiushu = peilvrow["rangqiu"].ToString();
                        string sonResult1 = result1.Substring(0, 1);
                        string sonResult2 = result1.Substring(1, 1);
                        if (rangqiushu == "0" && rangqiushu1 == "0" || rangqiushu != "0" && rangqiushu1 != "0")
                        {
                            double peilv1 = GetPeilv(peilvrow, sonResult1);
                            double peilv2 = GetPeilv(peilvrow, sonResult2);
                            if (peilv1 >= peilv2)
                            {
                                xinshui.Result1 = sonResult1;
                            }
                            else
                            {
                                xinshui.Result1 = sonResult2;
                            }
                        }

                    }
                }
                if (result2.Length == 2)
                {
                    DataSet ds2 = xinshuidal.GetPeilv(riqi, bianhao2);
                    foreach (DataRow peilvrow in ds2.Tables[0].Rows)
                    {
                        string rangqiushu = peilvrow["rangqiu"].ToString();
                        string sonResult1 = result1.Substring(0, 1);
                        string sonResult2 = result1.Substring(1, 1);
                        if (rangqiushu == "0" && rangqiushu2 == "0" || rangqiushu != "0" && rangqiushu2 != "0")
                        {
                            double peilv1 = GetPeilv(peilvrow, sonResult1);
                            double peilv2 = GetPeilv(peilvrow, sonResult2);
                            if (peilv1 >= peilv2)
                            {
                                xinshui.Result2 = sonResult1;
                            }
                            else
                            {
                                xinshui.Result2 = sonResult2;
                            }
                        }

                    }
                }               

                xinshuidal.InsertXinshuiGaopeiWhenNotExist(xinshui);

            }
            //添加到心水推荐高赔表中
            MessageBox.Show("导入成功！");
        }
    }
}
