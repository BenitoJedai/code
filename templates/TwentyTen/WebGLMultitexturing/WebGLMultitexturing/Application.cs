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
        public Application(IXDefaultPage page)
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

        private void Initialize(IHTMLCanvas c, WebGLRenderingContext gl, IXDefaultPage page)
        {
            // http://cs.helsinki.fi/u/ilmarihe/metatunnel.html
            // http://wakaba.c3.cx/w/puls.html

            Action<string> alert = Native.Window.alert;

            c.style.border = "1px solid yellow";

            page.MaxTextures.innerText = "" + gl.getParameter(gl.MAX_COMBINED_TEXTURE_IMAGE_UNITS);

            var vs = "attribute vec3 aVertexPosition;";
            vs += "attribute vec2 aTextureCoord;";
            vs += "uniform mat4 uModelViewMatrix;";
            vs += "uniform mat4 uProjectionMatrix;";
            vs += "varying vec2 vTextureCoord;";
            vs += "void main(void) {";
            vs += "gl_Position = uProjectionMatrix * uModelViewMatrix * vec4(aVertexPosition, 1.0);";
            vs += "vTextureCoord = vec2(aTextureCoord.x, 1-aTextureCoord.y);";
            vs += "}";


            var fs = "varying vec2 vTextureCoord;";
            fs += "uniform sampler2D uSamplerDiffuse1;";
            fs += "uniform sampler2D uSamplerDiffuse2;";
            fs += "uniform sampler2D uSamplerDiffuse3;";
            fs += "uniform sampler2D uSamplerDiffuse4;";
            fs += "uniform sampler2D uSamplerDiffuse5;";
            fs += "uniform sampler2D uSamplerDiffuse6;";
            fs += "void main(void) {";
            fs += "gl_FragColor = vec4(1,0,0,1)*texture2D(uSamplerDiffuse1, vTextureCoord)";
            fs += "+ vec4(0,1,0,1)*texture2D(uSamplerDiffuse2, vTextureCoord)";
            fs += "+ vec4(0,0,1,1)*texture2D(uSamplerDiffuse3, vTextureCoord)";
            fs += "+ vec4(0,1,1,1)*texture2D(uSamplerDiffuse4, vTextureCoord)";
            fs += "+ vec4(1,0,1,1)*texture2D(uSamplerDiffuse5, vTextureCoord)";
            fs += "+ vec4(1,1,0,1)*texture2D(uSamplerDiffuse6, vTextureCoord);";
            fs += "}";


            var xfs = gl.createShader(gl.FRAGMENT_SHADER);
            gl.shaderSource(xfs, fs);
            gl.compileShader(xfs);

            var xvs = gl.createShader(gl.VERTEX_SHADER);
            gl.shaderSource(xvs, vs);
            gl.compileShader(xvs);

            var shader = new foo();

            shader.program = gl.createProgram();
            gl.attachShader(shader.program, xvs);
            gl.attachShader(shader.program, xfs);
            gl.linkProgram(shader.program);

            gl.useProgram(shader.program);
            shader.aVertexPosition = gl.getAttribLocation(shader.program, "aVertexPosition");
            shader.aTextureCoord = gl.getAttribLocation(shader.program, "aTextureCoord");
            gl.enableVertexAttribArray((ulong)shader.aVertexPosition);
            gl.enableVertexAttribArray((ulong)shader.aTextureCoord);

            shader.u["uModelViewMatrix"] = gl.getUniformLocation(shader.program, "uModelViewMatrix");
            shader.u["uProjectionMatrix"] = gl.getUniformLocation(shader.program, "uProjectionMatrix");
            shader.u["uSamplerDiffuse1"] = gl.getUniformLocation(shader.program, "uSamplerDiffuse1");
            shader.u["uSamplerDiffuse2"] = gl.getUniformLocation(shader.program, "uSamplerDiffuse2");
            shader.u["uSamplerDiffuse3"] = gl.getUniformLocation(shader.program, "uSamplerDiffuse3");
            shader.u["uSamplerDiffuse4"] = gl.getUniformLocation(shader.program, "uSamplerDiffuse4");
            shader.u["uSamplerDiffuse5"] = gl.getUniformLocation(shader.program, "uSamplerDiffuse5");
            shader.u["uSamplerDiffuse6"] = gl.getUniformLocation(shader.program, "uSamplerDiffuse6");

            shader.MV_MATRIX = new double[] { 
                1.0, 0.0, 0.0, 0.0,
                0.0, 1.0, 0.0, 0.0,
                0.0, 0.0, 1.0, 0.0,
                0.0, 0.0, 0.0, 1.0 };


            shader.PROJECTION_MATRIX = new double[] { 
                                1.000, 0.000, 0.000, 0.000,
                                0.000, 1.000, 0.000, 0.000,
                                0.000, 0.000, 0.002, 0.000,
                                0.000, 0.000, 0.998, 1.000 };

            var bar = default(bar);

            bar = initObject(
                gl,
                delegate
                {
                    if (bar == null)
                        return;

                    renderFrame(gl, shader, bar);
                }
            );

            c.style.border = "2px solid green";

        }

        void drawObject(WebGLRenderingContext gl, foo shader, bar @object)
        {
            gl.useProgram(shader.program);

            gl.bindBuffer(gl.ARRAY_BUFFER, @object.vertex_buffer);
            gl.vertexAttribPointer((ulong)shader.aVertexPosition, 3, gl.FLOAT, false, 0, 0);

            gl.bindBuffer(gl.ARRAY_BUFFER, @object.texturecoord_buffer);
            gl.vertexAttribPointer((ulong)shader.aTextureCoord, 2, gl.FLOAT, false, 0, 0);

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
            gl.clearColor(0.0, 0.0, 0.0, 1.0);
            gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);
            drawObject(gl, shader, @object);
        }


        static WebGLTexture loadTexture(WebGLRenderingContext gl, string url, Action callback)
        {
            gl.enable(gl.TEXTURE_2D);

            var texture = gl.createTexture();
            var image = new IHTMLImage();

            image.InvokeOnComplete(
                delegate
                {
                    gl.bindTexture(gl.TEXTURE_2D, texture);

                    // http://blog.tojicode.com/2010/07/obsolete-teximage2d-wha.html
                    // Calling obsolete texImage2D(GLenum target, GLint level, HTMLImageElement image, GLboolean flipY, GLboolean premultiplyAlpha)

                    new IFunction("gl", "image",
                        "gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, image);").apply(null,
                        gl, image
                    );
                    //gl.texImage2D(gl.TEXTURE_2D, 0, image, false, false);

                    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (long)gl.LINEAR);
                    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (long)gl.LINEAR);

                    gl.bindTexture(gl.TEXTURE_2D, null);

                    callback();
                }
            );

            image.src = url;
            return texture;
        }

        static void createRectangle(double size, data data)
        {
            var half = size / 2;

            data.vertices = new double[] {
                -half, -half, 0,
                half, -half, 0,
                half, half, 0,
                -half, half, 0
            };

            data.indices = new ushort[] {
                2,1,0,
                0,3,2
            };

            data.uvs = new double[] {
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
                vertices = new double[] { },
                indices = new ushort[] { },
                uvs = new double[] { }
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
            public double[] vertices;
            public ushort[] indices;
            public double[] uvs;
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

            public double[] MV_MATRIX;
            public double[] PROJECTION_MATRIX;

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
