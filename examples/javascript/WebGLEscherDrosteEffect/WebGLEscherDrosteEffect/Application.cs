using System;
using ScriptCoreLib.GLSL;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;
using WebGLEscherDrosteEffect.HTML.Pages;
using WebGLEscherDrosteEffect.Shaders;

namespace WebGLEscherDrosteEffect
{
    using gl = WebGLRenderingContext;
    using WebGLFloatArray = Float32Array;
    using WebGLUnsignedShortArray = Uint16Array;

    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        /* Steps taken to create this example port of http://wakaba.c3.cx/w/escher_droste.html
         * 
         * 01. Create new Web Application project
         * 02. Collect the images, add to the html default design and rebuild assets
         * 03. Add shader source code and rebuild assets
         * 04. Verify type WebGLEscherDrosteEffect.Shaders.EscherDorsteFragmentShader
         * 05. Port function start
         * 06. Port function init
         * 07. Port function get_context
         * 08. Port function load_shader
         * 09. Define type aliases
         * 10. Port function draw
         * 11. Enable resizing
         * 12. Inline start and init
         * 13. Add preview image (use irfanview to set 72 dpi)
         * 14. Enable project template generation 
         * 15. Create Shaders.vNext
         * 16. Test on desktop with Chrome and Firefox 4 beta
         * 17. Test on android with Firefox (requires xampp)
         */

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page = null)
        {
            #region canvas 3D
            var canvas = new IHTMLCanvas();

            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;

            canvas.AttachToDocument();
            canvas.style.SetLocation(0, 0);

            // Initialise WebGL

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

            #region init

            var program = gl.createProgram(
                new EscherDorsteVertexShader(),
                new EscherDorsteFragmentShader()
            );

            gl.bindAttribLocation(program, 0, "position");

            gl.linkProgram(program);
            gl.useProgram(program);

            #region loadTexture
            Func<IHTMLImage, WebGLTexture> loadTexture = (image) =>
            {

                var texture_ = gl.createTexture();

                image.InvokeOnComplete(
                    delegate
                    {

                        gl.enable(gl.TEXTURE_2D);
                        gl.bindTexture(gl.TEXTURE_2D, texture_);
                        gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, image);
                        gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (int)gl.LINEAR);
                        gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (int)gl.LINEAR_MIPMAP_LINEAR);
                        gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_S, (int)gl.REPEAT);
                        gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_T, (int)gl.REPEAT);
                        gl.generateMipmap(gl.TEXTURE_2D);
                        //not needed?
                        //gl.bindTexture(gl.TEXTURE_2D, null);


                    }
                );

                return texture_;

            };
            #endregion

            var texture = loadTexture(new HTML.Images.FromAssets.escher());

            #region AtResize
            Action AtResize = delegate
            {
                canvas.width = Native.Window.Width;
                canvas.height = Native.Window.Height;

                var width = canvas.width;
                var height = canvas.height;

                gl.viewport(0, 0, canvas.width, canvas.height);



                var h = height / width;
                gl.uniform1f(gl.getUniformLocation(program, "h"), h);
            };

            AtResize();

            Native.Window.onresize += delegate
            {
                if (IsDisposed)
                    return;

                AtResize();
            };
            #endregion

            gl.uniform1i(gl.getUniformLocation(program, "texture"), 0);

            gl.enableVertexAttribArray(0);

            var vertices = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, vertices);
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(
                -1f, -1f, -1f, 1f, 1f, -1f, 1f, 1f
            ), gl.STATIC_DRAW);
            gl.vertexAttribPointer(0, 2, gl.FLOAT, false, 0, 0);

            var indices = gl.createBuffer();
            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indices);
            gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(
                0, 1, 2, 3
            ), gl.STATIC_DRAW);

            #endregion



            #region loop
            var start_time = new IDate().getTime();

            Action loop = null;

            loop = delegate
             {
                 if (IsDisposed)
                     return;

                 var t = (new IDate().getTime() - start_time) / 1000;

                 gl.uniform1f(gl.getUniformLocation(program, "t"), t);
                 gl.drawElements(gl.TRIANGLE_STRIP, 4, gl.UNSIGNED_SHORT, 0);
                 gl.flush();

                 Native.Window.requestAnimationFrame += loop;

             };

            Native.Window.requestAnimationFrame += loop;
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


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

        public readonly Action Dispose;


    }
}
