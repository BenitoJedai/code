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
using WebGLSimpleCubic.HTML.Pages;
using ScriptCoreLib.GLSL;
using ScriptCoreLib.JavaScript.WebGL;

namespace WebGLSimpleCubic
{
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using WebGLFloatArray = ScriptCoreLib.JavaScript.WebGL.Float32Array;
    using WebGLUnsignedShortArray = ScriptCoreLib.JavaScript.WebGL.Uint16Array;
    using Date = IDate;
    using WebGLSimpleCubic.Shaders;

    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        #region This example shall implement a Simple Cubic
        // 01. http://www.ibiblio.org/e-notes/Cryst/Cubic.html
        // 02. New project has been set up with new shaders amd preview image
        // 03. Disable InitializeContent and confirm the project builds with release version
        // 04. Commit to svn
        #endregion

        #region This example shall implement a Rotating Spiral
        // 01. http://www.brainjam.ca/stackoverflow/webglspiral.html
        // 02. Build this empty project to verify jsc does its thing.
        // 03. Running this project shows up as a web page
        // 04. Start looking at "view-source:http://www.brainjam.ca/stackoverflow/webglspiral.html"
        // 05. Extract fragment shader
        // 06. Save work and commit to svn.
        // 07. Convert shader code into .NET language
        // 08. Notice that float literals require suffix "f" unless we start supporting double in GLSL?
        // 09. Notice that uniforms and attributes are to be marked as .NET attributes
        // 10. Notice that not all operators may be defined ing ScriptCoreLib GLSL
        // 11. Fix ScriptCoreLib GLSL to support required shader operations
        // 12. Save all and commit.
        // 13. List javascript methods to be implemented
        // 14. Port javascript into C#
        // 15. Define WebGL type aliases
        // 16. Notice that C# anonymous types are immutable
        // 17. Notice that ScriptCoreLib defines IDate instead of Date
        // 18. Port "init" function
        // 19. Notice that we defined our shader source as string const
        // 20. Port "createProgram" function
        // 21. Port "createShader" function
        // 22. Port "onWindowResize" function
        // 23. Port "loop" function
        // 24. Save work and commit
        // 25. Clear jsc cache due to ScriptCoreLib update
        // 26. Run the project to see if there are any defects.
        // 27. Make canvas fullscreen/ fulldocument.
        // 28. Test, save, commit
        // 29. Enable PHP server in release build
        // 30. Test with Android Firefox 4
        // 31. Integrate with .frag and .vert files to generate types into AssetsLibrary
        // 32. Add AssetsLibrary pre build event
        // 33. Make sure JSC creates classes for frag and vert files
        #endregion



        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            //InitializeContent();




            @"WebGL loading..".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"WebGL Simple Cubic",
                value => value.ToDocumentTitle()
            );
        }

        private void InitializeContent()
        {
            // methods: 
            // init, createProgram, createShader, onWindowResize, loop

            //var effectDiv, sourceDiv, canvas, gl, buffer, vertex_shader, fragment_shader, currentProgram, vertex_position;

            var parameters_start_time = new Date().getTime();
            var parameters_time = 0L;
            var parameters_screenWidth = 0;
            var parameters_screenHeight = 0;
            var parameters_aspectX = 0.0f;
            var parameters_aspectY = 1.0f;


            var canvas = new IHTMLCanvas().AttachToDocument();

            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;
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

            var IsDisposed = false;

            Dispose = delegate
            {
                if (IsDisposed)
                    return;

                IsDisposed = true;

                canvas.Orphanize();
            };

            // Create Vertex buffer (2 triangles)

            var buffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, buffer);
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(-1.0f, -1.0f, 1.0f, -1.0f, -1.0f, 1.0f, 1.0f, -1.0f, 1.0f, 1.0f, -1.0f, 1.0f), gl.STATIC_DRAW);


            // Create Program

            #region createProgram
            Func<WebGLProgram> createProgram = () =>
            {
                var program = gl.createProgram();

                #region createShader
                Func<Shader, WebGLShader> createShader = (src) =>
                {
                    var shader = gl.createShader(src);

                    // verify
                    if (gl.getShaderParameter(shader, gl.COMPILE_STATUS) == null)
                    {
                        Native.Window.alert("error in SHADER:\n" + gl.getShaderInfoLog(shader));

                        return null;
                    }

                    return shader;
                };
                #endregion

                var vs = createShader(new CubicVertexShader());
                var fs = createShader(new CubicFragmentShader());

                if (vs == null || fs == null) return null;

                gl.attachShader(program, vs);
                gl.attachShader(program, fs);

                gl.deleteShader(vs);
                gl.deleteShader(fs);

                gl.linkProgram(program);

                if (gl.getProgramParameter(program, gl.LINK_STATUS) == null)
                {

                    Native.Window.alert("ERROR:\n" +
                  "VALIDATE_STATUS: " + gl.getProgramParameter(program, gl.VALIDATE_STATUS) + "\n" +
                  "ERROR: " + gl.getError() + "\n\n");

                    return null;

                }

                return program;

            };
            #endregion


            var currentProgram = createProgram();

            #region onWindowResize
            Action onWindowResize = delegate
            {
                if (IsDisposed)
                {
                    return;
                }

                canvas.width = Native.Window.Width;
                canvas.height = Native.Window.Height;

                parameters_screenWidth = canvas.width;
                parameters_screenHeight = canvas.height;

                parameters_aspectX = canvas.width / canvas.height;
                parameters_aspectY = 1.0f;

                gl.viewport(0, 0, canvas.width, canvas.height);
            };
            #endregion

            onWindowResize();

            Native.Window.onresize += delegate
            {
                onWindowResize();
            };

            Action loop = delegate
            {

                if (currentProgram == null) return;

                parameters_time = new Date().getTime() - parameters_start_time;

                gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                // Load program into GPU

                gl.useProgram(currentProgram);

                // Get var locations

                var vertex_position = gl.getAttribLocation(currentProgram, "position");

                // Set values to program variables

                gl.uniform1f(gl.getUniformLocation(currentProgram, "time"), parameters_time / 1000);
                gl.uniform2f(gl.getUniformLocation(currentProgram, "resolution"), parameters_screenWidth, parameters_screenHeight);
                gl.uniform2f(gl.getUniformLocation(currentProgram, "aspect"), parameters_aspectX, parameters_aspectY);

                // Render geometry

                gl.bindBuffer(gl.ARRAY_BUFFER, buffer);
                gl.vertexAttribPointer((ulong)vertex_position, 2, gl.FLOAT, false, 0, 0);
                gl.enableVertexAttribArray((ulong)vertex_position);
                gl.drawArrays(gl.TRIANGLES, 0, 6);
                gl.disableVertexAttribArray((ulong)vertex_position);

            };

            new ScriptCoreLib.JavaScript.Runtime.Timer(
                t =>
                {
                    if (IsDisposed)
                    {
                        t.Stop();
                        return;
                    }
                    loop();
                }
            ).StartInterval(1000 / 60);
        }

        public Action Dispose;
    }


}
