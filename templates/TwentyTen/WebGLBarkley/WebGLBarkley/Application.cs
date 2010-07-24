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
using WebGLBarkley.HTML.Pages;
using WebGLBarkley;
using ScriptCoreLib.Avalon;
using ScriptCoreLib.JavaScript.WebGL;

namespace WebGLBarkley
{
    using gl = WebGLRenderingContext;
    using ScriptCoreLib.GLSL;
    using System.Collections.Generic;

    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class Application
    {


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IXDefaultPage page)
        {
            int w = 512;
            int h = 512;

            page.Content.Clear();

            this.c = new IHTMLCanvas();
            c.style.border = "1px solid red";
            c.style.SetSize(w, h);
            c.AttachTo(page.Content);

            //this.gl = (ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext)new IFunction("c", "return c.getContext('experimental-webgl', {depth : false } );").apply(null, c);

            //   gl = c.getContext("experimental-webgl", {depth : false } );
            this.gl = (WebGLRenderingContext)c.getContext("experimental-webgl");

            if (gl != null)
            {
                gl.viewport(0, 0, w, h);

                Initialize();
            }
        }

        IHTMLCanvas c;
        WebGLRenderingContext gl;

        WebGLShader getShader(string source, ulong type)
        {
            var shader = gl.createShader(type);
            gl.shaderSource(shader, source);
            gl.compileShader(shader);

            if (gl.getShaderParameter(shader, gl.COMPILE_STATUS) == null)
                Native.Window.alert(gl.getShaderInfoLog(shader));

            return shader;
        }

        WebGLTexture texture;
        WebGLTexture texture2;


        WebGLFramebuffer FBO;
        WebGLFramebuffer FBO2;

        void draw()
        {
            var gl = this.gl;

            gl.useProgram(prog);
            if (it > 0)
            {
                gl.bindTexture(gl.TEXTURE_2D, texture);
                gl.bindFramebuffer(gl.FRAMEBUFFER, FBO2);
            }
            else
            {
                gl.bindTexture(gl.TEXTURE_2D, texture2);
                gl.bindFramebuffer(gl.FRAMEBUFFER, FBO);
            }
            gl.drawArrays(gl.TRIANGLE_STRIP, 0, 4);
            gl.flush();

            gl.useProgram(prog_show);
            gl.bindFramebuffer(gl.FRAMEBUFFER, null);
            gl.drawArrays(gl.TRIANGLE_STRIP, 0, 4);
            it = -it;
        }

        WebGLProgram prog;
        WebGLProgram prog_show;

        int delay = 3;
        int it = 1;
        double a = .5;
        double b = .04;
        double dt = .02;
        double eps = .02;
        double h = .3;

        #region shaders
        string shader_fs = @"
 uniform sampler2D uTexSamp;
  uniform float dt;
  uniform float dte;
  uniform float dth2;
  uniform float a;
  uniform float ba;
  varying vec2 vTexCoord;
  const float d = 1./512.;
void main(void) {
   vec4 t = texture2D(uTexSamp, vTexCoord);
   float u = t.r,  v = t.g,  u2 = t.b,  v2 = t.a;
   u += u2/256.;   v += v2/256.;
   float vnew = v + (u - v)*dt,  uth = v/a + ba,  unew;
   float tmp = dte*(u - uth);
   if ( u <= uth)  unew = u/(1. - tmp*(1. - u));
   else{
      tmp *= u;
      unew = (tmp + u)/(tmp + 1.);
   }
   unew += (texture2D(uTexSamp, vec2(vTexCoord.x, vTexCoord.y + d) ).r +
      texture2D(uTexSamp, vec2(vTexCoord.x, vTexCoord.y - d) ).r +
      texture2D(uTexSamp, vec2(vTexCoord.x + d, vTexCoord.y) ).r +
      texture2D(uTexSamp, vec2(vTexCoord.x - d, vTexCoord.y) ).r +
      
     (texture2D(uTexSamp, vec2(vTexCoord.x, vTexCoord.y + d) ).b +
      texture2D(uTexSamp, vec2(vTexCoord.x, vTexCoord.y - d) ).b +
      texture2D(uTexSamp, vec2(vTexCoord.x + d, vTexCoord.y) ).b +
      texture2D(uTexSamp, vec2(vTexCoord.x - d, vTexCoord.y) ).b)/256.
 
      - 4.*u)*dth2;
   u2 = fract(unew*256.);
   if (u2 > .5) unew -= d;
   v2 = fract(vnew*256.);
   if (v2 > .5) vnew -= d;
   gl_FragColor = vec4(unew, vnew, u2, v2 );
}
            ";

        string shader_fs_show = @"
  uniform sampler2D uTexSamp;
  varying vec2 vTexCoord;
void main(void) {
   vec4 t = texture2D(uTexSamp, vTexCoord);
   gl_FragColor = vec4(t.r, 2.*t.g, 0., 1.);
}
            ";

        string shader_vs =
@"
  attribute vec3 aPos;
  attribute vec2 aTexCoord;
  varying   vec2 vTexCoord;
void main(void) {
   gl_Position = vec4(aPos, 1.);
   vTexCoord = aTexCoord;
}
    ";
        #endregion



        private void Initialize()
        {
            var gl = this.gl;

            // http://cs.helsinki.fi/u/ilmarihe/metatunnel.html
            // http://wakaba.c3.cx/w/puls.html
            // http://www.ibiblio.org/e-notes/webgl/barkley.html

            Action<string> alert = Native.Window.alert;

            c.style.border = "1px solid yellow";







            this.prog = gl.createProgram();

            gl.attachShader(prog, getShader(shader_vs, gl.VERTEX_SHADER));
            gl.attachShader(prog, getShader(shader_fs, gl.FRAGMENT_SHADER));

            gl.linkProgram(prog);


            this.prog_show = gl.createProgram();
            gl.attachShader(prog_show, getShader(shader_vs, gl.VERTEX_SHADER));
            gl.attachShader(prog_show, getShader(shader_fs_show, gl.FRAGMENT_SHADER));
            gl.linkProgram(prog_show);

            var posBuffer = gl.createBuffer();
            gl.bindBuffer(gl.ARRAY_BUFFER, posBuffer);

            // todo: jsc promoted float to double? :) let's revert that at the next build
            var vertices = new WebGLFloatArray(new double[] { -1, -1, 0, 1, -1, 0, -1, 1, 0, 1, 1, 0 });
            var aPosLoc = gl.getAttribLocation(prog, "aPos");
            gl.enableVertexAttribArray((ulong)aPosLoc);
            var aTexLoc = gl.getAttribLocation(prog, "aTexCoord");
            gl.enableVertexAttribArray((ulong)aTexLoc);
            var texCoords = new WebGLFloatArray(new double[] { 0, 0, 1, 0, 0, 1, 1, 1 });
            var texCoordOffset = vertices.byteLength;
            gl.bufferData(gl.ARRAY_BUFFER,
                (long)(texCoordOffset + texCoords.byteLength), gl.STATIC_DRAW);
            gl.bufferSubData(gl.ARRAY_BUFFER, 0, vertices);
            gl.bufferSubData(gl.ARRAY_BUFFER, (long)texCoordOffset, texCoords);
            gl.vertexAttribPointer((ulong)aPosLoc, 3, gl.FLOAT, false, 0, 0);
            gl.vertexAttribPointer((ulong)aTexLoc, 2, gl.FLOAT, false, 0, (long)texCoordOffset);

            this.texture = gl.createTexture();
            gl.bindTexture(gl.TEXTURE_2D, texture);
            gl.pixelStorei(gl.UNPACK_ALIGNMENT, 1);
            var pixels = new List<byte>();
            var tSize = 512;
            #region why is this here below dynamic? :) jsc could optimize?
            for (var i = 0; i < tSize; i++)
                for (var j = 0; j < tSize; j++)
                {
                    pixels.Add(0); pixels.Add(0);
                    pixels.Add(0); pixels.Add(0);
                }
            #endregion

            for (var i = 257; i < 512; i++)
            {
                pixels[4 * (256 * tSize + i)] = 250;
                pixels[4 * (255 * tSize + i) + 1] = 55;
            }
            gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, tSize, tSize, 0,
              gl.RGBA, gl.UNSIGNED_BYTE, new WebGLUnsignedByteArray(pixels.ToArray()));
            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (long)gl.NEAREST);
            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (long)gl.NEAREST);
            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_S, (long)gl.CLAMP_TO_EDGE);
            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_T, (long)gl.CLAMP_TO_EDGE);
            this.texture2 = gl.createTexture();
            gl.bindTexture(gl.TEXTURE_2D, texture2);
            gl.pixelStorei(gl.UNPACK_ALIGNMENT, 1);
            gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, tSize, tSize, 0,
              gl.RGBA, gl.UNSIGNED_BYTE, new WebGLUnsignedByteArray(pixels.ToArray()));
            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, (long)gl.NEAREST);
            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, (long)gl.NEAREST);
            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_S, (long)gl.CLAMP_TO_EDGE);
            gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_T, (long)gl.CLAMP_TO_EDGE);
            this.FBO = gl.createFramebuffer();
            gl.bindFramebuffer(gl.FRAMEBUFFER, FBO);
            gl.framebufferTexture2D(gl.FRAMEBUFFER, gl.COLOR_ATTACHMENT0,
                gl.TEXTURE_2D, texture, 0);
            this.FBO2 = gl.createFramebuffer();
            gl.bindFramebuffer(gl.FRAMEBUFFER, FBO2);
            gl.framebufferTexture2D(gl.FRAMEBUFFER, gl.COLOR_ATTACHMENT0,
                gl.TEXTURE_2D, texture2, 0);

            gl.useProgram(prog);
            gl.uniform1f(gl.getUniformLocation(prog, "a"), a);
            gl.uniform1f(gl.getUniformLocation(prog, "ba"), b / a);
            gl.uniform1f(gl.getUniformLocation(prog, "dt"), dt);
            gl.uniform1f(gl.getUniformLocation(prog, "dte"), dt / eps);
            gl.uniform1f(gl.getUniformLocation(prog, "dth2"), dt / (h * h));
            gl.uniform1i(gl.getUniformLocation(prog, "uTexSamp"), 0);
            gl.useProgram(prog_show);
            gl.uniform1i(gl.getUniformLocation(prog_show, "uTexSamp"), 0);



            Action draw = this.draw;


            draw.AtInterval(delay);

            c.style.border = "1px solid green";
        }


    }

    class __shader_vs
    {
        [attribute]
        vec3 aPos;
        [attribute]
        vec2 aTexCoord;
        [varying]
        vec2 vTexCoord;

        void Main()
        {

        }
    }

    class __shader_fs_show
    {
  [uniform] sampler2D uTexSamp;
  [varying] vec2 vTexCoord;

        void Main()
        {

        }
    }
}
