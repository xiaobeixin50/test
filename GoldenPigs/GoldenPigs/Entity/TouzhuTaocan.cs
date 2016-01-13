using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPigs.Entity
{
    public class TouzhuTaocan
    {
        public int ID { get; set; }

        public int Touzhuleixing { get; set; }

        public string Touzhumingcheng { get; set; }

        public int Touzhuid { get; set; }
        public int Touzhubeishu { get; set; }

        public double Touzhujin { get; set; }
        public double Jiangjin { get; set; }
        public int Lucky { get; set; }

        public int Touzhuqishu { get; set; }

        public DateTime Touzhushijian { get; set; }

        public DateTime OperateTime { get; set; }

        public string Operator { get; set; }
    }
}
