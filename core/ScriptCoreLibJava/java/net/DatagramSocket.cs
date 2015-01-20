using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace java.net
{
    // X:\jsc.svn\examples\java\JVMCLRBroadcastLogger\JVMCLRBroadcastLogger\Library\__AndroidMulticast.cs

    [Script(IsNative = true)]
    public class DatagramSocket
    {
        public DatagramSocket() { throw null; }
        public DatagramSocket(int value) { throw null; }
        public DatagramSocket(SocketAddress value) { throw null; }
        public DatagramSocket(int arg0, InetAddress arg1) { throw null; }

        public virtual void bind(SocketAddress value) { throw null; }
        public virtual void close() { throw null; }
        public virtual void connect(SocketAddress value) { throw null; }
        public virtual void connect(InetAddress arg0, int arg1) { throw null; }
        public virtual void disconnect() { throw null; }
        public virtual bool getBroadcast() { throw null; }
        //public virtual DatagramChannel getChannel(){ throw null;}
        public virtual InetAddress getInetAddress() { throw null; }
        public virtual InetAddress getLocalAddress() { throw null; }
        public virtual int getLocalPort() { throw null; }
        public virtual SocketAddress getLocalSocketAddress() { throw null; }
        public virtual int getPort() { throw null; }
        public virtual int getReceiveBufferSize() { throw null; }
        public virtual SocketAddress getRemoteSocketAddress() { throw null; }
        public virtual bool getReuseAddress() { throw null; }
        public virtual int getSendBufferSize() { throw null; }
        public virtual int getSoTimeout() { throw null; }
        public virtual int getTrafficClass() { throw null; }
        public virtual bool isBound() { throw null; }
        public virtual bool isClosed() { throw null; }
        public virtual bool isConnected() { throw null; }
        public virtual void receive(DatagramPacket value) { throw null; }
        public virtual void send(DatagramPacket value) { throw null; }
        public virtual void setBroadcast(bool value) { throw null; }
        //public static void setDatagramSocketImplFactory(DatagramSocketImplFactory value){ throw null;}
        public virtual void setReceiveBufferSize(int value) { throw null; }
        public virtual void setReuseAddress(bool value) { throw null; }
        public virtual void setSendBufferSize(int value) { throw null; }
        public virtual void setSoTimeout(int value) { throw null; }
        public virtual void setTrafficClass(int value) { throw null; }
    }
}
