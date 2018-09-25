using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ColorFlower
{
    public partial class ResultDetailsCtrl : UserControl
    {
        public ResultDetailsCtrl()
        {
            InitializeComponent();
        }

        public void SetResultDetails(IList<ResultDetails> resultDetails)
        {
            dgvResultDetails.DataSource = resultDetails;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ResizeColumnWidth();
        }

        private void dgvResultDetails_SizeChanged(object sender, EventArgs e)
        {
            ResizeColumnWidth();
        }

        private void ResizeColumnWidth()
        {
            dgvResultDetails.Columns[0].Width = 60;
            dgvResultDetails.Columns[1].Width = 40;
            dgvResultDetails.Columns[4].Width = 40;
            dgvResultDetails.Columns[5].Width = 40;
            dgvResultDetails.Columns[2].MinimumWidth = 60;

            var resWidth = dgvResultDetails.Columns[0].Width + dgvResultDetails.Columns[1].Width
                + dgvResultDetails.Columns[4].Width + dgvResultDetails.Columns[5].Width;

            var width = (int)((this.Width - resWidth - 10) / 2);
            dgvResultDetails.Columns[2].Width = width;
            dgvResultDetails.Columns[3].Width = this.Width - width - resWidth - 22;
        }
    }
}
