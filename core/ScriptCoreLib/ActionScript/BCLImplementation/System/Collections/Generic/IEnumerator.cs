using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.IEnumerator<>))]
    public interface __IEnumerator<T> : __IEnumerator, IDisposable
    {
        T Current { get; }
    }
}
