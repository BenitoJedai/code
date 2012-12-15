using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using EventSourceForWebServiceYield.Design;
using EventSourceForWebServiceYield.HTML.Pages;

namespace EventSourceForWebServiceYield
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            page.Invoke.onclick +=
                delegate
                {
                    var now = DateTime.Now;

                    var n = new XElement("client",
                        new XAttribute("value", "" + now)
                    );

                    service.Invoke(
                        n,
                        xml =>
                        {
                            new IHTMLPre { innerText = xml.ToString() }.AttachTo(page.right);
                        }
                    );
                };

            page.InvokeSpecal.onclick +=
                delegate
                {
                    var now = DateTime.Now;

                    var n = new XElement("client",
                        new XAttribute("value", "" + now)
                    );

                    service.InvokeSpecial(
                        n,
                        xml =>
                        {
                            new IHTMLPre { innerText = xml.ToString() }.AttachTo(page.left);
                        }
                    );
                };

            page.Clear.onclick +=
                delegate
                {

                    page.left.Clear();
                    page.right.Clear();
                };
        }

    }
}
