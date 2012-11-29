using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Script,
ScriptTypeFilter(ScriptType.JavaScript),
ScriptTypeFilter(ScriptType.ActionScript)
]

namespace TestBCLImplementationVirtualMethod
{
    public class Class1
    {
        public virtual void foo()
        {

        }

        public virtual void foon(string ax, ref string bx)
        {

        }
    }

    [Script(Implements = typeof(Class1))]
    public class Class2
    {
        public virtual void foo()
        {

        }

        public virtual void foon(string ax, ref string bx)
        {

        }
    }

    [Script]
    public class Class3 : Class2
    {
        public override void foo()
        {

        }
    }

    [Script]
    public class Class5 : Class1
    {
        public override void foo()
        {

        }

        public override void foon(string ax, ref string bx)
        {

        }
    }


    [Script]
    public class Class4
    {
        public static void foo(Class2 x)
        {
            x.foo();
        }

        public static void foo(Class1 x)
        {
            x.foo();
        }
    }
}
