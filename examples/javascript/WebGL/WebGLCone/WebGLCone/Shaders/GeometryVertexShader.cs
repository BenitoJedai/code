using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.GLSL;

namespace WebGLCone.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __GeometryVertexShader : ScriptCoreLib.GLSL.VertexShader
    {
        [attribute]
        vec3 aPos;
        [attribute]
        vec3 aNorm;
        [uniform]
        mat4 mvMatrix;
        [uniform]
        mat4 prMatrix;
        [varying]
        vec4 color;

        readonly vec3 dirDif = vec3(0.0f, 0.0f, 1.0f);
        readonly vec3 dirHalf = vec3(-.4034f, .259f, .8776f);

        void main()
        {
            gl_Position = prMatrix * mvMatrix * vec4(aPos, 1.0f);
            vec3 rotNorm = (mvMatrix * vec4(aNorm, 0.0f)).xyz;
            float i = max(0.0f, dot(rotNorm, dirDif));
            color = vec4(.9f * i, .5f * i, 0.0f, 1.0f);
            i = pow(max(0f, dot(rotNorm, dirHalf)), 120.0f);
            color += vec4(i, i, i, 0f);
        }
    }
}
