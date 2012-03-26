using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.GLSL;

namespace WebGLPlanetGenerator.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __GeometryVertexShader : ScriptCoreLib.GLSL.VertexShader
    {
        [attribute]
        vec3 aVertexPosition;
        [attribute]
        vec4 aVertexNormal;
        [attribute]
        vec4 aVertexColor;

        [uniform]
        mat4 uMVMatrix;
        [uniform]
        mat4 uPMatrix;
        [uniform]
        mat4 uNMatrix;

        [varying]
        vec4 vColor;

        [varying]
        vec3 vLightWeighting;

        void main()
        {

            vec3 uAmbientColor = vec3(0.2f, 0.2f, 0.2f);
            vec3 uDirectionalColor = vec3(0.8f, 0.8f, 0.8f);
            vec3 uLightingDirection = vec3(0f, 1f, 0f);

            gl_Position = uPMatrix * uMVMatrix * vec4(aVertexPosition, 1.0f);
            gl_PointSize = 2.0f;
            //vLightWeighting = vec3(1.0, 1.0, 1.0);
            vec4 transformedNormal = uNMatrix * vec4(aVertexNormal.xyz, 1.0f);
            float directionalLightWeighting = max(dot(transformedNormal.xyz, uLightingDirection), 0.0f);
            vLightWeighting = uAmbientColor + uDirectionalColor * directionalLightWeighting;
            vColor = aVertexColor;
        }

    }
}
