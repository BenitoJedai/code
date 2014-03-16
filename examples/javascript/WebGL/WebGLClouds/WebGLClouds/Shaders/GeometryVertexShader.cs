using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.GLSL;

namespace WebGLClouds.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __GeometryVertexShader : ScriptCoreLib.GLSL.VertexShader
    {
        [uniform]
        mat4 modelViewMatrix;

        [uniform]
        mat4 projectionMatrix;


        [attribute]
        vec3 position;

        [attribute]
        vec2 uv;

        [varying]
        vec2 vUv;

        void main()
        {

            vUv = uv;
            gl_Position = projectionMatrix * modelViewMatrix * vec4(position, 1.0f);

        }

    }
}
