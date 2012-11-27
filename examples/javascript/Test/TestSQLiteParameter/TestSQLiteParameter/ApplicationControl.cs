using TestSQLiteParameter;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;

namespace TestSQLiteParameter
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            var now = DateTime.Now;
            InitializeContent("ApplicationControl_Load: " + now);
        }

        private void InitializeContent(string text = "")
        {


            this.listBox1.Items.Clear();
            this.applicationWebService1.WebMethod2(text,
                y =>
                {
                    this.listBox1.Items.Add(y);
                }
            );
        }

        class Last
        {
            public long value;
        }
        Last last;

        private void timer1_Tick(object sender, EventArgs e)
        {
            var Last = -1L;

            this.applicationWebService1.Table1_Last(
                svalue =>
                {
                    var value = long.Parse(svalue);

                    if (this.last == null)
                    {
                        this.last = new Last { value = value };
                        return;
                    }

                    if (value == this.last.value)
                        return;


                    this.last.value = value;

                    InitializeContent();

                }
            );
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            InitializeContent("button2_Click " + now);
        }

    }
}
