using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;
using System.ComponentModel;

namespace WebGLSimpleCubic.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __CubicFragmentShader : ScriptCoreLib.GLSL.FragmentShader
    {
        [varying]
        vec4 color;

        void main()
        {
            gl_FragColor = color;
        }
    }
}
