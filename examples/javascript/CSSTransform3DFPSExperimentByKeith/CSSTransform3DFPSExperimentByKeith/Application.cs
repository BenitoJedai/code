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
using System.Linq;
using System.Text;
using System.Xml.Linq;
using CSSTransform3DFPSExperimentByKeith.Design;
using CSSTransform3DFPSExperimentByKeith.HTML.Pages;
using System.Drawing;

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
                    InitializeContent();

                };
        }

        [Script(ExternalTarget = "window.__osxPlane.node")]
        public static IHTMLDiv __osxPlane_node;

        private
            // dynamic does not work in static yet?
            //static 
            void InitializeContent()
        {
            //dynamic window = Native.Window;

            //dynamic __osxPlane = window.__osxPlane;
            //IHTMLDiv __osxPlane_node = __osxPlane.node;


            var c = new Controls.UserControl1();

            c.BackColor = Color.Transparent;

            var x = c.GetHTMLTargetContainer();

            x.style.transform = "scale(0.5)";
            x.style.transformOrigin = "0% 0%";

            x.style.SetSize(
                __osxPlane_node.clientWidth * 2,
                __osxPlane_node.clientHeight * 2
            );

            c.AttachControlTo(__osxPlane_node);

        }

    }
}
