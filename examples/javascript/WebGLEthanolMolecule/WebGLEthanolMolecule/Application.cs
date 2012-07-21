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
using WebGLEthanolMolecule.Design;

namespace WebGLEthanolMolecule
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;



    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        /* Source: http://www.ibiblio.org/e-notes/webgl/gpu/make_cone.htm
         */

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page = null)
        {
            #region CanvasMatrix.js -> InitializeContent
            new CanvasMatrix().Content.With(
               source =>
               {
                   source.onload +=
                       delegate
                       {
                           InitializeContent(page);
                       };

                   source.AttachToDocument();
               }
           );
            #endregion


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

        void InitializeContent(IDefaultPage page = null)
        {


            var gl_viewportWidth = 500;
            var gl_viewportHeight = 500;

            #region canvas
            var canvas = new IHTMLCanvas().AttachToDocument();

            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;
            canvas.style.SetLocation(0, 0, gl_viewportWidth, gl_viewportHeight);

            canvas.width = gl_viewportWidth;
            canvas.height = gl_viewportHeight;
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






            var h = 1f;
            var r1 = .5f;
            var r2 = .2f;
            var nPhi = 500;


            var prog = gl.createProgram();


            #region createShader
            Func<ScriptCoreLib.GLSL.Shader, WebGLShader> createShader = (src) =>
            {
                var shader = gl.createShader(src);

                // verify
                if (gl.getShaderParameter(shader, gl.COMPILE_STATUS) == null)
                {
                    Native.Window.alert("error in SHADER:\n" + gl.getShaderInfoLog(shader));
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



            var pt = new IArray<float>();
            var nt = new IArray<float>();
            var Phi = 0.0;
            var dPhi = 2 * Math.PI / (nPhi - 1);

            var Nx = r1 - r2;
            var Ny = h;
            var N = (float)Math.Sqrt(Nx * Nx + Ny * Ny);

            Nx /= N;
            Ny /= N;

            for (var i = 0; i < nPhi; i++)
            {
                var cosPhi = Math.Cos(Phi);
                var sinPhi = Math.Sin(Phi);
                var cosPhi2 = Math.Cos(Phi + dPhi / 2);
                var sinPhi2 = Math.Sin(Phi + dPhi / 2);

                pt.push(-h / 2);
                pt.push((float)(cosPhi * r1));
                pt.push((float)(sinPhi * r1));   // points

                nt.push(Nx);
                nt.push((float)(Ny * cosPhi));
                nt.push((float)(Ny * sinPhi));         // normals

                pt.push(h / 2);
                pt.push((float)(cosPhi2 * r2));
                pt.push((float)(sinPhi2 * r2));  // points

                nt.push(Nx);
                nt.push((float)(Ny * cosPhi2));
                nt.push((float)(Ny * sinPhi2));       // normals

                Phi += dPhi;
            }

            var posLoc = gl.getAttribLocation(prog, "aPos");
            gl.enableVertexAttribArray((uint)posLoc);
            gl.bindBuffer(gl.ARRAY_BUFFER, gl.createBuffer());
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(pt.ToArray()), gl.STATIC_DRAW);
            gl.vertexAttribPointer((uint)posLoc, 3, gl.FLOAT, false, 0, 0);

            var normLoc = gl.getAttribLocation(prog, "aNorm");
            gl.enableVertexAttribArray((uint)normLoc);
            gl.bindBuffer(gl.ARRAY_BUFFER, gl.createBuffer());
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(nt), gl.STATIC_DRAW);
            gl.vertexAttribPointer((uint)normLoc, 3, gl.FLOAT, false, 0, 0);

            var prMatrix = new CanvasMatrix4();
            prMatrix.perspective(45f, 1f, .1f, 100f);



            var mvMatrix = new CanvasMatrix4();
            var rotMat = new CanvasMatrix4();
            rotMat.makeIdentity();
            rotMat.rotate(-40, 0, 1, 0);
            var mvMatLoc = gl.getUniformLocation(prog, "mvMatrix");

            gl.enable(gl.DEPTH_TEST);
            gl.depthFunc(gl.LEQUAL);
            gl.clearDepth(1.0f);
            gl.clearColor(0, 0, .5f, 1);

            var xOffs = 0;
            var yOffs = 0;
            var drag = 0;
            var xRot = 0;
            var yRot = 0;
            var transl = -1.5f;

            Action drawScene = delegate
            {


                gl.uniformMatrix4fv(gl.getUniformLocation(prog, "prMatrix"),
   false, new Float32Array(prMatrix.getAsArray()));

                gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);
                gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);


                rotMat.rotate(xRot / 5, 1, 0, 0);
                rotMat.rotate(yRot / 5, 0, 1, 0);

                yRot = 0;
                xRot = 0;

                mvMatrix.load(rotMat);
                mvMatrix.translate(0, 0, transl);

                gl.uniformMatrix4fv(mvMatLoc, false,
                  new Float32Array(mvMatrix.getAsArray()));
                gl.drawArrays(gl.TRIANGLE_STRIP, 0, 2 * nPhi);
                gl.flush();
            };



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




            #region tick
            var tick = default(Action);

            tick = delegate
            {
                if (IsDisposed)
                    return;

                c++;

                xRot += 2;
                yRot += 3;

                //Native.Document.title = "" + c;

                drawScene();
                //animate();

                Native.Window.requestAnimationFrame += tick;
            };

            tick();
            #endregion



            #region AtResize
            Action AtResize =
                delegate
                {
                    gl_viewportWidth = Native.Window.Width;
                    gl_viewportHeight = Native.Window.Height;

                    prMatrix = new CanvasMatrix4();
                    prMatrix.perspective(45f, (f)gl_viewportWidth / (f)gl_viewportHeight, 1f, 100f);

                    canvas.style.SetLocation(0, 0, gl_viewportWidth, gl_viewportHeight);

                    canvas.width = gl_viewportWidth;
                    canvas.height = gl_viewportHeight;
                };

            Native.Window.onresize +=
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
