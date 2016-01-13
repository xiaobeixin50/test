using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPigs.Entity
{
    public class BasketResult
    {
        public int id { get; set; }
        public string saishi { get; set; }
        public string saishibianhao { get; set; }
        public string bisaishijian { get; set; }

        public string kedui { get; set; }
        public string zhudui { get; set; }
        public string zhongchangbifen { get; set; }
        public string creator { get; set; }
        public DateTime createtime { get; set; }

        public String riqi { get; set; }
        public String bianhao { get; set; }
    }
}
