using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPigs.Entity
{
    public class Xinshui
    {
        public int ID { get; set; }
        public double Touru { get; set; }
        public string Riqi { get; set; }
        public int Beishu { get; set; }
        public string Liansai1 { get; set; }
        public string Bianhao1 { get; set; }
        public string Zhudui1 { get; set; }
        public string Kedui1 { get; set; }
        public string Result1 { get; set; }
        public string Rangqiushu1 { get; set; }
        public string Liansai2 { get; set; }
        public string Bianhao2 { get; set; }
        public string Zhudui2 { get; set; }
        public string Kedui2 { get; set; }
        public string Result2 { get; set; }
        public string Rangqiushu2 { get; set; }

        public string RealResult1 { get; set; }

        public string RealResult2 { get; set; }

        public string RealResultSp1 { get; set; }
        public string RealResultSp2 { get; set; }

        public string TouzhuSp1 { get; set; }
        public string TouzhuSp2 { get; set; }
        public int Lucky { get; set; }
        public double Jiangjin { get; set; }
        public int Exclude { get; set; }
        public string Operator { get; set; }
        public DateTime OperateTime { get; set; }

    }
}
