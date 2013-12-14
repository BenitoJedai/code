using TestAsyncCancel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace TestAsyncCancel
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var x = Foo();


        }

        public async Task Foo()
        {
            // http://msdn.microsoft.com/en-us/library/dd997396(v=vs.110).aspx
            // we need to upgrade throw/catch
            // to support cancellation

            for (int i = 0; i < 33; i++)
            {
                Task.Delay(3000);

                button1.Text = new { i }.ToString();

            }

        }
    }
}
