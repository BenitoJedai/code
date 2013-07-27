using android.content;
using ScriptCoreLib;
using ScriptCoreLibJava.Extensions;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using java.io;
using System.ComponentModel;
using android.net.wifi;
using java.net;
using System.IO;
using System.Threading;

namespace com.abstractatech.adminshell
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {

        public void ShellAsync(string e, Action<string> y)
        {
            // http://a3nm.net/blog/android_cli.html
#if Android
            // http://www.android.pk/blog/general/launch-app-through-adb-shell/
            //  am start -a android.intent.action.MAIN -n com.android.settings/.Settings
            // am start tel:210-385-0098
            // am start -a android.intent.action.CALL tel:245007
            // am start -a android.intent.action.SENDTO "sms:5245007" -e "sms_body" "heyy"   && input keyevent 22 && input keyevent 66
            // am start -a android.intent.action.SENDTO -d sms:1234567890 --es sms_body ohai --ez exit_on_sent true
            // am start -a android.intent.action.SENDTO -d smsto:245007 --es sms_body ":*" --ez exit_on_sent true && am start -a android.intent.action.SENDTO -d sms:5245007 --es sms_body ":*" --ez exit_on_sent true && input keyevent 22 && input keyevent 66
            // pm list packages
            // pm list packages -f
            //http://stackoverflow.com/questions/11201659/android-adb-shell-dumpsys-tool
            // am start -S -e sms_body 'your message body' \
            //-e address receiver -t 'vnd.android-dir/mms-sms' \
            //com.android.mms/com.android.mms.ui.ComposeMessageActivity \
            //&& adb shell input keyevent 66

            //am start -n com.google.android.youtube/.PlayerActivity -d http://www.youtube.com/watch?v=MTT-crZBB0k
            // http://stackoverflow.com/questions/7095470/android-read-send-text-messages-on-ubuntu

            //         System.InvalidOperationException: Sequence contains more than one element
            //at System.Linq.Enumerable.SingleOrDefault[TSource](IEnumerable`1 source)
            //at jsc.Languages.Java.JavaCompiler.GetArrayEnumeratorType() in x:\jsc.internal.svn\compiler\jsc\Languages\Java\JavaCompiler.overrride.cs:line 52
            //at jsc.Languages.Java.JavaCompiler.GetImportTypes(Type t, Boolean bExcludeJavaLang) in x:\jsc.internal.svn\compiler\jsc\Languages\Java\JavaCompiler.WriteImportTypes.cs:line 363
            //at jsc.Languages.Java.JavaCompiler.WriteImportTypes(Type ContextType) in x:\jsc.internal.svn\compiler\jsc\Languages\Java\JavaCompiler.WriteImportTypes.cs:line 22
            //at jsc.Languages.Java.JavaCompiler.CompileType(Type z) in x:\jsc.internal.svn\compiler\jsc\Languages\Java\JavaCompiler.CompileType.cs:line 43
            //at jsc.Languages.CompilerJob.<>c__DisplayClass1a.<CompileJava>b__17(Type xx) in x:\jsc.internal.svn\compiler\jsc\Languages\Java\CompilerJob.cs:line 120

            //            IsArrayEnumerator: ScriptCoreLib.Shared.BCLImplementation.System.__SZArrayEnumerator`1, ScriptCoreLibAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            //IsArrayEnumerator: ScriptCoreLib.Shared.BCLImplementation.System.__SZArrayEnumerator`1, ScriptCoreLibJava, Version=4.1.0.0, Culture=neutral, PublicKeyToken=null


            try
            {
                // http://stackoverflow.com/questions/9062182/android-icmp-ping

                var p = new java.lang.ProcessBuilder(new[] { "sh" }).redirectErrorStream(true).start();

                var os = new DataOutputStream(p.getOutputStream());
                //os.writeBytes(e + '\n');
                os.writeBytes(e + "\n");
                os.flush();

                // Close the terminal
                os.writeBytes("exit\n");
                os.flush();

                // read ping replys
                var reader = new BufferedReader(new InputStreamReader(p.getInputStream()));
                string line = reader.readLine();


                while (line != null)
                {
                    y(line);
                    line = reader.readLine();
                }
            }
            catch (System.Exception ex)
            {
                y("AndroidShellAsync error: " + new { ex.Message });

            }

#else
            y("ShellAsync not implemented.");
#endif

        }


        public /* will not be part of web service itself */ void Handler(WebServiceHandler h)
        {
            // http://tools.ietf.org/html/rfc2617#section-3.2.1

            var Authorization = h.Context.Request.Headers["Authorization"];

            var AuthorizationLiteralEncoded = Authorization.SkipUntilOrEmpty("Basic ");
            var AuthorizationLiteral = Encoding.ASCII.GetString(
                Convert.FromBase64String(AuthorizationLiteralEncoded)
            );

            var AuthorizationLiteralCredentials = new
            {
                user = AuthorizationLiteral.TakeUntilOrEmpty(":"),
                password = AuthorizationLiteral.SkipUntilOrEmpty(":"),
            };

            var Host = h.Context.Request.Headers["Host"].TakeUntilIfAny(":");


            System.Console.WriteLine(
                new
                {
                    AuthorizationLiteralCredentials,
                    Host,
                    h.Context.Request.UserHostAddress,
                }.ToString());

            var a = h.Applications.FirstOrDefault(k => k.TypeName == "a");

            if (h.Context.Request.Path == "/a")
            {
                var OK = false;


                if (Host == h.Context.Request.UserHostAddress)
                    OK = true;

                if (!string.IsNullOrEmpty(AuthorizationLiteralCredentials.user))
                    if (!string.IsNullOrEmpty(AuthorizationLiteralCredentials.password))
                        OK = true;

                if (OK)
                {
                    //#if Android
                    //                        var c = ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext;

                    //                        var intent = new Intent(c, typeof(foo.NotifyService).ToClass());

                    //                        intent.putExtra("data0", AuthorizationLiteralCredentials.user + " is using Remote Web Shell");

                    //                        c.startService(intent);
                    //#endif



                    h.Context.Response.ContentType = "text/javascript";
                    h.Context.Response.AddHeader("Cache-Control", "max-age=2592000");


                    //Implementation not found for type import :
                    //type: System.Web.HttpResponse
                    //method: Void AppendCookie(System.Web.HttpCookie)
                    // not working on android?
                    h.Context.Response.SetCookie(
                        new System.Web.HttpCookie("foo", "bar")
                    );

                    h.WriteSource(a);
                    h.CompleteRequest();
                    return;
                }

                h.Context.Response.StatusCode = 401;
                h.Context.Response.AddHeader(
                    "WWW-Authenticate",
                    "Basic realm=\"Android\""
                );

                // flush?
                h.Context.Response.Write(" ");
                h.CompleteRequest();

                return;
            }
        }





        public void DownloadSDK(WebServiceHandler h)
        {
            var HostUri = new
            {
                Host = h.Context.Request.Headers["Host"].TakeUntilIfAny(":"),
                Port = int.Parse(h.Context.Request.Headers["Host"].SkipUntilIfAny(":"))
            };


            //#if DEBUG
            //            // An attempt was made to access a socket in a way forbidden by its access permissions
            //            if (InternalMulticast == null)
            //                InternalMulticast = new WithClickOnceLANLauncher.ApplicationWebServiceMulticast
            //                {
            //                    Host = HostUri.Host,
            //                    Port = HostUri.Port,

            //                };
            //#else
            //            if (InternalMulticast == null)
            //                InternalMulticast = new AndroidApplicationWebServiceMulticast
            //                {
            //                    Host = HostUri.Host,
            //                    Port = HostUri.Port,

            //                };

            //            if (h.IsDefaultPath)
            //            {
            //                new Thread(
            //                      delegate()
            //                      {


            //                          InternalMulticast.SendVisitMeAt();
            //                      }
            //                                   )
            //                  {

            //                      Name = "client"
            //                  }.Start();

            //            }

            //#endif

            DownloadSDKFunction.DownloadSDK(h);

        }

        //#if DEBUG
        //        static WithClickOnceLANLauncher.ApplicationWebServiceMulticast InternalMulticast;
        //#else
        //        static AndroidApplicationWebServiceMulticast InternalMulticast;

        //#endif

    }

    class AndroidApplicationWebServiceMulticast : Component
    {
        WifiManager wifi;
        WifiManager.MulticastLock multicastLock;

        public event Action<string> AtData;

        public AndroidApplicationWebServiceMulticast()
        {
            AtData += AndroidApplicationWebServiceMulticast_AtData;

            new Thread(
                delegate()
                {
                    // http://stackoverflow.com/questions/12610415/multicast-receiver-malfunction
                    // http://answers.unity3d.com/questions/250732/android-build-is-not-receiving-udp-broadcasts.html

                    // Acquire multicast lock
                    wifi = (WifiManager)
                        ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext.getSystemService(Context.WIFI_SERVICE);
                    multicastLock = wifi.createMulticastLock("multicastLock");
                    //multicastLock.setReferenceCounted(true);
                    multicastLock.acquire();

                    System.Console.WriteLine("LANBroadcastListener ready...");
                    try
                    {
                        byte[] b = new byte[0x100];

                        // https://code.google.com/p/android/issues/detail?id=40003

                        MulticastSocket socket = new MulticastSocket(40404); // must bind receive side
                        socket.setBroadcast(true);
                        socket.setReuseAddress(true);
                        socket.setTimeToLive(30);
                        socket.setReceiveBufferSize(0x100);

                        socket.joinGroup(InetAddress.getByName("239.1.2.3"));
                        System.Console.WriteLine("LANBroadcastListener joinGroup...");
                        while (true)
                        {
                            DatagramPacket dgram = new DatagramPacket((sbyte[])(object)b, b.Length);
                            socket.receive(dgram); // blocks until a datagram is received

                            var bytes = new MemoryStream((byte[])(object)dgram.getData(), 0, dgram.getLength());


                            var listen = Encoding.UTF8.GetString(bytes.ToArray());



                            //dgram.setLength(b.Length); // must reset length field!s

                            if (AtData != null)
                                AtData(listen);

                        }
                    }
                    catch
                    {
                        System.Console.WriteLine("client error");
                    }
                }
            )
            {

                Name = "client"
            }.Start();


        }

        void AndroidApplicationWebServiceMulticast_AtData(string listen)
        {
            System.Console.WriteLine(

               new { server = new { listen } }
               );

            try
            {
                var xml = XElement.Parse(listen);

                if (xml.Value.StartsWith("Where are you?"))
                {
                    SendVisitMeAt();

                }
            }
            catch
            {

            }


        }

        public void SendVisitMeAt()
        {
            System.Console.WriteLine("SendVisitMeAt");

            this.Send(
                "Visit me at " + this.Host + ":" + this.Port
            );
        }

        int c;
        void Send(string data)
        {
            /// http://www.daniweb.com/software-development/java/threads/424998/udp-client-server-in-java

            c++;

            //var n = c + " hello world";
            var n =
                new XElement("string",
                    new XAttribute("c", "" + c),
                    data
                ).ToString();

            new Thread(
                delegate()
                {
                    try
                    {
                        DatagramSocket socket = new DatagramSocket(); //construct a datagram socket and binds it to the available port and the localhos
                        byte[] b = Encoding.UTF8.GetBytes(n.ToString());    //creates a variable b of type byte
                        DatagramPacket dgram;
                        dgram = new DatagramPacket((sbyte[])(object)b, b.Length, InetAddress.getByName("239.1.2.3"), 40404);//sends the packet details, length of the packet,destination address and the port number as parameters to the DatagramPacket  

                        socket.send(dgram); //send the datagram packet from this port
                    }
                    catch
                    {
                        System.Console.WriteLine("server error");
                    }
                }
            )
            {

                Name = "server"
            }.Start();
        }

        public int Port { get; set; }
        public string Host { get; set; }

    }

}
