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
using WebGLDoomByInt13h;
using WebGLDoomByInt13h.Design;
using WebGLDoomByInt13h.HTML.Pages;

namespace WebGLDoomByInt13h
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
            { WebGLDoomByInt13h.Assets.he3d.Scripts ref0; }
            { WebGLDoomByInt13h.Assets.he3d.Shaders ref0; }
            { WebGLDoomByInt13h.Assets.webgldoom.Shaders ref1; }
            { WebGLDoomByInt13h.Assets.webgldoom.Wads ref1; }

            { WebGLDoomByInt13h.Design.opensource.github.webgldoom.wadLoader ref3; }
            { WebGLDoomByInt13h.Design.opensource.github.webgldoom.wadLoader_audio ref3; }
            { WebGLDoomByInt13h.Design.opensource.github.webgldoom.wadLoader_heightmap ref3; }
            { WebGLDoomByInt13h.Design.opensource.github.webgldoom.wadLoader_level ref3; }
            { WebGLDoomByInt13h.Design.opensource.github.webgldoom.wadLoader_textures ref3; }
            { WebGLDoomByInt13h.Design.opensource.github.webgldoom.wadLoader_things ref3; }


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );



            Native.window.navigator.getUserMedia(
                stream =>
                {
                    page.v.src = stream.ToObjectURL();
                    page.v.play();
                }
            );
        }

    }
}
