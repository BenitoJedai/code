using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace com.abstractatech.multiscreen.formsexample
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            this.InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoCountCheckAndMaybeRefresh();
        }

        private void AtRefresh()
        {
            this.listBox1.Items.Clear();

            var s = new ApplicationXWebService();
            button1.Enabled = false;
            s.EnumerateItems("",
                k =>
                {
                    button1.Enabled = true;

                    this.listBox1.Items.Add(k);
                }
            );
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AtRefresh();
        }

        int InternalCount;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
                return;

            DoCountCheckAndMaybeRefresh();
        }

        private void DoCountCheckAndMaybeRefresh()
        {
            timer1.Enabled = false;

            new ApplicationXWebService().CountItems("",
                c =>
                {
                    var i = int.Parse(c);
                    if (i != InternalCount)
                        AtRefresh();

                    InternalCount = i;
                    timer1.Enabled = true;
                }
            );
        }



    }

}
