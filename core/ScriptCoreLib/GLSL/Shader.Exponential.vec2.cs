using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    using genType = vec2;

    partial class Shader
    {
        /// <summary>
        /// Returns x raised to the y power
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected genType pow(genType x, genType y) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns the natural exponentiation of x,
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected genType exp(genType x) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns the natural logarithm of x,
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected genType log(genType x) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns 2 raised to the x power
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected genType exp2(genType x) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns the base 2 logarithm of x
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected genType log2(genType x) { throw new NotImplementedException(); }

        protected genType sqrt(genType x) { throw new NotImplementedException(); }

        protected genType inversesqrt(genType x) { throw new NotImplementedException(); }
    }
}
