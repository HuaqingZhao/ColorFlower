using ColorFlower;
using ColorFlower.Common;
using ColorFlower.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace AcceptEmail
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.Hide();

            UtilityHelper.Load();

            Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        try
                        {
                            SyncResultUtility.Sync();
                        }
                        catch (Exception ex)
                        {
                            Trace.WriteLine(ex.Message);
                            MessageBox.Show(ex.Message);
                        }

                        if (DateTime.Now.Hour >= 20 && DateTime.Now.Hour < 21 && DateTime.Now.Minute > 30)
                        {
                            Thread.Sleep(10000);
                        }
                        else
                            Thread.Sleep(1000 * 60 * 30);
                    }
                });
        }
    }
}
