using ColorFlower.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ColorFlower
{
    public partial class AnalysisDetailsCtrl : UserControl
    {
        private int index;

        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
                groupBox1.Text = value.ToString();
            }
        }

        public AnalysisDetailsCtrl()
        {
            InitializeComponent();

            Dock = DockStyle.Fill;

            EventsContainer.IssueChanged += OnIssueChanged;
        }

        private void ResizeWidth()
        {
            var perw = (int)dataGridView1.Width / 10;

            for (int i = 0; i < 9; i++)
            {
                dataGridView1.Columns[i].Width = perw;
            }

            dataGridView1.Columns[9].Width = Width - perw * 9 - 10;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (row.Cells[i].Value == null) break;

                    if (row.Cells[i].Value.ToString().Equals("T"))
                    {
                        row.Cells[i].Style.BackColor = Color.Green;
                    }

                    row.Cells[i].Value = string.Empty;
                }
            }
        }

        private void OnIssueChanged(int issueNumber)
        {
            dataGridView1.DataSource = UtilityHelper.GenerateAnalysisItems(issueNumber, Index);

            var result = UtilityHelper.GetResult(issueNumber, index).ToString();

            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                item.HeaderCell.Style.BackColor = item.HeaderText.Equals(result) ? Color.Red : System.Drawing.SystemColors.Control;
            }

            ResizeWidth();
        }

        private void dataGridView1_SizeChanged(object sender, EventArgs e)
        {
            ResizeWidth();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}
