using ShellWithPing;
using ShellWithPing.Library;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;

namespace ShellWithPing
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            button1.Text = "...";

            applicationWebService1.PING_InvokeAsync("8.8.8.8",
                y =>
                {
                    button1.Text += "\n" + y;
                }
            );
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            var c = new ConsoleWindow().AppendLine(
@"example:
  go http://zproxy.wordpress.com
  ping 8.8.8.8
  bing x*x = y - 2
");

            c.Show();

            //System.Console.In.ReadLineAsync();

            c.AtCommand +=
                async (x, y) =>
                {
                    if (x.StartsWith("bing "))
                    {
                        var url = x.SkipUntilOrEmpty("bing ");

                        var f = new Form { Text = url };
                        var w = new WebBrowser { Dock = DockStyle.Fill }.AttachTo(f);
                        //w.Navigate("http://lmgtfy.com/?q=" + url);

                        w.Navigate("http://www.bing.com/search?q=" + url);

                        f.Show();
                        return;

                    }

                    if (x.StartsWith("go "))
                    {
                        var url = x.SkipUntilOrEmpty("go ");

                        var f = new Form { Text = url };
                        var w = new WebBrowser { Dock = DockStyle.Fill }.AttachTo(f);
                        w.Navigate(url);
                        f.Show();
                        return;

                    }

                    applicationWebService1.EchoAsync(x, y);

                };
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {

        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            var c = new ConsoleWindow { Text = "Administrator Shell", Color = Color.Red }
                .AppendLine(
@" *** WARNING *** be careful!
example:
 am start -a android.intent.action.CALL tel:245007
");

            c.Show();

            c.AtCommand += applicationWebService1.ShellAsync;
        }

    }
}
