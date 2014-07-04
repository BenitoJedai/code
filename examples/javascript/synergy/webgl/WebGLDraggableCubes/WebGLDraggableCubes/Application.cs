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
using WebGLDraggableCubes.Design;
using WebGLDraggableCubes.HTML.Pages;
using ScriptCoreLib.Shared.Lambda;

namespace WebGLDraggableCubes
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
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/13-dataviz/201407/20140704


            THREE.Color ReferenceTHREE;

            //DiagnosticsConsole.ApplicationContent.BindKeyboardToDiagnosticsConsole();

            // "X:\opensource\github\three.js\examples\js\controls\TrackballControls.js"

            // http://mrdoob.github.com/three.js/examples/webgl_interactive_draggablecubes.html
            #region await Three.js then do InitializeContent
            new[]
            {
                //new THREE.opensource.gihtub.three.js.build.three().Content,
                new TrackballControls().Content,
                new stats().Content,
                new AppCode().Content,
            }.ForEach(
                (SourceScriptElement, i, MoveNext) =>
                {
                    SourceScriptElement.AttachToDocument().onload +=
                        delegate
                        {
                            MoveNext();
                        };
                }
            )(
                delegate
                {
                    InitializeContent();
                }
            );
            #endregion
        }

        private void InitializeContent()
        {

        }

    }
}
