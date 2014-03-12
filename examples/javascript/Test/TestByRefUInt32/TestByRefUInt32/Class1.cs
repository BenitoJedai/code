using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestByRefUInt32
{
    public class Class1
    {
        public static void Foo(uint u)
        {
        }

        public static void Foo(ref uint u)
        {
            //  public static function Foo_2536ce5c_06000002(ref_arg1:*):void
            // jsc shall use Vector<> for byrefs!

            //OpCodes.Ldind_I4,
            //OpCodes.Ldind_Ref

            // 1>script : error JSC1000: opcode unsupported - [0x0002] ldind.u4   +1 -1{[0x0001] ldarg.0    +1 -0}

            Foo(u);
        }

        public static void Foo()
        {
            var u = 9u;

            Foo(ref u);
        }
    }
}
