using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestTupleMembers
{
    [Script(Implements = typeof(Tuple<,>))]
    internal class __Tuple<T1, T2>
    {
        public __Tuple(T1 item1, T2 item2)
        {
            this.Item1 = item1;
            this.Item2 = item2;
        }

        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }

        static void Invoke(__Tuple<T1, T2> x, T1 t1)
        {
            x.Item1 = t1;
        }
    }


}
