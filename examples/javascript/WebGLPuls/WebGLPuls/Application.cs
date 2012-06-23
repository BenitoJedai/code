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
    using WebGLPuls.Shaders;

    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class Application
    {
        // see also: http://meatfighter.com/puls/
        // it only took 2 years :)
        // Revision: 2744
        //Date: Friday, July 23, 2010 4:34:01 PM
        //Added : /templates/TwentyTen/WebGLPuls/WebGLPuls.sln




        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IXDefaultPage page = null)
        {

            InitializeContent(page);
        }

        public Action Dispose;

        private void InitializeContent(IXDefaultPage page = null)
        {
            var canvas = new IHTMLCanvas().AttachToDocument();

            Native.Document.body.style.overflow = IStyle.OverflowEnum.hidden;

            canvas.style.SetLocation(0, 0);


            //http://www.khronos.org/webgl/public-mailing-list/archives/1002/msg00125.html
        

            var gl = (WebGLRenderingContext)canvas.getContext("experimental-webgl");

            #region Dispose
            var IsDisposed = false;

            Dispose = delegate
            {
                if (IsDisposed)
                    return;

                IsDisposed = true;

                canvas.Orphanize();
            };
            #endregion


            #region AtResize
            Action AtResize = delegate
            {
                if (IsDisposed)
                {
                    return;
                }

                canvas.width = Native.Window.Width;
                canvas.height = Native.Window.Height;

     

                gl.viewport(0, 0, canvas.width, canvas.height);
            };

            AtResize();

            Native.Window.onresize += delegate
            {
                AtResize();
            };
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


            // http://cs.helsinki.fi/u/ilmarihe/metatunnel.html
            // http://wakaba.c3.cx/w/puls.html

            Action<string> alert = x => Native.Window.alert(x);


            #region fragment_shader_source
            var fragment_shader_source = @"
// #version 120

precision highp float;

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
	for (int xxx = 0; xxx < 64; xxx++)
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

        if (!(stepshift<MAXSTEPSHIFT&&i<MAXITERS))
            break;
	}
 
 
	vec3 col;
	if(c==0) col=vec3(0.0,0.0,0.0);
	else if(c==1) col=vec3(1.0,0.5,0.0);
	else if(c==2) col=vec3(0.0,1.0,0.0);
	else if(c==3) col=vec3(1.0,1.0,1.0);
	else if(c==4) col=vec3(0.5,0.5,0.5);
 
	gl_FragColor=vec4(col*(1.0-(float(i)-stepshift)/32.0),1.0);
}

            ";
            #endregion

            #region vertex_shader_source
            var vertex_shader_source =
    @"
// #version 120

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
            #endregion


          


            #region createShader
            Func<ScriptCoreLib.GLSL.Shader, WebGLShader> createShader = (src) =>
            {
                var shader = gl.createShader(src);

                // verify
                if (gl.getShaderParameter(shader, gl.COMPILE_STATUS) == null)
                {
                    Native.Window.alert("error in SHADER:\n" + gl.getShaderInfoLog(shader));
                    throw new InvalidOperationException("shader failed");
                }

                return shader;
            };
            #endregion

            var p = gl.createProgram();
            var vs = createShader(new PulsVertexShader());
            var fs = createShader(new PulsFragmentShader());

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

            //Native.Document.title = "WebGL..";

            gl.useProgram(p);



            var pos = 0;
            //var in_color = gl.getUniformLocation(p, "in_color");

            gl.enableVertexAttribArray((uint)pos);


            var verts = gl.createBuffer();

            gl.bindBuffer(gl.ARRAY_BUFFER, verts);
            gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(
              new float[] { 
                  -1,-1,  -1,1,  1,-1, 1,1,
              }
            ), gl.STATIC_DRAW);
            gl.vertexAttribPointer((uint)pos, 2, gl.FLOAT, false, 0, 0);

            var indicies = gl.createBuffer();

            gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indicies);



            gl.bufferData(gl.ELEMENT_ARRAY_BUFFER,
                new Uint16Array(
                /*new ushort[] {*/ 0, 1, 2, 3 /*}*/
                    )
                , gl.STATIC_DRAW);

            var start = new IDate().getTime();
            Action redraw = null;

            redraw = delegate
            {
                gl.viewport(0, 0, Native.Window.Width, Native.Window.Height);
                gl.uniform1f(gl.getUniformLocation(p, "h"), Native.Window.Height / Native.Window.Width);


                var timestamp = new IDate().getTime();
                var t = (float)((timestamp - start) / 1000.0 * 30);

                // INVALID_OPERATION <= getUniformLocation([Program 2], "t")
                gl.uniform1f(gl.getUniformLocation(p, "t"), t);

                // INVALID_OPERATION <= drawElements(TRIANGLE_STRIP, 4, UNSIGNED_SHORT, 0)
                gl.drawElements(gl.TRIANGLE_STRIP, 4, gl.UNSIGNED_SHORT, 0);
                gl.flush();


                Native.Window.requestAnimationFrame += redraw;

            };

            Native.Window.requestAnimationFrame += redraw;

        }


    }



}
