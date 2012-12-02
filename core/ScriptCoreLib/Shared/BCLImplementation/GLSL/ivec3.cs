using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ScriptCoreLib.Shared.BCLImplementation.GLSL
{
    using genType = Int32;
    using thisType = __ivec3;

    // http://msdn.microsoft.com/en-us/library/microsoft.xna.framework.vector3_members.aspx
    // http://www.lighthouse3d.com/tutorials/glsl-tutorial/data-types-and-variables/

    // global::LinqToSqlShared.Mapping. ?

    [Script(Implements = typeof(global::ScriptCoreLib.GLSL.ivec3))]
    [Description("Shader ivec3 for other platforms.")]
    internal
        // does jsc already support struct for all platforms?
        class __ivec3
    {
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

        public __ivec3()
            : this(0, 0, 0)
        {

        }

        public __ivec3(genType x, genType y, genType z)
        {
            this._x = x;
            this._y = y;
            this._z = z;
        }


        public override string ToString()
        {
            return new { x, y, z }.ToString();
        }
    }

}
