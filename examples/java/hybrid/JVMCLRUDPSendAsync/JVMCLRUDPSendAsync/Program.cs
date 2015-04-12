using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JVMCLRUDPSendAsync
{
    public class Class1
    {
        public Class1()
        {
        }
    }

    public class Class1<T>
    {
    }


    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // 2012desktop?

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );


            // X:\jsc.svn\examples\java\Test\TestNestedTypeImport\TestNestedTypeImport\Class1.cs
            Action goo = async delegate
            {
                // X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPSendAsync\ChromeUDPSendAsync\Application.cs


                #region xml

                var nmessage = "hello world";
                var Host = "";
                var PublicPort = "";

                var message =
                    new XElement("string",
                        new XAttribute("c", "" + 1),
                        new XAttribute("n", nmessage),
                        "Visit me at " + Host + ":" + PublicPort
                    ).ToString();

                #endregion
                var data = Encoding.UTF8.GetBytes(message);	   //creates a variable b of type byte

                Console.WriteLine("hi from goo");


                var socket = new UdpClient();
#if FEnableBroadcast
				socket.EnableBroadcast = true;

                // bind?


                // http://stackoverflow.com/questions/13691119/chrome-packaged-app-udp-sockets-not-working

                // chrome likes 0 too.
                var port = 0;


                // where is bind async?
                socket.Client.Bind(
                    // 192.168.43.12

                    //new IPEndPoint(IPAddress.Any, port: 40000)
                    new IPEndPoint(IPAddress.Parse("192.168.43.12"), port)
                );

                // 


                //Additional information: A request to send or receive data was disallowed because the socket is not connected and (when sending on a datagram socket using a sendto call) no address was supplied

#endif

                //socket.Connect(
                //     "127.0.0.1", 40804
                //    );

                // X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPNotification\ChromeUDPNotification\Application.cs
                var s = await socket.SendAsync(
                    data,
                    data.Length,
                    //,
                    //hostname: "239.1.2.3",
                    hostname: "127.0.0.1",
                    port: 40804
                );

                //socket.ReceiveAsync
                socket.Close();
            };
            goo();





            CLRProgram.CLRMain();
        }


    }


    public delegate XElement XElementFunc();

    [SwitchToCLRContext]
    static class CLRProgram
    {
        public static XElement XML { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void CLRMain()
        {
            System.Console.WriteLine(
                typeof(object).AssemblyQualifiedName
            );

            MessageBox.Show("click to close");

        }
    }


}
