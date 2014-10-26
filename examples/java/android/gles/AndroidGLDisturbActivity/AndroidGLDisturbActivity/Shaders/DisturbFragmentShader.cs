using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;
using System.ComponentModel;

namespace WebGLShaderDisturb.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __DisturbFragmentShader : ScriptCoreLib.GLSL.FragmentShader
    {
        [uniform]
        float time;
        [uniform]
        vec2 resolution;
        [uniform]
        sampler2D texture;

        void main()
        {

            vec2 position = -1.0f + 2.0f * gl_FragCoord.xy / resolution.xy;

            float a = atan(position.y, position.x);
            float r = sqrt(dot(position, position));

            var uv = new vec2();
            uv.x = cos(a) / r;
            uv.y = sin(a) / r;
            uv /= 10.0f;
            uv += time * 0.05f;

            vec3 color = texture2D(texture, uv).rgb;

            gl_FragColor = vec4(color * r * 1.5f, 1.0f);

        }
    }
}
