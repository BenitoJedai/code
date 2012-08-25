using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.WebGL;

namespace AndroidGLSpiralActivity.Shaders
{
    using gl = WebGLRenderingContext;


    public class SpiralSurface
    {
        public SpiralSurface(ISurface v)
        {
            v.onsurface +=
               gl =>
               {
                   //var __gl = (ScriptCoreLib.Android.__WebGLRenderingContext)(object)gl;

                   //Log.wtf("AndroidGLSpiralActivity", "onsurface");

                   var buffer = gl.createBuffer();

                   gl.bindBuffer(gl.ARRAY_BUFFER, buffer);

                   gl.bufferData(gl.ARRAY_BUFFER,
                    new Float32Array(
                            -1.0f, -1.0f, 1.0f,
                            -1.0f, -1.0f, 1.0f,
                            1.0f, -1.0f, 1.0f,
                            1.0f, -1.0f, 1.0f
                    ), gl.STATIC_DRAW);


                   // Create Program



                   #region createProgram

                   var program = gl.createProgram(
                       new SpiralVertexShader(),
                       new SpiralFragmentShader()
                   );



                   gl.linkProgram(program);
                   gl.useProgram(program);



                   #endregion

                   var parameters_time = 0L;
                   var parameters_screenWidth = 0;
                   var parameters_screenHeight = 0;
                   var parameters_aspectX = 0.0f;
                   var parameters_aspectY = 1.0f;

                   #region onresize
                   v.onresize +=
                       (width, height) =>
                       {
                           //Log.wtf("AndroidGLSpiralActivity", "onresize");

                           parameters_screenWidth = width;
                           parameters_screenHeight = height;

                           parameters_aspectX = (float)width / (float)height;
                           parameters_aspectY = 1.0f;

                           gl.viewport(0, 0, width, height);
                       };
                   #endregion


                   #region onframe
                   var framecount = 0;
                   v.onframe +=
                       delegate
                       {
                           var time = parameters_time / 1000f;

                           //if (framecount == 0)
                           //    Log.wtf("AndroidGLSpiralActivity", "onframe " + ((object)time).ToString());

                           parameters_time += 100;

                           gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);

                           // Load program into GPU


                           // Get var locations

                           var vertex_position = gl.getAttribLocation(program, "position");

                           // Set values to program variables

                           gl.uniform1f(gl.getUniformLocation(program, "time"), time);
                           gl.uniform2f(gl.getUniformLocation(program, "resolution"), parameters_screenWidth, parameters_screenHeight);
                           gl.uniform2f(gl.getUniformLocation(program, "aspect"), parameters_aspectX, parameters_aspectY);

                           // Render geometry

                           gl.bindBuffer(gl.ARRAY_BUFFER, buffer);
                           //opengl.glVertexAttribPointer(vertex_position, 2, (int)gl.FLOAT, false, 0, 0);
                           gl.vertexAttribPointer((uint)vertex_position, 2, gl.FLOAT, false, 0, 0);
                           gl.enableVertexAttribArray((uint)vertex_position);
                           gl.drawArrays(gl.TRIANGLES, 0, 6);
                           gl.disableVertexAttribArray((uint)vertex_position);

                           framecount++;
                       };
                   #endregion

                   //Log.wtf("AndroidGLSpiralActivity", "onsurface done");

               };
        }
    }

}
