using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.RuntimeTypeHandle))]
    internal sealed class __RuntimeTypeHandle
    {
        // is this used?
        public __RuntimeTypeHandle()
        {

        }

        // special method invoked on typeof(Type) statement
        public __RuntimeTypeHandle(IntPtr e)
        {
            this.Value = e;
        }


        public IntPtr Value
        {
            get;
            set;
        }

        public static implicit operator RuntimeTypeHandle(__RuntimeTypeHandle e)
        {
            return (RuntimeTypeHandle)(object)e;
        }
    }
}
