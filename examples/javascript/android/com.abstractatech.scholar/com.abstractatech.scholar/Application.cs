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
using com.abstractatech.scholar.Design;
using com.abstractatech.scholar.HTML.Pages;

namespace com.abstractatech.scholar
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


            var c = new IHTMLDiv();

            c.AttachToDocument();
            c.style.SetSize(400, 400);

            var layout = new Abstractatech.JavaScript.FileStorage.HTML.Pages.App();

            layout.Container.AttachTo(c);



            new Abstractatech.JavaScript.FileStorage.ApplicationContent(
                layout,
                service
            );

            new IHTMLIFrame { name = "view" }.AttachToDocument();



        }

    }
}
