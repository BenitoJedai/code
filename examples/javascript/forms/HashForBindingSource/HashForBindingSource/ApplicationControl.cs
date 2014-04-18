using HashForBindingSource;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HashForBindingSource
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            // um this event no longer fires?
            // wh the fk not?

            Debugger.Break();


        }

        private void button1_Click(object sender, System.EventArgs e)
        {

            // set it to come from the server?
            // can the client actually stream?
            // can the server actually stream?

            //this.webBrowser1.DocumentStream 

            this.webBrowser1.DocumentText =

                // rename to DocumentText ?
                HashForBindingSource.HTML.Pages.FooSource.Text;
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            this.webBrowser1.DocumentText =

                // rename to DocumentText ?
                HashForBindingSource.HTML.Pages.GooSource.Text;
        }

    }
}
