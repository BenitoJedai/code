using ScriptCoreLib.JavaScript.WebGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.BCLImplementation.GLSL;
using System.Dynamic;

namespace WebGLSphereRayTrace.Shaders
{
    using xyz = __vec3;

    using gl = WebGLRenderingContext;
    using ScriptCoreLib.GLSL;


    public class RaySurface
    {

        public RaySurface(ISurface s)
        {
            s.onsurface +=
               gl =>
               {
                   var program = default(WebGLProgram);

                   #region init

                   program = gl.createProgram(
                       new RayVertexShader(),
                       new RayFragmentShader()
                   );

                   gl.bindAttribLocation(program, 0, "position");

                   gl.linkProgram(program);

                   gl.useProgram(program);
                   #endregion

                   var program_uniforms = program.Uniforms(gl);


                   var aVertexPosition = gl.getAttribLocation(program, "aVertexPosition");
                   gl.enableVertexAttribArray((uint)aVertexPosition);

                   var aPlotPosition = gl.getAttribLocation(program, "aPlotPosition");
                   gl.enableVertexAttribArray((uint)aPlotPosition);




                   gl.clearColor(0.0f, 0.0f, 0.0f, 1.0f);

                   gl.clearDepth(1.0f);


                   #region initBuffers()
                   var vertexPositionBuffer = gl.createBuffer();
                   gl.bindBuffer(gl.ARRAY_BUFFER, vertexPositionBuffer);
                   var vertices = new float[]{
                        1.0f,  1.0f,
                        -1.0f,  1.0f,
                        1.0f, -1.0f,
                        -1.0f, -1.0f,
                    };
                   gl.bufferData(gl.ARRAY_BUFFER, vertices, gl.STATIC_DRAW);
                   gl.bindBuffer(gl.ARRAY_BUFFER, vertexPositionBuffer);
                   gl.vertexAttribPointer((uint)aVertexPosition, 2, gl.FLOAT, false, 0, 0);


                   var plotPositionBuffer = gl.createBuffer();
                   gl.bindBuffer(gl.ARRAY_BUFFER, plotPositionBuffer);
                   gl.vertexAttribPointer((uint)aPlotPosition, 3, gl.FLOAT, false, 0, 0);
                   #endregion

                   #region maths
                   Func<xyz, xyz, xyz> crossProd = (v1, v2) =>
                   {
                       return new xyz
                       {
                           x = v1.y * v2.z - v2.y * v1.z,
                           y = v1.z * v2.x - v2.z * v1.x,
                           z = v1.x * v2.y - v2.x * v1.y
                       };
                   };

                   Func<xyz, xyz> normalize = (v) =>
                   {
                       var l = (float)Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
                       return new xyz { x = v.x / l, y = v.y / l, z = v.z / l };
                   };

                   Func<xyz, xyz, xyz> vectAdd = (v1, v2) =>
                   {
                       return new xyz { x = v1.x + v2.x, y = v1.y + v2.y, z = v1.z + v2.z };
                   };

                   Func<xyz, xyz, xyz> vectSub = (v1, v2) =>
                   {
                       return new xyz { x = v1.x - v2.x, y = v1.y - v2.y, z = v1.z - v2.z };
                   };

                   Func<xyz, float, xyz> vectMul = (v, l) =>
                   {
                       return new xyz { x = v.x * l, y = v.y * l, z = v.z * l };
                   };

                   Action<xyz, List<float>> pushVec = (v, arr) =>
                   {
                       arr.Add(v.x);
                       arr.Add(v.y);
                       arr.Add(v.z);
                   };
                   #endregion


                   var ratio = 1f;

                   s.onresize +=
                       (width, height) =>
                       {
                           ratio = (float)width / (float)height;

                           gl.viewport(0, 0, width, height);

                           Console.WriteLine(new { width, height, ratio });
                       };

                   var t = 0f;
                   s.onframe +=
                       delegate
                       {
                           var x1 = (float)Math.Sin(t * 1.1) * 1.5f;
                           var y1 = (float)Math.Cos(t * 1.3) * 1.5f;
                           var z1 = (float)Math.Sin(t + Math.PI / 3) * 1.5f;
                           var x2 = (float)Math.Cos(t * 1.2) * 1.5f;
                           var y2 = (float)Math.Sin(t * 1.4) * 1.5f;
                           var z2 = (float)Math.Sin(t * 1.25 - Math.PI / 3) * 1.5f;
                           var x3 = (float)Math.Cos(t * 1.15) * 1.5f;
                           var y3 = (float)Math.Sin(t * 1.37) * 1.5f;
                           var z3 = (float)Math.Sin(t * 1.27) * 1.5f;

                           var cameraFrom = new xyz
                           {
                               x = (float)Math.Sin(t * 0.4f) * 18,
                               y = (float)Math.Sin(t * 0.13f) * 5 + 5,
                               z = (float)Math.Cos(t * 0.4f) * 18
                           };

                           var cameraTo = new xyz();
                           var cameraPersp = 6;
                           var up = new xyz { x = 0, y = 1, z = 0 };

                           var cameraDir = normalize(vectSub(cameraTo, cameraFrom));

                           var cameraLeft = normalize(crossProd(cameraDir, up));
                           var cameraUp = normalize(crossProd(cameraLeft, cameraDir));
                           // cameraFrom + cameraDir * cameraPersp
                           var cameraCenter = vectAdd(cameraFrom, vectMul(cameraDir, cameraPersp));
                           // cameraCenter + cameraUp + cameraLeft * ratio
                           var cameraTopLeft = vectAdd(vectAdd(cameraCenter, cameraUp),
                                                    vectMul(cameraLeft, ratio));
                           var cameraBotLeft = vectAdd(vectSub(cameraCenter, cameraUp),
                                                    vectMul(cameraLeft, ratio));
                           var cameraTopRight = vectSub(vectAdd(cameraCenter, cameraUp),
                                                    vectMul(cameraLeft, ratio));
                           var cameraBotRight = vectSub(vectSub(cameraCenter, cameraUp),
                                                    vectMul(cameraLeft, ratio));


                           //corners = [1.2, 1, -12, -1.2, 1, -12, 1.2, -1, -12, -1.2, -1, -12];
                           var corners = new List<float>();



                           pushVec(cameraTopRight, corners);
                           pushVec(cameraTopLeft, corners);
                           pushVec(cameraBotRight, corners);
                           pushVec(cameraBotLeft, corners);

                           gl.bufferData(gl.ARRAY_BUFFER, corners.ToArray(), gl.STATIC_DRAW);

                           program_uniforms.cameraPos = cameraFrom;
                           program_uniforms.sphere1Center = new xyz(x1, y1, z1);
                           program_uniforms.sphere2Center = new xyz(x2, y2, z2);
                           program_uniforms.sphere3Center = new xyz(x3, y3, z3);



                           gl.drawArrays(gl.TRIANGLE_STRIP, 0, 4);

                           t += 0.03f;
                           if (t > Math.PI * 200)
                           {
                               t -= (float)Math.PI * 200f;
                           }


                       };
               };
        }
    }


}
