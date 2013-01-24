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
using WebGLGTAByNiklas.Design;
using WebGLGTAByNiklas.HTML.Pages;

namespace WebGLGTAByNiklas
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



            Assets.Data INeedThis;


            // rebase
            new IHTMLBase { href = "assets/WebGLGTAByNiklas/" }.AttachToDocument();


            new IFunction(@"


            var stats = new Stats();

            // Align top-left
            stats.domElement.style.position = 'absolute';
            stats.domElement.style.left = '0px';
            stats.domElement.style.top = '0px';

            document.body.appendChild(stats.domElement);

            setInterval(function () {

                stats.update();

            }, 1000 / 60);


            var game = new GTA.Game();



            ").apply(null);
        }

    }
}
