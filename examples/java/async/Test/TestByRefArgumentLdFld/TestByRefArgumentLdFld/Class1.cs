using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace TestByRefArgumentLdFld
{
    public class Class1
    {
        public int state;

        public static void foo(ref Class1 r)
        {
            var s = r.state;

            // http://msdn.microsoft.com/en-us/library/system.reflection.emit.opcodes.ldind_ref(v=vs.110).aspx
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140216


            //public static  void foo(Class1[] ref_arg1)
            //{
            //    int num0;

            //    num0 = ref_arg1[0].state;
            //}

            //.method public hidebysig static void  foo(class TestByRefArgumentLdFld.Class1& r) cil managed
            //{
            //  // Code size       10 (0xa)
            //  .maxstack  1
            //  .locals init ([0] int32 s)
            //  IL_0000:  nop
            //  IL_0001:  ldarg.0
            //  IL_0002:  ldind.ref
            //  IL_0003:  ldfld      int32 TestByRefArgumentLdFld.Class1::state
            //  IL_0008:  stloc.0
            //  IL_0009:  ret
            //} // end of method Class1::foo

        }

        public static void goo(ref Class1 r)
        {
            //.method public hidebysig static void  goo(class TestByRefArgumentLdFld.Class1& r) cil managed
            //{
            //  // Code size       9 (0x9)
            //  .maxstack  8
            //  IL_0000:  nop
            //  IL_0001:  ldarg.0
            //  IL_0002:  call       void TestByRefArgumentLdFld.Class1::foo(class TestByRefArgumentLdFld.Class1&)
            //  IL_0007:  nop
            //  IL_0008:  ret
            //} // end of method Class1::goo



            foo(ref r);

        }
    }
}
