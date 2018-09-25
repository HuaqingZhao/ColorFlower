using ColorFlower.Common;
using ColorFlower.Entities;
using ColorFlower.Utilities;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ColorFlower.Controls
{
    public partial class SummaryCtrl : UserControl
    {
        public string PersonName { get; set; }

        public SummaryCtrl()
        {
            InitializeComponent();
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            if (this.IsDesignMode()) return;

            base.OnLayout(e);

            Initialize();
            Dock = DockStyle.Fill;
        }

        private void Initialize()
        {
            cmbIsssueNumber.Items.Clear();

            foreach (var item in IssueEntity.IssueResults)
            {
                cmbIsssueNumber.Items.Add(item.IssueID.ToString());
            }

            cmbIsssueNumber.SelectedItem = IssueEntity.IssueResults.Last().IssueID.ToString();

            GenerateSummary();
        }

        private void GenerateSummary()
        {
            if (cmbIsssueNumber.SelectedItem == null) return;

            var summary = ColorFlowerXmlUtility.GetSummary(PersonName, Convert.ToInt32(cmbIsssueNumber.SelectedItem.ToString()));

            lblIssueResult.Text = UtilityHelper.GetIssueResult(cmbIsssueNumber.SelectedItem.ToString());
            txtPay.Text = summary.Pay.ToString();
            txtIncome.Text = summary.Income.ToString();
            txtBalance.Text = summary.Balance.ToString();
            txtCount.Text = summary.Count.ToString();
            txtIssueOutput.Text = summary.Output;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var summmary = new Summary();

                summmary.Balance = txtBalance.Text.ToInt32();
                summmary.Count = txtCount.Text.ToInt32();
                summmary.Income = txtIncome.Text.ToInt32();
                summmary.Output = txtIssueOutput.Text;
                summmary.Pay = txtPay.Text.ToInt32();
                summmary.PersonName = PersonName;
                summmary.IssueNumber = Convert.ToInt32(cmbIsssueNumber.SelectedItem.ToString());

                ColorFlowerXmlUtility.SaveSummaryNode(summmary);

                MessageBox.Show("保持成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("保持失败！" + Environment.NewLine + ex.Message);
            }
        }

        private void cmbIsssueNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIsssueNumber.SelectedIndex > -1)
            {
                GenerateSummary();
            }
        }

        private void TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtBalance.Text = (txtIncome.Text.ToInt32() - txtPay.Text.ToInt32()).ToString();
            }
            catch (Exception ex) { }
        }
    }
}
