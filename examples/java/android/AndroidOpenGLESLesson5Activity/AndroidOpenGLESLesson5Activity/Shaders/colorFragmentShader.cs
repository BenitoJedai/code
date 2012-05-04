using System.ComponentModel;
using ScriptCoreLib.GLSL;

namespace AndroidOpenGLESLesson5Activity.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __per_pixelFragmentShader : FragmentShader
    {
        [varying]
        vec4 v_Color;          	// This is the color from the vertex shader interpolated across the 
        // triangle per fragment.

        // The entry point for our fragment shader.
        void main()
        {
            // Pass through the color
            gl_FragColor = v_Color;
        }


    }
}
