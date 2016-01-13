using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPigs.Entity
{
    public class Taocan
    {
        public int ID { get; set; }

        public int Qishu { get; set; }
        public DateTime Riqi { get; set; }
        
        public string Gailv { get; set; }
        public double Huibaolv { get; set; }

        public int Touru { get; set; }
        public double Jiangjin { get; set; }
        public int Lucky { get; set; }
        //0表示专家推荐，1表示数据模型
        public String Type { get; set; }

        public string Zhudui1 { get; set; }
        public string Kedui1 { get; set; }
        public string Zhudui2 { get; set; }
        public string Kedui2 { get; set; }
        public string  Remark { get; set; }




    }
}
