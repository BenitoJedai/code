using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

[assembly: Obfuscation(Feature = "script")]

namespace TestAnonymousType
{
    public class Class1 : global::ScriptCoreLibJava.IAssemblyReferenceToken
    {
        static void foo()
        {
            var u = new { x = 5, y = "hey", z = new object() };

            var q = u.ToString();

            
        }
    }
}
