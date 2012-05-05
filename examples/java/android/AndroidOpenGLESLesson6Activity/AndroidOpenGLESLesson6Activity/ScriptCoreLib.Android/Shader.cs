using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLib.Android
{
    [Script(Implements = typeof(ScriptCoreLib.GLSL.Shader))]
    internal class __Shader
    {
        public override string ToString()
        {
            return "/* GLSL shader source */";
        }
    }

    [Script(Implements = typeof(ScriptCoreLib.GLSL.FragmentShader))]
    internal class __FragmentShader : __Shader
    {

    }

    [Script(Implements = typeof(ScriptCoreLib.GLSL.VertexShader))]
    internal class __VertexShader : __Shader
    {

    }




    class WebGLRenderingContext
    {
        // let's try to mimic WebGL api and see how far we get
        // why is Android ES a static reference?

        // whats the most popular api looking at Y:\jsc.svn\examples\javascript\WebGLLesson16\WebGLLesson16\Application.cs ?


    }
}
