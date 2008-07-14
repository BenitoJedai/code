using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.CSharp2.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Func<>))]
    internal delegate T __Func<T>();

    [Script(Implements = typeof(global::System.Func<,>))]
    internal delegate TResult __Func<TArg0, TResult>(TArg0 arg0);

    [Script(Implements = typeof(global::System.Func<,,>))]
    internal delegate TResult __Func<TArg0, TArg1, TResult>(TArg0 arg0, TArg1 arg1);

    [Script(Implements = typeof(global::System.Func<,,,>))]
    internal delegate TResult __Func<TArg0, TArg1, TArg2, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2);

    [Script(Implements = typeof(global::System.Func<,,,,>))]
    internal delegate TResult __Func<TArg0, TArg1, TArg2, TArg3, TResult>(TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3);

}
