
namespace ScriptCoreLib.JavaScript.Net
{

    using ScriptCoreLib.JavaScript;

    using ScriptCoreLib.JavaScript.Runtime;
    using ScriptCoreLib.JavaScript.DOM;
    using ScriptCoreLib.JavaScript.Net;
    using ScriptCoreLib.JavaScript.DOM.HTML;
    using ScriptCoreLib.JavaScript.DOM.XML;


    using ScriptCoreLib;
    using ScriptCoreLib.Shared;
    using ScriptCoreLib.Shared.Drawing;
    using System;

    [Script]
    public class WebServiceProxy
    {
        public readonly string URL;

        public WebServiceProxy(string url)
        {
            this.URL = url;
        }

        static Expando DeserializeAsExpando(INode e)
        {
            Expando x = new Expando();

            foreach (IXMLElement v in e.childNodes)
            {

                x.SetMember(v.nodeName, DeserializeAsValue(v));
            }

            return x;
        }

        static Expando DeserializeAsValue(INode e)
        {
            if (e.childNodes.Length == 1)
                if (e.childNodes[0].nodeType == INode.NodeTypeEnum.TextNode)
                    return Expando.Of(e.childNodes[0].nodeValue);

            return DeserializeAsExpando(e);
        }

        


        public Action<Action<T>> CreateComplexProxy<T>(string method)
        {
            return CreateComplexProxy<T>(this.URL, method);
        }

        public static Action<Action<T>> CreateComplexProxy<T>(string URL, string method)
        {
            return delegate(Action<T> done)
            {
                new IXMLHttpRequest(URL + "/" + method, "",
                     delegate(IXMLHttpRequest r)
                     {
                         done(DeserializeAsValue(r.responseXML.documentElement).To<T>());
                     }
                 );
            };
        }
    }
}
