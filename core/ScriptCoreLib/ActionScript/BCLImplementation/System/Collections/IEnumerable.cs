using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Collections
{
    [Script(Implements = typeof(global::System.Collections.IEnumerable))]
    public interface __IEnumerable
    {
        __IEnumerator GetEnumerator();
    }
}
