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
using WebGLMetaTunnel.HTML.Pages;
using WebGLMetaTunnel;
using ScriptCoreLib.Avalon;
using ScriptCoreLib.JavaScript.WebGL;

namespace WebGLMetaTunnel
{
    using gl = WebGLRenderingContext;
    using ScriptCoreLib.GLSL;

    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class Application
    {
        public const int w = 400;
        public const int h = 400;

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IXDefaultPage page)
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

        private void Initialize(IHTMLCanvas c, WebGLRenderingContext gl)
        {
            // http://cs.helsinki.fi/u/ilmarihe/metatunnel.html

            Action<string> alert = Native.Window.alert;

            c.style.border = "1px solid yellow";

            var fragment_shader_source = @"
#version 120
uniform vec4 in_color;
vec2 v=(gl_FragCoord.xy-vec2(" + (w / 2) + @"," + (h / 2) + @"))/vec2(" + (w / 2) + @"," + (h / 2) + @");
float w=dot(in_color.xyz,vec3(1.0,256.0,65536.0))*.25;
const float s=0.5;

float obj(vec3 pos){
    float final=1.0;
    final*=distance(pos,vec3(cos(w)+sin(w*0.2),0.3,2.0+cos(w*0.5)*0.5));
    final*=distance(pos,vec3(-cos(w*0.7),0.3,2.0+sin(w*0.5)));
    final*=distance(pos,vec3(-sin(w*0.2)*0.5,sin(w),2.0));
    final *=cos(pos.y)*cos(pos.x)-0.1-cos(pos.z*7.+w*7.)*cos(pos.x*3.)*cos(pos.y*4.)*0.1;
    return final;
}

void main(){
    vec3 o=vec3(v.x,v.y,0.0);
    vec3 d=vec3(v.x+cos(w)*.3,v.y,1.0)/64.0;
    vec4 color=vec4(0.0);
    float t=0.0;
    for(int i=0;i<40;i++) {
    if(obj(o+d*t)<s){
    t-=5.0;
    for (int j=0; j<5; j++) if (obj(o+d*t)>=s) t+=1.0;
    vec3 e=vec3(0.01,.0,.0);
    vec3 n=vec3(0.0);
    n.y=obj(o+d*t)-obj(vec3(o+d*t+e.xyy));
    n.x=obj(o+d*t)-obj(vec3(o+d*t+e.yxy));
    n.z=obj(o+d*t)-obj(vec3(o+d*t+e.yyx));
    n=normalize(n);
    color+=max(dot(vec3(0.6,0.0,-0.5),n),0.0)+max(dot(vec3(-0.6,-0.0,-0.5),n),0.0)*0.5;color.a=1.0;break;
    }
    t+=5.0;
}
gl_FragColor=color+vec4(0.4,0.3,0.2,1.0)*(t*0.025);
            }";

            var vertex_shader_source =
    "attribute vec4 pos; void main() { gl_Position = vec4(pos.x * float(" + w / 2 + "), pos.y * float(" + h / 2 + "), 0.0, 1.0); }";

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

    #region GLSL
    class _fragment_shader_source : FragmentShader
    {
        [uniform]
        vec4 in_color;

        vec2 v;
        float w;
        const float s = 0.5f;

        public _fragment_shader_source()
        {
            //this.v = (gl_FragCoord.xy - vec2(" + (w / 2) + @", " + (h / 2) + @")) / vec2(" + (w / 2) + @", " + (h / 2) + @");
            //this.w = dot(in_color.xyz, vec3(1.0, 256.0, 65536.0)) * .25;
        }

        float obj(vec3 pos)
        {
            return default(float);
        }

        void main()
        {
            //vec4 color = vec4(0.0);

            //gl_FragColor = color + vec4(0.4, 0.3, 0.2, 1.0) * (t * 0.025);
        }
    }

    class _vertex_shader_source : VertexShader
    {
        [attribute]
        public vec4 pos;

        void main()
        {
            gl_Position = vec4(pos.x * (float)(Application.w / 2), pos.y * (float)(Application.h / 2), 0.0f, 1.0f);
        }

    }
    #endregion
}
