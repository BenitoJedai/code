using InteractiveMatrixTransformD.Design;
using InteractiveMatrixTransformD.HTML.Pages;
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

namespace InteractiveMatrixTransformD
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationCanvas content = new ApplicationCanvas();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            content.AttachToContainer(Native.Document.body);


            content.Width = Native.Window.Width;
            content.Height = Native.Window.Height;

            Native.Window.onresize +=
                delegate
                {
                    content.Width = Native.Window.Width;
                    content.Height = Native.Window.Height;
                };
        }

    }
}
