using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPigs.Entity.JsonObject
{
    public class BifaJsonEntity
    {
        public BifaSubJsonEntity result { get; set; }
        public String  status { get; set; }

        public int code { get; set; }

        public string msg { get; set; }
    }
}
