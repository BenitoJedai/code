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
using VirtualElementEvents;
using VirtualElementEvents.Design;
using VirtualElementEvents.HTML.Pages;
using ScriptCoreLib.JavaScript.Native;

namespace VirtualElementEvents
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // X:\jsc.svn\examples\javascript\WebGL\HeatZeekerRTS\HeatZeekerRTS\Application.cs

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //page[]

            document[IHTMLElement.HTMLElementEnum.button].css.hover.style.color = "blue";

            document[IHTMLElement.HTMLElementEnum.button].onclick +=
                e =>
                {
                    var button = (IHTMLButton)e.Element;

                    new IHTMLButton { "onclick " + new { button.innerText } }.AttachToDocument();
                };
        }

    }
}
