using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RayCaster1.source.java;

namespace RayCaster3
{
    public partial class RayCaster3Control : UserControl
    {
        public RayCaster3Control()
        {
            InitializeComponent();
        }

        RayCaster1Base Virtual = new RayCaster1Base();

        private void RayCaster3_Load(object sender, EventArgs e)
        {
            Virtual.showStatus = t => label1.Text = t;
            this.Refresh();
        }

        bool RayCaster3_Paint_Once;

        Bitmap b;

        private void RayCaster3_Paint(object sender, PaintEventArgs e)
        {
            //if (RayCaster3_Paint_Once)
            //{
            //    e.Graphics.DrawImage(b, Point.Empty);

            //    return;
            //}

            RayCaster3_Paint_Once = true;

            e.Graphics.DrawLine(Pens.Red, 55, 66, 324, 544);

            e.Graphics.FillRectangle(Brushes.Aqua, 4, 4, 332, 32);

            var a = new List<IDisposable>();

            b = new Bitmap(RayCaster1Base.DefaultWidth, RayCaster1Base.DefaultHeight);

            var g = Graphics.FromImage(b);

            var color = default(Color);

            Virtual.setColor = c => color = c;
            Virtual.drawLine =
                (x, y, cx, cy) =>
                {
                    var p = new Pen(Color.FromArgb(255, color.R, color.G, color.B), 1);

                    a.Add(p);

                    g.DrawLine(p, x, y, cx, cy);
                };

            Virtual.fillRect =
               (x, y, cx, cy) =>
               {
                   var p = new SolidBrush(Color.FromArgb(255, color.R, color.G, color.B));

                   a.Add(p);

                   g.FillRectangle(p, x, y, cx, cy);
               };

            Virtual.render();

            foreach (var v in a)
            {
                v.Dispose();
            }

            e.Graphics.DrawImage(b, Point.Empty);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Virtual.runonce();
            this.Refresh();

        }

        private void RayCaster3Control_KeyDown(object sender, KeyEventArgs e)
        {
            Virtual.fKeyUp.ProcessKeyDown((int)e.KeyCode);
            Virtual.fKeyDown.ProcessKeyDown((int)e.KeyCode);
            Virtual.fKeyLeft.ProcessKeyDown((int)e.KeyCode);
            Virtual.fKeyRight.ProcessKeyDown((int)e.KeyCode);

        }

        private void RayCaster3Control_KeyUp(object sender, KeyEventArgs e)
        {
            Virtual.fKeyUp.ProcessKeyUp((int)e.KeyCode);
            Virtual.fKeyDown.ProcessKeyUp((int)e.KeyCode);
            Virtual.fKeyLeft.ProcessKeyUp((int)e.KeyCode);
            Virtual.fKeyRight.ProcessKeyUp((int)e.KeyCode);
        }
    }
}
