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
        // http://webglreport.com/

        // WebGL 1.0 supports tokens up to 256 characters in length. WebGL 2.0 follows The OpenGL ES Shading Language,
        // Version 3.00 (OpenGL ES 3.0.3 §3.8) and allows tokens up to 1024 characters in length. 
        // Shaders containing tokens longer than 1024 characters must fail to compile.

        // "X:\opensource\android-ndk-r10c\sources\android\ndk_helper\shader.cpp"

        // There's no way to bail out of the loop early, at least with OpenGL ES 2.0 (WebGL) shaders. We can't break or do any sort of branching on the loop variable
        // http://nullprogram.com/blog/2014/06/01/


    }
}
