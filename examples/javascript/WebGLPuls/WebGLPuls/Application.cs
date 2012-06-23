// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Extensions;
using WebGLPuls.HTML.Pages;
using WebGLPuls;
using ScriptCoreLib.Avalon;
using ScriptCoreLib.JavaScript.WebGL;

namespace WebGLPuls
{
    using gl = WebGLRenderingContext;
    using ScriptCoreLib.GLSL;
    using WebGLPuls.Shaders;

    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class Application
    {
        // see also: http://meatfighter.com/puls/
        // it only took 2 years :)
        // Revision: 2744
        //Date: Friday, July 23, 2010 4:34:01 PM
        //Added : /templates/TwentyTen/WebGLPuls/WebGLPuls.sln




        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IXDefaultPage page = null)
        {

            InitializeContent(page);
        }

        public Action Dispose;

        private void InitializeContent(IXDefaultPage page = null)
        {
            var canvas = new IHTMLCanvas().AttachToDocument();

            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;

            canvas.style.SetLocation(0, 0);


            //http://www.khronos.org/webgl/public-mailing-list/archives/1002/msg00125.html
        

            var gl = (WebGLRenderingContext)canvas.getContext("experimental-webgl");

            #region Dispose
            var IsDisposed = false;

            Dispose = delegate
            {
                if (IsDisposed)
                    return;

                IsDisposed = true;

                canvas.Orphanize();
            };
            #endregion


            #region AtResize
            Action AtResize = delegate
            {
                if (IsDisposed)
                {
                    return;
                }

                canvas.width = Native.Window.Width;
                canvas.height = Native.Window.Height;

     

                gl.viewport(0, 0, canvas.width, canvas.height);
            };

            AtResize();

            Native.Window.onresize += delegate
            {
                AtResize();
            };
            #endregion


            #region requestFullscreen
            Native.Document.body.ondblclick +=
                delegate
                {
                    if (IsDisposed)
                        return;

                    // http://tutorialzine.com/2012/02/enhance-your-website-fullscreen-api/

                    Native.Document.body.requestFullscreen();


                };
            #endregion


            // http://cs.helsinki.fi/u/ilmarihe/metatunnel.html
            // http://wakaba.c3.cx/w/puls.html

            Action<string> alert = x => Native.Window.alert(x);



          


          


            #region createShader
            Func<ScriptCoreLib.GLSL.Shader, WebGLShader> createShader = (src) =>
            {
                var shader = gl.createShader(src);

                // verify
                if (gl.getShaderParameter(shader, gl.COMPILE_STATUS) == null)
                {
                    Native.Window.alert("error in SHADER:\n" + gl.getShaderInfoLog(shader));
                    throw new InvalidOperationException("shader failed");
                }

                return shader;
            };
            #endregion

            var p = gl.createProgram();
            var vs = createShader(new PulsVertexShader());
            var fs = createShader(new PulsFragmentShader());

            gl.attachShader(p, vs);
            gl.attachShader(p, fs);
            gl.bindAttribLocation(p, 0, "position");
            gl.linkProgram(p);

            var linked = gl.getProgramParameter(p, gl.LINK_STATUS);
            if (linked == null)
            {
                var error = gl.getProgramInfoLog(p);
                alert("Error while linking: " + error);
                return;
            }

            //Native.Document.title = "WebGL..";

            gl.useProgram(p);



            var pos = 0;
            //var in_color = gl.getUniformLocation(p, "in_color");

            gl.enableVertexAttribArray((uint)pos);


            var verts = gl.createBuffer();

            gl.bindBuffer(gl.ARRAY_BUFFER, verts);
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(
              new float[] { 
                  -1,-1,  -1,1,  1,-1, 1,1,
              }
            ), gl.STATIC_DRAW);
            gl.vertexAttribPointer((uint)pos, 2, gl.FLOAT, false, 0, 0);

            var indicies = gl.createBuffer();

            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indicies);



            gl.bufferData(gl.ELEMENT_ARRAY_BUFFER,
                new Uint16Array(
                /*new ushort[] {*/ 0, 1, 2, 3 /*}*/
                    )
                , gl.STATIC_DRAW);

            var start = new IDate().getTime();
            Action redraw = null;

            redraw = delegate
            {
                gl.viewport(0, 0, Native.Window.Width, Native.Window.Height);
                gl.uniform1f(gl.getUniformLocation(p, "h"), Native.Window.Height / Native.Window.Width);


                var timestamp = new IDate().getTime();
                var t = (float)((timestamp - start) / 1000.0 * 30);

                // INVALID_OPERATION <= getUniformLocation([Program 2], "t")
                gl.uniform1f(gl.getUniformLocation(p, "t"), t);

                // INVALID_OPERATION <= drawElements(TRIANGLE_STRIP, 4, UNSIGNED_SHORT, 0)
                gl.drawElements(gl.TRIANGLE_STRIP, 4, gl.UNSIGNED_SHORT, 0);
                gl.flush();


                Native.Window.requestAnimationFrame += redraw;

            };

            Native.Window.requestAnimationFrame += redraw;

        }


    }



}
