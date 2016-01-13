using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;
using System.Collections.Generic;


namespace GoldenPigs._1211
{
    public partial class ChatForm : Form
    {

        static ApplicationClass outlookApp = new Microsoft.Office.Interop.Outlook.ApplicationClass();
        static NameSpace ns;

        string userMail = "xiaobeixin50@163.com";
        string userPassword = "*Gems22222";
        public ChatForm()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

            Console.WriteLine("start to monitor new emails");
            ns = outlookApp.GetNamespace("MAPI");
            outlookApp.NewMailEx += new ApplicationEvents_11_NewMailExEventHandler(outlookApp_NewMailEx);
            outlookApp.NewMail += new ApplicationEvents_11_NewMailEventHandler(outlookApp_NewMail);
        }

        void outlookApp_NewMail()
        {
            Console.WriteLine("a new message comes: new email");
        }

        void outlookApp_NewMailEx(string EntryIDCollection)
        {
            Console.WriteLine("a new message comes");
            AnalyzeNewItem(EntryIDCollection);
        }

        private void AnalyzeNewItem(string entry)
        {

            try
            {
                MAPIFolder outLookFolder = ns.GetDefaultFolder(OlDefaultFolders.olFolderInbox);
                string storeID = outLookFolder.StoreID;

                MailItem latestMailItem = (MailItem)ns.GetItemFromID(entry, storeID);
                Console.WriteLine(latestMailItem.Subject);
                Console.WriteLine(latestMailItem.To);
                Console.WriteLine(latestMailItem.SenderName);
                Console.WriteLine(latestMailItem.ReceivedTime);
                Console.WriteLine(latestMailItem.Body);

                String addedContent = latestMailItem.SenderName + " " + latestMailItem.ReceivedTime + Environment.NewLine + latestMailItem.Body;


                tbTalk.Text = latestMailItem.Body;

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           

            //var inbox = ns.GetDefaultFolder(OlDefaultFolders.olFolderInbox);
            //List<MailItem> allmails = new List<MailItem>();
            //foreach (var item in inbox.Items)
            //{
            //    if (item is MailItem)
            //    {
            //        var mail = item as MailItem;
            //        allmails.Add(mail);
            //    }
            //}
            //var latest = allmails.Max(s => s.ReceivedTime);
            //var latestMailItem = allmails.FirstOrDefault(s => s.ReceivedTime == latest);
            //if (latestMailItem != null)
            //{
            //    Console.WriteLine(latestMailItem.Subject);
            //    Console.WriteLine(latestMailItem.To);
            //    Console.WriteLine(latestMailItem.SenderName);
            //    Console.WriteLine(latestMailItem.ReceivedTime);
            //    Console.WriteLine(latestMailItem.Body);
            //}
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                //主要逻辑是执行下面的语句
                //blat -to meichao.yin@baichanghui.com -s "test" -body "testbody"
                string commandLine = @"C:\Windows\System32\blat.exe -to lin.wu@renren-inc.com -s ""test"" -body ""testbody""";
                string argsTemplate = @"-to xiaobeixin50@163.com -s ""{0}"" -body ""{1}"" -u {2} -pw {3}";
                Process.Start(@"D:\devtools\blat3211_32.full\blat3211\full\blat.exe", string.Format(argsTemplate,tbTitle.Text,rtbContent.Text, userMail, userPassword));
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
