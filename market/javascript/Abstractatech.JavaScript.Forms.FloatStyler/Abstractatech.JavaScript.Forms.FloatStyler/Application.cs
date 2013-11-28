using Abstractatech.JavaScript.Forms.FloatStyler.Design;
using Abstractatech.JavaScript.Forms.FloatStyler.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;



namespace Abstractatech.JavaScript.Forms.FloatStyler
{


    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            content.AttachControlTo(page.Content);
            //content.AutoSizeControlTo(page.ContentSize);

            FormStyler.AtFormCreated = FormStylerLikeFloat.LikeFloat;

            new Form { Text = "Hello World" }.Show();

        }

    }
}
