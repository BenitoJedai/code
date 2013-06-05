using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System.Xml.Linq;
using java.io;
using java.net;
using java.util.zip;
using System.Collections;
using System.IO;
using System.Threading;
using System.Text;

namespace ConsoleMulticastExperiment
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            System.Console.WriteLine(": " + typeof(object).FullName);

            /// http://www.daniweb.com/software-development/java/threads/424998/udp-client-server-in-java

            new Thread(
                delegate()
                {
                    try
                    {
                        DatagramSocket socket = new DatagramSocket(); //construct a datagram socket and binds it to the available port and the localhos
                        byte[] b = Encoding.UTF8.GetBytes("hi from jvm!");    //creates a variable b of type byte
                        DatagramPacket dgram;
                        dgram = new DatagramPacket((sbyte[])(object)b, b.Length, InetAddress.getByName("239.1.2.3"), 40404);//sends the packet details, length of the packet,destination address and the port number as parameters to the DatagramPacket  
                        //dgram.setData(b);
                        System.Console.WriteLine(
                            "Sending " + b.Length + " bytes to " + dgram.getAddress() + ":" + dgram.getPort());//standard error output stream
                        while (true)
                        {
                            System.Console.WriteLine(".");
                            socket.send(dgram); //send the datagram packet from this port
                            Thread.Sleep(1000); //cause the current executed thread to sleep for a certain number of miliseconds
                        }
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


            new Thread(
                delegate()
                {
                    try
                    {
                        byte[] b = new byte[011];
                        DatagramPacket dgram = new DatagramPacket((sbyte[])(object)b, b.Length);
                        MulticastSocket socket = new MulticastSocket(40404); // must bind receive side
                        socket.joinGroup(InetAddress.getByName("239.1.2.3"));
                        while (true)
                        {
                            socket.receive(dgram); // blocks until a datagram is received
                            System.Console.WriteLine("Received "
                                + Encoding.UTF8.GetString((byte[])(object)dgram.getData())
                                +
                             " bytes from " + dgram.getAddress());
                            dgram.setLength(b.Length); // must reset length field!s
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


            CLRProgram.XML = new XElement("hello", "world");
            CLRProgram.CLRMain(
            );


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
        public static void CLRMain(

            )
        {
            System.Console.WriteLine(XML);
            System.Console.ReadKey();

        }
    }


}
