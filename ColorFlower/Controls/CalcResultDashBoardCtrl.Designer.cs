namespace ColorFlower
{
    partial class CalcResultDashBoardCtrl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.issueNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Index1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Index2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Index3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Index4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Index5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Index6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Index7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calcResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.patternSelectionCtrl1 = new ColorFlower.Controls.PatternSelectionCtrl();
            this.patternSelectionCtrl2 = new ColorFlower.Controls.PatternSelectionCtrl();
            this.patternSelectionCtrl3 = new ColorFlower.Controls.PatternSelectionCtrl();
            this.patternSelectionCtrl4 = new ColorFlower.Controls.PatternSelectionCtrl();
            this.patternSelectionCtrl5 = new ColorFlower.Controls.PatternSelectionCtrl();
            this.patternSelectionCtrl6 = new ColorFlower.Controls.PatternSelectionCtrl();
            this.patternSelectionCtrl7 = new ColorFlower.Controls.PatternSelectionCtrl();
            this.resultDetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcResultBindingSource)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultDetailsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(849, 440);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeight = 20;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.issueNumberDataGridViewTextBoxColumn,
            this.Index1,
            this.Index2,
            this.Index3,
            this.Index4,
            this.Index5,
            this.Index6,
            this.Index7,
            this.Count});
            this.dataGridView1.DataSource = this.calcResultBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 135);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(843, 302);
            this.dataGridView1.TabIndex = 0;
            // 
            // issueNumberDataGridViewTextBoxColumn
            // 
            this.issueNumberDataGridViewTextBoxColumn.DataPropertyName = "DisplayNumber";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.issueNumberDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.issueNumberDataGridViewTextBoxColumn.HeaderText = "期数";
            this.issueNumberDataGridViewTextBoxColumn.Name = "issueNumberDataGridViewTextBoxColumn";
            this.issueNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Index1
            // 
            this.Index1.DataPropertyName = "Index1";
            this.Index1.HeaderText = "1";
            this.Index1.Name = "Index1";
            this.Index1.ReadOnly = true;
            // 
            // Index2
            // 
            this.Index2.DataPropertyName = "Index2";
            this.Index2.HeaderText = "2";
            this.Index2.Name = "Index2";
            this.Index2.ReadOnly = true;
            // 
            // Index3
            // 
            this.Index3.DataPropertyName = "Index3";
            this.Index3.HeaderText = "3";
            this.Index3.Name = "Index3";
            this.Index3.ReadOnly = true;
            // 
            // Index4
            // 
            this.Index4.DataPropertyName = "Index4";
            this.Index4.HeaderText = "4";
            this.Index4.Name = "Index4";
            this.Index4.ReadOnly = true;
            // 
            // Index5
            // 
            this.Index5.DataPropertyName = "Index5";
            this.Index5.HeaderText = "5";
            this.Index5.Name = "Index5";
            this.Index5.ReadOnly = true;
            // 
            // Index6
            // 
            this.Index6.DataPropertyName = "Index6";
            this.Index6.HeaderText = "6";
            this.Index6.Name = "Index6";
            this.Index6.ReadOnly = true;
            // 
            // Index7
            // 
            this.Index7.DataPropertyName = "Index7";
            this.Index7.HeaderText = "7";
            this.Index7.Name = "Index7";
            this.Index7.ReadOnly = true;
            // 
            // Count
            // 
            this.Count.DataPropertyName = "Count";
            this.Count.HeaderText = "";
            this.Count.Name = "Count";
            this.Count.ReadOnly = true;
            // 
            // calcResultBindingSource
            // 
            this.calcResultBindingSource.DataSource = typeof(ColorFlower.CalcResult);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 9;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanel2.Controls.Add(this.patternSelectionCtrl1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.patternSelectionCtrl2, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.patternSelectionCtrl3, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.patternSelectionCtrl4, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.patternSelectionCtrl5, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.patternSelectionCtrl6, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.patternSelectionCtrl7, 7, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(843, 126);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // patternSelectionCtrl1
            // 
            this.patternSelectionCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patternSelectionCtrl1.IssueIndex = 1;
            this.patternSelectionCtrl1.Location = new System.Drawing.Point(96, 3);
            this.patternSelectionCtrl1.Name = "patternSelectionCtrl1";
            this.patternSelectionCtrl1.Size = new System.Drawing.Size(87, 120);
            this.patternSelectionCtrl1.TabIndex = 0;
            // 
            // patternSelectionCtrl2
            // 
            this.patternSelectionCtrl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patternSelectionCtrl2.IssueIndex = 2;
            this.patternSelectionCtrl2.Location = new System.Drawing.Point(189, 3);
            this.patternSelectionCtrl2.Name = "patternSelectionCtrl2";
            this.patternSelectionCtrl2.Size = new System.Drawing.Size(87, 120);
            this.patternSelectionCtrl2.TabIndex = 1;
            // 
            // patternSelectionCtrl3
            // 
            this.patternSelectionCtrl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patternSelectionCtrl3.IssueIndex = 3;
            this.patternSelectionCtrl3.Location = new System.Drawing.Point(282, 3);
            this.patternSelectionCtrl3.Name = "patternSelectionCtrl3";
            this.patternSelectionCtrl3.Size = new System.Drawing.Size(87, 120);
            this.patternSelectionCtrl3.TabIndex = 2;
            // 
            // patternSelectionCtrl4
            // 
            this.patternSelectionCtrl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patternSelectionCtrl4.IssueIndex = 4;
            this.patternSelectionCtrl4.Location = new System.Drawing.Point(375, 3);
            this.patternSelectionCtrl4.Name = "patternSelectionCtrl4";
            this.patternSelectionCtrl4.Size = new System.Drawing.Size(87, 120);
            this.patternSelectionCtrl4.TabIndex = 3;
            // 
            // patternSelectionCtrl5
            // 
            this.patternSelectionCtrl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patternSelectionCtrl5.IssueIndex = 5;
            this.patternSelectionCtrl5.Location = new System.Drawing.Point(468, 3);
            this.patternSelectionCtrl5.Name = "patternSelectionCtrl5";
            this.patternSelectionCtrl5.Size = new System.Drawing.Size(87, 120);
            this.patternSelectionCtrl5.TabIndex = 4;
            // 
            // patternSelectionCtrl6
            // 
            this.patternSelectionCtrl6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patternSelectionCtrl6.IssueIndex = 6;
            this.patternSelectionCtrl6.Location = new System.Drawing.Point(561, 3);
            this.patternSelectionCtrl6.Name = "patternSelectionCtrl6";
            this.patternSelectionCtrl6.Size = new System.Drawing.Size(87, 120);
            this.patternSelectionCtrl6.TabIndex = 5;
            // 
            // patternSelectionCtrl7
            // 
            this.patternSelectionCtrl7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patternSelectionCtrl7.IssueIndex = 7;
            this.patternSelectionCtrl7.Location = new System.Drawing.Point(654, 3);
            this.patternSelectionCtrl7.Name = "patternSelectionCtrl7";
            this.patternSelectionCtrl7.Size = new System.Drawing.Size(87, 120);
            this.patternSelectionCtrl7.TabIndex = 6;
            // 
            // resultDetailsBindingSource
            // 
            this.resultDetailsBindingSource.DataSource = typeof(ColorFlower.ResultDetails);
            // 
            // CalcResultDashBoardCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CalcResultDashBoardCtrl";
            this.Size = new System.Drawing.Size(849, 440);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcResultBindingSource)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.resultDetailsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource calcResultBindingSource;
        private System.Windows.Forms.BindingSource resultDetailsBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn issueNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Controls.PatternSelectionCtrl patternSelectionCtrl1;
        private Controls.PatternSelectionCtrl patternSelectionCtrl2;
        private Controls.PatternSelectionCtrl patternSelectionCtrl3;
        private Controls.PatternSelectionCtrl patternSelectionCtrl4;
        private Controls.PatternSelectionCtrl patternSelectionCtrl5;
        private Controls.PatternSelectionCtrl patternSelectionCtrl6;
        private Controls.PatternSelectionCtrl patternSelectionCtrl7;
    }
}
