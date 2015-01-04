using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace Test453DefaultIntPtr
{
    public static class Class1
    {
        // return (~~0);

        public static IntPtr Invoke() => default(IntPtr);
    }
}
