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
    public partial class TouzhuForm : Form
    {
        public TouzhuForm()
        {
            InitializeComponent();
            
            BindGridView();

        }

        private void BindGridView()
        {
            DataSet ds = new PeilvDAL().GetPeilvDataFromKaijiang();
            dgPeilv.DataSource = ds.Tables[0];

            SortedSet<string> liansai = new SortedSet<string>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                liansai.Add(row["liansai"].ToString());

            }

            cbLiansai.Items.Clear();
            cbLiansai.Items.Add("全部");
            foreach (string bisai in liansai)
            {
                cbLiansai.Items.Add(bisai);
            }
            cbLiansai.SelectedIndex = 0;

        }

        private int GetZhushu(Dictionary<string, List<SelectedTouzhu>> dic, int chuanShu)
        {
            int arrayCount = dic.Keys.Count;
            int[] peilvCount = new int[arrayCount];
            int counter = 0;
            int index = 0;
            foreach (String key in dic.Keys)
            {
                peilvCount[index] = dic[key].Count;
                index++;
            }
            double maxNumber = Math.Pow(10, arrayCount);
            int chuan = chuanShu;

            for (int i = 0; i < maxNumber; i++)
            {
                int[] digits = new int[arrayCount];
                if (GetNoZeroCount(i) == chuan)
                {
                    //所在位置不大于对应数组的值
                    if (NotLargeThanArray(i, peilvCount))
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }

        private void SaveTouzhu(Dictionary<string, List<SelectedTouzhu>> dic, int chuanShu, out double minJiangjin,out double maxJiangjin,out int zhushu)
        {
            minJiangjin = double.MaxValue;
            maxJiangjin = double.MinValue;

            

            zhushu = 0;
            int arrayCount = dic.Keys.Count;
            int[] peilvCount = new int[arrayCount];
            List<SelectedTouzhu>[] selectedTouzhus = new List<SelectedTouzhu>[arrayCount];
            int counter = 0;
            foreach (String key in dic.Keys)
            {
                peilvCount[counter] = dic[key].Count;
                selectedTouzhus[counter] = dic[key];
                counter++;
            }

            double maxNumber = Math.Pow(10, arrayCount);
            int chuan = chuanShu;

            for (int i = 0; i < maxNumber; i++)
            {
                int[] digits = new int[arrayCount];
                if (GetNoZeroCount(i, ref digits) == chuan)
                {
                    //所在位置不大于对应数组的值
                    if (NotLargeThanArray(i, peilvCount))
                    {
                        //获得一个排列符合条件
                        zhushu++;

                        List<SelectedTouzhu> composite = new List<SelectedTouzhu>();
                        for (int j = 0; j < digits.Length; j++)
                        {
                            int digit = digits[j];
                            if (digit != 0)
                            {
                                composite.Add(selectedTouzhus[j][digit - 1]);
                            }
                        }
                        double jiangjin = Convert.ToDouble(txtBeishu.Text) * 2;
                        //计算奖金
                        foreach (SelectedTouzhu touzhu in composite)
                        {
                            jiangjin *= touzhu.Peilv;
                        }
                        if (jiangjin > maxJiangjin)
                        {
                            maxJiangjin = jiangjin;
                        }
                        if (jiangjin < minJiangjin)
                        {
                            minJiangjin = jiangjin;
                        }

                    }
                }
            }
        }
        private void SaveTouzhu(Dictionary<string, List<SelectedTouzhu>> dic, int chuanShu, long batchid)
        {
            int arrayCount = dic.Keys.Count;
            int[] peilvCount = new int[arrayCount];
            List<SelectedTouzhu>[] selectedTouzhus = new List<SelectedTouzhu>[arrayCount];
            int counter = 0;
            foreach (String key in dic.Keys)
            {
                peilvCount[counter] = dic[key].Count;
                selectedTouzhus[counter] = dic[key];
                counter++;
            }

            double maxNumber = Math.Pow(10, arrayCount);
            int chuan = chuanShu;

            

            
            int totalTouru = 0;

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
                        touspf.Beishu = Convert.ToInt32(txtBeishu.Text);
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

           


        }
        private void btnTouzhu_Click(object sender, EventArgs e)
        {
            Dictionary<String, List<SelectedTouzhu>> dic = new Dictionary<string, List<SelectedTouzhu>>();
            SelectedTouzhu touzhu = null;
            foreach (DataGridViewRow row in dgPeilv.Rows)
            {
                //为什么要设置这个标志位，看不懂了
                bool flag = false;
                if (row.Cells[5].Selected == true)
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
                if (row.Cells[6].Selected == true)
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
                if (row.Cells[7].Selected == true)
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
                //if (flag)
                //{
                //    String key = row.Cells[2].Value.ToString() + row.Cells[3].Value.ToString()+row.Cells[8].Value.ToString();
                //    List<SelectedTouzhu> sel = null;
                //    if (dic.TryGetValue(key, out sel))
                //    {
                //        sel.Add(touzhu);

                //    }
                //    else
                //    {
                //        sel = new List<SelectedTouzhu>();
                //        sel.Add(touzhu);
                //        dic[key] = sel;
                //    }
                //}
            }

            long batchid = Convert.ToInt64(DateTime.Now.ToString("MMddhhmmss") + DateTime.Now.Millisecond);
            for (int i = 0; i < clbChuanJiang.Items.Count; i++)
            {
                if (clbChuanJiang.GetItemChecked(i))
                {
                    string chuanjiang =  clbChuanJiang.GetItemText(clbChuanJiang.Items[i]);
                    int chuan = Convert.ToInt32(chuanjiang.Substring(0, 1));
                    SaveTouzhu(dic, chuan, batchid);
                }
            }

            //这里插入income数据
            Income income = new Income();
            income.Amount = Convert.ToDouble(txtTouru.Text);
            income.IncomeType = "购奖";
            income.OperateTime = DateTime.Now;
            income.Operator = "吴林";
            income.TouzhuType = "竞彩足球";
            income.TouzhuID = batchid;
            new IncomeDAL().InsertIncome(income);

            MessageBox.Show("投注成功!");
            #region old code
            //int arrayCount = dic.Keys.Count;
            //int[] peilvCount = new int[arrayCount];
            //List<SelectedTouzhu>[] selectedTouzhus = new List<SelectedTouzhu>[arrayCount];
            //int counter = 0;
            //foreach (String key in dic.Keys)
            //{
            //    peilvCount[counter] = dic[key].Count;
            //    selectedTouzhus[counter] = dic[key];
            //    counter++;
            //}

            //double maxNumber = Math.Pow(10, arrayCount);
            //int chuan = 2;
            
            //for (int i = 0; i < maxNumber; i++)
            //{
            //    int[] digits = new int[arrayCount];
            //    if (GetNoZeroCount(i,ref digits) == chuan)
            //    {
            //        //所在位置不大于对应数组的值
            //        if (NotLargeThanArray(i, peilvCount))
            //        {
            //            List<SelectedTouzhu> composite = new List<SelectedTouzhu>();
            //            for (int j = 0; j < digits.Length; j++)
            //            {
            //                int digit = digits[j];
            //                if (digit != 0)
            //                {
            //                    composite.Add(selectedTouzhus[j][digit-1]);
            //                }
            //            }
            //            //将组合数据保存在数据库中
            //            string riqi = "";
            //            string zhudui="";
            //            string kedui="";
            //            string result = "";
            //            string peilv="";
            //            string beishu="";
            //            string rangqiu="";
            //            string jiangjin="";
            //            string Operator = "";
            //            string operatetime = "";

            //            foreach (SelectedTouzhu tou in composite)
            //            {
            //                riqi += tou.Riqi + ",";
            //                zhudui += tou.Zhudui + ",";
            //                kedui += tou.Kedui + ",";
            //                result += tou.Result + ",";
            //                peilv += tou.Peilv + ",";
            //                rangqiu += tou.Rangqiu + ",";

            //            }
            //            zhudui = zhudui.Trim(',');
            //            kedui = kedui.Trim(',');
            //            result = result.Trim(',');
            //            peilv = peilv.Trim(',');
            //            rangqiu = rangqiu.Trim(',');
            //            riqi = riqi.Trim(',');
            //            TouzhuSpf touspf = new TouzhuSpf();
            //            touspf.Beishu = Convert.ToInt32(txtBeishu.Text);
            //            touspf.Lucky = -1; //-1表示未验证是否中奖
            //            touspf.Operator = "吴林";
            //            touspf.OperateTime = DateTime.Now;
            //            touspf.Jiangjin = "0";
            //            touspf.Zhudui = zhudui;
            //            touspf.Kedui = kedui;
            //            touspf.Result = result;
            //            touspf.Peilv = peilv;
            //            touspf.Rangqiu = rangqiu;
            //            touspf.Riqi = riqi;
            //            new TouzhuSpfDAL().InsertTouzhuSpf(touspf);
            //        }
            //    }
            //}


            //int[] peilvCount = new int[8];
            //peilvCount[0] = 1;
            //peilvCount[1] = 1;
            //peilvCount[2] = 1;
            //peilvCount[3] = 1;
            //peilvCount[4] = 1;
            //peilvCount[5] = 1;
            //peilvCount[6] = 2;
            //peilvCount[7] = 1; ;
            //int peilvLenth = peilvCount.Length;
            //double maxNumber = Math.Pow(10, peilvLenth);
            //int chuan = 2;
            //for (int i = 0; i < maxNumber; i++)
            //{
            //    if (GetNoZeroCount(i) == chuan)
            //    {
            //        //所在位置不大于对应数组的值
            //        if (NotLargeThanArray(i, peilvCount))
            //        {
            //            Console.WriteLine(i);
            //        }
                    
            //    }
            //}
            #endregion


        }
        public bool  NotLargeThanArray(int num,int[] count)
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
            for(int i = 0 ; i< result.Length; i++){
                if (result[i] > count[i])
                {
                    return false;
                }
            }
            return true;
        }
        public int GetNoZeroCount(int num,ref int[] digits)
        {
            int result = 0;
            int index = 0;
            while (num != 0)
            {
                if (num % 10 != 0)
                {
                    result++;
                    digits[digits.Length - 1-index] = num % 10;
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
        private void dgPeilv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //clbChuanJiang.Items.Add("2串1",false);
            RefreshSelectedMatch();
            RefreshChuanJiang();
            //Console.WriteLine("clicked!");
        }

        private void RefreshChuanJiang()
        {
            clbChuanJiang.Items.Clear();
            HashSet<string> hs = new HashSet<string>();
            foreach (DataGridViewRow row in dgPeilv.Rows)
            {
                if (row.Cells[5].Selected || row.Cells[6].Selected || row.Cells[7].Selected)
                {
                    hs.Add(row.Cells[2].Value.ToString());
                }
            }
            if (hs.Count >= 2 && hs.Count<=8)
            {
                for (int i = 2; i <= hs.Count; i++)
                {

                    clbChuanJiang.Items.Add(i +"串1");
                }
            }


        }

        private void RefreshSelectedMatch()
        {
            lbSelectedMatch.Items.Clear();
            foreach (DataGridViewRow row in dgPeilv.Rows)
            {
               if(row.Cells[5].Selected || row.Cells[6].Selected || row.Cells[7].Selected)
               {
                   StringBuilder sb = new StringBuilder();
                   sb.Append(row.Cells[0].Value).Append(" ").Append(row.Cells[2].Value).Append(" ");
                   if (row.Cells[5].Selected)
                   {
                       sb.Append(row.Cells[4].Value).Append("胜 ");
                   }
                   if (row.Cells[6].Selected)
                   {
                       sb.Append(row.Cells[4].Value).Append("平 ");
                   }
                   if (row.Cells[7].Selected)
                   {
                       sb.Append(row.Cells[4].Value).Append("负 ");
                   }
                   lbSelectedMatch.Items.Add(sb.ToString());
               }
            }
        }

      

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PeilvDAL dal = new PeilvDAL();
            string riqi = dtpRiqi.Value.Date.ToString("yyyy-MM-dd");
            string liansai = cbLiansai.Text;            
            DataSet ds = dal.SearchPeilvFromKaijiang(riqi,liansai);
            dgPeilv.DataSource = ds.Tables[0];

            //更新联赛集合
            cbLiansai.Items.Clear();
            cbLiansai.Items.Add("全部");
            DataSet dsLiansai = new PeilvDAL().GetPeilvLiansaiFromKaijiang(riqi);
            foreach (DataRow row in dsLiansai.Tables[0].Rows)
            {
                cbLiansai.Items.Add(row["liansai"].ToString());

            }            
            cbLiansai.SelectedIndex = 0;
        }

        private void clbChuanJiang_ItemCheck(object sender, ItemCheckEventArgs e)
        {
          
            
            //获取选中的比赛
            Dictionary<String, List<SelectedTouzhu>> dic = new Dictionary<string, List<SelectedTouzhu>>();
            SelectedTouzhu touzhu = null;
            foreach (DataGridViewRow row in dgPeilv.Rows)
            {
                bool flag = false;
                if (row.Cells[5].Selected == true)
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
                if (row.Cells[6].Selected == true)
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
                if (row.Cells[7].Selected == true)
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
            //计算总数和奖金界限
            #region 只计算总数
            int total = 0;
            double maxJiangjin = double.MinValue;
            double minJiangjin = double.MaxValue;

            for (int i = 0; i < clbChuanJiang.Items.Count; i++)
            {
                if (i != clbChuanJiang.SelectedIndex)
                {
                    if (clbChuanJiang.GetItemChecked(i))
                    {
                        double max = 0;
                        double min = 0;
                        int zhushu = 0;
                        string chuanjiang = clbChuanJiang.GetItemText(clbChuanJiang.Items[i]);
                        int chuan = Convert.ToInt32(chuanjiang.Substring(0, 1));
                        //total += GetZhushu(dic, chuan);

                        SaveTouzhu(dic, chuan, out min, out max, out zhushu);
                        total += zhushu;
                        if (min < minJiangjin)
                        {
                            minJiangjin = min;
                        }
                        if (max > maxJiangjin)
                        {
                            maxJiangjin = max;
                        }
                    }
                }
                else
                {
                    if (!clbChuanJiang.GetItemChecked(i))
                    {
                        double max = 0;
                        double min = 0;
                        int zhushu = 0;
                      
                        string chuanjiang = clbChuanJiang.GetItemText(clbChuanJiang.Items[i]);
                        int chuan = Convert.ToInt32(chuanjiang.Substring(0, 1));
                        //total += GetZhushu(dic, chuan);

                        SaveTouzhu(dic, chuan, out min, out max, out zhushu);
                        total += zhushu;
                        if (min < minJiangjin)
                        {
                            minJiangjin = min;
                        }
                        if (max > maxJiangjin)
                        {
                            maxJiangjin = max;
                        }
                    }
                }
            }

            lblMinJiangjin.Text = minJiangjin.ToString();
            lblMaxJiangjin.Text = maxJiangjin.ToString();
            txtTouru.Text = (total * Convert.ToDouble(txtBeishu.Text) * 2).ToString();

            //int total = 0;
            ////计算所有的可能
            //for (int i = 0; i < clbChuanJiang.Items.Count; i++)
            //{
            //    if (i != clbChuanJiang.SelectedIndex)
            //    {
            //        if (clbChuanJiang.GetItemChecked(i))
            //        {
            //            string chuanjiang = clbChuanJiang.GetItemText(clbChuanJiang.Items[i]);
            //            int chuan = Convert.ToInt32(chuanjiang.Substring(0, 1));
            //            total += GetZhushu(dic, chuan);
            //        }
            //    }
            //    else
            //    {
            //        if (!clbChuanJiang.GetItemChecked(i))
            //        {
            //            string chuanjiang = clbChuanJiang.GetItemText(clbChuanJiang.Items[i]);
            //            int chuan = Convert.ToInt32(chuanjiang.Substring(0, 1));
            //            total += GetZhushu(dic, chuan);
            //        }
            //    }
            //}
            
            ////更新投入的金额
            //txtTouru.Text = (total * Convert.ToInt32(txtBeishu.Text) * 2).ToString();
            #endregion
        }

        private void TouzhuForm_Load(object sender, EventArgs e)
        {

        }

        private void txtBeishu_TextChanged(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(txtBeishu.Text)){
                    int beishu = Convert.ToInt32(txtBeishu.Text);
            int touru =  Convert.ToInt32(txtTouru.Text);
            double minjiangjin = Convert.ToDouble(lblMinJiangjin.Text);
            double maxjiangjin = Convert.ToDouble(lblMaxJiangjin.Text);

            txtTouru.Text = (touru / beishuOrigin * beishu).ToString();
            lblMinJiangjin.Text = (minjiangjin / beishuOrigin * beishu).ToString();
            lblMaxJiangjin.Text = (maxjiangjin / beishuOrigin * beishu).ToString();
            beishuOrigin = Convert.ToInt32(txtBeishu.Text);
            }
        
        }

        private int beishuOrigin = 0;
        private void txtBeishu_Enter(object sender, EventArgs e)
        {
            beishuOrigin = Convert.ToInt32(txtBeishu.Text);

        }

        private void txtBeishu_KeyPress(object sender, KeyPressEventArgs e)
        {
          
        }

        private void btnOptimize_Click(object sender, EventArgs e)
        {

        }
    }
}
