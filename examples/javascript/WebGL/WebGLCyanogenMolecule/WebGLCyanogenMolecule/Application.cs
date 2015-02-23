using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using WebGLCyanogenMolecule.HTML.Pages;
using WebGLCyanogenMolecule.Shaders;

namespace WebGLCyanogenMolecule
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;
    using ScriptCoreLib.Shared.BCLImplementation.GLSL;



    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /* Source: http://www.ibiblio.org/e-notes/webgl/models/ethanol.html
         * http://www.worldofmolecules.com/3D/dopamine_3d.htm
         */


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page = null)
        {

            var prMatrix = new CanvasMatrix4();

            var gl_viewportWidth = 500;
            var gl_viewportHeight = 500;

            var gl = new WebGLRenderingContext();
            var canvas = gl.canvas.AttachToDocument();
            #region AtResize
            Action AtResize =
                delegate
                {
                    gl_viewportWidth = Native.window.Width;
                    gl_viewportHeight = Native.window.Height;

                    prMatrix = new CanvasMatrix4();



                    prMatrix.perspective(45f,
                        (f)Native.window.aspect,
                        1f, 100f);

                    canvas.style.SetLocation(0, 0, gl_viewportWidth, gl_viewportHeight);

                    canvas.width = gl_viewportWidth;
                    canvas.height = gl_viewportHeight;
                };

            Native.window.onresize +=
                e =>
                {
                    AtResize();
                };
            AtResize();
            #endregion

            Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;








            var h = 1f;
            var r1 = .5f;
            var r2 = .2f;

            #region requestPointerLock
            var __pointer_x = 0;
            var __pointer_y = 0;

            canvas.onmousedown +=
                delegate
                {
                    canvas.requestPointerLock();
                };

            canvas.onmousemove +=
                e =>
                {
                    if (Native.Document.pointerLockElement == canvas)
                    {

                        __pointer_x += e.movementX;
                        __pointer_y += e.movementY;
                    }
                };

            canvas.onmouseup +=
                delegate
                {
                    //Native.Document.exitPointerLock();
                };
            #endregion



            var prog = gl.createProgram(
                new GeometryVertexShader(),
                new GeometryFragmentShader()
            );

            gl.linkProgram(prog);
            gl.useProgram(prog);

            var uniforms = prog.Uniforms(gl);

            var nPhi = 100;
            var nTheta = 50;
            var dPhi = 2 * Math.PI / nPhi;
            var dTheta = Math.PI / nTheta;

            var vertices = new IArray<float>();
            var ind = new IArray<ushort>();

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
                    vertices.push((f)(cosPhi * sinTheta));
                    vertices.push((f)(-sinPhi * sinTheta));
                    vertices.push((f)(cosTheta));
                }
            }
            for (var j = 0; j < nTheta; j++)
                for (var i = 0; i <= nPhi; i++)
                {
                    ind.push((ushort)(j * (nPhi + 1) + i));
                    ind.push((ushort)((j + 1) * (nPhi + 1) + i));
                }
            var posLocation = gl.getAttribLocation(prog, "aPos");
            gl.enableVertexAttribArray((uint)posLocation);
            var posBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, posBuffer);
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);
            gl.vertexAttribPointer((uint)posLocation, 3, gl.FLOAT, false, 0, 0);

            var indexBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indexBuffer);
            gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(ind.ToArray()),
              gl.STATIC_DRAW);

            //prMatrix.perspective(45, 1, .1, 100);
            gl.uniformMatrix4fv(gl.getUniformLocation(prog, "prMatrix"),
               false, new Float32Array(prMatrix.getAsArray()));
            var mvMatrix = new CanvasMatrix4();

            var mvMatLoc = gl.getUniformLocation(prog, "mvMatrix");


            gl.enable(gl.DEPTH_TEST);
            gl.depthFunc(gl.LEQUAL);
            gl.clearDepth(1.0f);
            gl.clearColor(0, 0, .8f, 1f);

            var xOffs = 0;
            var yOffs = 0;
            var drag = 0;
            var xRot = 0f;
            var yRot = 1f;
            var transl = -15.5f;




            #region drawScene
            Action drawScene = delegate
            {
                var rotMat = new CanvasMatrix4();
                rotMat.makeIdentity();

                #region draw
                Action<f, f, f, f, f, f, f> drawBall = (x, y, z, r, g, b, _scale) =>
                {
                    var scale = _scale * 1.4f;

                    mvMatrix.makeIdentity();
                    mvMatrix.translate(x, y, z);
                    mvMatrix.multRight(rotMat);
                    mvMatrix.translate(0, 0, transl);
                    gl.uniformMatrix4fv(mvMatLoc, false, new Float32Array(mvMatrix.getAsArray()));

                    //var colorLoc = gl.getUniformLocation(prog, "color");
                    //var scaleLoc = gl.getUniformLocation(prog, "scale");

                    uniforms.color = new __vec3(r, g, b);
                    uniforms.scale = scale;

                    //gl.uniform1f(scaleLoc, scale);
                    //gl.uniform3f(colorLoc, r, g, b);

                    for (var i = 0; i < nTheta; i++)
                        gl.drawElements(gl.TRIANGLE_STRIP, 2 * (nPhi + 1), gl.UNSIGNED_SHORT,
                          4 * (nPhi + 1) * i);
                };

                Action<f, f, f, f> drawBall_white = (x, y, z, _scale) =>
                drawBall(x, y, z, 1, 1, 1, _scale);


                Action<f, f, f, f> drawBall_red = (x, y, z, _scale) =>
                    drawBall(x, y, z, 1, 0, 0, _scale);

                Action<f, f, f, f> drawBall_blue = (x, y, z, _scale) =>
                 drawBall(x, y, z, 0, 0, 1, _scale);

                Action<f, f, f, f> drawBall_gray = (x, y, z, _scale) =>
              drawBall(x, y, z, .3f, .3f, .3f, _scale);
                #endregion

                gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);

                #region prMatrix
                gl.uniformMatrix4fv(gl.getUniformLocation(prog, "prMatrix"),
false, new Float32Array(prMatrix.getAsArray()));
                #endregion

                gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                rotMat.rotate(xRot / 3, 1, 0, 0);
                rotMat.rotate(yRot / 3, 0, 1, 0);

                //yRot = 0; 
                //xRot = 0;

                rotMat.rotate(__pointer_y * 1.0f, 1, 0, 0);
                rotMat.rotate(__pointer_x * 1.0f, 0, 1, 0);

                //__pointer_x = 0;
                //__pointer_y = 0;

                //http://en.wikipedia.org/wiki/Cyanogen
                #region C2N2
                drawBall_blue(0, -3, 0, 1f);
                drawBall_gray(0, -1, 0, 1.2f);
                drawBall_gray(0, 1, 0, 1.2f);
                drawBall_blue(0, 3, 0, 1f);
                #endregion


                //#region C6H3
                //drawBall_gray(2, -1, 0, 1.5f);

                //drawBall_gray(0, -2, 0, 1.5f);
                //drawBall_white(0, -3.5f, 0, 1f);


                //drawBall_gray(-2, -1, 0, 1.5f);

                //drawBall_gray(2, 1, 0, 1.5f);
                //drawBall_white(3 + 0.5f, 1.5f + 0.5f, 0, 1f);

                //drawBall_gray(0, 2, 0, 1.5f);
                //drawBall_white(0, 3.5f, 0, 1f);

                //drawBall_gray(-2, 1, 0, 1.5f);
                //#endregion

                //#region CH2-CH2
                //drawBall_white(6, -1 + 1, -1.5f, 1f);
                //drawBall_gray(6, -1, 0, 1.5f);
                //drawBall_white(6, -1 + 1, 1.5f, 1f);


                //drawBall_white(4, -2 - 1, -1.5f, 1f);
                //drawBall_gray(4, -2, 0, 1.5f);
                //drawBall_white(4, -2 - 1, 1.5f, 1f);
                //#endregion

                //#region NH2
                //drawBall_white(8, -2 - 1, -1.5f, 1f);
                //drawBall_blue(8, -2, 0, 1.5f);
                //drawBall_white(8, -2 - 1, 1.5f, 1f);
                //#endregion


                gl.flush();
            };
            #endregion

            #region mouse
            canvas.onmousedown += ev =>
            {
                ev.preventDefault();

                drag = 1;
                xOffs = ev.CursorX;
                yOffs = ev.CursorY;
            };

            canvas.onmouseup += ev =>
            {
                ev.preventDefault();


                drag = 0;
                xOffs = ev.CursorX;
                yOffs = ev.CursorY;
            };

            canvas.onmousemove += ev =>
            {
                if (drag == 0)
                    return;

                ev.preventDefault();

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

                    Native.Document.body.requestFullscreen();


                };
            #endregion


            var c = 0;
            Native.window.onframe += delegate
            {
                if (IsDisposed)
                    return;

                c++;

                xRot += 0.2f;
                yRot += 0.3f;


                drawScene();

            };





            //new IHTMLAnchor { "drag me to my.jsc-solutions.net" }.AttachToDocument().With(
            //    dragme =>
            //    {
            //        dragme.style.position = IStyle.PositionEnum.@fixed;
            //        dragme.style.left = "1em";
            //        dragme.style.bottom = "1em";

            //        dragme.AllowToDragAsApplicationPackage();
            //    }
            //);

        }

        public Action Dispose;
    }


}
