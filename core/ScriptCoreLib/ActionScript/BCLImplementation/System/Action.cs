using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Action<>))]
    internal delegate void __Action<T>(T t);

    [Script(Implements = typeof(global::System.Action<,>))]
    internal delegate void __Action<A, B>(A a, B b);
}
