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
using WebGLSpiral.HTML.Pages;
using ScriptCoreLib.GLSL;
using ScriptCoreLib.JavaScript.WebGL;

namespace WebGLSpiral
{
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using WebGLFloatArray = ScriptCoreLib.JavaScript.WebGL.Float32Array;
    using WebGLUnsignedShortArray = ScriptCoreLib.JavaScript.WebGL.Uint16Array;
    using Date = IDate;

    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    internal sealed class Application
    {
        // This example shall implement a Rotating Spiral 
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

        const string VertexShaderSource = @"
			attribute vec3 position;
 
			void main() {
 
				gl_Position = vec4( position, 1.0 );
 
			}
        ";



        const string FragmentShaderSource = @"
            uniform float time;
			uniform vec2 resolution;
			uniform vec2 aspect;
 
			void main( void ) {
 
				vec2 position = -aspect.xy + 2.0 * gl_FragCoord.xy / resolution.xy * aspect.xy;
                float angle = 0.0 ;
                float radius = sqrt(position.x*position.x + position.y*position.y) ;
                if (position.x != 0.0 && position.y != 0.0){
                    angle = degrees(atan(position.y,position.x)) ;
                }
                float amod = mod(angle+30.0*time-120.0*log(radius), 30.0) ;
                if (amod<15.0){
                    gl_FragColor = vec4( 0.0, 0.0, 0.0, 1.0 );
                } else{
                    gl_FragColor = vec4( 1.0, 1.0, 1.0, 1.0 );                    
                }
			}
        ";

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
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

            Action init = delegate
            {

                var vertex_shader = Application.VertexShaderSource;
                var fragment_shader = Application.FragmentShaderSource;

                //effectDiv = document.getElementById( 'effect' );
                //sourceDiv = document.getElementById( 'info' );
                //sourceDiv.innerHTML = '--- adapted from http://mrdoob.com/lab/javascript/webgl/glsl/02/ by mrdoob<br/>'+
                //                    '--- answer for http://stackoverflow.com/questions/4638317'

                var canvas = new IHTMLCanvas().AttachToDocument();


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

                // Create Vertex buffer (2 triangles)

                var buffer = gl.createBuffer();
                gl.bindBuffer(gl.ARRAY_BUFFER, buffer);
                gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(-1.0f, -1.0f, 1.0f, -1.0f, -1.0f, 1.0f, 1.0f, -1.0f, 1.0f, 1.0f, -1.0f, 1.0f), gl.STATIC_DRAW);


                // Create Program

                #region createProgram
                Func<string, string, WebGLProgram> createProgram = (vertex, fragment) =>
                {
                    var program = gl.createProgram();

                    #region createShader
                    Func<string, ulong, WebGLShader> createShader = (src, type) =>
                    {

                        var shader = gl.createShader(type);

                        gl.shaderSource(shader, src);
                        gl.compileShader(shader);
                        if (gl.getShaderParameter(shader, gl.COMPILE_STATUS) == null)
                        {
                            if (type == gl.VERTEX_SHADER)
                                Native.Window.alert("VERTEX SHADER:\n" + gl.getShaderInfoLog(shader));
                            else
                                Native.Window.alert("FRAGMENT SHADER:\n" + gl.getShaderInfoLog(shader));
                            return null;

                        }

                        return shader;
                    };
                    #endregion

                    var vs = createShader(vertex, gl.VERTEX_SHADER);
                    var fs = createShader("#ifdef GL_ES\nprecision highp float;\n#endif\n\n" + fragment, gl.FRAGMENT_SHADER);

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
                      "ERROR: " + gl.getError() + "\n\n" +
                      "- Vertex Shader -\n" + vertex + "\n\n" +
                      "- Fragment Shader -\n" + fragment);

                        return null;

                    }

                    return program;

                };
                #endregion


                var currentProgram = createProgram(vertex_shader, fragment_shader);

                #region onWindowResize
                Action onWindowResize = delegate
                {
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
                    delegate
                    {
                        loop();
                    }
                ).StartInterval(1000 / 60);
            };

            init();




            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }

    class FragmentShader : ScriptCoreLib.GLSL.FragmentShader
    {
        [uniform]
        float time;
        [uniform]
        vec2 resolution;
        [uniform]
        vec2 aspect;

        void main()
        {

            vec2 position = -aspect.xy + 2.0f * gl_FragCoord.xy / resolution.xy * aspect.xy;
            float angle = 0.0f;
            float radius = sqrt(position.x * position.x + position.y * position.y);
            if (position.x != 0.0f && position.y != 0.0f)
            {
                angle = degrees(atan(position.y, position.x));
            }
            float amod = mod(angle + 30.0f * time - 120.0f * log(radius), 30.0f);
            if (amod < 15.0)
            {
                gl_FragColor = vec4(0.0f, 0.0f, 0.0f, 1.0f);
            }
            else
            {
                gl_FragColor = vec4(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
    }

    class VertexShader : ScriptCoreLib.GLSL.VertexShader
    {
        [attribute]
        vec3 position;

        void main()
        {
            gl_Position = vec4(position, 1.0f);
        }
    }
}
