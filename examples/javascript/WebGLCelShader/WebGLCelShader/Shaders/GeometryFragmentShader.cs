using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;
using System.ComponentModel;

namespace WebGLCelShader.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __GeometryFragmentShader : FragmentShader
    {
        [uniform]
        mat4 viewMatrix;
        [uniform]
        vec3 cameraPosition;

        [uniform]
        vec3 uBaseColor;

        [uniform]
        vec3 uDirLightPos;
        [uniform]
        vec3 uDirLightColor;

        [uniform]
        vec3 uAmbientLightColor;

        [varying]
        vec3 vNormal;

        [varying]
        vec3 vRefract;

        void main()
        {

            float directionalLightWeighting = max(dot(normalize(vNormal), uDirLightPos), 0.0f);
            vec3 lightWeighting = uAmbientLightColor + uDirLightColor * directionalLightWeighting;

            float intensity = smoothstep(-0.5f, 1.0f, pow(length(lightWeighting), 20.0f));
            intensity += length(lightWeighting) * 0.2f;

            float cameraWeighting = dot(normalize(vNormal), vRefract);
            intensity += pow(1.0f - length(cameraWeighting), 6.0f);
            intensity = intensity * 0.2f + 0.3f;

            if (intensity < 0.50)
            {

                gl_FragColor = vec4(2.0f * intensity * uBaseColor, 1.0f);

            }
            else
            {

                gl_FragColor = vec4(1.0f - 2.0f * (1.0f - intensity) * (1.0f - uBaseColor), 1.0f);

            }

        }
    }
}
