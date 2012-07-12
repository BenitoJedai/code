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
using WebGLSphereRayTrace.Design;
using WebGLSphereRayTrace.HTML.Pages;
using ScriptCoreLib.JavaScript.WebGL;
using WebGLSphereRayTrace.Shaders;

namespace WebGLSphereRayTrace
{
    using gl = WebGLRenderingContext;
    using ScriptCoreLib.GLSL;
    using System.Collections.Generic;


    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // inspired by http://people.mozilla.com/~sicking/webgl/ray.html

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page  = null)
        {
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );

            InitializeContent();
        }

        public void InitializeContent()
        {
            #region canvas 3D
            var canvas = new IHTMLCanvas();

            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;

            canvas.AttachToDocument();
            canvas.style.SetLocation(0, 0);
            canvas.style.backgroundColor = "black";

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

            #region load_shader
            Func<Shader, WebGLShader> load_shader =
                src =>
                {
                    var shader = gl.createShader(src);

                    // verify
                    if (gl.getShaderParameter(shader, gl.COMPILE_STATUS) == null)
                    {
                        Native.Window.alert("error in SHADER:\n" + gl.getShaderInfoLog(shader));
                        throw new InvalidOperationException("shader");
                    }

                    return shader;
                };
            #endregion

            var shaderProgram = default(WebGLProgram);

            #region init

            shaderProgram = gl.createProgram();

            var vs = load_shader(new RayVertexShader());
            var fs = load_shader(new RayFragmentShader());


            gl.attachShader(shaderProgram, vs);
            gl.attachShader(shaderProgram, fs);

            gl.bindAttribLocation(shaderProgram, 0, "position");

            gl.linkProgram(shaderProgram);

            if (gl.getProgramParameter(shaderProgram, gl.LINK_STATUS) == null)
            {

                Native.Window.alert("ERROR:\n" +
                "VALIDATE_STATUS: " + gl.getProgramParameter(shaderProgram, gl.VALIDATE_STATUS) + "\n" +
                "ERROR: " + gl.getError() + "\n\n"
               );

                return;
            }

            gl.useProgram(shaderProgram);
            #endregion

            var aVertexPosition = gl.getAttribLocation(shaderProgram, "aVertexPosition");
            gl.enableVertexAttribArray((uint)aVertexPosition);

            var aPlotPosition = gl.getAttribLocation(shaderProgram, "aPlotPosition");
            gl.enableVertexAttribArray((uint)aPlotPosition);

            var cameraPos = gl.getUniformLocation(shaderProgram, "cameraPos");
            var sphere1Center = gl.getUniformLocation(shaderProgram, "sphere1Center");
            var sphere2Center = gl.getUniformLocation(shaderProgram, "sphere2Center");
            var sphere3Center = gl.getUniformLocation(shaderProgram, "sphere3Center");



            gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);

            gl.clearDepth(1.0f);
            

            #region initBuffers()
            var vertexPositionBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, vertexPositionBuffer);
            var vertices = new float[]{
                1.0f,  1.0f,
                -1.0f,  1.0f,
                1.0f, -1.0f,
                -1.0f, -1.0f,
            };
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);
            gl.bindBuffer(gl.ARRAY_BUFFER, vertexPositionBuffer);
            gl.vertexAttribPointer((uint)aVertexPosition, 2, gl.FLOAT, false, 0, 0);


            var plotPositionBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, plotPositionBuffer);
            gl.vertexAttribPointer((uint)aPlotPosition, 3, gl.FLOAT, false, 0, 0);
            #endregion


            Func<xyz, xyz, xyz> crossProd = (v1, v2) =>
            {
                return new xyz
                {
                    x = v1.y * v2.z - v2.y * v1.z,
                    y = v1.z * v2.x - v2.z * v1.x,
                    z = v1.x * v2.y - v2.x * v1.y
                };
            };

            Func<xyz, xyz> normalize = (v) =>
            {
                var l = (float)Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
                return new xyz { x = v.x / l, y = v.y / l, z = v.z / l };
            };

            Func<xyz, xyz, xyz> vectAdd = (v1, v2) =>
            {
                return new xyz { x = v1.x + v2.x, y = v1.y + v2.y, z = v1.z + v2.z };
            };

            Func<xyz, xyz, xyz> vectSub = (v1, v2) =>
            {
                return new xyz { x = v1.x - v2.x, y = v1.y - v2.y, z = v1.z - v2.z };
            };

            Func<xyz, float, xyz> vectMul = (v, l) =>
            {
                return new xyz { x = v.x * l, y = v.y * l, z = v.z * l };
            };

            Action<xyz, List<float>> pushVec = (v, arr) =>
            {
                arr.Add(v.x);
                arr.Add(v.y);
                arr.Add(v.z);
            };

            var t = 0f;
            var ratio = Native.Window.Width / Native.Window.Height;

            #region drawScene
            Action drawScene = delegate
              {
                  var x1 = (float)Math.Sin(t * 1.1) * 1.5f;
                  var y1 = (float)Math.Cos(t * 1.3) * 1.5f;
                  var z1 = (float)Math.Sin(t + Math.PI / 3) * 1.5f;
                  var x2 = (float)Math.Cos(t * 1.2) * 1.5f;
                  var y2 = (float)Math.Sin(t * 1.4) * 1.5f;
                  var z2 = (float)Math.Sin(t * 1.25 - Math.PI / 3) * 1.5f;
                  var x3 = (float)Math.Cos(t * 1.15) * 1.5f;
                  var y3 = (float)Math.Sin(t * 1.37) * 1.5f;
                  var z3 = (float)Math.Sin(t * 1.27) * 1.5f;

                  var cameraFrom = new xyz
                  {
                      x = (float)Math.Sin(t * 0.4f) * 18,
                      y = (float)Math.Sin(t * 0.13f) * 5 + 5,
                      z = (float)Math.Cos(t * 0.4f) * 18
                  };

                  var cameraTo = new xyz();
                  var cameraPersp = 6;
                  var up = new xyz { x = 0, y = 1, z = 0 };

                  var cameraDir = normalize(vectSub(cameraTo, cameraFrom));

                  var cameraLeft = normalize(crossProd(cameraDir, up));
                  var cameraUp = normalize(crossProd(cameraLeft, cameraDir));
                  // cameraFrom + cameraDir * cameraPersp
                  var cameraCenter = vectAdd(cameraFrom, vectMul(cameraDir, cameraPersp));
                  // cameraCenter + cameraUp + cameraLeft * ratio
                  var cameraTopLeft = vectAdd(vectAdd(cameraCenter, cameraUp),
                                           vectMul(cameraLeft, ratio));
                  var cameraBotLeft = vectAdd(vectSub(cameraCenter, cameraUp),
                                           vectMul(cameraLeft, ratio));
                  var cameraTopRight = vectSub(vectAdd(cameraCenter, cameraUp),
                                           vectMul(cameraLeft, ratio));
                  var cameraBotRight = vectSub(vectSub(cameraCenter, cameraUp),
                                           vectMul(cameraLeft, ratio));


                  //corners = [1.2, 1, -12, -1.2, 1, -12, 1.2, -1, -12, -1.2, -1, -12];
                  var corners = new List<float>();



                  pushVec(cameraTopRight, corners);
                  pushVec(cameraTopLeft, corners);
                  pushVec(cameraBotRight, corners);
                  pushVec(cameraBotLeft, corners);

                  gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(corners.ToArray()), gl.STATIC_DRAW);

                  gl.uniform3f(cameraPos, cameraFrom.x, cameraFrom.y, cameraFrom.z);
                  gl.uniform3f(sphere1Center, x1, y1, z1);
                  gl.uniform3f(sphere2Center, x2, y2, z2);
                  gl.uniform3f(sphere3Center, x3, y3, z3);

                  gl.drawArrays(gl.TRIANGLE_STRIP, 0, 4);

                  t += 0.03f;
                  if (t > Math.PI * 200)
                  {
                      t -= (float)Math.PI * 200f;
                  }
              };
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

            #region AtResize
            Action AtResize = delegate
            {
                canvas.width = Native.Window.Width;
                canvas.height = Native.Window.Height;

                var width = canvas.width;
                var height = canvas.height;

                gl.viewport(0, 0, canvas.width, canvas.height);

                ratio = Native.Window.Width / Native.Window.Height;
            };

            AtResize();

            Native.Window.onresize += delegate
            {
                if (IsDisposed)
                    return;

                AtResize();
            };
            #endregion

            #region loop
            var start_time = new IDate().getTime();

            Action loop = null;

            loop = delegate
            {
                if (IsDisposed)
                    return;

                drawScene();

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
        public Action Dispose;

        class xyz
        {
            public float x;
            public float y;
            public float z;
        }

    }
}
