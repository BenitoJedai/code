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
using WebGLPuls.HTML.Pages;
using WebGLPuls;
using ScriptCoreLib.Avalon;
using ScriptCoreLib.JavaScript.WebGL;

namespace WebGLPuls
{
    using gl = WebGLRenderingContext;
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
            
            //http://www.khronos.org/webgl/public-mailing-list/archives/1002/msg00125.html
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
            // http://wakaba.c3.cx/w/puls.html

            Action<string> alert = Native.Window.alert;

            c.style.border = "1px solid yellow";

            var fragment_shader_source = @"
uniform float t;
varying vec2 tc;
 
const float BLOWUP=66.0; /* 86.0 */
const float MAXSTEPSHIFT=8.0; /* 6.0 */
const int MAXITERS=34; /* 26 */
 
const float pi=3.1415926535;
 
float sum(vec3 v) { return v.x+v.y+v.z; }
 
int func(vec3 pos,float stepshift)
{
	vec3 v2=abs(fract(pos)-vec3(0.5,0.5,0.5))/2.0;
	float r=0.0769*sin(t*-0.0708);
	float blowup=BLOWUP/pow(2.0,stepshift+8.0);
 
	if(sum(v2)-0.1445+r<blowup) return 1;
	v2=vec3(0.25,0.25,0.25)-v2;
	if(sum(v2)-0.1445-r<blowup) return 2;
 
	int hue;
	float width;
	if(abs(sum(v2)-3.0*r-0.375)<0.03846)
	{
		width=0.1445;
		hue=4;
	}
	else
	{
		width=0.0676;
		hue=3;
	}
 
	if(sum(abs(v2.zxy-v2.xyz))-width<blowup) return hue;
 
	return 0;
}
 
void main()
{
	float x=tc.x*0.5;
	float y=tc.y*0.5;
 
	float sin_a=sin(t*0.00564);
	float cos_a=cos(t*0.00564);
 
	vec3 dir=vec3(x,-y,0.33594-x*x-y*y);
	dir=vec3(dir.y,dir.z*cos_a-dir.x*sin_a,dir.x*cos_a+dir.z*sin_a);
	dir=vec3(dir.y,dir.z*cos_a-dir.x*sin_a,dir.x*cos_a+dir.z*sin_a);
	dir=vec3(dir.y,dir.z*cos_a-dir.x*sin_a,dir.x*cos_a+dir.z*sin_a);
 
	vec3 pos=vec3(0.5,1.1875,0.875)+vec3(1.0,1.0,1.0)*0.0134*t;
 
	float stepshift=MAXSTEPSHIFT;
 
	if(fract(pow(x,y)*t*1000.0)>0.5) pos+=dir/pow(2.0,stepshift);
	else pos-=dir/pow(2.0,stepshift);
 
	int i=0;
	int c;
	do
	{
		c=func(pos,stepshift);
		if(c>0)
		{
			stepshift+=1.0;
			pos-=dir/pow(2.0,stepshift);
		}
		else
		{
			if(stepshift>0.0) stepshift-=1.0;
			pos+=dir/pow(2.0,stepshift);
			i++;
		}
	}
	while(stepshift<MAXSTEPSHIFT&&i<MAXITERS);
 
 
	vec3 col;
	if(c==0) col=vec3(0.0,0.0,0.0);
	else if(c==1) col=vec3(1.0,0.5,0.0);
	else if(c==2) col=vec3(0.0,1.0,0.0);
	else if(c==3) col=vec3(1.0,1.0,1.0);
	else if(c==4) col=vec3(0.5,0.5,0.5);
 
	gl_FragColor=vec4(col*(1.0-(float(i)-stepshift)/32.0),1.0);
}

            ";

            var vertex_shader_source =
    @"
attribute vec2 position;
attribute vec2 texcoord;
uniform float h;
varying vec2 tc;
 
void main()
{
	gl_Position=vec4(position,0.0,1.0);
	tc=vec2(position.x,position.y*h);
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
            gl.uniform1f(gl.getUniformLocation(p, "h"), h / w);



            var pos = 0;
            //var in_color = gl.getUniformLocation(p, "in_color");

            gl.enableVertexAttribArray((ulong)pos);


            var verts = gl.createBuffer();

            gl.bindBuffer(gl.ARRAY_BUFFER, verts);
            gl.bufferData(gl.ARRAY_BUFFER, new WebGLFloatArray(
              new double[] { 
                  -1,-1,  -1,1,  1,-1, 1,1,
              }
            ), gl.STATIC_DRAW);
            gl.vertexAttribPointer((ulong)pos, 2, gl.FLOAT, false, 0, 0);

            var indicies = gl.createBuffer();

            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indicies);

  

            gl.bufferData(gl.ELEMENT_ARRAY_BUFFER,
                new WebGLUnsignedShortArray(
                    /*new ushort[] {*/ 0, 1, 2, 3 /*}*/
                    )
                , gl.STATIC_DRAW);

            var start = new IDate().getTime();
            Action redraw = delegate
            {
                var timestamp = new IDate().getTime();
                var t = (timestamp - start) / 1000.0 * 30;

                gl.uniform1f(gl.getUniformLocation(p, "t"), t);
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
