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
    using ScriptCoreLib.Shared.Query;
    using ScriptCoreLib.Shared.Drawing;

    namespace __ClientScriptReflector
    {
        interface AssemblyReferenceToken :
            ScriptCoreLib.Shared.Query.IAssemblyReferenceToken
        {
        }
    }

    [Script]
    class __Type1
    {
        public string src;

        public override string ToString()
        {
            return src;
        }
    }

    [Script]
    class __Type2
    {
        public int status;
        public string headers;

        public override string ToString()
        {
            return status + " " + headers;
        }
    }

    [Script]
    public class ClientScriptReflector
    {
        static ClientScriptReflector()
        {
            Native.Window.onload +=
                delegate
                {
                    foreach (var v in Native.Document.getElementsByClassName("ClientScriptReflector").ToArray())
                    {
                        var target = v;

                        var btn = new IHTMLButton("Find scripts in this page");
                        var btn_clear = new IHTMLButton("Clear the list");
                        //var btn_break = new IHTMLButton("Debug break and show the current time");

                        //btn_break.onclick +=
                        //    delegate
                        //    {
                        //        //var demo = new { now = "" + IDate.Now, text = "" };

                        //        //System.Diagnostics.Debugger.Break();

                        //        //demo.text = "we should see this in the debugger.";

                        //        //Native.Window.alert(demo.ToString());
                        //    };

                        target.appendChild(btn, btn_clear/*, btn_break*/);

                        var ol = new IHTMLElement(IHTMLElement.HTMLElementEnum.ol);

                   

                        target.appendChild(ol);

                        btn_clear.onclick += delegate {
                            ol.removeChildren();

                            ol.appendChild(
                                new IHTMLElement(IHTMLElement.HTMLElementEnum.li, "Nothing to display - click the button above")
                            );
                        };

                        ol.appendChild(
                            new IHTMLElement(IHTMLElement.HTMLElementEnum.li, "Nothing to display - click the button above")
                        );

                        btn.onclick +=
                            delegate
                            {
                                ol.removeChildren();
                               
                                foreach (var vx in from i in Native.Document.getElementsByTagName("script")
                                                   where !string.IsNullOrEmpty(((IHTMLScript)i).src)
                                                   select new __Type1 { src = ((IHTMLScript)i).src })
                                {
                                    var script_size = new IHTMLElement(IHTMLElement.HTMLElementEnum.pre, "reading the file size");

                                    ol.appendChild(
                                          new IHTMLElement(IHTMLElement.HTMLElementEnum.li,
                                              new ITextNode(vx.ToString()),
                                              new IHTMLBreak(),
                                              new IHTMLAnchor(vx.src, "view source") { target = "_self" },
                                              new IHTMLBreak(),
                                              script_size
                                          )
                                      );

                                    new IXMLHttpRequest(HTTPMethodEnum.HEAD, vx.src,
                                        (r) =>
                                            {
                                                var data = new __Type2 { status = (int)r.status, headers = r.getAllResponseHeaders() };


                                                script_size.innerText = data.ToString();
                                            }
                                    );

                                };

                            };
                    }


                };
        }
    }
}

public partial class Controls_ClientScriptReflector : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
