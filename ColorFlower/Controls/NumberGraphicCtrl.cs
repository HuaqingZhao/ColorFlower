using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ColorFlower
{
    public partial class NumberGraphicCtrl : UserControl
    {
        private int wu;
        private int hu;
        private IDictionary<int, int> Dic;

        public NumberGraphicCtrl()
        {
            InitializeComponent();

            this.BackColor = Color.Gray;

            this.SizeChanged += NumberGraphicCtrl_SizeChanged;
        }

        void NumberGraphicCtrl_SizeChanged(object sender, EventArgs e)
        {
            if (Dic != null)
                Initialize();
        }

        public void GenerateNumberGraphic(IDictionary<int, int> dic)
        {
            Dic = dic;

            Initialize();
        }

        private void Initialize()
        {
            this.Controls.Clear();

            var maxCount = 0;
            foreach (KeyValuePair<int, int> item in Dic)
            {
                if (item.Value > maxCount)
                    maxCount = item.Value;
            }

            foreach (KeyValuePair<int, int> item in Dic)
            {
                this.Controls.Add(GenerateNumber(item.Key, item.Value, maxCount));
            }

            GenerateNumber();
        }

        private Label GenerateNumber(int number, int count, int maxCount)
        {
            var res = new Label();

            if (maxCount == 0) return res;

            res.Text = count.ToString();

            res.BackColor = Color.Blue;

            res.ForeColor = Color.Red;

            var widthUnit = (int)(this.Width / 20.0d);

            res.Font = new System.Drawing.Font("Aril", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            res.TextAlign = ContentAlignment.TopCenter;

            var width = widthUnit;

            wu = widthUnit;

            var hightUnit = (int)((this.Height - 20) / (decimal)maxCount);

            hu = hightUnit;

            var hight = (int)(hightUnit * count);

            res.Size = new System.Drawing.Size(width, hight);

            res.Location = new Point((number) * (widthUnit * 2) + 13, this.Height - res.Height - 15);//this.Height - 5);

            return res;
        }

        private void GenerateNumber()
        {
            for (int i = 0; i < 10; i++)
            {
                var res = new Label();

                res.Text = i.ToString();

                res.BackColor = Color.Blue;

                res.ForeColor = Color.Red;

                res.Font = new System.Drawing.Font("Aril", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                res.TextAlign = ContentAlignment.TopCenter;

                res.Size = new System.Drawing.Size(wu, 15);

                res.Location = new Point((i) * (wu * 2) + 13, this.Height - res.Height - 2);

                this.Controls.Add(res);

            }
        }
    }
}
