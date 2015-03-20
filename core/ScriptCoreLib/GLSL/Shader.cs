﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    public partial class Shader
    {
		// http://msdn.microsoft.com/en-us/library/ie/dn611835(v=vs.85).aspx
		// see also: http://www.opengl.org/sdk/docs/manglsl/

		// https://www.opengl.org/sdk/docs/man/html/glDepthRange.xhtml
		[uniform]
        public gl_DepthRangeParameters gl_DepthRange;


		// 7.3 Built-In Constants

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
