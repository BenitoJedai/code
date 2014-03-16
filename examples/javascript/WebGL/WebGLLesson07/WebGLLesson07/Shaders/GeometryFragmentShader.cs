using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;
using System.ComponentModel;

namespace WebGLLesson07.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __GeometryFragmentShader : FragmentShader
    {
        [varying]
        vec2 vTextureCoord;

        [varying]
        vec3 vLightWeighting;

        [uniform]
        sampler2D uSampler;

        void main()
        {
            vec4 textureColor = texture2D(uSampler, vec2(vTextureCoord.s, vTextureCoord.t));
            gl_FragColor = vec4(textureColor.rgb * vLightWeighting, textureColor.a);
        }
    }
}
