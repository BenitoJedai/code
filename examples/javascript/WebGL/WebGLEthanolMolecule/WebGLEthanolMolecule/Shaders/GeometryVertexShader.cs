using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.GLSL;

namespace WebGLEthanolMolecule.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __GeometryVertexShader : ScriptCoreLib.GLSL.VertexShader
    {
        [attribute]
        vec3 aPos; // Normals = Pos
        [uniform]
        mat4 mvMatrix;
        [uniform]
        mat4 prMatrix;
        [uniform]
        vec3 color;
        [uniform]
        float scale;
        [varying]
        vec3 col;


        readonly vec4 dirDif = vec4(0.0f, 0.0f, 1.0f, 0.0f);
        readonly vec4 dirHalf = vec4(-.4034f, .259f, .8776f, 0.0f);

        void main() 
        {
           gl_Position = prMatrix * mvMatrix * vec4(scale * aPos, 1.0f);
           vec4 rotNorm = mvMatrix * vec4(aPos, .0f);
           float i = max( 0.0f, dot(rotNorm, dirDif) );
           col = i * color;
           i = pow( max( 0.0f, dot(rotNorm, dirHalf) ), 30.0f);
           col += vec3(i, i, i);
        }
    }
}
