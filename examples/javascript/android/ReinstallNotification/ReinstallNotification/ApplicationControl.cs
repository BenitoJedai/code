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

        public Action Notify = delegate { };

        private void applicationWebServiceWithEvents1_oninstall(string packageName)
        {
            Console.WriteLine("applicationWebServiceWithEvents1_oninstall " + new { packageName });

            // wont work within chrome.webview
            Notify();

            comboBox1.Items.Add(packageName);

            //MessageBox.Show(new { packageName }.ToString());
        }

        private void ApplicationControl_Load(object sender, EventArgs e)
        {

        }

    }
}
