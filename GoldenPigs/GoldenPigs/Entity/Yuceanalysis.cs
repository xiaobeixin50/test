using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPigs.Entity
{
    public class Yuceanalysis
    {
        //zhudui,kedui,yucetype,yuceren,yucespf,hasrangqiu,rangqiushu,bisaishijian,bianhao,operator,operatetime
        public string Url { get; set; }
        public string Zhudui { get; set; }

        public string Kedui { get; set; }

        public string Yucetype { get; set; }

        public string Yuceren { get; set; }

        public string Yucespf { get; set; }

        public int Hasrangqiu { get; set; }

        public string Rangqiushu { get; set; }

        public DateTime Bisaishijian { get; set; }
        public DateTime Touzhushijian { get; set; }

        public string Bianhao { get; set; }

        public string Operator { get; set; }

        public DateTime OperateTime { get; set; }
    }
}
