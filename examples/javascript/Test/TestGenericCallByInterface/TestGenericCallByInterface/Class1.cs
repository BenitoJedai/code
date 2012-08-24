using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]

namespace TestGenericCallByInterface
{
    public interface IClass1<T>
    {
        T Foo(T e);
    }

    public class Class1 : IClass1<object>
    {
        public object Foo(object e)
        {
            return e;
        }
    }

    public static class Class2
    {
        static T InvokeFoo<T>(this IClass1<T> e, T t)
        {
            return e.Foo(t);
        }
    }
}
