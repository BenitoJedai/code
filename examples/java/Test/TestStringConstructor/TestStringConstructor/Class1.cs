using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]

namespace TestStringConstructor
{
    public class Class1
    {
        static void foo()
        {
            var x = new string('a', 8);

        }

    }

    [Script(
    Implements = typeof(global::System.String),
    ImplementationType = typeof(global::java.lang.String),
    InternalConstructor = true

    )]
    internal class __String
    {
        public __String(char c, int count)
        {
        }

        public static string InternalConstructor(char c, int count)
        {
            throw null;
        }
    }
}

namespace java.lang
{
    // http://docs.oracle.com/javase/1.5.0/docs/api/java/lang/String.html
    // http://developer.android.com/reference/java/lang/String.html
    [Script(IsNative = true)]
    public sealed class String
    { }
}