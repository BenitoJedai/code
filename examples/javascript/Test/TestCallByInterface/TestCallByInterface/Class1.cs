using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Script, ScriptTypeFilter(ScriptType.JavaScript)]

namespace TestCallByInterface
{
    public interface IBCLClass1
    {
        object Foo(object e);
    }

    [Script(Implements = typeof(IBCLClass1))]
    public interface __IBCLClass1
    {
        object Foo(object e);
    }



    [Script]
    public interface IClass1
    {
         object Foo(object e);
    }

    [Script]
    public class Class1 : IClass1, IBCLClass1
    {
        public object Foo(object e)
        {
            return e;
        }
    }

    [Script]
    public static class Class2
    {
        static void InvokeFoo(this IClass1 e, IBCLClass1 x)
        {
            e.Foo(null);
            x.Foo(null);
        }
    }
}
