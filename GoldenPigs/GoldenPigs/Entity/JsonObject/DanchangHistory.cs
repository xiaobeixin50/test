using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPigs.Entity.JsonObject
{
    public class DanchangHistory
    {
        public string term { get; set; }
        public List<string> historyTerms { get; set; }
        public List<string> canBuyMatchDate { get; set; }
        public List<String> allMatchList { get; set; }
        public List<String> allCanBuyMatchList { get; set; }
        public Dictionary<String,List<String>> canBuyArrangeInDate { get; set; }
        public Dictionary<String,String> canBuyMatchDateName { get; set; }
        public List<String> allMatchNameList { get; set; }
        public Dictionary<String, List<String>> allMatchNameValue { get; set; }
        public Dictionary<String, List<String>> allStopMatchNameValue { get; set; }
        public List<String> gudingspfList { get; set; }
        public List<String> gudingspfNoList { get; set; }
        public List<String> fudongspfList { get; set; }
        public List<String> fudongspfNoList { get; set; } 
    }
}
