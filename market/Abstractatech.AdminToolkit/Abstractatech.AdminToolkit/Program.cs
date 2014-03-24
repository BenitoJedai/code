using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Abstractatech.AdminToolkit.Data;
using System.Data.SQLite;
using ScriptCoreLib.Extensions;

namespace Abstractatech.AdminToolkit
{
    class Program
    {
        public static int port = 13389;
        public static string ip = "nuclear.monese.com";

        public void fake()
        {
            Type sqlLitec = typeof(SQLiteConnection);
            Type ext = typeof(System.Data.SQLite.SQLiteConnectionStringBuilderExtensions);
        }

        static void Main(string[] args)
        {
            var db = new PortScan.Scan();
            while (true)
            {
                IPAddress ipAddress = Dns.GetHostEntry(ip).AddressList[0];
                var i = new PortScanScanRow { IP = ipAddress.ToString(), Port = port.ToString() };

                //try
                //{
                //    
                //    System.Net.Sockets.Socket sock = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
                //    sock.Connect(ipAddress, port);
                //    if (sock.Connected == true)  // Port is in use and connection is successful
                //        Console.WriteLine("Port is Closed");
                //    sock.Close();
                //}
                //catch (SocketException ex)
                //{
                //    if (ex.ErrorCode == 10061)  // Port is unused and could not establish connection 
                //        Console.WriteLine("Port is Open!");
                //    else
                //        Console.WriteLine(ex.Message);
                //}
                Console.WriteLine(ipAddress);
                TcpClient tcpClient = new TcpClient();
                try
                {
                    var res = tcpClient.BeginConnect(ip,port,null,null);
                    var success = res.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));


                    if (success)
                    {
                        i.IsOpen = true;
                        Console.WriteLine("Port " + port + " Open");
                    }
                    else
                    {

                        i.IsOpen = false;

                        Console.WriteLine("Port " + port + " Closed");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Port " + port + " Closed");
                }
                db.Insert(i);
                Thread.Sleep(2000);
                
            }
            //Console.ReadLine();

        }
    }
}
