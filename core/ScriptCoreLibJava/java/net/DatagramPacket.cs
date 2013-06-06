using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace java.net
{
    [Script(IsNative = true)]
    public sealed class DatagramPacket
    {
        protected DatagramPacket() { throw null; }
        public DatagramPacket(sbyte[] arg0, int arg1) { throw null; }
        public DatagramPacket(sbyte[] arg0, int arg1, int arg2) { throw null; }
        public DatagramPacket(sbyte[] arg0, int arg1, SocketAddress arg2) { throw null; }
        public DatagramPacket(sbyte[] arg0, int arg1, InetAddress arg2, int arg3) { throw null; }
        public DatagramPacket(sbyte[] arg0, int arg1, int arg2, SocketAddress arg3) { throw null; }
        public DatagramPacket(sbyte[] arg0, int arg1, int arg2, InetAddress arg3, int arg4) { throw null; }

        public InetAddress getAddress() { throw null; }
        public sbyte[] getData() { throw null; }
        public int getLength() { throw null; }
        public int getOffset() { throw null; }
        public int getPort() { throw null; }
        public SocketAddress getSocketAddress() { throw null; }
        public void setAddress(InetAddress value) { throw null; }
        public void setData(sbyte[] value) { throw null; }
        public void setData(sbyte[] arg0, int arg1, int arg2) { throw null; }
        public void setLength(int value) { throw null; }
        public void setPort(int value) { throw null; }
        public void setSocketAddress(SocketAddress value) { throw null; }
    }
}
