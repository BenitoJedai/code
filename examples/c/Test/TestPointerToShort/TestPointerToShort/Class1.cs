using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace TestPointerToShort
{
    unsafe struct FooSignal8
    {
        public int samplesLength;

        //     struct tag_unsigned short** samples;
        public ushort* samples;

        public long value8;

        // by using delegates in a struct, a pointer can no longer taken
        //public Action signal1;
    }
}
