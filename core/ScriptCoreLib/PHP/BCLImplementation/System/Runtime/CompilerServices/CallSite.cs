using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Runtime.CompilerServices
{
    [Script(Implements = typeof(global::System.Runtime.CompilerServices.CallSite))]
    internal class __CallSite
    {
        public CallSiteBinder Binder { get; set; }
    }

    [Script(Implements = typeof(global::System.Runtime.CompilerServices.CallSite<>))]
    internal class __CallSite<T> : __CallSite
    {
        public static __CallSite<T> Create(CallSiteBinder binder)
        {
            throw new NotImplementedException("__CallSite.Create " + new { binder = binder.GetType().FullName });
        }
    }
}
