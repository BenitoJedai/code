using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestExplicitInterface
{
    [ScriptAttribute.ExplicitInterface]
    interface IInterface2
    {
        void bar();
        int bari();
    }
    [ScriptAttribute.ExplicitInterface]
    interface IFunc1<T>
    {
        T barii(T t1);
    }

    [ScriptAttribute.ExplicitInterface]
    interface IFunc2<T>
    {
        T barii2(T t2);
    }


    [ScriptAttribute.ExplicitInterface]
    interface IFunc1_System_String_
    {
        string barii();
    }

    [ScriptAttribute.ExplicitInterface]
    interface IInterface
    {
        void foo();
    }

    [Script(Implements = typeof(global::System.IDisposable))]
    internal interface __IDisposable
    {
        void Dispose();
    }


    public class Class1 :
        IInterface,
        IInterface2,
        //IDisposable,
        __IDisposable,
        //IFunc1<string>,
        IFunc1_System_String_
        , IFunc1<int>
        , IFunc2<int>
    {
        void IInterface.foo()
        {
        }

        public void bar()
        {
        }

        public void Dispose()
        {
        }

        void __IDisposable.Dispose()
        {
        }


        public int bari()
        {
            return 0;
        }

        //string IFunc1<string>.barii()
        public string barii()
        {
            return "";
        }

        //int IFunc1<int>.barii()
        //{
        //    return 0;
        //}

        string IFunc1_System_String_.barii()
        //public string barii()
        {
            return "";
        }

        public int barii(int t)
        {
            return t;
        }

        public int barii2(int t)
        {
            return t;
        }
    }

    public static class Program
    {
        public static void Main(string[] e)
        {
            var u = new Class1();



        }
    }

}
