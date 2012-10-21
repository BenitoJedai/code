using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.IO;

namespace ScriptCoreLib.Extensions
{
    public static class TcpListenerExtensions
    {
        static void BridgeStreamTo(this NetworkStream x, NetworkStream y, int ClientCounter, string prefix = "#")
        {
            new Thread(
               delegate()
               {
                   var buffer = new byte[0x100000];

                   while (true)
                   {
                       //
                       try
                       {

                           var c = x.Read(buffer, 0, buffer.Length);

                           if (c <= 0)
                               return;


                           Console.WriteLine(prefix + ClientCounter.ToString("x4") + " 0x" + c.ToString("x4") + " bytes");

                           if (prefix.StartsWith("?"))
                               Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, c));

                           y.Write(buffer, 0, c);

                           Thread.Sleep(1);
                       }
                       catch
                       {
                           //Console.WriteLine("#" + ClientCounter + " error");

                           return;
                       }
                   }
               }
           )
           {
               Name = "BridgeStreamTo",
               IsBackground = true,
               Priority = ThreadPriority.Lowest
           }.Start();
        }

        static void BridgeConnectionTo(this TcpClient x, TcpClient y, int ClientCounter, string rx, string tx)
        {
            x.GetStream().BridgeStreamTo(y.GetStream(), ClientCounter, rx);
            y.GetStream().BridgeStreamTo(x.GetStream(), ClientCounter, tx);
        }

        public static void BridgeConnectionToPort(this TcpListener x, int port)
        {
            BridgeConnectionToPort(x, port, "> ", "< ");
        }

        public static void BridgeConnectionToPort(this TcpListener x, int port, string rx, string tx)
        {
            x.Start();

            var ClientCounter = 0;

            new Thread(
                delegate()
                {
                    while (true)
                    {
                        var c = x.AcceptTcpClient();
                        ClientCounter++;

                        //Console.WriteLine("#" + ClientCounter + " BridgeConnectionToPort");

                        var y = new TcpClient();

                        y.Connect(new System.Net.IPEndPoint(IPAddress.Loopback, port));

                        c.BridgeConnectionTo(y, ClientCounter, rx, tx);
                    }


                }
            )
            {
                IsBackground = true,
                Name = "BridgeConnectionToPort"
            }.Start();
        }
    }
}
