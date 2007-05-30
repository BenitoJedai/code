using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.EventArgs))]
    internal class __EventArgs
    {
        public static readonly EventArgs Empty;

        static __EventArgs()
        {
            Empty = new __EventArgs();
        }

        #region
        static public implicit operator EventArgs(__EventArgs e)
        {
            return (EventArgs)(object)e;
        }

        static public implicit operator __EventArgs(EventArgs e)
        {
            return (__EventArgs)(object)e;
        }
        #endregion
    }
}
