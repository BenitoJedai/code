using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    using genType = Single;
    using System.Runtime.InteropServices;

    [Script(IsNative = true)]
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

        public static vec3 operator *(vec3 x, float y)
        {
            throw new NotImplementedException();
        }

        public static vec3 operator *(float x, vec3 y)
        {
            throw new NotImplementedException();
        }

        public vec3(genType x, genType y, genType z)
        {
            throw new NotImplementedException();
        }
    }
}
