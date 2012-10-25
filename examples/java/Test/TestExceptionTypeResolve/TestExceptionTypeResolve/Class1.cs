using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Obfuscation(Feature = "script")]

namespace TestExceptionTypeResolve
{
    [Script]
    public class Class2<T>
    {
    }

    [Script]
    public class Class1
    {
        static bool nop()
        {
            return true;
        }

        public static InvalidOperationException foo(
            InvalidOperationException __java_lang_RuntimeException,
            Class1 __Class1,
            IDisposable __IDisposable,
            string __String,
            int __int,
            bool __boolean,
            object __Object,
            KeyValuePair<Class1, int> __Class1_Class1_Integer,
            KeyValuePair<Class1, InvalidOperationException> __Class1_Class1_InvalidOperationException,
            Class2<KeyValuePair<Class1, InvalidOperationException>> __Class_1
            )
        {
            var x = new KeyValuePair<Class1, InvalidOperationException>();


            var __Class1_Class1_Integer_value = __Class1_Class1_Integer.Value;
            var __Class1_Class1_InvalidOperationException_value = __Class1_Class1_InvalidOperationException.Value;
 
            //__Class_1 = new Class2<KeyValuePair<Class1, InvalidOperationException>>();

 
            //nop();
            //try
            //{
            //    nop();

            //    //var iii = new[,] { { new InvalidOperationException(), new InvalidOperationException() } };
            //    var ii = new[] { new InvalidOperationException() };
            //    throw new InvalidOperationException();
            //    nop();
            //}
            //catch (Exception __throwable__)
            //{
            //    var u = (Exception)__throwable__;
            //    var i = __throwable__ as InvalidOperationException;


            //    nop();
            //    // this will cause a problem
            //    //throw;
            //    //nop();
            //}
            //nop();

            return null;
        }
    }

    [Script(Implements = typeof(global::System.Collections.Generic.KeyValuePair<,>))]
    internal class __KeyValuePair<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public __KeyValuePair()
            : this(default(TKey), default(TValue))
        {

        }

        // does this work for PHP?
        public __KeyValuePair(TKey Key, TValue Value)
        {
            this.Key = Key;
            this.Value = Value;
        }


    }

    [Script(Implements = typeof(global::System.IDisposable))]
    internal interface __IDisposable
    {
        void Dispose();
    }

    [Script(
        HasNoPrototype = true,
        Implements = typeof(global::System.InvalidOperationException)
        , ImplementationType = typeof(java.lang.RuntimeException)
    )]
    internal class __InvalidOperationException
    {

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
    // http://java.sun.com/j2se/1.4.2/docs/api/java/lang/RuntimeException.html
    [Script(IsNative = true)]
    public class RuntimeException
    {
    }
}

namespace java.lang
{
    // http://java.sun.com/j2se/1.4.2/docs/api/java/lang/Throwable.html
    [Script(IsNative = true)]
    public class Throwable
    {
        /// <summary>
        /// Returns the detail message string of this throwable.
        /// </summary>
        /// <returns></returns>
        public string getMessage()
        {
            return default(string);
        }

        public void printStackTrace()
        {

        }
    }
}
