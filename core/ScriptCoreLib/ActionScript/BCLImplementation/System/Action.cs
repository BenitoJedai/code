using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Action))]
    internal delegate void __Action();

    [Script(Implements = typeof(global::System.Action<>))]
    internal delegate void __Action<A>(A a);

    [Script(Implements = typeof(global::System.Action<,>))]
    internal delegate void __Action<A, B>(A a, B b);

    [Script(Implements = typeof(global::System.Action<,,>))]
    internal delegate void __Action<A, B, C>(A a, B b, C c);

    [Script(Implements = typeof(global::System.Action<,,,>))]
    internal delegate void __Action<A, B, C, D>(A a, B b, C c, D d);

}
