using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Collections;

namespace ScriptCoreLibJava.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.IEnumerator<>))]
    internal interface __IEnumerator<T> : IEnumerator, IDisposable
    {
        T Current { get; }
    }
}
