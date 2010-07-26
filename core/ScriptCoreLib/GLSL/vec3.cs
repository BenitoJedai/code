using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    using genType = Single;
    using System.Runtime.InteropServices;

    [Script]
    [StructLayout(LayoutKind.Explicit)]
    public struct vec3
    {
        [FieldOffset(0)]
        public genType x;
        [FieldOffset(0)]
        public genType r;
        [FieldOffset(0)]
        public genType s;

        [FieldOffset(sizeof(genType) * 1)]
        public genType y;
        [FieldOffset(sizeof(genType) * 1)]
        public genType g;
        [FieldOffset(sizeof(genType) * 1)]
        public genType t;

        [FieldOffset(sizeof(genType) * 2)]
        public genType z;
        [FieldOffset(sizeof(genType) * 2)]
        public genType b;
        [FieldOffset(sizeof(genType) * 2)]
        public genType p;
    }
}
