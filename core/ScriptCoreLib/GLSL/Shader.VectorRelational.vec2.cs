using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    using ivec = ivec2;
    using bvec = bvec2;
    using vec = vec2;

    partial class Shader
    {
        /// <summary>
        /// Returns the component-wise compare of x < y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected bvec lessThan(vec x, vec y) { throw new NotImplementedException(); }
        /// <summary>
        /// Returns the component-wise compare of x < y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected bvec lessThan(ivec x, ivec y) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns the component-wise compare of x <= y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected bvec lessThanEqual(vec x, vec y) { throw new NotImplementedException(); }
        /// <summary>
        /// Returns the component-wise compare of x <= y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected bvec lessThanEqual(ivec x, ivec y) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns the component-wise compare of x > y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected bvec greaterThan(vec x, vec y) { throw new NotImplementedException(); }
        /// <summary>
        /// Returns the component-wise compare of x > y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected bvec greaterThan(ivec x, ivec y) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns the component-wise compare of x >= y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected bvec greaterThanEqual(vec x, vec y) { throw new NotImplementedException(); }
        /// <summary>
        /// Returns the component-wise compare of x >= y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected bvec greaterThanEqual(ivec x, ivec y) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns the component-wise compare of x == y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected bvec equal(vec x, vec y) { throw new NotImplementedException(); }
        /// <summary>
        /// Returns the component-wise compare of x == y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected bvec equal(ivec x, ivec y) { throw new NotImplementedException(); }
        /// <summary>
        /// Returns the component-wise compare of x == y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected bvec equal(bvec x, bvec y) { throw new NotImplementedException(); }
        /// <summary>
        /// Returns the component-wise compare of x != y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected bvec notEqual(vec x, vec y) { throw new NotImplementedException(); }
        /// <summary>
        /// Returns the component-wise compare of x != y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected bvec notEqual(ivec x, ivec y) { throw new NotImplementedException(); }
        /// <summary>
        /// Returns the component-wise compare of x != y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected bvec notEqual(bvec x, bvec y) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns true if any component of x is true.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected bool any(bvec x) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns true only if all components of x are true.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected bool all(bvec x) { throw new NotImplementedException(); }
        /// <summary>
        /// Returns the component-wise logical complement of x.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected bvec not(bvec x) { throw new NotImplementedException(); }
    }
}
