using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NewTabContinuation;
using NewTabContinuation.Design;
using NewTabContinuation.HTML.Pages;

namespace NewTabContinuation
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140412

            // also show how does this relate to Workers async and localstorage, historic api
            // what about service worker?

            var state = "binary state?";
            var state64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(state));

            // at default location?
            if (Native.document.location.hash == "")
            {
                new IHTMLAnchor
                {
                    target = "_blank",
                    href = "#" + state64,
                    innerText = "click to continue"
                }.AttachToDocument();
            }
            else
            {
                var newstate = Encoding.UTF8.GetString(
                    Convert.FromBase64String(Native.document.location.hash.SkipUntilOrEmpty("#"))
                );

                new IHTMLPre { new { newstate } }.AttachToDocument();


            }
        }

    }
}
