using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    using genType = Single;
    using System.Runtime.InteropServices;

    // http://msdn.microsoft.com/en-us/library/microsoft.xna.framework.vector3_members.aspx

    [Script(IsNative = true)]
    [StructLayout(LayoutKind.Explicit)]
    public struct vec3
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

        #region [2]
        [FieldOffset(sizeof(genType) * 2)]
        public genType z;
        [FieldOffset(sizeof(genType) * 2)]
        public genType b;
        [FieldOffset(sizeof(genType) * 2)]
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


        public static vec3 operator *(vec3 x, genType y)
        {
            throw new NotImplementedException();
        }

        public static vec3 operator *(genType x, vec3 y)
        {
            throw new NotImplementedException();
        }

        public static vec3 operator +(vec3 x, vec3 y)
        {
            throw new NotImplementedException();
        }

        public static vec3 operator -(genType x, vec3 y)
        {
            throw new NotImplementedException();
        }

        public static vec3 operator -(vec3 x, vec3 y)
        {
            throw new NotImplementedException();
        }


        public static vec3 operator -(vec3 x)
        {
            throw new NotImplementedException();
        }

        public static vec3 operator -(vec3 x, float y)
        {
            throw new NotImplementedException();
        }



        public static vec3 operator +(vec3 x, float y)
        {
            throw new NotImplementedException();
        }

        public static vec3 operator /(vec3 x, float y)
        {
            throw new NotImplementedException();
        }




        public static vec3 operator *(vec3 x, vec3 y)
        {
            throw new NotImplementedException();
        }

        public static vec3 operator *(mat3 x, vec3 y)
        {
            throw new NotImplementedException();
        }

        public vec3(genType x, genType y, genType z)
        {
            throw new NotImplementedException();
        }



		// https://github.com/daeken/Shaderforth
		// Swizzling
		// EOpVectorSwizzle
		// https://chromium.googlesource.com/angle/angle/+/master/src/compiler/translator/OutputGLSLBase.cpp

		public vec2 xz { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public vec2 yz { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public vec2 yx { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public vec2 xy { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public vec2 yy { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }

        public vec3 rgb { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public vec3 yzx { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public vec3 xyz { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public vec3 zxy { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public vec3 wyz { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public vec3 xzx { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public vec3 zzx { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }


        public vec3 xxx { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public vec3 yyy { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }

        public vec4 xxxx { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public vec4 yyyy { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public vec4 zzzz { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }


    }


}
