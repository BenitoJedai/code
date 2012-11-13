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
using CSSTransform3DFPSExperimentByKeith.Design;
using CSSTransform3DFPSExperimentByKeith.HTML.Pages;

namespace CSSTransform3DFPSExperimentByKeith
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
            // http://www.keithclark.co.uk/labs/3dcss/demo/

            var prefetch = new HTML.Pages.TexturesImages();

            new Design.Library.threedee().Content.AttachToHead().onload +=
                delegate
                {

                };
        }

    }
}
