using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsExample.js
{
    using ScriptCoreLib.JavaScript.Windows.Forms;
    using ScriptCoreLib.Shared;

    [ScriptCoreLib.Script]
    public partial class Gradient : UserControl
    {
        List<Panel> Gradients = new List<Panel>();

        private Orientation _Orientation;

        public Orientation Orientation
        {
            get { return _Orientation; }
            set { _Orientation = value; UpdateGradientLocation(); }
        }

        public Gradient()
        {
            InitializeComponent();

            
            //Gradients = new Panel[16];

            int step = 16;
            int steps = 16;

            for (int i = 0; i < steps; i++)
            {
                var p = new Panel();

                var c = Math.Floor((double)((i  * 0xff / steps)));

                p.BackColor = Color.FromArgb((int)c, 0, 0);
                p.Location = new Point(0, i * step);
                p.Size = new Size(200, step);

                this.Controls.Add(p);
                Gradients.Add(p);
            }

            UpdateGradientColors();

        }

        private void Gradient_Resize(object sender, EventArgs e)
        {
            UpdateGradientLocation();


        }

        private void UpdateGradientLocation()
        {
            int step = 0;

            if (Orientation == Orientation.Vertical)
                step = (int)Math.Floor((double)(this.Height / Gradients.Count));
            else
                step = (int)Math.Floor((double)(this.Width / Gradients.Count));

            for (int i = 0; i < Gradients.Count; i++)
            {
                var p = Gradients[i]; //.GetHTMLTarget();

                if (Orientation == Orientation.Vertical)
                {
                    p.Location = new Point(0,  i * step);
                    if (i == Gradients.Count - 1)
                        step = Height - i * step;
                    p.Size = new Size(Width, step);
                }
                else
                {
                    p.Location = new Point( i * step, 0);

                    if (i == Gradients.Count - 1)
                        step = Width - i * step;
                    p.Size = new Size(step, Height);
                }
            }
        }

        void UpdateGradientColors()
        {
            var a = this.GradientStartColor;
            var b = this.GradientEndColor;

            if (Gradients.Count > 1)
            {
                for (int i = 0; i < Gradients.Count; i++)
                {
                    var p = Gradients[i];

                    var y = ((double)i) / (Gradients.Count - 1);
                    var x = 1 - y;

                    p.BackColor = Color.FromArgb(
                            (int)Math.Floor(x * a.R + y * b.R),
                            (int)Math.Floor(x * a.G + y * b.G),
                            (int)Math.Floor(x * a.B + y * b.B)
                        );
                }
            }
        }

        public event Action GradientColorChanged;

        protected virtual void OnGradientColorChanged()
        {
            UpdateGradientColors();

            if (GradientColorChanged != null)
                GradientColorChanged();
        }

        private Color _StartColor = Color.FromArgb(0, 0xff, 00);

        public Color GradientStartColor
        {
            get { return _StartColor; }
            set { _StartColor = value; OnGradientColorChanged();  }
        }

        private Color _EndColor = Color.FromArgb(0, 0, 0xFF);

        public Color GradientEndColor
        {
            get { return _EndColor; }
            set { _EndColor = value; OnGradientColorChanged(); }
        }
    }
}
