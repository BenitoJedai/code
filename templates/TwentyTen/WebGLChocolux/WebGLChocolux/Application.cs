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
using WebGLChocolux.HTML.Pages;
using WebGLChocolux;
using ScriptCoreLib.Avalon;
using ScriptCoreLib.JavaScript.WebGL;

namespace WebGLChocolux
{
    using gl = WebGLRenderingContext;
    using ScriptCoreLib.JavaScript.Runtime;
    using ScriptCoreLib.GLSL;

    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class Application
    {
        int w = 400;
        int h = 300;

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IXDefaultPage page)
        {
            var c = new IHTMLCanvas();
            c.style.border = "1px solid red";
            c.width = w;
            c.height = h;

            page.Content.Clear();
            c.AttachTo(page.Content);

            var gl = c.getContext("experimental-webgl");

            if (gl != null)
                Initialize(c, (ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext)gl);
        }

        private void Initialize(IHTMLCanvas c, WebGLRenderingContext gl)
        {
            // http://cs.helsinki.fi/u/ilmarihe/metatunnel.html

            Action<string> alert = Native.Window.alert;

            c.style.backgroundColor = JSColor.Black;
            c.style.border = "1px solid yellow";

            var fragment_shader_source = @"
varying vec3 s[4];

void main()
{
	float t, b, c, h = 0.0;
	vec3 m, n;
	vec3 p = vec3(.2);
	vec3 d = normalize(.001 * gl_FragCoord.rgb - p);

	for (int i = 0; i < 4; i++)
	{
		t=2.0;
		for (int i = 0; i < 4; i++)
		{
			b = dot(d, n = s[i] - p);
			c = b * b + .2 - dot(n, n);
			if (b - c < t)
			{
				if (c > 0.0)
				{
					m = s[i];
					t = b - c;
				}
			}
		}
		p += t * d;
		d = reflect(d, n = normalize(p - m));
		h += pow(n.x * n.x, 44.) + n.x * n.x * .2;
	}
	gl_FragColor = vec4(h, h * h, h * h * h * h, h);
}
";

            var vertex_shader_source =
    @"
attribute vec2 position;
uniform float t;
varying vec3 s[4];
 
void main()
{
	gl_Position = vec4(position, 0.0, 1.0);
	s[0] = vec3(0);
	s[3] = vec3(sin(abs(t * .0001)), cos(abs(t * .0001)), 0);
	s[1] = s[3].zxy;
	s[2] = s[3].zzx;
}            
            ";

            var vs = gl.createShader(gl.VERTEX_SHADER);
            gl.shaderSource(vs, vertex_shader_source);
            gl.compileShader(vs);

            var fs = gl.createShader(gl.FRAGMENT_SHADER);
            gl.shaderSource(fs, fragment_shader_source);
            gl.compileShader(fs);
            if ((int)gl.getShaderParameter(fs, gl.COMPILE_STATUS) != 1)
            {
                var error = gl.getShaderInfoLog(fs);
                alert("vs: " + error);
                return;
            }

            var p = gl.createProgram();
            gl.attachShader(p, vs);
            gl.attachShader(p, fs);
            gl.bindAttribLocation(p, 0, "position");
            gl.linkProgram(p);

            var linked = gl.getProgramParameter(p, gl.LINK_STATUS);
            if (linked == null)
            {
                var error = gl.getProgramInfoLog(p);
                alert("Error while linking: " + error);
                return;
            }


            gl.useProgram(p);
            gl.viewport(0, 0, w, h);

            gl.enableVertexAttribArray(0);


            var verts = gl.createBuffer();

            gl.bindBuffer(gl.ARRAY_BUFFER, verts);
            gl.bufferData(gl.ARRAY_BUFFER, new WebGLFloatArray(
              new double[] { -1, -1, -1, 1, 1, -1, 1, 1 }
            ), gl.STATIC_DRAW);
            gl.vertexAttribPointer((ulong)0, 2, gl.FLOAT, false, 0, 0);

            var indicies = gl.createBuffer();

            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indicies);

            var q = new WebGLUnsignedShortArray(0, 1, 2, 3);



            gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, q, gl.STATIC_DRAW);

            var start = new IDate().getTime();
            Action redraw = delegate
            {
                var timestamp = new IDate().getTime();
                var t = (timestamp - start) / 1000.0f * 30f;



                gl.uniform1f(gl.getUniformLocation(p, "t"), t * 100);
                gl.drawElements(gl.TRIANGLE_STRIP, 4, gl.UNSIGNED_SHORT, 0);
                gl.flush();

            };
            redraw();

            new global::ScriptCoreLib.JavaScript.Runtime.Timer(
                t =>
                {
                    redraw();
                }
            ).StartInterval(1000 / 24);

            c.style.border = "1px solid green";


        }

        #region GLSL
        class shader_fs_show : FragmentShader
        {
            [varying]
            vec3[] s = new vec3[4];

            void main()
            {
                float h = 0.0f;

                gl_FragColor = vec4(h, h * h, h * h * h * h, h);
            }
        }

        class shader_vs : VertexShader
        {
            [attribute]
            public vec2 position;
            [uniform]
            public float t;
            [varying]
            vec3[] s = new vec3[4];

            void main()
            {
                gl_Position = vec4(position, 0.0f, 1.0f);
                //s[0] = vec3(0);
                //s[3] = vec3(sin(abs(t * .0001)), cos(abs(t * .0001)), 0);
                //s[1] = s[3].zxy;
                //s[2] = s[3].zzx;
            }
        }
        #endregion

    }


}
