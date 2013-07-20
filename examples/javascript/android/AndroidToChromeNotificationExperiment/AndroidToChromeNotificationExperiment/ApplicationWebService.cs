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




            new Thread(
                delegate()
                {

                    try
                    {
                        var socket = new DatagramSocket(); //construct a datagram socket and binds it to the available port and the localhos


                        var b = Encoding.UTF8.GetBytes("hi from android!");    //creates a variable b of type byte
                        var dgram = new DatagramPacket((sbyte[])(object)b, b.Length, InetAddress.getByName("239.1.2.3"), 40404);//sends the packet details, length of the packet,destination address and the port number as parameters to the DatagramPacket  
                        socket.send(dgram); //send the datagram packet from this port
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine("server error " + new { ex.Message, ex.StackTrace });
                    }

                }
                             )
            {

                Name = "sender"
            }.Start();

        }


        // how would this look like?
        //internal void InitializeComponent(HTML.Pages.IApp page)
        //{

        //}
    }
}
