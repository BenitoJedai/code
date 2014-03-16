using System;
using System.Collections.Generic;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.Shared.Lambda;
using WebGLPlanetGenerator.Design;
using WebGLPlanetGenerator.HTML.Pages;
using WebGLPlanetGenerator.Shaders;

namespace WebGLPlanetGenerator
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;



    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        /* Source: https://github.com/a5huynh/planet-generator
         */

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault  page = null)
        {
            #region scripts -> InitializeContent
            new[]
            {
                new global::WebGLPlanetGenerator.Design.sylvester().Content,
                new global::WebGLPlanetGenerator.Design.glUtils().Content,
                new global::WebGLPlanetGenerator.Design.particle_terrain().Content,
                new global::WebGLPlanetGenerator.Design.planet().Content,
            }.ForEach(
                 (SourceScriptElement, i, MoveNext) =>
                 {
                     SourceScriptElement.AttachToDocument().onload +=
                         delegate
                         {
                             MoveNext();
                         };
                 }
             )(
                 delegate
                 {
                     InitializeContent(page);
                 }
             );
            #endregion

     
        }



        void InitializeContent(IDefault  page = null)
        {


            var gl_viewportWidth = 800;
            var gl_viewportHeight = 640;


            #region canvas
            var canvas = new IHTMLCanvas().AttachToDocument();

            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;
            canvas.style.SetLocation(0, 0, gl_viewportWidth, gl_viewportHeight);

            canvas.width = gl_viewportWidth;
            canvas.height = gl_viewportHeight;
            #endregion

            #region gl - Initialise WebGL


            var gl = default(gl);

            try
            {

                gl = (gl)canvas.getContext("experimental-webgl");

            }
            catch { }

            if (gl == null)
            {
                Native.window.alert("WebGL not supported");
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




            #region initShaders
            var shaderProgram = gl.createProgram(
                new GeometryVertexShader(),
                new GeometryFragmentShader()
            );
       

            gl.linkProgram(shaderProgram);
            gl.useProgram(shaderProgram);


            var vertexPositionAttribute = gl.getAttribLocation(shaderProgram, "aVertexPosition");
            gl.enableVertexAttribArray((uint)vertexPositionAttribute);

            var vertexNormalAttribute = gl.getAttribLocation(shaderProgram, "aVertexNormal");
            gl.enableVertexAttribArray((uint)vertexNormalAttribute);

            var vertexColorAttribute = gl.getAttribLocation(shaderProgram, "aVertexColor");
            gl.enableVertexAttribArray((uint)vertexColorAttribute);
            #endregion

     
            gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);
            gl.clearDepth(1.0f);
            gl.enable(gl.DEPTH_TEST);

            // missing from WebGL IDL?
            //gl.enable(gl.POLYGON_SMOOTH);

            // Enable point size
            gl.enable(0x8642);
            gl.depthFunc(gl.LEQUAL);

            #region initBuffers
            var vertexBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, vertexBuffer);

            var planet = new Planet(129);
            var planetData = planet.generate();

            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(planetData.vertices), gl.STATIC_DRAW);
            var vertexBuffer_itemSize = 3;
            var vertexBuffer_numItems = planetData.vertices.Length / 3;

            var colorBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, colorBuffer);
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(planetData.colors), gl.STATIC_DRAW);
            var colorBuffer_itemSize = 4;
            var colorBuffer_numItems = planetData.colors.Length / 4;

            var normalBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, normalBuffer);
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(planetData.normals), gl.STATIC_DRAW);
            var normalBuffer_itemSize = 3;
            var normalBuffer_numItems = planetData.normals.Length / 3;

            var indexBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indexBuffer);
            gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(planetData.indices), gl.STATIC_DRAW);
            var indexBuffer_itemSize = 1;
            var indexBuffer_numItems = planetData.indices.Length;
            #endregion


            var lastTime = 0L;
            var yRot = -180.0f;
            var ySpeed = -50.0f;

            #region glCore
            var mvMatrix = default(Matrix);
            var mvMatrixStack = new Stack<Matrix>();
            var pMatrix = default(Matrix);

            Action loadIdentity = delegate
             {
                 mvMatrix = __sylvester.Matrix.I(4);
             };

            Action<float, float, float, float> perspective = (fovy, aspect, znear, zfar) =>
             {
                 pMatrix = __glUtils.globals.makePerspective(fovy, aspect, znear, zfar);
             };

            Action<Matrix> multMatrix = (m) =>
            {
                mvMatrix = mvMatrix.x(m);
            };

            // http://prototypejs.org/api/array/flatten
            Action setMatrixUniforms =
                delegate
                {
                    var shaderProgram_pMatrixUniform = gl.getUniformLocation(shaderProgram, "uPMatrix");
                    gl.uniformMatrix4fv(shaderProgram_pMatrixUniform, false, new Float32Array(pMatrix.flatten()));
                    
                    var shaderProgram_mvMatrixUniform = gl.getUniformLocation(shaderProgram, "uMVMatrix");
                    gl.uniformMatrix4fv(shaderProgram_mvMatrixUniform, false, new Float32Array(mvMatrix.flatten()));

                    var normalMatrix = mvMatrix.inverse();
                    normalMatrix = normalMatrix.transpose();

                    var nUniform = gl.getUniformLocation(shaderProgram, "uNMatrix");
                    gl.uniformMatrix4fv(nUniform, false, new Float32Array(mvMatrix.flatten()));

                };

            Action<float, float[]> mvRotate = (ang, v) =>
            {
                var arad = ang * Math.PI / 180.0;
                var m = __sylvester.Matrix.Rotation(arad, __sylvester.Vector.create(new[] { v[0], v[1], v[2] })).ensure4x4();
                multMatrix(m);
            };

            Action<f, f, f, f, f, f, f, f, f> lookAt = (ex, ey, ez, cx, cy, cz, ux, uy, uz) =>
            {
                mvMatrix = mvMatrix.x(__glUtils.globals.makeLookAt(ex, ey, ez, cx, cy, cz, ux, uy, uz));
            };
            #endregion


            #region drawScene
            Action drawScene =
                delegate
                {
                    gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);
                    gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                    //perspective(45f, (float)gl_viewportWidth / (float)gl_viewportHeight, 0.1f, 100.0f);
                    perspective(60f, (float)gl_viewportWidth / (float)gl_viewportHeight, 0.1f, 1000.0f);
                    //perspective(60f, 800 / 640, 0.1f, 1000.0f);

                    loadIdentity();

                    lookAt(0, 0, 500, 0, 0, 0, 0, 1, 0);

                    mvRotate(yRot, new[] { 0f, 1f, 0f });

                    gl.bindBuffer(gl.ARRAY_BUFFER, vertexBuffer);
                    gl.vertexAttribPointer((uint)vertexPositionAttribute, vertexBuffer_itemSize, gl.FLOAT, false, 0, 0);

                    gl.bindBuffer(gl.ARRAY_BUFFER, normalBuffer);
                    gl.vertexAttribPointer((uint)vertexNormalAttribute, normalBuffer_itemSize, gl.FLOAT, false, 0, 0);

                    gl.bindBuffer(gl.ARRAY_BUFFER, colorBuffer);
                    gl.vertexAttribPointer((uint)vertexColorAttribute, colorBuffer_itemSize, gl.FLOAT, false, 0, 0);

                    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indexBuffer);
                    setMatrixUniforms();
                    gl.drawElements(gl.TRIANGLES, indexBuffer_numItems, gl.UNSIGNED_SHORT, 0);
                    //gl.drawArrays(gl.POINTS, 0, vertexBuffer.numItems);
                };
            #endregion

            #region animate
            Action animate = () =>
            {
                var timeNow = new IDate().getTime();
                if (lastTime != 0)
                {
                    var elapsed = timeNow - lastTime;
                    yRot += (ySpeed * elapsed) / 1000.0f;
                }
                lastTime = timeNow;
            };
            #endregion


            #region tick
            Action tick = null;
            
            tick = () =>
            {
                drawScene();
                animate();

                Native.window.requestAnimationFrame += tick;
            };

            Native.window.requestAnimationFrame += tick;
            #endregion

            #region AtResize
            Action AtResize = delegate
            {
                gl_viewportWidth = Native.window.Width;
                gl_viewportHeight = Native.window.Height;

                canvas.style.SetLocation(0, 0, Native.window.Width, Native.window.Height);

                canvas.width = Native.window.Width;
                canvas.height = Native.window.Height;
            };

            #region onresize
            Native.window.onresize +=
                delegate
                {
                    AtResize();
                };
            #endregion

            AtResize();
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

    }


}
