using System;
using ScriptCoreLib.GLSL;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;
using WebGLSpiral.HTML.Pages;
using WebGLSpiral.Shaders;

namespace WebGLSpiral
{
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;

    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // 01. http://www.brainjam.ca/stackoverflow/webglspiral.html

        #region This example shall implement a Rotating Spiral
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
        public Application(IDefaultPage page = null)
        {
            InitializeContent();
        }

        private void InitializeContent()
        {
            // methods: 
            // init, createProgram, createShader, onWindowResize, loop

            //var effectDiv, sourceDiv, canvas, gl, buffer, vertex_shader, fragment_shader, currentProgram, vertex_position;

            var parameters_start_time = new IDate().getTime();
            var parameters_time = 0L;
            var parameters_screenWidth = 0;
            var parameters_screenHeight = 0;
            var parameters_aspectX = 0.0f;
            var parameters_aspectY = 1.0f;


            var canvas = new IHTMLCanvas().AttachToDocument();

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


            // Create Vertex buffer (2 triangles)

            var buffer = gl.createBuffer();

            gl.bindBuffer(gl.ARRAY_BUFFER, buffer);
            gl.bufferData(gl.ARRAY_BUFFER, 
                new Float32Array(
                    -1.0f, -1.0f, 1.0f, 
                    -1.0f, -1.0f, 1.0f, 
                    1.0f, -1.0f, 1.0f, 
                    1.0f, -1.0f, 1.0f), gl.STATIC_DRAW);
            // Create Program


            var currentProgram = gl.createProgram();

            var vs = gl.createShader(new SpiralVertexShader());
            var fs = gl.createShader(new SpiralFragmentShader());

            gl.attachShader(currentProgram, vs);
            gl.attachShader(currentProgram, fs);

            gl.deleteShader(vs);
            gl.deleteShader(fs);

            gl.linkProgram(currentProgram);



            #region AtResize
            Action AtResize = delegate
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

            AtResize();

            Native.Window.onresize += delegate
            {
                AtResize();
            };
            #endregion


            #region loop
            Action loop = null;

            loop = delegate
            {
                if (IsDisposed)
                    return;

                if (currentProgram == null) return;

                parameters_time = new IDate().getTime() - parameters_start_time;

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
                gl.vertexAttribPointer((uint)vertex_position, 2, gl.FLOAT, false, 0, 0);
                gl.enableVertexAttribArray((uint)vertex_position);
                gl.drawArrays(gl.TRIANGLES, 0, 6);
                gl.disableVertexAttribArray((uint)vertex_position);

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

            @"WebGL loading..".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"WebGL..",
                value => value.ToDocumentTitle()
            );
        }

        public Action Dispose;
    }


}
