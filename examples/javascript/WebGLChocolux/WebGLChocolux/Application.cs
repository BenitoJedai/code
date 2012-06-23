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
using WebGLChocolux.HTML.Pages;
using WebGLChocolux;
using ScriptCoreLib.Avalon;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.GLSL;

namespace WebGLChocolux
{
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using WebGLFloatArray = ScriptCoreLib.JavaScript.WebGL.Float32Array;
    using WebGLUnsignedShortArray = ScriptCoreLib.JavaScript.WebGL.Uint16Array;
    using Date = IDate;
    using WebGLChocolux.Shaders;

    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class Application
    {


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IXDefaultPage page = null)
        {
            Initialize(page);
        }

        private void Initialize(IXDefaultPage page = null)
        {
            int w = Native.Window.Width;
            int h = Native.Window.Height;

            var canvas = new IHTMLCanvas().AttachToDocument();
            canvas.style.backgroundColor = JSColor.Black;
            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;
            canvas.style.SetLocation(0, 0);

            #region gl

            var gl = default(WebGLRenderingContext);

            try
            {

                gl = (WebGLRenderingContext)canvas.getContext("experimental-webgl");

            }
            catch { }

            if (gl == null)
            {
                Native.Window.alert("WebGL not supported");
                throw new InvalidOperationException("cannot create webgl context");
            }
            #endregion


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


            // http://cs.helsinki.fi/u/ilmarihe/metatunnel.html

            // jsc: can we take a direct delegate from native method?
            Action<string> alert = x => Native.Window.alert(x);


       

            var p = gl.createProgram();


            #region createShader
            Func<Shader, WebGLShader> createShader = (src) =>
            {
                var shader = gl.createShader(src);

                // verify
                if (gl.getShaderParameter(shader, gl.COMPILE_STATUS) == null)
                {
                    Native.Window.alert("error in SHADER:\n" + gl.getShaderInfoLog(shader));
                    throw new InvalidOperationException("shader");
                }

                return shader;
            };
            #endregion

            var vs = createShader(new ChocoluxVertexShader());
            var fs = createShader(new ChocoluxFragmentShader());


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


            gl.useProgram(p);
            gl.viewport(0, 0, w, h);

            gl.enableVertexAttribArray(0);


            var verts = gl.createBuffer();

            gl.bindBuffer(gl.ARRAY_BUFFER, verts);
            gl.bufferData(gl.ARRAY_BUFFER, new WebGLFloatArray(
              new [] { -1f, -1f, -1f, 1f, 1f, -1f, 1f, 1f }
            ), gl.STATIC_DRAW);
            gl.vertexAttribPointer((uint)0, 2, gl.FLOAT, false, 0, 0);

            var indicies = gl.createBuffer();

            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indicies);

            var q = new WebGLUnsignedShortArray(0, 1, 2, 3);



            gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, q, gl.STATIC_DRAW);

            var start = new IDate().getTime();
            Action redraw = null;
            
            redraw = delegate
            {
                var timestamp = new IDate().getTime();
                var t = (timestamp - start) / 1000.0f * 30f;



                gl.uniform1f(gl.getUniformLocation(p, "t"), t * 100);
                gl.drawElements(gl.TRIANGLE_STRIP, 4, gl.UNSIGNED_SHORT, 0);
                gl.flush();


                Native.Window.requestAnimationFrame += redraw;

            };

            Native.Window.requestAnimationFrame += redraw;

   

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

        }

        public Action Dispose;
    }



}
