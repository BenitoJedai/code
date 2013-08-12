using System;
using System.Collections.Generic;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;
using WebGLTunnel.HTML.Pages;
using WebGLTunnel.Library;
using WebGLTunnel.References;
using WebGLTunnel.Shaders;

namespace WebGLTunnel
{
    using gl = WebGLRenderingContext;
    using WebGLFloatArray = Float32Array;
    using WebGLUnsignedShortArray = Uint16Array;


    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault  page = null)
        {
            // view-source:http://www.rozengain.com/files/webgl/tunnel/
            // http://www.rozengain.com/blog/2010/08/10/using-webgl-glsl-shaders-to-create-a-tunnel-effect/

            #region __sylvester -> __glUtils -> InitializeContent
            new WebGLTunnel.References.__sylvester().Content.With(
               source =>
               {
                   source.onload +=
                       delegate
                       {
                           new WebGLTunnel.References.__glUtils().Content.With(
                               source2 =>
                               {
                                   source2.onload +=
                                       delegate
                                       {
                                           InitializeContent(page);


                                       };
                               }
                            ).AttachToDocument();


                       };
               }
            ).AttachToDocument();
            #endregion


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

        public Action Dispose;

        void InitializeContent(IDefault  page = null)
        {
            var vertices = new List<float>();
            var indices = new List<ushort>();
            var colors = new List<float>();
            var uvs = new List<float>();

            var radius = 7;
            var currentRadius = radius;
            var segments = (ushort)24;
            var spacing = 2;
            var numRings = 18;
            var index = (ushort)0;
            var currentTime = 0.0f;




            #region generateGeometry
            Action generateGeometry = delegate
            {
                for (var ring = 0; ring < numRings; ring++)
                {
                    for (var segment = 0; segment < segments; segment++)
                    {
                        var degrees = (360 / segments) * segment;
                        var radians = (Math.PI / 180) * degrees;
                        var x = (float)Math.Cos(radians) * currentRadius;
                        var y = (float)Math.Sin(radians) * currentRadius;
                        var z = ring * -spacing;

                        vertices.Add(x, y, z);

                        if (segment < (segments - 1) / 2)
                        {
                            uvs.Add((1.0f / (segments)) * segment * 2, (1.0f / 4) * ring);
                        }
                        else
                        {
                            uvs.Add(2.0f - ((1.0f / (segments)) * segment * 2), (1.0f / 4) * ring);
                        }

                        var color = 1.0f - ((1.0f / (numRings - 1)) * ring);
                        colors.Add(color, color, color, 1.0f);

                        if (ring < numRings - 1)
                        {
                            if (segment < segments - 1)
                            {
                                indices.Add(index, (ushort)(index + segments + 1), (ushort)(index + segments));
                                indices.Add(index, (ushort)(index + 1), (ushort)(index + segments + 1));
                            }
                            else
                            {
                                indices.Add(index, (ushort)(index + 1), (ushort)(index + segments));
                                indices.Add(index, (ushort)(index - segments + 1), (ushort)(index + 1));
                            }
                        }

                        index++;
                    }
                    currentRadius -= radius / numRings;
                }
            };


            generateGeometry();
            #endregion


            var size = 500;

            var gl_viewportWidth = size;
            var gl_viewportHeight = size;


            #region canvas
            var canvas = new IHTMLCanvas().AttachToDocument();

            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;
            canvas.style.SetLocation(0, 0, size, size);

            canvas.width = size;
            canvas.height = size;
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

            #region drawingMode
            var drawingMode = gl.TRIANGLES;

            page.With(
                delegate
                {
                    page.triangles.onchange +=
                        delegate
                        {
                            if (drawingMode == gl.TRIANGLES)
                                drawingMode = gl.LINE_STRIP;
                            else
                                drawingMode = gl.TRIANGLES;
                        };
                }
            );
            #endregion

            var shaderProgram = gl.createProgram(
                new TunnelVertexShader(),
                new TunnelFragmentShader()
                );



         


            gl.linkProgram(shaderProgram);
            gl.useProgram(shaderProgram);

            var shaderProgram_vertexPositionAttribute = gl.getAttribLocation(shaderProgram, "aVertexPosition");
            gl.enableVertexAttribArray((uint)shaderProgram_vertexPositionAttribute);

            var shaderProgram_vertexColorAttribute = gl.getAttribLocation(shaderProgram, "aVertexColor");
            gl.enableVertexAttribArray((uint)shaderProgram_vertexColorAttribute);

            var shaderProgram_textureCoordAttribute = gl.getAttribLocation(shaderProgram, "aTextureCoord");
            gl.enableVertexAttribArray((uint)shaderProgram_textureCoordAttribute);

            var shaderProgram_pMatrixUniform = gl.getUniformLocation(shaderProgram, "uPMatrix");
            var shaderProgram_mvMatrixUniform = gl.getUniformLocation(shaderProgram, "uMVMatrix");
            var shaderProgram_fTimeUniform = gl.getUniformLocation(shaderProgram, "fTime");
            var shaderProgram_samplerUniform = gl.getUniformLocation(shaderProgram, "uSampler");



            var mvMatrix = default(Matrix);
            var mvMatrixStack = new Stack<Matrix>();
            var pMatrix = default(Matrix);

            Action mvPopMatrix = delegate
           {
               mvMatrix = mvMatrixStack.Pop();
           };

            // http://prototypejs.org/api/array/flatten
            Action setMatrixUniforms =
                delegate
                {
                    gl.uniformMatrix4fv(shaderProgram_pMatrixUniform, false, new Float32Array(pMatrix.flatten()));
                    gl.uniformMatrix4fv(shaderProgram_mvMatrixUniform, false, new Float32Array(mvMatrix.flatten()));
                };


            Action mvPushMatrix = delegate
            {
                mvMatrixStack.Push(mvMatrix.dup());
            };



            Action loadIdentity = delegate
            {
                mvMatrix = __sylvester.Matrix.I(4);
            };


            Action<Matrix> multMatrix = (m) =>
            {
                mvMatrix = mvMatrix.x(m);
            };

            Action<float[]> mvTranslate = (v) =>
            {
                var m = __sylvester.Matrix.Translation(__sylvester.Vector.create(new[] { v[0], v[1], v[2] })).ensure4x4();
                multMatrix(m);
            };


            Action<float, float[]> mvRotate = (ang, v) =>
            {
                var arad = ang * Math.PI / 180.0;
                var m = __sylvester.Matrix.Rotation(arad, __sylvester.Vector.create(new[] { v[0], v[1], v[2] })).ensure4x4();
                multMatrix(m);
            };

            Action<float, float, float, float> perspective = (fovy, aspect, znear, zfar) =>
            {
                pMatrix = __glUtils.globals.makePerspective(fovy, aspect, znear, zfar);
            };



            #region IsDisposed
            var IsDisposed = false;

            Dispose = delegate
            {
                if (IsDisposed)
                    return;

                IsDisposed = true;

                canvas.Orphanize();
            };
            #endregion


            new HTML.Images.FromAssets.texture().InvokeOnComplete(
                (texture_image) =>
                {
                    #region initTexture
                    var texture = gl.createTexture();

                    gl.bindTexture(gl.TEXTURE_2D, texture);
                    gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, 1);
                    gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, texture_image);
                    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (int)gl.NEAREST);
                    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (int)gl.NEAREST);
                    gl.bindTexture(gl.TEXTURE_2D, null);
                    #endregion


                    #region initBuffers
                    var cubeVertexPositionBuffer = gl.createBuffer();
                    gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices.ToArray()), gl.STATIC_DRAW);
                    var cubeVertexPositionBuffer_itemSize = 3;
                    var cubeVertexPositionBuffer_numItems = vertices.Count / 3;

                    var cubeVertexColorBuffer = gl.createBuffer();
                    gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer);

                    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(colors.ToArray()), gl.STATIC_DRAW);
                    var cubeVertexColorBuffer_itemSize = 4;
                    var cubeVertexColorBuffer_numItems = colors.Count / 4;

                    var cubeVertexIndexBuffer = gl.createBuffer();
                    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
                    gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(indices.ToArray()), gl.STATIC_DRAW);
                    var cubeVertexIndexBuffer_itemSize = 1;
                    var cubeVertexIndexBuffer_numItems = indices.Count;

                    var cubeVertexTextureCoordBuffer = gl.createBuffer();
                    gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexTextureCoordBuffer);
                    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(uvs.ToArray()), gl.STATIC_DRAW);
                    var cubeVertexTextureCoordBuffer_itemSize = 2;
                    var cubeVertexTextureCoordBuffer_numItems = uvs.Count / 2;
                    #endregion


                    gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);

                    gl.clearDepth(1.0f);

                    gl.enable(gl.DEPTH_TEST);
                    gl.depthFunc(gl.LEQUAL);




                    Action AtResize = delegate
                    {

                        gl_viewportWidth = Convert.ToInt32( Native.Window.Width * zoom);
                        gl_viewportHeight = Convert.ToInt32( Native.Window.Height * zoom);

                        canvas.style.SetLocation(0, 0, Native.Window.Width, Native.Window.Height);

                        canvas.width = Convert.ToInt32( Native.Window.Width * zoom);
                        canvas.height = Convert.ToInt32(Native.Window.Height * zoom);
                    };

                    #region onresize
                    Native.Window.onresize +=
                        delegate
                        {
                            AtResize();
                        };
                    #endregion

                    AtResize();


                    var rCube = 0;

                    #region drawScene
                    Action drawScene = null;

                    drawScene = delegate
                    {
                        if (IsDisposed)
                            return;

                        gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);
                        gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                        perspective(45f, (float)gl_viewportWidth / (float)gl_viewportHeight, 0.1f, 100.0f);
                        loadIdentity();

                        mvTranslate(new[] { 0.0f, 0.0f, -8.0f });

                        mvPushMatrix();
                        mvRotate(rCube, new[] { 1f, 1f, 1f });

                        gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                        gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, cubeVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                        gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexColorBuffer);
                        gl.vertexAttribPointer((uint)shaderProgram_vertexColorAttribute, cubeVertexColorBuffer_itemSize, gl.FLOAT, false, 0, 0);

                        gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexTextureCoordBuffer);
                        gl.vertexAttribPointer((uint)shaderProgram_textureCoordAttribute, cubeVertexTextureCoordBuffer_itemSize, gl.FLOAT, false, 0, 0);

                        gl.activeTexture(gl.TEXTURE0);
                        gl.bindTexture(gl.TEXTURE_2D, texture);
                        gl.uniform1i(shaderProgram_samplerUniform, 0);

                        gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
                        setMatrixUniforms();

                        currentTime = (currentTime + 0.01f);
                        gl.uniform1f(shaderProgram_fTimeUniform, currentTime);
                        gl.drawElements(drawingMode, cubeVertexIndexBuffer_numItems, gl.UNSIGNED_SHORT, 0);

                        mvPopMatrix();

                        Native.Window.requestAnimationFrame += drawScene;
                    };

                    drawScene();
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
            );





        }


        public double zoom = 1.2;

    }

}
