using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestFormGenerics;
using System;

namespace TestFormGenerics
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        public event Action<string> SendStringViaGeneric;

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (SendStringViaGeneric != null)
                SendStringViaGeneric("hello world");
        }

    }
}
