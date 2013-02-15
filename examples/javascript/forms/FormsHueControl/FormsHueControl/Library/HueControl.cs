using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsHueControl.Library
{
    [DefaultEvent("AdjustHue")]
    public partial class HueControl : UserControl
    {
        public HueControl()
        {
            InitializeComponent();
        }

        int delta;
        int prev;

        public event Action<int> AdjustHue;

        private void HueControl_Scroll(object sender, ScrollEventArgs e)
        {
            var u8 = (byte)Math.Min(255,
                this.HorizontalScroll.Value * 240 / (this.panel1.Width - this.Width)
            );

            delta = u8 - prev;
            prev = u8;

            var hls = ScriptCoreLib.JavaScript.Runtime.JSColor.FromHLS(
                u8,

                120,
                240
                );

            var rgb = hls.ToRGB();

            this.panel1.BackColor = Color.FromArgb(rgb.R, rgb.G, rgb.B);

            //Console.WriteLine(
            //    new
            //    {
            //        u8
            //    }
            //);

            if (AdjustHue != null)
                AdjustHue(delta);
        }

        private void HueControl_Load(object sender, EventArgs e)
        {

        }
    }
}
