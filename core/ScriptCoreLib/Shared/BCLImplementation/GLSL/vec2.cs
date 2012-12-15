using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ScriptCoreLib.Shared.BCLImplementation.GLSL
{
    using genType = Single;
    using thisType = __vec2;

    // http://msdn.microsoft.com/en-us/library/microsoft.xna.framework.vector3_members.aspx
    // http://www.lighthouse3d.com/tutorials/glsl-tutorial/data-types-and-variables/


    [Script(Implements = typeof(global::ScriptCoreLib.GLSL.vec2))]
    [Description("Shader vec2 for other platforms.")]
    public
        // does jsc already support struct for all platforms?
        class __vec2
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

        public __vec2() : this(0, 0)
        {

        }

        public __vec2(genType x, genType y)
        {
            this._x = x;
            this._y = y;
        }


        public override string ToString()
        {
            return new { x, y }.InternalToString();
        }
    }

}
