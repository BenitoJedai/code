using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(ImplementsViaAssemblyQualifiedName = "System.Tuple, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
    internal static class __Tuple
    {
        public static __Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
        {
            return new __Tuple<T1, T2>(item1, item2);
        }
    }

    [Script(ImplementsViaAssemblyQualifiedName = "System.Tuple`2, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
    internal class __Tuple<T1, T2>
    {
        public __Tuple(T1 item1, T2 item2)
        {
            this.Item1 = item1;
            this.Item2 = item2;
        }

        public T1 Item1 { get; internal set; }
        public T2 Item2 { get; internal set; }
    }
}
