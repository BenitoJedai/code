using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.GLSL;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.DOM
{
    //using gl = global::ScriptCoreLib.JavaScript.WebGL.WebGLRenderingContext;

    [Script]
    public static class WebGLExtensions
    {

    }

    [Script]
    public static class LocalMediaStreamExtensions
    {
        // .src instead?
        public static string ToObjectURL(this MediaStream e)
        {
            var src = (string)new IFunction("return window.URL.createObjectURL(this);").apply(e);

            return src;
        }
    }
}
