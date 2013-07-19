using System;
using ScriptCoreLib.GLSL;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;
using WebGLShaderDisturb.HTML.Pages;
using WebGLShaderDisturb.Shaders;

namespace WebGLShaderDisturb
{
    using gl = WebGLRenderingContext;
    using WebGLFloatArray = Float32Array;
    using WebGLUnsignedShortArray = Uint16Array;
    using System.Dynamic;
    using ScriptCoreLib.Shared.BCLImplementation.GLSL;

    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page = null)
        {
            // view-source:http://mrdoob.com/lab/javascript/webgl/glsl/04/

            var parameters_start_time = new IDate().getTime();
            var parameters_time = 0L;
            var parameters_screenWidth = 0;
            var parameters_screenHeight = 0;

            var canvas = new IHTMLCanvas();

            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;

            canvas.AttachToDocument();
            canvas.style.SetLocation(0, 0);

            #region Initialise WebGL

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


            #region IsDisposed
            var IsDisposed = false;

            Dispose = delegate
            {
                if (IsDisposed)
                    return;

                IsDisposed = true;

                canvas.Orphanize();
            };
            #endregion

            // Create Vertex buffer (2 triangles)

            var buffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, buffer);
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(-1.0f, -1.0f, 1.0f, -1.0f, -1.0f, 1.0f, 1.0f, -1.0f, 1.0f, 1.0f, -1.0f, 1.0f), gl.STATIC_DRAW);

            // Create Program


            var program = gl.createProgram(
                new DisturbVertexShader(),
                new DisturbFragmentShader()
            );

          

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
                        gl.bindTexture(gl.TEXTURE_2D, null);


                    }
                );

                return texture_;

            };
            #endregion


            var texture = loadTexture(new HTML.Images.FromAssets.disturb());


            var vertexPositionLocation = default(long);
            var textureLocation = default(WebGLUniformLocation);

            #region resize
            Action resize = delegate
            {
                canvas.width = Native.Window.Width;
                canvas.height = Native.Window.Height;

                parameters_screenWidth = canvas.width;
                parameters_screenHeight = canvas.height;

                gl.viewport(0, 0, canvas.width, canvas.height);
            };

            Native.Window.onresize +=
                delegate
                {
                    if (IsDisposed)
                        return;

                    resize();
                };

            resize();
            #endregion


            #region loop
            Action loop = null;

            loop = delegate
            {
                if (IsDisposed)
                    return;

                if (program == null) return;

                parameters_time = new IDate().getTime() - parameters_start_time;

                gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                // Load program into GPU


                // Get var locations

                vertexPositionLocation = gl.getAttribLocation(program, "position");
                textureLocation = gl.getUniformLocation(program, "texture");

                // Set values to program variables

                var program_uniforms = program.Uniforms(gl);


                var resolution = new __vec2 { x = parameters_screenWidth, y = parameters_screenHeight };


                program_uniforms.time = parameters_time / 1000f;
                program_uniforms.resolution = resolution;

                //gl.uniform1f(gl.getUniformLocation(program, "time"), parameters_time / 1000);
                //gl.uniform2f(gl.getUniformLocation(program, "resolution"), parameters_screenWidth, parameters_screenHeight);

                gl.uniform1i(textureLocation, 0);
                gl.activeTexture(gl.TEXTURE0);
                gl.bindTexture(gl.TEXTURE_2D, texture);

                // Render geometry

                gl.bindBuffer(gl.ARRAY_BUFFER, buffer);
                gl.vertexAttribPointer((uint)vertexPositionLocation, 2, gl.FLOAT, false, 0, 0);
                gl.enableVertexAttribArray((uint)vertexPositionLocation);
                gl.drawArrays(gl.TRIANGLES, 0, 6);
                gl.disableVertexAttribArray((uint)vertexPositionLocation);




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


     
        }

        public readonly Action Dispose;
    }

    //class ShaderProgramUniforms : DynamicObject
    //{
    //    public WebGLProgram program;
    //    public gl gl;

    //    public override bool TrySetMember(SetMemberBinder binder, object value)
    //    {
    //        // cache location

    //        var isvec2 = value is __vec2;
    //        if (isvec2)
    //        {
    //            var value_vec2 = (__vec2)value;

    //            gl.uniform2f(
    //                gl.getUniformLocation(program, binder.Name),
    //                value_vec2
    //            );

    //            return true;
    //        }

    //        gl.uniform1f(gl.getUniformLocation(program, binder.Name), (float)value);
    //        return true;
    //    }
    //}
}
