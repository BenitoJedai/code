using ScriptCoreLib.GLSL;

namespace AndroidOpenGLESLesson1Activity.Shaders
{
    class __TriangleFragmentShader : FragmentShader
    {
        void main()
        {
            gl_FragColor = vec4(0.63671875f, 0.76953125f, 0.22265625f, 1.0f);
        }
    }
}
