using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeekerWithStarlingT10.Library
{



    public partial class FrameDiagnostics : Form
    {
        public FrameDiagnostics()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }


        public void Initialize(IFrameDiagnostics e)
        {
            Console.WriteLine("FrameDiagnostics.Initialize");
            this.checkBox1.CheckedChanged +=
                delegate
                {

                    e.user_pause.value = Convert.ToString(this.checkBox1.Checked);
                };

            this.checkBox2.CheckedChanged +=
                 delegate
                 {
                     e.hidetrees.value = Convert.ToString(this.checkBox2.Checked);
                 };

            this.checkBox3.CheckedChanged +=
                 delegate
                 {
                     e.hidegroundunits.value = Convert.ToString(this.checkBox3.Checked);
                 };

            this.checkBox4.CheckedChanged +=
                 delegate
                 {
                     e.hideairunits.value = Convert.ToString(this.checkBox4.Checked);
                 };

            this.button1.Click +=
               delegate
               {
                   e.F1();
               };
        }

        private void FrameDiagnostics_Load(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
