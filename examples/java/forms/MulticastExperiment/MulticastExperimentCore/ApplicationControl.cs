using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MulticastExperimentCore
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        MulticastSettings testSettings = new MulticastSettings()
        {
            Address = IPAddress.Parse("239.1.2.3"),
            Port = 40404,
            TimeToLive = 30
        };


        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Listen");


            button1.Enabled = false;


            //using (
            var receiver = new MulticastListener(testSettings);

            //)
            {

                receiver.StartListening(
                    bytes =>
                    {
                        var listen = Encoding.UTF8.GetString(bytes);


                        Console.WriteLine(new { listen });


                        button1.Invoke(
                            new Action(
                                delegate
                                {
                                    button1.Text = listen;
                                }
                            )
                        );
                    }
                );


                // await next click?
            }
        }

        int c;

        private void button2_Click(object sender, System.EventArgs e)
        {
            Console.WriteLine("Send");

            // Act
            using (var broadcaster = new MulticastBroadcaster(testSettings))
            {
                c++;

                //var n = c + " hello world";
                var n =
                    new XElement("y",
                        new XAttribute("c", "" + c),
                        "hello world"
                    ).ToString();

                //c + " hello world";

                broadcaster.Broadcast(Encoding.UTF8.GetBytes(n));

            }

        }

        // https://code.google.com/p/multicastdotnet/source/browse/#svn%2Ftrunk%2FMulticast%2FMulticast

        public class MulticastSettings
        {
            public IPAddress Address { get; set; }
            public int Port { get; set; }
            public int TimeToLive { get; set; }
        }

        public class MulticastListener : IDisposable
        {
            public event Action<byte[]> OnReceive;

            public MulticastSettings Settings { get; protected set; }

            public bool IsBound
            {
                get
                {
                    return UdpClient.Client != null
                            ? UdpClient.Client.IsBound
                            : false;
                }
            }

            private UdpClient udpClient;
            public UdpClient UdpClient
            {
                get { return udpClient ?? (udpClient = new UdpClient()); }
            }

            public IPEndPoint LocalIPEndPoint { get; protected set; }

            public MulticastListener(MulticastSettings settings)
                : this(settings, true)
            { }

            public MulticastListener(MulticastSettings settings, bool autoBindJoinConnect)
            {
                if (settings == null) throw new ArgumentNullException("settings");

                Settings = settings;

                if (autoBindJoinConnect) BindAndJoin();
            }

            private void BindAndJoin()
            {
                try
                {
                    LocalIPEndPoint = new IPEndPoint(IPAddress.Any, Settings.Port);
                    UdpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                    UdpClient.ExclusiveAddressUse = false;
                    UdpClient.EnableBroadcast = true;

                    UdpClient.Client.Bind(LocalIPEndPoint);
                    UdpClient.JoinMulticastGroup(Settings.Address, Settings.TimeToLive);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public void StartListening(Action<byte[]> handler)
            {
                try
                {
                    if (!IsBound) BindAndJoin();

                    OnReceive += handler;
                    AsyncCallback receiveCallback = new AsyncCallback(ReceiveCallback);
                    UdpClient.BeginReceive(receiveCallback, this);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public void StopListening()
            {
                OnReceive = null;
                if (IsBound) UnbindAndLeave();
            }

            private void UnbindAndLeave()
            {
                try
                {
                    UdpClient.DropMulticastGroup(Settings.Address);
                    UdpClient.Close();
                }
                catch (ObjectDisposedException)
                {
                    // expected exception fired when we close - swallow it up
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            private void ReceiveCallback(IAsyncResult ar)
            {
                try
                {
                    MulticastListener receiver = (MulticastListener)(ar.AsyncState);

                    UdpClient udpClient = receiver.UdpClient;
                    IPEndPoint ipEndPoint = receiver.LocalIPEndPoint;

                    byte[] receiveBytes = udpClient.EndReceive(ar, ref ipEndPoint);
                    OnReceive(receiveBytes);

                    AsyncCallback receiveCallback = new AsyncCallback(ReceiveCallback);
                    UdpClient.BeginReceive(receiveCallback, this);
                }
                catch (ObjectDisposedException)
                {
                    // expected exception fired when we close - swallow it up
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public void Dispose()
            {
                if (IsBound) UnbindAndLeave();
            }
        }

        public class MulticastBroadcaster : IDisposable
        {
            public MulticastSettings Settings { get; protected set; }

            public bool IsBound
            {
                get
                {
                    return UdpClient.Client != null
                            ? UdpClient.Client.IsBound
                            : false;
                }
            }

            public IPEndPoint LocalIPEndPoint { get; protected set; }
            public IPEndPoint RemoteIPEndPoint { get; protected set; }

            private UdpClient udpClient;
            public UdpClient UdpClient
            {
                get { return udpClient ?? (udpClient = new UdpClient()); }
            }

            public MulticastBroadcaster(MulticastSettings settings)
                : this(settings, true)
            { }

            public MulticastBroadcaster(MulticastSettings settings, bool autoBindJoinConnect)
            {
                if (settings == null) throw new ArgumentNullException("settings");

                Settings = settings;


                if (autoBindJoinConnect) BindJoinConnect();
            }

            private void BindJoinConnect()
            {
                try
                {
                    LocalIPEndPoint = new IPEndPoint(IPAddress.Any, Settings.Port);
                    RemoteIPEndPoint = new IPEndPoint(Settings.Address, Settings.Port);

                    UdpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                    UdpClient.ExclusiveAddressUse = false;
                    UdpClient.EnableBroadcast = true;

                    UdpClient.Client.Bind(LocalIPEndPoint);
                    UdpClient.JoinMulticastGroup(Settings.Address, Settings.TimeToLive);
                    UdpClient.Connect(RemoteIPEndPoint);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            private void UnbindLeaveDisconnect()
            {
                try
                {
                    UdpClient.DropMulticastGroup(Settings.Address);
                    UdpClient.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public void Broadcast(byte[] data)
            {
                if (!IsBound) BindJoinConnect();

                try
                {
                    AsyncCallback broadcastCallback = new AsyncCallback(BroadcastCallback);
                    UdpClient.BeginSend(data, data.Length, broadcastCallback, this);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            private void BroadcastCallback(IAsyncResult ar)
            {
                try
                {
                    MulticastBroadcaster broadcaster = (MulticastBroadcaster)(ar.AsyncState);

                    UdpClient udpClient = broadcaster.UdpClient;
                    int bytesSent = udpClient.EndSend(ar);
                }
                catch (ObjectDisposedException)
                {
                    // expected exception fired when the socket is closed - swallow it up
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public void Dispose()
            {
                UnbindLeaveDisconnect();
            }
        }


    }
}
