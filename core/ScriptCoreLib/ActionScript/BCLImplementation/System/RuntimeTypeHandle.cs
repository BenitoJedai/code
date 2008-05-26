using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.RuntimeTypeHandle))]
    internal class __RuntimeTypeHandle
    {
        // http://bugs.adobe.com/jira/browse/ASC-2677
        public IntPtr Value { get; /* private */ set; }

        public static explicit operator __RuntimeTypeHandle(Class _ptr)
        {
            return new __RuntimeTypeHandle { Value = (IntPtr)(object) new __IntPtr { ClassToken = _ptr } };
        }
    }
}
