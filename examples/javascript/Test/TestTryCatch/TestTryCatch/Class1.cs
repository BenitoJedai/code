using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
[assembly: Obfuscation(Feature = "script")]
namespace TestTryCatch
{
    public static class Class1
    {
        public static void Bar()
        {
        }

        static bool x;

        public static void __Catch()
        {
            Bar();
            try
            {
                Bar();
                if (x)
                    Bar();
            }
            catch
            {
                Bar();
            }
            Bar();
        }

        public static void __Finally()
        {
            Bar();
            try
            {
                Bar();
                if (x)
                    Bar();
            }
            finally
            {
                Bar();
            }
            Bar();
        }
    }
}
