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
using System.Net.NetworkInformation;

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

            //textBox1.Enabled = false;
            //textBox2.Enabled = false;
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

        private async void button1_Click(object sender, EventArgs e)
        {
            var externalPort = int.Parse(textBox1.Text);
            var internalPort = int.Parse(textBox2.Text.SkipUntilOrEmpty(":"));
            var internalHost = textBox2.Text.TakeUntilOrEmpty(":");

            button1.Enabled = false;

            MessageBox.Show("about to start UDP forwarding. " + new
            {

                externalPort,
                internalHost,
                internalPort
            });

            #region log
            Action<string> log =
                text =>
                {
                    Console.WriteLine(new { text });

                    this.Invoke(
                        new Action(
                            delegate
                            {
                                this.button1.Text = text;
                            }
                        )
                    );
                };
            #endregion



            new { }.With(
                async delegate
                {
                    var u = new UdpClient(externalPort);

                    while (true)
                    {
                        var x = await u.ReceiveAsync();
                        var data = x.Buffer;

                        log("> " + data.Length);


                        var socket = new UdpClient();

                        var s = await socket.SendAsync(
                            data,
                            data.Length,
                            hostname: internalHost,
                            port: internalPort
                        );
                    }
                }
            );

        }




        private void UserControl1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("enter UserControl1_Load");

            //I/System.Console( 6337): enter UserControl1_Load
            //D/dalvikvm( 6337): GC_CONCURRENT freed 460K, 6% free 8916K/9412K, paused 4ms+3ms, total 39ms
            //D/dalvikvm( 6337): WAIT_FOR_CONCURRENT_GC blocked 17ms
            //W/dalvikvm( 6337): Exception Ljava/lang/RuntimeException; thrown while initializing LScriptCoreLibJava/BCLImplementation/System/Net/__IPAddress;

            var data =
                from n in NetworkInterface.GetAllNetworkInterfaces()
                //let SupportsMulticast = n.SupportsMulticast

                let UnicastAddresses = n.GetIPProperties().UnicastAddresses
                //.With(
                //    xUnicastAddresses =>
                //    {
                //        Console.WriteLine("UserControl1_Load " + new { xUnicastAddresses });
                //    }
                //)

                from u in UnicastAddresses

                let IsLoopback = IPAddress.IsLoopback(u.Address)
                let IPv4 = u.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork

                // http://compnetworking.about.com/od/workingwithipaddresses/l/aa042400b.htm
                // http://ipaddressextensions.codeplex.com/SourceControl/latest#WorldDomination.Net/IPAddressExtensions.cs

                let get_IsPrivate = new Func<bool>(
                delegate
                {
                    Console.WriteLine("get_IsPrivate " + new { n.Description, u.Address, IPv4 });

                    var AddressBytes = u.Address.GetAddressBytes();

                    // should do a full mask check?
                    // http://en.wikipedia.org/wiki/IP_address
                    //var PrivateAddresses = new[] { 10, 172, 192 };

                    if (AddressBytes[0] == 10)
                        return true;

                    if (AddressBytes[0] == 172)
                        return true;

                    if (AddressBytes[0] == 192)
                        return true;

                    return false;

                }
                )


                let IsPrivate = get_IsPrivate()

                //let IsExternalCandidate =  !IsLoopback && IPv4

                select new
                {
                    IsPrivate,
                    IsLoopback,
                    IPv4,
                    //IsExternalCandidate,

                    u,
                    n
                };

            //I/System.Console(10259): enter __IPAddress
            //I/System.Console(10259): enter __IPAddress worker
            //I/System.Console(10259): exit __IPAddress { ElapsedMilliseconds = 4 }
            //I/System.Console(10259): exit __IPAddress worker { ElapsedMilliseconds = 6 }
            //I/System.Console(10259): get_IsPrivate { Description = lo, Address = ::1%1, IPv4 = false }
            //I/System.Console(10259): get_IsPrivate { Description = lo, Address = 127.0.0.1, IPv4 = true }
            //I/System.Console(10259): get_IsPrivate { Description = rmnet0, Address = 10.144.210.50, IPv4 = true }
            // 217.71.46.50


            //1     5 ms     7 ms     7 ms  192.168.43.1
            //2     *        *        *     Request timed out.
            //3  1645 ms   613 ms    65 ms  172.18.6.22
            //4   876 ms   469 ms    50 ms  rtr-int02.emt.ee [217.71.33.189]


            //            I/System.Console( 8343): enter __IPAddress
            //I/System.Console( 8343): enter __IPAddress worker
            //I/System.Console( 8343): exit __IPAddress { ElapsedMilliseconds = 7 }
            //I/System.Console( 8343): exit __IPAddress worker { ElapsedMilliseconds = 13 }
            //I/System.Console( 8343): get_IsPrivate { Description = pdp0, Address = 83.191.168.127, IPv4 = true }

            //1     2 ms     3 ms     1 ms  192.168.43.1
            //2  Transmit error: code 1231.








            var zIsExternalCandidate = data.FirstOrDefault(x => !x.IsLoopback);

            label1.Text += new { zIsExternalCandidate }.ToString();

        }
    }
}


//W/ActivityManager(  375): Activity idle timeout for ActivityRecord{43018028 u0 InteractivePortForwarding.Activities/.ApplicationActivity}
//W/ActivityManager(  375): Activity stop timeout for ActivityRecord{43018028 u0 InteractivePortForwarding.Activities/.ApplicationActivity}
//I/ActivityManager(  375): Activity reported stop, but no longer stopping: ActivityRecord{43018028 u0 InteractivePortForwarding.Activities/.ApplicationActivity}