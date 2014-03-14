using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestUInt64ShrUn
{
    public class Class1
    {
        public static void foo()
        {
            //b = 1095216660480;
            //c = (b >>> 4);

            //IL_0000:  nop
            //IL_0001:  ldc.i8     0xff00000000
            //IL_000a:  stloc.0
            //IL_000b:  ldloc.0
            //IL_000c:  ldc.i4.4
            //IL_000d:  shr.un
            //IL_000e:  stloc.1

            ulong x = 0xff00000000;

            //   c = (b / Math.pow(2, 4));
            var y = x >> 4;

        }
    }
}
