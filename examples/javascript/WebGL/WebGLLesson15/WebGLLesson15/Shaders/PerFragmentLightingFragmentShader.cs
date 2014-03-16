using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;
using System.ComponentModel;

namespace WebGLLesson15.Shaders
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
        bool uUseColorMap;
        [uniform]
        bool uUseSpecularMap;
        [uniform]
        bool uUseLighting;

        [uniform]
        vec3 uAmbientColor;

        [uniform]
        vec3 uPointLightingLocation;
        [uniform]
        vec3 uPointLightingSpecularColor;
        [uniform]
        vec3 uPointLightingDiffuseColor;

        [uniform]
        sampler2D uColorMapSampler;
        [uniform]
        sampler2D uSpecularMapSampler;


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
                vec3 normal = normalize(vTransformedNormal);

                float specularLightWeighting = 0.0f;
                float shininess = 32.0f;
                if (uUseSpecularMap)
                {
                    shininess = texture2D(uSpecularMapSampler, vec2(vTextureCoord.s, vTextureCoord.t)).r * 255.0f;
                }
                if (shininess < 255.0)
                {
                    vec3 eyeDirection = normalize(-vPosition.xyz);
                    vec3 reflectionDirection = reflect(-lightDirection, normal);

                    specularLightWeighting = pow(max(dot(reflectionDirection, eyeDirection), 0.0f), shininess);
                }

                float diffuseLightWeighting = max(dot(normal, lightDirection), 0.0f);
                lightWeighting = uAmbientColor
                    + uPointLightingSpecularColor * specularLightWeighting
                    + uPointLightingDiffuseColor * diffuseLightWeighting;
            }

            vec4 fragmentColor;
            if (uUseColorMap)
            {
                fragmentColor = texture2D(uColorMapSampler, vec2(vTextureCoord.s, vTextureCoord.t));
            }
            else
            {
                fragmentColor = vec4(1.0f, 1.0f, 1.0f, 1.0f);
            }
            gl_FragColor = vec4(fragmentColor.rgb * lightWeighting, fragmentColor.a);
        }
    }
}
