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
        // what about System.Windows.Vector

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
