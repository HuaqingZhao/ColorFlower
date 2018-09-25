using ColorFlower;
using ColorFlower.Common;
using ColorFlower.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Windows.Forms;
using System.Linq;
using System.Text;

namespace EMailViewer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadMail();
        }

        private void LoadMail()
        {
            UtilityHelper.Load();

            try
            {
                POP3 pope = new POP3("pop.163.com", "m18721134152@163.com", "tony200692");
                IList<MailMessage> listMailMessage = pope.GetNewMessages();

                var lastIssueDisplayNumber = UtilityHelper.GetIssueResultDisplay(IssueEntity.IssueResults.Last().IssueID + 1);

                lastIssueDisplayNumber = "69";
                foreach (var item in listMailMessage)
                {
                    if (!item.From.Equals("m18721134152@163.com")) continue;

                    if (!item.Subject.Equals(string.Format("第{0}期", lastIssueDisplayNumber))) continue;

                    var body = DecodeBase64(item.Body, Encoding.UTF8);

                    //textBox1.Text = body;
                    // got mail, exist it
                    break;
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>  
        /// 解码BASE64  
        /// </summary>  
        /// <param name="p_Text"></param>  
        /// <param name="p_Encoding"></param>  
        /// <returns></returns>  
        public string DecodeBase64(string p_Text, System.Text.Encoding p_Encoding)
        {
            if (p_Text.Trim().Length == 0) return "";
            byte[] _ValueBytes = Convert.FromBase64String(p_Text);
            var res = p_Encoding.GetString(_ValueBytes); ;
            return res;
        }  
    }
}
