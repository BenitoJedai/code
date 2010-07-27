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
        /// Converts degrees to radians,
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        protected genType radians(genType degrees) { throw new NotImplementedException(); }

        /// <summary>
        /// Converts radians to degrees,
        /// </summary>
        /// <param name="radians"></param>
        /// <returns></returns>
        protected genType degrees(genType radians) { throw new NotImplementedException(); }

        /// <summary>
        /// The standard trigonometric sine function
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        protected genType sin(genType angle) { throw new NotImplementedException(); }

        /// <summary>
        /// The standard trigonometric cosine function.
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        protected genType cos(genType angle) { throw new NotImplementedException(); }

        /// <summary>
        /// The standard trigonometric tangent.
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        protected genType tan(genType angle) { throw new NotImplementedException(); }

        /// <summary>
        /// Arc sine. Returns an angle whose sine is x.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected genType asin(genType x) { throw new NotImplementedException(); }

        /// <summary>
        /// Arc cosine. Returns an angle whose cosine is x.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected genType acos(genType x) { throw new NotImplementedException(); }

        /// <summary>
        /// Arc tangent. Returns an angle whose tangent is y/x. The
        /// signs of x and y are used to determine what quadrant the
        /// angle is in.
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        protected genType atan(genType y, genType x) { throw new NotImplementedException(); }

        /// <summary>
        /// Arc tangent. Returns an angle whose tangent is
        /// y_over_x.
        /// </summary>
        /// <param name="y_over_x"></param>
        /// <returns></returns>
        protected genType atan(genType y_over_x) { throw new NotImplementedException(); }

    }
}
