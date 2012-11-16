using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.GLSL;

namespace WebGLCelShader.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __GeometryVertexShader : ScriptCoreLib.GLSL.VertexShader
    {
        [uniform]
        mat4 objectMatrix;
        [uniform]
        mat4 modelViewMatrix;
        [uniform]
        mat4 projectionMatrix;
        [uniform]
        mat4 viewMatrix;
        [uniform]
        mat3 normalMatrix;
        [uniform]
        vec3 cameraPosition;
        [attribute]
        vec3 position;
        [attribute]
        vec3 normal;
        [attribute]
        vec2 uv;
        [attribute]
        vec2 uv2;

        [varying]
        vec3 vNormal;
        [varying]
        vec3 vRefract;

        void main()
        {

            vec4 mPosition = objectMatrix * vec4(position, 1.0f);
            vec4 mvPosition = modelViewMatrix * vec4(position, 1.0f);
            vec3 nWorld = normalize(
                mat3(
                    objectMatrix[0].xyz, 
                    objectMatrix[1].xyz, 
                    objectMatrix[2].xyz
                ) * normal);

            vNormal = normalize(normalMatrix * normal);

            vec3 I = mPosition.xyz - cameraPosition;
            vRefract = refract(normalize(I), nWorld, 1.02f);

            gl_Position = projectionMatrix * mvPosition;

        }
    }
}
