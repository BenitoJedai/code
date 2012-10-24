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

    [Script(Implements = typeof(global::System.Collections.IEnumerator))]
    internal interface __IEnumerator
    {
        void Reset();
    }

    [Script(IsArrayEnumerator = true)]
    public class SZArrayEnumerator<T> : __IEnumerable<T>, __IEnumerator
    {
        public void Reset()
        {
            throw new global::System.Exception("The method or operation is not implemented.");
        }

        public static implicit operator SZArrayEnumerator<T>(T[] e)
        {
            if (e == null)
                return null;

            return new SZArrayEnumerator<T>();
        }
    }



    [Script(
   HasNoPrototype = true,
  Implements = typeof(global::System.Exception),
  ImplementationType = typeof(java.lang.Throwable))]
    //ImplementationType = typeof(java.lang.Exception))]
    internal class __Exception
    {
        public __Exception() { }
        public __Exception(string e) { }
        public string Message
        {
            [Script(ExternalTarget = "getMessage")]
            get { return default(string); }
        }
    }
}

namespace java.lang
{
    // http://java.sun.com/j2se/1.4.2/docs/api/java/lang/Exception.html
    [Script(IsNative = true)]
    public class Throwable
    {


    }
}