using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestCLRBroadcast
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void InvokeAdvertise()
        {
            advertise();
        }



        public void Handler(WebServiceHandler h)
        {
            if (advertise == null)
            {

                // X:\jsc.svn\examples\javascript\Test\TestWebMethodIPAddress\TestWebMethodIPAddress\ApplicationWebService.cs


                #region HostUri
                var Referer = h.Context.Request.Headers["Referer"];
                if (Referer == null)
                    Referer = "any";

                var HostUri = new
                {
                    Host = h.Context.Request.Headers["Host"].TakeUntilIfAny(":"),
                    Port = h.Context.Request.Headers["Host"].SkipUntilOrEmpty(":")
                };
                #endregion

                Console.WriteLine(new { HostUri });

                //compiled! launching server! please wait...
                //20426 -> 17094
                //http://192.168.43.252:20426

                //> 0001 0x0163 bytes
                //{ HostUri = { Host = 192.168.43.252, Port = 20426 } }



                advertise = async delegate
                {
                    var message =
                        new XElement("string",
                            new XAttribute("c", "" + 1),
                              "Visit me at " + HostUri.Host + ":" + HostUri.Port
                        ).ToString();


                    //Console.WriteLine(new { HostUri });
                    Console.WriteLine(new { message });

                    // android send
                    // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Library\Templates\Java\InternalAndroidWebServiceActivity.cs

                    // chrome send
                    // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServer\ChromeTCPServer\Application.cs

                    // clr send
                    // X:\jsc.svn\market\Abstractatech.Multicast\Abstractatech.Multicast\Library\MulticastListener.cs

                    // new clr send:

                    var port = new Random().Next(16000, 40000);

                    var socket = new UdpClient();


                    socket.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

                    socket.ExclusiveAddressUse = false;
                    socket.EnableBroadcast = true;

                    var loc = new IPEndPoint(IPAddress.Any, port);
                    socket.Client.Bind(loc);


                    //socket.JoinMulticastGroup(IPAddress.Parse("239.1.2.3"), 30);

                    //var remote = new IPEndPoint(IPAddress.Parse("239.1.2.3"), 40404);
                    //socket.Connect(remote);


                    var data = Encoding.UTF8.GetBytes(message.ToString());    //creates a variable b of type byte

                    // 
                    //Additional information: Cannot send packets to an arbitrary host while connected.
                    var result = await socket.SendAsync(data, data.Length, "239.1.2.3", 40404);
                    //var result = await socket.SendAsync(data, data.Length);


                    socket.Close();

                };

                advertise();

            }
        }

        static Action advertise;
    }



}
