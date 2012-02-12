using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;
using System.ComponentModel;

namespace WebGLSimpleCubic.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __LineVertexShader : ScriptCoreLib.GLSL.VertexShader
    {
        [attribute]
        vec3 aPos;
        [uniform]
        mat4 mvMatrix;
        [uniform]
        mat4 prMatrix;

        void main()
        {
            gl_Position = prMatrix * mvMatrix * vec4(aPos, 1.0f);
        }
    }
}
