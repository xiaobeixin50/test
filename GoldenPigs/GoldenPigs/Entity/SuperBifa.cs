using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPigs.Entity
{
    public class SuperBifa
    {
        public long id { get; set; }
        public string riqi { get; set; }
        public string xingqi { get; set; }
        public string bianhao { get; set; }
        public string liansai { get; set; }
        public string zhudui { get; set; }
        public string kedui { get; set; }
        public string bifen { get; set; }
        public string kaisaishijian { get; set; }

        public double bifajiawei_sheng { get; set; }

        public double bifajiawei_ping { get; set; }
        public double bifajiawei_fu { get; set; }
        public double bifazhishu_sheng { get; set; }
        public double bifazhishu_ping { get; set; }
        public double bifazhishu_fu { get; set; }
        public double baijiaoupei_sheng { get; set; }
        public double baijiaoupei_ping { get; set; }
        public double baijiaoupei_fu { get; set; }
        public int chengjiaoe { get; set; }

        public double sheng { get; set; }

        public double ping{get; set; }
        public double fu { get; set; }

        public double dae_sheng { get; set; }
        public double dae_ping { get; set; }

        public double dae_fu { get; set; }


        public DateTime inserttime { get; set; }

        public int prize_rank { get; set; }

        public int dae_prize_rank { get; set; }

        public double first_sp { get; set; }

        public double second_sp { get; set; }

        public double third_sp { get; set; }




     }
}
