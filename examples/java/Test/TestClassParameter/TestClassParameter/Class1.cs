using java.lang;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
[assembly: Script, ScriptTypeFilter(ScriptType.Java)]
namespace TestClassParameter
{
    [Script(IsNative = true)]
    public class Foo
    {
        public Foo(Class a)
        {

        }

        public Foo(Type a)
        {

        }
    }

    public class Class1
    {
        public static void Invoke()
        {
            var t = typeof(Class1);

            var f = new Foo(t);
        }
    }
}

namespace java.lang
{
    [Script(IsNative = true)]
    public class Class
    {

    }
}