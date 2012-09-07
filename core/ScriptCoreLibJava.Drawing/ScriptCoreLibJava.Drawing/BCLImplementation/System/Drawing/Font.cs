using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Drawing;

namespace ScriptCoreLibJava.BCLImplementation.System.Drawing
{
    [Script(Implements = typeof(global::System.Drawing.Font))]
    internal class __Font
    {
        public readonly string _familyName;
        public readonly float _emSize;
        public readonly FontStyle _style;
        public readonly GraphicsUnit _unit;
        public readonly byte _gdiCharSet;

        public string KnownName;



        public __Font() : this("", 1, FontStyle.Regular, GraphicsUnit.Pixel, 0)
        {

        }

        public __Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet)
        {
            this._familyName = familyName;
            this._emSize = emSize;
            this._style = style;
            this._unit = unit;
            this._gdiCharSet = gdiCharSet;
        }

        #region operators
        static public implicit operator Font(__Font e)
        {
            return (Font)(object)e;
        }

        static public implicit operator __Font(Font e)
        {
            return (__Font)(object)e;
        }
        #endregion
    }
}
