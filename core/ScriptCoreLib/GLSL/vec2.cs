using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    /// <summary>
    /// a two component floating-point vector
    /// 
    /// Floating-point vector variables can be used to store a variety
    /// of things that are very useful in computer graphics: colors, normals, positions, texture coordinates, texture
    /// lookup results and the like.
    /// </summary>
    [Script]
    public struct vec2
    {
        public float x;
        public float r;
        public float s;
	    
        public float y;
        public float g;
        public float t;
    }
}
