using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FormsAppletWithMultipleJavaReferences;
using System;

namespace FormsAppletWithMultipleJavaReferences
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        public event Action AtClick;

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (AtClick != null)
                AtClick();
        }

    }
}
