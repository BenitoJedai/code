using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace cncserver
{
    using ScriptCoreLib.Shared;
    using gameclient.source.shared;


   
    public class ServerTransport<T>
    {
        public readonly MyTransportDescriptor<T> Descriptor = new MyTransportDescriptor<T>();

        public ServerTransport(Stream s)
        {
            JSONSerializer.Deserialize(Descriptor, s);
        }


        public T Data
        {
            get
            {
                return Descriptor.Data;
            }
            set
            {
                Descriptor.Data = value;
            }
        }



        public void WriteTo(Stream stream)
        {
            var s = new MemoryStream();

            foreach (char v in JSONBase.Protocol)
            {
                s.WriteByte((byte)v);
            }

            JSONSerializer.Serialize(Descriptor, s);

            s.WriteTo(stream);
        }
    }

}
