using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPigs.Entity
{
    public class FootMatch
    {
          //bestPeilv = shengsp;
          //    bestLucky = lucky;
          //    bestriqi = riqi;
          //    bestbianhao = bianhao;

       
        public string Riqi { get; set;}
        public String Bianhao { get; set; }
        public double RealPeilv { get; set; }
        public string Lucky { get; set; }

        public int IsSkip { get; set; }

        public double PingPeilv { get; set; }

        public double ShengPeilv { get; set; }

        public double FuPeilv { get; set; }
    }
}
