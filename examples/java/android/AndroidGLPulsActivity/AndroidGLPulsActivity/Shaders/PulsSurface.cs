using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.WebGL;

namespace AndroidGLPulsActivity.Shaders
{
    using gl = WebGLRenderingContext;


    public class PulsSurface
    {
        public PulsSurface(ISurface s)
        {
            s.onsurface +=
                gl =>
                {
                    // http://cs.helsinki.fi/u/ilmarihe/metatunnel.html
                    // http://wakaba.c3.cx/w/puls.html

                    var p = gl.createProgram(
                        new PulsVertexShader(),
                        new PulsFragmentShader()
                    );

                    gl.bindAttribLocation(p, 0, "position");
                    gl.linkProgram(p);

                    gl.useProgram(p);



                    var pos = 0;
                    //var in_color = gl.getUniformLocation(p, "in_color");

                    gl.enableVertexAttribArray((uint)pos);


                    var verts = gl.createBuffer();

                    gl.bindBuffer(gl.ARRAY_BUFFER, verts);
                    gl.bufferData(gl.ARRAY_BUFFER,
                      new float[] { 
                          -1,-1,  -1,1,  1,-1, 1,1,
                      }
                    , gl.STATIC_DRAW);
                    gl.vertexAttribPointer((uint)pos, 2, gl.FLOAT, false, 0, 0);

                    var indicies = gl.createBuffer();

                    gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, indicies);



                    gl.bufferData(gl.ELEMENT_ARRAY_BUFFER,
                        new ushort[] { 0, 1, 2, 3 }
                        , gl.STATIC_DRAW);

                    s.onresize +=
                        (w, h) =>
                        {
                            gl.uniform1f(gl.getUniformLocation(p, "h"), (float)h / (float)w);
                            gl.viewport(0, 0, w, h);
                        };

                    var t = 0f;

                    s.onframe +=
                        delegate
                        {


                            t += 3;

                            // INVALID_OPERATION <= getUniformLocation([Program 2], "t")
                            gl.uniform1f(gl.getUniformLocation(p, "t"), t);

                            // INVALID_OPERATION <= drawElements(TRIANGLE_STRIP, 4, UNSIGNED_SHORT, 0)
                            gl.drawElements(gl.TRIANGLE_STRIP, 4, gl.UNSIGNED_SHORT, 0);
                            gl.flush();

                        };
                };
        }
    }
}
