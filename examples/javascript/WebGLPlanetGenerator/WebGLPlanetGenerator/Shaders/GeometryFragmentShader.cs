using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;
using System.ComponentModel;

namespace WebGLPlanetGenerator.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __GeometryFragmentShader : FragmentShader
    {
        [varying]
        vec4 vColor;
        [varying]
        vec3 vLightWeighting;

        void main()
        {
            gl_FragColor = vColor * vec4(vLightWeighting, 1.0f);
        }
    }
}
