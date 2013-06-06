using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace java.net
{
    [Script(IsNative = true)]
    public class MulticastSocket : DatagramSocket
    {
        public MulticastSocket() { throw null; }
        public MulticastSocket(int value) { throw null; }
        public MulticastSocket(SocketAddress value) { throw null; }

        public virtual InetAddress getInterface() { throw null; }
        public virtual bool getLoopbackMode() { throw null; }
        public virtual NetworkInterface getNetworkInterface() { throw null; }
        public virtual int getTimeToLive() { throw null; }
        public virtual sbyte getTTL() { throw null; }
        public virtual void joinGroup(InetAddress value) { throw null; }
        public virtual void joinGroup(SocketAddress arg0, NetworkInterface arg1) { throw null; }
        public virtual void leaveGroup(InetAddress value) { throw null; }
        public virtual void leaveGroup(SocketAddress arg0, NetworkInterface arg1) { throw null; }
        public virtual void send(DatagramPacket arg0, sbyte arg1) { throw null; }
        public virtual void setInterface(InetAddress value) { throw null; }
        public virtual void setLoopbackMode(bool value) { throw null; }
        public virtual void setNetworkInterface(NetworkInterface value) { throw null; }
        public virtual void setTimeToLive(int value) { throw null; }
        public virtual void setTTL(sbyte value) { throw null; }
    }
}
