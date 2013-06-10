using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WithClickOnceLANLauncherShared
{
    [DefaultEvent("AtData")]
    public partial class MulticastListenerComponent : Component
    {
        public MulticastListenerComponent()
        {
            InitializeComponent();




        }

        Action<string> InternalAtData;
        public MulticastListener InternalListener;

        public event Action<string> AtData
        {
            add
            {
                InternalInitialize();

                InternalAtData += value;
            }
            remove { }
        }



        private void InternalInitialize()
        {
            // http://imar.spaanjaars.com/278/how-do-i-detect-design-time-vs-run-time-in-a-net-control
            // http://stackoverflow.com/questions/34664/designmode-with-controls
            if (this.DesignMode)
                return;

            if (InternalListener == null)
            {
                InternalListener = new MulticastListener(MulticastSettings.testSettings);

                //using (
                Console.WriteLine("MulticastListener! " + new { System.Diagnostics.Process.GetCurrentProcess().Id });

                //)
                {

                    InternalListener.StartListening(
                        bytes =>
                        {
                            var listen = Encoding.UTF8.GetString(bytes);

                            if (InternalAtData != null)
                                InternalAtData(listen);

                            //Console.WriteLine(new { listen });


                            //button1.Invoke(
                            //    new Action(
                            //        delegate
                            //        {
                            //            button1.Text = listen;
                            //        }
                            //    )
                            //);
                        }
                    );


                    // await next click?
                }
            }
        }
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
                //                at WithClickOnceLANLauncherShared.FindServiceProviderOverMulticast.multicastListener1_AtData(String listen)
                //at WithClickOnceLANLauncherShared.MulticastListenerComponent.<InternalInitialize>b__0(Byte[] bytes) in x:\jsc.svn\examples\javascript\android\WithClickOnceLANLauncher\WithClickOnceLANLauncherShared\MulticastListener.cs:line 61
                //at WithClickOnceLANLauncherShared.MulticastListener.ReceiveCallback(IAsyncResult ar) in x:\jsc.svn\examples\javascript\android\WithClickOnceLANLauncher\WithClickOnceLANLauncherShared\MulticastListener.cs:line 189

                if (Debugger.IsAttached)
                    Debugger.Break();
            }
        }

        public void Dispose()
        {
            if (IsBound) UnbindAndLeave();
        }
    }

}
