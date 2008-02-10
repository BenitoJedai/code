using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Collections
{
    [Script(Implements = typeof(global::System.Collections.IEnumerator))]
    public interface __IEnumerator
    {
        object Current { get; }

        bool MoveNext();

        void Reset();
    }
}
