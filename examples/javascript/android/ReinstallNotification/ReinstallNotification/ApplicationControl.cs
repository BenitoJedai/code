using ReinstallNotification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReinstallNotification.Activities
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void applicationWebServiceWithEvents1_oninstall(string packageName)
        {
            Console.WriteLine("applicationWebServiceWithEvents1_oninstall " + new { packageName });
            MessageBox.Show(new { packageName }.ToString());
        }

    }
}
