using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.Cursors))]
    internal class __Cursors
    {
        static public Cursor Hand
        {
            get
            {
                return  new __Cursor { Value = ScriptCoreLib.JavaScript.DOM.IStyle.CursorEnum.pointer };
            }
        }
    }
}
