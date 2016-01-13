using GoldenPigs.Entity.JsonObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenPigs.Entity
{
    public class Kaijiang
    {
        public Kaijiang()
        { }
        public Kaijiang(string bianhao,Race race)
        {
            this.Bianhao = bianhao;
            this.BqcResult = race.resultBqc;
            this.BqcSp = GetBqcSp(race.resultBqc, race.bqcSp);
            this.Kedui = race.guestTeam;
            this.Liansai = race.matchName;
            this.OperateTime = DateTime.Now;
            this.Operator = "吴林";
            //这里用wholescore更准确
            this.QcbfResult = race.wholeScore;
            this.QcbfSp = GetBfSp(race.resultBf, race.bfSp);
            this.RqShu = Convert.ToInt32(race.concede);
            this.RqspfResult = race.resultSpf;
            this.RqspfSp = GetSpfSp(race.resultSpf, race.spfSp);
            this.SpfResult = race.resultXspf;
            this.SpfSp = GetXspfSp(race.resultXspf, race.xspfSp);
            this.ZjqResult = race.resultZjq;
            this.ZjqSp = GetZjqSp(race.resultZjq, race.zjqSp);
            this.Zhudui = race.homeTeam;

            //需要增加原始的sp
            String[] spfsps = race.xspfSp.Split('-');

            this.ShengSp = Convert.ToDouble(spfsps[0]);
            this.PingSp = Convert.ToDouble(spfsps[1]);
            this.FuSp = Convert.ToDouble(spfsps[2]);

            string[] rqspfsps = race.spfSp.Split('-');
            this.RqShengSp = Convert.ToDouble(rqspfsps[0]);
            this.RqPingSp = Convert.ToDouble(rqspfsps[1]);
            this.RqFuSp = Convert.ToDouble(rqspfsps[2]);

            this.ShengSpInt = (int) this.ShengSp;
            this.PingSpInt = (int) this.PingSp;
            this.FuSpInt = (int) this.FuSp;
            this.RqShengSpInt = (int) this.RqShengSp;
            this.RqPingSpInt = (int) this.RqPingSp;
            this.RqFuSpInt = (int) this.RqFuSp;

            this.ShengSpFracInt = (int)(this.ShengSp*10);
            this.PingSpFracInt = (int)(this.PingSp * 10);
            this.FuSpFracInt = (int)(this.FuSp * 10);
            this.RqShengSpFracInt = (int)(this.RqShengSp * 10);
            this.RqPingSpFracInt = (int)(this.RqPingSp * 10);
            this.RqFuSpFracInt = (int)(this.RqFuSp * 10);
        }

        private double GetZjqSp(string result, string sp)
        {
            if (result == "")
            {
                return 0;
            }
            string[] sps = sp.Split('-');
            int zjq = Convert.ToInt32(result);
            if (zjq >= 7)
            {
                return Convert.ToDouble(sps[7]);
            }
            else
            {
                return Convert.ToDouble(sps[zjq]);
            }
        }
        private double GetSpfSp(string result, string sp)
        {
            if (result == "")
            {
                return 0;
            }
            string[] sps = sp.Split('-');
            string temp = "0";
            if (result == "3")
            {
                temp = sps[0];
            }
            else if (result == "1")
            {
                temp = sps[1];
            }
            else
            {
                temp = sps[2];
            }
            return Convert.ToDouble(temp);
        }

        private double GetXspfSp(string result, string sp)
        {
            if (result == "")
            {
                return 0;
            }
            string[] sps = sp.Split('-');
            string temp = "0";
            if (result == "3")
            {
                temp = sps[0];
            }
            else if (result == "1")
            {
                temp = sps[1];
            }
            else
            {
                temp = sps[2];
            }
            return Convert.ToDouble(temp);
        }
        private double GetBqcSp(string result, string sp)
        {
            if (result == "")
            {
                return 0;
            }
            string[] sps = sp.Split('-');
            string temp = "0";
            switch (result)
            {
                case "33": temp = sps[0]; break;
                case "31": temp = sps[1]; break;
                case "30": temp = sps[2]; break;
                case "13": temp = sps[3]; break;
                case "11": temp = sps[4]; break;
                case "10": temp = sps[5]; break;
                case "03": temp = sps[6]; break;
                case "01": temp = sps[7]; break;
                case "00": temp = sps[8]; break;
               
            }
            return Convert.ToDouble(temp);
        }

        private double GetBfSp(string result, string sp)
        {
            if (result == "")
            {
                return 0;
            }
            string[] sps = sp.Split('-');
            string temp = "0";
            switch (result)
            {
                case "10": temp = sps[1]; break;
                case "20": temp = sps[2]; break;
                case "21": temp = sps[3]; break;
                case "30": temp = sps[4]; break;
                case "31": temp = sps[5]; break;
                case "32": temp = sps[6]; break;
                case "40": temp = sps[7]; break;
                case "41": temp = sps[8]; break;
                case "42": temp = sps[9]; break;
                case "50": temp = sps[10]; break;
                case "51": temp = sps[11]; break;
                case "52": temp = sps[12]; break;
                case "00": temp = sps[14]; break;
                case "11": temp = sps[15]; break;
                case "22": temp = sps[16]; break;
                case "33": temp = sps[17]; break;
                case "01": temp = sps[19]; break;
                case "02": temp = sps[20]; break;
                case "12": temp = sps[21]; break;
                case "03": temp = sps[22]; break;
                case "13": temp = sps[23]; break;
                case "23": temp = sps[24]; break;
                case "04": temp = sps[25]; break;
                case "14": temp = sps[26]; break;
                case "24": temp = sps[27]; break;
                case "05": temp = sps[28]; break;
                case "15": temp = sps[29]; break;
                case "25": temp = sps[30]; break;
                default:
                    if (result.Length == 2)
                    {
                        int zhubifen = Convert.ToInt32(result.Substring(0, 1));
                        int kebifen = Convert.ToInt32(result.Substring(1));
                        if (zhubifen > kebifen)
                        {
                            temp = sps[0];
                        }
                        else if (zhubifen == kebifen)
                        {
                            temp = sps[13];
                        }
                        else
                        {
                            temp = sps[18];
                        }
                    }
                    break;

            }
            return Convert.ToDouble(temp);
        }

        public int ID { get; set; }

        public string Xingqi { get; set; }

        public string Riqi { get; set; }

        public string Bianhao { get; set; }
        public string Liansai { get; set; }

        public DateTime Bisaishijian { get; set; }

        public string Zhudui { get; set; }

        public string Zhuduiliansai { get; set; }

        public int Zhuduipaiming { get; set; }

        public string Kedui { get; set; }
        public string Keduiliansai{ get; set; }
        public int Keduipaiming { get; set; }
        public string SpfResult { get; set; }

        public double SpfSp { get; set; }

        public int RqShu { get; set; }
        public string RqspfResult { get; set; }

        public double RqspfSp { get; set; }
        
        public string QcbfResult { get; set; }

        public double QcbfSp { get; set; }

        public string ZjqResult { get; set; }

        public double ZjqSp { get; set; }

        public string BqcResult { get; set; }

        public double BqcSp { get; set; }

        public string Operator { get; set; }

        public DateTime OperateTime { get; set; }

        public double ShengSp { get; set; }

        public double PingSp { get; set; }

        public double FuSp { get; set; }

        public double RqShengSp { get; set; }

        public double RqPingSp { get; set; }

        public double RqFuSp { get; set; }
        public int ShengSpInt { get; set; }

        public int PingSpInt { get; set; }

        public int FuSpInt { get; set; }

        public int RqShengSpInt { get; set; }

        public int RqPingSpInt { get; set; }

        public int RqFuSpInt { get; set; }

        public int ShengSpFracInt { get; set; }

        public int PingSpFracInt { get; set; }

        public int FuSpFracInt { get; set; }

        public int RqShengSpFracInt { get; set; }

        public int RqPingSpFracInt { get; set; }

        public int RqFuSpFracInt { get; set; }

        public DateTime EndTime { get; set; }

    }
}
