using AndroidNFCEvents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AndroidNFCEvents
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }


        private void applicationControl_onnfc1_onnfc(string id)
        {
            Console.WriteLine("applicationWebService_onnfc1_onnfc " + new { id });

            // wont work within chrome.webview

            listBox1.Items.Add(id);
        }


        private void applicationControl_onnfc1_onnfc_syncframe(int syncframe)
        {
            this.label1.Text = new { syncframe }.ToString();
        }

        private void ApplicationControl_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add("list of tags found:");

        }

    }
}
