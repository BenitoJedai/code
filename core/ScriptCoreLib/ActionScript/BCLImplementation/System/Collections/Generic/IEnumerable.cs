using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.IEnumerable<>))]
    public interface __IEnumerable<T> : __IEnumerable
    {
        IEnumerator<T> GetEnumerator();
    }
}
