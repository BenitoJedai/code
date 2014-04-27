using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing
{
    [Script(Implements = typeof(global::System.Drawing.Font))]
    public class __Font
    {
        public string Name { get; set; }
        public float Size { get; set; }


        internal readonly FontStyle _style;
        internal readonly GraphicsUnit _unit;
        internal readonly byte _gdiCharSet;

        internal string KnownName;


        public __Font()
        {

        }

        public __Font(string familyName, float emSize)
        {
            this.Name = familyName;
            this.Size = emSize;

        }

        // script: error JSC1000: Missing Script Attribute? Native constructor was invoked, please implement [System.Drawing.Font..ctor(System.String, System.Single, System.Drawing.FontStyle)]
        public __Font(string familyName, float emSize, FontStyle style)
            : this(familyName, emSize, style, default(GraphicsUnit), default(byte))
        {

        }

        public __Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet)
        {
            this.Name = familyName;
            this.Size = emSize;
            this._style = style;
            this._unit = unit;
            this._gdiCharSet = gdiCharSet;
        }

        public __Font(Font f, FontStyle style)
        {
            this.Name = f.Name;
            this.Size = f.Size;
            this._style = style;
            //this._unit = f.Unit;
            //this._gdiCharSet = f.GdiCharSet;


        }

        public bool Underline { get { return this._style == FontStyle.Underline; } }

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
