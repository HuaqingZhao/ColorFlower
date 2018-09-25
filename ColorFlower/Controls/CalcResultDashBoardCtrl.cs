using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using ColorFlower.Common;

namespace ColorFlower
{
    public partial class CalcResultDashBoardCtrl : UserControl
    {
        public IList<CalcResult> CalcResults { get; set; }

        public CalcResultDashBoardCtrl()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            EventsContainer.PatternSelectionChanged += OnPatternSelectionChanged;
            EventsContainer.IssueResultChanged += OnIssueResultChanged; 
        }

        private void SetDataSource()
        {
            dataGridView1.DataSource = CalcResults;
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            if (this.IsDesignMode()) return;

            base.OnLayout(e);

            UtilityHelper.Load();

            Initialize();

            ResizeColumnWidth();

            RenderCells();
        }

        private void RenderCells()
        {
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                var displayNumber = item.Cells[0].Value.ToString();

                var calRes = new CalcResult();
                foreach (var calcResults in CalcResults)
                {
                    if (calcResults.DisplayNumber.Equals(displayNumber))
                    {
                        calRes = calcResults;
                        break;
                    }
                }

                for (int i = 1; i < 8; i++)
                {
                    var cell = item.Cells[i];

                    if (calRes != null)
                    {
                        if (calRes.ResultMatches[i])
                            item.Cells[i].Style.BackColor = Color.Green;
                    }
                }

                RenderRange(item, calRes);
            }
        }

        private static void RenderRange(DataGridViewRow item, CalcResult calRes)
        {
            if (calRes.ResultMatches[1] && calRes.ResultMatches[4])
                item.Cells[0].Style.BackColor = Color.Orange;

            if (calRes.ResultMatches[1] && calRes.ResultMatches[2] && calRes.ResultMatches[3] && calRes.ResultMatches[4])
                item.Cells[0].Style.BackColor = Color.Red;

            if (calRes.ResultMatches[1] && calRes.ResultMatches[2] && calRes.ResultMatches[3] && calRes.ResultMatches[4]
                && calRes.ResultMatches[5] && calRes.ResultMatches[6] && calRes.ResultMatches[7])
                item.Cells[0].Style.BackColor = Color.Green;
        }

        private void ResizeColumnWidth()
        {
            var percw = (int)((this.Width - 80) / 8d);

            for (int i = 1; i < 9; i++)
            {
                dataGridView1.Columns[i].Width = percw;
            }

            var width = this.Width - percw * 8 - 27;
            if (width < 0)
                width = 5;
            dataGridView1.Columns[0].Width = width;

            if (percw < 0)
                percw = 5;

            this.tableLayoutPanel2.ColumnStyles.Clear();
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, width));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, percw));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, percw));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, percw));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, percw));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, percw));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, percw));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, percw));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, percw));
        }       

        private void OnPatternSelectionChanged()
        {
            ReLoad();
        }

        private void ReLoad()
        {
            IDictionary<int, string> selectedPatternNames = new Dictionary<int, string>();

            selectedPatternNames.Add(1, patternSelectionCtrl1.SelectedPatternName);
            selectedPatternNames.Add(2, patternSelectionCtrl2.SelectedPatternName);
            selectedPatternNames.Add(3, patternSelectionCtrl3.SelectedPatternName);
            selectedPatternNames.Add(4, patternSelectionCtrl4.SelectedPatternName);
            selectedPatternNames.Add(5, patternSelectionCtrl5.SelectedPatternName);
            selectedPatternNames.Add(6, patternSelectionCtrl6.SelectedPatternName);
            selectedPatternNames.Add(7, patternSelectionCtrl7.SelectedPatternName);

            CalcResults = UtilityHelper.GeneratorCalcResult(selectedPatternNames);

            SetDataSource();

            RenderCells();
        }

        private void OnIssueResultChanged()
        {
            ReLoad();
        }
    }
}
