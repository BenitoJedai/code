using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebGLMetaTunnel.HTML.Pages;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;

namespace WebGLMetaTunnelXX
{
    using gl = WebGLRenderingContext;
    using ScriptCoreLib.JavaScript;

    class vec2
    {
        public float x;
        public float y;

        public vec2(double x, double y)
        {

        }

        public static vec2 operator -(vec2 a, vec2 b)
        {
            return a;
        }

        public static vec2 operator /(vec2 a, vec2 b)
        {
            return a;
        }
    }

    class vec3
    {
        public float x;
        public float y;
        public float z;

        public vec3 xyy;
        public vec3 yxy;
        public vec3 yyx;

        public vec3(double x, double y, double z)
        {

        }

        public static vec3 operator -(vec3 a, vec3 b)
        {
            return a;
        }

        public static vec3 operator +(vec3 a, vec3 b)
        {
            return a;
        }

        public static vec3 operator /(vec3 a, vec3 b)
        {
            return a;
        }

        public static vec3 operator /(vec3 a, float b)
        {
            return a;
        }

        public static vec3 operator *(vec3 a, float b)
        {
            return a;
        }
    }

    class vec4
    {
        public float x;
        public float y;
        public float z;
        public float a;

        public static vec4 operator *(vec4 a, double b)
        {
            return a;
        }
        public static vec4 operator +(vec4 a, double b)
        {
            return a;
        }
        public static vec4 operator +(vec4 a, vec4 b)
        {
            return a;
        }
    }
    class gl_FragCoord
    {
        public static vec2 xy;
    }

    class uniform_vec4
    {
        public vec3 xyz
        {
            get
            {
                return null;
            }
        }
    }

    class ShaderApplication
    {
        public vec4 color;
        public vec4 gl_FragColor;
        public float t;
        public vec2 vec2(double a, double b)
        {
            return null;
        }

        public vec3 vec3(vec3 v)
        {
            return null;
        }

        public vec3 vec3(double a, double b = 0, double c = 0)
        {
            return null;
        }

        public vec4 vec4(double a, double c1 = 0, double c2 = 0, double c3 = 0)
        {
            return null;
        }


        public float dot(vec3 a, vec3 b)
        {
            return 0;
        }

        public float cos(double e)
        {
            return 0;
        }

        public float sin(double e)
        {
            return 0;
        }

        public float distance(vec3 e, vec3 x)
        {
            return 0;
        }

        public vec3 normalize(vec3 n)
        {
            throw new NotImplementedException();
        }


        public float max(float p, double p_2)
        {
            return 0;
        }
    }


    class Mockup
    {
        int w = 400;
        int h = 400;

        public Mockup(IXDefaultPage page)
        {
            var c = new IHTMLCanvas();
            c.style.border = "1px solid red";
            c.style.SetSize(w, h);
            page.Content.Clear();
            c.AttachTo(page.Content);

            var gl = c.getContext("experimental-webgl");

            if (gl != null)
                Initialize(c, (ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext)gl);
        }

        class fragment_shader_source : ShaderApplication
        {
            public fragment_shader_source(float _w = 400, float _h = 400)
            {
                uniform_vec4 in_color = default(uniform_vec4);

                const float s = 0.5f;

                vec2 v = (gl_FragCoord.xy - vec2(_w / 2, _h / 2)) / vec2(_w / 2, _h / 2);
                float w = dot(in_color.xyz, vec3(1.0, 256.0, 65536.0)) * .25f;

                Func<vec3, float> obj =
                    pos =>
                    {
                        float final = 1.0f;
                        final *= distance(pos, vec3(cos(w) + sin(w * 0.2), 0.3, 2.0 + cos(w * 0.5) * 0.5));
                        final *= distance(pos, vec3(-cos(w * 0.7), 0.3, 2.0 + sin(w * 0.5)));
                        final *= distance(pos, vec3(-sin(w * 0.2) * 0.5, sin(w), 2.0));
                        final *= cos(pos.y) * cos(pos.x) - 0.1f - cos(pos.z * 7f + w * 7f) * cos(pos.x * 3f) * cos(pos.y * 4f) * 0.1f;
                        return final;
                    };

                Action main = delegate
                {
                    vec3 o = vec3(v.x, v.y, 0.0);
                    vec3 d = vec3(v.x + cos(w) * .3f, v.y, 1.0f) / 64.0f;
                    vec4 color = vec4(0.0);
                    float t = 0.0f;
                    for (int i = 0; i < 40; i++)
                    {
                        if (obj(o + d * t) < s)
                        {
                            t -= 5.0f;
                            for (int j = 0; j < 5; j++) if (obj(o + d * t) >= s) t += 1.0f;
                            vec3 e = vec3(0.01, .0, .0);
                            vec3 n = vec3(0.0);
                            n.y = obj(o + d * t) - obj(vec3(o + d * t + e.xyy));
                            n.x = obj(o + d * t) - obj(vec3(o + d * t + e.yxy));
                            n.z = obj(o + d * t) - obj(vec3(o + d * t + e.yyx));
                            n = normalize(n);
                            color += max(dot(vec3(0.6, 0.0, -0.5), n), 0.0) + max(dot(vec3(-0.6, -0.0, -0.5), n), 0.0) * 0.5; color.a = 1.0f; break;
                        }
                        t += 5.0f;
                    }
                };

                gl_FragColor = this.color + vec4(0.4, 0.3, 0.2, 1.0) * (this.t * 0.025);
            }
        }

        private void Initialize(IHTMLCanvas c, WebGLRenderingContext gl)
        {
            // http://cs.helsinki.fi/u/ilmarihe/metatunnel.html

            Action<string> alert = Native.Window.alert;

            c.style.border = "1px solid yellow";



            var vertex_shader_source =
    "attribute vec4 pos; void main() { gl_Position = vec4(pos.x * float(" + w / 2 + "), pos.y * float(" + h / 2 + "), 0.0, 1.0); }";

            var vs = gl.createShader(gl.VERTEX_SHADER);
            gl.shaderSource(vs, vertex_shader_source);
            gl.compileShader(vs);

            var fs = gl.createShader(gl.FRAGMENT_SHADER);
            gl.shaderSource(fs, new fragment_shader_source(w, h).ToString());
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
            gl.linkProgram(p);

            var linked = gl.getProgramParameter(p, gl.LINK_STATUS);
            if (linked == null)
            {
                var error = gl.getProgramInfoLog(p);
                alert("Error while linking: " + error);
                return;
            }

            gl.viewport(0, 0, w, h);
            gl.useProgram(p);

            var pos = gl.getAttribLocation(p, "pos");
            var in_color = gl.getUniformLocation(p, "in_color");

            gl.enableVertexAttribArray((ulong)pos);


            var verts = gl.createBuffer();

            gl.bindBuffer(gl.ARRAY_BUFFER, verts);



            gl.bufferData(gl.ARRAY_BUFFER, new WebGLFloatArray(
              new double[] { -1, 1, 1, 1, -1, -1, 1, -1 }
            ), gl.STATIC_DRAW);
            gl.vertexAttribPointer((ulong)pos, 2, gl.FLOAT, false, 0, 0);

            Action redraw = delegate
            {
                var n = DateTime.Now.Ticks / 0x10000;
                var r = n & 0xff;
                var g = (n >> 8) & 0xff;
                var b = (n >> 16) & 0xff;
                // workaround for chrome webGL not invalidating canvas on drawArrays
                gl.clear(gl.COLOR_BUFFER_BIT);
                gl.uniform4f(in_color, r / 255.0, g / 255.0, b / 255.0, 1.0);
                gl.drawArrays(gl.TRIANGLE_STRIP, 0, 4);
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

    }
}
