using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ScriptCoreLibJava.BCLImplementation.System.Threading
{
    // http://referencesource.microsoft.com/#mscorlib/system/threading/synchronizationcontext.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Threading/SynchronizationContext.cs

    [Script(Implements = typeof(global::System.Threading.SynchronizationContext))]
    public class __SynchronizationContext
    {
        // used by ?

        public static void SetSynchronizationContext(SynchronizationContext s)
        {

        }
    }
}
