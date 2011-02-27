﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;
using System.ComponentModel;

namespace WebGLSpiral.Shaders.vNext
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
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
