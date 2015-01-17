using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Concurrent
{
    // http://referencesource.microsoft.com/#mscorlib/system/Collections/Concurrent/ConcurrentStack.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Collections\Generic\Stack.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Collections\Concurrent\ConcurrentStack.cs
    // https://github.com/dotnet/corefx/blob/master/src/System.Collections.Concurrent/src/System/Collections/Concurrent/ConcurrentStack.cs


    [Script(Implements = typeof(global::System.Collections.Concurrent.ConcurrentStack<>))]
    internal class __ConcurrentStack<T>
    {
        // should jsc exclude placeholder types?
        // unles user apps seem to need such partial types?
        // therwise its excess baggage?

        // would concurrent work accross devices over lan?
    }
}
