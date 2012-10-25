using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestThrowWithoutReturn
{
    internal static partial class __Enumerable
    {

        public static object Range(int start, int count)
        {
            long num;
            num = (((long)start) + ((long)count)) - ((long)1);
            if (count < 0)
            {

            }
            else if (num <= ((long)0x7fffffff))
            {
                return null;
            }

            throw null;
        }
    }
}
