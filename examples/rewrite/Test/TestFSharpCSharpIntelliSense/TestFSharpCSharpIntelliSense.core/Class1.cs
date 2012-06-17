using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TestFSharpCSharpIntelliSense.core
{
    [Obfuscation(Exclude = true)]
    public class Class2
    {
        public static void Foo()
        {
            Class1.Foo();
        }
    }

    public class Class1
    {
        public static void Foo()
        {
            Bar();
            Bar_a();
            Bar_b();
            Bar_private();
            Bar_private_a();
        }

        internal static int a;

        internal static void Bar(string x = "Bar")
        {
            // pure method
        }

        internal static void Bar_a(string x = "Bar_a")
        {
            // pure method with static variable
            a++;
        }

        private static int b;

        internal static void Bar_b(string x = "Bar_b")
        {
            // pure method with static variable
            b++;
        }


        private static void Bar_private(string x = "Bar_private")
        {
            // pure method
        }

        private static void Bar_private_a(string x = "Bar_private_a")
        {
            // pure method with static variable
            a++;
        }

        public static int Property1 { get; set; }
        static int Property2 { get; set; }
    }
}
