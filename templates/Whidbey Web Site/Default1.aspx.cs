using ScriptCoreLib;

[assembly: Script, ScriptTypeFilter(ScriptType.JavaScript, "JavaScript")]

namespace JavaScript
{


    using ScriptCoreLib.JavaScript;

    using ScriptCoreLib.JavaScript.Runtime;
    using ScriptCoreLib.JavaScript.DOM;
    using ScriptCoreLib.JavaScript.DOM.HTML;
    using ScriptCoreLib.JavaScript.DOM.XML;


    using ScriptCoreLib;
    using ScriptCoreLib.Shared;
    using ScriptCoreLib.Shared.Drawing;

    [Script]
    public sealed class ProxyWebService
    {
        public readonly string URL;

        public ProxyWebService(string URL)
        {
            this.URL = URL;

            this.HelloWorld = CreateComplexProxy<JavaScript.U>(URL, "HelloWorld");
            this.HelloWorld2 = CreateComplexProxy<JavaScript.UX>(URL, "HelloWorld2");
        }

        static Expando DeserializeAsExpando(INode e)
        {
            Expando x = new Expando();

            foreach (IXMLElement v in e.childNodes)
            {
                if (v.nodeType != INode.NodeTypeEnum.ElementNode)
                    throw new ScriptCoreLib.JavaScript.System.ScriptException("ElementNode expected");

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

        static Action<Action<T>> CreateComplexProxy<T>(string URL, string method)
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

        public readonly Action<Action<JavaScript.U>> HelloWorld;
        public readonly Action<Action<JavaScript.UX>> HelloWorld2;
    }

    /// <summary>
    /// Summary description for Class2
    /// </summary>
    [Script]
    public class Class2
    {
        static Class2()
        {

            Native.Window.onload +=
                delegate
                {
                    IHTMLButton btn = new IHTMLButton("asp.net c# : hello world 6");

                    Native.Document.body.appendChild(btn);

                    btn.style.color = Color.Red;

                    ProxyWebService p = new ProxyWebService("WebService.asmx");

                    btn.onclick += delegate
                    {
                        btn.disabled = true;

                        p.HelloWorld2(
                             delegate(UX r)
                             {
                                 btn.disabled = false;

                                 Native.Window.alert(r.A + " - " + r.B.A);
                             }
                         );
                    };

                    Console.Log("asp net hello world : c#");
                };
        }
    }

}

namespace MyServer
{
    using System.Data;
    using System.Diagnostics;
    using System.Configuration;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using System.Query;
    using System.Reflection;

    using jsc.server;

    public partial class Default1 : Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            WebTools.CompileAndRegisterClientScript(this);
        }

        protected void AnimatedLabel1_Load(object sender, System.EventArgs e)
        {

        }
    }

}