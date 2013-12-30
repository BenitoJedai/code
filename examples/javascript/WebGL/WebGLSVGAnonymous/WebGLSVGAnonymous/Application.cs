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
using WebGLSVGAnonymous.HTML.Pages;
using WebGLSVGAnonymous.Shaders;

namespace WebGLSVGAnonymous
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;



    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /* This example will be a port of http://learningwebgl.com/blog/?p=370 by Giles
         * 
         * 01. Created a new project of type Web Application
         * 02. initGL
         * 03. initShaders
         */

        public Action Dispose;
        public IHTMLCanvas canvas;

        public Application(IDefault page = null)
        {
            var size = 500;

            var gl = new WebGLRenderingContext(antialias: true, preserveDrawingBuffer: true);

            this.canvas = gl.canvas.AttachToDocument();

            canvas.style.backgroundColor = "black";
            //canvas.style.backgroundColor = "blue";

            Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;
            canvas.style.SetLocation(0, 0, size, size);

            canvas.width = size;
            canvas.height = size;

            var gl_viewportWidth = size;
            var gl_viewportHeight = size;


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

            #region AtResize
            Action AtResize =
                delegate
                {
                    gl_viewportWidth = Native.window.Width;
                    gl_viewportHeight = Native.window.Height;

                    canvas.style.SetLocation(0, 0, gl_viewportWidth, gl_viewportHeight);

                    canvas.width = gl_viewportWidth;
                    canvas.height = gl_viewportHeight;
                };

            if (page != null)
                Native.window.onresize +=
                    e =>
                    {
                        AtResize();
                    };
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





            #region initShaders
            var shaderProgram = gl.createProgram(
                new GeometryVertexShader(),
                new GeometryFragmentShader()
            );


            gl.linkProgram(shaderProgram);
            gl.useProgram(shaderProgram);

            var shaderProgram_vertexPositionAttribute = gl.getAttribLocation(shaderProgram, "aVertexPosition");
            gl.enableVertexAttribArray((uint)shaderProgram_vertexPositionAttribute);

            var shaderProgram_textureCoordAttribute = gl.getAttribLocation(shaderProgram, "aTextureCoord");
            gl.enableVertexAttribArray((uint)shaderProgram_textureCoordAttribute);

            var shaderProgram_pMatrixUniform = gl.getUniformLocation(shaderProgram, "uPMatrix");
            var shaderProgram_mvMatrixUniform = gl.getUniformLocation(shaderProgram, "uMVMatrix");

            // new in lesson 05
            var shaderProgram_samplerUniform = gl.getUniformLocation(shaderProgram, "uSampler");
            #endregion



            var mvMatrix = glMatrix.mat4.create();
            var mvMatrixStack = new Stack<Float32Array>();

            var pMatrix = glMatrix.mat4.create();

            #region new in lesson 03
            Action mvPushMatrix = delegate
            {
                var copy = glMatrix.mat4.create();
                glMatrix.mat4.set(mvMatrix, copy);
                mvMatrixStack.Push(copy);
            };

            Action mvPopMatrix = delegate
            {
                mvMatrix = mvMatrixStack.Pop();
            };
            #endregion


            #region setMatrixUniforms
            Action setMatrixUniforms =
                delegate
                {
                    gl.uniformMatrix4fv(shaderProgram_pMatrixUniform, false, pMatrix);
                    gl.uniformMatrix4fv(shaderProgram_mvMatrixUniform, false, mvMatrix);
                };
            #endregion




            #region init buffers


            #region cubeVertexPositionBuffer
            var cubeVertexPositionBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
            var vertices = new[]{

                // Front face RED
                -1.0f, -1.0f,  1.0f,
                 1.0f, -1.0f,  1.0f,
                 1.0f,  1.0f,  1.0f,
                -1.0f,  1.0f,  1.0f,

                //// Back face YELLOW
                //-1.0f, -1.0f, -1.0f,
                //-1.0f,  1.0f, -1.0f,
                // 1.0f,  1.0f, -1.0f,
                // 1.0f, -1.0f, -1.0f,

                //// Top face GREEN
                //-1.0f,  1.0f, -1.0f,
                //-1.0f,  1.0f,  1.0f,
                // 1.0f,  1.0f,  1.0f,
                // 1.0f,  1.0f, -1.0f,

                //// Bottom face BEIGE
                //-1.0f, -1.0f, -1.0f,
                // 1.0f, -1.0f, -1.0f,
                // 1.0f, -1.0f,  1.0f,
                //-1.0f, -1.0f,  1.0f,

                //// Right face PURPLE
                // 1.0f, -1.0f, -1.0f,
                // 1.0f,  1.0f, -1.0f,
                // 1.0f,  1.0f,  1.0f,
                // 1.0f, -1.0f,  1.0f,

                //// Left face
                //-1.0f, -1.0f, -1.0f,
                //-1.0f, -1.0f,  1.0f,
                //-1.0f,  1.0f,  1.0f,
                //-1.0f,  1.0f, -1.0f
            };
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);

            var cubeVertexPositionBuffer_itemSize = 3;
            //var cubeVertexPositionBuffer_numItems = 6 * 6;
            var cubeVertexPositionBuffer_numItems = 6 * 1;
            #endregion

            #region cubeVertexTextureCoordBuffer

            var cubeVertexTextureCoordBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexTextureCoordBuffer);
            var textureCoords = new float[]{
                  // Front face
                  0.0f, 0.0f,
                  1.0f, 0.0f,
                  1.0f, 1.0f,
                  0.0f, 1.0f,

                  //// Back face
                  //1.0f, 0.0f,
                  //1.0f, 1.0f,
                  //0.0f, 1.0f,
                  //0.0f, 0.0f,

                  //// Top face
                  //0.0f, 1.0f,
                  //0.0f, 0.0f,
                  //1.0f, 0.0f,
                  //1.0f, 1.0f,

                  //// Bottom face
                  //1.0f, 1.0f,
                  //0.0f, 1.0f,
                  //0.0f, 0.0f,
                  //1.0f, 0.0f,

                  //// Right face
                  //1.0f, 0.0f,
                  //1.0f, 1.0f,
                  //0.0f, 1.0f,
                  //0.0f, 0.0f,

                  //// Left face
                  //0.0f, 0.0f,
                  //1.0f, 0.0f,
                  //1.0f, 1.0f,
                  //0.0f, 1.0f,
            };
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(textureCoords), gl.STATIC_DRAW);
            var cubeVertexTextureCoordBuffer_itemSize = 2;
            var cubeVertexTextureCoordBuffer_numItems = 24;

            var cubeVertexIndexBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
            var cubeVertexIndices = new UInt16[]{
                0, 1, 2,      0, 2, 3,    // Front face
                4, 5, 6,      4, 6, 7,    // Back face
                8, 9, 10,     8, 10, 11,  // Top face
                12, 13, 14,   12, 14, 15, // Bottom face
                16, 17, 18,   16, 18, 19, // Right face
                20, 21, 22,   20, 22, 23  // Left face
            };

            gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(cubeVertexIndices), gl.STATIC_DRAW);
            var cubeVertexIndexBuffer_itemSize = 1;
            var cubeVertexIndexBuffer_numItems = cubeVertexPositionBuffer_numItems;

            #endregion

            #endregion

            var tex1 = gl.createTexture();
            var tex1i = new WebGLSVGAnonymous.HTML.Images.FromAssets.Anonymous_LogosSingleNoWings();
            //var tex1i = new WebGLSVGAnonymous.HTML.Images.FromAssets.nehe();
            // WebGL: drawElements: texture bound to texture unit 0 is not renderable. It maybe non-power-of-2 and have incompatible texture filtering or is not 'texture complete'. Or the texture is Float or Half Float type with linear filtering while OES_float_linear or OES_half_float_linear extension is not enabled. 
            tex1i.width = 1024 * 2;
            tex1i.height = 1024 * 2;




            // initTexture new in lesson 05
            var tex0 = gl.createTexture();
            var tex0i = new WebGLSVGAnonymous.HTML.Images.FromAssets.Anonymous_LogosSingleWings();
            //var tex0i = new WebGLSVGAnonymous.HTML.Images.FromAssets.nehe();
            // WebGL: drawElements: texture bound to texture unit 0 is not renderable. It maybe non-power-of-2 and have incompatible texture filtering or is not 'texture complete'. Or the texture is Float or Half Float type with linear filtering while OES_float_linear or OES_half_float_linear extension is not enabled. 
            tex0i.width = 1024 * 2;
            tex0i.height = 1024 * 2;




            tex1i.InvokeOnComplete(
                delegate
                {
                    tex0i.InvokeOnComplete(
                        delegate
                        {
                            // this is a workaround
                            // chrome has a bug where svg textures are merged..
                            var tex1ii = new CanvasRenderingContext2D(1024 * 2, 1024 * 2);

                            tex1ii.drawImage(
                                tex1i, 0, 0, 1024 * 2, 1024 * 2);

                            {
                                gl.activeTexture(gl.TEXTURE1);
                                gl.bindTexture(gl.TEXTURE_2D, tex1);
                                gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, 1);
                                gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, tex1ii.canvas);
                                gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (int)gl.NEAREST);
                                gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (int)gl.NEAREST);
                                gl.generateMipmap(gl.TEXTURE_2D);

                                gl.bindTexture(gl.TEXTURE_2D, null);
                            }


                            {
                                gl.activeTexture(gl.TEXTURE0);
                                gl.bindTexture(gl.TEXTURE_2D, tex0);
                                gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, 1);
                                // http://msdn.microsoft.com/en-us/library/ie/dn302435(v=vs.85).aspx
                                gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, tex0i);
                                gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (int)gl.NEAREST);
                                gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (int)gl.NEAREST);
                                gl.generateMipmap(gl.TEXTURE_2D);
                                gl.bindTexture(gl.TEXTURE_2D, null);
                            }



                            //gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);
                            //gl.enable(gl.DEPTH_TEST);
                            gl.enable(gl.BLEND);
                            //gl.enable(gl.CULL_FACE);

                            // http://stackoverflow.com/questions/11521035/blending-with-html-background-in-webgl
                            gl.blendFuncSeparate(gl.SRC_ALPHA, gl.ONE_MINUS_SRC_ALPHA, gl.ONE, gl.ONE_MINUS_SRC_ALPHA);





                            var xRot = 0.0f;
                            var yRot = 0.0f;
                            var zRot = 0.0f;
                            var lastTime = 0L;
                            Action animate = delegate
                            {
                                var timeNow = new IDate().getTime();
                                if (lastTime != 0)
                                {
                                    var elapsed = timeNow - lastTime;

                                    //xRot += (9 * elapsed) / 1000.0f;
                                    yRot += (40 * elapsed) / 1000.0f;
                                    //zRot += (9 * elapsed) / 1000.0f;
                                }
                                lastTime = timeNow;
                            };

                            Func<float, float> degToRad = (degrees) =>
                            {
                                return degrees * (f)Math.PI / 180f;
                            };


                            var f = new Designer();

                            f.trackBar1.Value = -20;
                            f.trackBar2.Value = -10;


                            if (page != null)
                                f.Show();

                            Action<bool> drawScene = Anonymous_LogosSingleNoWings_Checked =>
                            {




                                glMatrix.mat4.perspective(45f, (float)gl_viewportWidth / (float)gl_viewportHeight, 0.1f, 100.0f, pMatrix);
                                glMatrix.mat4.identity(mvMatrix);

                                var u1 = f.trackBar1.Value * 0.1f;
                                glMatrix.mat4.translate(mvMatrix, new float[] { 0.0f, 0.0f, u1 });

                                //glMatrix.mat4.rotate(mvMatrix, degToRad(xRot), new[] { 1f, 0f, 0f });

                                if (Anonymous_LogosSingleNoWings_Checked)
                                    glMatrix.mat4.rotate(mvMatrix, degToRad(yRot), new[] { 0f, 1f, 0f });
                                else
                                    glMatrix.mat4.rotate(mvMatrix, degToRad(f.trackBar3.Value), new[] { 0f, 1f, 0f });

                                var u2 = f.trackBar2.Value * 0.1f;
                                glMatrix.mat4.translate(mvMatrix, new float[] { 0.0f, 0.0f, u2 });

                                Native.document.title = new { u1, u2 }.ToString();

                                //glMatrix.mat4.rotate(mvMatrix, degToRad(zRot), new[] { 0f, 0f, 1f });


                                gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                                gl.vertexAttribPointer((uint)shaderProgram_vertexPositionAttribute, cubeVertexPositionBuffer_itemSize, gl.FLOAT, false, 0, 0);

                                gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexTextureCoordBuffer);
                                gl.vertexAttribPointer((uint)shaderProgram_textureCoordAttribute, cubeVertexTextureCoordBuffer_itemSize, gl.FLOAT, false, 0, 0);



                                if (Anonymous_LogosSingleNoWings_Checked)
                                {
                                    gl.activeTexture(gl.TEXTURE0);

                                    gl.bindTexture(gl.TEXTURE_2D, tex0);
                                    gl.uniform1i(shaderProgram_samplerUniform, 0);
                                }
                                else
                                {
                                    gl.activeTexture(gl.TEXTURE0);

                                    gl.bindTexture(gl.TEXTURE_2D, tex1);
                                    gl.uniform1i(shaderProgram_samplerUniform, 0);
                                }



                                gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
                                setMatrixUniforms();
                                gl.drawElements(gl.TRIANGLES, cubeVertexIndexBuffer_numItems, gl.UNSIGNED_SHORT, 0);

                                gl.bindTexture(gl.TEXTURE_2D, null);


                            };


                            var c = 0;


                            Native.window.onframe += delegate
                            {
                                c++;

                                if (page == null)
                                {
                                    gl_viewportWidth = canvas.clientWidth;
                                    gl_viewportHeight = canvas.clientHeight;

                                    canvas.style.SetLocation(0, 0, gl_viewportWidth, gl_viewportHeight);

                                    canvas.width = gl_viewportWidth;
                                    canvas.height = gl_viewportHeight;
                                }
                                gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);
                                gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);


                                if (f.Anonymous_LogosSingleNoWings.Checked)
                                    drawScene(false);

                                if (f.Anonymous_LogosSingleWings.Checked)
                                    drawScene(true);

                                animate();

                            };



                        }
                    );
                }
            );

        }

    }


}
