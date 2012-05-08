using System.ComponentModel;
using ScriptCoreLib.GLSL;

namespace AndroidOpenGLESLesson3Activity.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __pointVertexShader : VertexShader
    {
        [uniform]
        mat4 u_MVPMatrix;
        [attribute]
        vec4 a_Position;

        void main()
        {
            gl_Position = u_MVPMatrix * a_Position;
            gl_PointSize = 5.0f;
        }
    }
}
