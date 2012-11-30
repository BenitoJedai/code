using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace ScriptCoreLib.GLSL
{
    using genType = Int32;
    using thisType = ivec3;

    // http://msdn.microsoft.com/en-us/library/microsoft.xna.framework.vector3_members.aspx
    // http://www.lighthouse3d.com/tutorials/glsl-tutorial/data-types-and-variables/

    [Script(IsNative = true)]
    [StructLayout(LayoutKind.Explicit)]
    public struct ivec3
    {
        #region [0]
        [FieldOffset(0)]
        genType _x;
        public genType x { get { return _x; } set { _x = value; } }

        [FieldOffset(0)]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public genType r;
        [FieldOffset(0)]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public genType s;
        #endregion



        #region [1]
        [FieldOffset(sizeof(genType) * 1)]
        genType _y;
        public genType y { get { return _y; } set { _y = value; } }

        [FieldOffset(sizeof(genType) * 1)]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public genType g;
        [FieldOffset(sizeof(genType) * 1)]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public genType t;
        #endregion

        #region [2]
        [FieldOffset(sizeof(genType) * 2)]
        genType _z;
        public genType z { get { return _z; } set { _z = value; } }

        [FieldOffset(sizeof(genType) * 2)]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public genType b;
        [FieldOffset(sizeof(genType) * 2)]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public genType p;
        #endregion

        #region []
        public vec2 this[int i]
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
        #endregion


        public static ivec3 operator *(ivec3 x, genType y)
        {
            throw new NotImplementedException();
        }

        public static ivec3 operator *(genType x, ivec3 y)
        {
            throw new NotImplementedException();
        }

        public static ivec3 operator +(ivec3 x, ivec3 y)
        {
            throw new NotImplementedException();
        }

        public static ivec3 operator -(genType x, ivec3 y)
        {
            throw new NotImplementedException();
        }

        public static ivec3 operator -(ivec3 x, ivec3 y)
        {
            throw new NotImplementedException();
        }


        public static ivec3 operator -(ivec3 x)
        {
            throw new NotImplementedException();
        }

        public static ivec3 operator -(ivec3 x, float y)
        {
            throw new NotImplementedException();
        }



        public static ivec3 operator +(ivec3 x, float y)
        {
            throw new NotImplementedException();
        }

        public static ivec3 operator /(ivec3 x, float y)
        {
            throw new NotImplementedException();
        }




        public static ivec3 operator *(ivec3 x, ivec3 y)
        {
            throw new NotImplementedException();
        }

        public static ivec3 operator *(mat3 x, ivec3 y)
        {
            throw new NotImplementedException();
        }

        public ivec3(genType x, genType y, genType z)
        {
            throw new NotImplementedException();
        }



        [Browsable(false)]
        public vec2 yz { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        [Browsable(false)]
        public vec2 yx { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        [Browsable(false)]
        public vec2 xy { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        [Browsable(false)]
        public vec2 yy { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }

        [Browsable(false)]
        public ivec3 rgb { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        [Browsable(false)]
        public ivec3 yzx { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        [Browsable(false)]
        public ivec3 xyz { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        [Browsable(false)]
        public ivec3 zxy { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        [Browsable(false)]
        public ivec3 wyz { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        [Browsable(false)]
        public ivec3 xzx { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        [Browsable(false)]
        public ivec3 zzx { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }


        [Browsable(false)]
        public ivec3 xxx { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        [Browsable(false)]
        public ivec3 yyy { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }

        [Browsable(false)]
        public vec4 xxxx { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        [Browsable(false)]
        public vec4 yyyy { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        [Browsable(false)]
        public vec4 zzzz { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }

        public override string ToString()
        {
            return new { x, y, z }.ToString();
        }
    }
}
