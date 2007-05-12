using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{

    [Script(Implements = typeof(global::System.WeakReference))]
    internal class __WeakReference
    {
        public __WeakReference(object e)
        {
            // weak reference not supported
        }
    }
}
