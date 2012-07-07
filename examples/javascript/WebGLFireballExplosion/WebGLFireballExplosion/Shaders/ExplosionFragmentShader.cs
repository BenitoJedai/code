using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;

namespace WebGLFireballExplosion.Shaders
{
    class ExplosionVertexShader : VertexShader
    {

        [uniform]
        sampler2D tDiffuse;
        [uniform]
        vec2 resolution;
        [varying]
        vec2 vUv;

        void main()
        {
            vec2 p = gl_FragCoord.xy / resolution.xy;
            vec2 d = .5f - p;
            float distance = 2.0f * length(d);

            vec3 color = texture2D(tDiffuse, vec2(distance, 0.0f)).rgb;

            gl_FragColor = vec4(color, 1.0f);

        }


    }
}
