using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/tuple.cs
    //[Script(ImplementsViaAssemblyQualifiedName = "System.Tuple, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
    [Script(Implements = typeof(Tuple))]
    public static class __Tuple
    {
        public static __Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
        {
            return new __Tuple<T1, T2>(item1, item2);
        }
    }

    //[Script(ImplementsViaAssemblyQualifiedName = "System.Tuple`2, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
    [Script(Implements = typeof(Tuple<,>))]
    public class __Tuple<T1, T2>
    {
        public __Tuple(T1 item1, T2 item2)
        {
            this.Item1 = item1;
            this.Item2 = item2;
        }

        public T1 Item1 { get; internal set; }
        public T2 Item2 { get; internal set; }
    }

    [Script(Implements = typeof(Tuple<,,>))]
    public class __Tuple<T1, T2,T3>
    {
        // x:\jsc.svn\core\scriptcorelib.extensions\scriptcorelib.extensions\query\experimental\queryexpressionbuilder.cs

        public __Tuple(T1 item1, T2 item2, T3 item3)
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
        }

        public T1 Item1 { get; internal set; }
        public T2 Item2 { get; internal set; }
        public T3 Item3 { get; internal set; }
    }
}
