using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;

namespace WebGLFireballExplosion.Shaders
{
    class __ExplosionFragmentShader : FragmentShader
    {

        [varying]
        vec2 vUv;
        [uniform]
        sampler2D tExplosion;
        [varying]
        vec3 vReflect;
        [varying]
        vec3 pos;
        [varying]
        float ao;
        [varying]
        float d;
        float PI = 3.14159265358979323846264f;

        float random(vec3 scale, float seed) { return fract(sin(dot(gl_FragCoord.xyz + seed, scale)) * 43758.5453f + seed); }

        void main()
        {

            vec3 color = texture2D(tExplosion, vec2(0f, 1.0f - 1.3f * ao + .01f * random(vec3(12.9898f, 78.233f, 151.7182f), 0.0f))).rgb;
            gl_FragColor = vec4(color.rgb, 1.0f);

        }

    }
}
