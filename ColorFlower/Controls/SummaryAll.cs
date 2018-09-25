using ColorFlower.Utilities;
using System;
using System.Windows.Forms;

namespace ColorFlower.Controls
{
    public partial class SummaryAll : UserControl
    {
        public SummaryAll()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            var names = ColorFlowerXmlUtility.GetSummaryPersonNames();

            foreach (var item in names)
            {
                var sumctrl = new SummaryCtrl();
                sumctrl.PersonName = item;

                var tabPage = new TabPage();
                tabPage.Controls.Add(sumctrl);
                tabPage.Location = new System.Drawing.Point(4, 22);
                tabPage.Padding = new System.Windows.Forms.Padding(3);
                tabPage.Text = item;
                tabPage.UseVisualStyleBackColor = true;

                tabMain.Controls.Add(tabPage);
            }
        }
    }
}
