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
using CSSOrientation;
using CSSOrientation.Design;
using CSSOrientation.HTML.Pages;

namespace CSSOrientation
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
            Native.document.body.css.style.transition = "background 300ms linear, margin-left 300ms linear";

            Native.document.body.css.orientation.landscape.style.background = "cyan";

            // make room for sidebar
            Native.document.body.css.orientation.landscape.style.marginLeft = "10em";


            Native.document.body.css.orientation.portrait.style.background = "yellow";
            Native.document.body.css.orientation.portrait.style.marginTop = "10em";

            // does not seem to apply to print option tho??

            var landscapesidebar = new IHTMLDiv
            {

            }.AttachToDocument();

            landscapesidebar.css.style.position = IStyle.PositionEnum.@fixed;
            landscapesidebar.css.style.background = "white";


            landscapesidebar.css.style.transition = "left 300ms linear";

            landscapesidebar.css.style.left = "-10em";

            landscapesidebar.css.orientation.landscape.style.left = "1em";
            landscapesidebar.css.orientation.landscape.style.width = "8em";
            landscapesidebar.css.orientation.landscape.style.top = "1em";
            landscapesidebar.css.orientation.landscape.style.bottom = "1em";


            var portraitsidebar = new IHTMLDiv
            {

            }.AttachToDocument();

            portraitsidebar.css.style.position = IStyle.PositionEnum.@fixed;
            portraitsidebar.css.style.background = "white";


            portraitsidebar.css.style.transition = "top 300ms linear";

            portraitsidebar.css.style.top = "-10em";


            portraitsidebar.css.orientation.portrait.style.left = "1em";
            portraitsidebar.css.orientation.portrait.style.height = "8em";
            portraitsidebar.css.orientation.portrait.style.top = "1em";
            portraitsidebar.css.orientation.portrait.style.right = "1em";

        }

    }
}
