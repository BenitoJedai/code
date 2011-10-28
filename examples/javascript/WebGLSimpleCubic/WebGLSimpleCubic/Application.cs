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
        // 05. Add CanvasMatrix.js
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
            new Library.CanvasMatrix().Content.With(
                CanvasMatrix =>
                {
                    CanvasMatrix.onload +=
                        delegate
                        {
                            //new IFunction("alert(CanvasMatrix4);").apply(null);

                            InitializeSimpleCubicContent();
                        };

                    CanvasMatrix.AttachToDocument();
                }
            );


            //InitializeContent();




            @"WebGL loading..".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"WebGL Simple Cubic",
                value => value.ToDocumentTitle()
            );
        }

        private void InitializeSimpleCubicContent()
        {
            // functions to port manually
            // * webGLStart
            // * canvas.resize
            // * getShader

            //var gl, canvas;

            var size = 500;

            #region canvas
            var canvas = new IHTMLCanvas().AttachToDocument();

            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;
            canvas.style.SetLocation(4, 4, size, size);
            canvas.style.border = "1px solid red";
            #endregion

            #region gl - Initialise WebGL


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

            #region getShader
            //function getShader ( gl, id ){
            //   var shaderScript = document.getElementById ( id );
            //   var str = "";
            //   var k = shaderScript.firstChild;
            //   while ( k ){
            //     if ( k.nodeType == 3 ) str += k.textContent;
            //     k = k.nextSibling;
            //   }
            //   var shader;
            //   if ( shaderScript.type == "x-shader/x-fragment" )
            //           shader = gl.createShader ( gl.FRAGMENT_SHADER );
            //   else if ( shaderScript.type == "x-shader/x-vertex" )
            //           shader = gl.createShader(gl.VERTEX_SHADER);
            //   else return null;
            //   gl.shaderSource(shader, str);
            //   gl.compileShader(shader);
            //   if (gl.getShaderParameter(shader, gl.COMPILE_STATUS) == 0)
            //      alert(id +"\n"+ gl.getShaderInfoLog(shader));
            //   return shader;
            //}
            #endregion




            //   canvas = document.getElementById("canvas");
            //   var size = Math.min(window.innerWidth, window.innerHeight) - 10;
            //   canvas.width =  size;   canvas.height = size;
            //   if (!window.WebGLRenderingContext){
            //     alert("Your browser does not support WebGL. See http://get.webgl.org");
            //     return;}
            //   try { gl = canvas.getContext("experimental-webgl");
            //   } catch(e) {}
            //   if ( !gl ) {alert("Can't get WebGL"); return;}
            //   gl.viewport(0, 0, size, size);

            //   var prog  = gl.createProgram();
            //   gl.attachShader(prog, getShader( gl, "shader-vs" ));
            //   gl.attachShader(prog, getShader( gl, "shader-fs" ));
            //   var posLoc = 0;
            //   gl.bindAttribLocation(prog, posLoc, "aPos");
            //   var normLoc = 1;
            //   gl.bindAttribLocation(prog, normLoc, "aNorm");
            //   gl.linkProgram(prog);
            //   gl.useProgram(prog);

            //   var a = 1;
            //   var pt = [-a,-a,a, a,-a,a, -a,a,a, a,a,a,  // cubic
            //     -a,a,a, a,a,a, -a,a,-a, a,a,-a,
            //     -a,a,-a, a,a,-a, -a,-a,-a, a,-a,-a,  -a,-a,-a, a,-a,-a, -a,-a,a, a,-a,a,
            //     a,a,a, a,a,-a, a,-a,a, a,-a,-a,  -a,a,a, -a,a,-a, -a,-a,a, -a,-a,-a];
            //   var nt = [0,0,1, 0,0,1, 0,0,1, 0,0,1,  0,1,0, 0,1,0, 0,1,0, 0,1,0,
            //     0,0,-1, 0,0,-1, 0,0,-1, 0,0,-1,  0,-1,0, 0,-1,0, 0,-1,0, 0,-1,0,
            //     1,0,0, 1,0,0, 1,0,0, 1,0,0,  -1,0,0, -1,0,0, -1,0,0, -1,0,0];
            //   var ind = [0,1,2,1,2,3, 4,5,6,5,6,7, 8,9,10,9,10,11,
            //     12,13,14,13,14,15, 16,17,18,17,18,19, 20,21,22,21,22,23];

            //   var nPhi = 25, nTheta = 12, r = .15,
            //     dPhi = 2*Math.PI/nPhi, dTheta = Math.PI/nTheta;
            //   for (var j = 0; j <= nTheta; j++ ){
            //      var Theta    = j * dTheta;
            //      var cosTheta = Math.cos ( Theta );
            //      var sinTheta = Math.sin ( Theta );
            //      for (var i = 0; i <= nPhi; i++ ){
            //         var Phi    = i * dPhi;
            //         var cosPhi = Math.cos ( Phi );
            //         var sinPhi = Math.sin ( Phi );
            //         pt.push( r*cosPhi*sinTheta, -r*sinPhi*sinTheta,  r*cosTheta );
            //         nt.push( cosPhi * sinTheta, -sinPhi * sinTheta,  cosTheta );
            //      }
            //   }
            //   var n1 = nPhi + 1, off = 24;
            //   for ( i = 0; i < nTheta; i++ )
            //     for ( j = 0; j < nPhi; j++ )
            //       ind.push (i*n1+j+off, (i+1)*n1+j+1+off, i*n1+j+1+off, i*n1+j+off,
            //         (i+1)*n1+j+off, (i+1)*n1+j+1+off );

            //   gl.enableVertexAttribArray( posLoc );
            //   gl.bindBuffer(gl.ARRAY_BUFFER, gl.createBuffer());
            //   gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(pt), gl.STATIC_DRAW);
            //   gl.vertexAttribPointer(posLoc, 3, gl.FLOAT, false, 0, 0);

            //   gl.enableVertexAttribArray( normLoc );
            //   gl.bindBuffer(gl.ARRAY_BUFFER, gl.createBuffer());
            //   gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(nt), gl.STATIC_DRAW);
            //   gl.vertexAttribPointer(normLoc, 3, gl.FLOAT, false, 0, 0);

            //   gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, gl.createBuffer());
            //   gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(ind),
            //     gl.STATIC_DRAW);

            //   var prMatrix = new CanvasMatrix4();
            //   prMatrix.perspective(45, 1, .1, 100);
            //   gl.uniformMatrix4fv( gl.getUniformLocation(prog,"prMatrix"),
            //      false, new Float32Array(prMatrix.getAsArray()) );
            //   var mvMatrix = new CanvasMatrix4();
            //   var rotMat = new CanvasMatrix4();
            //   rotMat.makeIdentity();
            //   rotMat.rotate(25, 1,1,0);
            //   var mvMatLoc = gl.getUniformLocation(prog,"mvMatrix");
            //   var colorLoc = gl.getUniformLocation(prog,"u_color");

            //   var line_prog  = gl.createProgram();
            //   gl.attachShader(line_prog, getShader( gl, "line-vs" ));
            //   gl.attachShader(line_prog, getShader( gl, "line-fs" ));
            //   var lineLoc = 2;
            //   gl.bindAttribLocation(line_prog, lineLoc, "aPos");
            //   gl.linkProgram(line_prog);
            //   gl.useProgram(line_prog);
            //   gl.uniformMatrix4fv( gl.getUniformLocation(line_prog,"prMatrix"),
            //      false, new Float32Array(prMatrix.getAsArray()) );
            //   var mvMatLineLoc = gl.getUniformLocation(line_prog,"mvMatrix");

            //   var pt = [2,1,1, -2,1,1, 2,-1,1, -2,-1,1, 2,1,-1, -2,1,-1, 2,-1,-1, -2,-1,-1,
            //     1,2,1, 1,-2,1, 1,2,-1, 1,-2,-1, -1,2,1, -1,-2,1, -1,2,-1, -1,-2,-1, 
            //     1,1,2, 1,1,-2, -1,1,2, -1,1,-2, 1,-1,2, 1,-1,-2, -1,-1,2, -1,-1,-2
            //   ];
            //   gl.enableVertexAttribArray( lineLoc );
            //   gl.bindBuffer(gl.ARRAY_BUFFER, gl.createBuffer());
            //   gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(pt), gl.STATIC_DRAW);
            //   gl.vertexAttribPointer(lineLoc, 3, gl.FLOAT, false, 0, 0);

            //   gl.enable(gl.DEPTH_TEST);
            //   gl.depthFunc(gl.LEQUAL);
            //   gl.clearDepth(1.0);
            //   gl.clearColor(.5, 1., .5, 1);
            //   gl.lineWidth( 2 );
            //   var xOffs = yOffs = 0,  drag  = 0;
            //   var xRot = yRot = 0;
            //   var transl = -6;
            //   drawScene();

            #region drawScene
            //  function drawScene(){
            //    gl.useProgram(prog);
            //    rotMat.rotate(xRot/5, 1,0,0);  rotMat.rotate(yRot/5, 0,1,0);
            //    yRot = 0;  xRot = 0;

            //    gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

            //    gl.uniform4f( colorLoc, 1, 1, 0, 1 );
            //    drawBall(1, 1, 1);   drawBall(-1, 1, 1);   drawBall(1, -1, 1);
            //    drawBall(1, 1, -1);  drawBall(-1, -1, 1);  drawBall(-1, 1, -1);
            //    drawBall(1, -1, -1); drawBall(-1, -1, -1);

            //    mvMatrix.load( rotMat );
            //    mvMatrix.translate(0, 0, transl);
            //    gl.uniformMatrix4fv( mvMatLoc, false,
            //      new Float32Array(mvMatrix.getAsArray()) );

            //    gl.enable(gl.BLEND);
            //    gl.blendFunc(gl.SRC_ALPHA, gl.ONE_MINUS_SRC_ALPHA);
            //    gl.uniform4f( colorLoc, .0, .0, .9, .7 );
            //    gl.depthMask(false);
            //    gl.drawElements(gl.TRIANGLES, 36, gl.UNSIGNED_SHORT, 0);
            //    gl.depthMask(true);
            //    gl.disable(gl.BLEND);

            //    gl.useProgram(line_prog);
            //    gl.uniformMatrix4fv( mvMatLineLoc, false,
            //      new Float32Array(mvMatrix.getAsArray()) );
            //    gl.drawArrays(gl.LINES, 0, 24);

            //    gl.flush ();
            //  }
            #endregion

            #region drawBall
            //  function drawBall(x, y, z){
            //    mvMatrix.makeIdentity();
            //    mvMatrix.translate(x, y, z);
            //    mvMatrix.multRight( rotMat );
            //    mvMatrix.translate(0, 0, transl);
            //    gl.uniformMatrix4fv( mvMatLoc, false,
            //      new Float32Array(mvMatrix.getAsArray()) );
            //    gl.drawElements(gl.TRIANGLES, 6*nPhi*nTheta, gl.UNSIGNED_SHORT, 72);
            //  }
            #endregion

            #region canvas.resize
            //  canvas.resize = function (){
            //    var size = Math.min(window.innerWidth, window.innerHeight) - 10;
            //    canvas.width =  size;   canvas.height = size;
            //    gl.viewport(0, 0, size, size);
            //    drawScene();
            //  }
            #endregion

            #region mouse
            //  canvas.onmousedown = function ( ev ){
            //     drag  = 1;
            //     xOffs = ev.clientX;  yOffs = ev.clientY;
            //  }
            //  canvas.onmouseup = function ( ev ){
            //     drag  = 0;
            //     xOffs = ev.clientX;  yOffs = ev.clientY;
            //  }
            //  canvas.onmousemove = function ( ev ){
            //     if ( drag == 0 ) return;
            //     if ( ev.shiftKey ) {
            //        transl *= 1 + (ev.clientY - yOffs)/1000;
            //        yRot = - xOffs + ev.clientX; }
            //     else {
            //        yRot = - xOffs + ev.clientX;  xRot = - yOffs + ev.clientY; }
            //     xOffs = ev.clientX;  yOffs = ev.clientY;
            //     drawScene();
            //  }
            #endregion

            #region wheelHandler
            //  var wheelHandler = function(ev) {
            //    var del = 1.1;
            //    if (ev.shiftKey) del = 1.01;
            //    var ds = ((ev.detail || ev.wheelDelta) > 0) ? del : (1 / del);
            //    transl *= ds;
            //    drawScene();
            //    ev.preventDefault();
            //  };
            #endregion
            //  canvas.addEventListener('DOMMouseScroll', wheelHandler, false);
            //  canvas.addEventListener('mousewheel', wheelHandler, false);

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
