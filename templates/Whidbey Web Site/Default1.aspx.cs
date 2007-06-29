using ScriptCoreLib;

[assembly: Script, ScriptTypeFilter(ScriptType.JavaScript, "JavaScript")]

namespace JavaScript
{


    using ScriptCoreLib.JavaScript;

    using ScriptCoreLib.JavaScript.Runtime;
    using ScriptCoreLib.JavaScript.DOM;
    using ScriptCoreLib.JavaScript.Net;
    using ScriptCoreLib.JavaScript.DOM.HTML;
    using ScriptCoreLib.JavaScript.DOM.XML;


    using ScriptCoreLib;
    using ScriptCoreLib.Shared;
    using ScriptCoreLib.Shared.Query;
    using ScriptCoreLib.Shared.Drawing;

    [Script]
    public sealed class MyWebService
    {
        public MyWebService(WebServiceProxy p)
        {
            // if we knew the return types we could spawn the proxies
            // automatically

            this.HelloWorld = p.CreateComplexProxy<JavaScript.U>("HelloWorld");
            this.HelloWorld2 = p.CreateComplexProxy<JavaScript.UX>("HelloWorld2");
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
                    MyWebService p = new MyWebService(new WebServiceProxy("WebService.asmx"));

                    

                    IHTMLButton btn = new IHTMLButton("asp.net c# : hello world 8");

                    Native.Document.body.appendChild(btn);

                    btn.style.color = Color.Red;

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

                    IHTMLButton b2 = new IHTMLButton("xxx");

                    b2.attachToDocument();

                    b2.onclick += delegate
                    {
                        Native.Document.body.style.backgroundColor = Color.Red;
                    };
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