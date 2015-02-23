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
using java.util;
using java.net;
using android.util;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace AndroidTcpListenerActivity.Activities
{

    public delegate void NetworkStreamAction(NetworkStream s);

    public class ApplicationActivity : Activity
    {
        ScriptCoreLib.Android.IAssemblyReferenceToken ref1;


        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            //ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);


            var b = new Button(this).AttachTo(ll);


            var ip = getLocalIpAddress();



            b.WithText("server at " + ip);
            b.AtClick(
                v =>
                {
                    var random = new System.Random();

                    // Error 312 (net::ERR_UNSAFE_PORT): Unknown error.
                    var port = random.Next(1024, 32000);

                    var uri = "http://" + ip;

                    uri += ":";
                    uri += ((object)(port)).ToString();

                    b.setText(uri);

                    Toast.makeText(
                          this,
                          "connect to this web server", 
                          Toast.LENGTH_LONG
                      ).show();

                    var ipa = Dns.GetHostAddresses(ip)[0];


                    Action<string> log = x => Log.wtf("ApplicationActivity", x);

                    // jsc does not import generic param, why?
                    //Action<NetworkStream> AtConnection =
                    NetworkStreamAction AtConnection =
                        s =>
                        {

                            //log("AtConnection");

                            var r = new StreamReader(s);

                            var h0 = r.ReadLine();

                            //log("ReadLine done");

                            var m = new MemoryStream();

                            Action<string> WriteLineASCII = (string e) =>
                            {
                                var x = Encoding.ASCII.GetBytes(e + "\r\n");

                                m.Write(x, 0, x.Length);
                            };

                            WriteLineASCII("HTTP/1.1 200 OK");
                            WriteLineASCII("Content-Type:	text/html; charset=utf-8");
                            //WriteLineASCII("Content-Length: " + data.Length);
                            WriteLineASCII("Connection: close");


                            WriteLineASCII("");
                            WriteLineASCII("");
                            WriteLineASCII("<html>");

                            WriteLineASCII("<body><h1 style='color: red;'>Hello world</h2><h3>jsc</h3><pre>" + h0 + "</pre></body>");

                            WriteLineASCII("</html>");

                            log("write done");

                            var oa = m.ToArray();

                            s.Write(oa, 0, oa.Length);

                            s.Flush();
                            s.Close();
                        };

                    //                    AndroidTcpListenerActivity.AndroidActivity 003e create: AndroidTcpListenerActivity.Activities.ApplicationActivity+<>c__DisplayClass8+<>c__DisplayClassb
                    //switch to STA Exception:
                    //System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. ---> System.ArgumentException: The IL Generator cannot be used while there are unclosed exceptions.
                    //   at System.Reflection.Emit.ILGenerator.BakeByteArray()
                    //   at System.Reflection.Emit.MethodBuilder.CreateMethodBodyHelper(ILGenerator il)
                    //   at System.Reflection.Emit.TypeBuilder.CreateTypeNoLock()
                    //   at System.Reflection.Emit.TypeBuilder.CreateType()

                    new Thread(
                            delegate()
                            {
                                var r = new TcpListener(ipa, port);

                                //try
                                //{
                                r.Start();

                                while (true)
                                {
                                    //log("AcceptTcpClient");
                                    var c = r.AcceptTcpClient();
                                    //log("AcceptTcpClient done, GetStream");

                                    var s = c.GetStream();
                                    //log("AcceptTcpClient done, GetStream done");

                                    new Thread(
                                        delegate()
                                        {
                                            //log("before AtConnection");
                                            AtConnection(s);
                                        }
                                    )
                                    {
                                        IsBackground = true,
                                    }.Start();
                                }

                                //}
                                //catch
                                //{
                                //    log("AcceptTcpClient error!");

                                //    throw;
                                //}


                            }
                        )
                    {
                        IsBackground = true,
                    }.Start();
                }
            );

            var b2 = new Button(this);
            b2.setText("The other button!");
            ll.addView(b2);

            this.setContentView(sv);
        }

        public static string getLocalIpAddress()
        {
            var value = "";

            try
            {
                for (Enumeration en = NetworkInterface.getNetworkInterfaces(); en.hasMoreElements(); )
                {
                    NetworkInterface intf = (NetworkInterface)en.nextElement();
                    for (Enumeration enumIpAddr = intf.getInetAddresses(); enumIpAddr.hasMoreElements(); )
                    {
                        InetAddress inetAddress = (InetAddress)enumIpAddr.nextElement();

                        Log.wtf("getLocalIpAddress", inetAddress.getHostAddress().ToString());

                        var v6 = inetAddress is Inet6Address;

                        if (v6)
                        {
                        }
                        else if (!inetAddress.isLoopbackAddress())
                        {
                            if (value == "")
                                value = inetAddress.getHostAddress().ToString();
                        }
                    }
                }
            }
            catch
            {
            }

            if (value == "")
            {
                // no wifi
                value = "127.0.0.1";
            }

            return value;
        }
    }


}
