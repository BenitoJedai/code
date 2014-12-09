using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace TestGenericArrayRuntimeLength
{
    public class Class1 //: ScriptCoreLibJava.IAssemblyReferenceToken
    {
        // X:\jsc.svn\examples\java\hybrid\Test\TestGenericArray\TestGenericArray\Program.cs

        public Class1(object[] args)
        {

            //TestGenericArray.Class1_1<Class1>[] class1_1Array0;

            //class1_1Array0 = (TestGenericArray.Class1_1<Class1>[])__Array.CreateInstance(__Type.GetTypeFromHandle(__RuntimeTypeHandle.op_Explicit(TestGenericArray.Class1_1.class)), ((int)(args.length)));
            //         class1_1Array0 = new TestGenericArrayRuntimeLength.Class1_1<Class1>[((int)(args.length))];
            var a = new Class1<Class1>[args.Length];
            //var a = new TResult[tasks.Length];

            //TestGenericArray.Class1_1<Class1>[] class1_1Array0;

            //class1_1Array0 = (TestGenericArray.Class1_1<Class1>[])Object(TestGenericArray.Class1_1<Class1>, 1);
            // class1_1Array0 = (TestGenericArray.Class1_1<Class1>[])__Array.CreateInstance(__Type.GetTypeFromHandle(__RuntimeTypeHandle.op_Explicit(TestGenericArray.Class1_1.class)), 1);

        }

        //.method public hidebysig specialname rtspecialname 
        //        instance void  .ctor() cil managed
        //{
        //  // Code size       17 (0x11)
        //  .maxstack  1
        //  .locals init ([0] class TestGenericArray.Class1`1<class TestGenericArray.Class1>[] a)
        //  IL_0000:  ldarg.0
        //  IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
        //  IL_0006:  nop
        //  IL_0007:  nop
        //  IL_0008:  ldc.i4.1
        //  IL_0009:  newarr     class TestGenericArray.Class1`1<class TestGenericArray.Class1>
        //  IL_000e:  stloc.0
        //  IL_000f:  nop
        //  IL_0010:  ret
        //} // end of method Class1::.ctor
    }

    public class Class1<T>
    {
    }
}
