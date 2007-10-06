using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Action))]
    internal delegate void __Action();

    /* Defined in mscorlib.dll
     * 
    [Script(Implements = typeof(global::System.Action<>))]
    internal delegate void __Action<T>();
    */
    [Script(Implements = typeof(global::System.Action<>))]
    internal delegate void __Action<TArg0>(TArg0 arg0);

    [Script(Implements = typeof(global::System.Action<,>))]
    internal delegate void __Action<TArg0, TArg1>(TArg0 arg0, TArg1 arg1);

    [Script(Implements = typeof(global::System.Action<,,>))]
    internal delegate void __Action<TArg0, TArg1, TArg2>(TArg0 arg0, TArg1 arg1, TArg2 arg2);


}
