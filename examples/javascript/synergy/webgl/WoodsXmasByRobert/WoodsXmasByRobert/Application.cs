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
using WoodsXmasByRobert.Design;
using WoodsXmasByRobert.HTML.Pages;

namespace WoodsXmasByRobert
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
            //<!-- Snow flakes -->

            new IHTMLScript { type = "x-shader/x-vertex", id = "vertexshader", innerText = new Shaders.particlesVertexShader().ToString() }.AttachToDocument();
            new IHTMLScript { type = "x-shader/x-fragment", id = "fragmentshader", innerText = new Shaders.particlesFragmentShader().ToString() }.AttachToDocument();


            new AppCode().Content.AttachToDocument();



        }

    }
}
