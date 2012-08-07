using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

[assembly: Obfuscation(Feature = "script")]


namespace foo
{
        public interface IClass2
    {
        void Foo();
    }
}

namespace TestGenericConstraint
{
    using foo;

    public interface IClass1
    {
        void Foo();
    }

    public class Class1 : IClass1, IClass2
    {
        public void Foo()
        {
            new Class1<Class1>(this);
        }

        public static T Invoke<T>(T e) where T : Class1, IClass1, IClass2
        {
            e.Foo();
            return e;
        }
    }

    public class Class1<T> where T : Class1, IClass1, IClass2
    {
        public Class1(T e)
        {
            e.Foo();

        }

    }
}
