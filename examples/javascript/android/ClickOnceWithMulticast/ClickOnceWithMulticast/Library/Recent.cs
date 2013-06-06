using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClickOnceWithMulticast.Library
{
    public partial class Recent : Form
    {
        public readonly ApplicationWebService service = new ApplicationWebService();



        public Recent()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            var a = new List<string>();


            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                a.Add,
                //value => this.listBox1.Items.Add(value)

                done: delegate
                {
                    this.listBox1.Items.Clear();
                    this.listBox1.Items.AddRange(a.ToArray());
                }
            );
        }
    }
}
