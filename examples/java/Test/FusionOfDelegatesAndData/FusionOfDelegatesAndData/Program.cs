using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml.Linq;

namespace FusionOfDelegatesAndData
{
    class Program
    {
        static XElement xml
        {
            get
            {
                return new XElement("xml");
            }
        }


        static IProtocol protocol
        {
            get
            {
                return new Protocol
                {
                    DynamicGetData = x =>
                    {
                        // by modifing the in param the JVMNET rewriter should detect that and use it as out param too..
                        x.Add(new XElement("jvm"));

                        return new ProtocolData { xml = x };
                    }
                };
            }
        }

        // the following scenario is not yet supported..
        //xml = null;
        //protocol = null;

        public static void Main(string[] args)
        {


            ExtensionsToSwitchToCLRContext.Method1(

                xml: xml,
                protocol: protocol
            );

            ExtensionsToSwitchToCLRContext.Method1(
                Data: "hello",
                xml: xml,
                protocol: protocol
            );

            ExtensionsToSwitchToCLRContext.Method1(
                Delegate: delegate { },
                xml: xml,
                protocol: protocol

            );

            ExtensionsToSwitchToCLRContext.Method1(
               Data: "hello",
               Delegate: delegate { },
                xml: xml,
                protocol: protocol


            );
        }
    }

    enum ProtocolEnum
    {
        Value0,
        value1
    }

    class ProtocolData
    {
        public XElement xml;

        // cannot be null
        public byte[] bytes = new byte[0];

        public int port = 32;

        public ProtocolEnum enumvalue = ProtocolEnum.value1;
    }

    class Protocol : IProtocol
    {

        public delegate ProtocolData GetDataDelegate(XElement e);

        public GetDataDelegate DynamicGetData;

        public ProtocolData GetData(XElement e)
        {
            return DynamicGetData(e);
        }
    }

    interface IProtocol
    {
        ProtocolData GetData(XElement e);
    }

    [SwitchToCLRContext]
    static class ExtensionsToSwitchToCLRContext
    {
        public static void Method1(string Data = null, Action Delegate = null, XElement xml = null, IProtocol protocol = null)
        {
            if (xml != null)
                xml.Add(new XElement("clr"));

            Console.WriteLine(new { Data, Delegate, xml, protocol });

            if (protocol != null)
            {
                var n = protocol.GetData(xml);

                Console.WriteLine(new { n.xml, n.bytes, n.port, n.enumvalue });
            }


        }
    }
}
