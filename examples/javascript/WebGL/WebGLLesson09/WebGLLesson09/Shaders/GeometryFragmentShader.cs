using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;
using System.ComponentModel;

namespace WebGLLesson09.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __GeometryFragmentShader : FragmentShader
    {
        [varying] vec2 vTextureCoord;

        [uniform] sampler2D uSampler;

        [uniform] vec3 uColor;

        void main() {
            vec4 textureColor = texture2D(uSampler, vec2(vTextureCoord.s, vTextureCoord.t));
            gl_FragColor = textureColor * vec4(uColor, 1.0f);
        }
    }
}
