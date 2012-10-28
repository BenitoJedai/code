using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

[assembly: Obfuscation(Feature = "script")]




namespace TestGenericConstraint
{
    namespace foo
    {
        public interface IClass2
        {
            void Foo();
        }

        public interface IClass3<T>
        {
            void Foo();
        }

    }

    public interface IClass3<T>
    {
        void Foo();
    }

    public interface IClass1
    {
        void Foo();
    }

    // TestGenericConstraint.foo.IClass3_1 is already defined in a single-type import
    public class Class1 : IClass1, foo.IClass2, TestGenericConstraint.IClass3<string>, foo.IClass3<object>
    {
        public void Foo()
        {
            new Class1<Class1>(this);
        }

        public static T Invoke<T>(T e) where T : Class1, IClass1, foo.IClass2
        {
            e.Foo();
            return e;
        }
    }

    public class Class1<T> where T : 
        Class1, 
        IClass1, foo.IClass2, TestGenericConstraint.IClass3<string>, foo.IClass3<object>
    {
        public Class1(T e)
        {
            e.Foo();

        }

    }
}
