using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
    // implements System.Core
    // should be defined in ScriptCoreLib.Query but
    // this assembly needs to use them
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

    [Script(ImplementsViaAssemblyQualifiedName = "System.Action`5, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
    internal delegate void __Action<A, B, C, D, E>(A a, B b, C c, D d, E e);

}
