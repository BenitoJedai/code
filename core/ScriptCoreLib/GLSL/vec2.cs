using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ScriptCoreLib.GLSL
{
    using genType = Single;

    /// <summary>
    /// a two component floating-point vector
    /// 
    /// Floating-point vector variables can be used to store a variety
    /// of things that are very useful in computer graphics: colors, normals, positions, texture coordinates, texture
    /// lookup results and the like.
    /// </summary>
    [Script]
    [StructLayout(LayoutKind.Explicit)]
    public struct vec2
    {
        #region [0]
        [FieldOffset(0)]
        public genType x;
        [FieldOffset(0)]
        public genType r;
        [FieldOffset(0)]
        public genType s;
        #endregion

        #region [1]
        [FieldOffset(sizeof(genType) * 1)]
        public genType y;
        [FieldOffset(sizeof(genType) * 1)]
        public genType g;
        [FieldOffset(sizeof(genType) * 1)]
        public genType t;
        #endregion

        public genType this[int i]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
