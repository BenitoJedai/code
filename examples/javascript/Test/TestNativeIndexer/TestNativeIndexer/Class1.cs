using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ScriptCoreLib;
[assembly: Obfuscation(Feature = "script")]
namespace TestNativeIndexer
{
    [Script(HasNoPrototype = true)]
    public class Class1Base
    {
        public string this[byte e]
        {
            set
            {
            }
        }
    }


    [Script(HasNoPrototype = true)]
    public class Class1 : Class1Base
    {
        public string this[int e]
        {
            set
            {
            }
        }
    }

    class MyClass
    {
        public MyClass()
        {
            var c = default(Class1);
            
            c[5] = "a";
            c[(byte)5] = "x";
        }
    }
}
