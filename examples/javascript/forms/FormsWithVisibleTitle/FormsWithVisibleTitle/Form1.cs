using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsWithVisibleTitle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.ScrollableControl.get_VerticalScroll()]


            //this.applicationControl1.scrollto

            // need to get to top get correct PreferredSize
            this.applicationControl1.VerticalScroll.Value = 0;
            this.applicationControl1.HorizontalScroll.Value = 0;

            var p = this.applicationControl1.PreferredSize;
            this.Text = new { p }.ToString();

            this.SuspendLayout();
            this.ClientSize = p;
            this.ResumeLayout();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int c = 0;

            this.applicationControl1.Layout +=
                delegate
                {
                    c++;

                    this.SuspendLayout();

                    this.applicationControl1.AutoScroll = false;
                    //this.applicationControl1.VerticalScroll.Value = 0;
                    //this.applicationControl1.HorizontalScroll.Value = 0;

                    // in desktop mode at some point the screen becomes a limiting factor.
                    // since desktop does not auto resize we get maxed out
                    var p = this.applicationControl1.PreferredSize;
                    this.Text = "applicationControl1.Layout " + new { c, p };

                    this.ClientSize = p;
                    this.applicationControl1.AutoScroll = true;
                    this.ResumeLayout();


                };
        }

        private void applicationControl1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_BackColorChanged(object sender, EventArgs e)
        {
            this.applicationControl1.BackColor = this.BackColor;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
