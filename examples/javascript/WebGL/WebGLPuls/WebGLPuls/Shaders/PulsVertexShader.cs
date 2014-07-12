using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.GLSL;

namespace SpiderModel.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __PulsVertexShader : ScriptCoreLib.GLSL.VertexShader
    {
        [attribute]
        vec2 position;

        [attribute]
        vec2 texcoord;

        [uniform]
        float h;

        [varying]
        vec2 tc;


        void main()
        {
            gl_Position = vec4(position, 0.0f, 1.0f);
            tc = vec2(position.x, position.y * h);
        }
    }
}
