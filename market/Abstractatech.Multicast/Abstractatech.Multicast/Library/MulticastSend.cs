using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Linq;

namespace WithClickOnceLANLauncherShared
{
    public partial class MulticastSendComponent : Component
    {
        public MulticastSendComponent()
        {
            InitializeComponent();
        }

        int c;

        public void Send(string e)
        {
            // Act
            using (var broadcaster = new MulticastBroadcaster(MulticastSettings.testSettings))
            {
                c++;

                //var n = c + " hello world";
                var n =
                    new XElement("string",
                        new XAttribute("c", "" + c),
                        e
                    ).ToString();

                //c + " hello world";

                broadcaster.Broadcast(Encoding.UTF8.GetBytes(n));

            }
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

                // An attempt was made to access a socket in a way forbidden by its access permissions
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
