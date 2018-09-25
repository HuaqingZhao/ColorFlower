namespace ColorFlower
{
    partial class AnalysisDetailsCtrl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.item0DataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item1DataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item2DataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item3DataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item4DataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item5DataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item6DataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item7DataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item8DataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item9DataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.analysisItemBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.analysisItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.analysisItemBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.analysisItemBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.analysisItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.analysisItemBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(806, 307);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.item0DataGridViewCheckBoxColumn,
            this.item1DataGridViewCheckBoxColumn,
            this.item2DataGridViewCheckBoxColumn,
            this.item3DataGridViewCheckBoxColumn,
            this.item4DataGridViewCheckBoxColumn,
            this.item5DataGridViewCheckBoxColumn,
            this.item6DataGridViewCheckBoxColumn,
            this.item7DataGridViewCheckBoxColumn,
            this.item8DataGridViewCheckBoxColumn,
            this.item9DataGridViewCheckBoxColumn});
            this.dataGridView1.DataSource = this.analysisItemBindingSource2;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 16);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(800, 288);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.SizeChanged += new System.EventHandler(this.dataGridView1_SizeChanged);
            // 
            // item0DataGridViewCheckBoxColumn
            // 
            this.item0DataGridViewCheckBoxColumn.DataPropertyName = "Item0";
            this.item0DataGridViewCheckBoxColumn.HeaderText = "0";
            this.item0DataGridViewCheckBoxColumn.Name = "item0DataGridViewCheckBoxColumn";
            this.item0DataGridViewCheckBoxColumn.ReadOnly = true;
            this.item0DataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.item0DataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // item1DataGridViewCheckBoxColumn
            // 
            this.item1DataGridViewCheckBoxColumn.DataPropertyName = "Item1";
            this.item1DataGridViewCheckBoxColumn.HeaderText = "1";
            this.item1DataGridViewCheckBoxColumn.Name = "item1DataGridViewCheckBoxColumn";
            this.item1DataGridViewCheckBoxColumn.ReadOnly = true;
            this.item1DataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.item1DataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // item2DataGridViewCheckBoxColumn
            // 
            this.item2DataGridViewCheckBoxColumn.DataPropertyName = "Item2";
            this.item2DataGridViewCheckBoxColumn.HeaderText = "2";
            this.item2DataGridViewCheckBoxColumn.Name = "item2DataGridViewCheckBoxColumn";
            this.item2DataGridViewCheckBoxColumn.ReadOnly = true;
            this.item2DataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.item2DataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // item3DataGridViewCheckBoxColumn
            // 
            this.item3DataGridViewCheckBoxColumn.DataPropertyName = "Item3";
            this.item3DataGridViewCheckBoxColumn.HeaderText = "3";
            this.item3DataGridViewCheckBoxColumn.Name = "item3DataGridViewCheckBoxColumn";
            this.item3DataGridViewCheckBoxColumn.ReadOnly = true;
            this.item3DataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.item3DataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // item4DataGridViewCheckBoxColumn
            // 
            this.item4DataGridViewCheckBoxColumn.DataPropertyName = "Item4";
            this.item4DataGridViewCheckBoxColumn.HeaderText = "4";
            this.item4DataGridViewCheckBoxColumn.Name = "item4DataGridViewCheckBoxColumn";
            this.item4DataGridViewCheckBoxColumn.ReadOnly = true;
            this.item4DataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.item4DataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // item5DataGridViewCheckBoxColumn
            // 
            this.item5DataGridViewCheckBoxColumn.DataPropertyName = "Item5";
            this.item5DataGridViewCheckBoxColumn.HeaderText = "5";
            this.item5DataGridViewCheckBoxColumn.Name = "item5DataGridViewCheckBoxColumn";
            this.item5DataGridViewCheckBoxColumn.ReadOnly = true;
            this.item5DataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.item5DataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // item6DataGridViewCheckBoxColumn
            // 
            this.item6DataGridViewCheckBoxColumn.DataPropertyName = "Item6";
            this.item6DataGridViewCheckBoxColumn.HeaderText = "6";
            this.item6DataGridViewCheckBoxColumn.Name = "item6DataGridViewCheckBoxColumn";
            this.item6DataGridViewCheckBoxColumn.ReadOnly = true;
            this.item6DataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.item6DataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // item7DataGridViewCheckBoxColumn
            // 
            this.item7DataGridViewCheckBoxColumn.DataPropertyName = "Item7";
            this.item7DataGridViewCheckBoxColumn.HeaderText = "7";
            this.item7DataGridViewCheckBoxColumn.Name = "item7DataGridViewCheckBoxColumn";
            this.item7DataGridViewCheckBoxColumn.ReadOnly = true;
            this.item7DataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.item7DataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // item8DataGridViewCheckBoxColumn
            // 
            this.item8DataGridViewCheckBoxColumn.DataPropertyName = "Item8";
            this.item8DataGridViewCheckBoxColumn.HeaderText = "8";
            this.item8DataGridViewCheckBoxColumn.Name = "item8DataGridViewCheckBoxColumn";
            this.item8DataGridViewCheckBoxColumn.ReadOnly = true;
            this.item8DataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.item8DataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // item9DataGridViewCheckBoxColumn
            // 
            this.item9DataGridViewCheckBoxColumn.DataPropertyName = "Item9";
            this.item9DataGridViewCheckBoxColumn.HeaderText = "9";
            this.item9DataGridViewCheckBoxColumn.Name = "item9DataGridViewCheckBoxColumn";
            this.item9DataGridViewCheckBoxColumn.ReadOnly = true;
            this.item9DataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.item9DataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // analysisItemBindingSource2
            // 
            this.analysisItemBindingSource2.DataSource = typeof(ColorFlower.AnalysisItem);
            // 
            // analysisItemBindingSource
            // 
            this.analysisItemBindingSource.DataSource = typeof(ColorFlower.AnalysisItem);
            // 
            // analysisItemBindingSource1
            // 
            this.analysisItemBindingSource1.DataSource = typeof(ColorFlower.AnalysisItem);
            // 
            // AnalysisDetailsCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(60, 0);
            this.Name = "AnalysisDetailsCtrl";
            this.Size = new System.Drawing.Size(806, 307);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.analysisItemBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.analysisItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.analysisItemBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource analysisItemBindingSource;
        private System.Windows.Forms.BindingSource analysisItemBindingSource2;
        private System.Windows.Forms.BindingSource analysisItemBindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn item0DataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn item1DataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn item2DataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn item3DataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn item4DataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn item5DataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn item6DataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn item7DataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn item8DataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn item9DataGridViewCheckBoxColumn;
    }
}
