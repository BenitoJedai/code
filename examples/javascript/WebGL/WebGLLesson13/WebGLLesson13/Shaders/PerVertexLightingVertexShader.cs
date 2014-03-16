using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.GLSL;

namespace WebGLLesson13.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __PerVertexLightingVertexShader : ScriptCoreLib.GLSL.VertexShader
    {
        [attribute]
        vec3 aVertexPosition;
        [attribute]
        vec3 aVertexNormal;
        [attribute]
        vec2 aTextureCoord;

        [uniform]
        mat4 uMVMatrix;
        [uniform]
        mat4 uPMatrix;
        [uniform]
        mat3 uNMatrix;

        [uniform]
        vec3 uAmbientColor;

        [uniform]
        vec3 uPointLightingLocation;
        [uniform]
        vec3 uPointLightingColor;

        [uniform]
        bool uUseLighting;

        [varying]
        vec2 vTextureCoord;
        [varying]
        vec3 vLightWeighting;

        void main()
        {
            vec4 mvPosition = uMVMatrix * vec4(aVertexPosition, 1.0f);
            gl_Position = uPMatrix * mvPosition;
            vTextureCoord = aTextureCoord;

            if (!uUseLighting)
            {
                vLightWeighting = vec3(1.0f, 1.0f, 1.0f);
            }
            else
            {
                vec3 lightDirection = normalize(uPointLightingLocation - mvPosition.xyz);

                vec3 transformedNormal = uNMatrix * aVertexNormal;
                float directionalLightWeighting = max(dot(transformedNormal, lightDirection), 0.0f);
                vLightWeighting = uAmbientColor + uPointLightingColor * directionalLightWeighting;
            }
        }
    }
}
