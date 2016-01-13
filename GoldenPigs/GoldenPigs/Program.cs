using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GoldenPigs._0630;
using GoldenPigs._1211;

namespace GoldenPigs
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new TestForm()); 
            //Application.Run(new TouzhuForm());
            //Application.Run(new ImportDataFromUrl());
            //Application.Run(new ImportData());
            //Application.Run(new ZhongjiangchaxunForm());

            //Application.Run(new MainForm());
            //Application.Run(new DanchangCelveForm());

            Application.Run(new MainControlForm());
            //Application.Run(new ChatForm());

            //Application.Run(new DataStaticsForm());
            //Application.Run(new DataVisualizationForm());
            //Application.Run(new BackTestingForm());
        }
    }
}
