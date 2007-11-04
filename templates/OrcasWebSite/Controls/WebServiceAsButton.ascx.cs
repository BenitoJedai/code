using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ScriptCoreLib;

[assembly: Script, ScriptTypeFilter(ScriptType.JavaScript, "JavaScript")]

namespace JavaScript
{
    using ScriptCoreLib.JavaScript;
    using ScriptCoreLib.JavaScript.Runtime;
    using ScriptCoreLib.JavaScript.Net;
    using ScriptCoreLib.JavaScript.DOM;
    using ScriptCoreLib.JavaScript.DOM.HTML;


    using ScriptCoreLib;
    using ScriptCoreLib.Shared;
    using ScriptCoreLib.Shared.Drawing;


    namespace __WebServiceAsButton
    {
        interface AssemblyReferenceToken :
            ScriptCoreLib.Shared.Query.IAssemblyReferenceToken
        {
        }
    }
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


    [Script]
    public class WebServiceAsButton
    {
        static WebServiceAsButton()
        {
            Native.Window.onload +=
                delegate
                {
                    var p = new MyWebService(new WebServiceProxy("WebService.asmx"));

                    foreach (var v in Native.Document.getElementsByClassName("WebServiceAsButton").ToArray())
                    {
                        var x = (IHTMLButton)v;


                    
                        x.onclick +=
                            delegate
                            {
                                x.disabled = true;

                                p.HelloWorld2(
                                    i =>
                                      {
                                          x.disabled = false;

                                          x.innerText = i.A  + " " + i.B.A;

                                      }
                                  );
                            };

                    }


                };
        }
    }
}

public partial class Controls_WebServiceAsButton : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
