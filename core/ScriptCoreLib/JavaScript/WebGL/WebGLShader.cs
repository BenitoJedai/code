using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/canvas/WebGLShader.cpp
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/canvas/WebGLShader.h
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/canvas/WebGLShader.idl

    [Script(HasNoPrototype = true)]
    public class WebGLShader
    {
        // "X:\opensource\android-ndk-r10c\sources\android\ndk_helper\shader.cpp"

        // There's no way to bail out of the loop early, at least with OpenGL ES 2.0 (WebGL) shaders. We can't break or do any sort of branching on the loop variable
        // http://nullprogram.com/blog/2014/06/01/


    }
}
