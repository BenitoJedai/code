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


    public class Class1 : IInterface, IInterface2, IDisposable
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
    }
}
