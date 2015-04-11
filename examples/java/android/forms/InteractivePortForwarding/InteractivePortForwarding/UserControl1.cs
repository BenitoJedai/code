using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using ScriptCoreLib.Extensions;

namespace InteractivePortForwarding
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
    

            var externalPort = int.Parse(textBox1.Text);
            var internalPort = int.Parse(textBox2.Text.SkipUntilOrEmpty(":"));
            var internalHost = textBox2.Text.TakeUntilOrEmpty(":");

			textBox1.Enabled = false;
			textBox2.Enabled = false;
			button2.Enabled = false;
			MessageBox.Show("about to start. " + new
            {

                externalPort,
                internalHost,
                internalPort
            });

            await Task.Delay(100);

            #region log
            Action<string> log =
                text =>
                {
                    this.Invoke(
                        new Action(
                            delegate
                            {
                                this.button2.Text = text;
                            }
                        )
                    );
                };
            #endregion

            log("> " + new { externalPort, Environment.CurrentManagedThreadId });

            var l = new TcpListener(IPAddress.Any, 8080);

            l.Start();

            while (true)
            {
                var c = await l.AcceptTcpClientAsync();

                log("accept " + new { c, Thread.CurrentThread.ManagedThreadId });

                //[javac] Compiling 694 source files to W:\bin\classes
                //[javac] W:\src\InteractivePortForwarding\UserControl1__button2_Click_d__e__MoveNext_0600002a.java:983: error: incompatible types
                //[javac]         class70 = /* unbox <>c__DisplayClass7 */ref_arg2[0].__t__stack;
                //[javac]                                                            ^
                //[javac]   required: UserControl1___c__DisplayClass7
                //[javac]   found:    Object

                forward(internalPort, internalHost, c);

            }
        }

        private static async void forward(int internalPort, string internalHost, TcpClient c)
        {
            var cexternal = c;
            var cinternal = new TcpClient();

            await cinternal.ConnectAsync(internalHost, internalPort);

            Action close = delegate
            {
                cexternal.Close();
                cinternal.Close();
            };

            forward("> ", cexternal.GetStream(), cinternal.GetStream(), close);
            forward("< ", cinternal.GetStream(), cexternal.GetStream(), close);
        }

        static async void forward(string prefix, NetworkStream from, NetworkStream to, Action close)
        {

            var buffer = new byte[1024 * 1024];

            do
            {
                // why no implict buffer?
                var count = await from.ReadAsync(buffer, 0, buffer.Length);

                Console.WriteLine(prefix + count);

                //                I/System.Console( 6199): > 393
                //I/System.Console( 6199): < 85
                //I/System.Console( 6199): < -1

                if (count < 0)
                {
                    close();
                    return;
                }

                await to.WriteAsync(buffer, 0, count);
            }
            while (true);
        }

        static async void yield(TcpClient c)
        {
            var s = c.GetStream();

            // could we switch into a worker thread?
            // jsc would need to split the stream object tho

            var buffer = new byte[1024];
            // why no implict buffer?
            var count = await s.ReadAsync(buffer, 0, buffer.Length);

            // IPv4 Address. . . . . . . . . . . : 192.168.1.196

            var input = Encoding.UTF8.GetString(buffer, 0, count);

            //new IHTMLPre { new { input } }.AttachToDocument();
            Console.WriteLine(new { Thread.CurrentThread.ManagedThreadId, input });


            // http://stackoverflow.com/questions/369498/how-to-prevent-iframe-from-redirecting-top-level-window
            var outputString = @"HTTP/1.0 200 OK 
Content-Type:	text/html; charset=utf-8
Connection: close

<body><h1 style='color: red;'>Hello world</h2><h3>jsc</h3>
hello world. jvm clr android async tcp? udp?<iframe  sandbox='allow-forms' src='http://www.whatsmyip.us/'><iframe>
</body>
";
            var obuffer = Encoding.UTF8.GetBytes(outputString);

            await s.WriteAsync(obuffer, 0, obuffer.Length);

            c.Close();
        }
    }
}
