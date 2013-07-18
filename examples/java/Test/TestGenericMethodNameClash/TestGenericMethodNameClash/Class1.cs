using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]
namespace TestGenericMethodNameClash
{
    // internal interface __IEquatable<T>
    //{
    //    bool Equals(T other);
    //}

    public static class __XEnumerable_GenericMethod
    {
        public static int Sum<T>(IFunc<T, int> e)
        {
            return 0;
        }

        public static double Sum<T>(IFunc<T, double> e)
        {
            return 0;
        }
    }

    public static class Test__XEnumerable_GenericMethod
    {
        public static void Invoke(
            IFunc<object, int> a,
            IFunc<object, double> b,
            IFunc<int, double> c)
        {
            __XEnumerable_GenericMethod.Sum(a);
            __XEnumerable_GenericMethod.Sum(b);
            __XEnumerable_GenericMethod.Sum(c);
        }
    }

    public interface IFunc<T, R>
    {

    }

    public interface IFoo<T>
    {
        void XFoo(IFoo<T> e);
    }

    public class Class1

        // 1>  Unhandled Exception: System.NotSupportedException: The interface was not marked as [ScriptAttribute.ExplicitInterface].
        // { SourceInterface = TestGenericMethodNameClash.IFoo`1[System.Int32], SourceType = TestGenericMethodNameClash.Class1, TargetMethod = Void XFoo(TestGenericMethodNameClash.IFoo`1[System.Int32]) }
    : IFoo<int>
    //, 
    // IFoo<double>
    {
        public static int Foo(IFoo<int> e)
        {
            return 0;
        }

        public static double Foo(IFoo<double> e)
        {
            return 0;
        }

        public static void YFoo<T>(T e)
        {
        }

        public static void YFoo(object e)
        {
        }



        public void XFoo(IFoo<int> e)
        {
        }

        //public void XFoo(IFoo<double> e)
        //{
        //}
    }
}
