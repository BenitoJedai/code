using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestMinimizeToSidebar
{
    public partial class ApplicationForm : Form
    {
        public ApplicationForm()
        {
            InitializeComponent();
        }

        public event Action<Form> TimeToSwitch;

        private void button1_Click(object sender, EventArgs e)
        {
            var f = new ApplicationForm { };
            
            f.Show();

            if (TimeToSwitch != null)
                TimeToSwitch(f);
        }
    }
}
