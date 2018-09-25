using ColorFlower.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Linq;
using ColorFlower.Utilities;

namespace ColorFlower
{
    public partial class IssueResultCtrl : UserControl
    {
        public IssueResultCtrl()
        {
            InitializeComponent();
        }

        private void Initialize()
        {
            var irs = new List<IssueResultDisplay>();

            foreach (var item in IssueEntity.IssueResults)
            {
                var ir = new IssueResultDisplay();
                ir.IssueNumber = item.IssueID.ToString();

                var sb = new StringBuilder();

                for (int i = 0; i < 7; i++)
                {
                    sb.Append(item.OpenCodeInt[i] + " ");
                }

                ir.Result = sb.ToString();

                irs.Add(ir);
            }

            dataGridView1.DataSource = irs;
        }

        private void dataGridView1_Resize(object sender, EventArgs e)
        {
            dataGridView1.Columns[0].Width = 70;
            var resWidth = dataGridView1.Columns[0].Width;

            var width = this.Width - resWidth - 27;
            dataGridView1.Columns[1].Width = width;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (this.IsDesignMode()) return;

            base.OnLoad(e);

            Load();
        }

        private void Load()
        {
            Initialize();

            if (dataGridView1.Rows.Count - 1 > 0)
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
            }
        }

        private void btnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                EmailUtility.Send();
                MessageBox.Show("发送成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("发送失败！" + Environment.NewLine + ex.Message);
            }
        }

        private void btnAddIssueResult_Click(object sender, EventArgs e)
        {
            try
            {
                UtilityHelper.NewResultAdded(new IssueResult()
                {
                    IssueID = txtIssueNumber.Text.ToInt32(),
                    OpenCode = txtIssueResult.Text,
                    OpenDate = DateTime.Now
                });

                UtilityHelper.Initialize();
                txtIssueNumber.Text = string.Empty;
                txtIssueResult.Text = string.Empty;
                Load();
                EventsContainer.IssueResultChanged.FireEvent();
                EmailUtility.Send();
                MessageBox.Show("增加成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("增加失败！" + Environment.NewLine + ex.Message);
            }
        }
    }
}
