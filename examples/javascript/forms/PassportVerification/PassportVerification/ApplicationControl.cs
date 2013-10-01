using PassportVerification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace PassportVerification
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private async void button1_Click(object sender, System.EventArgs e)
        {
            // Warning	4	This async method lacks 'await' operators and will run 
            // synchronously. Consider using the 'await' operator to 
            // await non-blocking API calls, or 'await Task.Run(...)' to 
            // do CPU-bound work on a background thread.	


            button1.Enabled = false;

            panel1.BackColor = Color.FromArgb(0, 0, 0);
            label1.ForeColor = Color.FromArgb(0xff, 0xff, 0);
            label1.Text = "?";

            await Task.Delay(11);

            var x = await this.applicationWebService1.dokumendi_kehtivuse_kontroll(this.textBox1.Text);

            //            ---------------------------

            //---------------------------
            //Dokumenti X ei ole v&auml;lja antud.
            //---------------------------
            //OK   
            //---------------------------

            //---------------------------
            //Dokument AA0000075 on kehtiv.
            //---------------------------
            //OK   
            //---------------------------


            var xx = x.Replace("&auml;", "ä");

            //MessageBox.Show(xx);

            var valid = x.Contains("on kehtiv");

            if (valid)
            {
                panel1.BackColor = Color.FromArgb(0, 0x7f, 0);
                label1.ForeColor = Color.FromArgb(0xff, 0xff, 0);
            }
            else
            {
                panel1.BackColor = Color.FromArgb(0x7f, 0, 0);
                label1.ForeColor = Color.FromArgb(0xff, 0xff, 0);
            }
            label1.Text = xx;

            //button1.Text = "Verify";

            button1.Enabled = true;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(0, 0, 0);
            label1.ForeColor = Color.FromArgb(0xff, 0xff, 0xff);
        }


    }


}
