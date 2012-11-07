using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
[assembly: Obfuscation(Feature = "script")]

namespace TestAnoymousTypeArray
{
    public class Class1
    {
        public static T[] ToArray<T>(T e)
        {
            return null;
        }


        static void foo<T>(T e)
        {
            var a = Class1.ToArray(e);
        }
    }
}
