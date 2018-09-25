using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Linq;
using ColorFlower.Common;
using ColorFlower.Utilities;

namespace ColorFlower
{
    public partial class DetailCtrl : UserControl
    {
        private IDictionary<string, string> Patterns;

        public int IssueIndex { get; set; }

        private int IssueResultIndex
        {
            get
            {
                if (cmdIndex.SelectedItem == null)
                    return IssueEntity.IssueResults.Last().IssueID;
                else
                    return Convert.ToInt32(cmdIndex.SelectedItem.ToString());
            }
        }

        public string PatternsText
        {
            get
            {
                var txt = txtPattern.Text;

                if (txt.Equals(string.Empty)) return string.Empty;

                var reg = new Regex("[0-9,;]+");
                var ms = reg.Matches(txt);

                var sb = new StringBuilder();

                foreach (Match item in ms)
                {
                    sb.AppendLine(item.Value.ToString());
                }

                var s = sb.ToString();

                if (s.Length - 2 > 0)
                    s = s.Substring(0, s.Length - 2);

                return s;
            }
        }

        public DetailCtrl()
        {
            InitializeComponent();

            Dock = DockStyle.Fill;

            EventsContainer.IssueResultChanged -= Initialize;
            EventsContainer.IssueResultChanged += Initialize;
            EventsContainer.PatternsChanged += Initialize;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (this.IsDesignMode()) return;

            Initialize();
        }

        private void Initialize()
        {
            Patterns = UtilityHelper.GetPattern(IssueIndex);

            cmbPattern.Items.Clear();

            var ps = new List<string>();
            foreach (KeyValuePair<string, string> item in Patterns)
            {
                ps.Add(item.Key);
            }

            ps.Sort();
            cmbPattern.Items.AddRange(ps.ToArray());


            if (cmbPattern.Items.Count > 0)
                cmbPattern.SelectedIndex = 0;

            cmdIndex.Items.Clear();

            foreach (var item in IssueEntity.IssueResults)
            {
                cmdIndex.Items.Add(item.IssueID.ToString());
            }

            if (IssueEntity.IssueResults.Count > 0)
            {
                var last = IssueEntity.IssueResults.Last().IssueID + 1;


                cmdIndex.Items.Add((last).ToString());

                cmdIndex.SelectedItem = (IssueEntity.IssueResults.Last().IssueID + 1).ToString();

                GeneratorOutput();
            }
        }

        private void GeneratorOutput()
        {
            txtOutput.Text = ColorFlowerXmlUtility.GetOutput(string.Format("root/Outputs/Output{0}/Item[@index='{1}']", IssueResultIndex.ToString(), IssueIndex));
        }

        private void GenerateSummary(IList<ResultDetails> rds)
        {
            try
            {
                rds = rds.Take(Consts.TakeCount).ToList<ResultDetails>();

                int rightCount = (from rd in rds where rd.IsMatch.Equals("对") select rd).Count();

                var wrongCount = rds.Count - rightCount;

                var percent = Math.Round((decimal)((double)rightCount / (double)(rightCount + wrongCount)), 2) * 100;
                lblSummary.Text = string.Format("对:{0}, 错:{1}, 对的概率:{2}%", rightCount, wrongCount, percent);
            }
            catch (Exception e) { }
        }

        private void cmbPattern_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPattern.SelectedIndex > -1)
            {
                foreach (KeyValuePair<string, string> item in Patterns)
                {
                    if (cmbPattern.SelectedItem.ToString().Equals(item.Key))
                    {
                        txtPattern.Text = item.Value;
                        txtPatternName.Text = item.Key;
                    }
                }
            }
        }

        private void cmdIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmdIndex.SelectedIndex > -1)
            {
                DetailsCalc();
                GeneratorOutput();

                GenerateNumberGraphic();
            }
        }

        private void txtPattern_TextChanged(object sender, EventArgs e)
        {
            if (cmdIndex.SelectedItem != null)
                DetailsCalc();
        }

        private void DetailsCalc()
        {
            var rds = UtilityHelper.DetailsCalc(IssueResultIndex, IssueIndex, PatternsText);

            resultDetailsCtrl1.SetResultDetails(rds.Take(Consts.TakeCount).ToList());
            GenerateSummary(rds);
        }

        private void btnAnalysis_Click(object sender, EventArgs e)
        {
            try
            {
                UtilityHelper.AutoAnalysis();
                MessageBox.Show("分析成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("分析成功！");
            }
        }


        private void GenerateNumberGraphic()
        {
            numberGraphicCtrl1.GenerateNumberGraphic(UtilityHelper.GenerateNumber(IssueResultIndex, IssueIndex));
        }

        private void btnSavePatterns_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPatternName.Text))
            {
                MessageBox.Show("模型名字不能为空！");
                return;
            }

            try
            {
                var xPath = string.Format("root/Patterns/Item{0}/Item[@name='{1}']", IssueIndex, txtPatternName.Text);

                var isExists = ColorFlowerXmlUtility.IsExists(xPath);

                if (!isExists)
                {
                    xPath = string.Format("root/Patterns/Item{0}", IssueIndex);
                    ColorFlowerXmlUtility.AddPatternNode(xPath, txtPatternName.Text.Trim(), PatternsText.Trim());
                }
                else
                {
                    ColorFlowerXmlUtility.UpdatePatternNode(xPath, PatternsText.Trim());
                }

                MessageBox.Show("保存成功！");

                ReLoad();

                EventsContainer.PatternsChanged.FireEvent();
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败！");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var count = UtilityHelper.ClearPatterns(IssueIndex);
                MessageBox.Show("分析成功！ " + count);

                UtilityHelper.Reinitialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show("分析成功！");
            }
        }

        private void ReLoad()
        {
            UtilityHelper.Load();

            Initialize();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPattern.Text = string.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ColorFlowerXmlUtility.AddOutputNode("root/Outputs", IssueResultIndex, IssueIndex, txtOutput.Text))
                {
                    MessageBox.Show("增加成功！");
                }
                else
                {
                    MessageBox.Show("增加失败！名字已经存在！");
                }

                ReLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show("增加失败！");
            }
        }
    }
}
