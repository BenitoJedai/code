using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace TestNamespacedAnonymousTypes
{
    public class Class1
    {

        //Implementation not found for type import :
        //type: System.Text.StringBuilder
        //method: Void .ctor()
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!

        static void foo()
        {

            var goo = new { };

            //goo.
        }
    }
}
