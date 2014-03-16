using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.GLSL;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.Shared.Drawing;
using WebGLSimpleCubic.HTML.Pages;
using WebGLSimpleCubic.Shaders;

namespace WebGLSimpleCubic
{
    using f = Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using WebGLFloatArray = ScriptCoreLib.JavaScript.WebGL.Float32Array;
    using WebGLUnsignedShortArray = ScriptCoreLib.JavaScript.WebGL.Uint16Array;


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
        // 06. Rename Math.cos to Math.Cos
        // 07. Make use of array.push
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
        public Application(IDefault page = null)
        {
            var gl_viewportWidth = Native.window.Width;
            var gl_viewportHeight = Native.window.Height;




            var gl = new WebGLRenderingContext();


            var canvas = gl.canvas.AttachToDocument();

            Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;
            canvas.style.SetLocation(0, 0, gl_viewportWidth, gl_viewportWidth);


            gl.viewport(0, 0, gl_viewportWidth, gl_viewportWidth);


            var prog = gl.createProgram(
                new CubicVertexShader(),
                new CubicFragmentShader()
                );


            var posLoc = 0U;
            gl.bindAttribLocation(prog, posLoc, "aPos");
            var normLoc = 1U;
            gl.bindAttribLocation(prog, normLoc, "aNorm");
            gl.linkProgram(prog);
            gl.useProgram(prog);

            #region data
            var a = 1.0f; // where is it used? what shall be the type?
            var pt0 = new float[] {-a,-a,a, a,-a,a, -a,a,a, a,a,a,  // cubic
                 -a,a,a, a,a,a, -a,a,-a, a,a,-a,
                 -a,a,-a, a,a,-a, -a,-a,-a, a,-a,-a,  -a,-a,-a, a,-a,-a, -a,-a,a, a,-a,a,
                 a,a,a, a,a,-a, a,-a,a, a,-a,-a,  -a,a,a, -a,a,-a, -a,-a,a, -a,-a,-a};
            var nt = new float[] {0,0,1, 0,0,1, 0,0,1, 0,0,1,  0,1,0, 0,1,0, 0,1,0, 0,1,0,
                 0,0,-1, 0,0,-1, 0,0,-1, 0,0,-1,  0,-1,0, 0,-1,0, 0,-1,0, 0,-1,0,
                 1,0,0, 1,0,0, 1,0,0, 1,0,0,  -1,0,0, -1,0,0, -1,0,0, -1,0,0};
            var ind = new ushort[] {0,1,2,1,2,3, 4,5,6,5,6,7, 8,9,10,9,10,11,
                 12,13,14,13,14,15, 16,17,18,17,18,19, 20,21,22,21,22,23};

            var nPhi = 25;
            var nTheta = 12;
            var r = .15;
            var dPhi = 2.0 * Math.PI / nPhi;
            var dTheta = Math.PI / nTheta;

            for (var j = 0; j <= nTheta; j++)
            {
                var Theta = j * dTheta;
                var cosTheta = Math.Cos(Theta);
                var sinTheta = Math.Sin(Theta);
                for (var i = 0; i <= nPhi; i++)
                {
                    var Phi = i * dPhi;
                    var cosPhi = Math.Cos(Phi);
                    var sinPhi = Math.Sin(Phi);


                    ((IArray<float>)(object)pt0).push((float)(r * cosPhi * sinTheta));
                    ((IArray<float>)(object)pt0).push((float)(-r * sinPhi * sinTheta));
                    ((IArray<float>)(object)pt0).push((float)(r * cosTheta));

                    ((IArray<float>)(object)nt).push((float)(cosPhi * sinTheta));
                    ((IArray<float>)(object)nt).push((float)(-sinPhi * sinTheta));
                    ((IArray<float>)(object)nt).push((float)(cosTheta));
                }
            }
            var n1 = nPhi + 1;
            var off = 24;
            for (var i = 0; i < nTheta; i++)
                for (var j = 0; j < nPhi; j++)
                {
                    ((IArray<int>)(object)ind).push(i * n1 + j + off);
                    ((IArray<int>)(object)ind).push((i + 1) * n1 + j + 1 + off);
                    ((IArray<int>)(object)ind).push(i * n1 + j + 1 + off);
                    ((IArray<int>)(object)ind).push(i * n1 + j + off);
                    ((IArray<int>)(object)ind).push((i + 1) * n1 + j + off);
                    ((IArray<int>)(object)ind).push((i + 1) * n1 + j + 1 + off);
                }
            #endregion

            gl.enableVertexAttribArray(posLoc);
            gl.bindBuffer(gl.ARRAY_BUFFER, gl.createBuffer());
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(pt0), gl.STATIC_DRAW);
            gl.vertexAttribPointer(posLoc, 3, gl.FLOAT, false, 0, 0);

            gl.enableVertexAttribArray(normLoc);
            gl.bindBuffer(gl.ARRAY_BUFFER, gl.createBuffer());
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(nt), gl.STATIC_DRAW);
            gl.vertexAttribPointer(normLoc, 3, gl.FLOAT, false, 0, 0);

            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, gl.createBuffer());
            gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(ind),
              gl.STATIC_DRAW);

            var prMatrix = new CanvasMatrix4();
            prMatrix.perspective(45f, 1f, .1f, 100f);

            gl.uniformMatrix4fv(gl.getUniformLocation(prog, "prMatrix"),
                  false, new Float32Array(prMatrix.getAsArray()));

            var mvMatrix = new CanvasMatrix4();
            var rotMat = new CanvasMatrix4();
            rotMat.makeIdentity();
            rotMat.rotate(25, 1, 1, 0);

            var mvMatLoc = gl.getUniformLocation(prog, "mvMatrix");
            var colorLoc = gl.getUniformLocation(prog, "u_color");

            var line_prog = gl.createProgram(
                new LineVertexShader(),
                new LineFragmentShader()
                );


            var lineLoc = 2U;
            gl.bindAttribLocation(line_prog, lineLoc, "aPos");

            gl.linkProgram(line_prog);
            gl.useProgram(line_prog);
            gl.uniformMatrix4fv(gl.getUniformLocation(line_prog, "prMatrix"),
               false, new Float32Array(prMatrix.getAsArray()));
            var mvMatLineLoc = gl.getUniformLocation(line_prog, "mvMatrix");

            var pt1 = new float[]{2,1,1, -2,1,1, 2,-1,1, -2,-1,1, 2,1,-1, -2,1,-1, 2,-1,-1, -2,-1,-1,
                1,2,1, 1,-2,1, 1,2,-1, 1,-2,-1, -1,2,1, -1,-2,1, -1,2,-1, -1,-2,-1, 
                1,1,2, 1,1,-2, -1,1,2, -1,1,-2, 1,-1,2, 1,-1,-2, -1,-1,2, -1,-1,-2
            };

            gl.enableVertexAttribArray(lineLoc);
            gl.bindBuffer(gl.ARRAY_BUFFER, gl.createBuffer());
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(pt1), gl.STATIC_DRAW);
            gl.vertexAttribPointer(lineLoc, 3, gl.FLOAT, false, 0, 0);

            gl.enable(gl.DEPTH_TEST);
            gl.depthFunc(gl.LEQUAL);
            gl.clearDepth(1.0f);
            gl.clearColor(.5f, 1f, .5f, 1f);
            gl.lineWidth(2);

            var xOffs = 0;
            var yOffs = 0;
            var drag = 0;
            var xRot = 0f;
            var yRot = 0f;
            var transl = -6.0f;


            #region drawBall
            Action<f, f, f> drawBall = (x, y, z) =>
            {
                mvMatrix.makeIdentity();
                mvMatrix.translate(x, y, z);
                mvMatrix.multRight(rotMat);
                mvMatrix.translate(0, 0, transl);
                gl.uniformMatrix4fv(mvMatLoc, false,
                  new Float32Array(mvMatrix.getAsArray()));

                gl.drawElements(gl.TRIANGLES, 6 * nPhi * nTheta, gl.UNSIGNED_SHORT, 72);
            };
            #endregion

            #region drawScene
            Action drawScene = delegate
            {


                gl.viewport(0, 0, gl_viewportWidth, gl_viewportWidth);
                gl.useProgram(prog);


                gl.uniformMatrix4fv(gl.getUniformLocation(prog, "prMatrix"),
   false, new Float32Array(prMatrix.getAsArray()));

                rotMat.rotate(xRot / 5, 1, 0, 0);
                rotMat.rotate(yRot / 5, 0, 1, 0);

                yRot = 0;
                xRot = 0;

                gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                gl.uniform4f(colorLoc, 1, 1, 0, 1);

                drawBall(1, 1, 1); drawBall(-1, 1, 1); drawBall(1, -1, 1);
                drawBall(1, 1, -1); drawBall(-1, -1, 1); drawBall(-1, 1, -1);
                drawBall(1, -1, -1); drawBall(-1, -1, -1);

                mvMatrix.load(rotMat);
                mvMatrix.translate(0, 0, transl);

                gl.uniformMatrix4fv(mvMatLoc, false,
                  new Float32Array(mvMatrix.getAsArray()));

                gl.enable(gl.BLEND);
                gl.blendFunc(gl.SRC_ALPHA, gl.ONE_MINUS_SRC_ALPHA);
                gl.uniform4f(colorLoc, .0f, .0f, .9f, .7f);
                gl.depthMask(false);
                gl.drawElements(gl.TRIANGLES, 36, gl.UNSIGNED_SHORT, 0);
                gl.depthMask(true);
                gl.disable(gl.BLEND);



                gl.useProgram(line_prog);

                gl.uniformMatrix4fv(gl.getUniformLocation(line_prog, "prMatrix"),
   false, new Float32Array(prMatrix.getAsArray()));

                gl.uniformMatrix4fv(mvMatLineLoc, false,
                  new Float32Array(mvMatrix.getAsArray()));
                gl.drawArrays(gl.LINES, 0, 24);

                gl.flush();
            };
            #endregion


            drawScene();



            #region AtResize
            Action AtResize = delegate
            {
                gl_viewportWidth = Native.window.Width;
                gl_viewportHeight = Native.window.Height;

                prMatrix = new CanvasMatrix4();

                //var aspect = (f)gl_viewportWidth / (f)gl_viewportHeight;
                var aspect = Native.window.aspect;

                Console.WriteLine(
                    new { gl_viewportWidth, gl_viewportHeight, aspect }
                    );
                //Native.document.title = new { aspect }.ToString();

                prMatrix.perspective(45f, (f)aspect, 1f, 100f);


                canvas.style.SetLocation(0, 0, gl_viewportWidth, gl_viewportHeight);

                canvas.width = gl_viewportWidth;
                canvas.height = gl_viewportHeight;

                drawScene();
            };

            AtResize();

            Native.window.onresize += delegate
            {
                AtResize();
            };
            #endregion


            #region mouse
            canvas.onmousedown += ev =>
            {
                ev.PreventDefault();

                drag = 1;
                xOffs = ev.CursorX;
                yOffs = ev.CursorY;

                canvas.requestPointerLock();
            };

            canvas.onmouseup += ev =>
            {
                ev.PreventDefault();


                drag = 0;
                xOffs = ev.CursorX;
                yOffs = ev.CursorY;

                Native.Document.exitPointerLock();
            };

            canvas.onmousemove += ev =>
            {
                if (drag == 0)
                    return;

                if (Native.Document.pointerLockElement == canvas)
                {
                    xRot += ev.movementY;
                    yRot += ev.movementX;
                    drawScene();

                    return;
                }

                ev.PreventDefault();

                if (ev.shiftKey)
                {
                    transl *= 1 + (ev.CursorY - yOffs) / 1000;
                    yRot = -xOffs + ev.CursorX;
                }
                else
                {
                    yRot = -xOffs + ev.CursorX;
                    xRot = -yOffs + ev.CursorY;
                }

                xOffs = ev.CursorX;
                yOffs = ev.CursorY;
                drawScene();
            };
            #endregion

            #region onmousewheel
            canvas.onmousewheel +=
                ev =>
                {
                    var del = 1.1f;

                    if (ev.shiftKey)
                        del = 1.01f;

                    if (ev.WheelDirection > 0)
                        transl *= del;
                    else
                        transl *= (1 / del);

                    drawScene();



                    ev.PreventDefault();
                };
            #endregion


            #region IsDisposed
            var IsDisposed = false;

            this.Dispose = delegate
            {
                if (IsDisposed)
                    return;

                IsDisposed = true;

                canvas.Orphanize();
            };
            #endregion


            #region requestFullscreen
            Native.Document.body.ondblclick +=
                delegate
                {
                    if (IsDisposed)
                        return;

                    // http://tutorialzine.com/2012/02/enhance-your-website-fullscreen-api/

                    canvas.requestFullscreen();


                };
            #endregion

            #region tick

            Native.window.onframe += delegate
            {
                if (IsDisposed)
                    return;

                if (drag == 0)
                {
                    xRot += 2;
                    yRot += 3;
                }

                drawScene();
                //animate();

            };

            #endregion



            //Native.Document.body.style.backgroundColor = Color.FromRGB(0x80, 0xFF, 0x80);
        }

        public Action Dispose;
    }


}
