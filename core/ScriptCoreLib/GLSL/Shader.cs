using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    public partial class Shader
    {
        [uniform]
        public gl_DepthRangeParameters gl_DepthRange;


        //
        // Implementation dependent constants. The example values below
        // are the minimum values allowed for these maximums.
        //
        [mediump]
        protected static readonly int gl_MaxVertexAttribs = 8;
        [mediump]
        protected static readonly int gl_MaxVertexUniformVectors = 128;
        [mediump]
        protected static readonly int gl_MaxVaryingVectors = 8;
        [mediump]
        protected static readonly int gl_MaxVertexTextureImageUnits = 0;
        [mediump]
        protected static readonly int gl_MaxCombinedTextureImageUnits = 8;
        [mediump]
        protected static readonly int gl_MaxTextureImageUnits = 8;
        [mediump]
        protected static readonly int gl_MaxFragmentUniformVectors = 16;
        [mediump]
        protected static readonly int gl_MaxDrawBuffers = 1;
    }
}
