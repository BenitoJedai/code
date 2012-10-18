using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.IEnumerator<>))]
    internal interface __IEnumerator<T> : __IEnumerator, IDisposable
    {
        T Current { get; }
    }
}
