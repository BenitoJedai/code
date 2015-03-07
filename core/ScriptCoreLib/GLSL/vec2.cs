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
    [Script(IsNative = true)]
    [StructLayout(LayoutKind.Explicit)]
    public struct vec2
    {
        // "X:\opensource\android-ndk-r10c\sources\android\ndk_helper\vecmath.h"

        // see also: http://glm.g-truc.net/code.html

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

        #region []
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
        #endregion





        public static vec2 operator *(vec2 x, float y)
        {
            throw new NotImplementedException();
        }

        public static vec2 operator *(float x, vec2 y)
        {
            throw new NotImplementedException();
        }

        public static vec2 operator *(vec2 x, vec2 y)
        {
            throw new NotImplementedException();
        }

        public static vec2 operator /(vec2 x, float y)
        {
            throw new NotImplementedException();
        }

        public static vec2 operator /(vec2 x, vec2 y)
        {
            throw new NotImplementedException();
        }

        public static vec2 operator +(vec2 x, float y)
        {
            throw new NotImplementedException();
        }

        public static vec2 operator +(float y, vec2 x) { throw new NotImplementedException(); }
        public static vec2 operator -(vec2 y, float x) { throw new NotImplementedException(); }

        public static vec2 operator +(vec2 y, vec2 x)
        {
            throw new NotImplementedException();
        }

        public static vec2 operator -(vec2 x)
        {
            throw new NotImplementedException();
        }

        //x,y,z,w	Useful for points, vectors, normals
        //r,g,b,a	Useful for colors
        //s,t,p,q	Useful for texture coordinates

        // Dot products are to be to be generated!
        public vec2 xy { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        
        public vec3 xxx { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public vec3 yyy { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        
        public vec4 xxxx { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public vec4 yyyy { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }

    }
}
