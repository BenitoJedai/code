using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.GLSL;
using System.ComponentModel;

namespace WebGLTunnel.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __TunnelFragmentShader : ScriptCoreLib.GLSL.FragmentShader
    {
        [uniform]
        sampler2D uSampler;

        [varying]
        vec4 vColor;
        [varying]
        vec2 vTextureCoord;

        void main()
        {
            // -- get the pixel from the texture
            vec4 textureColor = texture2D(uSampler, vec2(vTextureCoord.s, vTextureCoord.t));
            // -- multiply the texture pixel with the vertex color
            gl_FragColor = vColor * textureColor;
        }
    }
}
