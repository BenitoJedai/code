using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.GLSL;

namespace WebGLLesson06.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __GeometryVertexShader : ScriptCoreLib.GLSL.VertexShader
    {
        [attribute]
        vec3 aVertexPosition;

        [attribute]
        vec2 aTextureCoord;


        [uniform]
        mat4 uMVMatrix;

        [uniform]
        mat4 uPMatrix;

        [varying]
        vec2 vTextureCoord;

        void main()
        {
            gl_Position = uPMatrix * uMVMatrix * vec4(aVertexPosition, 1.0f);
            vTextureCoord = aTextureCoord;
        }
    }
}
