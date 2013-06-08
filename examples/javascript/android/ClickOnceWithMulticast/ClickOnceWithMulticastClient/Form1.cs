using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClickOnceWithMulticastClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            var uri = (string)e.Data.GetData("Text");

            //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.WebBrowser.set_ScriptErrorsSuppressed(System.Boolean)]

            if (Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            {
                this.listBox1.Items.Add(uri);

                // do we have to spam android to listen?
                for (int i = 0; i < 8; i++)
                {
                    // Act
                    using (var broadcaster = new MulticastBroadcaster(MulticastSettings.testSettings))
                    {

                        broadcaster.Broadcast(Encoding.UTF8.GetBytes(uri));

                    } Thread.Sleep(20);
                }
            }
        }
    }

    // https://code.google.com/p/multicastdotnet/source/browse/#svn%2Ftrunk%2FMulticast%2FMulticast

    public class MulticastSettings
    {
        public static MulticastSettings testSettings = new MulticastSettings()
        {
            Address = IPAddress.Parse("239.1.2.3"),
            Port = 40404,
            TimeToLive = 30
        };


        public IPAddress Address { get; set; }
        public int Port { get; set; }
        public int TimeToLive { get; set; }
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


        public MulticastBroadcaster(MulticastSettings settings, bool autoBindJoinConnect = true)
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
                UdpClient.DontFragment = true;
                UdpClient.Ttl = 30;

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
                //AsyncCallback broadcastCallback = new AsyncCallback(BroadcastCallback);
                //UdpClient.BeginSend(data, data.Length, broadcastCallback, this);

                UdpClient.Send(data, data.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void BroadcastCallback(IAsyncResult ar)
        //{
        //    try
        //    {
        //        MulticastBroadcaster broadcaster = (MulticastBroadcaster)(ar.AsyncState);

        //        UdpClient udpClient = broadcaster.UdpClient;
        //        int bytesSent = udpClient.EndSend(ar);
        //    }
        //    catch (ObjectDisposedException)
        //    {
        //        // expected exception fired when the socket is closed - swallow it up
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void Dispose()
        {
            UnbindLeaveDisconnect();
        }
    }

}
