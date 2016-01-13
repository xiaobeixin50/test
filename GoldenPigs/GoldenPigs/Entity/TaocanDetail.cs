using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPigs.Entity
{
    public class TaocanDetail
    {
        public int TaocanID	{get;set;}
        public string Zhudui1{get;set;}	
        public int Zhuduilucky1{get;set;}

        public String Zhuduishengfu1 { get; set; }
        public String Zhudui2	{get;set;}
        public int Zhuduilucky2 { get; set; }
        public String Zhuduishengfu2 { get; set; }
        
        public int Beishu	{get;set;}
        public double Jiangjin	{get;set;}
        public int Lucky { get; set; }
        public String Operator { get; set; }
        public DateTime OperateTime { get; set; }

        public int TiaozhengFlag { get; set; }

    }
}
