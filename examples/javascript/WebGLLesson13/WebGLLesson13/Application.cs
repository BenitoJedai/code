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
using WebGLLesson13.Design;
using WebGLLesson13.HTML.Pages;
using ScriptCoreLib.GLSL;
using System.ComponentModel;


namespace WebGLLesson13
{
    using f = System.Single;
    using gl = ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;

    [Description("This type will run as JavaScript.")]
    public sealed class Application
    {
        // based on http://learningwebgl.com/lessons/lesson13/index.html

        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly DefaultStyle style = new DefaultStyle();

        public Action Dispose;


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            #region await __glMatrix then do InitializeContent
            new __glMatrix().Content.With(
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


            style.Content.AttachToHead();
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

        void InitializeContent(IDefaultPage page = null)
        {

            var gl_viewportWidth = Native.Window.Width;
            var gl_viewportHeight = Native.Window.Height;

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

            if (page != null)
                page.Toolbar.Orphanize().AttachToDocument();

            if (page == null)
                page = new DefaultPage();

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
                    gl_viewportWidth = Native.Window.Width;
                    gl_viewportHeight = Native.Window.Height;

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

            #region createShader
            Func<ScriptCoreLib.GLSL.Shader, WebGLShader> createShader = (src) =>
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

            #region programs
            var programs = 
                new[]
                {
                    new 
                    { 
                        vs = (FragmentShader)new Shaders.PerFragmentLightingFragmentShader(),
                        fs = (VertexShader)new Shaders.PerFragmentLightingVertexShader()
                    },

                    new 
                    { 
                        vs = (FragmentShader)new Shaders.PerVertexLightingFragmentShader(),
                        fs = (VertexShader)new Shaders.PerVertexLightingVertexShader()
                    }
                }.Select(
                    p =>
                    {
                        var vs = createShader(p.vs);
                        var fs = createShader(p.fs);

                        if (vs == null || fs == null) throw new InvalidOperationException("shader failed");

                        var program = gl.createProgram();

                        gl.attachShader(program, vs);
                        gl.attachShader(program, fs);

                        gl.linkProgram(program);


                        var vertexPositionAttribute = gl.getAttribLocation(program, "aVertexPosition");
                        gl.enableVertexAttribArray((ulong)vertexPositionAttribute);

                        var vertexNormalAttribute = gl.getAttribLocation(program, "aVertexNormal");
                        gl.enableVertexAttribArray((ulong)vertexNormalAttribute);

                        var textureCoordAttribute = gl.getAttribLocation(program, "aTextureCoord");
                        gl.enableVertexAttribArray((ulong)textureCoordAttribute);

                        var pMatrixUniform = gl.getUniformLocation(program, "uPMatrix");
                        var mvMatrixUniform = gl.getUniformLocation(program, "uMVMatrix");
                        var nMatrixUniform = gl.getUniformLocation(program, "uNMatrix");
                        var samplerUniform = gl.getUniformLocation(program, "uSampler");
                        var useTexturesUniform = gl.getUniformLocation(program, "uUseTextures");
                        var useLightingUniform = gl.getUniformLocation(program, "uUseLighting");
                        var ambientColorUniform = gl.getUniformLocation(program, "uAmbientColor");
                        var pointLightingLocationUniform = gl.getUniformLocation(program, "uPointLightingLocation");
                        var pointLightingColorUniform = gl.getUniformLocation(program, "uPointLightingColor");

                        return new
                        {
                            program,

                            vertexPositionAttribute,
                            vertexNormalAttribute,
                            textureCoordAttribute,

                            pMatrixUniform,
                            mvMatrixUniform,
                            nMatrixUniform,
                            samplerUniform,
                            useTexturesUniform,
                            useLightingUniform,
                            ambientColorUniform,
                            pointLightingLocationUniform,
                            pointLightingColorUniform
                        };
                    }
            ).ToArray();
            #endregion



            var perVertexProgram = programs.First();
            var perFragmentProgram = programs.Last();

            var currentProgram = perVertexProgram;

            var mvMatrix = __glMatrix.mat4.create();
            var mvMatrixStack = new Stack<Float32Array>();

            var pMatrix = __glMatrix.mat4.create();

            #region mvPushMatrix
            Action mvPushMatrix = delegate
            {
                var copy = __glMatrix.mat4.create();
                __glMatrix.mat4.set(mvMatrix, copy);
                mvMatrixStack.Push(copy);
            };
            #endregion

            #region mvPopMatrix
            Action mvPopMatrix = delegate
            {
                mvMatrix = mvMatrixStack.Pop();
            };
            #endregion



            #region degToRad
            Func<float, float> degToRad = (degrees) =>
            {
                return degrees * (f)Math.PI / 180f;
            };
            #endregion


            new HTML.Images.FromAssets.crate().InvokeOnComplete(
                crate =>
                    new HTML.Images.FromAssets.moon().InvokeOnComplete(
                        moon =>
                        {
                            #region setMatrixUniforms
                            Action setMatrixUniforms =
                                delegate
                                {
                                    gl.uniformMatrix4fv(currentProgram.pMatrixUniform, false, pMatrix);
                                    gl.uniformMatrix4fv(currentProgram.mvMatrixUniform, false, mvMatrix);

                                    var normalMatrix = __glMatrix.mat3.create();
                                    __glMatrix.mat4.toInverseMat3(mvMatrix, normalMatrix);
                                    __glMatrix.mat3.transpose(normalMatrix);
                                    gl.uniformMatrix3fv(currentProgram.nMatrixUniform, false, normalMatrix);
                                };
                            #endregion

                            #region cubeVertexPositionBuffer
                            var cubeVertexPositionBuffer = gl.createBuffer();
                            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                            var vertices = new f[]
                                   {
                                       // Front face
                                       -1.0f, -1.0f,  1.0f,
                                        1.0f, -1.0f,  1.0f,
                                        1.0f,  1.0f,  1.0f,
                                       -1.0f,  1.0f,  1.0f,

                                       // Back face
                                       -1.0f, -1.0f, -1.0f,
                                       -1.0f,  1.0f, -1.0f,
                                        1.0f,  1.0f, -1.0f,
                                        1.0f, -1.0f, -1.0f,

                                       // Top face
                                       -1.0f,  1.0f, -1.0f,
                                       -1.0f,  1.0f,  1.0f,
                                        1.0f,  1.0f,  1.0f,
                                        1.0f,  1.0f, -1.0f,

                                       // Bottom face
                                       -1.0f, -1.0f, -1.0f,
                                        1.0f, -1.0f, -1.0f,
                                        1.0f, -1.0f,  1.0f,
                                       -1.0f, -1.0f,  1.0f,

                                       // Right face
                                        1.0f, -1.0f, -1.0f,
                                        1.0f,  1.0f, -1.0f,
                                        1.0f,  1.0f,  1.0f,
                                        1.0f, -1.0f,  1.0f,

                                       // Left face
                                       -1.0f, -1.0f, -1.0f,
                                       -1.0f, -1.0f,  1.0f,
                                       -1.0f,  1.0f,  1.0f,
                                       -1.0f,  1.0f, -1.0f
                                   };

                            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);
                            var cubeVertexPositionBuffer_itemSize = 3;
                            var cubeVertexPositionBuffer_numItems = 24;
                            #endregion

                            #region cubeVertexNormalBuffer
                            var cubeVertexNormalBuffer = gl.createBuffer();
                            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexNormalBuffer);
                            var vertexNormals = new f[]
                            {
                                // Front face
                                0.0f,  0.0f,  1.0f,
                                0.0f,  0.0f,  1.0f,
                                0.0f,  0.0f,  1.0f,
                                0.0f,  0.0f,  1.0f,

                                // Back face
                                0.0f,  0.0f, -1.0f,
                                0.0f,  0.0f, -1.0f,
                                0.0f,  0.0f, -1.0f,
                                0.0f,  0.0f, -1.0f,

                                // Top face
                                0.0f,  1.0f,  0.0f,
                                0.0f,  1.0f,  0.0f,
                                0.0f,  1.0f,  0.0f,
                                0.0f,  1.0f,  0.0f,

                                // Bottom face
                                0.0f, -1.0f,  0.0f,
                                0.0f, -1.0f,  0.0f,
                                0.0f, -1.0f,  0.0f,
                                0.0f, -1.0f,  0.0f,

                                // Right face
                                1.0f,  0.0f,  0.0f,
                                1.0f,  0.0f,  0.0f,
                                1.0f,  0.0f,  0.0f,
                                1.0f,  0.0f,  0.0f,

                                // Left face
                                -1.0f,  0.0f,  0.0f,
                                -1.0f,  0.0f,  0.0f,
                                -1.0f,  0.0f,  0.0f,
                                -1.0f,  0.0f,  0.0f,
                            };
                            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertexNormals), gl.STATIC_DRAW);
                            var cubeVertexNormalBuffer_itemSize = 3;
                            var cubeVertexNormalBuffer_numItems = 24;
                            #endregion

                            #region cubeVertexTextureCoordBuffer
                            var cubeVertexTextureCoordBuffer = gl.createBuffer();
                            gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexTextureCoordBuffer);
                            var textureCoords = new f[]
                                   {
                                       // Front face
                                       0.0f, 0.0f,
                                       1.0f, 0.0f,
                                       1.0f, 1.0f,
                                       0.0f, 1.0f,

                                       // Back face
                                       1.0f, 0.0f,
                                       1.0f, 1.0f,
                                       0.0f, 1.0f,
                                       0.0f, 0.0f,

                                       // Top face
                                       0.0f, 1.0f,
                                       0.0f, 0.0f,
                                       1.0f, 0.0f,
                                       1.0f, 1.0f,

                                       // Bottom face
                                       1.0f, 1.0f,
                                       0.0f, 1.0f,
                                       0.0f, 0.0f,
                                       1.0f, 0.0f,

                                       // Right face
                                       1.0f, 0.0f,
                                       1.0f, 1.0f,
                                       0.0f, 1.0f,
                                       0.0f, 0.0f,

                                       // Left face
                                       0.0f, 0.0f,
                                       1.0f, 0.0f,
                                       1.0f, 1.0f,
                                       0.0f, 1.0f,
                                   };
                            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(textureCoords), gl.STATIC_DRAW);
                            var cubeVertexTextureCoordBuffer_itemSize = 2;
                            #endregion

                            #region cubeVertexIndexBuffer
                            var cubeVertexIndexBuffer = gl.createBuffer();
                            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
                            var cubeVertexIndices = new ushort[]{
                                       0, 1, 2,      0, 2, 3,    // Front face
                                       4, 5, 6,      4, 6, 7,    // Back face
                                       8, 9, 10,     8, 10, 11,  // Top face
                                       12, 13, 14,   12, 14, 15, // Bottom face
                                       16, 17, 18,   16, 18, 19, // Right face
                                       20, 21, 22,   20, 22, 23  // Left face
                                   };
                            gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(cubeVertexIndices), gl.STATIC_DRAW);
                            var cubeVertexIndexBuffer_itemSize = 1;
                            var cubeVertexIndexBuffer_numItems = 36;
                            #endregion

                            #region moon
                            var latitudeBands = 30;
                            var longitudeBands = 30;
                            var radius = 2;

                            var vertexPositionData = new List<f>();
                            var normalData = new List<f>();
                            var textureCoordData = new List<f>();
                            for (var latNumber = 0; latNumber <= latitudeBands; latNumber++)
                            {
                                var theta = latNumber * Math.PI / latitudeBands;
                                var sinTheta = (f)Math.Sin(theta);
                                var cosTheta = (f)Math.Cos(theta);

                                for (var longNumber = 0; longNumber <= longitudeBands; longNumber++)
                                {
                                    var phi = longNumber * 2 * Math.PI / longitudeBands;
                                    var sinPhi = (f)Math.Sin(phi);
                                    var cosPhi = (f)Math.Cos(phi);

                                    var x = cosPhi * sinTheta;
                                    var y = cosTheta;
                                    var z = sinPhi * sinTheta;
                                    var u = 1 - (longNumber / longitudeBands);
                                    var v = 1 - (latNumber / latitudeBands);

                                    normalData.Add(x);
                                    normalData.Add(y);
                                    normalData.Add(z);
                                    textureCoordData.Add(u);
                                    textureCoordData.Add(v);
                                    vertexPositionData.Add(radius * x);
                                    vertexPositionData.Add(radius * y);
                                    vertexPositionData.Add(radius * z);
                                }
                            }

                            var indexData = new List<ushort>();
                            for (var latNumber = 0; latNumber < latitudeBands; latNumber++)
                            {
                                for (var longNumber = 0; longNumber < longitudeBands; longNumber++)
                                {
                                    var first = (latNumber * (longitudeBands + 1)) + longNumber;
                                    var second = first + longitudeBands + 1;
                                    indexData.Add((ushort)first);
                                    indexData.Add((ushort)second);
                                    indexData.Add((ushort)(first + 1));

                                    indexData.Add((ushort)second);
                                    indexData.Add((ushort)(second + 1));
                                    indexData.Add((ushort)(first + 1));
                                }
                            }

                            var moonVertexNormalBuffer = gl.createBuffer();
                            gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexNormalBuffer);
                            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(normalData.ToArray()), gl.STATIC_DRAW);
                            var moonVertexNormalBuffer_itemSize = 3;
                            var moonVertexNormalBuffer_numItems = normalData.Count / 3;

                            var moonVertexTextureCoordBuffer = gl.createBuffer();
                            gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexTextureCoordBuffer);
                            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(textureCoordData.ToArray()), gl.STATIC_DRAW);
                            var moonVertexTextureCoordBuffer_itemSize = 2;
                            var moonVertexTextureCoordBuffer_numItems = textureCoordData.Count / 2;

                            var moonVertexPositionBuffer = gl.createBuffer();
                            gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexPositionBuffer);
                            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertexPositionData.ToArray()), gl.STATIC_DRAW);
                            var moonVertexPositionBuffer_itemSize = 3;
                            var moonVertexPositionBuffer_numItems = vertexPositionData.Count / 3;

                            var moonVertexIndexBuffer = gl.createBuffer();
                            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, moonVertexIndexBuffer);
                            gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new Uint16Array(indexData.ToArray()), gl.STREAM_DRAW);
                            var moonVertexIndexBuffer_itemSize = 1;
                            var moonVertexIndexBuffer_numItems = indexData.Count;
                            #endregion


                            

                            #region handleLoadedTexture
                            Action<WebGLTexture, IHTMLImage> handleLoadedTexture = (texture, texture_image) =>
                            {
                                gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, 1);
                                gl.bindTexture(gl.TEXTURE_2D, texture);
                                gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, texture_image);
                                gl.texParameteri((ulong)gl.TEXTURE_2D, (ulong)gl.TEXTURE_MAG_FILTER, (long)gl.LINEAR);
                                gl.texParameteri((ulong)gl.TEXTURE_2D, (ulong)gl.TEXTURE_MIN_FILTER, (long)gl.LINEAR_MIPMAP_NEAREST);
                                gl.generateMipmap(gl.TEXTURE_2D);

                                gl.bindTexture(gl.TEXTURE_2D, null);
                            };
                            #endregion

                        


                            var moonTexture = gl.createTexture();
                            handleLoadedTexture(moonTexture, moon);


                            var crateTexture = gl.createTexture();
                            handleLoadedTexture(crateTexture, crate);




                            gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);
                            gl.enable(gl.DEPTH_TEST);

                            var moonAngle = 180f;
                            var cubeAngle = 0f;

                            var lastTime = 0L;

                            #region animate
                            Action animate = () =>
                            {
                                var timeNow = new IDate().getTime();
                                if (lastTime != 0)
                                {
                                    var elapsed = timeNow - lastTime;

                                    moonAngle += 0.05f * elapsed;
                                    cubeAngle += 0.05f * elapsed;
                                }
                                lastTime = timeNow;
                            };
                            #endregion


                            //Func<string, f> parseFloat = Convert.ToSingle;
                            Func<string, f> parseFloat = x => float.Parse(x);



                            Action drawScene = () =>
                            {
                                    gl.viewport(0, 0, gl_viewportWidth, gl_viewportHeight);
                                    gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                                    __glMatrix.mat4.perspective(45, gl_viewportWidth / gl_viewportHeight, 0.1f, 100.0f, pMatrix);

                                    #region useProgram
                                    var perFragmentLighting = page.per_fragment.@checked;
                                    if (perFragmentLighting) {
                                        currentProgram = perFragmentProgram;
                                    } else {
                                        currentProgram = perVertexProgram;
                                    }
                                    gl.useProgram(currentProgram.program);
                                    #endregion

                                    var lighting = page.lighting.@checked;

                                    #region [uniform] uUseLighting <- lighting
                                    gl.uniform1i(currentProgram.useLightingUniform, Convert.ToInt32( lighting));
                                    #endregion

                                    if (lighting)
                                    {
                                        #region [uniform] vec3 uAmbientColor <- (f ambientR, f ambientG, f ambientB)
                                        gl.uniform3f(
                                            currentProgram.ambientColorUniform,
                                            parseFloat(page.ambientR.value),
                                            parseFloat(page.ambientG.value),
                                            parseFloat(page.ambientB.value)
                                        );
                                        #endregion

                                        #region [uniform] vec3 uPointLightingLocation <- (f lightPositionX, f lightPositionY, f lightPositionZ)
                                        gl.uniform3f(
                                            currentProgram.pointLightingLocationUniform,
                                            parseFloat(page.lightPositionX.value),
                                            parseFloat(page.lightPositionY.value),
                                            parseFloat(page.lightPositionZ.value)
                                        );
                                        #endregion

                                        #region [uniform] vec3 uPointLightingColor <- (f pointR, f pointG, f pointB)
                                        gl.uniform3f(
                                            currentProgram.pointLightingColorUniform,
                                            parseFloat(page.pointR.value),
                                            parseFloat(page.pointG.value),
                                            parseFloat(page.pointB.value)
                                        );
                                        #endregion
                                    }

                                    var textures = page.textures.@checked;

                                    #region [uniform] uUseTextures <- (bool) textures
                                    gl.uniform1i(currentProgram.useTexturesUniform, Convert.ToInt32( textures));
                                    #endregion

                                //    mat4.identity(mvMatrix);

                                //    mat4.translate(mvMatrix, [0, 0, -5]);

                                //    mat4.rotate(mvMatrix, degToRad(30), [1, 0, 0]);

                                //    mvPushMatrix();
                                //    mat4.rotate(mvMatrix, degToRad(moonAngle), [0, 1, 0]);
                                //    mat4.translate(mvMatrix, [2, 0, 0]);
                                //    gl.activeTexture(gl.TEXTURE0);
                                //    gl.bindTexture(gl.TEXTURE_2D, moonTexture);
                                //    gl.uniform1i(currentProgram.samplerUniform, 0);

                                //    gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexPositionBuffer);
                                //    gl.vertexAttribPointer(currentProgram.vertexPositionAttribute, moonVertexPositionBuffer.itemSize, gl.FLOAT, false, 0, 0);

                                //    gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexTextureCoordBuffer);
                                //    gl.vertexAttribPointer(currentProgram.textureCoordAttribute, moonVertexTextureCoordBuffer.itemSize, gl.FLOAT, false, 0, 0);

                                //    gl.bindBuffer(gl.ARRAY_BUFFER, moonVertexNormalBuffer);
                                //    gl.vertexAttribPointer(currentProgram.vertexNormalAttribute, moonVertexNormalBuffer.itemSize, gl.FLOAT, false, 0, 0);

                                //    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, moonVertexIndexBuffer);
                                //    setMatrixUniforms();
                                //    gl.drawElements(gl.TRIANGLES, moonVertexIndexBuffer.numItems, gl.UNSIGNED_SHORT, 0);
                                //    mvPopMatrix();


                                //    mvPushMatrix();
                                //    mat4.rotate(mvMatrix, degToRad(cubeAngle), [0, 1, 0]);
                                //    mat4.translate(mvMatrix, [1.25, 0, 0]);
                                //    gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexPositionBuffer);
                                //    gl.vertexAttribPointer(currentProgram.vertexPositionAttribute, cubeVertexPositionBuffer.itemSize, gl.FLOAT, false, 0, 0);

                                //    gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexNormalBuffer);
                                //    gl.vertexAttribPointer(currentProgram.vertexNormalAttribute, cubeVertexNormalBuffer.itemSize, gl.FLOAT, false, 0, 0);

                                //    gl.bindBuffer(gl.ARRAY_BUFFER, cubeVertexTextureCoordBuffer);
                                //    gl.vertexAttribPointer(currentProgram.textureCoordAttribute, cubeVertexTextureCoordBuffer.itemSize, gl.FLOAT, false, 0, 0);

                                //    gl.activeTexture(gl.TEXTURE0);
                                //    gl.bindTexture(gl.TEXTURE_2D, crateTexture);
                                //    gl.uniform1i(currentProgram.samplerUniform, 0);

                                //    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, cubeVertexIndexBuffer);
                                //    setMatrixUniforms();
                                //    gl.drawElements(gl.TRIANGLES, cubeVertexIndexBuffer.numItems, gl.UNSIGNED_SHORT, 0);
                                //    mvPopMatrix();
                            };



   


    //function tick() {
    //    requestAnimFrame(tick);
    //    drawScene();
    //    animate();
    //}


    //function webGLStart() {
    //    var canvas = document.getElementById("lesson13-canvas");
    //    initGL(canvas);
    //    initShaders();
    //    initBuffers();
    //    initTextures();

    //    gl.clearColor(0.0, 0.0, 0.0, 1.0);
    //    gl.enable(gl.DEPTH_TEST);

    //    tick();
    //}























                            #region tick
                            Action tick = null;

                            tick = () =>
                            {
                                if (IsDisposed)
                                    return;


                                animate();
                                //drawScene();


                                Native.Window.requestAnimationFrame += tick;
                            };

                            tick();
                            #endregion


                        }
                    )

            );
        }

    }


}
