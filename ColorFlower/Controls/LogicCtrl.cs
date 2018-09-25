using ColorFlower.Common;
using System;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace ColorFlower
{
    public partial class LogicCtrl : UserControl
    {
        public LogicCtrl()
        {
            InitializeComponent();

            Initialize();
        }

        private void Initialize()
        {
            cmbIssue.Items.Clear();
            for (int i = 1; i < 101; i++)
            {
                cmbIssue.Items.Add(i.ToString());
            }

            cmbIndex.Items.Clear();
            for (int i = 1; i < 8; i++)
            {
                cmbIndex.Items.Add(i.ToString());
            }

            cmbPercent.Items.Clear();
            for (int i = 90; i < 101; i++)
            {
                cmbPercent.Items.Add(i.ToString());
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            cmbIssue.SelectedIndex = IssueEntity.IssueResults.Last().IssueID - 1;
            cmbIndex.SelectedIndex = 0;
            cmbPercent.SelectedIndex = cmbPercent.Items.Count - 1;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            Analysis();
        }

        private void Analysis()
        {
            if (cmbIssue.SelectedItem == null ||
                cmbIndex.SelectedItem == null ||
                cmbPercent.SelectedItem == null)
                return;

            txtOutput.Text = string.Empty;

            var first = IssueEntity.IssueResults.First().IssueID;

            var res = UtilityHelper.AnalysisAll(cmbIssue.SelectedItem.ToInt32(),
                cmbIndex.SelectedItem.ToInt32(),
                cmbPercent.SelectedItem.ToInt32(), first);

            var count = 1;
            var sb = new StringBuilder();
            var output = new StringBuilder();

            foreach (var item in res)
            {
                var temp = item;
                if (item.Length != 7)
                {
                    for (int i = 0; i < 7 - item.Length; i++)
                    {
                        temp += " ";
                    }
                }

                sb.Append(temp + Consts.OutputFormat);

                if (count == 10)
                {
                    output.AppendLine(sb.ToString());
                    output.AppendLine();
                    sb.Clear();
                    count = 0;
                }

                count++;
            }

            txtOutput.Text = output.ToString();
        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            Analysis();
        }
    }
}
