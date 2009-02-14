using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Collections
{
    [Script(Implements = typeof(global::System.Collections.IEnumerable))]
    internal interface __IEnumerable
    {
        IEnumerator GetEnumerator();
    }
}
