using ScriptCoreLib.GLSL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CSSShaderSphereSimple.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __sphere_simplFragmentShader : ScriptCoreLib.GLSL.FragmentShader
    {
        // Varyings

        [varying]
        float v_light;

        // Main

        void main()
        {

            float r, g, b;
            r = g = b = v_light;

            css_ColorMatrix = mat4(
                r, 0.0f, 0.0f, 0.0f,
                0.0f, g, 0.0f, 0.0f,
                0.0f, 0.0f, b, 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f
            );

        }

        // https://dvcs.w3.org/hg/FXTF/raw-file/tip/filters/index.html#shader-processing-model
        /* inherited */ mat4 css_ColorMatrix;

    }
}
