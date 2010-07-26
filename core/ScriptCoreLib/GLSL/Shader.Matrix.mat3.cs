using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.GLSL
{
    using mat = mat3;

    partial class Shader
    {
        /// <summary>
        /// Multiply matrix x by matrix y component-wise, i.e.,
        /// result[i][j] is the scalar product of x[i][j] and y[i][j].
        /// Note: to get linear algebraic matrix multiplication, use
        /// the multiply operator (*).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        protected mat matrixCompMult(mat x, mat y) { throw new NotImplementedException(); }

   
    }
}
