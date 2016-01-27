using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPigs.Entity
{
    public class DataPeilvAnalysis
    {
        public string riqi { get; set; }
        public string bianhao { get; set; }
        public string zhudui { get; set; }
        public string kedui { get; set; }
        public int rqshu { get; set; }
        public string spfresult { get; set; }
        public string rqspfresult { get; set; }

       

        public double shenglv { get; set; }
        public double pinglv { get; set; }
        public double fulv { get; set; }

        public double rqshenglv { get; set; }
        public double rqpinglv { get; set; }
        public double rqfulv { get; set; }

        public int tuijianspf { get; set; }
        public int tuijianrqspf { get; set; }

        public int totalcount { get; set; }
        public int totalrqcount { get; set; }
        public int shengspfracint { get; set; }
        public int pingspfracint { get; set; }
        public int fuspfracint { get; set; }

        public int rqshengspfracint { get; set; }
        public int rqpingspfracint { get; set; }
        public int rqfuspfracint { get; set; }

        //胜平负结果的概率排行
        public int spfgailvrank { get; set; }
        public int rqspfgailvrank { get; set; }

        //最近一场比赛的胜平负结果概率排行
        public int lastspfgailvrank { get; set; }
        public int lastrqspfgailvrank { get; set; }

        public int lastspf { get; set; }
        public int lastrqspf { get; set; }
        
    }
}
