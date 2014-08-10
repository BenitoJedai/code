using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Collections
{
    // http://referencesource.microsoft.com/#mscorlib/system/collections/ienumerable.cs

    [Script(Implements = typeof(global::System.Collections.IEnumerable))]
    public interface __IEnumerable
    {
        // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Collections\Generic\IEnumerable.cs

        // vb com disp id -4. ah the memories.
        IEnumerator GetEnumerator();
    }
}
