using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPigs.Entity
{
    public class Peilv
    {
        public String Riqi { get; set; }
        public String Bianhao { get; set; }
        public String Liansai { get; set; }

        public String Zhudui { get; set; }

        public string Zhuduipaiming { get; set; }
        public string Kedui { get; set; }
        public string Keduipaiming { get; set; }
        public double ShengSp { get; set; }
        public double PingSp { get; set; }
        public double FuSp { get; set; }
        public int Rangqiu { get; set; }
        //public double RqShengSp { get; set; }
        //public double RqPingSp { get; set; }
        //public double RqFuSp { get; set; }

        public string Operator { get; set; }
        public DateTime OperateTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
