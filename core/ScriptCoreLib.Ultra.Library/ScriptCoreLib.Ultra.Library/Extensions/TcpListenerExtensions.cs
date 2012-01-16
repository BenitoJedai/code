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
        public static void BridgeStreamTo(this Stream x, Stream y)
        {
             new Thread(
                delegate()
                {
                    var buffer = new byte[0x10000];

                    while (true)
                    {
                        //
                        try
                        {
                            var c = x.Read(buffer, 0, buffer.Length);

                            if (c < 0)
                                return;

                            if (c == 0)
                            {
                                Thread.Sleep(10);
                            }
                            else
                            {
                                Console.WriteLine(c + " bytes ...");

                                y.Write(buffer, 0, c);

                                Thread.Sleep(1);
                            }
                        }
                        catch
                        {
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

        public static void BridgeConnectionTo(this TcpClient x, TcpClient y)
        {
            x.GetStream().BridgeStreamTo(y.GetStream());
            y.GetStream().BridgeStreamTo(x.GetStream());
        }

        public static void BridgeConnectionToPort(this TcpListener x, int port)
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

                        var y = new TcpClient();

                        y.Connect(new System.Net.IPEndPoint(IPAddress.Loopback, port));

                        c.BridgeConnectionTo(y);
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
