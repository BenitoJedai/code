using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.Android.Manifest;
using ScriptCoreLib.Extensions;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using android.content;

namespace AndroidTCPServerAsync.Activities
{
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity : Activity
    {

        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            //ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            var b = new Button(this).AttachTo(ll);

            b.WithText("before AtClick");
            b.AtClick(
                v =>
                {
                    b.setText("AtClick");
                }
            );


            this.setContentView(sv);



            var s = new SemaphoreSlim(0);

            //java.lang.Object, rt
            //enter async { ManagedThreadId = 1 }
            //awaiting for SemaphoreSlim{ ManagedThreadId = 1 }
            //after delay{ ManagedThreadId = 8 }
            //http://127.0.0.1:8080
            //{ fileName = http://127.0.0.1:8080 }
            //enter catch { mname = <0032> nop.try } ClauseCatchLocal:
            //{ Message = , StackTrace = java.lang.RuntimeException
            //        at ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__TcpListener.AcceptTcpClientAsync(__TcpListener.java:131)

            new { }.With(
                async delegate
                {
                    //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
                    //enter async { ManagedThreadId = 1 }
                    //awaiting for SemaphoreSlim{ ManagedThreadId = 1 }
                    //after delay{ ManagedThreadId = 4 }
                    //http://127.0.0.1:8080
                    //awaiting for SemaphoreSlim. done.{ ManagedThreadId = 1 }
                    //--
                    //accept { c = System.Net.Sockets.TcpClient, ManagedThreadId = 6 }
                    //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
                    //accept { c = System.Net.Sockets.TcpClient, ManagedThreadId = 8 }
                    //{ ManagedThreadId = 6, input = GET / HTTP/1.1


                    Console.WriteLine("enter async " + new { Thread.CurrentThread.ManagedThreadId });

                    // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAsync\ChromeTCPServerAsync\Application.cs
                    await Task.Delay(100);

                    Console.WriteLine("after delay" + new { Thread.CurrentThread.ManagedThreadId });

                    // Additional information: Only one usage of each socket address (protocol/network address/port) is normally permitted
                    // close the other server!
                    var l = new TcpListener(IPAddress.Any, 8080);

                    l.Start();


                    var href =
                        "http://127.0.0.1:8080";

                    Console.WriteLine(
                        href
                    );



                    this.runOnUiThread(
                        delegate
                        {
                            var i = new Intent(Intent.ACTION_VIEW,
                               android.net.Uri.parse(href)
                           );

                            // http://vaibhavsarode.wordpress.com/2012/05/14/creating-our-own-activity-launcher-chooser-dialog-android-launcher-selection-dialog/
                            var ic = Intent.createChooser(i, href);


                            this.startActivity(ic);
                        }
                    );



                    new { }.With(
                        async delegate
                        {
                            while (true)
                            {
                                var c = await l.AcceptTcpClientAsync();

                                Console.WriteLine("accept " + new { c, Thread.CurrentThread.ManagedThreadId });

                                yield(c);
                            }
                        }
                    );

                    // jump back to main thread..
                    s.Release();
                }
            );
        }


        static async void yield(TcpClient c)
        {
            var s = c.GetStream();

            // could we switch into a worker thread?
            // jsc would need to split the stream object tho

            var buffer = new byte[1024];
            // why no implict buffer?
            var count = await s.ReadAsync(buffer, 0, buffer.Length);

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
