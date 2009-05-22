using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibAppJet.JavaScript.BCLImplementation.System
{

    [Script(Implements = typeof(global::System.Action<>))]
    internal delegate void __Action<T>(T t);

    // implements System.Core
    // should be defined in ScriptCoreLib.Query but
    // this assembly needs to use them

    [Script(Implements = typeof(global::System.Action))]
    internal delegate void __Action();

    [Script(Implements = typeof(global::System.Action<,>))]
    internal delegate void __Action<TArg0, TArg1>(TArg0 arg0, TArg1 arg1);

    [Script(Implements = typeof(global::System.Action<,,>))]
    internal delegate void __Action<TArg0, TArg1, TArg2>(TArg0 arg0, TArg1 arg1, TArg2 arg2);

    [Script(Implements = typeof(global::System.Action<,,,>))]
    internal delegate void __Action<TArg0, TArg1, TArg2, TArg3>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3);

}
