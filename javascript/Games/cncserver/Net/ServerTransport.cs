using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Query;
using System.Xml.XLinq;
using System.Data.DLinq;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace cncserver
{
    using ScriptCoreLib.Shared;
    using cnc.source.shared;


   
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
        }
        

       


    }

}
