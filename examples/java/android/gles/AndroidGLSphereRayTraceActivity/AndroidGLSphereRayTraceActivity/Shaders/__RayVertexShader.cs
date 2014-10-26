using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;

namespace AndroidGLSphereRayTraceActivity.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __RayVertexShader : ScriptCoreLib.GLSL.VertexShader
    {
        [attribute]
        vec2 aVertexPosition;
        [attribute]
        vec3 aPlotPosition;

        [varying]
        vec3 vPosition;

        void main()
        {
            gl_Position = vec4(aVertexPosition, 1.0f, 1.0f);
            vPosition = aPlotPosition;
        }
    }
}
