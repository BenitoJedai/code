using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;
using System.ComponentModel;

namespace WebGLClouds.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __GeometryFragmentShader : FragmentShader
    {

        [uniform]
        sampler2D map;

        [uniform]
        vec3 fogColor;
        [uniform]
        float fogNear;
        [uniform]
        float fogFar;

        [varying]
        vec2 vUv;

        void main()
        {

            float depth = gl_FragCoord.z / gl_FragCoord.w;
            float fogFactor = smoothstep(fogNear, fogFar, depth);

            gl_FragColor = texture2D(map, vUv);
            gl_FragColor.w *= pow(gl_FragCoord.z, 20.0f);
            gl_FragColor = mix(gl_FragColor, vec4(fogColor, gl_FragColor.w), fogFactor);

        }
    }
}
