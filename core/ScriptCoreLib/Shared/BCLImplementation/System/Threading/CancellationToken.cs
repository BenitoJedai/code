using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Threading
{
    // http://referencesource.microsoft.com/#mscorlib/system/threading/CancellationToken.cs

    [Script(Implements = typeof(global::System.Threading.CancellationToken))]
    internal class __CancellationToken
    {
        // jsc does not yet allow to cancel a task
    }
}
