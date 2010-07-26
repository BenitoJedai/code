using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    // where the input arguments (and corresponding output)
    // can be float, vec2, vec3, or vec4, genType is used as the argument.

    using genType = vec4;

    partial class Shader
    {
        /// <summary>
        /// Returns x if x >= 0, otherwise it returns –x.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected genType abs(genType x) { throw new NotImplementedException(); }
        /// <summary>
        /// Returns 1.0 if x > 0, 0.0 if x = 0, or –1.0 if x < 0
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected genType sign(genType x) { throw new NotImplementedException(); }
        /// <summary>
        /// Returns a value equal to the nearest integer that is less than or equal to x
        /// </summary>
        protected genType floor(genType x) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns a value equal to the nearest integer that is greater than or equal to x
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected genType ceil(genType x) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns x – floor (x)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected genType fract(genType x) { throw new NotImplementedException(); }

        /// <summary>
        /// Modulus (modulo). Returns x – y * floor (x/y)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected genType mod(genType x, float y) { throw new NotImplementedException(); }

        /// <summary>
        /// Modulus. Returns x – y * floor (x/y)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected genType mod(genType x, genType y) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns y if y < x, otherwise it returns x
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected genType min(genType x, genType y) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns y if y < x, otherwise it returns x
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected genType min(genType x, float y) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns y if x < y, otherwise it returns x.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected genType max(genType x, genType y) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns y if x < y, otherwise it returns x.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected genType max(genType x, float y) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns min (max (x, minVal), maxVal)
        /// Results are undefined if minVal > maxVal.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="minVal"></param>
        /// <param name="maxVal"></param>
        /// <returns></returns>
        protected genType clamp(genType x,
        genType minVal,
        genType maxVal) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns min (max (x, minVal), maxVal)
        /// Results are undefined if minVal > maxVal.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="minVal"></param>
        /// <param name="maxVal"></param>
        /// <returns></returns>
        protected genType clamp(genType x,
        float minVal,
        float maxVal) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns the linear blend of x and y, i.e. x
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        protected genType mix(genType x,
        genType y,
        genType a) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns the linear blend of x and y, i.e. x
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        protected genType mix(genType x,
        genType y,
        float a) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns 0.0 if x < edge, otherwise it returns 1.0
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        protected genType step(genType edge, genType x) { throw new NotImplementedException(); }
        /// <summary>
        /// Returns 0.0 if x < edge, otherwise it returns 1.0
        /// </summary>
        /// <param name="edge0"></param>
        /// <param name="edge1"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        protected genType step(float edge, genType x) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns 0.0 if x &lt;= edge0 and 1.0 if x &gt;= edge1 and
        /// performs smooth Hermite interpolation between 0 and 1
        /// when edge0 &lt; x &lt; edge1. This is useful in cases where
        /// you would want a threshold function with a smooth
        /// transition. This is equivalent to:
        /// genType t;
        /// t = clamp ((x – edge0) / (edge1 – edge0), 0, 1);
        /// return t * t * (3 – 2 * t);
        /// Results are undefined if edge0 &gt;= edge1.
        /// </summary>
        /// <param name="edge0"></param>
        /// <param name="edge1"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        protected genType smoothstep(genType edge0,
        genType edge1,
        genType x) { throw new NotImplementedException(); }

        /// <summary>
        /// <summary>
        /// Returns 0.0 if x &lt;= edge0 and 1.0 if x &gt;= edge1 and
        /// performs smooth Hermite interpolation between 0 and 1
        /// when edge0 &lt; x &lt; edge1. This is useful in cases where
        /// you would want a threshold function with a smooth
        /// transition. This is equivalent to:
        /// genType t;
        /// t = clamp ((x – edge0) / (edge1 – edge0), 0, 1);
        /// return t * t * (3 – 2 * t);
        /// Results are undefined if edge0 &gt;= edge1.
        /// </summary>
        /// </summary>
        protected genType smoothstep(float edge0,
                float edge1,
                genType x) { throw new NotImplementedException(); }

    }
}
