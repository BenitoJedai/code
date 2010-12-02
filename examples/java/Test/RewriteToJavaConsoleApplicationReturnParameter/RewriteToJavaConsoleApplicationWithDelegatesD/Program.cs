#define FEATURE_DELEGATES

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Threading;

namespace RewriteToJavaConsoleApplicationWithDelegatesD
{
    class Program
    {
        public static void Main(string[] args)
        {
#if FEATURE_DELEGATES
            ExtensionsToSwitchToCLRContext2.Method1(
                () => "from jvm!", new Foo()
            );
#else
            ExtensionsToSwitchToCLRContext2.Method2(new Tuple { Text = "world" });
#endif
        }
    }

    delegate string GetString();

    class Tuple
    {
        public string Text;
    }

    interface IFoo
    {
        string Invoke1(string e);
        void Invoke3();
        string Invoke2(string e);
    }

    [SwitchToCLRContext]
    static class ExtensionsToSwitchToCLRContext2
    {


#if FEATURE_DELEGATES
        public static void Method1(GetString f, IFoo foo)
        {
            Console.WriteLine("Method1");

            Console.WriteLine("foo: " + foo.Invoke1("clr"));
            Console.WriteLine("GetString: " + f());
            Console.WriteLine("GetString: " + f());
            Console.WriteLine("GetString: " + f());
        }
#else
        public static void Method2(Tuple e)
        {
            Console.WriteLine("hello " + e.Text);
        }
#endif
    }
}
