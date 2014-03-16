using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.GLSL;

namespace WebGLLesson07.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __GeometryVertexShader : ScriptCoreLib.GLSL.VertexShader
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
        vec3 uLightingDirection;
        [uniform]
        vec3 uDirectionalColor;
        [uniform]
        bool uUseLighting;
        [varying]
        vec2 vTextureCoord;
        [varying]
        vec3 vLightWeighting;

        void main()
        {
            gl_Position = uPMatrix * uMVMatrix * vec4(aVertexPosition, 1.0f);
            vTextureCoord = aTextureCoord;

            if (!uUseLighting)
            {
                vLightWeighting = vec3(1.0f, 1.0f, 1.0f);
            }
            else
            {
                vec3 transformedNormal = uNMatrix * aVertexNormal;
                float directionalLightWeighting = max(dot(transformedNormal, uLightingDirection), 0.0f);
                vLightWeighting = uAmbientColor + uDirectionalColor * directionalLightWeighting;
            }
        }
    }
}
