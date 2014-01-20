using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using global::System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(Cursor))]
    internal class __Cursor
    {
        public DOM.IStyle.CursorEnum Value;

        public static Cursor Current
        {
            get { return Cursors.Default; }
            set
            {
                __Cursor cursor = value;
                Native.document.documentElement.style.cursor = cursor.Value;
            }
        }

        #region
        static public implicit operator Cursor(__Cursor e)
        {
            return (Cursor)(object)e;
        }

        static public implicit operator __Cursor(Cursor e)
        {
            return (__Cursor)(object)e;
        }
        #endregion
    }
}
