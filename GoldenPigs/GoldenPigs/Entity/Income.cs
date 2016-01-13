using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPigs.Entity
{
    public class Income
    {
        public long id { get; set; }
        public double Amount{get;set;}
        public string IncomeType { get; set; }
        public string Operator { get; set; }
        public DateTime OperateTime { get; set; }

        public long TouzhuID { get; set; }
        public string TouzhuType { get; set; }
        
    }
}
