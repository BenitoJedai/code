using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Obfuscation(Feature = "script")]
namespace TestIsInterface
{
    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201309-1/20130904-iprogress

    //[Script(Implements = typeof(global::System.IProgress))]
    [Script(ImplementsViaAssemblyQualifiedName = "System.IProgress`1")]
    internal interface __IProgress<in T>
    {


        void Report(T value);
    }

    //[Script(Implements = typeof(global::System.Progress))]
    [Script(ImplementsViaAssemblyQualifiedName = "System.Progress`1")]
    internal class __Progress<T> : __IProgress<T>
    {
        void __IProgress<T>.Report(T value)
        {
        }
    }

    class X
    {
        public static void Invoke(object x)
        {
            var z = x is __IProgress<object>;

        }
    }
}
