using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ColorFlower.Utilities;

namespace ColorFlower.Controls
{
    public partial class PatternSelectionCtrl : UserControl
    {
        private IDictionary<string, string> patterns;
        private IDictionary<string, string> defaultPatterns;

        public int IssueIndex { get; set; }

        public string SelectedPatternName { get; set; }

        public PatternSelectionCtrl()
        {
            InitializeComponent();

            EventsContainer.PatternsChanged += OnPatternsChanged;
        }

        private void OnPatternsChanged()
        {
            ReLoad();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (this.IsDesignMode()) return;

            ReLoad();
        }

        private void ReLoad()
        {
            LoadConfig();

            InitializeLayout();
        }

        private void LoadConfig()
        {
            var xPath = string.Format("root/Patterns/Item{0}", IssueIndex);

            patterns = ColorFlowerXmlUtility.GetPatterns(xPath);

            defaultPatterns = ColorFlowerXmlUtility.GetDefaultPatterns();

            cmbPatternName.Items.Clear();

            var ps = new List<string>();
            foreach (KeyValuePair<string, string> item in patterns)
            {
                ps.Add(item.Key);
            }

            ps.Sort();
            cmbPatternName.Items.AddRange(ps.ToArray());

            cmbPatternName.Items.Add("Wrong");
        }

        private void InitializeLayout()
        {
            var selectedPatternName = defaultPatterns[IssueIndex.ToString()];
            btnMakeDefault.Text = selectedPatternName;

            if (cmbPatternName.Items.Contains(selectedPatternName))
                cmbPatternName.SelectedItem = selectedPatternName;
            else if (cmbPatternName.Items.Count >= 0)
                cmbPatternName.SelectedIndex = 0;
        }

        private void cmbPatternName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPatternName.SelectedIndex > -1)
            {
                SelectedPatternName = cmbPatternName.SelectedItem.ToString();
                if (!SelectedPatternName.Equals("Wrong"))
                {
                    txtPattern.Text = patterns[SelectedPatternName];
                }
                else
                {
                    var sb = new StringBuilder();
                    foreach (KeyValuePair<string, string> item in patterns)
                    {
                        if (item.Key.StartsWith("W"))
                        {
                            sb.Append(item.Value);
                        }
                    }

                    txtPattern.Text = sb.ToString();
                }

                EventsContainer.PatternSelectionChanged.FireEvent();
            }
        }

        private void btnMakeDefault_Click(object sender, EventArgs e)
        {
            if (ColorFlowerXmlUtility.SaveDefaultPattern(IssueIndex, cmbPatternName.SelectedItem.ToString()))
            {
                MessageBox.Show("保存成功！");
                btnMakeDefault.Text = cmbPatternName.SelectedItem.ToString();
            }
            else
                MessageBox.Show("保存失败!");
        }
    }
}
