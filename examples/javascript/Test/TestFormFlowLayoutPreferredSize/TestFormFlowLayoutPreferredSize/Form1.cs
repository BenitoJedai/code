using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestFormFlowLayoutPreferredSize
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // { PreferredSize = {Width=16, Height=38} }

            //var bottom = this.flowLayoutPanel1.Controls.AsEnumerable().Max(x => x.Bottom);
            var bottom = this.flowLayoutPanel1.Controls.AsEnumerable().Sum(x => x.Height);

            Console.WriteLine(

                //new { this.PreferredSize }
                //new { this.flowLayoutPanel1.PreferredSize },
                new { bottom }

                );

            var c = SizeFromClientSize(new Size(0, bottom));

            this.Height = c.Height;
        }
    }
}
