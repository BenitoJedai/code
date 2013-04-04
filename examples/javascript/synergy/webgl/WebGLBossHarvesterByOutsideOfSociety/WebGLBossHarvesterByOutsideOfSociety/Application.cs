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
using WebGLBossHarvesterByOutsideOfSociety.Design;
using WebGLBossHarvesterByOutsideOfSociety.HTML.Pages;

namespace WebGLBossHarvesterByOutsideOfSociety
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
            #region await Three.js then do InitializeContent
            new[]
            {
                //new CANNON.opensource.github.cannon.js.build.cannon().Content,
                new THREE.opensource.gihtub.three.js.build.three().Content,
                //new global::WebGLCannonPhysicsEngine.Design.References.PointerLockControls().Content,
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

        private static void InitializeContent()
        {
        }
    }
}
