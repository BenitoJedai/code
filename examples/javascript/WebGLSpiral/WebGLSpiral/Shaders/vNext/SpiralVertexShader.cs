using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;

namespace WebGLSpiral.Shaders.vNext
{
    class SpiralVertexShader : ScriptCoreLib.GLSL.VertexShader
    {
        [attribute]
        vec3 position;

        void main()
        {
            gl_Position = vec4(position, 1.0f);
        }
    }
}
