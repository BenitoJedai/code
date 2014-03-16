using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;
using System.ComponentModel;

namespace WebGLSimpleCubic.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __CubicVertexShader : ScriptCoreLib.GLSL.VertexShader
    {
        [attribute]
        vec3 aPos;
        [attribute]
        vec3 aNorm;

        [uniform]
        mat4 mvMatrix;

        [uniform]
        mat4 prMatrix;
        [uniform]
        vec4 u_color;
        [varying]
        vec4 color;
        readonly vec3 dirDif = new vec3(0.0f, 0.0f, 1.0f);
        readonly vec3 dirHalf = new vec3(-.4034f, .259f, .8776f);

        void main()
        {
           gl_Position = prMatrix * mvMatrix * vec4(aPos, 1.0f);
           vec3 rotNorm = (mvMatrix * vec4(aNorm, .0f)).xyz;
           float i = abs( dot(rotNorm, dirDif) );
           color = vec4(i*u_color.rgb, u_color.a);
           i = .5f*pow( max( 0.0f, dot(rotNorm, dirHalf) ), 40.0f);
           color += vec4(i, i, i, 0.0f);
        }
    }
}
