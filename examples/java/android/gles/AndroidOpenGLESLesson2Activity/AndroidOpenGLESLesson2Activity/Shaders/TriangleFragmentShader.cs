using System.ComponentModel;
using ScriptCoreLib.GLSL;

namespace AndroidOpenGLESLesson2Activity.Shaders
{
    [Description("Future versions of JSC will allow shaders to be written in a .NET language")]
    class __TriangleFragmentShader : FragmentShader
    {
        // precision in the fragment shader.				
        [varying]
        vec4 v_Color;          		// This is the color from the vertex shader interpolated across the 
        // triangle per fragment.		  


        void main()                    		// The entry point for our fragment shader.
        {
            gl_FragColor = v_Color;     		// Pass the color directly through the pipeline.		  
        }
    }
}
