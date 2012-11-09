using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MSVSFormStyle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_ClientSizeChanged(object sender, EventArgs e)
        {
            //if (this.webBrowser1.Dock == DockStyle.Fill)
            //    return;

            //var c = this.ClientSize;

            //this.webBrowser1.MoveTo(0, 0).SizeTo(c.Width, c.Height);
        }
    }
}
