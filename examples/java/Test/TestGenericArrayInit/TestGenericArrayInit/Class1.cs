using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace TestGenericArrayInit
{


    public class xTuple
    {
        //new TestGenericArrayInit.xTuple_2<Object, Integer>[1][0] = xTuple.<Object, Integer>Create(null, 0);
        //Class1.Target = new TestGenericArrayInit.xTuple_2<Object, Integer>[1];

        internal static xTuple<T, T2> Create<T, T2>(T v1, T2 v2)
        {
            return new xTuple<T, T2>();
        }
    }
    public class xTuple<T, T2>
    {
    }

    public class Class1
    {

        //    tuple_2Array0 = new TestGenericArrayInit.xTuple_2<Object, Integer>[] {
        //        xTuple.<Object, Integer>Create(null, 0)
        //    };
        //Class1.Target = tuple_2Array0;

        // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRTupleArrayLast\TestJVMCLRTupleArrayLast\Program.cs
        public static xTuple<object, int>[] Target = new[] { xTuple.Create(default(object), 0) };
    }
}
