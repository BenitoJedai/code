using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Script(IsCoreLib = true)]
[assembly: Obfuscation(Feature = "script")]

namespace TestStringDomainMemory
{
    public class Class1
    {
        public static string foo = "foo1";
        public static object goo = new object();

        public Class1()
        {
            var foo1 = foo;

            foo = "bar";

            var goo1 = goo;

            goo = this;
        }

    }
}
