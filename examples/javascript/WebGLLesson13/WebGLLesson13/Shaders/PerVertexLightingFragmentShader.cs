using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;
using System.ComponentModel;

namespace WebGLLesson13.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __PerVertexLightingFragmentShader : FragmentShader
    {
        [varying]
        vec2 vTextureCoord;
        [varying]
        vec3 vLightWeighting;

        [uniform]
        bool uUseTextures;

        [uniform]
        sampler2D uSampler;

        void main()
        {
            vec4 fragmentColor;
            if (uUseTextures)
            {
                fragmentColor = texture2D(uSampler, vec2(vTextureCoord.s, vTextureCoord.t));
            }
            else
            {
                fragmentColor = vec4(1.0f, 1.0f, 1.0f, 1.0f);
            }
            gl_FragColor = vec4(fragmentColor.rgb * vLightWeighting, fragmentColor.a);
        }
    }
}
