using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace TestInt32Div
{
    public static class Class1
    {
        // X:\jsc.svn\examples\javascript\test\TestIntegerDivOpcode\TestIntegerDivOpcode\Class1.cs
        // X:\jsc.svn\examples\javascript\Test\TestIntegerDiv\TestIntegerDiv\Application.cs


        //IL_0000:  nop
        //IL_0001:  ldc.i4.4

        //IL_0002:  ldstr      "RENJTQ=="
        //IL_0007:  call instance int32[mscorlib] System.String::get_Length()

        //IL_000c:  mul
        //IL_000d:  ldc.i4.3
        //IL_000e:  div

        //IL_000f:  stloc.0
        //IL_0010:  ret

        // /

        //      IL_0000:  nop
        //      IL_0001:  ldc.i4.s   10
        //IL_0003:  stloc.0
        //IL_0004:  ret

        static void Invoke(int Length)
        {
            // jsc should just know the length with the actual string?


            //var capacity = 4 * "RENJTQ==".Length / 3;
            var capacity = 4 * Length / 3;

            //     d = (~~(b / c));


            //     c = (((4 * b) / 3));
            // 10
        }
    }
}
