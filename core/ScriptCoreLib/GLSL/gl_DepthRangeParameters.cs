using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    /// <summary>
    /// Depth range in window coordinates
    /// </summary>
    [Script]
    public struct gl_DepthRangeParameters
    {
        [highp]
        float near; // n
        [highp]
        float far; // f
        [highp]
        float diff; // f - n
    };
}
