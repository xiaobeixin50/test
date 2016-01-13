using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPigs.Entity
{
    public class MatchShouyi
    {
        public string riqi { get; set; }

        public string bianhao { get; set; }

        public double totaltouru { get; set; }

        public double totalprize { get; set; }
        public double shouyi { get; set; }

        public string lucky { get; set; }
        public double huiche { get; set; }

        public int IsSkip { get; set; }
    }

}
