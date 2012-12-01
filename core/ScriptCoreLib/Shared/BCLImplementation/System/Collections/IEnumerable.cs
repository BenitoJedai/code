using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Collections
{
    [Script(Implements = typeof(global::System.Collections.IEnumerable))]
    public interface __IEnumerable
    {
        IEnumerator GetEnumerator();
    }
}
