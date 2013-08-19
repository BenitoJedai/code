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
using WebGLEthanolMolecule.HTML.Pages;
using WebGLEthanolMolecule.Shaders;

namespace WebGLEthanolMolecule
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;



    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        /* Source: http://www.ibiblio.org/e-notes/webgl/models/ethanol.html
         * 
         */

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page = null)
        {
            var gl_viewportWidth = 500;
            var gl_viewportHeight = 500;




            var gl = new WebGLRenderingContext();

            var canvas = gl.canvas.AttachToDocument();

            Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;
            canvas.style.SetLocation(0, 0, gl_viewportWidth, gl_viewportHeight);

            canvas.width = gl_viewportWidth;
            canvas.height = gl_viewportHeight;




            var h = 1f;
            var r1 = .5f;
            var r2 = .2f;


            var prog = gl.createProgram();


            #region createShader
            Func<ScriptCoreLib.GLSL.Shader, WebGLShader> createShader = (src) =>
            {
                var shader = gl.createShader(src);

                // verify
                if (gl.getShaderParameter(shader, gl.COMPILE_STATUS) == null)
                {
                    Native.window.alert("error in SHADER:\n" + gl.getShaderInfoLog(shader));
                    throw new InvalidOperationException("shader failed");
                }

                return shader;
            };
            #endregion

            var vs = createShader(new GeometryVertexShader());
            var fs = createShader(new GeometryFragmentShader());


            gl.attachShader(prog, vs);
            gl.attachShader(prog, fs);


            gl.linkProgram(prog);
            gl.useProgram(prog);

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

            var prMatrix = new CanvasMatrix4();
            //prMatrix.perspective(45, 1, .1, 100);
            gl.uniformMatrix4fv(gl.getUniformLocation(prog, "prMatrix"),
               false, new Float32Array(prMatrix.getAsArray()));
            var mvMatrix = new CanvasMatrix4();
            var rotMat = new CanvasMatrix4();
            rotMat.makeIdentity();
            var mvMatLoc = gl.getUniformLocation(prog, "mvMatrix");
            var colorLoc = gl.getUniformLocation(prog, "color");
            var scaleLoc = gl.getUniformLocation(prog, "scale");

            gl.enable(gl.DEPTH_TEST);
            gl.depthFunc(gl.LEQUAL);
            gl.clearDepth(1.0f);
            gl.clearColor(0, 0, .8f, 1f);

            var xOffs = 0;
            var yOffs = 0;
            var drag = 0;
            var xRot = 0f;
            var yRot = 0f;
            var transl = -10.5f;

            #region drawBall
            Action<f, f, f, f, f, f, f> drawBall = (x, y, z, r, g, b, _scale) =>
            {
                var scale = _scale * 1f;

                mvMatrix.makeIdentity();
                mvMatrix.translate(x, y, z);
                mvMatrix.multRight(rotMat);
                mvMatrix.translate(0, 0, transl);
                gl.uniformMatrix4fv(mvMatLoc, false, new Float32Array(mvMatrix.getAsArray()));
                gl.uniform1f(scaleLoc, scale);
                gl.uniform3f(colorLoc, r, g, b);
                for (var i = 0; i < nTheta; i++)
                    gl.drawElements(gl.TRIANGLE_STRIP, 2 * (nPhi + 1), gl.UNSIGNED_SHORT,
                      4 * (nPhi + 1) * i);
            };
            #endregion

            Action<f, f, f, f> drawBall_white = (x, y, z, _scale) =>
                drawBall(x, y, z, 1, 1, 1, _scale);


            Action<f, f, f, f> drawBall_red = (x, y, z, _scale) =>
                drawBall(x, y, z, 1, 0, 0, _scale);


            #region drawScene
            Action drawScene = delegate
            {
                gl.viewport(0, 0, gl_viewportWidth, gl_viewportWidth);

                #region prMatrix
                gl.uniformMatrix4fv(gl.getUniformLocation(prog, "prMatrix"),
false, new Float32Array(prMatrix.getAsArray()));
                #endregion

                gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);
                rotMat.rotate(xRot / 3, 1, 0, 0); rotMat.rotate(yRot / 3, 0, 1, 0);
                yRot = 0; xRot = 0;
                drawBall(0, 0, 0, .3f, .3f, .3f, 1.5f);
                drawBall(1, 1, 1, .3f, .3f, .3f, 1.5f);

                drawBall_white(2, 2, 0, 1);
                drawBall_white(2, 0, 2, 1);
                drawBall_white(0, 2, 2, 1);
                drawBall_white(-1, -1, 1, 1);
                drawBall_white(1, -1, -1, 1);

                drawBall_red(-1, 1, -1, 1.5f);

                drawBall_white(-2, 0, -2, 1);
                gl.flush();
            };
            #endregion

            #region mouse
            canvas.onmousedown += ev =>
            {
                ev.PreventDefault();

                drag = 1;
                xOffs = ev.CursorX;
                yOffs = ev.CursorY;
            };

            canvas.onmouseup += ev =>
            {
                ev.PreventDefault();


                drag = 0;
                xOffs = ev.CursorX;
                yOffs = ev.CursorY;
            };

            canvas.onmousemove += ev =>
            {
                if (drag == 0)
                    return;
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




            #region AtResize
            Action AtResize =
                delegate
                {
                    gl_viewportWidth = Native.window.Width;
                    gl_viewportHeight = Native.window.Height;

                    prMatrix = new CanvasMatrix4();
                    prMatrix.perspective(45f, (f)gl_viewportWidth / (f)gl_viewportHeight, 1f, 100f);

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



        }

        public Action Dispose;
    }


}
