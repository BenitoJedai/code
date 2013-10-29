using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CSSGoldenCursor;
using CSSGoldenCursor.Design;
using CSSGoldenCursor.HTML.Pages;
using CSSGoldenCursor.HTML.Images.FromAssets;

namespace CSSGoldenCursor
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
            // X:\jsc.svn\examples\javascript\HotPolygon\HotPolygon\Library\HotPolygon.cs

            //IStyleSheet.all["*"].style.setProperty(
            //Native.document.body.style.setProperty(

            IStyleSheet.all["*"].style.setProperty(
                //"cursor", "url('" + new _3dgarro().src + "'), auto", ""
                "cursor", "url('" + new cursor().src + "'), auto", ""
            );

        }

    }
}
