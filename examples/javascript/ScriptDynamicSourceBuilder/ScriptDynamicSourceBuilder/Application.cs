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
using ScriptDynamicSourceBuilder;
using ScriptDynamicSourceBuilder.Design;
using ScriptDynamicSourceBuilder.HTML.Pages;

namespace ScriptDynamicSourceBuilder
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

            var s = new StringBuilder();

            s.AppendLine("alert('hi');");


            //script: error JSC1000: You tried to instance a class which seems to be marked as native.
            //script: error JSC1000: type has no callable constructor: [ScriptCoreLib.JavaScript.DOM.Blob] Void .ctor(System.String[])

            var blob = new Blob(new[] { s.ToString() });
            //var url = URL.createObjectURL(blob);

            //<script src="blob:http%3A//192.168.43.252%3A28384/6a37d088-4403-4eb2-b45e-1d19083b304e"></script>

            new IHTMLScript { src = blob.ToObjectURL() }.AttachToDocument();

        }

    }
}
