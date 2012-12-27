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



            #region hint such that our assets stay around

            { ITexturesImages ref0; }
            { Audio ref0; }


            { Design.models.eagle ref1; }
            { Design.models.glowbulb ref1; }
            { Design.models.horse ref1; }
            { Design.models.rock ref1; }
            { Design.models.sleigh ref1; }
            { Design.models.treeDead ref1; }
            { Design.models.treeEvergreenHigh ref1; }
            { Design.models.weeds01 ref1; }

            #endregion


            new AppCode().Content.AttachToDocument().onload +=
                delegate
                {
                    var w = Native.Window;

                    dynamic window = w;

                    var renderer = (THREE_WebGLRenderer)(object)window.webglRenderer;
                    var camera = (THREE_PerspectiveCamera)(object)window.camera;


                    //var renderer = (THREE_WebGLRenderer)new IFunction("return this.webglRenderer;").apply(Native.Window);
                    //var camera = (THREE_PerspectiveCamera)new IFunction("return this.camera;").apply(Native.Window);


                    Native.Window.onresize +=
                        delegate
                        {

                            // notify the renderer of the size change
                            renderer.setSize(Native.Window.Width, Native.Window.Height);


                            // update the camera
                            camera.aspect = Native.Window.Width / Native.Window.Height;
                            camera.updateProjectionMatrix();
                        };

                    Native.Document.body.ondblclick +=
                        delegate
                        {
                            Native.Document.body.requestFullscreen();
                        };


               
                    Native.Document.oncontextmenu +=
                        e =>
                        {
                            e.preventDefault();
                        };

                    Native.Document.onmousemove +=
                        e =>
                        {
                            var windowHalfX = Native.Window.Width >> 1;
                            var windowHalfY = Native.Window.Height >> 1;

                            var mouseX = (e.CursorX - windowHalfX);
                            var mouseY = (e.CursorY - windowHalfY);

                            window.mouseXpercent = mouseX / windowHalfX;
                            window.mouseYpercent = mouseY / windowHalfY;
                        };
                };

        }

    }
}
