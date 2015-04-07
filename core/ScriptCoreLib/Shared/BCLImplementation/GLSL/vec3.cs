using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.Extensions;

namespace ScriptCoreLib.Shared.BCLImplementation.GLSL
{
    using genType = Single;
    using thisType = __vec3;

    // http://msdn.microsoft.com/en-us/library/microsoft.xna.framework.vector3_members.aspx
    // http://www.lighthouse3d.com/tutorials/glsl-tutorial/data-types-and-variables/


    [Script(Implements = typeof(global::ScriptCoreLib.GLSL.vec3))]
    [Description("Shader vec3 for other platforms.")]
    public
        // does jsc already support struct for all platforms?
        class __vec3
    {
		// http://www.palladiumconsulting.com/2014/05/net-simd-api-thoughts/

		// System.Numerics.VectorMath.IsHardwareAccelerated 
		// http://www.drdobbs.com/windows/64-bit-simd-code-from-c/240168851
		// http://www.nuget.org/packages/Microsoft.Bcl.Simd
		// http://blogs.msdn.com/b/dotnet/archive/2014/04/07/the-jit-finally-proposed-jit-and-simd-are-getting-married.aspx
		// The hardware-dependent vector type is:
		//System.Numerics.Vector<T>
		// https://code.msdn.microsoft.com/windowsdesktop/SIMD-Sample-f2c8c35a

		// what about System.Windows.Vector
		// http://geekswithblogs.net/akraus1/archive/2014/04/04/155858.aspx

		#region [0]
		genType _x;
        public genType x { get { return _x; } set { _x = value; } }
        #endregion

        #region [1]
        genType _y;
        public genType y { get { return _y; } set { _y = value; } }
        #endregion

        #region [2]
        genType _z;
        public genType z { get { return _z; } set { _z = value; } }
        #endregion


        public __vec3()
            : this(0, 0, 0)
        {

        }

        public __vec3(genType x, genType y, genType z)
        {
            this._x = x;
            this._y = y;
            this._z = z;
        }


        public override string ToString()
        {
            return new { x, y, z }.InternalToString();
        }
    }

}
