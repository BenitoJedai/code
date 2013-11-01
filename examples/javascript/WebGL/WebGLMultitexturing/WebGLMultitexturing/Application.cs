// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Extensions;
using WebGLMultitexturing.HTML.Pages;
using WebGLMultitexturing;
using ScriptCoreLib.Avalon;
using ScriptCoreLib.JavaScript.WebGL;

namespace WebGLMultitexturing
{
    using gl = WebGLRenderingContext;
    using ScriptCoreLib.GLSL;
    using System.Collections.Generic;
    using WebGLMultitexturing.HTML.Images.FromAssets;
    using WebGLFloatArray = Float32Array;
    using WebGLUnsignedShortArray = Uint16Array;

    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class Application
    {
        int w = 256;
        int h = 256;

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page)
        {
            var c = new IHTMLCanvas();
            c.style.border = "1px solid red";

            //http://www.khronos.org/webgl/public-mailing-list/archives/1002/msg00125.html
            c.width = w;
            c.height = h;

            page.Content.Clear();
            c.AttachTo(page.Content);

            var gl = c.getContext("experimental-webgl");

            if (gl != null)
                Initialize(c, (ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext)gl, page);
        }

        private void Initialize(IHTMLCanvas c, WebGLRenderingContext gl, IDefault  page)
        {
            // http://cs.helsinki.fi/u/ilmarihe/metatunnel.html
            // http://wakaba.c3.cx/w/puls.html

            Action<string> alert = Native.window.alert;

            c.style.border = "1px solid yellow";

            page.MaxTextures.innerText = "" + gl.getParameter(gl.MAX_COMBINED_TEXTURE_IMAGE_UNITS);

            // https://www.khronos.org/webgl/public-mailing-list/archives/1007/msg00034.html

            var vs = "";

            vs += "precision highp float; \n";
            vs += "attribute vec3 aVertexPosition;";
            vs += "attribute vec2 aTextureCoord;";
            vs += "uniform mat4 uModelViewMatrix;";
            vs += "uniform mat4 uProjectionMatrix;";
            vs += "varying vec2 vTextureCoord;";
            vs += "void main(void) {";
            vs += "gl_Position = uProjectionMatrix * uModelViewMatrix * vec4(aVertexPosition, 1.0);";
            vs += "vTextureCoord = vec2(aTextureCoord.x, 1.0 - aTextureCoord.y);";
            vs += "}";

           var fs = "";
            
            fs += "precision highp float; \n";
            fs += "varying vec2 vTextureCoord;";
            fs += "uniform sampler2D uSamplerDiffuse1;";
            fs += "uniform sampler2D uSamplerDiffuse2;";
            fs += "uniform sampler2D uSamplerDiffuse3;";
            fs += "uniform sampler2D uSamplerDiffuse4;";
            fs += "uniform sampler2D uSamplerDiffuse5;";
            fs += "uniform sampler2D uSamplerDiffuse6;";
            fs += "void main(void) {";
            fs += "gl_FragColor = vec4(1.0, 0.0, 0.0, 1.0) * texture2D(uSamplerDiffuse1, vTextureCoord)";
            fs += "+ vec4(0.0, 1.0, 0.0, 1.0) * texture2D(uSamplerDiffuse2, vTextureCoord)";
            fs += "+ vec4(0.0, 0.0, 1.0, 1.0) * texture2D(uSamplerDiffuse3, vTextureCoord)";
            fs += "+ vec4(0.0, 1.0, 1.0, 1.0) * texture2D(uSamplerDiffuse4, vTextureCoord)";
            fs += "+ vec4(1.0, 0.0, 1.0, 1.0) * texture2D(uSamplerDiffuse5, vTextureCoord)";
            fs += "+ vec4(1.0, 1.0, 0.0, 1.0) * texture2D(uSamplerDiffuse6, vTextureCoord);";
            fs += "}";


            var xfs = gl.createShader(gl.FRAGMENT_SHADER);
            gl.shaderSource(xfs, fs);
            gl.compileShader(xfs);
            if ((int)gl.getShaderParameter(xfs, gl.COMPILE_STATUS) != 1)
            {
                // vs: ERROR: 0:2: '' : Version number not supported by ESSL 
                // fs: ERROR: 0:1: '' : No precision specified for (float) 

                var error = gl.getShaderInfoLog(xfs);
                Native.window.alert("fs: " + error);
                return;
            }

            var xvs = gl.createShader(gl.VERTEX_SHADER);
            gl.shaderSource(xvs, vs);
            gl.compileShader(xvs);
            if ((int)gl.getShaderParameter(xvs, gl.COMPILE_STATUS) != 1)
            {
                // vs: ERROR: 0:2: '' : Version number not supported by ESSL 
                // vs: ERROR: 0:10: '-' :  wrong operand types  no operation '-' exists that takes a left-hand operand of type 'const mediump int' and a right operand of type 'float' (or there is no acceptable conversion)


                var error = gl.getShaderInfoLog(xvs);
                Native.window.alert("vs: " + error);
                return;
            }

            var shader = new foo();

            shader.program = gl.createProgram();
            gl.attachShader(shader.program, xvs);
            gl.attachShader(shader.program, xfs);
            gl.linkProgram(shader.program);

            var linked = gl.getProgramParameter(shader.program, gl.LINK_STATUS);
            if (linked == null)
            {
                var error = gl.getProgramInfoLog(shader.program);
                Native.window.alert("Error while linking: " + error);
                return;
            }

            gl.useProgram(shader.program);
            shader.aVertexPosition = gl.getAttribLocation(shader.program, "aVertexPosition");
            shader.aTextureCoord = gl.getAttribLocation(shader.program, "aTextureCoord");
            gl.enableVertexAttribArray((uint)shader.aVertexPosition);
            gl.enableVertexAttribArray((uint)shader.aTextureCoord);

            shader.u["uModelViewMatrix"] = gl.getUniformLocation(shader.program, "uModelViewMatrix");
            shader.u["uProjectionMatrix"] = gl.getUniformLocation(shader.program, "uProjectionMatrix");

            shader.u["uSamplerDiffuse1"] = gl.getUniformLocation(shader.program, "uSamplerDiffuse1");
            shader.u["uSamplerDiffuse2"] = gl.getUniformLocation(shader.program, "uSamplerDiffuse2");
            shader.u["uSamplerDiffuse3"] = gl.getUniformLocation(shader.program, "uSamplerDiffuse3");
            shader.u["uSamplerDiffuse4"] = gl.getUniformLocation(shader.program, "uSamplerDiffuse4");
            shader.u["uSamplerDiffuse5"] = gl.getUniformLocation(shader.program, "uSamplerDiffuse5");
            shader.u["uSamplerDiffuse6"] = gl.getUniformLocation(shader.program, "uSamplerDiffuse6");

            shader.MV_MATRIX = new float[] { 
                1.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 1.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 1.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f };


            shader.PROJECTION_MATRIX = new float[] { 
                                1.000f, 0.000f, 0.000f, 0.000f,
                                0.000f, 1.000f, 0.000f, 0.000f,
                                0.000f, 0.000f, 0.002f, 0.000f,
                                0.000f, 0.000f, 0.998f, 1.000f };

            var bar = default(bar);

            bar = initObject(
                gl,
                delegate
                {
                    if (bar == null)
                        return;

                    renderFrame(gl, shader, bar);

                    c.style.border = "2px solid green";
                }
            );



        }

        void drawObject(WebGLRenderingContext gl, foo shader, bar @object)
        {
            gl.useProgram(shader.program);

            gl.bindBuffer(gl.ARRAY_BUFFER, @object.vertex_buffer);
            gl.vertexAttribPointer((uint)shader.aVertexPosition, 3, gl.FLOAT, false, 0, 0);

            gl.bindBuffer(gl.ARRAY_BUFFER, @object.texturecoord_buffer);
            gl.vertexAttribPointer((uint)shader.aTextureCoord, 2, gl.FLOAT, false, 0, 0);

            gl.activeTexture(gl.TEXTURE0);
            gl.bindTexture(gl.TEXTURE_2D, @object.t["texture1"]);
            gl.uniform1i(shader.u["uSamplerDiffuse1"], 0);

            gl.activeTexture(gl.TEXTURE1);
            gl.bindTexture(gl.TEXTURE_2D, @object.t["texture2"]);
            gl.uniform1i(shader.u["uSamplerDiffuse2"], 1);

            gl.activeTexture(gl.TEXTURE2);
            gl.bindTexture(gl.TEXTURE_2D, @object.t["texture3"]);
            gl.uniform1i(shader.u["uSamplerDiffuse3"], 2);

            gl.activeTexture(gl.TEXTURE3);
            gl.bindTexture(gl.TEXTURE_2D, @object.t["texture4"]);
            gl.uniform1i(shader.u["uSamplerDiffuse4"], 3);

            gl.activeTexture(gl.TEXTURE4);
            gl.bindTexture(gl.TEXTURE_2D, @object.t["texture5"]);
            gl.uniform1i(shader.u["uSamplerDiffuse5"], 4);

            gl.activeTexture(gl.TEXTURE5);
            gl.bindTexture(gl.TEXTURE_2D, @object.t["texture6"]);
            gl.uniform1i(shader.u["uSamplerDiffuse6"], 5);

            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, @object.index_buffer);

            gl.uniformMatrix4fv(shader.u["uProjectionMatrix"], false, shader.PROJECTION_MATRIX);
            gl.uniformMatrix4fv(shader.u["uModelViewMatrix"], false, shader.MV_MATRIX);

            gl.drawElements(gl.TRIANGLES, @object.n_elements, gl.UNSIGNED_SHORT, 0);
        }

        void renderFrame(WebGLRenderingContext gl, foo shader, bar @object)
        {
            gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);
            gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);
            drawObject(gl, shader, @object);
        }


        static WebGLTexture loadTexture(WebGLRenderingContext gl, string url, Action callback)
        {
            //gl.enable(gl.TEXTURE_2D);

            var texture = gl.createTexture();
            var image = new IHTMLImage();

            Console.WriteLine("loading: " + url);
            image.src = url;

            image.InvokeOnComplete(
                delegate
                {
                    Console.WriteLine("loaded: " + url);

                    gl.bindTexture(gl.TEXTURE_2D, texture);
                    gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, image);
                    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (int)gl.LINEAR);
                    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (int)gl.LINEAR);
                    gl.bindTexture(gl.TEXTURE_2D, null);

                    callback();
                }
            );

            return texture;
        }

        static void createRectangle(double size, data data)
        {
            var half = (float)(size / 2);

            data.vertices = new float[] {
                -half, -half, 0,
                half, -half, 0,
                half, half, 0,
                -half, half, 0
            };

            data.indices = new ushort[] {
                2,1,0,
                0,3,2
            };

            data.uvs = new float[] {
                0,0,
                1,0,
                1,1,
                0,1
            };
        }

        static bar initObject(WebGLRenderingContext gl, Action callback)
        {

            var data = new data
            {
                vertices = new float[] { },
                indices = new ushort[] { },
                uvs = new float[] { }
            };

            createRectangle(2, data);

            var @object = new bar();

            @object.vertex_buffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, @object.vertex_buffer);
            gl.bufferData(gl.ARRAY_BUFFER, new WebGLFloatArray(data.vertices), gl.STATIC_DRAW);

            @object.index_buffer = gl.createBuffer();
            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, @object.index_buffer);
            gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, new WebGLUnsignedShortArray(data.indices), gl.STATIC_DRAW);

            @object.texturecoord_buffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, @object.texturecoord_buffer);
            gl.bufferData(gl.ARRAY_BUFFER, new WebGLFloatArray(data.uvs), gl.STATIC_DRAW);

            @object.n_elements = data.indices.Length;

            @object.t["texture1"] = loadTexture(gl, new texture1().src, callback);
            @object.t["texture2"] = loadTexture(gl, new texture2().src, callback);
            @object.t["texture3"] = loadTexture(gl, new texture3().src, callback);
            @object.t["texture4"] = loadTexture(gl, new texture4().src, callback);
            @object.t["texture5"] = loadTexture(gl, new texture5().src, callback);
            @object.t["texture6"] = loadTexture(gl, new texture6().src, callback);
            return @object;
        }

        class data
        {
            public float[] vertices;
            public ushort[] indices;
            public float[] uvs;
        }

        class bar
        {
            public WebGLBuffer index_buffer;
            public WebGLBuffer vertex_buffer;
            public WebGLBuffer texturecoord_buffer;
            public int n_elements;

            public readonly Dictionary<string, WebGLTexture> t = new Dictionary<string, WebGLTexture>();
        }

        class foo
        {
            public WebGLProgram program;

            public float[] MV_MATRIX;
            public float[] PROJECTION_MATRIX;

            public long aVertexPosition;
            public long aTextureCoord;

            public readonly Dictionary<string, WebGLUniformLocation> u = new Dictionary<string, WebGLUniformLocation>();

        }


    }

    #region GLSL
    abstract class __fragment_shader : FragmentShader
    {
        [uniform]
        public float t;

        [varying]
        vec2 tc;

        const float BLOWUP = 66.0f; /* 86.0 */
        const float MAXSTEPSHIFT = 8.0f; /* 6.0 */
        const int MAXITERS = 34; /* 26 */

        const float pi = 3.1415926535f;

        abstract protected float sum(vec3 v);
        abstract protected int func(vec3 pos, float stepshift);

        void main()
        {
        }
    }

    class __vertex_shader : VertexShader
    {
        [attribute]
        vec2 position;

        [attribute]
        vec2 texcoord;

        [uniform]
        float h;

        [varying]
        vec2 tc;


        void main()
        {
            //gl_Position = vec4(position, 0.0, 1.0);
            //tc = vec2(position.x, position.y * h);
        }
    }
    #endregion

}
