using com.abstractatech.multiscreen.formsexample;
using com.abstractatech.multiscreen.formsexample.Library;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace com.abstractatech.multiscreen.formsexample
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            new Form2().Show();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            new Form3().Show();
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            if (!checkBox1.Checked)
                return;
            checkBox1.Enabled = false;
            timer1.Enabled = false;

            new ApplicationXWebService().CountItems("",
                c =>
                {
                    button2.Text = "Read (" + c + ")";
                    timer1.Enabled = true;
                    checkBox1.Enabled = true;
                }
            );

        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            new MazeForm().Show();
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            new MandelbrotForm().Show();
        }

        private void button6_Click(object sender, System.EventArgs e)
        {
            new LINQForm().Show();

        }

        //ApplicationWebService service;

        private void button7_Click(object sender, System.EventArgs e)
        {
            new SQLiteWithDataGridView.Library.GridForm
            {
                service = new ApplicationXWebService() 
            }.Show();
        }

        private void button4_Click_1(object sender, System.EventArgs e)
        {
            new PlasmaForm().Show();

        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {

        }

    }
}
