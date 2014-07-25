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
using TestShadowBody;
using TestShadowBody.Design;
using TestShadowBody.HTML.Pages;

namespace TestShadowBody
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
            // this will override everything being shown
            //page.body.shadow.appendChild("the shadow");


            var s = new ShadowLayout().AttachTo(Native.document.documentElement.shadow);

            // override the whole document.
            //Native.document.documentElement.shadow.appendChild("the shadow");



            //Console.WriteLine(new { Native.document.documentElement.parentNode });
            // :40ms {{ parentNode = [object HTMLDocument] }}
            //Native.document.documentElement.parentNode.shadow.appendChild("the shadow");

                // cool!
            new IHTMLIFrame { src = "http://example.com" }.AttachTo(
                s.TopSideBar
            );

        }

    }
}
