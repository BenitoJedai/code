using System.ComponentModel;
using ScriptCoreLib.GLSL;

namespace AndroidOpenGLESLesson6Activity.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __pointFragmentShader : FragmentShader
    {

        void main()
        {
            gl_FragColor = vec4(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }
}
