using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;
using System.ComponentModel;

namespace AndroidGLAccelerometerSpiralActivity.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __SpiralFragmentShader : ScriptCoreLib.GLSL.FragmentShader
    {
        [uniform]
        float time;
        [uniform]
        vec2 resolution;
        [uniform]
        vec2 aspect;

        void main()
        {

            vec2 position = -aspect.xy + 2.0f * gl_FragCoord.xy / resolution.xy * aspect.xy;
            float angle = 0.0f;
            float radius = sqrt(position.x * position.x + position.y * position.y);
            if (position.x != 0.0f && position.y != 0.0f)
            {
                angle = degrees(atan(position.y, position.x));
            }
            float amod = mod(angle + 30.0f * time - 120.0f * log(radius), 30.0f);
            if (amod < 15.0)
            {
                gl_FragColor = vec4(0.0f, 0.0f, 0.0f, 1.0f);
            }
            else
            {
                gl_FragColor = vec4(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
    }
}
