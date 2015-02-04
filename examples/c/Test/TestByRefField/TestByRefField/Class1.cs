using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: System.Reflection.Obfuscation(Feature = "script")]


namespace TestByRefField
{
    public struct Class1
    {
        public long value8;

        static void DoCallbackFoo(ref Class1 y)
        {
            var loc1 = y.value8;

            y.value8 = loc1;
        }
    }
}
