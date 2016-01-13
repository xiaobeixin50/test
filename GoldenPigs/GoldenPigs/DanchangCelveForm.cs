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
    public partial class DanchangCelveForm : Form
    {
        private int controlPointX = 0;
        private int controlPointY = 0;
        private int txtControlWidth = 100;
        private int txtControlHeight = 21;
        private int controlPad = 50;

        private int tabIndex = 10;
        private int addedControlCount = 0;

        private List<TextBox> addedTxtList = new List<TextBox>();

        public DanchangCelveForm()
        {
            InitializeComponent();
        }

        private void btnAddControls_Click(object sender, EventArgs e)
        {
//            System.Windows.Forms.TextBox txtnewResult  = new System.Windows.Forms.TextBox();;
//
//            txtnewResult.Location = new System.Drawing.Point(500, 49);
//            txtnewResult.Name = "txtResult1";
//            txtnewResult.Size = new System.Drawing.Size(100, 21);
//            txtnewResult.TabIndex = 2;
//            this.Controls.Add(txtnewResult);

            controlPointX = 0;
            if (controlPointY == 0)
            {
                controlPointY = 100;
            }

            for (int i = 0; i < 4; i++)
            {
                System.Windows.Forms.TextBox txtnewResult = new System.Windows.Forms.TextBox();
                txtnewResult.Location = new System.Drawing.Point(controlPointX + txtControlWidth + controlPad,
                    controlPointY);
                txtnewResult.Name = "txtNew" + addedControlCount++;
                txtnewResult.Size = new System.Drawing.Size(txtControlWidth, txtControlHeight);
                txtnewResult.TabIndex = tabIndex++;
                this.Controls.Add(txtnewResult);
                addedTxtList.Add(txtnewResult);
                controlPointX = controlPointX + txtControlWidth + controlPad;
            }
            controlPointY = controlPointY + controlPad + txtControlHeight;
        }

        private void DanchangCelveForm_Load(object sender, EventArgs e)
        {
        }

        private void btnCalcResult_Click(object sender, EventArgs e)
        {
            double benqian = 200;
            int celve = 1;

            benqian = Convert.ToInt32(txtBenqian.Text);
            celve = comCelve.SelectedIndex + 1;
           
            double benefit = 0;
            int counter = 0;
            double peilv1 = 0;
            double peilv2 = 0;
            double peilv3 = 0;
            int kaijiangIndex = 0;
            for (int i = 0; i < addedControlCount;)
            {
                peilv1 = Convert.ToDouble(addedTxtList[i++].Text);
                peilv2 = Convert.ToDouble(addedTxtList[i++].Text);
                peilv3 = Convert.ToDouble(addedTxtList[i++].Text);
                kaijiangIndex = Convert.ToInt32(addedTxtList[i++].Text);
                benefit += calcBenefit(benqian, peilv1, peilv2, peilv3, kaijiangIndex, celve);
            }
            this.txtResult.Text = benefit.ToString();
        }

        private double calcBenefit(double benqian, double peilv1, double peilv2, double peilv3, int kaijiangIndex,
            int celve)
        {
            //策略1标识不投首赔，策略2，表示不投中赔，策略3表示不投末陪
            double shoupei = GetShoupei(peilv1, peilv2, peilv3);
            double zhongpei = GetZhongpei(peilv1, peilv2, peilv3);
            double mopei = GetMopei(peilv1, peilv2, peilv3);

            int shoupeiIndex = GetShoupeiIndex(peilv1, peilv2, peilv3);
            int zhongpeiIndex = GetZhongpeiIndex(peilv1, peilv2, peilv3);
            int mopeiIndex = GetMopeiIndex(peilv1, peilv2, peilv3);

            double yingqianPeilv = 0;
            double baobenPeilv = 0;


            switch (celve)
            {
                case 1:
                    if (kaijiangIndex == shoupeiIndex)
                    {
                        return benqian*(-1);
                    }
                    if (kaijiangIndex == zhongpeiIndex)
                    {
                        return 0;
                    }
                    yingqianPeilv = mopei;
                    baobenPeilv = zhongpei;

                    break;
                case 2:
                    if (kaijiangIndex == zhongpeiIndex)
                    {
                        return benqian*(-1);
                    }
                    if (kaijiangIndex == shoupeiIndex)
                    {
                        return 0;
                    }
                    yingqianPeilv = mopei;
                    baobenPeilv = shoupei;
                    break;
                case 3:
                    if (kaijiangIndex == mopeiIndex)
                    {
                        return benqian*(-1);
                    }
                    if (kaijiangIndex == shoupeiIndex)
                    {
                        return 0;
                    }
                    yingqianPeilv = zhongpei;
                    baobenPeilv = shoupei;
                    break;
            }

            double benefit = (benqian - benqian/baobenPeilv)*yingqianPeilv - benqian;
            return benefit;
        }

        private int GetShoupeiIndex(double peilv1, double peilv2, double peilv3)
        {
            if (peilv1 <= peilv2)
            {
                if (peilv3 < peilv1)
                {
                    return 3;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                if (peilv3 < peilv2)
                {
                    return 3;
                }
                else
                {
                    return 2;
                }
            }
        }

        private int GetZhongpeiIndex(double peilv1, double peilv2, double peilv3)
        {
            if (peilv1 < peilv2)
            {
                if (peilv3 < peilv1)
                {
                    return 1;
                }
                else if (peilv3 >= peilv1 && peilv3 < peilv2)
                {
                    return 3;
                }
                else
                {
                    return 2;
                }
            }
            else if (peilv1 == peilv2)
            {
                if (peilv1 <= peilv3)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                if (peilv3 < peilv2)
                {
                    return 2;
                }
                else if (peilv3 >= peilv2 && peilv3 < peilv1)
                {
                    return 3;
                }
                else
                {
                    return 1;
                }
            }
        }

        private int GetMopeiIndex(double peilv1, double peilv2, double peilv3)
        {
            if (peilv1 > peilv2)
            {
                if (peilv3 > peilv1)
                {
                    return 3;
                }
                else
                {
                    return 1;
                }
            }
            else if (peilv1 == peilv2)
            {
                if (peilv3 >= peilv1)
                {
                    return 3;
                }else
                {
                    return 2;
                }
            }
            else 
            {
                if (peilv3 >= peilv2)
                {
                    return 3;
                }
                else
                {
                    return 2;
                }
            }
        }

        private double GetShoupei(double peilv1, double peilv2, double peilv3)
        {
            if (peilv1 <= peilv2)
            {
                if (peilv3 < peilv1)
                {
                    return peilv3;
                }
                else
                {
                    return peilv1;
                }
            }
            else
            {
                if (peilv3 < peilv2)
                {
                    return peilv3;
                }
                else
                {
                    return peilv2;
                }
            }
        }

        private double GetZhongpei(double peilv1, double peilv2, double peilv3)
        {
            if (peilv1 < peilv2)
            {
                if (peilv3 < peilv1)
                {
                    return peilv1;
                }
                else if (peilv3 >= peilv1 && peilv3 <= peilv2)
                {
                    return peilv3;
                }
                else
                {
                    return peilv2;
                }
            }
            else if (peilv1 == peilv2)
            {
                if (peilv1 <= peilv3)
                {
                    return peilv2;
                }
                else
                {
                    return peilv1;
                }
            }
            else
            {
                if (peilv3 < peilv2)
                {
                    return peilv2;
                }
                else if (peilv3 >= peilv2 && peilv3 < peilv1)
                {
                    return peilv3;
                }
                else
                {
                    return peilv1;
                }
            }
        }

        private double GetMopei(double peilv1, double peilv2, double peilv3)
        {
            if (peilv1 >= peilv2)
            {
                if (peilv3 > peilv1)
                {
                    return peilv3;
                }
                else
                {
                    return peilv1;
                }
            }
            else
            {
                if (peilv3 > peilv2)
                {
                    return peilv3;
                }
                else
                {
                    return peilv2;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }
    }
}