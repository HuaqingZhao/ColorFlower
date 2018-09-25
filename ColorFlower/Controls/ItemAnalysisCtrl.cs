using ColorFlower.Common;
using System;
using System.Windows.Forms;
using System.Linq;

namespace ColorFlower
{
    public partial class ItemAnalysisCtrl : UserControl
    {
        public ItemAnalysisCtrl()
        {
            InitializeComponent();

            Dock = DockStyle.Fill;

            if (this.IsDesignMode()) return;

            GenerateIssueNumber();
        }

        private void GenerateIssueNumber()
        {
            for (int i = 1; i < 200; i++)
            {
                cmbIssueNumber.Items.Add(i.ToString());
            }
        }

        private void cmbIssueNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIssueNumber.SelectedIndex > -1)
            {
                EventsContainer.IssueChanged(cmbIssueNumber.SelectedItem.ToString().ToInt32());
            }
        }

        private void btnNow_Click(object sender, EventArgs e)
        {
            cmbIssueNumber.SelectedIndex = IssueEntity.IssueResults.Last().IssueID;
        }
    }
}
