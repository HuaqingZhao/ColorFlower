namespace ColorFlower
{
    partial class ResultDetailsCtrl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvResultDetails = new System.Windows.Forms.DataGridView();
            this.issueNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.issueIndexDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.excludeNumbersDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.includeNumbersDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resultNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isMatchDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resultDetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultDetailsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvResultDetails
            // 
            this.dgvResultDetails.AllowUserToAddRows = false;
            this.dgvResultDetails.AllowUserToDeleteRows = false;
            this.dgvResultDetails.AllowUserToResizeRows = false;
            this.dgvResultDetails.AutoGenerateColumns = false;
            this.dgvResultDetails.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvResultDetails.ColumnHeadersHeight = 25;
            this.dgvResultDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvResultDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.issueNumberDataGridViewTextBoxColumn,
            this.issueIndexDataGridViewTextBoxColumn,
            this.excludeNumbersDataGridViewTextBoxColumn,
            this.includeNumbersDataGridViewTextBoxColumn,
            this.resultNumberDataGridViewTextBoxColumn,
            this.isMatchDataGridViewCheckBoxColumn});
            this.dgvResultDetails.DataSource = this.resultDetailsBindingSource;
            this.dgvResultDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResultDetails.Location = new System.Drawing.Point(0, 0);
            this.dgvResultDetails.Name = "dgvResultDetails";
            this.dgvResultDetails.ReadOnly = true;
            this.dgvResultDetails.RowHeadersVisible = false;
            this.dgvResultDetails.Size = new System.Drawing.Size(418, 326);
            this.dgvResultDetails.TabIndex = 0;
            this.dgvResultDetails.SizeChanged += new System.EventHandler(this.dgvResultDetails_SizeChanged);
            // 
            // issueNumberDataGridViewTextBoxColumn
            // 
            this.issueNumberDataGridViewTextBoxColumn.DataPropertyName = "DisplayNumber";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.issueNumberDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.issueNumberDataGridViewTextBoxColumn.HeaderText = "期数";
            this.issueNumberDataGridViewTextBoxColumn.Name = "issueNumberDataGridViewTextBoxColumn";
            this.issueNumberDataGridViewTextBoxColumn.ReadOnly = true;
            this.issueNumberDataGridViewTextBoxColumn.Width = 80;
            // 
            // issueIndexDataGridViewTextBoxColumn
            // 
            this.issueIndexDataGridViewTextBoxColumn.DataPropertyName = "IssueIndex";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.issueIndexDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.issueIndexDataGridViewTextBoxColumn.HeaderText = "位数";
            this.issueIndexDataGridViewTextBoxColumn.Name = "issueIndexDataGridViewTextBoxColumn";
            this.issueIndexDataGridViewTextBoxColumn.ReadOnly = true;
            this.issueIndexDataGridViewTextBoxColumn.Width = 80;
            // 
            // excludeNumbersDataGridViewTextBoxColumn
            // 
            this.excludeNumbersDataGridViewTextBoxColumn.DataPropertyName = "ExcludeNumbers";
            this.excludeNumbersDataGridViewTextBoxColumn.HeaderText = "无数";
            this.excludeNumbersDataGridViewTextBoxColumn.Name = "excludeNumbersDataGridViewTextBoxColumn";
            this.excludeNumbersDataGridViewTextBoxColumn.ReadOnly = true;
            this.excludeNumbersDataGridViewTextBoxColumn.Width = 150;
            // 
            // includeNumbersDataGridViewTextBoxColumn
            // 
            this.includeNumbersDataGridViewTextBoxColumn.DataPropertyName = "IncludeNumbers";
            this.includeNumbersDataGridViewTextBoxColumn.HeaderText = "有数";
            this.includeNumbersDataGridViewTextBoxColumn.Name = "includeNumbersDataGridViewTextBoxColumn";
            this.includeNumbersDataGridViewTextBoxColumn.ReadOnly = true;
            this.includeNumbersDataGridViewTextBoxColumn.Width = 150;
            // 
            // resultNumberDataGridViewTextBoxColumn
            // 
            this.resultNumberDataGridViewTextBoxColumn.DataPropertyName = "ResultNumber";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.resultNumberDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.resultNumberDataGridViewTextBoxColumn.HeaderText = "开";
            this.resultNumberDataGridViewTextBoxColumn.Name = "resultNumberDataGridViewTextBoxColumn";
            this.resultNumberDataGridViewTextBoxColumn.ReadOnly = true;
            this.resultNumberDataGridViewTextBoxColumn.Width = 70;
            // 
            // isMatchDataGridViewCheckBoxColumn
            // 
            this.isMatchDataGridViewCheckBoxColumn.DataPropertyName = "IsMatch";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.isMatchDataGridViewCheckBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.isMatchDataGridViewCheckBoxColumn.HeaderText = "对错";
            this.isMatchDataGridViewCheckBoxColumn.Name = "isMatchDataGridViewCheckBoxColumn";
            this.isMatchDataGridViewCheckBoxColumn.ReadOnly = true;
            this.isMatchDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.isMatchDataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.isMatchDataGridViewCheckBoxColumn.Width = 70;
            // 
            // resultDetailsBindingSource
            // 
            this.resultDetailsBindingSource.DataSource = typeof(ColorFlower.ResultDetails);
            // 
            // ResultDetailsCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvResultDetails);
            this.Name = "ResultDetailsCtrl";
            this.Size = new System.Drawing.Size(418, 326);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultDetailsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvResultDetails;
        private System.Windows.Forms.BindingSource resultDetailsBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn issueNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn issueIndexDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn excludeNumbersDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn includeNumbersDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn resultNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn isMatchDataGridViewCheckBoxColumn;
    }
}
