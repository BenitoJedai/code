using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;
using System.ComponentModel;

namespace WebGLLesson13.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __PerFragmentLightingFragmentShader : FragmentShader
    {

        [varying]
        vec2 vTextureCoord;
        [varying]
        vec3 vTransformedNormal;
        [varying]
        vec4 vPosition;
        [uniform]
        bool uUseLighting;
        [uniform]
        bool uUseTextures;
        [uniform]
        vec3 uAmbientColor;
        [uniform]
        vec3 uPointLightingLocation;
        [uniform]
        vec3 uPointLightingColor;
        [uniform]
        sampler2D uSampler;


        void main()
        {
            vec3 lightWeighting;
            if (!uUseLighting)
            {
                lightWeighting = vec3(1.0f, 1.0f, 1.0f);
            }
            else
            {
                vec3 lightDirection = normalize(uPointLightingLocation - vPosition.xyz);

                float directionalLightWeighting = max(dot(normalize(vTransformedNormal), lightDirection), 0.0f);
                lightWeighting = uAmbientColor + uPointLightingColor * directionalLightWeighting;
            }

            vec4 fragmentColor;
            if (uUseTextures)
            {
                fragmentColor = texture2D(uSampler, vec2(vTextureCoord.s, vTextureCoord.t));
            }
            else
            {
                fragmentColor = vec4(1.0f, 1.0f, 1.0f, 1.0f);
            }
            gl_FragColor = vec4(fragmentColor.rgb * lightWeighting, fragmentColor.a);
        }
    }
}
