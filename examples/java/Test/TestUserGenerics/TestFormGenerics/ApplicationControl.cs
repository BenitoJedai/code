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

        void S0()
        {
            try
            {
                S1();
            }
            catch (csharp.ThrowableException t)
            {
                // jsc: just "throw" does not yet seem to work? :)
                // jsc: just "throw t" does not yet seem to work? :)

                throw new InvalidOperationException();
            }
        }

        void S1()
        {

        }
    }
}
