using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Threading
{
    // http://msdn.microsoft.com/en-us/library/system.threading.interlocked(v=vs.110).aspx
    // http://referencesource.microsoft.com/#mscorlib/system/threading/Interlocked.cs
    // https://github.com/Microsoft/referencesource/blob/master/mscorlib/system/Interlocked.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Threading/Interlocked.cs

    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Threading/Interlocked.cs
    // https://github.com/kswoll/WootzJs/blob/master/WootzJs.Runtime/Threading/Interlocked.cs

    [Script(Implements = typeof(global::System.Threading.Interlocked))]
    public static class __Interlocked
    {
        // with a service worker and web workers
        // we could start concurrency


        // used by
        // https://github.com/dotnet/corefx/blob/master/src/System.Collections.Concurrent/src/System/Collections/Concurrent/ConcurrentStack.cs


        //  internal static extern int CompareExchange(ref int location1, int value, int comparand, ref bool succeeded);
    }
}
