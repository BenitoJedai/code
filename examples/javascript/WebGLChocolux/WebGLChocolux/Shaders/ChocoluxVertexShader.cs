using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.GLSL;

namespace WebGLEscherDrosteEffect.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __ChocoluxVertexShader : ScriptCoreLib.GLSL.VertexShader
    {
        [attribute]
        public vec2 position;
        [uniform]
        public float t;
        [varying]
        vec3[] s = new vec3[4];

        void main()
        {
            gl_Position = vec4(position, 0.0f, 1.0f);
            s[0] = vec3(0);
            s[3] = vec3(sin(abs(t * .0001f)), cos(abs(t * .0001f)), 0);
            s[1] = s[3].zxy;
            s[2] = s[3].zzx;
        }


    }
}
