using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{
	[Script(Implements = typeof(ScriptCoreLib.GLSL.Shader))]
    internal class __Shader
    {
        // http://visualstudio.uservoice.com/forums/121579-visual-studio/suggestions/6230276-add-syntax-highlighting-and-intellisense-for-shad

        public override string ToString()
        {
            return "/* GLSL shader source */";
        }
    }
}
