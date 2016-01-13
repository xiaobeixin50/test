using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPigs.Entity
{
    public class KaijiangStats
    {
        public int id { get; set; }
        public int totalcount { get; set; }
        public int shengspint { get; set; }
        public int pingspint { get; set; }
        public int fuspint { get; set; }

        public int shengcount { get; set; }
        public int pingcount { get; set; }
        public int fucount { get; set; }

        public double shenglv { get; set; }
        public double pinglv { get; set; }
        public double fulv { get; set; }
        public int lastspf { get; set; }
        public int last10sheng { get; set; }
        public int last10ping { get; set; }
        public int last10fu { get; set; }
        public string updater { get; set; }
        public DateTime updatetime { get; set; }
    }
}
