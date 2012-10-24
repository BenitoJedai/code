using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestArrayAsIEnumerable
{
    public class Class1
    {
        public static IEnumerable<object> foo(object[] e)
        {
            var x = (IEnumerable<object>)e;
            bar(e);

            return e;
        }

        public static void bar(IEnumerable<object> e)
        {

        }
    }



    [Script(Implements = typeof(global::System.Collections.Generic.IEnumerable<>))]
    internal interface __IEnumerable<T>
    {
    }

    [Script(IsArrayEnumerator = true)]
    public class SZArrayEnumerator<T> : __IEnumerable<T>
    {
        public static implicit operator SZArrayEnumerator<T>(T[] e)
        {
            if (e == null)
                return null;

            return new SZArrayEnumerator<T>();
        }
    }
}
