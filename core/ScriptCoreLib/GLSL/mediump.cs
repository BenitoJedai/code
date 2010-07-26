using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    // http://www.khronos.org/registry/gles/specs/2.0/GLSL_ES_Specification_1.0.17.pdf

   



    /// <summary>
    /// Satisfies the minimum requirements above for the fragment language. Its
    /// range and precision has to be greater than or the same as provided by lowp
    /// and less than or the same as provided by highp.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class mediump : Attribute
    {

    }
}
