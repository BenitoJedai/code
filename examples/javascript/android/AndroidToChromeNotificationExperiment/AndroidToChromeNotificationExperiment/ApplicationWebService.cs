using java.net;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace AndroidToChromeNotificationExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void NotifyChromeViaLANBroadcast(string e, Action<string> y)
        {
            // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Library\Templates\Java\InternalAndroidWebServiceActivity.cs



#if !DEBUG
            Console.WriteLine("NotifyChromeViaLANBroadcast");



            #region Send
            int c = 0;
            Action<string, string, string> Send = (string data, string preview, string nn) =>
            {
                /// http://www.daniweb.com/software-development/java/threads/424998/udp-client-server-in-java

                c++;

                //var n = c + " hello world";
                var message =
                    new XElement("string",
                        new XAttribute("c", "" + c),
                        new XAttribute("preview", preview),
                        new XAttribute("n", nn),
                        data
                    ).ToString();

                Console.WriteLine(new { message });

                new Thread(
                    delegate()
                    {
                        try
                        {
                            var socket = new DatagramSocket(); //construct a datagram socket and binds it to the available port and the localhos
                            byte[] b = Encoding.UTF8.GetBytes(message.ToString());    //creates a variable b of type byte
                            var dgram = new DatagramPacket((sbyte[])(object)b, b.Length, InetAddress.getByName("239.1.2.3"), 40404);//sends the packet details, length of the packet,destination address and the port number as parameters to the DatagramPacket  

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
            };
            #endregion


            // send one without image too...
            Send(
                "Visit me at 127.0.0.1:80",
                "",
                "foo.bar"
            );
#endif

        }

        public void Handler()
        {

        }

        // how would this look like?
        //internal void InitializeComponent(HTML.Pages.IApp page)
        //{

        //}
    }
}
