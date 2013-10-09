using FormsWebServiceViaInheritance;
using FormsWebServiceViaInheritance.Library;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsWebServiceViaInheritance
{
    public partial class ApplicationControl : ApplicationWebService
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private async void button1_Click(object sender, System.EventArgs e)
        {
            button1.Enabled = false;

            button1.Text = "in 3...";


            await Task.Delay(2000);
            button1.Text = "in 2...";

            await Task.Delay(1000);
            button1.Text = "in 1...";
            await Task.Delay(1000);
            button1.Text = "...";

            Foo = "before await";

            var x = await GetString("button1_Click");


            button1.Text = x;

            button1.Enabled = true;
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            var x = new TimerDisplay();
            this.Controls.Add(x);
            x.Go();

        }

        private async void button3_Click(object sender, System.EventArgs e)
        {
            var x = await this.GetTimerDisplay();
            this.Controls.Add(x);
            x.Go();
        }

    }
}
