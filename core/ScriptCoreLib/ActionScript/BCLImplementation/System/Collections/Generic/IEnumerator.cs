using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.IEnumerator<>))]
    internal interface __IEnumerator<T> : IEnumerator, IDisposable
    {
        T Current { get; }
    }
}
