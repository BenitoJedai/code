using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace GenericDefaultPrimitive
{
    public class Class1
    {
        public Class1()
        {
            //IL_0000:  ldarg.0
            //IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
            //IL_0006:  nop
            //IL_0007:  nop
            //IL_0008:  call       !!0 GenericDefaultPrimitive.Class2::FirstOrDefault<int64>()
            //IL_000d:  stloc.0
            //IL_000e:  nop
            //IL_000f:  ret


            // did we unbox null into 0?
            var x = Class2.FirstOrDefault<long>();

            //long num0;

            //num0 = Class2.<Long>FirstOrDefault();

        }
    }

    public class Class2
    {
        public static T FirstOrDefault<T>()
        {
            // sending generiv null
            return default(T);
        }
    }
}
