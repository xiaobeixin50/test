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
    public partial class YuceTouzhuForm : Form
    {
        public YuceTouzhuForm()
        {
            InitializeComponent();
        }

        private void btnTuijian_Click(object sender, EventArgs e)
        {
            //推荐算法是什么？
            //1.北单和竞彩都预测的比赛
            //2.赔率最高的比赛
            DateTime date = dtpTouzhushijian.Value.Date;
            DataSet dsYuce = new YucerawdataDAL().GetYucerawdataAnalysis(date);

            PeilvDAL dal = new PeilvDAL();
            string riqi = dtpTouzhushijian.Value.Date.ToString("yyyy-MM-dd");
            string liansai = "全部";
            DataSet ds = dal.SearchPeilv(riqi, liansai);
            dgvTuijian.DataSource = ds.Tables[0];

            foreach (DataRow row in dsYuce.Tables[0].Rows)
            {
                //string kedui = row["kedui"].ToString();
                //string zhudui = row["zhudui"].ToString();
                string kedui = row["keduireal"].ToString();
                string zhudui = row["zhuduireal"].ToString();
                string yucespf = row["yucespf"].ToString();
                string hasrangqiu = row["hasrangqiu"].ToString();
                string rangqiushu = row["rangqiushu"].ToString();

                foreach (DataGridViewRow gridRow in dgvTuijian.Rows)
                {
                    if (gridRow.Index == dgvTuijian.Rows.Count - 1)
                    {
                        continue;
                    }
                    if (gridRow.Cells[2].Value.ToString().Equals(zhudui) || gridRow.Cells[3].Value.ToString().Equals(kedui))
                    {
                        if (hasrangqiu.Equals("0") && gridRow.Cells[4].Value.ToString().Equals("0"))
                        {
                            char[] chars = yucespf.ToCharArray();
                            foreach (char ch in chars)
                            {
                                if (ch <= '9' && ch >= '0')
                                {
                                    switch (ch)
                                    {
                                        case '0': SetCellColor(gridRow.Cells[7]); break;
                                        case '1': SetCellColor(gridRow.Cells[6]); break;
                                        case '3': SetCellColor(gridRow.Cells[5]); break;

                                    }
                                }
                            }
                        }
                        else if (hasrangqiu.Equals("1") && !gridRow.Cells[4].Value.ToString().Equals("0"))
                        {
                            char[] chars = yucespf.ToCharArray();
                            foreach (char ch in chars)
                            {
                                if (ch <= '9' && ch >= '0')
                                {
                                    switch (ch)
                                    {
                                        case '0': SetCellColor(gridRow.Cells[7]); break;
                                        case '1': SetCellColor(gridRow.Cells[6]); break;
                                        case '3': SetCellColor(gridRow.Cells[5]); break;

                                    }
                                }
                            }
                        }

                    }
                }



            }
        }

        private void btnAutoTouzhu_Click(object sender, EventArgs e)
        {
            //按自然顺序二串一
            if (checkBox1.Checked)
            {
                TouzhuByNature();
            }
            

            //按sp顺序二串一
            if (checkBox2.Checked)
            {
                TouzhuBySp();
            }
            

            //只选择包含3的去投注
            if (checkBox3.Checked)
            {
                TouzhuByOnly3();
            }

            //只选择包含0的去投注
            if (checkBox4.Checked)
            {
                TouzhuByOnly0();
            }

            //只选择名人的去投注
            if (checkBox5.Checked)
            {
                //需要重新绑定grid
                //然后按照自然顺序投注
                TouzhuByMingren();
            }

            MessageBox.Show("自动投注成功！");
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

        private void TouzhuByNature()
        {
            //按自然顺序二串一
            Dictionary<String, List<SelectedTouzhu>> dic = new Dictionary<string, List<SelectedTouzhu>>();
            SelectedTouzhu touzhu = null;
            foreach (DataGridViewRow row in dgvTuijian.Rows)
            {
                if (dic.Keys.Count == 2)
                {
                    SaveTouzhu(dic, 2, "竞彩足球-自动投注1");
                    dic = new Dictionary<string, List<SelectedTouzhu>>();
                }
                bool flag = false;
                if (row.Cells[5].Style.BackColor == Color.Red || row.Cells[5].Style.BackColor == Color.Blue)
                {
                    touzhu = new SelectedTouzhu();
                    touzhu.Zhudui = row.Cells[2].Value.ToString();
                    touzhu.Kedui = row.Cells[3].Value.ToString();
                    touzhu.Rangqiu = row.Cells[4].Value.ToString();
                    touzhu.Result = "胜";
                    touzhu.Peilv = Convert.ToDouble(row.Cells[5].Value);
                    touzhu.Riqi = row.Cells[8].Value.ToString();
                    flag = true;
                    if (flag)
                    {
                        String key = row.Cells[2].Value.ToString() + row.Cells[3].Value.ToString() + row.Cells[8].Value.ToString();
                        List<SelectedTouzhu> sel = null;
                        if (dic.TryGetValue(key, out sel))
                        {
                            sel.Add(touzhu);

                        }
                        else
                        {
                            sel = new List<SelectedTouzhu>();
                            sel.Add(touzhu);
                            dic[key] = sel;
                        }
                    }
                }
                if (row.Cells[6].Style.BackColor == Color.Red || row.Cells[6].Style.BackColor == Color.Blue)
                {
                    touzhu = new SelectedTouzhu();
                    touzhu.Zhudui = row.Cells[2].Value.ToString();
                    touzhu.Kedui = row.Cells[3].Value.ToString();
                    touzhu.Rangqiu = row.Cells[4].Value.ToString();
                    touzhu.Result = "平";
                    touzhu.Peilv = Convert.ToDouble(row.Cells[6].Value);
                    touzhu.Riqi = row.Cells[8].Value.ToString();
                    flag = true;
                    if (flag)
                    {
                        String key = row.Cells[2].Value.ToString() + row.Cells[3].Value.ToString() + row.Cells[8].Value.ToString();
                        List<SelectedTouzhu> sel = null;
                        if (dic.TryGetValue(key, out sel))
                        {
                            sel.Add(touzhu);

                        }
                        else
                        {
                            sel = new List<SelectedTouzhu>();
                            sel.Add(touzhu);
                            dic[key] = sel;
                        }
                    }
                }
                if (row.Cells[7].Style.BackColor == Color.Red || row.Cells[7].Style.BackColor == Color.Blue)
                {
                    touzhu = new SelectedTouzhu();
                    touzhu.Zhudui = row.Cells[2].Value.ToString();
                    touzhu.Kedui = row.Cells[3].Value.ToString();
                    touzhu.Rangqiu = row.Cells[4].Value.ToString();
                    touzhu.Result = "负";
                    touzhu.Peilv = Convert.ToDouble(row.Cells[7].Value);
                    touzhu.Riqi = row.Cells[8].Value.ToString();
                    flag = true;
                    if (flag)
                    {
                        String key = row.Cells[2].Value.ToString() + row.Cells[3].Value.ToString() + row.Cells[8].Value.ToString();
                        List<SelectedTouzhu> sel = null;
                        if (dic.TryGetValue(key, out sel))
                        {
                            sel.Add(touzhu);

                        }
                        else
                        {
                            sel = new List<SelectedTouzhu>();
                            sel.Add(touzhu);
                            dic[key] = sel;
                        }
                    }
                }
            }
        }
        private void TouzhuBySp()
        {
            //按sp顺序二串一

            SortedList<string, SelectedTouzhu> touzhuList = new SortedList<string, SelectedTouzhu>();
            SelectedTouzhu touzhu = null;
            foreach (DataGridViewRow row in dgvTuijian.Rows)
            {

                if (row.Cells[5].Style.BackColor == Color.Red || row.Cells[5].Style.BackColor == Color.Blue)
                {
                    touzhu = new SelectedTouzhu();
                    touzhu.Zhudui = row.Cells[2].Value.ToString();
                    touzhu.Kedui = row.Cells[3].Value.ToString();
                    touzhu.Rangqiu = row.Cells[4].Value.ToString();
                    touzhu.Result = "胜";
                    touzhu.Peilv = Convert.ToDouble(row.Cells[5].Value);
                    touzhu.Riqi = row.Cells[8].Value.ToString();
                    touzhuList.Add(touzhu.Peilv + touzhu.Zhudui, touzhu);
                }
                if (row.Cells[6].Style.BackColor == Color.Red || row.Cells[6].Style.BackColor == Color.Blue)
                {
                    touzhu = new SelectedTouzhu();
                    touzhu.Zhudui = row.Cells[2].Value.ToString();
                    touzhu.Kedui = row.Cells[3].Value.ToString();
                    touzhu.Rangqiu = row.Cells[4].Value.ToString();
                    touzhu.Result = "平";
                    touzhu.Peilv = Convert.ToDouble(row.Cells[6].Value);
                    touzhu.Riqi = row.Cells[8].Value.ToString();

                    touzhuList.Add(touzhu.Peilv + touzhu.Zhudui, touzhu);
                }
                if (row.Cells[7].Style.BackColor == Color.Red || row.Cells[7].Style.BackColor == Color.Blue)
                {
                    touzhu = new SelectedTouzhu();
                    touzhu.Zhudui = row.Cells[2].Value.ToString();
                    touzhu.Kedui = row.Cells[3].Value.ToString();
                    touzhu.Rangqiu = row.Cells[4].Value.ToString();
                    touzhu.Result = "负";
                    touzhu.Peilv = Convert.ToDouble(row.Cells[7].Value);
                    touzhu.Riqi = row.Cells[8].Value.ToString();

                    touzhuList.Add(touzhu.Peilv + touzhu.Zhudui, touzhu);
                }

            }

            SelectedTouzhu p1, p2;
            p1 = null;
            p2 = null;
            foreach (SelectedTouzhu tou in touzhuList.Values)
            {
                p1 = p2;
                p2 = tou;
                if (p1 != null && p2 != null && p1.Zhudui != p2.Zhudui)
                {
                    Dictionary<String, List<SelectedTouzhu>> dic = new Dictionary<string, List<SelectedTouzhu>>();
                    String key1 = p1.Zhudui + p1.Kedui + p1.Riqi;
                    List<SelectedTouzhu> sel1 = null;
                    sel1 = new List<SelectedTouzhu>();
                    sel1.Add(p1);
                    dic[key1] = sel1;
                    String key2 = p2.Zhudui + p2.Kedui + p2.Riqi;
                    List<SelectedTouzhu> sel2 = null;
                    sel2 = new List<SelectedTouzhu>();
                    sel2.Add(p2);
                    dic[key2] = sel2;
                    SaveTouzhu(dic, 2, "竞彩足球-自动投注2");
                    p1 = null;
                    p2 = null;
                }
            }


        }

        private void TouzhuByOnly3()
        {
            Dictionary<String, List<SelectedTouzhu>> dic = new Dictionary<string, List<SelectedTouzhu>>();
            SelectedTouzhu touzhu = null;
            foreach (DataGridViewRow row in dgvTuijian.Rows)
            {
                if (dic.Keys.Count == 2)
                {
                    SaveTouzhu(dic, 2, "竞彩足球-自动投注3");
                    dic = new Dictionary<string, List<SelectedTouzhu>>();
                }

               
                bool flag = false;
                if (row.Cells[5].Style.BackColor == Color.Red || row.Cells[5].Style.BackColor == Color.Blue)
                {
                    touzhu = new SelectedTouzhu();
                    touzhu.Zhudui = row.Cells[2].Value.ToString();
                    touzhu.Kedui = row.Cells[3].Value.ToString();
                    touzhu.Rangqiu = row.Cells[4].Value.ToString();
                    touzhu.Result = "胜";
                    touzhu.Peilv = Convert.ToDouble(row.Cells[5].Value);
                    touzhu.Riqi = row.Cells[8].Value.ToString();
                    flag = true;
                    if (flag)
                    {
                        String key = row.Cells[2].Value.ToString() + row.Cells[3].Value.ToString() + row.Cells[8].Value.ToString();
                        List<SelectedTouzhu> sel = null;
                        if (dic.TryGetValue(key, out sel))
                        {
                            sel.Add(touzhu);

                        }
                        else
                        {
                            sel = new List<SelectedTouzhu>();
                            sel.Add(touzhu);
                            dic[key] = sel;
                        }
                    }
                }
                //if (row.Cells[6].Style.BackColor == Color.Red || row.Cells[6].Style.BackColor == Color.Blue)
                //{
                //    touzhu = new SelectedTouzhu();
                //    touzhu.Zhudui = row.Cells[2].Value.ToString();
                //    touzhu.Kedui = row.Cells[3].Value.ToString();
                //    touzhu.Rangqiu = row.Cells[4].Value.ToString();
                //    touzhu.Result = "平";
                //    touzhu.Peilv = Convert.ToDouble(row.Cells[6].Value);
                //    touzhu.Riqi = row.Cells[8].Value.ToString();
                //    flag = true;
                //    if (flag)
                //    {
                //        String key = row.Cells[2].Value.ToString() + row.Cells[3].Value.ToString() + row.Cells[8].Value.ToString();
                //        List<SelectedTouzhu> sel = null;
                //        if (dic.TryGetValue(key, out sel))
                //        {
                //            sel.Add(touzhu);

                //        }
                //        else
                //        {
                //            sel = new List<SelectedTouzhu>();
                //            sel.Add(touzhu);
                //            dic[key] = sel;
                //        }
                //    }
                //}
                //if (row.Cells[7].Style.BackColor == Color.Red || row.Cells[7].Style.BackColor == Color.Blue)
                //{
                //    touzhu = new SelectedTouzhu();
                //    touzhu.Zhudui = row.Cells[2].Value.ToString();
                //    touzhu.Kedui = row.Cells[3].Value.ToString();
                //    touzhu.Rangqiu = row.Cells[4].Value.ToString();
                //    touzhu.Result = "负";
                //    touzhu.Peilv = Convert.ToDouble(row.Cells[7].Value);
                //    touzhu.Riqi = row.Cells[8].Value.ToString();
                //    flag = true;
                //    if (flag)
                //    {
                //        String key = row.Cells[2].Value.ToString() + row.Cells[3].Value.ToString() + row.Cells[8].Value.ToString();
                //        List<SelectedTouzhu> sel = null;
                //        if (dic.TryGetValue(key, out sel))
                //        {
                //            sel.Add(touzhu);

                //        }
                //        else
                //        {
                //            sel = new List<SelectedTouzhu>();
                //            sel.Add(touzhu);
                //            dic[key] = sel;
                //        }
                //    }
                //}
            }
        }
        private void TouzhuByOnly0()
        {
            Dictionary<String, List<SelectedTouzhu>> dic = new Dictionary<string, List<SelectedTouzhu>>();
            SelectedTouzhu touzhu = null;
            foreach (DataGridViewRow row in dgvTuijian.Rows)
            {
                if (dic.Keys.Count == 2)
                {
                    SaveTouzhu(dic, 2, "竞彩足球-自动投注4");
                    dic = new Dictionary<string, List<SelectedTouzhu>>();
                }
                bool flag = false;
                //if (row.Cells[5].Style.BackColor == Color.Red || row.Cells[5].Style.BackColor == Color.Blue)
                //{
                //    touzhu = new SelectedTouzhu();
                //    touzhu.Zhudui = row.Cells[2].Value.ToString();
                //    touzhu.Kedui = row.Cells[3].Value.ToString();
                //    touzhu.Rangqiu = row.Cells[4].Value.ToString();
                //    touzhu.Result = "胜";
                //    touzhu.Peilv = Convert.ToDouble(row.Cells[5].Value);
                //    touzhu.Riqi = row.Cells[8].Value.ToString();
                //    flag = true;
                //    if (flag)
                //    {
                //        String key = row.Cells[2].Value.ToString() + row.Cells[3].Value.ToString() + row.Cells[8].Value.ToString();
                //        List<SelectedTouzhu> sel = null;
                //        if (dic.TryGetValue(key, out sel))
                //        {
                //            sel.Add(touzhu);

                //        }
                //        else
                //        {
                //            sel = new List<SelectedTouzhu>();
                //            sel.Add(touzhu);
                //            dic[key] = sel;
                //        }
                //    }
                //}
                //if (row.Cells[6].Style.BackColor == Color.Red || row.Cells[6].Style.BackColor == Color.Blue)
                //{
                //    touzhu = new SelectedTouzhu();
                //    touzhu.Zhudui = row.Cells[2].Value.ToString();
                //    touzhu.Kedui = row.Cells[3].Value.ToString();
                //    touzhu.Rangqiu = row.Cells[4].Value.ToString();
                //    touzhu.Result = "平";
                //    touzhu.Peilv = Convert.ToDouble(row.Cells[6].Value);
                //    touzhu.Riqi = row.Cells[8].Value.ToString();
                //    flag = true;
                //    if (flag)
                //    {
                //        String key = row.Cells[2].Value.ToString() + row.Cells[3].Value.ToString() + row.Cells[8].Value.ToString();
                //        List<SelectedTouzhu> sel = null;
                //        if (dic.TryGetValue(key, out sel))
                //        {
                //            sel.Add(touzhu);

                //        }
                //        else
                //        {
                //            sel = new List<SelectedTouzhu>();
                //            sel.Add(touzhu);
                //            dic[key] = sel;
                //        }
                //    }
                //}
                if (row.Cells[7].Style.BackColor == Color.Red || row.Cells[7].Style.BackColor == Color.Blue)
                {
                    touzhu = new SelectedTouzhu();
                    touzhu.Zhudui = row.Cells[2].Value.ToString();
                    touzhu.Kedui = row.Cells[3].Value.ToString();
                    touzhu.Rangqiu = row.Cells[4].Value.ToString();
                    touzhu.Result = "负";
                    touzhu.Peilv = Convert.ToDouble(row.Cells[7].Value);
                    touzhu.Riqi = row.Cells[8].Value.ToString();
                    flag = true;
                    if (flag)
                    {
                        String key = row.Cells[2].Value.ToString() + row.Cells[3].Value.ToString() + row.Cells[8].Value.ToString();
                        List<SelectedTouzhu> sel = null;
                        if (dic.TryGetValue(key, out sel))
                        {
                            sel.Add(touzhu);

                        }
                        else
                        {
                            sel = new List<SelectedTouzhu>();
                            sel.Add(touzhu);
                            dic[key] = sel;
                        }
                    }
                }
            }
        }

        private void TouzhuByMingren()
        {
            //重新绑定 
            DateTime date = dtpTouzhushijian.Value.Date;
            DataSet dsYuce = new YucerawdataDAL().GetYucerawdataAnalysisHasMingRen(date);

            PeilvDAL dal = new PeilvDAL();
            string riqi = dtpTouzhushijian.Value.Date.ToString("yyyy-MM-dd");
            string liansai = "全部";
            DataSet ds = dal.SearchPeilv(riqi, liansai);
            dgvTuijian.DataSource = ds.Tables[0];

            foreach (DataRow row in dsYuce.Tables[0].Rows)
            {
                //string kedui = row["kedui"].ToString();
                //string zhudui = row["zhudui"].ToString();
                string kedui = row["keduireal"].ToString();
                string zhudui = row["zhuduireal"].ToString();
                string yucespf = row["yucespf"].ToString();
                string hasrangqiu = row["hasrangqiu"].ToString();
                string rangqiushu = row["rangqiushu"].ToString();

                foreach (DataGridViewRow gridRow in dgvTuijian.Rows)
                {
                    if (gridRow.Index == dgvTuijian.Rows.Count - 1)
                    {
                        continue;
                    }
                    if (gridRow.Cells[2].Value.ToString().Equals(zhudui) || gridRow.Cells[3].Value.ToString().Equals(kedui))
                    {
                        if (hasrangqiu.Equals("0") && gridRow.Cells[4].Value.ToString().Equals("0"))
                        {
                            char[] chars = yucespf.ToCharArray();
                            foreach (char ch in chars)
                            {
                                if (ch <= '9' && ch >= '0')
                                {
                                    switch (ch)
                                    {
                                        case '0': SetCellColor(gridRow.Cells[7]); break;
                                        case '1': SetCellColor(gridRow.Cells[6]); break;
                                        case '3': SetCellColor(gridRow.Cells[5]); break;

                                    }
                                }
                            }
                        }
                        else if (hasrangqiu.Equals("1") && !gridRow.Cells[4].Value.ToString().Equals("0"))
                        {
                            char[] chars = yucespf.ToCharArray();
                            foreach (char ch in chars)
                            {
                                if (ch <= '9' && ch >= '0')
                                {
                                    switch (ch)
                                    {
                                        case '0': SetCellColor(gridRow.Cells[7]); break;
                                        case '1': SetCellColor(gridRow.Cells[6]); break;
                                        case '3': SetCellColor(gridRow.Cells[5]); break;

                                    }
                                }
                            }
                        }

                    }
                }

            }
            TouzhuByNatureForMingren(); 
        }

        private void TouzhuByNatureForMingren()
        {
            //按自然顺序二串一
            Dictionary<String, List<SelectedTouzhu>> dic = new Dictionary<string, List<SelectedTouzhu>>();
            SelectedTouzhu touzhu = null;
            foreach (DataGridViewRow row in dgvTuijian.Rows)
            {
                if (dic.Keys.Count == 2)
                {
                    SaveTouzhu(dic, 2, "竞彩足球-自动投注5");
                    dic = new Dictionary<string, List<SelectedTouzhu>>();
                }
                bool flag = false;
                if (row.Cells[5].Style.BackColor == Color.Red || row.Cells[5].Style.BackColor == Color.Blue)
                {
                    touzhu = new SelectedTouzhu();
                    touzhu.Zhudui = row.Cells[2].Value.ToString();
                    touzhu.Kedui = row.Cells[3].Value.ToString();
                    touzhu.Rangqiu = row.Cells[4].Value.ToString();
                    touzhu.Result = "胜";
                    touzhu.Peilv = Convert.ToDouble(row.Cells[5].Value);
                    touzhu.Riqi = row.Cells[8].Value.ToString();
                    flag = true;
                    if (flag)
                    {
                        String key = row.Cells[2].Value.ToString() + row.Cells[3].Value.ToString() + row.Cells[8].Value.ToString();
                        List<SelectedTouzhu> sel = null;
                        if (dic.TryGetValue(key, out sel))
                        {
                            sel.Add(touzhu);

                        }
                        else
                        {
                            sel = new List<SelectedTouzhu>();
                            sel.Add(touzhu);
                            dic[key] = sel;
                        }
                    }
                }
                if (row.Cells[6].Style.BackColor == Color.Red || row.Cells[6].Style.BackColor == Color.Blue)
                {
                    touzhu = new SelectedTouzhu();
                    touzhu.Zhudui = row.Cells[2].Value.ToString();
                    touzhu.Kedui = row.Cells[3].Value.ToString();
                    touzhu.Rangqiu = row.Cells[4].Value.ToString();
                    touzhu.Result = "平";
                    touzhu.Peilv = Convert.ToDouble(row.Cells[6].Value);
                    touzhu.Riqi = row.Cells[8].Value.ToString();
                    flag = true;
                    if (flag)
                    {
                        String key = row.Cells[2].Value.ToString() + row.Cells[3].Value.ToString() + row.Cells[8].Value.ToString();
                        List<SelectedTouzhu> sel = null;
                        if (dic.TryGetValue(key, out sel))
                        {
                            sel.Add(touzhu);

                        }
                        else
                        {
                            sel = new List<SelectedTouzhu>();
                            sel.Add(touzhu);
                            dic[key] = sel;
                        }
                    }
                }
                if (row.Cells[7].Style.BackColor == Color.Red || row.Cells[7].Style.BackColor == Color.Blue)
                {
                    touzhu = new SelectedTouzhu();
                    touzhu.Zhudui = row.Cells[2].Value.ToString();
                    touzhu.Kedui = row.Cells[3].Value.ToString();
                    touzhu.Rangqiu = row.Cells[4].Value.ToString();
                    touzhu.Result = "负";
                    touzhu.Peilv = Convert.ToDouble(row.Cells[7].Value);
                    touzhu.Riqi = row.Cells[8].Value.ToString();
                    flag = true;
                    if (flag)
                    {
                        String key = row.Cells[2].Value.ToString() + row.Cells[3].Value.ToString() + row.Cells[8].Value.ToString();
                        List<SelectedTouzhu> sel = null;
                        if (dic.TryGetValue(key, out sel))
                        {
                            sel.Add(touzhu);

                        }
                        else
                        {
                            sel = new List<SelectedTouzhu>();
                            sel.Add(touzhu);
                            dic[key] = sel;
                        }
                    }
                }
            }
        }
        private void SaveTouzhu(Dictionary<string, List<SelectedTouzhu>> dic, int chuanShu, string type)
        {
            int arrayCount = dic.Keys.Count;
            int[] peilvCount = new int[arrayCount];
            List<SelectedTouzhu>[] selectedTouzhus = new List<SelectedTouzhu>[arrayCount];
            int counter = 0;
            int touru = 1;
            foreach (String key in dic.Keys)
            {
                peilvCount[counter] = dic[key].Count;
                selectedTouzhus[counter] = dic[key];
                counter++;
                touru = touru * dic[key].Count;
            }

            double maxNumber = Math.Pow(10, arrayCount);
            int chuan = chuanShu;

            long batchid = Convert.ToInt64(DateTime.Now.ToString("MMddhhmmss") + DateTime.Now.Millisecond);

            for (int i = 0; i < maxNumber; i++)
            {
                int[] digits = new int[arrayCount];
                if (GetNoZeroCount(i, ref digits) == chuan)
                {
                    //所在位置不大于对应数组的值
                    if (NotLargeThanArray(i, peilvCount))
                    {
                        List<SelectedTouzhu> composite = new List<SelectedTouzhu>();
                        for (int j = 0; j < digits.Length; j++)
                        {
                            int digit = digits[j];
                            if (digit != 0)
                            {
                                composite.Add(selectedTouzhus[j][digit - 1]);
                            }
                        }
                        //将组合数据保存在数据库中
                        string riqi = "";
                        string zhudui = "";
                        string kedui = "";
                        string result = "";
                        string peilv = "";
                        string beishu = "";
                        string rangqiu = "";
                        string jiangjin = "";
                        string Operator = "";
                        string operatetime = "";

                        foreach (SelectedTouzhu tou in composite)
                        {
                            riqi += tou.Riqi + ",";
                            zhudui += tou.Zhudui + ",";
                            kedui += tou.Kedui + ",";
                            result += tou.Result + ",";
                            peilv += tou.Peilv + ",";
                            rangqiu += tou.Rangqiu + ",";

                        }
                        zhudui = zhudui.Trim(',');
                        kedui = kedui.Trim(',');
                        result = result.Trim(',');
                        peilv = peilv.Trim(',');
                        rangqiu = rangqiu.Trim(',');
                        riqi = riqi.Trim(',');
                        TouzhuSpf touspf = new TouzhuSpf();
                        touspf.Beishu = 1000;
                        touspf.Lucky = -1; //-1表示未验证是否中奖
                        touspf.Operator = "吴林";
                        touspf.OperateTime = DateTime.Now;
                        touspf.Jiangjin = "0";
                        touspf.Zhudui = zhudui;
                        touspf.Kedui = kedui;
                        touspf.Result = result;
                        touspf.Peilv = peilv;
                        touspf.Rangqiu = rangqiu;
                        touspf.Riqi = riqi;

                        touspf.BatchID = batchid;
                        new TouzhuSpfDAL().InsertTouzhuSpf(touspf);
                    }
                }
            }

            //这里插入income数据
            Income income = new Income();
            //income.Amount = Convert.ToDouble(txtTouru.Text);
            income.Amount = touru * 1000.0 * 2;
            income.IncomeType = "购奖";
            income.OperateTime = DateTime.Now;
            income.Operator = "吴林";

            income.TouzhuType = type;
            income.TouzhuID = batchid;
            new IncomeDAL().InsertIncome(income);


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
