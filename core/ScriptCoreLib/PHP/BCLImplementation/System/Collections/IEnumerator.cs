using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Collections
{
    [Script(Implements = typeof(global::System.Collections.IEnumerator))]
    internal interface __IEnumerator
    {
        object Current { get; }

        bool MoveNext();

        void Reset();
    }
}
