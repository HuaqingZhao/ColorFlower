using ColorFlower.Common;
using ColorFlower.Utilities;
using System;
using System.Windows.Forms;
using System.Linq;

namespace ColorFlower.Controls
{
    public partial class PatternManageCtrl : UserControl
    {
        public int IssueIndex
        {
            get
            {
                return cmbIssueIndex.ToInt32();
            }
        }

        public PatternManageCtrl()
        {
            InitializeComponent();

            Dock = DockStyle.Fill;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (this.IsDesignMode()) return;

            Initialize();
        }

        private void Initialize()
        {
            if (this.IsDesignMode()) return;

            cmbIssueNumber.Items.Clear();
            cmbCharLength.Items.Clear();
            cmbSelectionLength.Items.Clear();
            cmbThreshold.Items.Clear();
            cmbStart.Items.Clear();

            var lastestIssueNumber = IssueEntity.IssueResults.Last().IssueID;

            var lastestIssueIndex = IssueEntity.IssueResults.Last().IssueIndex;

            for (int i = lastestIssueIndex; i > lastestIssueIndex - 15; i--)
            {
                cmbIssueNumber.Items.Add(UtilityHelper.GetIssueID(i));
            }

            cmbIssueNumber.SelectedIndex = 4;

            for (int i = 5; i < 20; i++)
            {
                cmbCharLength.Items.Add(i.ToString());
            }

            cmbCharLength.SelectedIndex = 10;

            for (int i = 0; i < 20; i++)
            {
                cmbSelectionLength.Items.Add(i.ToString());
            }

            cmbSelectionLength.SelectedIndex = 1;

            for (int i = 40; i < 101; i++)
            {
                cmbThreshold.Items.Add(i.ToString());
            }

            cmbThreshold.SelectedIndex = 20;

            cmbIssueIndex.SelectedIndex = 0;

            for (int i = 0; i < 20; i++)
            {
                cmbStart.Items.Add(i.ToString());
            }

            cmbStart.SelectedIndex = 0;
            LoadPatterns();
        }

        public void LoadPatterns()
        {
            clbMain.Items.Clear();

            var patterns = UtilityHelper.GetPattern(IssueIndex);

            foreach (var item in patterns)
            {
                clbMain.Items.Add(string.Format("{0} - {1}", item.Key, UtilityHelper.Calc(IssueIndex, item.Value)));
            }

            lblCount.Text = patterns.Count.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除？", "删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    for (int i = 0; i < clbMain.CheckedItems.Count; i++)
                    {
                        var txt = clbMain.CheckedItems[i].ToString();
                        var patternName = txt.SplitTo("-")[0].Trim();

                        var xPath = string.Format("root/Patterns/Item{0}/Item[@name='{1}']", IssueIndex, patternName);

                        ColorFlowerXmlUtility.DeletePatternNode(xPath);
                    }

                    UtilityHelper.Initialize();
                    EventsContainer.PatternsChanged.FireEvent();
                    LoadPatterns();

                    MessageBox.Show("删除成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除失败！");
                }
            }
        }

        private void btnAnlysis_Click(object sender, EventArgs e)
        {
            try
            {
                var c = 7;
                UtilityHelper.Analysis(cmbIssueNumber.ToInt32(), cmbIssueIndex.ToInt32(), cmbThreshold.ToInt32(), cmbStart.ToInt32(), cmbCharLength.ToInt32(), cmbSelectionLength.ToInt32(), ref c);
                UtilityHelper.Initialize();
                EventsContainer.PatternsChanged.FireEvent();
                LoadPatterns();
                MessageBox.Show("分析成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("分析失败！");
            }
        }

        private void cmbIssueIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIssueIndex.SelectedIndex > -1)
            {
                LoadPatterns();
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clbMain.Items.Count; i++)
            {
                clbMain.SetItemChecked(i, true);
            }
        }

        private void btnAnalysisAll_Click(object sender, EventArgs e)
        {
            try
            {
                var c = 7;
                for (int i = 1; i < 8; i++)
                {
                    UtilityHelper.Analysis(cmbIssueNumber.ToInt32(), i, cmbThreshold.ToInt32(), cmbStart.ToInt32(), cmbCharLength.ToInt32(), cmbSelectionLength.ToInt32(), ref c);
                }
                UtilityHelper.Initialize();
                EventsContainer.PatternsChanged.FireEvent();
                LoadPatterns();
                MessageBox.Show("分析成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("分析失败！");
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除所有？", "删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    for (int k = 1; k < 8; k++)
                    {
                        var xPath = string.Format("root/Patterns/Item{0}/Item", k);

                        ColorFlowerXmlUtility.DeleteAllPatternNode(xPath);
                    }

                    UtilityHelper.Initialize();
                    EventsContainer.PatternsChanged.FireEvent();
                    LoadPatterns();

                    MessageBox.Show("删除成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除失败！");
                }
            }
        }
    }
}
