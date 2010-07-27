using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    using genType = vec4;

    partial class Shader
    {

        /// <summary>
        /// Returns the length of vector x,
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected float length(genType x) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns the distance between p0 and p1,
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        protected float distance(genType p0, genType p1) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns the dot product of x and y,
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected float dot(genType x, genType y) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns the cross product of x and y,
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected genType cross(genType x, genType y) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns a vector in the same direction as x but with a
        /// length of 1.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected genType normalize(genType x) { throw new NotImplementedException(); }

        /// <summary>
        /// If dot(Nref, I) &lt; 0 return N, otherwise return –N.
        /// </summary>
        /// <param name="N"></param>
        /// <param name="I"></param>
        /// <param name="Nref"></param>
        /// <returns></returns>
        protected genType faceforward(genType N, genType I, genType Nref) { throw new NotImplementedException(); }

        /// <summary>
        /// For the incident vector I and surface orientation N,
        /// returns the reflection direction:
        /// I – 2 * dot(N, I) * N
        /// N must already be normalized in order to achieve the
        /// desired result.
        /// </summary>
        /// <param name="I"></param>
        /// <param name="N"></param>
        /// <returns></returns>
        protected genType reflect(genType I, genType N) { throw new NotImplementedException(); }


        /// <summary>
        /// For the incident vector I and surface normal N, and the
        /// ratio of indices of refraction eta, return the refraction
        /// vector.
        /// </summary>
        /// <param name="I"></param>
        /// <param name="N"></param>
        /// <param name="eta"></param>
        /// <returns></returns>
        protected genType refract(genType I, genType N, float eta) { throw new NotImplementedException(); }
    }
}
