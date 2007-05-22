using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.RuntimeTypeHandle))]
    internal sealed class __RuntimeTypeHandle
    {
        public __RuntimeTypeHandle()
        {

        }

        // special method invoked on typeof(Type) statement
        public __RuntimeTypeHandle(IntPtr e)
        {
            _Value = e;
        }

        private IntPtr _Value;

        public IntPtr Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public static implicit operator RuntimeTypeHandle(__RuntimeTypeHandle e)
        {
            return (RuntimeTypeHandle)(object)e;
        }
    }
}
